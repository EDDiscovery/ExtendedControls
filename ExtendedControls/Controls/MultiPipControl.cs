/*
 * Copyright © 2022-2024 EDDiscovery development team
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this
 * file except in compliance with the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required bylicable law or agreed to in writing, software distributed under
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
    public class MultiPipControl : Panel
    {
        [System.ComponentModel.Browsable(true)]
        public override string Text { get { return base.Text;} set { base.Text= value; Invalidate(); } }

        public int Value { get { return pips; } set { pips = value; Invalidate(); } }
        public int MaxValue { get { return pipmax; } set { pipmax = value; Invalidate(); } }
        public int PipsPerClick { get { return pipsperclick; } set { pipsperclick = value; Invalidate(); } }

        public Color PipColor { get { return pipcolor; } set { pipcolor = value; Invalidate(); } }
        public Color HalfPipColor { get { return halfpipcolor; } set { halfpipcolor = value; Invalidate(); } }
        public Color BorderColor { get { return bordercolor; } set { bordercolor = value; Invalidate(); } }
        public int BorderWidth { get { return borderwidth; } set { borderwidth = value; Invalidate(); } }

        public int InterSpacing { get { return interspacegap; } set { interspacegap = value; Invalidate(); } }

        public List<MultiPipControl> Others { get;  } = new List<MultiPipControl>();            // add to inform others of a pip click. If set, right click is disabled
        public int PipsTakenPerCLickFromOthers { get; set; } = 1;       // pips removed from others until we get PipsPerClick
        public void Add(MultiPipControl other)      // add another pip control as a source for pips
        {
            Others.Add(other);
        }

        public int Request(int pips)               // give me these number of pips, return what you can give me
        {
            int taken = Math.Min(Value, pips);
            if ( taken>0)
                Value -= taken;
            return taken;
        }

        public MultiPipControl()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |        // "Free" double-buffering (1/3).
                ControlStyles.OptimizedDoubleBuffer |       // "Free" double-buffering (2/3).
                ControlStyles.ResizeRedraw |                // Invalidate after a resize or if the Padding changes.
                ControlStyles.Selectable |                  // We can receive focus from mouse-click or tab (see the Selectable prop).
                ControlStyles.SupportsTransparentBackColor |// BackColor.A can be less than 255.
                ControlStyles.UserPaint |                   // "Free" double-buffering (3/3); OnPaintBackground and OnPaint are needed.
                ControlStyles.UseTextForAccessibility,      // Use Text for the mnemonic char (and accessibility) if not empty, else the previous Label in the tab order.
                true);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            int numpips = pipmax / 2;

            int pipvavailable = ClientRectangle.Height - interspacegap*2 - borderwidth*2;   // what is available for pips
            int pipvcentre = ClientRectangle.Height / 2;        // centre of pip line

            int bordertop = borderwidth;        // where the border top is..

            if (Text.HasChars())        // if we are painting text
            {
                int textvuse = Font.Height + interspacegap; // we use the font height to take off space
                pipvavailable -= textvuse;
                pipvcentre = (ClientRectangle.Height - textvuse) / 2 + textvuse;    // reestimate the pip line
                bordertop += textvuse;      // move the border down

                TextRenderer.DrawText(pe.Graphics, Text, Font, new Rectangle(borderwidth, 0, ClientSize.Width - borderwidth * 2, Font.Height), ForeColor);
            }

            if (BorderColor != Color.Transparent)
            {
                using (Pen pen = new Pen(bordercolor, borderwidth))
                    pe.Graphics.DrawRectangle(pen, borderwidth, bordertop, ClientRectangle.Width - 1 - BorderWidth, ClientRectangle.Height - 1 -bordertop);
            }

            int piphspacingpixels = borderwidth + interspacegap * 2 + borderwidth;      // pixels not in the game of computing size
            int pipwh = Math.Min(pipvavailable, (ClientRectangle.Width - piphspacingpixels) / numpips); // work out min of vavailable vs width available
            float piphmult = (float)(ClientRectangle.Width - piphspacingpixels) / numpips;       // distance between pips keep float for accuracy
            int piph1 = borderwidth + interspacegap + pipwh / 2 + ((int)piphmult - pipwh) /2;      // placing of first pip horizonally, its pipwh/2 plus shift right if its smaller than h available

            for (int i = 0; i < numpips; i++)
            {
                Point centre = new Point((int)(piphmult*i+ piph1) , pipvcentre);
               // System.Diagnostics.Debug.WriteLine($"{i} {ClientSize} Centre {centre} piph1 {piph1} pipmult {piphmult} pipwh {pipwh}");

                int lm = (i + 1) * 2;

                if (pips >= lm - 1)
                {
                    using (Brush br = new SolidBrush(pips >= lm ? pipcolor : halfpipcolor))
                    {
                        pe.Graphics.FillEllipse(br, new Rectangle(centre.X - pipwh / 2, centre.Y - pipwh / 2, pipwh, pipwh));
                    }
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Right)
            {
                if ( Others.Count == 0 && pips>0)
                    Value = Math.Max(0, pips - PipsPerClick);
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (pips < MaxValue)
                {
                    if ( Others.Count > 0 )
                    {
                        int pipswanted = PipsPerClick;
                        int pipsgot = 0;
                        int other = 0;
                        while( pipsgot < pipswanted)    // go around and ask for PipstakenPerClickFromothers until we have enough
                        {
                            int taken = Others[other].Request(PipsTakenPerCLickFromOthers);
                            pipsgot += taken;
                            other = (other + 1) % Others.Count;
                        }

                        Value = Math.Min(MaxValue, pips + PipsPerClick);
                    }
                    else
                        Value = Math.Min(MaxValue, pips + PipsPerClick);

                }
            }
        }


        private int pips = 8;
        private int pipmax = 8;
        private int pipsperclick = 2;
        private Color pipcolor = Color.Orange;
        private Color halfpipcolor = Color.Orange.Multiply(0.75f);
        private Color bordercolor = Color.Orange;
        private int borderwidth = 2;
        private int interspacegap = 1;


    }
}

