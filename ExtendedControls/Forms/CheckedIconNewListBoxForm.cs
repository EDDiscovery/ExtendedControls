/*
 * Copyright © 2024-2024 EDDiscovery development team
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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class CheckedIconNewListBoxForm : Form
    {
        public bool ApplyTheme { get; set; } = true;               // use to control if ALL or None is reported, else its all entries or empty list
        public bool CloseOnDeactivate { get; set; } = true;         // close on deactivate
        public bool HideOnDeactivate { get; set; } = false;         // hide instead
        public bool CloseOnChange { get; set; } = false;            // close when any is changed
        public bool HideOnChange { get; set; } = false;             // hide when any is changed
        public bool DeactivatedWithin(long ms)                      // have we deactivated in this timeperiod. use for debouncing buttons if hiding
        {
            return LastDeactivateTime.IsRunning && LastDeactivateTime.TimeRunning < ms; // if running, and we are within ms from the time it started running, we have deactivated within
        }
        public BaseUtils.MSTicks LastDeactivateTime { get; private set; } = new BaseUtils.MSTicks();    // .Running if deactivated

        public void PositionBelow(Control c) { SetLocation = c.PointToScreen(new Point(0, c.Height)); }
        public Point SetLocation { get; set; } = new Point(int.MinValue, -1);     // force to this location (SlideLeft/SlideUp may change this)
        public Size CloseBoundaryRegion { get; set; } = new Size(0, 0);     // set size >0 to enable boundary close

        public bool AllOrNoneBack { get; set; } = true;            // use to control if ALL or None is reported by GetChecked, else its all entries or empty list

        // Called on close or hide
        public Action<string, Object> SaveSettings;                

        // ** use UC.Add or UC.Remove to add/remove items. If you do, use ForceRedrawOnNextShow to indicate it needs a redraw
        public CheckedIconGroupUserControl UC { get; private set; }       

        public CheckedIconNewListBoxForm()
        {
            InitializeComponent();
            UC = new CheckedIconGroupUserControl();
            UC.Dock = DockStyle.Fill;
            Controls.Add(UC);
            timer.Interval = 100;
            timer.Tick += CheckMouse;
        }

        // Get the checked list taking into account the grouping And the AllOrNoneBack
        public string GetChecked()
        {
            return UC.GetChecked(AllOrNoneBack);
        }

        // Call if you altered the Item List and force a redraw on next Show.
        public void ForceRedrawOnNextShow()
        {
            forceredraw = true;
        }

        // show with settings as a list;list of tags under this control
        public void Show(string settings, Control positionunder, IWin32Window parent, Object tag = null)
        {
            UC.Set(settings);
            Tag = tag;
            PositionBelow(positionunder);
            Show(parent);
        }

        // show with settings as a list;list of tags at this point
        public void Show(string settings, Point point, IWin32Window parent, Object tag = null)         // quick form version
        {
            UC.Set(settings);
            Tag = tag;
            SetLocation = point;
            Show(parent);
        }

        // given an enum list, and a set of bools indicating if each is set or not, show
        public void Show(Type ofenum, bool[] ctrlset, Control positionunder, IWin32Window parent, Object tag = null)
        {
            string settings = "";

            foreach (var v in Enum.GetValues(ofenum))
            {
                if (ctrlset[(int)v])
                    settings += v.ToString() + UC.SettingsSplittingChar;
            }

            UC.Set(settings);
            Tag = tag;
            PositionBelow(positionunder);
            Show(parent);
        }

        // show using a long to control - the tags should be numeric decimal versions of the flags
        // if no flags are set, then you can use a default standard option
        public void Show(long value, Control positionunder, IWin32Window parent, Object tag = null, int setonfornoflags = -1)
        {
            string settings = "";
            for (int i = 0; i < 63; i++)
            {
                if ((value & (1L << i)) != 0)
                    settings += (1L << i).ToStringInvariant() + UC.SettingsSplittingChar;
            }

            if (settings == "" && setonfornoflags >= 0)     // if nothing on and we have a default
               settings = UC.ItemList[setonfornoflags].Tag;

            UC.Set(settings);
            Tag = tag;
            PositionBelow(positionunder);
            Show(parent);
        }

        // basic show
        public new void Show(IWin32Window parent)
        {
            if (ApplyTheme)
            {
                Theme.Current?.ApplyStd(this,true);
            }

            // if we moved from last redraw, or we were altered and we have displayed, or the font has changed
            // we won't OnLoad, so we need to redraw here
            if ((!lastbounds.IsEmpty && SetLocation != this.Location) || (forceredraw && !lastbounds.IsEmpty) || ( lastfont != null && lastfont != Font))
            {
                //System.Diagnostics.Debug.WriteLine($"ListBoxNewForm Previously shown but now moved/altered/font");
                ReDraw();
            }

            base.Show(parent);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //System.Diagnostics.Debug.WriteLine($"ListBoxNewForm OnLoad draw");
            ReDraw();
        }


        // called to redraw and then reposition dependent on what was drawn
        private void ReDraw()
        { 
            var location = SetLocation.X != int.MinValue ? SetLocation : Location;
            var rect = UC.Render(location);             
            Location = rect.Location;
            ClientSize = rect.Size;
            lastbounds = rect;
            lastfont = Font;
            forceredraw = false;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            LastDeactivateTime.Stop();          // indicate deact - this is really just to be nice with the .Running flag of the class

            //System.Diagnostics.Debug.WriteLine($"{BaseUtils.AppTicks.TickCountLap("P1")} Activated");

            if (!CloseBoundaryRegion.IsEmpty)
            {
                timer.Start();
            }
            else
            {
                //System.Diagnostics.Debug.WriteLine($"Warning a CheckedIconListBoxForm is not using CloseBoundary ${Environment.StackTrace}");
            }
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);

            //System.Diagnostics.Debug.WriteLine("Deactivate event start");

            LastDeactivateTime.Run();     // start timer..
            timer.Stop();
            SaveSettingsEvent();

            if (CloseOnDeactivate)
            {
                //System.Diagnostics.Debug.WriteLine("Close");
                this.Close();
            }
            else if (HideOnDeactivate)
            {
                if (Owner != null)
                {
                    //System.Diagnostics.Debug.WriteLine("Disassociate and hide start");
                    var o = Owner;          // calling Hide() when the owner is not ready to receive the focus causes windows to go and get another window to place
                    Owner = null;           // disassociating it temp from its owner seems to solve this. Probably because it can pick that window now.
                    Hide();
                    Owner = o;
                    //System.Diagnostics.Debug.WriteLine("Disassociate and hide end");
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine("No owner, hide");
                    Hide();
                }
            }

            //System.Diagnostics.Debug.WriteLine("Deactivate event end");
        }

        private void CloseOrHide()
        {
            if (CloseOnDeactivate)
            {
                // System.Diagnostics.Debug.WriteLine("Close or hide - close");
                this.Close();
            }
            else if (HideOnDeactivate)
            {
                //System.Diagnostics.Debug.WriteLine("Close or hide - hide");
                Hide();
            }
        }

        protected virtual void SaveSettingsEvent()
        {
            string settings = UC.GetChecked(AllOrNoneBack);
            SaveSettings?.Invoke(settings, Tag);     // at this level, we return all items.
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            timer.Stop();       // emergency stop, should have stopped by Deactivate..
            base.OnClosing(e);
        }

        private void CheckMouse(object sender, EventArgs e)     // best way of knowing your inside the client..  turned on only if CloseIfCursorOutsideBoundary
        {
            Rectangle client = ClientRectangle;
            client.Inflate(CloseBoundaryRegion);       // overlap area

            if (Control.MouseButtons != MouseButtons.None)      // if we note a buttom down, we may be scrolling, note
            {
                mousebuttonsdown = true;
                //System.Diagnostics.Debug.WriteLine($"Noted mouse button down");
            }
            else
            {
                if (client.Contains(this.PointToClient(MousePosition)))     // if we are inside the box, cancel mouse down and set closedown to 0
                {
                    // System.Diagnostics.Debug.WriteLine($"Inside box, clear mbd");
                    mousebuttonsdown = false;
                    closedowncount = 0;
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine($"outside box, {mousebuttonsdown}");
                    if (!mousebuttonsdown)              // 
                    {
                        if (++closedowncount == 10)     // N*timertick wait
                        {
                            //System.Diagnostics.Debug.WriteLine("Out of bounds");
                            CloseOrHide();
                        }
                    }
                }
            }
        }


        private Timer timer = new Timer();      // timer to monitor for entry into form when transparent.. only sane way in forms
        private bool mousebuttonsdown = false;
        private int closedowncount = 0;

        private Rectangle lastbounds;       // last bounds the position was done on
        private Font lastfont;  // last font
        private bool forceredraw = false;       // force reposition 

    }
}
