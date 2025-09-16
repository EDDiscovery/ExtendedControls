/*
 * Copyright 2025-2025 EDDiscovery development team
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
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ExtendedControls
{
    // this is a coloured area, either solid (BackColor) or gradient filled
    public class DockingPads : Control, IThemeable
    {
        public int SelectedIndex { get { return selectedIndex; } set { SetIndex(value); } }

        // Font selected font family (not size, its autosized)
        // ForeColor is text colour
        public Color LargePad { get { return largePad; } set { largePad = value; Invalidate(); } }
        public Color MediumPad { get { return mediumPad; } set { mediumPad = value; Invalidate(); } }
        public Color SmallPad { get { return smallPad; } set { smallPad = value; Invalidate(); } }
        public Color BorderColor { get { return borderColor; } set { borderColor = value; Invalidate(); } }
        public float NonSelectedIntensity { get { return nonselectedintensity; } set { nonselectedintensity = value; Invalidate(); } }

        public DockingPads()
        {
            ForeColor = Color.Black;
            timer.Tick += T_Tick;
            timer.Interval =50;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void SetIndex(int index)
        {
            if (index != selectedIndex)     // only when changed
            {
                timer.Stop();

                selectedIndex = index;
                selectedintensity = 1.0f;

                if (selectedIndex > 0)
                {
                    timer.Start();
                    starttime = (uint)Environment.TickCount;
                }

                DrawImage();                // force a redraw and invalidate
                Invalidate();
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            int radius = Radius;
            int radiusstart = radius / 8;
            int centrex = ClientRectangle.Width / 2;
            int centrey = ClientRectangle.Height / 2;

            int radients = segments.Length;

            // coord system for windows is not normal with respect to how you would normally draw a cos/sin plot (+Y upwards).
            // +Y is down, making the plot rotate clockwise
            // and the start position will be at 90degrees, 
            // rotate the start pos by 90 anticlockwise and position so centre of pad is up

            double radiantstartpos = (-90.0 - 360.0 / 24).Radians();

            pads = new List<GraphicsPath>();
            pads.Add(null);     // 0 is empty

            for (int i = 0; i < radients; i++)
            {
                double angleleft = i * Math.PI * 2 / radients + radiantstartpos;
                double angleright = (i + 1) * Math.PI * 2 / radients + radiantstartpos;
                //System.Diagnostics.Debug.WriteLine($"{i} at {angleleft.Degrees()} {angleright.Degrees()}");

                // from outer segment inwards
                for (int segment = segments[i] - 1; segment >= 0; segment--)       
                {
                    double lenouter = (radius - radiusstart) * ((double)(segment + 1) / segments[i]) + radiusstart;
                    double leninner = (radius - radiusstart) * ((double)(segment) / segments[i]) + radiusstart;

                    Point p1l = new Point((int)(lenouter * Math.Cos(angleleft)) + centrex, (int)(lenouter * Math.Sin(angleleft)) + centrey);
                    Point p2l = new Point((int)(leninner * Math.Cos(angleleft)) + centrex, (int)(leninner * Math.Sin(angleleft)) + centrey);
                    Point p1r = new Point((int)(lenouter * Math.Cos(angleright)) + centrex, (int)(lenouter * Math.Sin(angleright)) + centrey);
                    Point p2r = new Point((int)(leninner * Math.Cos(angleright)) + centrex, (int)(leninner * Math.Sin(angleright)) + centrey);

                    if (pads.Count == 11)     // far right, place indicators
                    {
                        indicatorsize = radius / 6;
                        indicatorradiusoffset = (int)(lenouter * Math.Cos((angleleft + angleright) / 2)) + indicatorsize/2;
                    }

                    //System.Diagnostics.Debug.WriteLine($"Radius {i} Seg {segment} = {lenouter} {leninner}");

                    var path = new GraphicsPath();
                    path.AddLines(new Point[] { p1l, p1r, p2r, p2l });
                    pads.Add(path);
                }

            }
        }

        // Draw to bitmap, this is done to prevent flicker during indication of pad

        private void DrawImage()
        {
            image?.Dispose();
            image = new Bitmap(Math.Max(1,Width), Math.Max(1,Height));

            using (Graphics gr = Graphics.FromImage(image))
            {
                // paint the pads
                using (Font fnt = GetFont() )
                {
                    for (int pad = 1; pad < pads.Count; pad++)
                    {
                        DrawPad(pad, gr, fnt);
                    }
                }

                if (BorderColor != Color.Empty)
                {
                    // overpaint the dividers
                    using (Pen p = new Pen(BorderColor, 1))
                    {
                        for (int pad = 1; pad < pads.Count; pad++)
                        {
                            gr.DrawPath(p, pads[pad]);
                        }
                    }
                }

                using (Brush br = new SolidBrush(Color.FromArgb(255, 1, 129, 1)))       // avoid pure green as its used for transparency
                {
                    gr.FillEllipse(br, new Rectangle(Width / 2 + indicatorradiusoffset - indicatorsize / 2, Height / 2 - indicatorsize / 2, indicatorsize, indicatorsize));
                }
                using (Brush br = new SolidBrush(Color.FromArgb(255, 128, 1, 1)))
                {
                    gr.FillEllipse(br, new Rectangle(Width / 2 - indicatorradiusoffset - indicatorsize / 2, Height / 2 - indicatorsize / 2, indicatorsize, indicatorsize));
                }
            }
        }

        protected void DrawPad(int pad, Graphics gr, Font fnt)
        {
            Color fill = padsize[pad] == 3 ? LargePad : padsize[pad] == 2 ? MediumPad : SmallPad;
            if (selectedIndex > 0)
            {
                fill = fill.MultiplyBrightness(pad == selectedIndex ? selectedintensity : nonselectedintensity);
            }

            using (Brush br = new SolidBrush(fill))
            {
                gr.FillPath(br, pads[pad]);
            }

            using (Brush br = new SolidBrush(ForeColor))
            {
                using (StringFormat form = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    if (selectedIndex == 0 || pad == selectedIndex)
                    {
                        var bounds = pads[pad].GetBounds();
                        int radius = Radius;

                        if ((pad >= 9 && pad <= 10) || (pad >= 39 && pad <= 40))            // nerf large pads at 2/10 oclock
                            bounds.Offset(0, radius / 25);
                        else if ((pad >= 31 && pad <= 34) || (pad >= 16 && pad <= 19))      // 4/8
                            bounds.Offset(0, -radius / 40);
                        else if ((pad >= 26 && pad <= 30) || (pad >= 41))                    // 7/11
                            bounds.Offset(radius / 40, 0);
                        else if ((pad >= 20 && pad <= 23) || (pad >= 5 && pad <= 8))          // 5/1
                            bounds.Offset(-radius / 40, 0);

                        //   System.Diagnostics.Debug.WriteLine($"Radius {pad} Bounds {bounds}");
                        gr.DrawString($"{pad}", fnt, br, bounds, form);
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (image == null)      // ensure we have an image, start up scenario
                DrawImage();

            e.Graphics.DrawImage(image,new Point(0,0));
        }

        private void T_Tick(object sender, EventArgs e)
        {
            uint timesince = (uint)Environment.TickCount - starttime;
            double timemod = Math.Abs(((int)(timesince % 2000) - 1000) / 1000.0);
            selectedintensity = 0.6f + 0.4f * (float)timemod;

            if (image != null)      // may get race condition of image being null vs tick, so double check for paranoia
            {
                // System.Diagnostics.Debug.WriteLine($"{timesince} {timemod} {intensity}");
                using (Font fnt = GetFont())
                {
                    using (Graphics gr = Graphics.FromImage(image))
                    {
                        DrawPad(selectedIndex, gr, fnt);
                    }
                }

            }
            Invalidate();
        }

        private int Radius
        {
            get
            {
                // reserve some width for indicators, use full height 
                return Math.Min(ClientRectangle.Width * 26 / 32, ClientRectangle.Height) / 2;
            }
        }

        private Font GetFont()
        {
            return new Font(Font.FontFamily, (float)Math.Max(2, Radius / 16));
        }


        // no children
        public bool Theme(Theme t, Font fnt)        
        {
            return false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            DrawImage();
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            DrawImage();
            Invalidate();
        }

        private Color largePad = Color.FromArgb(255,192,1,1), mediumPad = Color.FromArgb(255,192,192,0), smallPad = Color.FromArgb(255,0,192,192), borderColor = Color.Black;
        private int selectedIndex = 0;
        private List<GraphicsPath> pads = new List<GraphicsPath>();
        private Timer timer = new Timer();
        private uint starttime;
        private float selectedintensity = 1.0f;
        private int indicatorsize = 8;
        private int indicatorradiusoffset = 8;
        private float nonselectedintensity = 0.4f;

        private static int[] padsize = new int[] {0,    // entry 0 unused
                                         1,3,2,1,       // 1
                                         2,1,2,2,       // 5
                                         3,3,           // 9
                                         2,1,1,1,2,     // 11
                                         1,3,2,1,       // 16
                                         2,1,2,2,       // 20
                                         3,3,           // 24
                                         2,1,1,1,2,     // 26
                                         1,3,2,1,       // 31
                                         2,1,2,2,       // 35
                                         3,3,           // 39
                                         1,2,2,2,1};    // 41

        static int[] segments = new int[] { 4, 4, 2, 5, 4, 4, 2, 5, 4, 4, 2, 5 };

        private Bitmap image;

    }
}
