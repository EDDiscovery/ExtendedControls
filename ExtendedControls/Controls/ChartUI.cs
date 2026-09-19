/*
 * Copyright 2022-2025 EDDiscovery development team
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
using System.Windows.Forms.DataVisualization.Charting;

namespace ExtendedControls
{
    public partial class ExtChart
    {
        #region UI

        //////////////////////////////////////////////////////////////////////// Wheel

        public void EnableZoomMouseWheelX(bool on = true)
        {
            if (on)
                mousewheelx.Add(CurrentChartArea);
            else
                mousewheelx.Remove(CurrentChartArea);
        }

        //////////////////////////////////////////////////////////////////////// Context menu definition
        public void AddContextMenu(string[] text, Action<ToolStripMenuItem>[] actions, Action<ToolStripMenuItem[]> opening = null)
        {
            System.Diagnostics.Debug.Assert(text.Length == actions.Length);

            var ct = new ContextMenuStrip();
            var tms = new ToolStripMenuItem[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                tms[i] = new ToolStripMenuItem() { Name = text[i], Text = text[i], Tag = i };
                tms[i].Click += (s, e) => { actions[(int)(((ToolStripMenuItem)s).Tag)]?.Invoke(s as ToolStripMenuItem); };
                ct.Items.Add(tms[i]);
            }

            if (opening != null)
            {
                ct.Opening += (s, e) => { opening.Invoke(tms); };
            }

            ContextMenuStrip = ct;
        }

        //////////////////////////////////////////////////////////////////////// Click Objects

        // PointF is % in chart.
        public void ReportOnMouseDown(Action<HitTestResult, PointF, MouseEventArgs> action)
        {
            MouseDown += (s, e) =>
            {
                try
                {
                    var hittest = HitTest(e.Location.X, e.Location.Y);
                    var percentage = new PointF(e.Location.X * 100F / (float)this.Width, e.Location.Y * 100F / (float)this.Height);
                    action.Invoke(hittest, percentage, e);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"********Exception in chart mouse down {ex}");
                }
            };
        }


        // Is this click, given in pointf's in chart, inside the legend, and if so, what % pos is it
        public PointF? ReportLegendClickPosition(PointF fp, int legend = -1)
        {
            Legend l = legend == -1 ? CurrentLegend : this.Legends[legend];
            if (fp.X >= l.Position.X && fp.X <= l.Position.Right && fp.Y >= l.Position.Y && fp.Y <= l.Position.Bottom)
                return new PointF((fp.X - l.Position.X) * 100F / l.Position.Width, (fp.Y - l.Position.Y) * 100F / l.Position.Height);
            else
                return null;
        }


        public bool Theme(Theme t, Font fnt)
        {
            Font = fnt;        // log the font with the chart, so you can use it directly in further explicit themeing
            BackColor = t.Form;

            // so the themer only overrides border/back colours if the user has set them to a value already. It does not override empty entries 
            // the user can chose if titles/legends border and back is themed

            SetAllTitlesColorFont(t.GridCellText, t.GetScaledFont(1.5f), t.GridCellBack,
                                      Color.Empty, 1,
                                      t.GroupBorder, 1, ChartDashStyle.Solid);

            SetAllLegendsColorFont(t.GridCellText, fnt, BackColor, 6, Color.FromArgb(128, 0, 0, 0),
                                        t.GridCellText, t.GridCellBack, fnt, StringAlignment.Center, LegendSeparatorStyle.Line, t.GridBorderLines,
                                        t.GridBorderLines, ChartDashStyle.Solid, 0,
                                        LegendSeparatorStyle.Line, t.GroupBorder, 1);

            // we theme all chart areas, backwards, so chartarea0 is the one left selected            
            for (int i = ChartAreas.Count - 1; i >= 0; i--)
            {
                // System.Diagnostics.Debug.WriteLine($"Themer {Parent.Name} Theme Chart Area {i}");
                SetCurrentChartArea(i);
                SetChartAreaColors(t.GridCellBack, t.GridBorderLines);

                SetXAxisMajorGridWidthColor(1, ChartDashStyle.Solid, t.GridBorderLines);
                SetYAxisMajorGridWidthColor(1, ChartDashStyle.Solid, t.GridBorderLines);
                SetXAxisLabelColorFont(t.GridCellText, fnt);
                SetYAxisLabelColorFont(t.GridCellText, fnt);
                SetXAxisTitle(CurrentChartArea.AxisX.Title, fnt, t.GridCellText);
                SetYAxisTitle(CurrentChartArea.AxisY.Title, fnt, t.GridCellText);

                SetXCursorColors(t.GridScrollArrowBack, t.GridCellText, 2);
                SetYCursorColors(t.GridScrollArrowBack, t.GridCellText, 2);

                SetXCursorScrollBarColors(t.GridSliderBack, t.GridScrollButtonBack);
                SetYCursorScrollBarColors(t.GridSliderBack, t.GridScrollButtonBack);
            }

            for (int i = Series.Count - 1; i >= 0; i--)        // backwards so chart 0 is left the pick
            {
                // System.Diagnostics.Debug.WriteLine($"Themer {Parent.Name} Theme series {i}");
                SetCurrentSeries(i);
                SetSeriesColor(t.GetChartColor(i));
                SetSeriesDataLabelsColorFont(t.GridCellText, fnt, Color.Transparent);
                SetSeriesMarkersColorSize(t.GridScrollArrowBack, 4, t.GridScrollButtonBack, 2);
            }

            return false;

        }

        #endregion

        #region ///////////////////////////////////////////////////////////// Private

        private void ExtChart_AxisViewChanged(object senderunused, ViewEventArgs e)       // user only interaction calls this
        {
            if (e.Axis == e.ChartArea.AxisX)             // if axis is x, we give the autozoom y a chance
            {
                double size = e.ChartArea.AxisX.ScaleView.ViewMaximum - e.ChartArea.AxisX.ScaleView.ViewMinimum;
                //System.Diagnostics.Debug.WriteLine($"Chart User Zoom to {size:F2}");
                AutoZoomY(e.ChartArea);                 // only scale if zoomed
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (this.ChartAreas.Count>0)
            {
                ChartArea ch = this.ChartAreas[0];      // this is the default, in case the hittest excepts
                //System.Diagnostics.Debug.WriteLine($"\r\nChart WHEEL current {ch.AxisX.Minimum:F2}..{ch.AxisX.Maximum:F2} | Cur {ch.AxisX.ScaleView.ViewMinimum:F2}..{ch.AxisX.ScaleView.ViewMaximum:F2} Sz {ch.AxisX.ScaleView.ViewMaximum- ch.AxisX.ScaleView.ViewMinimum} | {ch.AxisY.ScaleView.ViewMinimum:F2}..{ch.AxisY.ScaleView.ViewMaximum:F2} Sz {ch.AxisY.ScaleView.ViewMaximum- ch.AxisY.ScaleView.ViewMinimum:F2}");

                try
                {
                    // saw an exception in HitTest which made no sense, so lets just cover it up and see if it occurs via debug
                    // Sept 18 2026 tried again, no rhyme or reason why it excepts, so we presume chart 0 now if it does except
                    // set min limits and it still excepted in some circumstances even though the limits were good

                    var hitres = HitTest(e.X, e.Y);
                    //System.Diagnostics.Debug.WriteLine($"Chart WHEEL Hit test {hitres.ChartElementType} ca {hitres.ChartArea?.Name} ax {hitres.Axis?.Name} pi {hitres.PointIndex} se {hitres.Series?.Name} so {hitres.SubObject}");
                    bool grapharea = hitres.ChartElementType == ChartElementType.PlottingArea || hitres.ChartElementType == ChartElementType.Gridlines ||
                                hitres.ChartElementType == ChartElementType.DataPoint;

                    if (!(grapharea || (hitres.ChartElementType == ChartElementType.AxisLabels && hitres.Axis == ch.AxisX)))
                        return;
                }
                catch (Exception ex)
                {
                   //System.Diagnostics.Debug.WriteLine($"********Exception in chart mouse wheel {ex}");
                }

                // we have it enabled, and in graph area, or on x axis labels
                if (mousewheelx.Contains(ch) )
                {
                    Axis ax = ch.AxisX;
                    var shift = (Control.ModifierKeys & Keys.Shift) != 0;
                    double size = ax.ScaleView.ViewMaximum - ax.ScaleView.ViewMinimum;
                    double xpos = shift ? ax.ScaleView.ViewMinimum + size / 2 : ax.PixelPositionToValue(e.Location.X);
                    
                    //System.Diagnostics.Debug.WriteLine($"Chart Zoom {ax.ScaleView.ViewMinimum:F2} {ax.ScaleView.ViewMaximum:F2} Size {size:F2} gs {ax.Maximum - ax.Minimum:F2}");

                    if (e.Delta > 0)
                    {
                        size /= ZoomMouseWheelXZoomFactor;

                        if (!double.IsNaN(ax.ScaleView.MinSize))
                            size = Math.Max(ax.ScaleView.MinSize, size);

                        // although you can order something, you may get something bigger due to chart
                        //System.Diagnostics.Debug.WriteLine($"Chart Zoom in ordered {xpos - size / 2:F2} {xpos + size / 2:F2} = {size:F2}");
                        ax.ScaleView.Zoom(xpos - size / 2, xpos + size / 2);
                       // System.Diagnostics.Debug.WriteLine($"Chart Zoom got {ax.ScaleView.ViewMaximum - ax.ScaleView.ViewMinimum:F2}");
                        AutoZoomY(ch);      // need to give the autozoom a chance to operate as well
                    }
                    else
                    {
                        if (ax.ScaleView.IsZoomed)
                        {
                            size *= ZoomMouseWheelXZoomFactor;
                            //System.Diagnostics.Debug.WriteLine($"Chart Zoom Out to {size:F2}");
                            ZoomTo(ax, xpos, size);
                            AutoZoomY(ch);
                        }
                    }
                }
            }
        }

        private void ZoomTo(Axis ax, double pos, double size)
        {
            if (!double.IsNaN(ax.ScaleView.MinSize))
                size = Math.Max(ax.ScaleView.MinSize, size);

            double graphsize = ax.Maximum - ax.Minimum;

            if (size >= graphsize)                         // if the size has grown beyond, we reset x zoom
            {
                ax.ScaleView.ZoomReset(0);
            }
            else
            {
                if (pos + size / 2 > ax.Maximum)                   // make sure we don't zoom off the left/right of the max/min
                    ax.ScaleView.Zoom(ax.Maximum - size, ax.Maximum);
                else if (pos - size / 2 < ax.Minimum)
                    ax.ScaleView.Zoom(ax.Minimum, ax.Minimum + size);
                else
                    ax.ScaleView.Zoom(pos - size / 2, pos + size / 2);
            }
        }

        private void AutoZoomY(ChartArea ch)
        {
            if (autozoomy.Contains(ch))     // if autozoom Y is enabled on this chart
            {
                if (ch.AxisX.ScaleView.IsZoomed)        // if x is zoomed or we force it, we adjust y to min/max
                {
                    var minmax = ch.MinMaxY(Series);
                    if (minmax.Item1 != double.MaxValue)       // we must have some data points to zoom into, this means non were
                    {
                        //System.Diagnostics.Debug.WriteLine($"Chart Ordered Y Limits");
                        SetYLimits(ch, minmax);
                    }
                    else
                    {
                        //System.Diagnostics.Debug.WriteLine($"Chart AutoY Size no points in view {ch.AxisX.ScaleView.ViewMinimum:F2}..{ch.AxisX.ScaleView.ViewMaximum:F2} | {ch.AxisY.ScaleView.ViewMinimum:F2}..{ch.AxisY.ScaleView.ViewMaximum:F2}");
                    }
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine($"X not zoomed, Y reset");    
                    ch.AxisY.ScaleView.ZoomReset(0);        // x is not zoomed, reset y back to default
                }
            }
        }

        private void SetYLimits(ChartArea ch, Tuple<double, double> minmax)
        {
            var delta = minmax.Item2 - minmax.Item1;
            if (delta == 0)                         // Single point in view
                delta = Math.Max(1, Math.Abs(minmax.Item1) * AutoScaleYAddedPercent / 100.0);       // make a litle delta up
            var margin = delta * (AutoScaleYAddedPercent / 100.0);      // from the difference, add a little bit
            if ( !double.IsNaN(ch.AxisY.ScaleView.MinSize))
                margin = Math.Max(ch.AxisY.ScaleView.MinSize / 2, margin);
            double min = Math.Max(minmax.Item1 - margin, ch.AxisY.Minimum);
            double max = Math.Min(minmax.Item2 + margin, ch.AxisY.Maximum);
            //System.Diagnostics.Debug.WriteLine($"Chart Y Limits max {minmax} at % {AutoScaleYAddedPercent} delta {minmax.Item2 - minmax.Item1} margin {margin} giving {min} - {max}");
            ch.AxisY.ScaleView.Zoom(min, max);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            boundssizedat = Rectangle.Empty;            // if we changed the font, cause a resize event
        }

        protected override void OnPrePaint(ChartPaintEventArgs e)
        {
            SizeTitleFonts();
            base.OnPrePaint(e);
        }

        private Rectangle boundssizedat;
        private void SizeTitleFonts()
        {
            if (Bounds != boundssizedat)        // if changed layout size, we recalc the title sizes placed manually
            {
                boundssizedat = Bounds;
                foreach (var t in Titles)
                {
                    if (!t.Position.Auto)
                    {
                        Rectangle area = GetArea(t.Position);
                        //System.Diagnostics.Debug.WriteLine($"Title pos {t.Position} = {area} chart area {Bounds}");
                        t.Font = DrawingHelpersStaticFunc.GetFontToFit(t.Text, t.Font, new Size(area.Width - 12, area.Height - 4));     //12 pixels for borders etc and spacing etc.
                    }
                }
            }
        }


        private HashSet<ChartArea> autozoomy = new HashSet<ChartArea>();
        private HashSet<ChartArea> mousewheelx = new HashSet<ChartArea>();

        #endregion

    }

    static class ChartExtensions
    {
        // in the current chart area, for each series in that chartarea, and all y points, find max/min
        static public Tuple<double, double> MinMaxY(this ChartArea chart, SeriesCollection chartseries)
        {
            return chart.MinMaxY(chartseries, chart.AxisX.ScaleView.ViewMinimum, chart.AxisX.ScaleView.ViewMaximum);
        }


        // return the min and max of Y within these X series values. across all Y points
        static public Tuple<double, double> MinMaxY(this ChartArea chart, SeriesCollection chartseries, double startx, double endx)
        {
            double ymin = double.MaxValue;
            double ymax = double.MinValue;

            //System.Diagnostics.Debug.WriteLine($"MinMaxYInChartArea X {min}-{max}");

            foreach (var series in chartseries)
            {
                if (series.ChartArea == chart.Name)     // if the series is in the chart
                {
                    foreach (DataPoint dp in series.Points)
                    {
                        if (dp.XValue >= startx && dp.XValue <= endx)       // within X range
                        {
                            foreach (var y in dp.YValues)
                            {
                               // System.Diagnostics.Debug.WriteLine($"{startx}..{endx} dp {dp.XValue} Y {y}");
                                ymin = Math.Min(y, ymin);
                                ymax = Math.Max(y, ymax);
                            }
                        }
                        else
                        {
                            //  System.Diagnostics.Debug.WriteLine($"..dp reject {dp.XValue}");
                        }
                    }
                }
            }

            //System.Diagnostics.Debug.WriteLine($"...MinMaxYInChartArea Y {ymin}-{ymax}");
            return new Tuple<double, double>(ymin, ymax);
        }
    }
}




