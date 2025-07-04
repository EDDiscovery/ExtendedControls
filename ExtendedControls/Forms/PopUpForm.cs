/*
 * Copyright 2016-2019 EDDiscovery development team
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
    public partial class PopUpForm : Form
    {
        public Point SetLocation { get; set; } = new Point(int.MinValue, -1);     // force to this location.
        public void PositionBelow(Control c) { SetLocation = c.PointToScreen(new Point(0, c.Height)); }

        public int CheckInterval { get; set; } = 500;

        public Panel ContentPanel { get; }

        public PopUpForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
        }
        public PopUpForm(Point location, Size clientsize, int checkinterval = 500)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            ClientSize = clientsize;
            SetLocation = location;
            CheckInterval = checkinterval;
            ContentPanel = new Panel();
            ContentPanel.BorderStyle = BorderStyle.FixedSingle;
            ContentPanel.Dock = DockStyle.Fill;
            Controls.Add(ContentPanel);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (SetLocation.X != int.MinValue)
                Location = SetLocation;

            this.PositionSizeWithinScreen(this.Width, this.Height, true, new Size(16,16));    // keep it on the screen. 

            tm.Interval = CheckInterval;
            tm.Tick += Tm_Tick;
            tm.Start();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            tm.Stop();
            base.OnClosing(e);
        }

        private void Tm_Tick(object sender, EventArgs e)
        {
            var point = Cursor.Position;
            if (!this.Bounds.Contains(point))
            {
                //System.Diagnostics.Debug.WriteLine("Close timer due to out of bounds");
                Close();
            }
        }

        protected override void OnDeactivate(EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Form deactivated");
            base.OnDeactivate(e);
            Close();
        }

        private Timer tm = new Timer();
    }
}
