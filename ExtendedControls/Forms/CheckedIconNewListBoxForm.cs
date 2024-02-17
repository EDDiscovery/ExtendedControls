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
        public Point SetLocation { get; set; } = new Point(int.MinValue, -1);     // force to this location (FitToScreen may slide this left/up)
        public Size CloseBoundaryRegion { get; set; } = new Size(0, 0);     // set size >0 to enable boundary close

        public bool AllOrNoneBack { get; set; } = true;            // use to control if ALL or None is reported, else its all entries or empty list
        public Action<string, Object> SaveSettings;                // Action on close or hide

        public CheckedIconGroupUserControl UC { get; private set; }       // use this to add to

        public CheckedIconNewListBoxForm()
        {
            InitializeComponent();
            UC = new CheckedIconGroupUserControl();
            UC.Dock = DockStyle.Fill;
            Controls.Add(UC);
            timer.Interval = 100;
            timer.Tick += CheckMouse;
        }
        
        public void Show(string settings, Control positionunder, IWin32Window parent)
        {
            UC.Set(settings);
            PositionBelow(positionunder);
            Show(parent);
        }

        public new void Show(IWin32Window parent)
        {
            // intercept this while hidden, and check if we need a relayout
            // if we were shown, and we moved, or we were cleared
            if ((!lastbounds.IsEmpty && SetLocation != this.Location) || forcereposition)
            {
                //System.Diagnostics.Debug.WriteLine($"{BaseUtils.AppTicks.TickCount} Previously shown but now moved or cleared");
                Position();
            }

            if ( ApplyTheme)
            {
                Theme.Current?.ApplyStd(this);
                FormBorderStyle = FormBorderStyle.None;
            }

            base.Show(parent);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Position();
        }

        private void Position()
        { 
            var location = SetLocation.X != int.MinValue ? SetLocation : Location;
            var rect = UC.Render(location);              // we give max size, it gives client size wanted..
            Location = rect.Location;
            ClientSize = rect.Size;
            lastbounds = rect;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            LastDeactivateTime.Stop();          // indicate deact - this is really just to be nice with the .Running flag of the class

            //System.Diagnostics.Debug.WriteLine($"{BaseUtils.AppTicks.TickCountLap("P1")} Activated");

            if (!CloseBoundaryRegion.IsEmpty)
                timer.Start();
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
            string settings = UC.GetCheckedGroup(AllOrNoneBack);
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
        private Rectangle lastbounds;       // last bounds the position was done on
        private bool forcereposition = false;       // force reposition 
        private bool mousebuttonsdown = false;
        private int closedowncount = 0;
    }
}
