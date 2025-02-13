/*
 * Copyright © 2016-2025 EDDiscovery development team
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this
 * file except in compliance with the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software distributed under
 * the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
 * ANY KIND, either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 */

using BaseUtils.Win32;
using BaseUtils.Win32Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{

    /// <summary>
    /// A customized form offering a fully functional System Menu for everyone. <see cref="WM.SYSCOMMAND"/> 
    /// </summary>
    public class SmartSysMenuForm : Form
    {
        /// <summary>
        /// Smart menu item - separator, checked item, unchecked item, submenu 
        /// </summary>
        public class SystemMenuItem
        {
            public string Text { get; set; }            // null for separator
            public bool Checked { get; set; }           // Check state

            // called on menu open
            public Action<SystemMenuItem> OnShown { get; set; }

            // called on menu clicked
            public Action<SystemMenuItem> OnClick { get; set; }

            // submenu
            public List<SystemMenuItem> Children { get; set; } = null;

            // Tag
            public Object Tag { get; set; }
            public SmartSysMenuForm Form { get; set; }  // set on install, associated form
            internal IntPtr MenuHandle { get; set; }    // set on install (only for this set of code to set)
            internal int ID { get; set; } = -1;        // set on install
            public SystemMenuItem()     // separator
            {
                Text = null;
            }

            // menu item, optionally checked
            public SystemMenuItem(string text, Action<SystemMenuItem> onShown, Action<SystemMenuItem> onClick, bool checkedp = false) 
            {
                Text = text;
                OnShown = onShown;
                OnClick = onClick;
                Checked = checkedp;
            }
            public SystemMenuItem(string text, List<SystemMenuItem> children)   // submenu
            {
                Text = text;
                Children = children;
            }
            public void SetCheck(bool value)
            {
                Checked = value;
                UnsafeNativeMethods.CheckMenuItem(MenuHandle, ID, Checked ? MF.CHECKED : MF.UNCHECKED);
            }
            public void ToggleCheck()
            {
                Checked = !Checked;
                UnsafeNativeMethods.CheckMenuItem(MenuHandle, ID, Checked ? MF.CHECKED : MF.UNCHECKED);
            }
        }

        /// <summary>
        /// Set top most, calls OnTopMostChanged
        /// </summary>

        [DefaultValue(false)]
        public new bool TopMost
        {
            get { return base.TopMost; }
            set
            {
                if (base.TopMost != value)
                {
                    base.TopMost = value;
                    OnTopMostChanged(this);
                }
            }
        }

        public Action<SmartSysMenuForm> TopMostChanged;

        /// <summary>
        /// Allow resize operation
        /// </summary>
        protected virtual bool AllowResize { get; set; } = true;


        /// <summary>
        /// Install more menu items to system menu
        /// </summary>
        /// <param name="list"></param>
        /// <summary>
        /// Install more menu items to system menu
        /// </summary>
        /// <param name="list"></param>
        public void InstallSystemMenuItems(List<SystemMenuItem> list)
        {
            IntPtr hSysMenu = UnsafeNativeMethods.GetSystemMenu(Handle, false); // get handle to system menu
            InstallItems(list, hSysMenu);
        }

        #region Implementation

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                if (_SysMenuCreationHackEnabled)
                    cp.Style |= WS.SYSMENU;
                return cp;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                TopMostChanged = null;
            base.Dispose(disposing);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                UnsafeNativeMethods.GetSystemMenu(Handle, true);        // reset system menu
                InstallStandardSystemMenuItems();
            }
        }

        protected virtual void OnTopMostChanged(SmartSysMenuForm form)
        {
            TopMostChanged?.Invoke(this);
        }

        protected IntPtr SendMessage(int msg, IntPtr wparam, IntPtr lparam)
        {
            Message message = Message.Create(this.Handle, msg, wparam, lparam);
            this.WndProc(ref message);
            return message.Result;
        }

        protected void ShowSystemMenu(Point screenPt)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT && IsHandleCreated)
            {
                var hMenu = UnsafeNativeMethods.GetSystemMenu(Handle, false);
                if (hMenu != IntPtr.Zero)
                {
                    int cmd = UnsafeNativeMethods.TrackPopupMenuEx(hMenu, UnsafeNativeMethods.GetSystemMetrics(SystemMetrics.MENUDROPALIGNMENT) | TPM.RETURNCMD,
                        screenPt.X, screenPt.Y, Handle, IntPtr.Zero);
                    if (cmd != 0)
                        UnsafeNativeMethods.PostMessage(Handle, WM.SYSCOMMAND, (IntPtr)cmd, IntPtr.Zero);
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM.CREATE:         // Win32: set _SysMenuCreationHackEnabled to ensure that we get a system menu at startup.
                    {
                        _SysMenuCreationHackEnabled = Environment.OSVersion.Platform == PlatformID.Win32NT;
                        base.WndProc(ref m);
                        _SysMenuCreationHackEnabled = false;
                        return;
                    }

                case WM.DESTROY:        // Win32: certain events (changing ShowInTaskBar) may destroy us. Re-enable the hack.
                    {
                        _SysMenuCreationHackEnabled = Environment.OSVersion.Platform == PlatformID.Win32NT;
                        break;
                    }

                case WM.INITMENU:       // Win32: refresh the system menu before displaying it.
                    {
                        base.WndProc(ref m);    // Base should always get first crack at this.

                        if (m.WParam != IntPtr.Zero && Environment.OSVersion.Platform == PlatformID.Win32NT && IsHandleCreated)
                        {
                            bool maximized = WindowState == FormWindowState.Maximized;
                            // these don't matter too much, but it helps the user to know if something isn't allowed. WM_SYSCOMMAND is where we verify.
                            UnsafeNativeMethods.EnableMenuItem(m.WParam, SC.RESTORE,  AllowResize &&  maximized ? MF.ENABLED : MF.GRAYED);
                            UnsafeNativeMethods.EnableMenuItem(m.WParam, SC.SIZE,     AllowResize && !maximized ? MF.ENABLED : MF.GRAYED);
                            UnsafeNativeMethods.EnableMenuItem(m.WParam, SC.MINIMIZE, AllowResize               ? MF.ENABLED : MF.GRAYED);
                            UnsafeNativeMethods.EnableMenuItem(m.WParam, SC.MAXIMIZE, AllowResize && !maximized ? MF.ENABLED : MF.GRAYED);

                            if (FormBorderStyle == FormBorderStyle.None) // base.WndProc() is useless...
                            {
                                UnsafeNativeMethods.EnableMenuItem(m.WParam, SC.MOVE, maximized ? MF.GRAYED : MF.ENABLED);
                                UnsafeNativeMethods.EnableMenuItem(m.WParam, SC.CLOSE, MF.ENABLED);
                            }

                            // initialise any items needed
                            foreach (var entry in systemmenuitems)
                                entry.Value.OnShown?.Invoke(entry.Value);

                            // This only works reliably on the application's MainForm.
                            UnsafeNativeMethods.SetMenuDefaultItem(m.WParam, maximized && AllowResize ? SC.RESTORE : (!maximized && AllowResize ? SC.MAXIMIZE : SC.CLOSE), 0);
                            m.Result = IntPtr.Zero;
                        }
                        return;
                    }

                case WM.NCRBUTTONUP:    // Win32: Display the system menu.
                    {
                        if (FormBorderStyle == FormBorderStyle.None && m.WParam == (IntPtr)HT.CAPTION)
                        {
                            ShowSystemMenu(new Point((int)(long)m.LParam));
                            m.Result = IntPtr.Zero;
                            return;
                        }
                        break;
                    }

                case WM.SYSCOMMAND:     // Process any system commands intended for this window (SC_ONTOP / SC_OPACITYSUBMENU).
                    {
                        int wp = unchecked((int)(long)m.WParam);

                        if (m.WParam == (IntPtr)SC.KEYMENU && m.LParam == (IntPtr)' ' && (CreateParams.Style & WS.SYSMENU) == 0 && (CreateParams.Style & WS.CAPTION) == 0)
                            ShowSystemMenu(PointToScreen(new Point(5, 5)));
                        else if (!AllowResize && (m.WParam == (IntPtr)SC.MAXIMIZE || m.WParam == (IntPtr)SC.SIZE || m.WParam == (IntPtr)SC.RESTORE))
                            return;     // Access Denied.
                        else if (systemmenuitems.TryGetValue(wp, out SystemMenuItem smi))
                            smi.OnClick?.Invoke(smi);
                        else
                            break;

                        m.Result = IntPtr.Zero;
                        return;
                    }
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// Install standard system menu items, for now, always there
        /// </summary>
        private void InstallStandardSystemMenuItems()
        {
            var sysMenus = new List<SystemMenuItem>()
            {
                new SystemMenuItem(),
                new SystemMenuItem("On &Top", x=> { x.SetCheck(x.Form.TopMost); }, x=>{ x.Form.TopMost = !x.Form.TopMost; }),
                new SystemMenuItem("&Opacity",
                                new List<SystemMenuItem>() {
                                        new SystemMenuItem("100%", x=> { int opac = (int)Math.Round(Opacity * 10); x.SetCheck(opac==10);}, x => { x.Form.Opacity = 1;  }),
                                        new SystemMenuItem("90%",  x=>{ int opac = (int)Math.Round(Opacity * 10); x.SetCheck(opac==9);}, x => { x.Form.Opacity = 0.9;  }),
                                        new SystemMenuItem("80%", x=> { int opac = (int)Math.Round(Opacity * 10); x.SetCheck(opac==8);}, x => { x.Form.Opacity = 0.8;  }),
                                        new SystemMenuItem("70%", x=> { int opac = (int)Math.Round(Opacity * 10); x.SetCheck(opac==7);}, x => { x.Form.Opacity = 0.7;  }),
                                        new SystemMenuItem("60%", x=> { int opac = (int)Math.Round(Opacity * 10); x.SetCheck(opac==6);}, x => { x.Form.Opacity = 0.6;  }),
                                        new SystemMenuItem("50%", x=> { int opac = (int)Math.Round(Opacity * 10); x.SetCheck(opac==5);}, x => { x.Form.Opacity = 0.5;  }),
                                        new SystemMenuItem("40%", x=> { int opac = (int)Math.Round(Opacity * 10); x.SetCheck(opac==4);}, x => { x.Form.Opacity = 0.4;  }),
                                        new SystemMenuItem("30%", x=> { int opac = (int)Math.Round(Opacity * 10); x.SetCheck(opac==3);}, x => { x.Form.Opacity = 0.3;  }),
                                        new SystemMenuItem("20%", x=> { int opac = (int)Math.Round(Opacity * 10); x.SetCheck(opac==2);}, x => { x.Form.Opacity = 0.2;  }),
                                        new SystemMenuItem("10%", x=> { int opac = (int)Math.Round(Opacity * 10); x.SetCheck(opac==1);}, x => { x.Form.Opacity = 0.1;  }),
                                }),
            };

            InstallSystemMenuItems(sysMenus);
        }

        private void InstallItems(List<SystemMenuItem> list, IntPtr hSysMenu)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT && IsHandleCreated)
            {
                foreach (var entry in list)
                {
                    entry.MenuHandle = hSysMenu;

                    if (entry.Children != null)
                    {
                        // we add this non clickable (therefore won't ever be reported) item to the array solely so that it can be found if required later
                        systemmenuitems[additionalentryno++] = entry;

                        IntPtr hSubmenu = UnsafeNativeMethods.CreateMenu();

                        UnsafeNativeMethods.AppendMenu(hSysMenu, MF.STRING | MF.POPUP, (int)hSubmenu, entry.Text);

                        InstallItems(entry.Children, hSubmenu);
                    }
                    else if (entry.Text != null)
                    {
                        entry.ID = additionalentryno;
                        entry.Form = this;
                        systemmenuitems[entry.ID] = entry;
                        UnsafeNativeMethods.AppendMenu(hSysMenu, entry.Checked == true ? MF.CHECKED : MF.UNCHECKED, additionalentryno++, entry.Text);
                    }
                    else
                        UnsafeNativeMethods.AppendMenu(hSysMenu, MF.SEPARATOR, 1, "");
                }
            }
            else
            {
                System.Diagnostics.Trace.WriteLine("SmarkSysMenuForm install items before handle created or not on WIN32");
            }
        }

        // If WS.SYSMENU is not active at first WM.CREATE, the menu will not be created. Since FormBorderStyle may clear WS.SYSMENU, we have
        // to fake it during startup. WS.SYSMENU is meaningless to us outside of WM.CREATE. Seealso https://stackoverflow.com/a/16695606
        // CAUTION: if WS.SYSMENU is enabled but WS.CAPTION is not, all hittests, including our sysmenu, min/max/close, etc., will get ignored!
        private bool _SysMenuCreationHackEnabled = Environment.OSVersion.Platform == PlatformID.Win32NT;

        // 0x000D-0x001F are reserved by us for future expansion, while 0x0000 and 0xF000+ are system reserved.

        protected const int SC_ADDITIONALMENU = 0x0020;
        private int additionalentryno = SC_ADDITIONALMENU;
        Dictionary<int, SystemMenuItem> systemmenuitems = new Dictionary<int, SystemMenuItem>();       // map system menu no to entry

        #endregion

    }
}
