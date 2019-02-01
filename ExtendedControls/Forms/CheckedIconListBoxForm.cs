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
 * EDDiscovery is not affiliated with Frontier Developments plc.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    // Fiollows the CheckedListBoxForm functionality, but with Icons added.

    public class CheckedIconListBoxForm : Form
    {
        // Colours for check box
        public Color CheckBoxColor { get; set; } = Color.Gray;     
        public Color CheckBoxInnerColor { get; set; } = Color.White;
        public Color CheckColor { get; set; } = Color.DarkBlue;
        public Color MouseOverColor { get; set; } = Color.CornflowerBlue;
        public Size CheckBoxSize { get; set; } = new Size(0, 0);                   // if not set, ImageSize sets the size, or first image, else 24/24
        public int TickBoxReductionSize { get; set; } = 10;                        // After working out size, reduce by this amount
        public int VerticalSpacing { get; set; } = 4;
        public int HorizontalSpacing { get; set; } = 4;

        // Colours for Scroll bar
        public Color BorderColor { get; set; } = Color.White;
        public Color SliderColor { get; set; } = Color.DarkGray;
        public Color ArrowButtonColor { get; set; } = Color.LightGray;
        public Color ArrowBorderColor { get; set; } = Color.LightBlue;
        public float ArrowUpDrawAngle { get; set; } = 90F;                  
        public float ArrowDownDrawAngle { get; set; } = 270F;               
        public float ArrowColorScaling { get; set; } = 0.5F;                
        public Color ThumbButtonColor { get; set; } = Color.DarkBlue;
        public Color ThumbBorderColor { get; set; } = Color.Yellow;
        public float ThumbDrawAngle { get; set; } = 0F;                     
        public float ThumbColorScaling { get; set; } = 0.5F;                
        public Color MouseOverButtonColor { get; set; } = Color.Green;
        public Color MousePressedButtonColor { get; set; } = Color.Red;

        public FlatStyle FlatStyle { get; set; } = FlatStyle.System;
        public bool CloseOnDeactivate { get; set; } = true;
        public List<string> Items { get; set; }
        public List<Image> ImageItems { get; set; }                                 // can be shorter then Items.
        public Size ImageSize { get; set; } = new Size(0, 0);                       // if not set, each image sets its size. If no images, then use this to set alternate size 
        public bool IsOpen { get; set; } = false;

        public event ItemCheckEventHandler CheckedChanged;

        private Point position;
        private Size size;
        private ExtCheckBox[] checkboxes;
        private bool ignorechangeevent = false;

        public CheckedIconListBoxForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            AutoSize = false;
            Padding = new Padding(0);
            IsOpen = true;
            Items = new List<string>();
            ImageItems = new List<Image>();
        }

        public void PositionSize(Point p, Size s)
        {
            position = p;
            size = s;
        }

        public void PositionBelow(Control b, Size s)
        {
            Point p = b.PointToScreen(new Point(0, b.Size.Height));
            position = p;
            size = s;
        }

        public void SetColour(Color backcolour, Color textc)        // Forecolour used for text
        {
            BackColor = backcolour;
            ForeColor = textc;
        }

        public void SetFont(Font fnt)
        {
            Font = fnt;
        }

        public void SetChecked(string value, int ignore = 0)        // using ; as the separator
        {
            SetChecked(value.SplitNoEmptyStrings(';'));
        }

        public void SetChecked(List<string> list, int ignore = 0)   // null allowed
        {
            if ( list != null )
                SetChecked(list.ToArray(), ignore);
        }

        public void SetChecked(string[] list, int ignore = 0)       // empty array allowed
        {
            ignorechangeevent = true;

            CreateCB();

            bool all = list.Length == 1 && list[0].Equals("All");

            for (int i = ignore; i < Items.Count; i++)
                checkboxes[i].Checked = list.Contains(Items[i]) || all;

            ignorechangeevent = false;
        }

        public void SetChecked(bool c, int ignore = 0, int count = 0)
        {
            ignorechangeevent = true;
            if (count == 0)
                count = Items.Count - ignore;

            for (int i = ignore; count-- > 0; i++)
                checkboxes[i].Checked = c;
            ignorechangeevent = false;
        }

        public string GetChecked(int ignore = 0, bool allornone = true)            // semicolon list of options with trailing ;, or All, or None if selected
        {
            string ret = "";

            int total = 0;
            for (int i = ignore; i < Items.Count; i++)
            {
                if (checkboxes[i].Checked)
                {
                    ret += Items[i] + ";";
                    total++;
                }
            }

            if (allornone)
            {
                if (total == Items.Count - ignore)
                    ret = "All";
                if (ret.Length == 0)
                    ret = "None";
            }

            return ret;
        }

        public List<string> GetCheckedList(int ignore = 0, bool allornone = true)
        {
            return GetChecked(ignore,allornone).SplitNoEmptyStartFinish(';').ToList();
        }

        public string[] GetCheckedArray(int ignore = 0, bool allornone = true)
        {
            return GetChecked(ignore,allornone).SplitNoEmptyStartFinish(';');
        }

        #region Implementation

        private void CreateCB()
        {
            if (checkboxes == null)
            {
                checkboxes = new ExtCheckBox[Items.Count];

                for (int i = 0; i < Items.Count; i++)
                {
                    checkboxes[i] = new ExtCheckBox()
                    {
                        Text = "",
                        Checked = false,
                        Tag = i
                    };

                    checkboxes[i].CheckedChanged += CheckedIconListBoxForm_CheckedChanged;
                }
            }
        }

        protected override void OnShown(EventArgs e)
        {
            Location = position;
            Size = size;

            int lpos = HorizontalSpacing;
            int vpos = VerticalSpacing;

            CreateCB();

            bool hasimagesize = ImageSize.Width > 0 && ImageSize.Height > 0;        // has fixed size.. if so use it, else base it on first image, or 24x24
            Size imgsize = hasimagesize ? ImageSize : (ImageItems.Count > 0 ? ImageItems[0].Size : new Size(24, 24));

            Size chkboxsize = (CheckBoxSize.Height < 1 || CheckBoxSize.Width < 1) ? imgsize : CheckBoxSize; // based on imagesize or checkboxsize
            chkboxsize = new Size(Math.Max(4, chkboxsize.Width), Math.Max(4, chkboxsize.Height));

            System.Diagnostics.Debug.WriteLine("Check box size " + chkboxsize + " img " + imgsize);

            ExtPanelScroll ps = new ExtPanelScroll();
            ps.Dock = DockStyle.Fill;
            Controls.Add(ps);

            ExtScrollBar sb = new ExtScrollBar();
            ps.Controls.Add(sb);

            sb.BorderColor = BorderColor;
            sb.SliderColor = SliderColor;

            sb.ArrowButtonColor = ArrowButtonColor;
            sb.ArrowBorderColor = ArrowBorderColor;
            sb.ArrowUpDrawAngle = ArrowUpDrawAngle;
            sb.ArrowDownDrawAngle = ArrowDownDrawAngle;
            sb.ArrowColorScaling = ArrowColorScaling;
            sb.ThumbButtonColor = ThumbButtonColor;
            sb.ThumbBorderColor = ThumbBorderColor;
            sb.ThumbDrawAngle = ThumbDrawAngle;
            sb.ThumbColorScaling = ThumbColorScaling;
            sb.MouseOverButtonColor = MouseOverButtonColor;
            sb.MousePressedButtonColor = MousePressedButtonColor;
            sb.FlatStyle = FlatStyle;

            for (int i = 0; i < Items.Count; i++)
            {
                if (i < ImageItems.Count)
                    imgsize = hasimagesize ? ImageSize : ImageItems[i].Size;        // set size of item

                int vcentre = vpos + imgsize.Height / 2;

                checkboxes[i].Location = new Point(lpos, vcentre - chkboxsize.Height / 2);
                checkboxes[i].Size = chkboxsize;
                checkboxes[i].BackColor = this.BackColor;
                checkboxes[i].CheckBoxColor = this.CheckBoxColor;
                checkboxes[i].CheckBoxInnerColor = this.CheckBoxInnerColor;
                checkboxes[i].CheckColor = this.CheckColor;
                checkboxes[i].MouseOverColor = this.MouseOverColor;
                checkboxes[i].FlatStyle = FlatStyle;
                checkboxes[i].TickBoxReductionSize = TickBoxReductionSize;
                ps.Controls.Add(checkboxes[i]);

                int tpos = checkboxes[i].Right + HorizontalSpacing;

                if (i < ImageItems.Count)
                {
                    var ipanel = new PanelNoTheme()
                    {
                        BackgroundImage = ImageItems[i],
                        Tag = i,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Size = imgsize,
                        Location = new Point(tpos, vpos),
                    };

                    tpos += imgsize.Width + HorizontalSpacing;

                    ipanel.MouseDown += Ipanel_MouseDown;
                    ps.Controls.Add(ipanel);
                }

                var text = new Label()
                {
                    Text = (string)Items[i],
                    Tag = i,
                    Font = this.Font,
                    ForeColor = this.ForeColor,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Size = new Size(this.Width - HorizontalSpacing - ps.ScrollBarWidth - tpos, imgsize.Height),
                    Location = new Point(tpos, vpos),
                };

                text.MouseDown += Text_MouseDown;
                ps.Controls.Add(text);

                vpos += imgsize.Height + VerticalSpacing;
            }

        }

        protected override void OnDeactivate(EventArgs e)
        {
            if (CloseOnDeactivate)
            {
                base.OnDeactivate(e);
                this.Close();
                IsOpen = false;
            }
        }


        private void Ipanel_MouseDown(object sender, MouseEventArgs e)
        {
            PanelNoTheme p = sender as PanelNoTheme;
            checkboxes[(int)p.Tag].Checked = !checkboxes[(int)p.Tag].Checked;       // fires off a check changed
        }

        private void Text_MouseDown(object sender, MouseEventArgs e)
        {
            Label p = sender as Label;
            checkboxes[(int)p.Tag].Checked = !checkboxes[(int)p.Tag].Checked;       // fires off a check changed
        }

        // Sent after the change, different from the checkedlistboxform as its sent during the change before the item check changes. beware.
        private void CheckedIconListBoxForm_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckedChanged != null && !ignorechangeevent)
            {
                ExtCheckBox cb = sender as ExtCheckBox;
                int index = (int)cb.Tag;
                var state = checkboxes[index].Checked ? CheckState.Checked : CheckState.Unchecked;
                var prevstate = checkboxes[index].Checked ? CheckState.Unchecked : CheckState.Checked;

                ItemCheckEventArgs i = new ItemCheckEventArgs((int)cb.Tag,state,prevstate);
                CheckedChanged(this,i );
            }
        }

        #endregion
    }
}
