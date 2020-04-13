/*
 * Copyright © 2017-2019 EDDiscovery development team
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
 * 
 * EDDiscovery is not affiliated with Frontier Developments plc.
 */

using BaseUtils.Win32;
using BaseUtils.Win32Constants;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class DraggableForm : SmartSysMenuForm
    {
        //The number of pixels from the top of the <see cref="SmartSysMenuForm"/> that will be interpreted as caption
        // area when the <see cref="SmartSysMenuForm"/> is unframed. The default value is <c>28</c>.
        public uint CaptionHeight { get; set; } = 28;

        protected virtual bool WinLeftRightEnabledInBorderless { get; } = true;     // override to disable borderless intercept

        public DraggableForm()
        {
            dblClickTimer = new Timer();
            dblClickTimer.Tick += (o, e) => { ((Timer)o).Enabled = false; };
            screenmc = new BaseUtils.WindowMovementControl(this);
            KeyPreview = true;
        }

        // call this in a mouse down handler to make it move the window

        public void OnCaptionMouseDown(Control sender, MouseEventArgs e)
        {
            sender.Capture = false;
            if (FormBorderStyle == FormBorderStyle.None)
            {
                var ptScreen = sender.PointToScreen(e.Location);
                var ptForm = this.PointToClient(ptScreen);

                if (ptForm.Y >= 0 && ptForm.Y < this.CaptionHeight)
                {
                    var lParam = unchecked((IntPtr)((ushort)ptScreen.X | ((ushort)ptScreen.Y << 16)));

                    switch (e.Button)
                    {
                        case MouseButtons.Left: SendMessage(WM.NCLBUTTONDOWN, (IntPtr)HT.CAPTION, lParam); break;
                        case MouseButtons.Middle: SendMessage(WM.NCMBUTTONDOWN, (IntPtr)HT.CAPTION, lParam); break;
                        case MouseButtons.Right: SendMessage(WM.NCRBUTTONDOWN, (IntPtr)HT.CAPTION, lParam); break;
                    }
                }
            }
        }

        public void OnCaptionMouseUp(Control sender, MouseEventArgs e)
        {
            if (FormBorderStyle == FormBorderStyle.None)
            {
                var ptScreen = sender.PointToScreen(e.Location);
                var ptForm = this.PointToClient(ptScreen);

                if (ptForm.Y >= 0 && ptForm.Y < this.CaptionHeight)
                {
                    var lParam = unchecked((IntPtr)((ushort)ptScreen.X | ((ushort)ptScreen.Y << 16)));

                    switch (e.Button)
                    {
                        case MouseButtons.Left: SendMessage(WM.NCLBUTTONUP, (IntPtr)HT.CAPTION, lParam); break;
                        case MouseButtons.Middle: SendMessage(WM.NCMBUTTONUP, (IntPtr)HT.CAPTION, lParam); break;
                        case MouseButtons.Right: SendMessage(WM.NCRBUTTONUP, (IntPtr)HT.CAPTION, lParam); break;
                    }
                }
            }
        }

        #region Private implementation

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;

                // Allow minimize/restore operations to occur by clicking on the TaskBar icon, even when unframed.
                if (MinimizeBox && ShowInTaskbar)
                {
                    cp.ClassStyle |= CS.DBLCLKS;
                    cp.Style |= WS.MINIMIZEBOX; // using these turn on winkey - up/down functionality
                }

                return cp;
            }
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            bool windowsborder = this.FormBorderStyle != FormBorderStyle.None;

            if ( !windowsborder && WinLeftRightEnabledInBorderless && (m.Msg == WM.KEYDOWN || m.Msg == WM.KEYUP ))     // it also sends WM.CHAR
            {
                Keys key = (Keys)m.WParam;
                bool keydown = m.Msg == WM.KEYDOWN;

                //System.Diagnostics.Debug.WriteLine("Keypreview " + m.Msg + " " + m.LParam.ToString("x") + " " + m.WParam + " = " + keydown + " " + key.ToString());

                switch (key)
                {
                    case Keys.LWin:
                        winkey_down = keydown;
                        break;

                    case Keys.Left:
                        if (winkey_down && !keydown)            // windows is not giving us the winkey + leftdown key, just the leftup.  So have to comprimise. 
                        {
                            screenmc.Align(true);
                        }
                        break;
                    case Keys.Right:
                        if (winkey_down && !keydown)
                        {
                            screenmc.Align(false);
                        }
                        break;

                        // up/down not implemented, windows will do that.  
                        // This does not work exactly as a border window since windows up/down functionality messes it up. 
                        // later we could try and emulate the lot. but this is good for now.
                }
            }

            return base.ProcessKeyPreview(ref m);
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dblClickTimer != null)
                {
                    dblClickTimer.Enabled = false;
                    dblClickTimer.Tag = null;
                    dblClickTimer.Dispose();
                    dblClickTimer = null;
                }
            }
            base.Dispose(disposing);
        }


        protected override void WndProc(ref Message m)
        {
            bool windowsborder = this.FormBorderStyle != FormBorderStyle.None;
            //System.Diagnostics.Debug.WriteLine("Message {0:x} {1} {2}", m.Msg, m.LParam, m.WParam);

            switch (m.Msg)
            {
                case WM.GETMINMAXINFO:  // Set form min/max sizes when not using a windows border.
                    {
                        if (m.LParam != IntPtr.Zero && Environment.OSVersion.Platform == PlatformID.Win32NT && !windowsborder)
                        {
                            var sc = Screen.FromControl(this);
                            var scb = sc.Bounds;
                            var wa = sc.WorkingArea;
                            var mmi = Marshal.PtrToStructure<UnsafeNativeMethods.MINMAXINFO>(m.LParam);

                            mmi.ptMaxPosition.X = wa.Left - scb.Left;
                            mmi.ptMaxPosition.Y = wa.Top - scb.Top;

                            if (AllowResize)
                            {
                                if (!this.MaximumSize.IsEmpty)
                                {
                                    mmi.ptMaxSize.X = mmi.ptMaxTrackSize.X = this.MaximumSize.Width;
                                    mmi.ptMaxSize.Y = mmi.ptMaxTrackSize.Y = this.MaximumSize.Height;
                                }
                                else
                                {
                                    mmi.ptMaxSize.X = wa.Width;
                                    mmi.ptMaxSize.Y = wa.Height;
                                    //mmi.ptMaxSize.X = mmi.ptMaxTrackSize.X = wa.Width;
                                    //mmi.ptMaxSize.Y = mmi.ptMaxTrackSize.Y = wa.Height;
                                }

                                if (!this.MinimumSize.IsEmpty)
                                {
                                    mmi.ptMinTrackSize.X = this.MinimumSize.Width;
                                    mmi.ptMinTrackSize.Y = this.MinimumSize.Height;
                                }
                                else
                                {
                                    mmi.ptMinTrackSize.X = UnsafeNativeMethods.GetSystemMetrics(SystemMetrics.CXMINTRACK);
                                    mmi.ptMinTrackSize.Y = UnsafeNativeMethods.GetSystemMetrics(SystemMetrics.CYMINTRACK);
                                }
                                //  System.Diagnostics.Debug.WriteLine("SET TRACK SIZE " + mmi.ptMinTrackSize + " - " + mmi.ptMaxTrackSize);
                            }
                            else
                            {
                                mmi.ptMaxSize.X = mmi.ptMaxTrackSize.X = mmi.ptMinTrackSize.X = ClientSize.Width;
                                mmi.ptMaxSize.Y = mmi.ptMaxTrackSize.Y = mmi.ptMinTrackSize.Y = ClientSize.Height;
                            }

                            Marshal.StructureToPtr(mmi, m.LParam, false);
                            m.Result = IntPtr.Zero;
                            //System.Diagnostics.Debug.WriteLine("MINMAX " + mmi.ptMaxSize);
                            return;
                        }
                        break;
                    }

                case WM.NCHITTEST:      // Windows honours NCHITTEST; Mono does not 
                    {
                        if (!AllowResize)
                        {
                            m.Result = (IntPtr)HT.CAPTION;
                        }
                        else
                        {
                            base.WndProc(ref m);

                            if (WindowState == FormWindowState.Minimized)
                                return;

                            var p = PointToClient(new Point(unchecked((int)m.LParam)));
                            const int CaptionHeight = 32;
                            const int edgesz = 5;   // 5 is generous.. really only a few pixels gets thru before the subwindows grabs them

                            int botarea = Controls.OfType<ExtStatusStrip>().FirstOrDefault()?.Height ?? Controls.OfType<StatusStrip>().FirstOrDefault()?.Height ?? CaptionHeight;

                            if (SizeGripStyle != SizeGripStyle.Hide && WindowState != FormWindowState.Maximized && (p.X + p.Y >= ClientSize.Width + ClientSize.Height - botarea))
                            {
                                m.Result = (IntPtr)HT.BOTTOMRIGHT;
                            }
                            else if (m.Result == (IntPtr)HT.CLIENT && !windowsborder)
                            {
                                int rw = 1, col = 1;

                                if (WindowState != FormWindowState.Maximized)
                                {
                                    if (p.Y <= edgesz)                                      // top edge
                                        rw = 0;
                                    else if (p.Y >= ClientSize.Height - edgesz)             // bottom edge
                                        rw = 2;

                                    if (p.X <= edgesz)                                      // left side
                                        col = 0;
                                    else if (p.X >= ClientSize.Width - edgesz)              // right side
                                        col = 2;
                                }
                                var htarr = new int[][]
                                {
                                    new int[] { HT.TOPLEFT, HT.TOP, HT.TOPRIGHT },
                                    new int[] { HT.LEFT, p.Y < CaptionHeight ? HT.CAPTION : HT.CLIENT, HT.RIGHT },
                                    new int[] { HT.BOTTOMLEFT, HT.BOTTOM, HT.BOTTOMRIGHT }
                                };
                                m.Result = (IntPtr)htarr[rw][col];
                                System.Diagnostics.Debug.WriteLine("Result " + rw + " " + col + " " +m.Result);
                            }
                        }

                        return;
                    }

                case WM.NCLBUTTONDOWN:  // Monitor and intercept double-clicks, ignoring the fact that it may occur over multiple controls with/without capture.
                    {
                        if (!windowsborder && m.WParam == (IntPtr)HT.CAPTION)
                        {
                            var p = new Point(unchecked((int)m.LParam));
                            if (dblClickTimer.Enabled && ((Rectangle)dblClickTimer.Tag).Contains(p))
                            {
                                dblClickTimer.Enabled = false;
                                dblClickTimer.Tag = Rectangle.Empty;
                                SendMessage(WM.NCLBUTTONDBLCLK, (IntPtr)HT.CAPTION, m.LParam);
                                m.Result = IntPtr.Zero;
                                return;
                            }
                            else
                            {
                                dblClickTimer.Enabled = false;
                                dblClickTimer.Interval = SystemInformation.DoubleClickTime;
                                var dblclksz = SystemInformation.DoubleClickSize;
                                var dblclkrc = new Rectangle(p, dblclksz);
                                dblclkrc.Offset(dblclksz.Width / -2, dblclksz.Height / -2);
                                dblClickTimer.Tag = dblclkrc;
                                dblClickTimer.Enabled = true;
                            }
                        }
                        break;
                    }

                case WM.SIZING:     // do not do WINDOWPOSCHANGED as this is a programatic change, we want user interaction
                    {
                        screenmc.ResetCentre();     // resize, reset centre knowledge 
                        break;
                    }
                case WM.MOVING:
                    {
                        screenmc.ResetCentre();     // moving, reset centre knowledge 
                        break;
                    }
            }
            base.WndProc(ref m);
        }

        private BaseUtils.WindowMovementControl screenmc;
        private bool winkey_down = false;
        private System.Windows.Forms.Timer dblClickTimer = null;

        #endregion
    }
}
