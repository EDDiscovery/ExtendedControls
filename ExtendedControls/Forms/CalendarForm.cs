/*
 * Copyright © 2016-2019 EDDiscovery development team
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
 *
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class CalendarForm : Form
    {
        public bool CloseOnDeactivate { get; set; } = true;         // close on deactivate
        public bool HideOnDeactivate { get; set; } = false;         // hide instead
        public bool CloseOnSelection { get; set; } = false;            // close when any is changed
        public bool HideOnSelection { get; set; } = false;             // hide when any is changed

        public Point SetLocation { get; set; } = new Point(int.MinValue, -1);     // force to this location.
        public void PositionBelow(Control c) { SetLocation = c.PointToScreen(new Point(0, c.Height)); }

        public MonthCalendar Calendar { get { return monthCalendar; } }     // direct setting of properties

        public DateTime Value { get { return monthCalendar.SelectionStart; } set 
            {
                if (value < monthCalendar.MinDate)
                    value = monthCalendar.MinDate;
                else if (value > monthCalendar.MaxDate)
                    value = monthCalendar.MaxDate;

                monthCalendar.SelectionStart = monthCalendar.SelectionEnd = value; 
            } }

        public Action<CalendarForm, DateTime> Selected;               // Action on selected (mouse selection)

        private bool closing = false;

        public CalendarForm()
        {
            InitializeComponent();
            monthCalendar.MaxSelectionCount = 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.monthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateSelected);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (SetLocation.X != int.MinValue)
                Location = SetLocation;

            this.PositionSizeWithinScreen(this.Width, this.Height, true, new Size(64,64));    // keep it on the screen. 
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            closing = true;
            base.OnClosing(e);

        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);

            if (!closing)       // just in case we get into circles
            {
                if (CloseOnDeactivate)
                {
                    // System.Diagnostics.Debug.WriteLine("Deactivated close");
                    this.Close();
                }
                else if (HideOnDeactivate)
                {
                    HideMe();
                }
            }
        }

        private void HideMe()
        {
            if (Owner != null)
            {
                bool otm = Owner.TopMost;
                Owner.TopMost = true;
                Hide();
                Owner.TopMost = otm;
            }
            else
                Hide();

        }

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            Selected?.Invoke(this, monthCalendar.SelectionStart);
            if (CloseOnSelection)
            {
                //System.Diagnostics.Debug.WriteLine("selected close");
                this.Close();
            }
            else if (HideOnSelection)
            {
                HideMe();
            }
        }
    }
}
