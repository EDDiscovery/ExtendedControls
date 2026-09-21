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
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ExtProgressBarMultiSegment : Control, IThemeable
    {
        // Back used as background of whole control. Fore is not used
        public Color[] SegmentColors { get; set; }
        public double[] SegmentValues { get; set; }
        public void ChangeValue(int i, int value) { SegmentValues[i] = value; Invalidate(); }
        public double Maximum { get { return max; } set { max = value; Invalidate(); } }
        public double Limit { get { return limit; } set { limit = value; Invalidate();} }
        public Color LimitLineColor { get { return limitlinecolor; } set { limitlinecolor = value; Invalidate(); } }
        public Color BorderColor { get { return bordercolor; } set { bordercolor = value; Invalidate(); } }
        public Color BarBackColor { get { return barbackcolor; } set { barbackcolor = value; Invalidate(); } }
        public Color HighlightColor1 { get { return highlightwashcolor1; } set { highlightwashcolor1 = value; Invalidate(); } }
        public Color HighlightColor2 { get { return highlightwashcolor2; } set { highlightwashcolor2 = value; Invalidate(); } }
        public double Marker1 { get { return markers[0]; } set { markers[0] = value; Invalidate(); } }
        public double Marker2 { get { return markers[1]; } set { markers[1] = value; Invalidate(); } }
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

        public ExtProgressBarMultiSegment() : base()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                                ControlStyles.AllPaintingInWmPaint |        // "Free" double-buffering (1/3).
                                ControlStyles.OptimizedDoubleBuffer,
                                true );
            wintimer.Tick += T_Tick;
            wintimer.Start();
        }

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

            int heightreserved = (int)(BarHeightReserve * area.Height / 100.0);

            Rectangle bararea = new Rectangle(area.X + BarWidthMargin, area.Y + heightreserved, area.Width - BarWidthMargin*2, area.Height - heightreserved*2);

            // if we have no displayedsegmentvalues, its the first time, lets build to it
            if ( displayedsegmentvalues == null && SegmentValues != null)
                displayedsegmentvalues = new double[SegmentValues.Length];

            if ( bararea.Width>0 ) // ensure not minimised away
            { 
                using (Brush br = new SolidBrush(barbackcolor))     // fill whole bar with back colour
                {
                    e.Graphics.FillRectangle(br, bararea);
                }

                // if we have segments and enough colours we can draw
                if (displayedsegmentvalues?.Length > 0 && displayedsegmentvalues.Length <= SegmentColors?.Length )    
                {
                    double valueperpixed = (double)bararea.Width / Maximum;     // each count of value is worth this number of pixels

                    // number of pixels in bar, 1 added for rounding errors
                    int hbarwidth = (int)(displayedsegmentvalues.Sum() * valueperpixed + 1);

                    using (Bitmap dbmp = new Bitmap(hbarwidth, bararea.Height))
                    {
                        using (Graphics g = Graphics.FromImage(dbmp))
                        {
                            //g.Clear(Color.Black);

                            int xpos = 0;
                            for (int i = 0; i < SegmentValues.Length; i++)
                            {
                                if (displayedsegmentvalues[i] > 0)     // if it has a value to show..
                                {
                                    // For this segment, we pick what is to show. We may run out of currentshowntotal, in which case this will stop it
                                    // convert to pixels at a fixed pixel/value, always show at least 1 pixel
                                    int segmentpixels = Math.Max(1,(int)(valueperpixed * displayedsegmentvalues[i] + 0.5));        
                                    //System.Diagnostics.Debug.WriteLine($"Segment {i} tot {total} toshow {toshow} pixels {segmentpixels} from {xpos}");

                                    if (segmentpixels > 0)
                                    {
                                        var rbrush = new Rectangle(xpos - 1, 0, segmentpixels + 1, bararea.Height);
                                        var rdraw = new Rectangle(xpos, 0, segmentpixels, bararea.Height);

                                        //System.Diagnostics.Debug.WriteLine($"  draw {rdraw} using {rbrush}");
                                        using (Brush br1 = new LinearGradientBrush(rbrush, SegmentColors[i], SegmentColors[i], 0.0))
                                        {
                                            g.FillRectangle(br1, rdraw);
                                        }

                                        xpos += segmentpixels;
                                    }
                                    else
                                        break;
                                }
                            }

                            int highlightpos = (int)((double)hbarwidth * highlightsweeppercent / 100.0);

                            // right highlight area, from the highlight to normal
                            // best order to prevent artifacts, right first, then left
                            {
                                var rr = new Rectangle(bararea.X + highlightpos - 1, 0, hbarwidth - highlightpos + 1, bararea.Height);
                                if (rr.Width > 0)
                                {
                                    // System.Diagnostics.Debug.WriteLine($"Right {highlightsweeppercent} {highlightpos} {barwidthpixels} {rr}");
                                    using (Brush br = new LinearGradientBrush(rr, HighlightColor2, HighlightColor1, 0.0))
                                        g.FillRectangle(br, rr);
                                }
                            }
                            {
                                var rb = new Rectangle(bararea.X - 1, 0, highlightpos + 1, bararea.Height);
                                var rll = new Rectangle(bararea.X, 0, highlightpos, bararea.Height);
                                if (rb.Width > 0 && rll.Width > 0)
                                {
                                    //System.Diagnostics.Debug.WriteLine($" Left {highlightpos} {barwidthpixels} {rll}");
                                    using (Brush br = new LinearGradientBrush(rb, HighlightColor1, HighlightColor2, 0.0))
                                        g.FillRectangle(br, rll);
                                }
                            }
                        }

                        e.Graphics.DrawImage(dbmp, bararea.Location);
                    }

                    // if limit is set other than max, show it
                    if (limit >= 0 && limit < max)
                    {
                        int pos = bararea.X + (int)((limit / max) * bararea.Width);
                        // System.Diagnostics.Debug.WriteLine($"LimitMS {limit} max {max} {bararea.Width} {pos}");
                        using (Pen pc1 = new Pen(LimitLineColor, MarkerWidth))
                            e.Graphics.DrawLine(pc1, new Point(pos, area.Y), new Point(pos, area.Y + area.Height));     // 1 more pixel due to it not drawing last
                    }

                    double total = displayedsegmentvalues.Sum();
                    foreach (double marker in markers)
                    {
                        // only show if up to trackto
                        if (marker >= 0 && marker <= total)
                        {
                            int pos = bararea.X + (int)((marker / max) * bararea.Width);
                            using (Pen pc1 = new Pen(MarkerLineColor, MarkerWidth))
                                e.Graphics.DrawLine(pc1, new Point(pos, bararea.Y), new Point(pos, bararea.Y + bararea.Height + 1));     // 1 more pixel due to it not drawing last
                        }
                    }
                }
            }
        }

        private void T_Tick(object sender, EventArgs e)
        {
            if (SegmentValues != null)       // must protect for designer
            {
                if (displayedsegmentvalues == null )            // paranoia defense for tick before display
                    displayedsegmentvalues = new double[SegmentValues.Length];

                // we move across and move the displayedsegment values up to the selected values
                for (int i = 0; i < SegmentValues.Length; i++)
                {
                    if (!SegmentValues[i].ApproxEquals(displayedsegmentvalues[i]))
                    {
                        if (displayedsegmentvalues[i] < SegmentValues[i])
                        {
                            displayedsegmentvalues[i] = Math.Min(SegmentValues[i], displayedsegmentvalues[i] + TrackSpeed);
                        }
                        else
                        {
                            displayedsegmentvalues[i] = Math.Max(SegmentValues[i], displayedsegmentvalues[i] - TrackSpeed);
                        }

                        Invalidate();

                    }
                }
            }

            if (highlightsweeppercent < BarMaximumPercent)
            {
                highlightsweeppercent += 5;
                if (highlightsweeppercent < BarMaximumPercentNoUpdate)
                    Invalidate();
            }
            else
            {
                highlightsweeppercent = 0;
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

        double max = 110;
        private double limit = 100;
        private int highlightsweeppercent = 0;     // 0-100
        private double[] markers = new double[2] { -1, -1 };

        private double[] displayedsegmentvalues = null;
        private static readonly object EVENT_VALUECHANGED = new object();

        Color bordercolor = Color.Black;
        Color limitlinecolor = Color.Cyan;
        Color markerlinecolor = Color.Cyan;
        Color barbackcolor = Color.FromArgb(100, 200, 200, 200);

        Color highlightwashcolor1 = Color.FromArgb(0, 255, 255, 255);
        Color highlightwashcolor2 = Color.FromArgb(220, 255, 255, 255);

        Timer wintimer = new Timer() { Interval = 50 };
    }
}

