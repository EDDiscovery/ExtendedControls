/*
 * Copyright © 2016-2022 EDDiscovery development team
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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
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

        public Size CloseBoundaryRegion { get; set; } = new Size(0, 0);     // set size >0 to enable boundary close

        public FlatStyle FlatStyle { get; set; } = FlatStyle.System;

        public bool CloseOnDeactivate { get; set; } = true;         // close on deactivate
        public bool HideOnDeactivate { get; set; } = false;         // hide instead
        public bool CloseOnChange { get; set; } = false;            // close when any is changed
        public bool HideOnChange { get; set; } = false;             // hide when any is changed

        public long LastDeactivateTime { get; set; } = -Environment.TickCount;      // when we deactivate, we record last time
        public bool DeactivatedWithin(long ms)                      // have we deactivated in this timeperiod. use for debouncing buttons if hiding
        {
            long timesince = Environment.TickCount - LastDeactivateTime;
            //System.Diagnostics.Debug.WriteLine($"Deactivate time {timesince}");
            return timesince <= ms;
        }

        // if not set, each image sets its size. If no images, then use this to set alternate size 
        // if fonth > imageheight, then images are scaled to font size
        public Size ImageSize { get; set; } = new Size(0, 0);

        public Size ScreenMargin { get; set; } = new Size(64, 64);

        public int ItemCount { get { return controllist.Count; } }

        public Point SetLocation { get; set; } = new Point(int.MinValue, -1);     // force to this location.
        public void PositionBelow(Control c) { SetLocation = c.PointToScreen(new Point(0, c.Height)); }
        public bool RightAlignedToLocation { get; set; } = false;

        public Action<CheckedIconListBoxForm, ItemCheckEventArgs, Object> CheckedChanged;       // called after save back to say fully changed.
        public Action<int,string,string,object,MouseEventArgs> ButtonPressed;        // called if right click on panel/icon, or if a button has been defined

        public Action<string, Object> SaveSettings;                // Action on close or hide
        public bool AllOrNoneBack { get; set; } = true;            // use to control if ALL or None is reported, else its all entries or empty list

        public char SettingsSplittingChar { get; set; } = ';';      // what char is used for split settings

        // this will speed up multiple adds a touch
        public void StartAdd()
        {
           // System.Diagnostics.Debug.WriteLine($"{BaseUtils.AppTicks.TickCountLap("SA",true)} Start add {panelscroll.Controls.Count}");
            panelscroll.SuspendLayout();
        }
        public void EndAdd()
        {
            //panelscroll.ResumeLayout();
            System.Diagnostics.Debug.WriteLine($"{BaseUtils.AppTicks.TickCountLap("SA")} End add");
        }

        public void AddItem(string tag, string text, Image img = null, bool attop = false, string exclusive = null, bool disableuncheck = false, bool button = false, object usertag = null)
        {
            ControlSet c = AddItem(tag, text, img, exclusive, disableuncheck, button, usertag);

            if (attop)
                controllist.Insert(0, c);
            else
                controllist.Add(c);
        }

        public void SetChecked(string tag, bool state = true)        // using ; as the separator
        {
            if (tag != null)
                SetChecked(tag.SplitNoEmptyStrings(SettingsSplittingChar), state);
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
                        controllist[i].extcheckbox.CheckState = state;
                    }
                }

                ignorechangeevent = false;
            }
        }

        public void SetCheckedFromToEnd(int startat, bool state = true)
        {
            SetChecked(startat, state ? CheckState.Checked : CheckState.Unchecked, -1);
        }

        public void SetChecked(int pos, bool state)
        {
            SetChecked(pos, state ? CheckState.Checked : CheckState.Unchecked);
        }

        // full one allowing intermediates.
        // end = 0 means just check start. -1 means check all
        public void SetChecked(int start, CheckState state, int end = 0)       
        {
            ignorechangeevent = true;
            if (end == 0)
                end = start;
            else if (end < 0)
                end = controllist.Count() - 1;

            for (int i = start; i <= end; i++)  // inclusive
            {
                if ( controllist[i].extcheckbox != null)
                    controllist[i].extcheckbox.CheckState = state;
            }

            ignorechangeevent = false;
        }

        public string GetChecked(int ignore = 0, bool allornone = true)            // semicolon list of options with trailing ;, or All, or None if selected
        {
            string ret = "";

            int total = 0;
            int totalchecks = 0;
            for (int i = ignore; i < controllist.Count; i++)
            {
                if (controllist[i].extcheckbox != null)     // if its a check box..
                {
                    //System.Diagnostics.Debug.WriteLine($"Tick on {controllist[i].label.Text}");
                    totalchecks++;

                    if (controllist[i].extcheckbox.CheckState == CheckState.Checked)    // if its checked
                    {
                        ret += controllist[i].tag + SettingsSplittingChar;
                        total++;
                    }
                }
            }

            if (allornone)
            {
                if (total == totalchecks)
                    ret = "All";
                if (ret.Length == 0)
                    ret = "None";
            }

            return ret;
        }

        public List<string> GetCheckedList(int ignore = 0, bool allornone = true)
        {
            return GetChecked(ignore, allornone).SplitNoEmptyStartFinish(SettingsSplittingChar).ToList();
        }

        public string[] GetCheckedArray(int ignore = 0, bool allornone = true)
        {
            return GetChecked(ignore, allornone).SplitNoEmptyStartFinish(SettingsSplittingChar);
        }

        // call to clear the items - you can add new ones after
        public void Clear()
        {
            controllist.Clear();
            panelscroll.ClearControls();
            positiondone = false;
            Refresh();
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
            timer.Interval = 100;
            timer.Tick += CheckMouse;
        }

        #region Implementation

        private ControlSet AddItem(string tag, string text, Image img = null, string exclusive = null, bool disableuncheck = false, bool button = false, object usertag = null)
        {
            ControlSet c = new ControlSet();

            panelscroll.SuspendLayout();

            c.tag = tag;
            c.usertag = usertag;
            c.exclusivetags = exclusive;
            c.disableuncheck = disableuncheck;

            if (button == false)
            {
                ExtCheckBox cb = new ExtCheckBox();
                cb.SuspendLayout();
                cb.BackColor = this.BackColor;
                cb.CheckBoxColor = this.CheckBoxColor;
                cb.CheckBoxInnerColor = this.CheckBoxInnerColor;
                cb.CheckColor = this.CheckColor;
                cb.MouseOverColor = this.MouseOverColor;
                cb.FlatStyle = FlatStyle;
                cb.TickBoxReductionRatio = TickBoxReductionRatio;
                cb.CheckedChanged += CheckedIconListBoxForm_CheckedChanged;
                cb.MouseDown += (s, e) => { if (e.Button == MouseButtons.Right) ButtonPressed.Invoke(controllist.IndexOf(c),c.tag, text, c.usertag, e); };
                cb.Visible = false;
                c.extcheckbox = cb;
                panelscroll.Controls.Add(cb);
            }

            Label lb = new Label()
            {
                AutoSize = false,       // don't autosize here, it seems to take forever.
                Text = (string)text,
                Tag = controllist.Count,
                ForeColor = this.ForeColor,
                TextAlign = ContentAlignment.MiddleLeft,
                Visible = false,
            };
            lb.SuspendLayout();

            lb.MouseDown += (sender, e) =>
            {
                if (c.extcheckbox != null && e.Button == MouseButtons.Left)
                    c.extcheckbox.CheckState = c.extcheckbox.CheckState == CheckState.Unchecked ? CheckState.Checked : CheckState.Unchecked;
                else
                    ButtonPressed?.Invoke(controllist.IndexOf(c), c.tag, text, c.usertag, e);
            };

            c.label = lb;
            panelscroll.Controls.Add(lb);

            if (img != null)
            {
                PictureBox ipanel = new PictureBox()
                {
                    Image = img,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Tag = controllist.Count,
                    Visible = false,
                };

                ipanel.SuspendLayout();

                ipanel.MouseDown += (s, e) =>
                {
                    if (c.extcheckbox != null && e.Button == MouseButtons.Left)
                        c.extcheckbox.CheckState = c.extcheckbox.CheckState == CheckState.Unchecked ? CheckState.Checked : CheckState.Unchecked;
                    else
                        ButtonPressed?.Invoke(controllist.IndexOf(c),c.tag, text, c.usertag, e);
                };

                c.icon = ipanel;
                panelscroll.Controls.Add(ipanel);
            }

            panelscroll.ResumeLayout();

            return c;
        }

        // call to position controls, will do nothing if already did this
        private void PositionControls()
        {
            if (positiondone)
                return;

            Refresh();

            int lpos = HorizontalSpacing;
            maxheight = VerticalSpacing;

            foreach (var ce in controllist)
            {
                if (ce.icon != null)                  // find first non null and use for defsize
                {
                    defsize = ce.icon.Image.Size;
                    break;
                }
            }

           // System.Diagnostics.Debug.WriteLine($"{BaseUtils.AppTicks.TickCountLap("L1", true)} Controls list {controllist.Count} {panelscroll.Controls.Count}");

            bool hasimagesize = ImageSize.Width > 0 && ImageSize.Height > 0;        // has fixed size.. if so use it, else base it on first image, or 24x24
            Size imgsize = hasimagesize ? ImageSize : defsize;

            Size chkboxsize = (CheckBoxSize.Height < 1 || CheckBoxSize.Width < 1) ? imgsize : CheckBoxSize; // based on imagesize or checkboxsize
            chkboxsize = new Size(Math.Max(4, chkboxsize.Width), Math.Max(4, chkboxsize.Height));

            panelscroll.SuspendLayout();    // double speed doing this

            int col1 = lpos;                // records where the col1 (icon then text) pos is, in case the check box is not present on a particular row

            for (int i = 0; i < controllist.Count; i++)
            {
                var cl = controllist[i];

                if (cl.icon != null)
                {
                    imgsize = hasimagesize ? ImageSize : cl.icon.Image.Size;        // set size of item
                }

                //System.Diagnostics.Debug.WriteLine(i + "=" + imgsize);

                int fonth = (int)cl.label.Font.GetHeight() + 2;

                int vspacing = Math.Max(fonth, imgsize.Height);

                int vcentre = maxheight + vspacing / 2;

                if (cl.extcheckbox != null)
                {
                    cl.extcheckbox.Location = new Point(lpos, vcentre - chkboxsize.Height / 2);
                    cl.extcheckbox.Size = chkboxsize;
                    cl.extcheckbox.Tag = i;        // store index of control when displayed
                    col1 = cl.extcheckbox.Right + HorizontalSpacing;        // set col1 position
                }

                int textpos = col1;

                if (cl.icon != null)
                {
                    Size iconsize = (fonth > imgsize.Height) ? new Size((int)(imgsize.Width * (float)fonth / imgsize.Height), fonth) : imgsize;
                    cl.icon.Size = iconsize;
                    cl.icon.Location = new Point(col1, vcentre - iconsize.Height / 2);
                    textpos += iconsize.Width + HorizontalSpacing;
                }

                cl.label.AutoSize = true;       // now autosize
                cl.label.Location = new Point(textpos, vcentre - cl.label.Height / 2);     // and position

                maxwidth = Math.Max(maxwidth, cl.label.Right + 1);
                maxheight += vspacing + VerticalSpacing;
            }

            //System.Diagnostics.Debug.WriteLine($"{BaseUtils.AppTicks.TickCountLap("L1")} Controls positioned");

            for (int i = 0; i < controllist.Count; i++)     // now turn them on, once all are in position
            {
                var cl = controllist[i];
                if (cl.extcheckbox != null)
                {
                    cl.extcheckbox.Visible = true;
                    cl.extcheckbox.ResumeLayout();
                }
                cl.label.Visible = true;
                cl.label.ResumeLayout();
                if (cl.icon != null)
                {
                    cl.icon.Visible = true;
                    cl.icon.ResumeLayout();
                }
            }

            panelscroll.ResumeLayout();

            positiondone = true;

            //System.Diagnostics.Debug.WriteLine($"{BaseUtils.AppTicks.TickCountLap("L1")} Controls Visible resumed");

        }


        protected override void OnLoad(EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine($"{BaseUtils.AppTicks.TickCount} On Load");
            base.OnLoad(e);
            PositionControls();
        }

        protected override void OnActivated(EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine($"{BaseUtils.AppTicks.TickCount} On Activated");
            base.OnActivated(e);

            PositionControls();     // if we hid, we may need to redo this if we have updated the list

            if (SetLocation.X != int.MinValue)
                Location = SetLocation;

            this.PositionSizeWithinScreen(maxwidth + 16 + panelscroll.ScrollBarWidth, maxheight, true, ScreenMargin);    // keep it on the screen. 

            if (!CloseBoundaryRegion.IsEmpty)
                timer.Start();
            else
            {
                //System.Diagnostics.Debug.WriteLine($"Warning a CheckedIconListBoxForm is not using CloseBoundary ${Environment.StackTrace}");
            }
        }

        protected override void OnShown(EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine($"{BaseUtils.AppTicks.TickCount} On shown");
            base.OnShown(e);
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
                                controllist[ix].extcheckbox.Checked = false;
                        }
                    }
                    else
                    {
                        string[] tags = controllist[index].exclusivetags.Split(SettingsSplittingChar);
                        foreach (string s in tags)
                        {
                            //System.Diagnostics.Debug.WriteLine($"Tag {controllist[index].tag} turn off {s}");
                            int offindex = controllist.FindIndex(x => x.tag == s);
                            if (offindex >= 0)
                                controllist[offindex].extcheckbox.Checked = false;
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

            //System.Diagnostics.Debug.WriteLine("Deactivate event start");

            LastDeactivateTime = Environment.TickCount;
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
            SaveSettings?.Invoke(GetChecked(0, AllOrNoneBack), Tag);     // at this level, we return all items.
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

        private class ControlSet
        {
            public ExtCheckBox extcheckbox;        // may not be there is just a click item
            public PictureBox icon;
            public Label label;
            public string tag;  // logical tag for settings
            public string exclusivetags;        // ones which must be off if this is on
            public bool disableuncheck; // no unchecking
            public object usertag;  // user data for item
        };

        private List<ControlSet> controllist;
        private bool ignorechangeevent = false;
        private ExtPanelScroll panelscroll;
        private ExtScrollBar sb;

        private int maxwidth = 0;
        private int maxheight = 0;

        private Size defsize = new Size(24, 24);        // default size if no other sizes given by ImageSize/Icons etc
        private Timer timer = new Timer();      // timer to monitor for entry into form when transparent.. only sane way in forms
        private bool mousebuttonsdown = false;
        private int closedowncount = 0;

        private bool positiondone = false;       // we have executed position, normally done one in onload, but if reset, can be redone

        #endregion
    }
}
