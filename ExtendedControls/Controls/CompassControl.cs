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
    public partial class CompassControl : Control, IDisposable
    {
        public int WidthDegrees { get { return widthdegrees; } set { widthdegrees = value; Restart(); } }   // no of degrees to show
        public bool ShowNegativeDegrees { get { return degreeoffset != 0; } set { degreeoffset = value ? -180 : 0; Restart(); } }      // -180 to +180

        // Colours
        public Color StencilColor { get { return stencilcolor; } set { stencilcolor = value; Restart(); } }
        public Color CentreTickColor { get { return centretickcolor; } set { centretickcolor = value; Invalidate(); } } // Transparent turns it off
        public Color BugColor { get { return bugcolor; } set { bugcolor = value; Invalidate(); } }

        // Stencil tick rate
        public int StencilMajorTicksAt { get { return stencilmajortickat; } set { stencilmajortickat = value; Restart(); } }    // degrees
        public int StencilMinorTicksAt { get { return stencilminortickat; } set { stencilminortickat = value; Restart(); } } // degrees
        public bool AutoSetStencilTicks { get { return autosetstencilticks; } set { autosetstencilticks = value; Restart(); } } 

        // Bug
        public double Bug { get { return bug + degreeoffset; } set { MoveBugToBearing(value - degreeoffset); } }    // Nan to disable
        public int BugSizePixels { get { return bugsize; } set { bugsize = value; Invalidate(); } }  

        // Text band size
        public double TextBandRatioToFont { get { return textbandratio; } set { textbandratio = value; Restart(); } }

        // +180 or 360 is allowed as a sub for -180 or 0. Argument exception if out of range for both
        public double Bearing { get { return bearing + degreeoffset; } set { MoveToBearing(value - degreeoffset, false); } }   // go direct
        public double SlewToBearing { get { return slewtobearing + degreeoffset; } set { MoveToBearing(value - degreeoffset , true); } } // slew to
        public int SlewRateDegreesSec { get { return slewrate;} set { slewrate = value; } }

        // optional distance
        public double Distance { get { return distance; } set { distancemessage = string.Empty; distance = value; Invalidate(); } }     // NaN to disable, default
        public string DistanceFormat { get { return distanceformat; } set { distanceformat = value; ; } } // as "text {0:0.##} text". DOES NOT INVALIDATE.
        public void DistanceDisable(string msg) { distance = double.NaN; distancemessage = msg; Invalidate(); }

        // optional glideslope
        public double GlideSlope { get { return glideslope; } set { glideslope = value; Invalidate(); } }   // NaN to disable, default

        // optional message during Disable
        public string DisableMessage { get { return disablemessage; } set { disablemessage = value; Invalidate(); } } // below bar

        // optimised setters - do more at once
        public void Set(double bearing, double bug, double distance, bool slewto = false )
        {
            if (double.IsNaN(bearing))
                Enabled = false;
            else
            {
                if (!Enabled)
                    Enabled = true;

                if (this.distance != distance)
                {
                    this.distance = distance;
                    Invalidate();
                }

                if (this.bug != bug )
                {
                    MoveBugToBearing(bug);
                }

                MoveToBearing(bearing, slewto);
            }
        }

        #region Vars and Init

        private double bearing = 0;       // always 0-360 internally
        private double slewtobearing = 0;  // where we are going
        private int slewrate = 10;          // degrees/sec
        private double bug = double.NaN;  // always 0-360 or Nan
        private int degreeoffset = 0;
        private double distance = double.NaN;
        private string distanceformat = "{0:0.##}";
        private string distancemessage = string.Empty;
        private string disablemessage = string.Empty;
        private double glideslope = double.NaN;
        private Color stencilcolor = Color.Red;
        private int stencilmajortickat = 20;
        private int stencilminortickat = 5;
        private bool autosetstencilticks = false;
        private Color centretickcolor = Color.Green;
        private Color bugcolor = Color.White;
        private int bugsize = 10;
        private int widthdegrees = 180;
        private double textbandratio = 1.5;
        private System.Windows.Forms.Timer slewtimer;
        private System.Diagnostics.Stopwatch slewstopwatch;
        private double accumulateddegrees;

        public CompassControl()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |        // "Free" double-buffering (1/3).
                ControlStyles.OptimizedDoubleBuffer |       // "Free" double-buffering (2/3).
                ControlStyles.ResizeRedraw |                // Invalidate after a resize or if the Padding changes.
                ControlStyles.SupportsTransparentBackColor, // BackColor.A can be less than 255.
                true);
            slewtimer = new Timer();
            slewtimer.Tick += Slewtimer_Tick;
            slewtimer.Interval = 50;
            slewstopwatch = new System.Diagnostics.Stopwatch();
        }

        public new void Dispose()
        {
            compass?.Dispose();
            slewtimer.Dispose();
        }

        private void MoveToBearing(double v, bool slew)     // always 0-360 incl
        {
            if (v >= 0 && v <= 360)     
            {
                v = v % 360;

                double delta = Math.Abs(bearing - v);
                double pixelstomove = delta * pixelsperdegree;

                //System.Diagnostics.Debug.WriteLine("B {0} to {1} Dist {2} pixels {3} slew {4}", bearing, v, delta, pixelstomove, slew);

                slewstopwatch.Reset();

                if (pixelstomove >= 1) // if visual change..
                {
                    if (slew)
                    {
                        slewtobearing = v;
                        slewtimer.Start();
                        slewstopwatch.Start();
                        accumulateddegrees = 0;
                    }
                    else
                    {                               // setting this stops any slewing and Stop.
                        slewtimer.Stop();
                        slewtobearing = bearing = v;
                        Invalidate();
                    }
                }
                else
                {
                    slewtimer.Stop();
                    slewtobearing = bearing = v;        // no need to repaint.
                }
            }
            else
                throw new ArgumentOutOfRangeException();
        }

        private void MoveBugToBearing(double v)
        {
            if (double.IsNaN(v))
            {
                if (!double.IsNaN(bug))
                {
                    bug = v;
                    Invalidate();
                }
            }
            else if (v >= 0 && v <= 360)
            {
                v = v % 360;

                double delta = Math.Abs(bug - v);       // how far..
                double pixelstomove = delta * pixelsperdegree;

                bug = v;

                if (pixelstomove >= 1)
                    Invalidate();
            }
            else
                throw new ArgumentOutOfRangeException();

            Invalidate();
        }

        private void Slewtimer_Tick(object sender, EventArgs e)
        {
            double degmovenow = (double)slewrate * (double)slewstopwatch.ElapsedMilliseconds / 1000.0;     // at this point we should have moved this number of degrees
            double step = degmovenow - accumulateddegrees;      // so we need to step this
            accumulateddegrees = degmovenow;

            double delta = (slewtobearing - bearing + 360) % 360;     // modulo

            //System.Diagnostics.Debug.WriteLine("B {0} to {1} Delta {2} step {3} degofmovement {4}", bearing, slewtobearing, delta, step, degmovenow);

            if (delta >= step && delta <= 360.0 - step)  // if difference is bigger than step, either way around..
            {
                bearing += (delta < 180) ? step : -step;
                bearing = (bearing + 360) % 360;
            }
            else
            {
                slewtimer.Stop();
                slewstopwatch.Reset();
                bearing = slewtobearing;
                //System.Diagnostics.Debug.WriteLine("..stop at {0}", bearing);
            }
            Invalidate();
        }

        private void Restart()
        {
            compass?.Dispose();
            compass = null;
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            Restart();  // resize needs a new compass drawn
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);      
            Restart();  // font change needs a new compass drawn
        }

        #endregion

        #region Paint

        private Bitmap compass = null;      // holds bitmap of compass
        private int majortickcomputedheight = 1;    // holds what the centre tick height was
        System.Drawing.Drawing2D.SmoothingMode textsmoothingmode;   // holds computed smoothing mode, dependent on back colour transparency
        private bool lastdrawnenablestate = true;
        private double pixelsperdegree = 1;                 // set by compass

        // paint compass to compass variable, return majortickcomputedheight
        // compass is sizes to height-reserved/width of client area
        private void PaintCompass(int bottomreserved)
        {
            compass?.Dispose();     // ensure we are clean
            compass = null;

            pixelsperdegree = (double)this.Width / (double)WidthDegrees;

            int bitmapwidth = Math.Max(1, (int)(360 * pixelsperdegree));        // size of bitmap
            int bitmapheight = Math.Max(1, Height - bottomreserved);

            compass = new Bitmap(bitmapwidth, bitmapheight);

            using (Graphics g = Graphics.FromImage(compass))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

                if (!BackColor.IsFullyTransparent())
                {
                    using (Brush b = new SolidBrush(BackColor))
                        g.FillRectangle(b, new Rectangle(0, 0, compass.Width, compass.Height));
                }

                int yline = 0;

                SizeF sz = g.MeasureString("360", Font);
                int fontline = bitmapheight - (int)(sz.Height + 1);
                majortickcomputedheight = fontline;
                int smalltickdepth = majortickcomputedheight / 2;

                System.Diagnostics.Debug.WriteLine($"Draw compass {Width} {WidthDegrees} = pix/deg {pixelsperdegree} bm {bitmapwidth} x {bitmapheight} bigtick {majortickcomputedheight} small {smalltickdepth} fontline {fontline}");

                int stmajor = stencilmajortickat;
                int stminor = stencilminortickat;

                if ( autosetstencilticks )
                {
                    double minmajorticks = sz.Width / pixelsperdegree;        
                    // System.Diagnostics.Debug.WriteLine("Major min ticks at {0} = {1}", sz.Width, minmajorticks);
                    if (minmajorticks >= 40)
                    {
                        stmajor = 80; stminor = 20;
                    }
                    else if (minmajorticks >= 20)
                    {
                        stmajor = 40; stminor = 10;
                    }
                    else if (minmajorticks >= 10)
                    {
                        stmajor = 20; stminor = 5;
                    }
                    else 
                    {
                        stmajor = 10; stminor = 2;
                    }
                }

                Color sc = Enabled ? StencilColor : StencilColor.Multiply(0.5F);
                Pen p1 = new Pen(sc, 1);
                Pen p2 = new Pen(sc, 2);
                Brush textb = new SolidBrush(Enabled ? this.ForeColor : this.ForeColor.Multiply(0.5F));
                var fmt = DrawingHelpersStaticFunc.StringFormatFromContentAlignment(ContentAlignment.MiddleCenter);

                for (int d = pixelstart; d < 360 + pixelstart; d++)
                {
                    int x = (int)((d - pixelstart) * pixelsperdegree);

                    bool majortick = d % stmajor == 0;
                    bool minortick = (d % stminor == 0) && !majortick;

                    if (majortick)
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                        g.DrawLine(p2, new Point(x, yline), new Point(x, yline + majortickcomputedheight));
                        g.SmoothingMode = textsmoothingmode;
                        g.DrawString(ToVisual(d).ToStringInvariant(), this.Font, textb, new Rectangle(x - 30, fontline, 60, compass.Height - fontline), fmt);

                        //DEBUG g.DrawLine(p1, x - sz.Width / 2, fontline, x + sz.Width / 2, fontline);
                    }

                    if ( minortick )
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                        g.DrawLine(p1, new Point(x, yline), new Point(x, yline + smalltickdepth));
                    }

                }

                p1.Dispose();
                p2.Dispose();
                textb.Dispose();
                fmt.Dispose();
            }
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (DesignMode)        // FromImage craps out in designer, so turn this whole thing off
                return;

            var newtextsmoothingmode = BackColor.IsFullyTransparent() ? System.Drawing.Drawing2D.SmoothingMode.None : System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // these make the compass bitmap redraw

            if (compass == null || Enabled != lastdrawnenablestate || newtextsmoothingmode != textsmoothingmode)
            {
                SizeF sz = pe.Graphics.MeasureString("GgJjy", Font);
                PaintCompass(Math.Min(Height / 2, (int)(sz.Height * textbandratio + 1)));
                lastdrawnenablestate = Enabled;
                textsmoothingmode = newtextsmoothingmode;
            }

            //System.Diagnostics.Debug.WriteLine("Bearing {0} bug {1}", bearing, bug);

            using (Brush textb = new SolidBrush(this.ForeColor))
            {
                double startdegree = bearing - WidthDegrees / 2;       // where do we start

                int p1start = Bitmappixel((360 + startdegree) % 360);       // this whole bit was a bit mind warping!
                int p1width = Math.Min(compass.Width - p1start, this.Width);     // paint all you can up to compass end, limited by control width
                int left = this.Width - p1width;                        // and this is what we need from the start of the image..

                //System.Diagnostics.Debug.WriteLine("start {0} First paint {1} w {2} then {3}", startdegree, p1start, p1width, left);

                pe.Graphics.DrawImage(compass, new Rectangle(0, 0, p1width, compass.Height), p1start, 0, p1width, compass.Height, GraphicsUnit.Pixel);
                if (left > 0)
                    pe.Graphics.DrawImage(compass, new Rectangle(p1width, 0, left, compass.Height), 0, 0, left, compass.Height, GraphicsUnit.Pixel);

                int textheight = Height - compass.Height;

                if (!Enabled)
                {
                    if (disablemessage.Length > 0)
                    {
                        var fmt = DrawingHelpersStaticFunc.StringFormatFromContentAlignment(ContentAlignment.MiddleCenter);
                        pe.Graphics.SmoothingMode = textsmoothingmode;
                        pe.Graphics.DrawString(disablemessage, this.Font, textb, new Rectangle(0, compass.Height, this.Width, textheight), fmt);
                        fmt.Dispose();
                    }
                }
                else
                {
                    if (!CentreTickColor.IsFullyTransparent())
                    {
                        using (Pen p2 = new Pen(CentreTickColor, 4))
                            pe.Graphics.DrawLine(p2, new Point(this.Width / 2, 0), new Point(this.Width / 2, majortickcomputedheight));
                    }

                    string distancetext = (double.IsNaN(distance) ? "" : string.Format(distanceformat, distance)) + distancemessage;
                    string glideslopetext = double.IsNaN(glideslope) ? "" : " (glideslope " + glideslope.ToString("0") + "°) ";

                    if (!double.IsNaN(bug))     // if bug is running.
                    {
                        using (Brush bbrush = new SolidBrush(BugColor))
                        {
                            int bugpixel = Bitmappixel(bug);                 // which pixel in the image is this?
                            int bugx = (bugpixel >= p1start) ? (bugpixel - p1start) : (bugpixel + p1width); // adjust to account for image wrap
                            double delta = (bug - bearing + 360) % 360;

                            //System.Diagnostics.Debug.WriteLine("bug {0} {1} => {2} Delta {3}", p1start, bugpixel, bugx, delta);

                            if (bugx < BugSizePixels || bugx >= this.Width - BugSizePixels)     // if not withing display range for bug
                            {
                                Rectangle textrectangle;
                                StringFormat fmt;
                                string text;

                                int bugy = compass.Height + (Height - compass.Height) / 2;      // centre of bug

                                bool itsleft = bugx < BugSizePixels || (delta > 180 && delta <= 270);

                                if (itsleft)
                                {
                                    pe.Graphics.FillPolygon(bbrush, new Point[3] { new Point(0, bugy), new Point(BugSizePixels * 2, bugy - BugSizePixels), new Point(BugSizePixels * 2, bugy + BugSizePixels) });

                                    int xmargin = BugSizePixels * 2;
                                    textrectangle = new Rectangle(xmargin, compass.Height, this.Width - xmargin, textheight);
                                    fmt = DrawingHelpersStaticFunc.StringFormatFromContentAlignment(ContentAlignment.MiddleLeft);
                                    text = ToVisual(bug) + "° " + distancetext;
                                }
                                else
                                {
                                    pe.Graphics.FillPolygon(bbrush, new Point[3] { new Point(this.Width - 1, bugy), new Point(this.Width - 1 - BugSizePixels * 2, bugy + BugSizePixels), new Point(this.Width - 1 - BugSizePixels * 2, bugy - BugSizePixels) });

                                    int xmargin = 2;
                                    textrectangle = new Rectangle(0, compass.Height, this.Width - BugSizePixels * 2 - xmargin, textheight);
                                    fmt = DrawingHelpersStaticFunc.StringFormatFromContentAlignment(ContentAlignment.MiddleRight);
                                    text = distancetext + " " + ToVisual(bug) + "°";
                                }

                                pe.Graphics.SmoothingMode = textsmoothingmode;
                                pe.Graphics.DrawString(text, this.Font, textb, textrectangle, fmt);

                                fmt.Dispose();
                            }
                            else
                            {
                                int bugy = compass.Height;      // at top of bar, to compass bottom
                                pe.Graphics.FillPolygon(bbrush, new Point[3] { new Point(bugx, bugy), new Point(bugx - BugSizePixels, bugy + BugSizePixels * 2), new Point(bugx + BugSizePixels, bugy + BugSizePixels * 2) });

                                if (distancetext.Length > 0)  // if anything to paint
                                {
                                    Rectangle textrectangle;
                                    StringFormat fmt;

                                    if (bugx > this.Width / 2)    // left or right, dependent
                                    {
                                        fmt = DrawingHelpersStaticFunc.StringFormatFromContentAlignment(ContentAlignment.MiddleRight);
                                        textrectangle = new Rectangle(0, compass.Height, bugx - BugSizePixels * 1, textheight);
                                    }
                                    else
                                    {
                                        int xs = bugx + BugSizePixels * 1;
                                        textrectangle = new Rectangle(xs, compass.Height, this.Width - xs, textheight);
                                        fmt = DrawingHelpersStaticFunc.StringFormatFromContentAlignment(ContentAlignment.MiddleLeft);
                                    }

                                    pe.Graphics.SmoothingMode = textsmoothingmode;
                                    pe.Graphics.DrawString(distancetext + glideslopetext, this.Font, textb, textrectangle, fmt);
                                    fmt.Dispose();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (distancetext.Length > 0)
                        {
                            var fmt = DrawingHelpersStaticFunc.StringFormatFromContentAlignment(ContentAlignment.MiddleCenter);
                            pe.Graphics.SmoothingMode = textsmoothingmode;
                            pe.Graphics.DrawString(distancetext, this.Font, textb, new Rectangle(0, compass.Height, this.Width, 24), fmt);
                            fmt.Dispose();
                        }
                    }
                }
            }
        }

        // debug used to check accuracy of below - keep for now       int[] pixelatdegree = new int[360];       //            return pixelatdegree[(int)degree - pixelstart]; pixelatdegree[d - pixelstart] = x;

        private int ToVisual(double h)      // just correct the visual rep of degree.
        {
            int r = ((int)h + degreeoffset); return (r == -180) ? 180 : r;
        }

        private const int pixelstart = -5;  // ?? can't remember why we need this

        int Bitmappixel(double degree)      // handle the strange -5 pixelstart stuff
        {
            if (degree >= 360 + pixelstart)
                degree -= 360;
            return (int)((degree - pixelstart) * pixelsperdegree);
        }

        #endregion
    }
}
