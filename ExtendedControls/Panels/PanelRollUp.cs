/*
 * Copyright 2016-2025 EDDiscovery development team
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
    public class ExtPanelRollUp : ExtPanelAutoHeightWidth
    {
        public int RollUpDelay { get; set; } = 1000;            // before rolling
        public int UnrollHoverDelay { get; set; } = 1000;       // set to large value and forces click to open functionality
        public int RolledUpHeight { get; set; } = 5;
        public int RollUpAnimationTime { get; set; } = 500;            // animation time
        public bool ShowHiddenMarker { get { return hiddenmarkershow; } set { hiddenmarkershow = value; } }

        // 0 = full width >0 width from left, <0 width in centre
        public int HiddenMarkerWidth { get; set; } = 0;
        // 0 = off, else >0 width on far right.  Only set if HiddenMarkerWidth!=0 (not checked)
        public int SecondHiddenMarkerWidth { get; set; } = 0;   

        public bool PinState { get { return pinbutton.Checked; } set { SetPinState(value); } }

        public event EventHandler DeployStarting;
        public event EventHandler DeployCompleted;
        public event EventHandler RetractStarting;
        public event EventHandler RetractCompleted;

        public ExtPanelRollUp()
        {
            SuspendLayout();

            pinbutton = new ExtCheckBox();
            pinbutton.Appearance = Appearance.Normal;
            pinbutton.FlatStyle = FlatStyle.Popup;
            int sz = Font.ScalePixels(32);
            pinbutton.Size = new Size(sz, sz);
            pinbutton.Image = ExtendedControls.Properties.Resources.pindownwhite2;          //colours 222 and 255 used
            pinbutton.ImageUnchecked = ExtendedControls.Properties.Resources.pinupwhite2;
            pinbutton.ImageLayout = ImageLayout.Stretch;
            pinbutton.Checked = true;
            pinbutton.CheckedChanged += Pinbutton_CheckedChanged;
            pinbutton.TickBoxReductionRatio = 1;
            pinbutton.Name = "RUP Pinbutton";
            pinbutton.Anchor = AnchorStyles.None;
            pinbutton.Visible = false;

            hiddenmarker1 = new ExtButtonDrawn();
            hiddenmarker1.Name = "RUP Hidden marker 1";
            hiddenmarker1.ImageSelected = ExtButtonDrawn.ImageType.Bars;
            hiddenmarker1.Visible = false;
            hiddenmarker1.Padding = new Padding(0);
            hiddenmarker1.Click += Hiddenmarker_Click;
            hiddenmarker1.Anchor = AnchorStyles.None;

            hiddenmarker2 = new ExtButtonDrawn();
            hiddenmarker2.Name = "RUP Hidden marker 2";
            hiddenmarker2.ImageSelected = ExtButtonDrawn.ImageType.Bars;
            hiddenmarker2.Visible = false;
            hiddenmarker2.Padding = new Padding(0);
            hiddenmarker2.Click += Hiddenmarker_Click;
            hiddenmarker2.Anchor = AnchorStyles.None;

            Controls.Add(pinbutton);
            Controls.Add(hiddenmarker1);
            Controls.Add(hiddenmarker2);

            ResumeLayout();

            mode = Mode.Down;
            timer = new Timer();
            timer.Tick += Timer_Tick;
        }

        protected override void OnHandleDestroyed(EventArgs e)      // seen it continuing to tick
        {
            timer.Stop();
        }

        public void SetToolTip(ToolTip t, string ttpin = null, string ttmarker = null)
        {
            if (ttpin == null)
                ttpin = "Pin to stop this menu bar disappearing automatically".Tx();
            if (ttmarker == null)
                ttmarker = "Click or hover over this to unroll the menu bar".Tx();
            t.SetToolTip(pinbutton, ttpin);
            t.SetToolTip(hiddenmarker1, ttmarker);
            t.SetToolTip(hiddenmarker2, ttmarker);
        }

        public void SetPinState(bool state)
        {
            pinbutton.Checked = state;
            if (state)
                RollDown();
            else
                RollUp();
        }

        private void Pinbutton_CheckedChanged(object sender, EventArgs e)
        {
            PinStateChanged?.Invoke(this, pinbutton);
        }

        public void SetPinImages(Image up, Image down)
        {
            pinbutton.Image = up;
            pinbutton.ImageUnchecked = down;
        }

        // this panel controls visibility of its children.  Thus you can't use control.visible. use these to find out visiblity state and set it
        // only applicable for direct children of this control. Grandchildren can be controlled normally

        public void SetVisibility(Control c, bool visible)
        {
            System.Diagnostics.Debug.Assert(Controls.Contains(c));
            c.Visible = visible;
            visibleState[c] = visible;
        }

        // is it going to be visible when rolled down?
        public bool IsSetVisible(Control c)
        {
            if (visibleState.TryGetValue(c, out bool res))
                return res;
            else
                return true;
        }

        public void RollDown()      // start the roll down..
        {
            timer.Stop();

            if (mode == Mode.DownAwaitRollDecision)     // cancel the decision
            {
                mode = Mode.Down;
            }
            else if (mode != Mode.Down)
            {
                //  System.Diagnostics.Debug.WriteLine(Environment.TickCount + " roll down, start animating, size " + unrolledheight);
                mode = Mode.RollingDown;
                targetrolltimer.TimeoutAt(RollUpAnimationTime);
                timer.Interval = rolltimerinterval;
                DeployStarting?.Invoke(this, EventArgs.Empty);
                timer.Start();

                foreach (Control c in Controls)     // everything except hidden marker and the pin visible
                {
                    if (c!=hiddenmarker1 && c!= hiddenmarker2 && c!= pinbutton && IsSetVisible(c))
                    {
                        //System.Diagnostics.Debug.WriteLine($"Set ctrl {c.Name} visible");
                        c.Visible = true;
                    }
                }
            }
        }

        public void RollUp()        // start the roll up
        {
            timer.Stop();

            if (mode == Mode.UpAwaitRollDecision)     // cancel the decision
            {
                mode = Mode.Up;
            }
            else if (mode != Mode.Up)
            {
                // System.Diagnostics.Debug.WriteLine(Environment.TickCount + " roll up, start animating , child size " + this.FindMaxSubControlArea(0, 0));
                mode = Mode.RollingUp;
                targetrolltimer.TimeoutAt(RollUpAnimationTime);
                timer.Interval = rolltimerinterval;
                RetractStarting?.Invoke(this, EventArgs.Empty);
                AutoHeightWidthDisable = true;  // disable Auto width/height controls on PanelAutoHeightWidth
                timer.Start();

                autosizedpanels.Clear();
                foreach (Control c in Controls)     // panels attached must not be autosized
                {
                    Panel p = c as Panel;

                    if (p != null && p.AutoSize == true)    // if autosize panel, disable it.
                    {
                        autosizedpanels.Add(p);
                        p.AutoSize = false;
                    }
                }

                wasautosized = this.AutoSize;
                this.AutoSize = false;          // and we must not be..
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            if (ClientRectangle.Width > 0)
            {
                pinbutton.Location = new Point(ClientRectangle.Width - pinbutton.Width - 8, 2);

                int hmwidth = Math.Abs(HiddenMarkerWidth);
                if (hmwidth == 0)
                    hmwidth = ClientRectangle.Width;

                hiddenmarker1.Bounds = new Rectangle((HiddenMarkerWidth < 0) ? ((ClientRectangle.Width - hmwidth) / 2) : 0, 0, hmwidth, hiddenmarker1.Height);
                hiddenmarker2.Bounds = new Rectangle(ClientRectangle.Width - SecondHiddenMarkerWidth, 0, SecondHiddenMarkerWidth, hiddenmarker2.Height);

               //System.Diagnostics.Debug.WriteLine($"RollUp hidden marker {hiddenmarker1.Location}/{hiddenmarker1.Size} : {hiddenmarker2.Left}");
            }

        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            if (mode == Mode.Down)
            {
                unrolledheight = ClientRectangle.Height;
                //System.Diagnostics.Debug.WriteLine(this.Name + " Roll down size" + unrolledheight);
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)      // when a control is hooked, we place a mouse enter/leave so we know we are still within this panel
        {
            base.OnControlAdded(e);
            //System.Diagnostics.Debug.WriteLine("Added " + e.Control.Name + " " + e.Control.GetType().Name);
            e.Control.MouseEnter += Control_MouseEnter;
            e.Control.MouseLeave += Control_MouseLeave;
            foreach (Control c in e.Control.Controls)
            {
                c.MouseEnter += Control_MouseEnter;
                c.MouseLeave += Control_MouseLeave;
                foreach (Control d in c.Controls)
                {
                    c.MouseEnter += Control_MouseEnter;
                    c.MouseLeave += Control_MouseLeave;
                }
            }
        }

        private void StartRollUpTimer()
        {
            if (mode == Mode.Down)
            {
                //System.Diagnostics.Debug.WriteLine($"{Environment.TickCount} RUP start downawaitrolldecision");
                timer.Stop();
                timer.Interval = RollUpDelay;
                timer.Start();
                mode = Mode.DownAwaitRollDecision;
            }
        }

        private void StartRollDownTimer()
        {
            if (mode == Mode.Up)
            {
                //System.Diagnostics.Debug.WriteLine($"{Environment.TickCount} RUP start upawaitrolldecision");
                timer.Stop();
                timer.Interval = UnrollHoverDelay;
                timer.Start();
                mode = Mode.UpAwaitRollDecision;
            }
        }

        protected override void OnMouseEnter(EventArgs eventargs)
        {
            base.OnMouseEnter(eventargs);
            StartRollDownTimer();
            pinbutton.Visible = mode == Mode.Down || mode == Mode.DownAwaitRollDecision;        // if we are in these modes, the pin should be made visible
            //System.Diagnostics.Debug.WriteLine($"{Environment.TickCount} RUP Enter panel Pin {pinbutton.Visible}");
        }

        private void Control_MouseEnter(object sender, EventArgs e)
        {
            StartRollDownTimer();
            pinbutton.Visible = mode == Mode.Down || mode == Mode.DownAwaitRollDecision;
            //System.Diagnostics.Debug.WriteLine($"{Environment.TickCount} RUP Entered {sender.GetType().Name} Pin {pinbutton.Visible}");
        }

        protected override void OnMouseLeave(EventArgs eventargs)
        {
            //System.Diagnostics.Debug.WriteLine($"{Environment.TickCount} RUP Left panel");
            base.OnMouseLeave(eventargs);
            StartRollUpTimer();
        }

        private void Control_MouseLeave(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine($"{Environment.TickCount} RUP Left {sender.GetType().Name}");
            StartRollUpTimer();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            double rollpercent = targetrolltimer.Percentage;
            int rolldiff = unrolledheight - RolledUpHeight;

            bool inarea = this.IsHandleCreated && this.RectangleScreenCoords().Contains(MousePosition.X, MousePosition.Y);

            //System.Diagnostics.Debug.WriteLine($"{Environment.TickCount} {rollpercent}% mode {mode} inarea {inarea}");

            if (mode == Mode.DownAwaitRollDecision)    // timer up, check if out of area and not pinned, if so roll up
            {
                timer.Stop();

                if (!inarea && !pinbutton.Checked)      // if not in area, and its not checked..
                {
                    RollUp();
                }
                else
                {
                    mode = Mode.Down;   // back to down
                    pinbutton.Visible = inarea;     // pin button visiblity reset
                    //System.Diagnostics.Debug.WriteLine($".. Pin {pinbutton.Visible}");
                }
            }
            else if (mode == Mode.RollingUp)        // roll up animation, move one step on, check for end
            {
                Height = Math.Max((int)(unrolledheight - rolldiff * rollpercent), RolledUpHeight);

                Refresh();

                if (Height == RolledUpHeight)    // end
                {
                    targetrolltimer.Stop();
                    timer.Stop();

                    // everything first turned off
                    foreach (Control c in Controls)     
                        c.Visible = false;

                    // then set on the hidden markers
                    hiddenmarker1.Visible = hiddenmarkershow;
                    hiddenmarker2.Visible = hiddenmarkershow && SecondHiddenMarkerWidth > 0;            // only visible if width >0 (not full or centre) and both sides
                    
                    //System.Diagnostics.Debug.WriteLine($".. Hidden {hiddenmarker1.Visible} Pin {pinbutton.Visible}");

                    mode = Mode.Up;
                    RetractCompleted?.Invoke(this, EventArgs.Empty);
                }
            }
            else if (mode == Mode.UpAwaitRollDecision) // timer up.. check if in area, if so roll down
            {
                timer.Stop();

                if (inarea)
                {
                    RollDown();
                }
                else
                {
                    mode = Mode.Up;     // back to up
                }
            }
            else if (mode == Mode.RollingDown) // roll down animation, move one step on, check for end
            {
                Height = Math.Min((int)(RolledUpHeight + rolldiff * rollpercent), unrolledheight);

                Invalidate(true);
                Refresh();

                if (Height == unrolledheight)        // end, everything is already visible.  hide the hidden marker
                {
                    targetrolltimer.Stop();
                    timer.Stop();
                    mode = Mode.Down;

                    AutoHeightWidthDisable = false;

                    hiddenmarker1.Visible = false;          // no markers when down
                    hiddenmarker2.Visible = false;
                    pinbutton.Visible = inarea;             // but maybe pin button if mouse is in area
                    //System.Diagnostics.Debug.WriteLine($".. Hidden {hiddenmarker1.Visible} Pin {pinbutton.Visible}");

                    if (wasautosized)
                        this.AutoSize = true;

                    foreach (Panel p in autosizedpanels)
                        p.AutoSize = true;

                    DeployCompleted?.Invoke(this, EventArgs.Empty);
                }

                if (!inarea && !pinbutton.Checked)      // but not in area now, and not held.. so start roll up procedure
                    StartRollUpTimer();
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            int sz = Font.ScalePixels(32);
            pinbutton.Size = new Size(sz, sz);
        }

        private void Hiddenmarker_Click(object sender, EventArgs e)
        {
            if (mode == Mode.UpAwaitRollDecision)
            {
                //System.Diagnostics.Debug.WriteLine("Cancel roll down pause and expand now");
                timer.Stop();
                Timer_Tick(sender, e);
            }
        }


        private bool wasautosized = false;
        private List<Panel> autosizedpanels = new List<Panel>();

        private ExtCheckBox pinbutton;        // public so you can theme them with colour/IAs
        private ExtButtonDrawn hiddenmarker1;
        private ExtButtonDrawn hiddenmarker2;

        private BaseUtils.MSTicks targetrolltimer = new BaseUtils.MSTicks();
        private const int rolltimerinterval = 25;

        private Action<ExtPanelRollUp, ExtCheckBox> PinStateChanged = null;

        private enum Mode { Up, UpAwaitRollDecision, Down, DownAwaitRollDecision, RollingUp, RollingDown };
        private Mode mode;
        private int unrolledheight;     // on resize, and down, its set.
        private Timer timer;
        private bool hiddenmarkershow = true;           // if to show it at all.

        private Dictionary<Control, bool> visibleState = new Dictionary<Control, bool>();       // presume all visible unless this is set

    }
}
