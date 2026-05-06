/*
 * Copyright 2026-2026 EDDiscovery development team
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
    public class ExtProgressBar : Control, IThemeable
    {
        // Back used as background of whole control. Fore is not used
        public Color BarColor { get { return barcolor; } set { barcolor = value; Invalidate(); } }
        public Color BarHighlightColor { get { return barcolorhighlight; } set { barcolorhighlight = value; Invalidate(); } }
        public Color LimitColor { get { return limitcolor; } set { limitcolor = value; Invalidate(); } }
        public Color LimitHighlightColor { get { return limithighlightcolor; } set { limithighlightcolor = value; Invalidate(); } }
        public Color LimitLineColor { get { return limitlinecolor; } set { limitlinecolor = value; Invalidate(); } }
        public Color BorderColor { get { return bordercolor; } set { bordercolor = value; Invalidate(); } }
        public int Maximum { get { return max; } set { Set(curvalue, value, min, limit); } }
        public int Limit { get { return limit; } set { Set(curvalue, max, min, value); } }
        public int Minimum { get { return min; } set { Set(curvalue, max, value, limit); } }
        public int Value { get { return curvalue; } set { Set(value, max, min, limit); } }
        public int Marker1 { get { return markers[0]; } set { markers[0] = value; Invalidate(); } }
        public int Marker2 { get { return markers[1]; } set { markers[1] = value; Invalidate(); } }
        public Color MarkerLineColor { get { return markerlinecolor; } set { markerlinecolor = value; Invalidate(); } }
        public double BarHeightReserve { get; set; } = 25;  // %
        public int BarWidthMargin { get; set; } = 4;        // Pixel
        public int MarkerWidth { get; set; } = 2;           // Pixel
        public int TrackSpeed { get; set; } = 5;            // Value incremented/decremeneted per tick
        public int BarMaximumPercent { get; set; } = 400;   // 0-100 is sweeping across the area of the bar (as set), 100-400 moves the delta point further right
        public int BarMaximumPercentNoUpdate { get; set; } = 300;        // 300-400 does not cause invalidates

        #region Events

        public event EventHandler ValueChanged
        {
            add { Events.AddHandler(EVENT_VALUECHANGED, value); }
            remove { Events.RemoveHandler(EVENT_VALUECHANGED, value); }
        }

        #endregion

        #region Implementation

        public ExtProgressBar() : base()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                                ControlStyles.AllPaintingInWmPaint |        // "Free" double-buffering (1/3).
                                ControlStyles.OptimizedDoubleBuffer,
                                true );
            wintimer.Tick += T_Tick;
            wintimer.Start();
        }

        int bitmap = 0;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle area = ClientRectangle;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            if (!BorderColor.IsFullyTransparent())
            {
                using (GraphicsPath g1 = DrawingHelpersStaticFunc.RectCutCorners(1, 1, ClientRectangle.Width - 2, ClientRectangle.Height - 1, 1, 1))
                {
                    using (Pen pc1 = new Pen(BorderColor, 1.0F))
                        e.Graphics.DrawPath(pc1, g1);
                }

                area.Inflate(-2, -2);
            }

            int hres = (int)(BarHeightReserve * area.Height / 100.0);

            Rectangle bararea = new Rectangle(area.X + BarWidthMargin, area.Y + hres, area.Width - BarWidthMargin*2, area.Height - hres*2);

            int barwidth = (int)(((double)trackto / max) * bararea.Width);

            if (barwidth > 0)
            {
                Color one = trackto > limit ? LimitColor : BarColor;
                Color two = trackto > limit ? LimitHighlightColor : BarHighlightColor;

                int highlightpos = (int)((double)barwidth * highlightsweep / 100.0);

                // best order to prevent artifacts, right first, then left
                
                var rr = new Rectangle(bararea.X + highlightpos -1, bararea.Y, barwidth - highlightpos+1, bararea.Height);
                if (rr.Width > 0)
                {
                    using (Brush br1 = new LinearGradientBrush(rr, two, one, 0.0))
                        e.Graphics.FillRectangle(br1, rr);
                }

                // the -1 is to force the first left pixel, which appears to error (https://stackoverflow.com/questions/110081/lineargradientbrush-artifact-workaround) off screen

                var rl = new Rectangle(bararea.X-1, bararea.Y, highlightpos+1, bararea.Height);

                var rll = new Rectangle(bararea.X, bararea.Y, Math.Min(highlightpos,barwidth), bararea.Height);
                if (rll.Width>0)
                {
                    using (Brush br1 = new LinearGradientBrush(rl, one, two, 0.0))
                    {
                        e.Graphics.FillRectangle(br1, rll);

                        //if (bitmap < 100)     // keep test code
                        //{
                        //    Bitmap bmp = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                        //    using (Graphics gg = Graphics.FromImage(bmp))
                        //    {
                        //        gg.FillRectangle(br1, rll);
                        //    }

                        //    System.Diagnostics.Debug.WriteLine($"Draw {bitmap} brush {rl} -> into {rll}");
                        //    bmp.Save($"c:\\code\\AA\\{bitmap}.bmp");

                        //    bmp.Dispose();

                        //    bitmap++;
                        //}
                    }
                }
            }

            // if limit is set other than max, show it
            if (limit >= 0 && limit < max)
            {
                int pos = bararea.X + (int)(((double)limit / max) * bararea.Width);
                using (Pen pc1 = new Pen(LimitLineColor, MarkerWidth))
                    e.Graphics.DrawLine(pc1, new Point(pos, area.Y), new Point(pos, area.Y + area.Height));     // 1 more pixel due to it not drawing last
            }

            foreach (int marker in markers)
            {
                // only show if up to trackto
                if (marker >= 0 && marker <= trackto)
                {
                    int pos = bararea.X + (int)(((double)marker / max) * bararea.Width);
                    using (Pen pc1 = new Pen(MarkerLineColor, MarkerWidth))
                        e.Graphics.DrawLine(pc1, new Point(pos, bararea.Y), new Point(pos, bararea.Y + bararea.Height + 1));     // 1 more pixel due to it not drawing last
                }
            }
        }

        private void Set(int cv, int mx, int mi ,int lm )
        {
            int oldvalue = curvalue;
            max = mx;
            min = mi;
            limit = lm;
            curvalue = Math.Max(Math.Min(cv, mx), mi);        // limit in

            if (oldvalue != curvalue)
                OnValueChanged(new EventArgs());

            Invalidate();
        }

        private void T_Tick(object sender, EventArgs e)
        {
            if ( trackto != curvalue)
            {
                if (trackto < curvalue)
                    trackto = Math.Min(curvalue, trackto + TrackSpeed);
                else
                    trackto = Math.Max(curvalue, trackto - TrackSpeed);
                Invalidate();
            }

            if (highlightsweep < BarMaximumPercent)
            {
                highlightsweep += 5;
                if (highlightsweep < BarMaximumPercentNoUpdate)
                    Invalidate();
            }
            else
            {
                highlightsweep = 0;
                Invalidate();
            }
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[EVENT_VALUECHANGED];
            if (handler != null) handler(this, e);
        }

        public bool Theme(Theme t, Font fnt)
        {
            BorderColor = t.TextBlockBorderColor;
            this.BackColor = Color.Transparent;
            return false;
        }

        #endregion

        private int max = 100;
        private int limit = 90;
        private int min = 0;
        private int curvalue = 0;
        private int trackto = 0;
        private int highlightsweep = 0;     // 0-100
        private int[] markers = new int[2] { -1, -1 };

        private static readonly object EVENT_VALUECHANGED = new object();

        Color barcolor = Color.Green;
        Color barcolorhighlight = Color.FromArgb(0, 255, 0);
        Color limitcolor = Color.FromArgb(128,0,0);
        Color limithighlightcolor = Color.FromArgb(255,0,0);
        Color bordercolor = Color.Black;
        Color limitlinecolor = Color.Cyan;
        Color markerlinecolor = Color.Cyan;

        Timer wintimer = new Timer() { Interval = 50 };
    }
}
