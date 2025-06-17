/*
 * Copyright © 2016-2023 EDDiscovery development team
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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ExtListBoxForm : Form
    {
        public delegate void OnSelectedIndexChanged(object sender, EventArgs e, bool key);
        public event OnSelectedIndexChanged SelectedIndexChanged;
        public event KeyPressEventHandler KeyPressed;
        public event KeyEventHandler OtherKeyPressed;

        public ExtListBox ListBox { get; set; }         // accessible for themeing

        public List<string> Items { get { return ListBox.Items; } set { ListBox.Items = value; } }
        public int[] ItemSeperators { get { return ListBox.ItemSeperators; } set { ListBox.ItemSeperators = value; } }     // set to array giving index of each separator
        public List<Image> ImageItems { get { return ListBox.ImageItems; } set { ListBox.ImageItems = value; } }
        public Color SelectionBackColor { get { return ListBox.SelectionBackColor; } set { ListBox.SelectionBackColor = value; this.BackColor = value; } }
        public Color SelectionBackColor2 { get { return ListBox.SelectionBackColor2; } set { ListBox.SelectionBackColor2 = value; } }
        public Color BorderColor { get { return ListBox.BorderColor; } set { ListBox.BorderColor = value; } }
        public Color MouseOverBackgroundColor { get { return ListBox.SelectionColor; } set { ListBox.SelectionColor = value; } }
        public int SelectedIndex { get { return ListBox.SelectedIndex; } set { ListBox.SelectedIndex = value; } }
        public Color ItemSeperatorColor { get { return ListBox.ItemSeperatorColor; } set { ListBox.ItemSeperatorColor = value; } }
        public FlatStyle FlatStyle { get { return ListBox.FlatStyle; } set { ListBox.FlatStyle = value; } }
        public new Font Font { get { return base.Font; } set { base.Font = value; ListBox.Font = value; } }
        public bool FitToItemsHeight { get { return ListBox.FitToItemsHeight; } set { ListBox.FitToItemsHeight = value; } }
        public bool FitImagesToItemHeight { get { return ListBox.FitImagesToItemHeight; } set { ListBox.FitImagesToItemHeight = value; } }                    // if set images need to fit within item height

        public Point SetLocation { get; set; } = new Point(int.MinValue, -1);     // force to this location.
        public void PositionBelow(Control c) { SetLocation = c.PointToScreen(new Point(0, c.Height)); }
        public void PositionBelow(Control c, int xoff, int yoff = 0) { SetLocation = c.PointToScreen(new Point(xoff, c.Height+yoff)); }
        public bool RightAlignedToLocation { get; set; } = false;
        private bool CloseOnDeactivate { get; set; } = false;

        public ExtListBoxForm(string name = "", bool closeondeact = true)
        {
            CloseOnDeactivate = closeondeact;

            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.ListBox = new ExtListBox();
            this.Name = this.ListBox.Name = name;
            this.ListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.ListBox.Dock = DockStyle.Fill;
            this.ListBox.Visible = true;
            this.ListBox.SelectedIndexChanged += listcontrol_SelectedIndexChanged;
            this.ListBox.KeyPressed += listcontrol_KeyPressed;
            this.ListBox.OtherKeyPressed += listcontrol_OtherKeyPressed;
            this.ListBox.Margin = new Padding(0);
            this.ListBox.FitToItemsHeight = false;
            this.Padding = new Padding(0);
            this.Controls.Add(this.ListBox);
            this.Activated += new System.EventHandler(this.FormActivated);
        }

        private void FormActivated(object sender, EventArgs e)
        {
            if ( SetLocation.X != int.MinValue)
            {
                Location = SetLocation;
            }

            int border = Bounds.Height - ClientRectangle.Height;        // any windows border..

            int ih = (int)Font.GetHeight() + 2;
            int hw = ih * Items.Count + 4 + border;

          //  System.Diagnostics.Debug.WriteLine("Set LBF loc " + Location + " Font " + Font + " ih " + ih + " hw " + hw);

            using (Graphics g = this.CreateGraphics())
            {
                Size max = ListBox.MeasureItems(g);
                this.PositionSizeWithinScreen(max.Width + 4 + ListBox.ScrollBar.Width, hw, true, new Size(64,64));    // keep it on the screen. 
            }

            //            System.Diagnostics.Debug.WriteLine(".. now " + Location + " " + Size + " Items " + Items.Count + " ih "  + ih + " hw" + hw);
        }

        public void KeyDownAction(KeyEventArgs e)
        {
            ListBox.KeyDownAction(e);
        }


        private void listcontrol_SelectedIndexChanged(object sender, EventArgs e, bool key)
        {
            if (CloseOnDeactivate)
                this.Close();

            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, e, key);

        }

        private void listcontrol_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (KeyPressed != null)
                KeyPressed(this, e);
        }

        private void listcontrol_OtherKeyPressed(object sender, KeyEventArgs e)
        {
            if (OtherKeyPressed != null)
                OtherKeyPressed(this, e);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);

            if ( CloseOnDeactivate)
                this.Close();
        }

    }

}
