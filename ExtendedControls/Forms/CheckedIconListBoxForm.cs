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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    // Follows the List Box, in a form, with icons functionality

    public class CheckedIconListBoxForm : Form
    {
        // Colours for check box
        public Color CheckBoxColor { get; set; } = Color.Gray;
        public Color CheckBoxInnerColor { get; set; } = Color.White;
        public Color CheckColor { get; set; } = Color.DarkBlue;
        public Color MouseOverColor { get; set; } = Color.CornflowerBlue;
        public Size CheckBoxSize { get; set; } = new Size(0, 0);                   // if not set, ImageSize sets the size, or first image, else 24/24
        public float TickBoxReductionRatio { get; set; } = 0.75f;                        // After working out size, reduce by this amount
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

        public bool CloseOnDeactivate { get; set; } = true;         // close on deactivate
        public bool HideOnDeactivate { get; set; } = false;         // hide instead
        public bool CloseOnChange { get; set; } = false;            // close when any is changed
        public bool HideOnChange { get; set; } = false;             // hide when any is changed

        // if not set, each image sets its size. If no images, then use this to set alternate size 
        // if fonth > imageheight, then images are scaled to font size
        public Size ImageSize { get; set; } = new Size(0, 0);

        public Size ScreenMargin { get; set; } = new Size(64, 64);

        public int ItemCount { get { return controllist.Count; } }

        public Point SetLocation { get; set; } = new Point(int.MinValue, -1);     // force to this location.
        public void PositionBelow(Control c) { SetLocation = c.PointToScreen(new Point(0, c.Height)); }
        public bool RightAlignedToLocation { get; set; } = false;

        public Action<CheckedIconListBoxForm, ItemCheckEventArgs,Object> CheckedChanged;       // called after save back to say fully changed.

        public Action<string, Object> SaveSettings;                // Action on close or hide
        public bool AllOrNoneBack { get; set; } = true;            // use to control if ALL or None is reported, else its all entries or empty list

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

        public void AddItems(IEnumerable<string> tags, IEnumerable<string> text, IEnumerable<Image> image = null, bool attop = false)
        {
            string[] tg = tags.ToArray();
            string[] tx = text.ToArray();
            Image[] im = image?.ToArray();
            ControlSet[] list = new ControlSet[tx.Count()];

            for (int i = 0; i < tx.Count(); i++)
            {
                list[i] = AddItem(tg[i], tx[i], (im != null && i < im.Length) ? im[i] : null, null);
            }

            if (attop)
            {
                for (int i = tx.Count() - 1; i >= 0; i--)
                    controllist.Insert(0, list[i]);
            }
            else
                controllist.AddRange(list);
        }

        public void AddItem(string tag, string text, Image img = null, bool attop = false, string exclusive = null, bool disableuncheck = false)
        {
            ControlSet c = AddItem(tag, text, img, exclusive, disableuncheck);

            if (attop)
                controllist.Insert(0, c);
            else
                controllist.Add(c);
        }

        private ControlSet AddItem(string tag, string text, Image img = null, string exclusive = null, bool disableuncheck = false)
        {
            ControlSet c = new ControlSet();

            panelscroll.SuspendLayout();

            c.tag = tag;
            c.exclusivetags = exclusive;
            c.disableuncheck = disableuncheck;

            ExtCheckBox cb = new ExtCheckBox();
            cb.BackColor = this.BackColor;
            cb.CheckBoxColor = this.CheckBoxColor;
            cb.CheckBoxInnerColor = this.CheckBoxInnerColor;
            cb.CheckColor = this.CheckColor;
            cb.MouseOverColor = this.MouseOverColor;
            cb.FlatStyle = FlatStyle;
            cb.TickBoxReductionRatio = TickBoxReductionRatio;
            cb.CheckedChanged += CheckedIconListBoxForm_CheckedChanged;
            c.checkbox = cb;
            panelscroll.Controls.Add(cb);

            Label lb = new Label()
            {
                AutoSize = false,       // don't autosize here, it seems to take forever.
                Text = (string)text,
                Tag = controllist.Count,
                ForeColor = this.ForeColor,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            lb.MouseDown += Text_MouseDown;

            c.label = lb;
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

            panelscroll.ResumeLayout();

            return c;
        }

        class ControlSet
        {
            public ExtCheckBox checkbox;
            public Panel icon;
            public Label label;
            public string tag;  // logical tag for settings
            public string exclusivetags;        // ones which must be off if this is on
            public bool disableuncheck; // no unchecking
        };

        private List<ControlSet> controllist;
        private bool ignorechangeevent = false;
        private ExtPanelScroll panelscroll;
        private ExtScrollBar sb;

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
                    { 
                        controllist[i].checkbox.CheckState = state;
                    }
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

        public CheckedIconListBoxForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            AutoSize = false;
            Padding = new Padding(0);
            controllist = new List<ControlSet>();
            panelscroll = new ExtPanelScroll();
            panelscroll.Dock = DockStyle.Fill;
            Controls.Add(panelscroll);
            sb = new ExtScrollBar();
            panelscroll.Controls.Add(sb);
            panelscroll.ScrollBar.HideScrollBar = true;
        }

        #region Implementation

        int maxw = 0;
        int maxh = 0;

        protected override void OnLoad(EventArgs e)
        {
            //Stopwatch sw = new Stopwatch(); sw.Start();

            int lpos = HorizontalSpacing;
            maxh = VerticalSpacing;

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

            panelscroll.SuspendLayout();    // double speed doing this

            for (int i = 0; i < controllist.Count; i++)
            {
                if (controllist[i].icon != null)
                    imgsize = hasimagesize ? ImageSize : controllist[i].icon.BackgroundImage.Size;        // set size of item

                //System.Diagnostics.Debug.WriteLine(i + "=" + imgsize);

                int fonth = (int)controllist[i].label.Font.GetHeight() + 2;

                int vspacing = Math.Max(fonth, imgsize.Height);

                int vcentre = maxh + vspacing / 2;

                controllist[i].checkbox.Location = new Point(lpos, vcentre - chkboxsize.Height / 2);
                controllist[i].checkbox.Size = chkboxsize;
                controllist[i].checkbox.Tag = i;        // store index of control when displayed

                int tpos = controllist[i].checkbox.Right + HorizontalSpacing;

                if (controllist[i].icon != null)
                {
                    Size iconsize = (fonth > imgsize.Height) ? new Size((int)(imgsize.Width * (float)fonth / imgsize.Height), fonth) : imgsize;
                    controllist[i].icon.Size = iconsize;
                    controllist[i].icon.Location = new Point(tpos, vcentre - iconsize.Height / 2);
                    tpos += iconsize.Width + HorizontalSpacing;
                }

                controllist[i].label.AutoSize = true;       // now autosize
                controllist[i].label.Location = new Point(tpos, vcentre-controllist[i].label.Height/2);     // and position
                    
                maxw = Math.Max(maxw, controllist[i].label.Right + 1);

                maxh += vspacing + VerticalSpacing;
            }

            panelscroll.ResumeLayout();

            //System.Diagnostics.Debug.WriteLine("OnLoad in " + sw.ElapsedMilliseconds);
            base.OnLoad(e);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (SetLocation.X != int.MinValue)
                Location = SetLocation;

            this.PositionSizeWithinScreen(maxw + 16 + panelscroll.ScrollBarWidth, maxh, true, ScreenMargin);    // keep it on the screen. 
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

        // Sent after the change, so cb.CheckState is the new state
        private void CheckedIconListBoxForm_CheckedChanged(object sender, EventArgs e)
        {
            if (!ignorechangeevent)
            {
                ExtCheckBox cb = sender as ExtCheckBox;
                int index = (int)cb.Tag;

                if (controllist[index].disableuncheck && cb.Checked == false) // if uncheckable, then set it back. Used in radio button situations
                {
                    ignorechangeevent = true;
                    cb.Checked = true;
                    ignorechangeevent = false;
                }

                if (cb.Checked && controllist[index].exclusivetags != null)      // if checking, and we have tags which must be turned off
                {
                    ignorechangeevent = true;
                    if (controllist[index].exclusivetags.Equals("All"))     // turn off all but us
                    {
                        for(int ix = 0; ix < controllist.Count; ix++)
                        {
                            if ( ix != index && controllist[ix].tag != "All" && controllist[ix].tag != "None")
                                controllist[ix].checkbox.Checked = false;
                        }
                    }
                    else
                    {
                        string[] tags = controllist[index].exclusivetags.Split(";");
                        foreach (string s in tags)
                        {
                            System.Diagnostics.Debug.WriteLine($"Tag {controllist[index].tag} turn off {s}");
                            int offindex = controllist.FindIndex(x => x.tag == s);
                            if (offindex >= 0)
                                controllist[offindex].checkbox.Checked = false;
                        }
                    }
                    ignorechangeevent = false;
                }

                var prevstate = cb.Checked ? CheckState.Unchecked : CheckState.Checked;

                ItemCheckEventArgs i = new ItemCheckEventArgs(index,cb.CheckState, prevstate);
                CheckChangedEvent(cb,i);       // derived classes first.
                CheckedChanged?.Invoke(this,i,Tag);

                if (CloseOnChange)
                {
                    Close();
                }
                else if (HideOnChange)
                {
                    Hide();
                }
            }
        }

        protected virtual void CheckChangedEvent(ExtCheckBox cb, ItemCheckEventArgs e) { }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);

            if (CloseOnDeactivate)
            {
                SaveSettingsEvent();
                this.Close();
            }
            else if (HideOnDeactivate)
            {
                SaveSettingsEvent();

                if ( Owner != null )
                {
                    var o = Owner;          // calling Hide() when the owner is not ready to receive the focus causes windows to go and get another window to place
                    Owner = null;           // disassociating it temp from its owner seems to solve this. Probably because it can pick that window now.
                    Hide();
                    Owner = o;
                }
                else
                {
                    Hide();
                }
            }
        }

        protected virtual void SaveSettingsEvent()
        {
            SaveSettings?.Invoke(GetChecked(0, AllOrNoneBack), Tag);     // at this level, we return all items.
        }

        Timer tmr = new Timer();

        #endregion
    }
}
