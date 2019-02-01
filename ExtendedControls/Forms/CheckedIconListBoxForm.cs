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
    // Follows the CheckedListBoxForm functionality, but with Icons added.

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
        public Color BorderColor { get { return sb.BorderColor; } set { sb.BorderColor = value; } }
        public Color SliderColor { get { return sb.SliderColor; } set { sb.SliderColor = value; } }
        public Color ArrowButtonColor { get { return sb.ArrowButtonColor; } set { sb.ArrowButtonColor = value; } }
        public Color ArrowBorderColor { get { return sb.ArrowBorderColor; } set { sb.ArrowBorderColor = value; } }
        public float ArrowUpDrawAngle { get { return sb.ArrowUpDrawAngle; } set { sb.ArrowUpDrawAngle = value; } }
        public float ArrowDownDrawAngle { get { return sb.ArrowDownDrawAngle; } set { sb.ArrowDownDrawAngle = value; } }
        public float ArrowColorScaling { get { return sb.ArrowColorScaling; } set { sb.ArrowColorScaling = value; } }
        public Color ThumbButtonColor { get { return sb.ThumbButtonColor; } set { sb.ThumbButtonColor = value; } }
        public Color ThumbBorderColor { get { return sb.ThumbBorderColor; } set { sb.ThumbBorderColor = value; } }
        public float ThumbDrawAngle { get { return sb.ThumbDrawAngle; } set { sb.ThumbDrawAngle = value; } }
        public float ThumbColorScaling { get { return sb.ThumbColorScaling; } set { sb.ThumbColorScaling = value; } }
        public Color MouseOverButtonColor { get { return sb.MouseOverButtonColor; } set { sb.MouseOverButtonColor = value; } }
        public Color MousePressedButtonColor { get { return sb.MousePressedButtonColor; } set { sb.MousePressedButtonColor = value; } }

        public FlatStyle FlatStyle { get; set; } = FlatStyle.System;
        public bool CloseOnDeactivate { get; set; } = true;

        public void SetItems(string[] i) { items = i.ToList(); PrepareText(); }
        public void SetItems(List<string> i) { items = i; PrepareText(); }
        public void AddItem(string i) { int c = items.Count; items.Add(i); PrepareText(c); }
        public void AddItems(string[] i) { int c = items.Count; items.AddRange(i); PrepareText(c); }
        public void AddItems(List<string> i) { int c = items.Count; items.AddRange(i); PrepareText(c); }

        public void SetImageItems(Image[] i) { imageItems = i.ToList(); PrepareImages(); }
        public void SetImageItems(List<Image> i) { imageItems = i; PrepareImages(); }
        public void AddImageItem(Image i) { int c = imageItems.Count; imageItems.Add(i); PrepareImages(c); }
        public void AddImageItems(Image[] i) { int c = imageItems.Count; imageItems.AddRange(i); PrepareImages(c); }
        public void AddImageItems(List<Image> i) { int c = imageItems.Count; imageItems.AddRange(i); PrepareImages(c); }

        public Size ImageSize { get; set; } = new Size(0, 0);                       // if not set, each image sets its size. If no images, then use this to set alternate size 
        public bool IsOpen { get; set; } = false;

        public event ItemCheckEventHandler CheckedChanged;

        private Point position;
        private Size size;
        private bool ignorechangeevent = false;
        private List<string> items;
        private List<Image> imageItems;
        class ControlSet
        {
            public ExtCheckBox checkbox;
            public Panel icon;
            public Label text;
        };

        private List<ControlSet> controllist;
        private ExtPanelScroll ps;
        private ExtScrollBar sb;

        public CheckedIconListBoxForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            AutoSize = false;
            Padding = new Padding(0);
            IsOpen = true;
            items = new List<string>();
            imageItems = new List<Image>();
            ps = new ExtPanelScroll();
            ps.Dock = DockStyle.Fill;
            Controls.Add(ps);
            sb = new ExtScrollBar();
            ps.Controls.Add(sb);
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
            if (list != null)
                SetChecked(list.ToArray(), ignore);
        }

        public void SetChecked(string[] list, int ignore = 0)       // empty array allowed
        {
            ignorechangeevent = true;

            bool all = list.Length == 1 && list[0].Equals("All");

            for (int i = ignore; i < items.Count; i++)
                controllist[i].checkbox.Checked = list.Contains(items[i]) || all;

            ignorechangeevent = false;
        }

        public void SetChecked(bool c, int ignore = 0, int count = 0)
        {
            ignorechangeevent = true;
            if (count == 0)
                count = items.Count - ignore;

            for (int i = ignore; count-- > 0; i++)
                controllist[i].checkbox.Checked = c;
            ignorechangeevent = false;
        }

        public string GetChecked(int ignore = 0, bool allornone = true)            // semicolon list of options with trailing ;, or All, or None if selected
        {
            string ret = "";

            int total = 0;
            for (int i = ignore; i < items.Count; i++)
            {
                if (controllist[i].checkbox.Checked)
                {
                    ret += items[i] + ";";
                    total++;
                }
            }

            if (allornone)
            {
                if (total == items.Count - ignore)
                    ret = "All";
                if (ret.Length == 0)
                    ret = "None";
            }

            return ret;
        }

        public List<string> GetCheckedList(int ignore = 0, bool allornone = true)
        {
            return GetChecked(ignore, allornone).SplitNoEmptyStartFinish(';').ToList();
        }

        public string[] GetCheckedArray(int ignore = 0, bool allornone = true)
        {
            return GetChecked(ignore, allornone).SplitNoEmptyStartFinish(';');
        }

        #region Implementation

        private void PrepareText(int startfrom = 0)          // text items have changed, so create checkboxes
        {
            if (controllist == null)
                controllist = new List<ControlSet>();

            for (int i = startfrom; i < items.Count; i++)
            {
                if (i >= controllist.Count)
                    controllist.Add(new ControlSet());          // ensure we have a control set

                if (controllist[i].text != null)                // remove old ones - strange if they do this..
                {
                    ps.Controls.Remove(controllist[i].text);
                    controllist[i].text.Dispose();
                }

                if (controllist[i].checkbox != null)            // remove old ones - strange if they do this..
                {
                    ps.Controls.Remove(controllist[i].checkbox);
                    controllist[i].checkbox.Dispose();
                }

                ExtCheckBox cb = new ExtCheckBox();
                cb.BackColor = this.BackColor;
                cb.CheckBoxColor = this.CheckBoxColor;
                cb.CheckBoxInnerColor = this.CheckBoxInnerColor;
                cb.CheckColor = this.CheckColor;
                cb.MouseOverColor = this.MouseOverColor;
                cb.FlatStyle = FlatStyle;
                cb.TickBoxReductionSize = TickBoxReductionSize;
                cb.CheckedChanged += CheckedIconListBoxForm_CheckedChanged;
                cb.Tag = i;

                controllist[i].checkbox = cb;
                ps.Controls.Add(cb);

                Label text = new Label()
                {
                    Text = (string)items[i],
                    Tag = i,
                    Font = this.Font,
                    ForeColor = this.ForeColor,
                    TextAlign = ContentAlignment.MiddleLeft,
                };
                text.MouseDown += Text_MouseDown;

                controllist[i].text = text;
                ps.Controls.Add(text);
            }
        }

        private void PrepareImages(int startfrom = 0)
        {
            if (controllist == null)
                controllist = new List<ControlSet>();

            for (int i = startfrom; i < imageItems.Count; i++)
            {
                if (i >= controllist.Count)
                    controllist.Add(new ControlSet());          // ensure we have a control set

                if (controllist[i].icon != null)                // remove old ones - strange if they do this..
                {
                    ps.Controls.Remove(controllist[i].icon);
                    controllist[i].icon.Dispose();
                }

                PanelNoTheme ipanel = new PanelNoTheme()
                {
                    BackgroundImage = imageItems[i],
                    Tag = i,
                    BackgroundImageLayout = ImageLayout.Stretch,
                };

                ipanel.MouseDown += Ipanel_MouseDown;
                controllist[i].icon = ipanel;
                ps.Controls.Add(ipanel);
            }
        }

        protected override void OnShown(EventArgs e)        // here we position the controls
        {
            Location = position;
            Size = size;

            int lpos = HorizontalSpacing;
            int vpos = VerticalSpacing;

            bool hasimagesize = ImageSize.Width > 0 && ImageSize.Height > 0;        // has fixed size.. if so use it, else base it on first image, or 24x24
            Size imgsize = hasimagesize ? ImageSize : (imageItems.Count > 0 ? imageItems[0].Size : new Size(24, 24));

            Size chkboxsize = (CheckBoxSize.Height < 1 || CheckBoxSize.Width < 1) ? imgsize : CheckBoxSize; // based on imagesize or checkboxsize
            chkboxsize = new Size(Math.Max(4, chkboxsize.Width), Math.Max(4, chkboxsize.Height));

            for (int i = 0; i < items.Count; i++)
            {
                if (i < imageItems.Count)
                    imgsize = hasimagesize ? ImageSize : imageItems[i].Size;        // set size of item

                int vcentre = vpos + imgsize.Height / 2;

                controllist[i].checkbox.Location = new Point(lpos, vcentre - chkboxsize.Height / 2);
                controllist[i].checkbox.Size = chkboxsize;

                int tpos = controllist[i].checkbox.Right + HorizontalSpacing;

                if (controllist[i].icon != null)
                {
                    controllist[i].icon.Size = imgsize;
                    controllist[i].icon.Location = new Point(tpos, vpos);
                    tpos += imgsize.Width + HorizontalSpacing;
                }

                controllist[i].text.Size = new Size(this.Width - HorizontalSpacing - ps.ScrollBarWidth - tpos, imgsize.Height);
                controllist[i].text.Location = new Point(tpos, vpos);

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
            ExtCheckBox cb = controllist[(int)p.Tag].checkbox;
            cb.Checked = !cb.Checked;
        }

        private void Text_MouseDown(object sender, MouseEventArgs e)
        {
            Label p = sender as Label;
            ExtCheckBox cb = controllist[(int)p.Tag].checkbox;
            cb.Checked = !cb.Checked;
        }

        // Sent after the change, different from the checkedlistboxform as its sent during the change before the item check changes. beware.
        private void CheckedIconListBoxForm_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckedChanged != null && !ignorechangeevent)
            {
                ExtCheckBox cb = sender as ExtCheckBox;
                int index = (int)cb.Tag;
                var state = cb.Checked ? CheckState.Checked : CheckState.Unchecked;
                var prevstate = cb.Checked ? CheckState.Unchecked : CheckState.Checked;

                ItemCheckEventArgs i = new ItemCheckEventArgs((int)cb.Tag,state,prevstate);
                CheckedChanged(this,i );
            }
        }

        #endregion
    }
}
