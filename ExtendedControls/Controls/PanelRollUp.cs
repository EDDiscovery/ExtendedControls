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
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ExtPanelRollUp : Panel
    {
        public int RollUpDelay { get; set; } = 1000;            // before rolling
        public int UnrollHoverDelay { get; set; } = 1000;       // set to large value and forces click to open functionality
        public int RolledUpHeight { get; set; } = 5;
        public int RollUpAnimationTime { get; set; } = 500;            // animation time
        public bool ShowHiddenMarker { get { return hiddenmarkershow; } set { hiddenmarkershow = value; SetHMViz(); } }

        public int HiddenMarkerWidth { get; set; } = 0;   // 0 = full width >0 width from left, <0 width in centre
        public int SecondHiddenMarkerWidth { get; set; } = 0;   // 0 = off, else >0 width on far right.  Only set if HiddenMarkerWidth!=0 (not checked)

        public bool PinState { get { return pinbutton.Checked; } set { SetPinState(value); } }

        public event EventHandler DeployStarting;
        public event EventHandler DeployCompleted;
        public event EventHandler RetractStarting;
        public event EventHandler RetractCompleted;

        private ExtCheckBox pinbutton;        // public so you can theme them with colour/IAs
        private ExtButtonDrawn hiddenmarker1;
        private ExtButtonDrawn hiddenmarker2;

        long targetrolltickstart;     // when the roll is supposed to be in time
        const int rolltimerinterval = 25;

        Action<ExtPanelRollUp, ExtCheckBox> PinStateChanged = null;

        enum Mode { Up, UpAwaitRollDecision, Down, DownAwaitRollDecision, RollingUp, RollingDown };
        private int unrolledheight;     // on resize, and down, its set.
        Mode mode;
        Timer timer;
        bool hiddenmarkershow = true;           // if to show it at all.
        bool hiddenmarkershouldbeshown = false; // if to show it now

        public ExtPanelRollUp()
        {
            SuspendLayout();

            pinbutton = new ExtCheckBox();
            pinbutton.Appearance = Appearance.Normal;
            pinbutton.FlatStyle = FlatStyle.Popup;
            pinbutton.Size = new Size(32, 32);
            pinbutton.Image = ExtendedControls.Properties.Resources.pindownwhite2;          //colours 222 and 255 used
            pinbutton.ImageUnchecked = ExtendedControls.Properties.Resources.pinupwhite2;
            pinbutton.ImageLayout = ImageLayout.Stretch;
            pinbutton.Checked = true;
            pinbutton.CheckedChanged += Pinbutton_CheckedChanged;
            pinbutton.TickBoxReductionRatio = 1;
            pinbutton.Name = "RUP Pinbutton";

            hiddenmarker1 = new ExtButtonDrawn();
            hiddenmarker1.Name = "Hidden marker";
            hiddenmarker1.ImageSelected = ExtButtonDrawn.ImageType.Bars;
            hiddenmarker1.Visible = false;
            hiddenmarker1.Padding = new Padding(0);
            hiddenmarker1.Click += Hiddenmarker_Click;

            hiddenmarker2 = new ExtButtonDrawn();
            hiddenmarker2.Name = "Hidden marker";
            hiddenmarker2.ImageSelected = ExtButtonDrawn.ImageType.Bars;
            hiddenmarker2.Visible = false;
            hiddenmarker2.Padding = new Padding(0);
            hiddenmarker2.Click += Hiddenmarker_Click;

            Controls.Add(pinbutton);
            Controls.Add(hiddenmarker1);
            Controls.Add(hiddenmarker2);

            ResumeLayout();

            mode = Mode.Down;
            timer = new Timer();
            timer.Tick += Timer_Tick;

            SetPin(false);
        }

        protected override void OnHandleDestroyed(EventArgs e)      // seen it continuing to tick
        {
            timer.Stop();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            pinbutton.BackColor = BackColor;
            hiddenmarker1.BackColor = hiddenmarker2.BackColor = BackColor;
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

        private void SetHMViz()
        {
            bool hm1vis = hiddenmarkershow && hiddenmarkershouldbeshown;        // done this way, don't trust visible property, sometimes it can be delayed being set so can't reread it
            hiddenmarker1.Visible = hm1vis;
            hiddenmarker2.Visible = hm1vis && SecondHiddenMarkerWidth > 0;            // only visible if width >0 (not full or centre) and both sides
//            System.Diagnostics.Debug.WriteLine("Hidden vis " + hm1vis + " ci set"  + hiddenmarker1.Size + hiddenmarker1.Location);
        }

        public void SetToolTip(ToolTip t, string ttpin = null, string ttmarker = null)
        {
            if (ttpin == null)
                ttpin = "Pin to stop this menu bar disappearing automatically".Tx("RUPPin");
            if (ttmarker == null)
                ttmarker = "Click or hover over this to unroll the menu bar".Tx("RUPMarker");
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

        public void RollDown()      // start the roll down..
        {
            timer.Stop();

            if (mode == Mode.DownAwaitRollDecision)     // cancel the decision
                mode = Mode.Down;
            else if (mode != Mode.Down)
            {
                //System.Diagnostics.Debug.WriteLine(Environment.TickCount + " roll down, start animating, size " + unrolledheight);
                mode = Mode.RollingDown;
                targetrolltickstart = Environment.TickCount;
                timer.Interval = rolltimerinterval;
                DeployStarting?.Invoke(this, EventArgs.Empty);
                timer.Start();

                foreach (Control c in Controls)     // everything except hidden marker visible
                {
                    if (!Object.ReferenceEquals(c, hiddenmarker1) && !Object.ReferenceEquals(c, hiddenmarker2))
                        c.Visible = true;
                }
            }
        }

        public void RollUp()        // start the roll up
        {
            timer.Stop();

            if (mode == Mode.UpAwaitRollDecision)     // cancel the decision
                mode = Mode.Up;
            else if (mode != Mode.Up )
            {
                //System.Diagnostics.Debug.WriteLine(Environment.TickCount + " roll up, start animating , child size " + this.FindMaxSubControlArea(0, 0));
                mode = Mode.RollingUp;
                targetrolltickstart = Environment.TickCount;
                timer.Interval = rolltimerinterval;
                RetractStarting?.Invoke(this, EventArgs.Empty);
                timer.Start();

                foreach (Control c in Controls)     // panels attached must not be autosized
                {
                    if (c is Panel)
                        (c as Panel).AutoSize = false;
                }

                wasautosized = this.AutoSize;
                this.AutoSize = false;          // and we must not be..
            }
        }

        bool wasautosized = false;

        private void StartRollUpTimer()
        {
            if (mode == Mode.Down )
            {
                //System.Diagnostics.Debug.WriteLine(Environment.TickCount + " start roll up pause mode " + mode + " -> PauseBeforeRollUp");
                timer.Stop();
                timer.Interval = RollUpDelay;
                timer.Start();
                mode = Mode.DownAwaitRollDecision;
            }
        }

        private void StartRollDownTimer()
        {
            if (mode == Mode.Up )
            {
                //System.Diagnostics.Debug.WriteLine(Environment.TickCount + " start roll down pause mode " + mode + " -> PauseBeforeRollDown");
                timer.Stop();
                timer.Interval = UnrollHoverDelay;
                timer.Start();
                mode = Mode.UpAwaitRollDecision;
            }
        }

        protected override void OnMouseEnter(EventArgs eventargs)
        {
            //System.Diagnostics.Debug.WriteLine("RUP Enter panel");
            base.OnMouseEnter(eventargs);
            StartRollDownTimer();
            SetPin(true);
        }

        private void Control_MouseEnter(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("RUP Entered " + sender.GetType().Name);
            StartRollDownTimer();
            SetPin(true);
        }

        protected override void OnMouseLeave(EventArgs eventargs)
        {
            //System.Diagnostics.Debug.WriteLine("RUP Left panel");
            base.OnMouseLeave(eventargs);
            StartRollUpTimer();
        }

        private void Control_MouseLeave(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("RUP Left " + sender.GetType().Name);
            StartRollUpTimer();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            if (ClientRectangle.Width > 0)
            {
                pinbutton.Left = ClientRectangle.Width - pinbutton.Width - 8;
                pinbutton.Top = 0;

                int hmwidth = Math.Abs(HiddenMarkerWidth);
                if (hmwidth == 0)
                    hmwidth = ClientRectangle.Width;

                hiddenmarker1.Left = (HiddenMarkerWidth < 0) ? ((ClientRectangle.Width - hmwidth) / 2) : 0;
                hiddenmarker2.Left = ClientRectangle.Width - SecondHiddenMarkerWidth;       // shown on right, when visible
                hiddenmarker1.Width = hmwidth;
                hiddenmarker2.Width = SecondHiddenMarkerWidth;       // shown on right, when visible
            }

            base.OnLayout(levent);
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

        private void Timer_Tick(object sender, EventArgs e)
        {
            double rollpercent = (double)(Environment.TickCount - targetrolltickstart) / RollUpAnimationTime;
            int rolldiff = unrolledheight - RolledUpHeight;

            bool inarea = this.IsHandleCreated && this.RectangleScreenCoords().Contains(MousePosition.X, MousePosition.Y);

            //System.Diagnostics.Debug.WriteLine(Environment.TickCount + " " + rollpercent + " mode " + mode + " inarea " + inarea);

            if (mode == Mode.RollingUp)        // roll up animation, move one step on, check for end
            {
                Height = Math.Max((int)(unrolledheight - rolldiff * rollpercent), RolledUpHeight);
                //System.Diagnostics.Debug.WriteLine(Environment.TickCount + " At " + Height + " child size " + this.FindMaxSubControlArea(0, 0) + " child " + this.Controls[3].FindMaxSubControlArea(0, 0));

                if (Height == RolledUpHeight)    // end
                {
                    timer.Stop();
                    foreach (Control c in Controls)
                    {
                        if (!Object.ReferenceEquals(c, hiddenmarker1) && !Object.ReferenceEquals(c, hiddenmarker2))       // everything hidden but hm
                            c.Visible = false;
                    }

                    hiddenmarkershouldbeshown = true;
                    SetHMViz();
                    System.Diagnostics.Debug.WriteLine(Environment.TickCount + " At min h" + Height);

                    mode = Mode.Up;
                    RetractCompleted?.Invoke(this, EventArgs.Empty);
                }
            }
            else if (mode == Mode.RollingDown) // roll down animation, move one step on, check for end
            {
                Height = Math.Min((int)(RolledUpHeight + rolldiff * rollpercent), unrolledheight);
                //System.Diagnostics.Debug.WriteLine(Environment.TickCount + " At " + Height);

                if (Height == unrolledheight)        // end, everything is already visible.  hide the hidden marker
                {
                    timer.Stop();
                    mode = Mode.Down;
                    hiddenmarkershouldbeshown = false;
                    SetHMViz();

                    if (wasautosized)
                    {
                        this.AutoSize = true;

                        foreach (Control c in Controls)     // all panels below become autosized.. shortcut for now.
                        {
                            if (c is Panel)
                                (c as Panel).AutoSize = true;
                        }
                    }

                    DeployCompleted?.Invoke(this, EventArgs.Empty);
                }

                if (!inarea && !pinbutton.Checked)      // but not in area now, and not held.. so start roll up procedure
                    StartRollUpTimer();
            }
            else if (mode == Mode.UpAwaitRollDecision) // timer up.. check if in area, if so roll down
            {
                timer.Stop();

                if (inarea)
                    RollDown();
                else
                    mode = Mode.Up;     // back to up
            }
            else if (mode == Mode.DownAwaitRollDecision)    // timer up, check if out of area and not pinned, if so roll up
            {
                timer.Stop();

                pinbutton.Visible = inarea;     // visible is same as inarea flag..

                //System.Diagnostics.Debug.WriteLine("Pause before - in area" + inarea + " pin " + pinbutton.Checked);
                if (!inarea && !pinbutton.Checked)      // if not in area, and its not checked..
                    RollUp();
                else
                    mode = Mode.Down;   // back to down
            }
        }


        private void SetPin(bool vis)
        {
            int sz = Font.ScalePixels(32);
            pinbutton.Size = new Size(sz, sz);
            pinbutton.Visible = vis;
        }

    }
}