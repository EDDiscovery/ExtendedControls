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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class UpDown : Control, IThemeable
    {
        // Call Invalidate if you change these..
        public override Color ForeColor { get; set; } = SystemColors.ControlText;       // arrow
        public override Color BackColor { get; set; } = SystemColors.Control;
        public Color BackColor2 { get; set; } = SystemColors.Control;
        public float MouseOverScaling { get; set; } = 1.3F;
        public float MouseSelectedScaling { get; set; } = 1.3F;
        public float GradientDirection { get { return gradientdirection; } set { gradientdirection = value; Invalidate(); } }
        public Color BorderColor { get; set; } = Color.Gray;
        public float DisabledScaling { get; set; } = 0.5F;      // when disabled, scale down colours

        public delegate void OnSelected(object sender, MouseEventArgs e);   
        public event OnSelected Selected;

        #region Implementation
        public UpDown() : base()
        {
            repeatclick = new Timer();
            repeatclick.Tick += new EventHandler(RepeatClick);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            if (ClientRectangle.Width > 0 && ClientRectangle.Height > 0)
            {
                int halfway = ClientRectangle.Height / 2 - 1;
                upper = new Rectangle(1, 0, ClientRectangle.Width - 2, halfway);
                lower = new Rectangle(1, halfway + 2, ClientRectangle.Width - 2, halfway);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (upper.Width == 0)
                return;

            float multup = !Enabled ? DisabledScaling : mousepressed == MouseOver.MouseOverUp ? MouseSelectedScaling : mouseover == MouseOver.MouseOverUp ? MouseOverScaling : 1.0f;
            float multdown = !Enabled ? DisabledScaling : mousepressed == MouseOver.MouseOverDown ? MouseSelectedScaling : mouseover == MouseOver.MouseOverDown ? MouseOverScaling : 1.0f;

            Rectangle area = new Rectangle(0, 0, lower.Width, lower.Height + 1); // seems to make it linear paint between

            //System.Diagnostics.Debug.WriteLine($"Mult Up {multup} {multdown} {BackColor.Multiply(multup)} {BackColor2.Multiply(multup)}");

            using (Brush b = new LinearGradientBrush(area, BackColor.Multiply(multup), BackColor2.Multiply(multup), GradientDirection))
                e.Graphics.FillRectangle(b, upper);

            using (Brush b = new LinearGradientBrush(area, BackColor.Multiply(multdown), BackColor2.Multiply(multdown), GradientDirection))
                e.Graphics.FillRectangle(b, lower);

            using (Pen p = new Pen(BorderColor, 1F))
            {
                int right = ClientRectangle.Width - 1;
                int bottom = ClientRectangle.Height - 1;
                e.Graphics.DrawLine(p, 0, 0, 0, bottom);
                e.Graphics.DrawLine(p, right, 0, right, bottom);
                e.Graphics.DrawLine(p, 0, upper.Height, right, upper.Height);
                e.Graphics.DrawLine(p, 0, upper.Height + 1, right, upper.Height + 1);
            }

            if (Enabled)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                using (Pen p = new Pen(mousepressed == MouseOver.MouseOverUp ? ForeColor.Multiply(MouseSelectedScaling) : ForeColor))
                {
                    int hoffset = upper.Width / 3;
                    int voffset = upper.Height / 3;

                    Point arrowpt1u = new Point(upper.X + hoffset, upper.Y + upper.Height - voffset);
                    Point arrowpt2u = new Point(upper.X + upper.Width / 2, upper.Y + voffset);
                    Point arrowpt3u = new Point(upper.X + upper.Width - hoffset, arrowpt1u.Y);
                    e.Graphics.DrawLine(p, arrowpt1u, arrowpt2u);            // the arrow!
                    e.Graphics.DrawLine(p, arrowpt2u, arrowpt3u);
                }

                using (Pen p = new Pen(mousepressed == MouseOver.MouseOverDown ? ForeColor.Multiply(MouseSelectedScaling) : ForeColor))
                {
                    int hoffset = lower.Width / 3;
                    int voffset = lower.Height / 3;

                    Point arrowpt1d = new Point(lower.X + hoffset, lower.Y + voffset);
                    Point arrowpt2d = new Point(lower.X + lower.Width / 2, lower.Y + lower.Height - voffset);
                    Point arrowpt3d = new Point(lower.X + lower.Width - hoffset, arrowpt1d.Y);

                    e.Graphics.DrawLine(p, arrowpt1d, arrowpt2d);            // the arrow!
                    e.Graphics.DrawLine(p, arrowpt2d, arrowpt3d);

                }
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            }
}

        protected override void OnMouseMove(MouseEventArgs eventargs)
        {
            base.OnMouseMove(eventargs);

            if (upper.Contains(eventargs.Location))
            {
                if (mouseover != MouseOver.MouseOverUp)
                {
                    mouseover = MouseOver.MouseOverUp;
                    Invalidate();
                }
            }
            else if (lower.Contains(eventargs.Location))
            {
                if (mouseover != MouseOver.MouseOverDown)
                {
                    mouseover = MouseOver.MouseOverDown;
                    Invalidate();
                }
            }
            else if (mouseover != MouseOver.MouseOverNone)
            {
                mouseover = MouseOver.MouseOverNone;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs eventargs)
        {
            base.OnMouseLeave(eventargs);
            mouseover = MouseOver.MouseOverNone;
            mousepressed = MouseOver.MouseOverNone;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);

            if (upper.Contains(mevent.Location))
            {
                mousepressed = MouseOver.MouseOverUp;
                Invalidate();
                MouseEventArgs me = new MouseEventArgs(mevent.Button, mevent.Clicks, mevent.X, mevent.Y, 1);
                if (Selected != null)
                    Selected(this, me);
                StartRepeatClick(me);

            }
            else if (lower.Contains(mevent.Location))
            {
                mousepressed = MouseOver.MouseOverDown;
                Invalidate();
                MouseEventArgs me = new MouseEventArgs(mevent.Button, mevent.Clicks, mevent.X, mevent.Y, -1);
                if (Selected != null)
                    Selected(this, me);
                StartRepeatClick(me);
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            mousepressed = MouseOver.MouseOverNone;
            repeatclick.Stop();
            Invalidate();
        }

        private void StartRepeatClick(MouseEventArgs e)
        {
            if (!repeatclick.Enabled)                       // if not enabled, turn it on.  Since this gets repeatedly called we need to check..
            {
                mouseargs = e;
                repeatclick.Interval = 250;
                repeatclick.Start();
            }
        }

        private void RepeatClick(object sender, EventArgs e)
        {
            repeatclick.Interval = 50;                      // resetting interval is okay when enabled
            if (Selected != null)
                Selected(this, mouseargs);
        }

        public bool Theme(Theme t, Font fnt)
        {
            BackColor = t.ButtonBackColor;
            BackColor2 = t.ButtonBackColor2;
            ForeColor = t.ButtonTextColor;
            MouseOverScaling = t.MouseOverScaling;
            MouseSelectedScaling = t.MouseSelectedScaling;
            BorderColor = t.ButtonBorderColor;
            GradientDirection = t.ButtonGradientDirection;
            return false;
        }

        #endregion

        private enum MouseOver {  MouseOverUp , MouseOverDown, MouseOverNone };
        private MouseOver mouseover = MouseOver.MouseOverNone;
        private MouseOver mousepressed = MouseOver.MouseOverNone;
        private Rectangle upper;
        private Rectangle lower;
        private float gradientdirection = 90F;
        private Timer repeatclick;
        private MouseEventArgs mouseargs;
    }
}
