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
        public int LargeChange { get { return sb.LargeChange; } set { sb.LargeChange = value; } }

        public FlatStyle FlatStyle { get; set; } = FlatStyle.System;
        public bool CloseOnDeactivate { get; set; } = true;

        public Size ImageSize { get; set; } = new Size(0, 0);                       // if not set, each image sets its size. If no images, then use this to set alternate size 
        public bool IsOpen { get; set; } = false;

        public int ItemCount { get { return controllist.Count; } }

        public Action<CheckedIconListBoxForm, ItemCheckEventArgs> CheckedChanged;       // called after save back to say fully changed.

        private Size defsize = new Size(24, 24);        // default size if no other sizes given by ImageSize/Icons etc

        public void SetItems(IEnumerable<string> tags, IEnumerable<string> text, IEnumerable<Image> image = null)
        {
            if (controllist.Count > 0)
            {
                panelscroll.Controls.Clear();

                foreach (var c in controllist)
                {
                    c.checkbox.Dispose();
                    c.icon.Dispose();
                    c.label.Dispose();
                }

                controllist = new List<ControlSet>();
            }

            AddItems(tags,text,image);
        }

        public void AddItems(IEnumerable<string> tags, IEnumerable<string> text, IEnumerable<Image> image = null)
        {
            string[] tg = tags.ToArray();
            string[] tx = text.ToArray();
            Image[] im = image?.ToArray();

            for( int i = 0; i < tx.Count(); i++ )
            {
                AddItem(tg[i], tx[i], (im != null && i < im.Length) ? im[i] : null);
            }
        }

        public void AddItem(string tag, string text, Image img = null)
        {
            ControlSet c = new ControlSet();

            c.tag = tag;

            ExtCheckBox cb = new ExtCheckBox();
            cb.BackColor = this.BackColor;
            cb.CheckBoxColor = this.CheckBoxColor;
            cb.CheckBoxInnerColor = this.CheckBoxInnerColor;
            cb.CheckColor = this.CheckColor;
            cb.MouseOverColor = this.MouseOverColor;
            cb.FlatStyle = FlatStyle;
            cb.TickBoxReductionSize = TickBoxReductionSize;
            cb.CheckedChanged += CheckedIconListBoxForm_CheckedChanged;
            cb.Tag = controllist.Count;
            c.checkbox = cb;
            panelscroll.Controls.Add(cb);

            Label lb = new Label()
            {
                Text = (string)text,
                Tag = controllist.Count,
                Font = this.Font,
                ForeColor = this.ForeColor,
                TextAlign = ContentAlignment.MiddleLeft,
            };
            lb.MouseDown += Text_MouseDown;

            c.label = lb;
            panelscroll.Controls.Add(lb);
            panelscroll.Controls.Add(lb);

            if (img != null)
            {
                PanelNoTheme ipanel = new PanelNoTheme()
                {
                    BackgroundImage = img,
                    Tag = controllist.Count,
                    BackgroundImageLayout = ImageLayout.Stretch,
                };

                ipanel.MouseDown += Ipanel_MouseDown;
                c.icon = ipanel;
                panelscroll.Controls.Add(ipanel);
            }

            controllist.Add(c);
        }

        class ControlSet
        {
            public ExtCheckBox checkbox;
            public Panel icon;
            public Label label;
            public string tag;  // logical tag for settings
        };

        private List<ControlSet> controllist;

        private Point position;
        private Size size;
        private bool ignorechangeevent = false;
        private ExtPanelScroll panelscroll;
        private ExtScrollBar sb;

        public CheckedIconListBoxForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            AutoSize = false;
            Padding = new Padding(0);
            IsOpen = true;
            controllist = new List<ControlSet>();
            panelscroll = new ExtPanelScroll();
            panelscroll.Dock = DockStyle.Fill;
            Controls.Add(panelscroll);
            sb = new ExtScrollBar();
            panelscroll.Controls.Add(sb);
            panelscroll.ScrollBar.HideScrollBar = true;
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

        public void SetChecked(string tag, bool state = true)        // using ; as the separator
        {
            if ( tag != null )
                SetChecked(tag.SplitNoEmptyStrings(';'),state);
        }

        public void SetChecked(List<string> taglist, bool state = true)   // null allowed
        {
            if (taglist != null)
                SetChecked(taglist.ToArray(), state);
        }

        public void SetChecked(string[] taglist, bool state = true)    // empty array is okay
        {
            if (taglist != null)
                SetChecked(taglist, state ? CheckState.Checked : CheckState.Unchecked);
        }

        public void SetChecked(string[] taglist, CheckState state = CheckState.Checked)    // empty array is okay
        {
            if (taglist != null)
            {
                ignorechangeevent = true;

                for (int i = 0; i < controllist.Count; i++)
                {
                    if (taglist.Contains(controllist[i].tag))
                        controllist[i].checkbox.CheckState = state;
                }

                ignorechangeevent = false;
            }
        }

        public void SetCheckedFromToEnd(int start, bool state = true)
        {
            SetChecked(start, state, -1);
        }

        public void SetChecked(int start, bool state, int end = 0)
        {
            SetChecked(start, state ? CheckState.Checked : CheckState.Unchecked, end);
        }

        public void SetChecked(int start, CheckState state = CheckState.Checked, int end = 0)       // full one allowing intermediates
        {
            ignorechangeevent = true;
            if (end == 0)
                end = start;
            else if (end < 0)
                end = controllist.Count() - 1;

            for (int i = start; i <= end; i++)  // inclusive
            {
                controllist[i].checkbox.CheckState = state;
            }

            ignorechangeevent = false;
        }

        public string GetChecked(int ignore = 0, bool allornone = true)            // semicolon list of options with trailing ;, or All, or None if selected
        {
            string ret = "";

            int total = 0;
            for (int i = ignore; i < controllist.Count; i++)
            {
                if (controllist[i].checkbox.CheckState == CheckState.Checked)
                {
                    ret += controllist[i].tag + ";";
                    total++;
                }
            }

            if (allornone)
            {
                if (total == controllist.Count - ignore)
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

        public int HeightNeeded()
        {
            bool hasimagesize = ImageSize.Width > 0 && ImageSize.Height > 0;        // has fixed size.. if so use it, else base it on first image, or 24x24
            Size imgsize = hasimagesize ? ImageSize : defsize;

            int vpos = VerticalSpacing;
            for (int i = 0; i < controllist.Count; i++)
            {
                if (controllist[i].icon != null)
                    imgsize = hasimagesize ? ImageSize : controllist[i].icon.BackgroundImage.Size;        // set size of item

                vpos += imgsize.Height + VerticalSpacing;
            }

            return vpos;
        }

        #region Implementation

        protected override void OnShown(EventArgs e)        // must be done in OnShown, not on OnLayout.. doing that screws up
        {
            Location = position;        // first place it onto the correct screen.. so that we know what screen to clip to
            Size = new Size(10, 10);        // size it small, so windows itself does not bump the control to another screen.
            this.PositionSizeWithinScreen(size.Width,size.Height, true);    // then keep it on the screen.  always lock to button H
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

//            System.Diagnostics.Debug.WriteLine("Client " + ClientRectangle);

            int lpos = HorizontalSpacing;
            int vpos = VerticalSpacing;

            foreach (var ce in controllist)
            {
                if (ce.icon != null)                  // find first non null and use for defsize
                {
                    defsize = ce.icon.BackgroundImage.Size;
                    break;
                }
            }

            //System.Diagnostics.Debug.WriteLine("Def icon size" + defsize);

            bool hasimagesize = ImageSize.Width > 0 && ImageSize.Height > 0;        // has fixed size.. if so use it, else base it on first image, or 24x24
            Size imgsize = hasimagesize ? ImageSize : defsize;

            Size chkboxsize = (CheckBoxSize.Height < 1 || CheckBoxSize.Width < 1) ? imgsize : CheckBoxSize; // based on imagesize or checkboxsize
            chkboxsize = new Size(Math.Max(4, chkboxsize.Width), Math.Max(4, chkboxsize.Height));

            //System.Diagnostics.Debug.WriteLine("Chk " + chkboxsize + " " + imgsize);

            for (int i = 0; i < controllist.Count; i++)
            {
                if (controllist[i].icon != null)
                    imgsize = hasimagesize ? ImageSize : controllist[i].icon.BackgroundImage.Size;        // set size of item

                //System.Diagnostics.Debug.WriteLine(i + "=" + imgsize);

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

                controllist[i].label.Size = new Size(this.Width - HorizontalSpacing - panelscroll.ScrollBarWidth - tpos, imgsize.Height);
                controllist[i].label.Location = new Point(tpos, vpos);

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
            cb.CheckState = cb.CheckState == CheckState.Unchecked ? CheckState.Checked : CheckState.Unchecked;
        }

        private void Text_MouseDown(object sender, MouseEventArgs e)
        {
            Label p = sender as Label;
            ExtCheckBox cb = controllist[(int)p.Tag].checkbox;
            cb.CheckState = cb.CheckState == CheckState.Unchecked ? CheckState.Checked : CheckState.Unchecked;
        }

        // Sent after the change, different from the checkedlistboxform as its sent during the change before the item check changes. beware.
        private void CheckedIconListBoxForm_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckedChanged != null && !ignorechangeevent)
            {
                ExtCheckBox cb = sender as ExtCheckBox;
                int index = (int)cb.Tag;

                var prevstate = cb.Checked ? CheckState.Unchecked : CheckState.Checked;

                ItemCheckEventArgs i = new ItemCheckEventArgs((int)cb.Tag,cb.CheckState, prevstate);
                CheckedChanged(this,i );
            }
        }

        #endregion
    }
}
