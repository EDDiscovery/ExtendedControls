/*
 * Copyright © 2022-2022 EDDiscovery development team
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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExtendedControls
{
    // Does not work in MONO
    public class ExtChart : Chart
    {
        public ChartArea CurrentChartArea { set; get; }
        public Series CurrentSeries { set; get; }
        public DataPoint CurrentDataPoint { get; set; }

        public ExtChart() : base()
        {
        }

        public void AddTitle(string name, Docking dck = Docking.Top, Color? titlecolor = null, Font f = null, Color? backcolor = null, ContentAlignment? alignment = null)
        {
            if (titlecolor != null && f != null)
            {
                Titles.Add(new Title(name, dck, f, titlecolor.Value));        // Can add multiple
            }
            else
                Titles.Add(new Title(name, dck));

            var t = Titles[Titles.Count - 1];

            if (backcolor.HasValue)
                t.BackColor = backcolor.Value;
            if (alignment.HasValue)
                t.Alignment = alignment.Value;
        }

        public void SetAllTitleColorFont(Color titlecolor, Font f, Color? backcolor = null)
        {
            foreach (var x in Titles)
            {
                x.Font = f;
                x.ForeColor = titlecolor;
                if (backcolor.HasValue)
                    x.BackColor = backcolor.Value;
            }
        }

        public void SetBorder(int width, ChartDashStyle style, Color? borderlinecolor = null)
        {
            BorderlineDashStyle = style;
            BorderlineWidth = width;
            if ( borderlinecolor != null)
                BorderlineColor = borderlinecolor.Value;
        }

        //////////////////////////////////////////////////////////////////////////// Legend

        // add legend to chart. Each legend seems to represent the series in order. Optional themeing
        public Legend AddLegend(Color? textcolor, Color? backcolor = null, Font f = null, string name = "default")
        {
            var legend = Legends.Add(name);
            if ( textcolor != null)
                legend.ForeColor = textcolor.Value;
            if ( f!= null)
                legend.Font = f;
            if (backcolor != null)
                legend.BackColor = backcolor.Value;
            return legend;
        }

        //////////////////////////////////////////////////////////////////////////// Chart Area

        // make a chart area
        public ChartArea AddChartArea(string name)
        {
            ChartArea ca = new ChartArea();
            ca.Name = name;
            ChartAreas.Add(ca);
            CurrentChartArea = ca;
            return ca;
        }
        public void SetCurrentChartArea(int i)
        {
            CurrentChartArea = this.ChartAreas[i];
        }
        public ChartArea SetChartAreaColors( Color? back = null,
                                    Color? bordercolor = null, int width = 2, ChartDashStyle style = ChartDashStyle.Solid)
        {
            if (back != null)
                CurrentChartArea.BackColor = back.Value;
            if (bordercolor != null)
                CurrentChartArea.BorderColor = bordercolor.Value;
            CurrentChartArea.BorderWidth = width;
            CurrentChartArea.BorderDashStyle = style;
            return CurrentChartArea;
        }

        //////////////////////////////////////////////////////////////////////////// X

        // configure CurrentChartArea X axis Interval
        public void SetXAxisInterval(int interval, IntervalAutoMode im = IntervalAutoMode.FixedCount)
        {
            CurrentChartArea.AxisX.Interval = interval;
            CurrentChartArea.AxisX.IntervalAutoMode = im;
        }

        // label formatting on chart axis
        public void SetXAxisLabelColorFont(Color? r = null, Font f = null)
        {
            if (r != null)
                CurrentChartArea.AxisX.LabelStyle.ForeColor = r.Value;
            if (f != null)
                CurrentChartArea.AxisX.LabelStyle.Font = f;
        }
        public void SetXAxisFormat(string format)
        {
            CurrentChartArea.AxisX.LabelStyle.Format = format;
        }

        // configure CurrentChartArea X axis Major grid
        public void SetXAxisMajorGrid(int width, ChartDashStyle style, Color? major)
        {
            if ( major.HasValue )
                CurrentChartArea.AxisX.MajorGrid.LineColor = major.Value;
            CurrentChartArea.AxisX.MajorGrid.LineWidth = width;
            CurrentChartArea.AxisX.MajorGrid.LineDashStyle = style;
        }
        // configure CurrentChartArea X axis Minor grid
        public void SetXAxisMinorGrid(int width, ChartDashStyle style, Color? minor)
        {
            if ( minor.HasValue)
                CurrentChartArea.AxisX.MinorGrid.LineColor = minor.Value;
            CurrentChartArea.AxisX.MinorGrid.LineWidth = width;
            CurrentChartArea.AxisX.MinorGrid.LineDashStyle = style;
            CurrentChartArea.AxisX.MinorGrid.Enabled = true;
        }

        // Enable/disable X cursor
        public void XCursor(bool enabled = true)
        {
            CurrentChartArea.CursorX.IsUserEnabled = enabled;
        }
        // Enable/disable X selection box and autoscrolling beyond area
        public void XCursorSelection(bool userallowed = true, bool autoscroll = true)
        {
            CurrentChartArea.CursorX.IsUserSelectionEnabled = userallowed;
            CurrentChartArea.CursorX.AutoScroll = autoscroll;
        }
        public void SetXCursorColors(Color lc, Color sc, int lw = 2)
        {
            CurrentChartArea.CursorX.SelectionColor = sc;
            CurrentChartArea.CursorX.LineColor = lc;
            CurrentChartArea.CursorX.LineWidth = lw;
        }
        public void SetXCursorScrollBarColors(Color scrollbarback, Color scrollbarbutton)
        {
            CurrentChartArea.AxisX.ScrollBar.BackColor = scrollbarback;
            CurrentChartArea.AxisX.ScrollBar.ButtonColor = scrollbarbutton;
        }
        public void ZoomOutX()
        {
            CurrentChartArea.AxisX.ScaleView.ZoomReset();
        }
        public void ZoomResetX()
        {
            CurrentChartArea.AxisX.ScaleView.ZoomReset(0);
        }

        public void ZoomX(double min, double max)
        {
            CurrentChartArea.AxisX.ScaleView.Zoom(min, max);
        }

        public bool IsZoomedX { get { return CurrentChartArea.AxisX.ScaleView.IsZoomed; } }

        //////////////////////////////////////////////////////////////////////////// Y

        // configure CurrentChartArea Y axis Interval
        public void SetYAxisInterval(int interval, IntervalAutoMode im = IntervalAutoMode.FixedCount)
        {
            CurrentChartArea.AxisY.Interval = interval;
            CurrentChartArea.AxisY.IntervalAutoMode = im;
        }

        // label formatting on chart axis
        public void SetYAxisLabelColorFont(Color? r = null, Font f = null)
        {
            if ( r.HasValue)
                CurrentChartArea.AxisY.LabelStyle.ForeColor = r.Value;
            if ( f != null)
                CurrentChartArea.AxisY.LabelStyle.Font = f;
        }
        public void SetYAxisFormat(string format)
        {
            CurrentChartArea.AxisY.LabelStyle.Format = format;
        }

        // configure CurrentChartArea Y axis Major grid
        public void SetYAxisMajorGrid(int width, ChartDashStyle style, Color? major = null)
        {
            if ( major.HasValue)
                CurrentChartArea.AxisY.MajorGrid.LineColor = major.Value;
            CurrentChartArea.AxisY.MajorGrid.LineWidth = width;
            CurrentChartArea.AxisY.MajorGrid.LineDashStyle = style;
        }
        // configure CurrentChartArea Y axis Minor grid
        public void SetYAxisMinorGrid(int width, ChartDashStyle style, Color? minor = null)
        {
            if ( minor != null)
                CurrentChartArea.AxisY.MinorGrid.LineColor = minor.Value;
            CurrentChartArea.AxisY.MinorGrid.LineWidth = width;
            CurrentChartArea.AxisY.MinorGrid.LineDashStyle = style;
            CurrentChartArea.AxisY.MinorGrid.Enabled = true;
        }

        // Enable/disable Y cursor
        public void YCursor(bool enabled = true)
        {
            CurrentChartArea.CursorY.IsUserEnabled = enabled;
        }
        // Enable/disable Y selection box and zooming into it
        public void YCursorSelection(bool userallowed = true, bool autoscroll = true)
        {
            CurrentChartArea.CursorY.IsUserSelectionEnabled = userallowed;
            CurrentChartArea.CursorY.AutoScroll = autoscroll;
        }
        public void SetYCursorColors(Color lc, Color sc, int lw = 2)
        {
            CurrentChartArea.CursorY.SelectionColor = sc;
            CurrentChartArea.CursorY.LineColor = lc;
            CurrentChartArea.CursorY.LineWidth = lw;
        }
        public void SetYCursorScrollBarColors(Color scrollbarback, Color scrollbarbutton)
        {
            CurrentChartArea.AxisY.ScrollBar.BackColor = scrollbarback;
            CurrentChartArea.AxisY.ScrollBar.ButtonColor = scrollbarbutton;
        }

        public void ZoomOutY()
        {
            CurrentChartArea.AxisY.ScaleView.ZoomReset();
        }

        public void ZoomResetY()
        {
            CurrentChartArea.AxisY.ScaleView.ZoomReset(0);
        }
        public void ZoomY(double min, double max)
        {
            CurrentChartArea.AxisY.ScaleView.Zoom(min, max);
        }

        public bool IsZoomedY { get { return CurrentChartArea.AxisY.ScaleView.IsZoomed; } }

        // applies to current chart area
        public void YAutoScale(bool on = true, bool enableyscrollbar = true)      
        {
            if (on)
            {
                if (autozoomy.Count == 0)       // only one handler for all chartareas, so we need a class to keep track if its on
                    AxisViewChanged += ExtChart_AxisViewChanged;

                autozoomy.Add(CurrentChartArea);

                if (enableyscrollbar)
                    CurrentChartArea.AxisY.ScrollBar.Enabled = enableyscrollbar;
            }
            else
            {
                autozoomy.Remove(CurrentChartArea);
                if ( autozoomy.Count == 0)
                    AxisViewChanged -= ExtChart_AxisViewChanged;

                CurrentChartArea.AxisY.ScrollBar.Enabled = enableyscrollbar;
            }
        }


        //////////////////////////////////////////////////////////////////////////// Series

        public Series AddSeries(string name, string chartarea, SeriesChartType type, Color? seriescolor = null )
        {
            Series series = new Series();
            series.Name = name;
            series.ChartArea = chartarea;
            if ( seriescolor.HasValue)
                series.Color = seriescolor.Value;
            series.ChartType = type;
            CurrentSeries = series;
            Series.Add(series);
            return series;
        }

        public void SetSeriesColor(Color seriescolor)
        {
            CurrentSeries.Color = seriescolor;
        }

        public void SetCurrentSeries(int i)
        {
            CurrentSeries = Series[i];
        }

        public void ClearSeriesPoints(int i)
        {
            Series[i].Points.Clear();
        }

        public Series ShowSeriesMarkers(MarkerStyle style)
        {
            CurrentSeries.MarkerStyle = style;
            return CurrentSeries;
        }

        public Series SetSeriesMarkersColorSize(Color color, int markersize = 1, Color? markerbordercolor = null, int markerborderwidth = 2)
        {
            CurrentSeries.MarkerColor = color;
            CurrentSeries.MarkerSize = markersize;
            if (markerbordercolor.HasValue)
            {
                CurrentSeries.MarkerBorderColor = markerbordercolor.Value;
                CurrentSeries.MarkerBorderWidth = markerborderwidth;
            }
            return CurrentSeries;
        }

        // configure CurrentSeries data labels
        public Series ShowSeriesDataLabels()
        {
            CurrentSeries.IsValueShownAsLabel = true;
            return CurrentSeries;
        }

        public Series SetSeriesDataLabels(Color? back = null, Font fnt = null, Color? defaultlabelcolor = null)     // fore colour set by data point
        {
            CurrentSeries.LabelBackColor = back ?? Color.Transparent;
            if (fnt != null)
                CurrentSeries.Font = fnt;
            if ( defaultlabelcolor != null )
                CurrentSeries.LabelForeColor = defaultlabelcolor.Value;
            return CurrentSeries;
        }

        // configure CurrentSeries labels borders
        public Series SetSeriesDataLabelsBorder(Color fore, int width, ChartDashStyle style)
        {
            CurrentSeries.LabelBorderColor = fore;
            CurrentSeries.LabelBorderDashStyle = style;
            CurrentSeries.LabelBorderWidth = width;
            return CurrentSeries;
        }

        public Series SetSeriesXAxisLabelType(ChartValueType chartvaluetypex)
        {
            CurrentSeries.XValueType = chartvaluetypex;
            return CurrentSeries;
        }

        public Series SetSeriesYAxisLabelType(ChartValueType chartvaluetypey)
        {
            CurrentSeries.XValueType = chartvaluetypey;
            return CurrentSeries;
        }

        //////////////////////////////////////////////////////////////////////////// Points

        // Add point to CurrentSeries
        public DataPoint AddPoint(DataPoint d, string label = null)
        {
            CurrentSeries.Points.Add(d);
            CurrentDataPoint = d;
            if (label != null)
                CurrentDataPoint.AxisLabel = label;
            return d;
        }

        // Add xy point to CurrentSeries
        // for dates, the chart uses the day count as the X enumerator.
        public DataPoint AddXY(object x, object y, string label = null)
        {
            CurrentSeries.Points.AddXY(x, y);
            CurrentDataPoint = CurrentSeries.Points[CurrentSeries.Points.Count - 1];
            if (label != null)
                CurrentDataPoint.AxisLabel = label;
            return CurrentDataPoint;
        }

        public void SetCurrentPoint(int i)
        {
            CurrentDataPoint = CurrentSeries.Points[i];
        }

        public void SetPointLabel(string label)
        {
            CurrentDataPoint.AxisLabel = label;
        }

        // Set label properties of CurrentDataPoint
        public void SetPointLabelCustomColorBorder(Color fore, Color back, Color borderfore, int width, ChartDashStyle style)
        {
            SetPointLabelCustomColorBorder(CurrentDataPoint, fore, back, borderfore, width, style);
        }

        // Set marker properties of CurrentDataPoint
        public void SetPointMarkerStyle(MarkerStyle style, Color color, int markersize = 1, Color? markerbordercolor = null, int markerborderwidth = 2)
        {
            SetPointMarkerStyle(CurrentDataPoint, style, color, markersize, markerbordercolor, markerborderwidth);
        }

        // markers

        // Set label properties of a point
        static public void SetPointLabelCustomColorBorder(DataPoint dp, Color fore, Color back, Color borderfore, int width, ChartDashStyle style)
        {
            dp.LabelForeColor = fore;
            dp.LabelBackColor = back;
            dp.LabelBorderColor = borderfore;
            dp.LabelBorderWidth = width;
            dp.LabelBorderDashStyle = style;
        }

        // Set marker style of a point
        static public void SetPointMarkerStyle(DataPoint dp, MarkerStyle style, Color color, int markersize = 1, Color? markerbordercolor = null, int markerborderwidth = 2)
        {
            dp.MarkerStyle = style;
            dp.MarkerColor = color;
            dp.MarkerSize = markersize;
            if (markerbordercolor.HasValue)
            {
                dp.MarkerBorderColor = markerbordercolor.Value;
                dp.MarkerBorderWidth = markerborderwidth;
            }
        }

        //////////////////////////////////////////////////////////////////////// Wheel

        public void EnableZoomMouseWheelX(bool on = true)
        {
            if (on)
                mousewheelx.Add(CurrentChartArea);
            else
                mousewheelx.Remove(CurrentChartArea);
        }

        public double ZoomMouseWheelXMinimumPercent { get; set; } = 5;


        #region ///////////////////////////////////////////////////////////// Private

        private void ExtChart_AxisViewChanged(object senderunused, ViewEventArgs e)       // user only interaction calls this
        {
            if (e.Axis == e.ChartArea.AxisX)             // if axis is x, we give the autozoom y a chance
                AutoZoomY(e.ChartArea);
        }

        private void AutoZoomY(ChartArea ch)
        {
            if (autozoomy.Contains(ch))     // if enabled on this chart
            {
                if (ch.AxisX.ScaleView.IsZoomed)        // if x is zoomed, we adjust y to min/max
                {
                    var minmax = ch.MinMaxYInChartArea(Series);
                    //System.Diagnostics.Debug.WriteLine($"X Zoomed Min max {minmax.Item1} - {minmax.Item2}");
                    ch.AxisY.ScaleView.Zoom(minmax.Item1, minmax.Item2);
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine($"X not zoomed, Y reset");    
                    ch.AxisY.ScaleView.ZoomReset(0);        // x is not zoomed, reset y back to default
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            var hitres = HitTest(e.X, e.Y);
            //System.Diagnostics.Debug.WriteLine($"Hit test {hitres.ChartElementType} ca {hitres.ChartArea?.Name} ax {hitres.Axis?.Name} pi {hitres.PointIndex} se {hitres.Series?.Name} so {hitres.SubObject}");

            bool grapharea = hitres.ChartElementType == ChartElementType.PlottingArea || hitres.ChartElementType == ChartElementType.Gridlines ||
                            hitres.ChartElementType == ChartElementType.DataPoint;

            ChartArea ch = hitres.ChartArea;

            // we have it enabled, and in graph area, or on x axis labels
            if (mousewheelx.Contains(ch) && ( grapharea || (hitres.ChartElementType == ChartElementType.AxisLabels && hitres.Axis == ch.AxisX)))
            {
                Axis ax = ch.AxisX;
                double xpos = ax.PixelPositionToValue(e.Location.X);
                double size = ax.ScaleView.ViewMaximum - ax.ScaleView.ViewMinimum;
                double graphsize = ax.Maximum - ax.Minimum;
                //System.Diagnostics.Debug.WriteLine($"Zoom {ax.ScaleView.ViewMinimum} {ax.ScaleView.ViewMaximum} = {ax.ScaleView.ViewMaximum - ax.ScaleView.ViewMinimum} gs {graphsize}");

                if (e.Delta > 0)
                {
                    size /= 1.2;
                    if (size > graphsize * ZoomMouseWheelXMinimumPercent / 100.0)       // not we limit the zoom in
                    {
                        ax.ScaleView.Zoom(xpos - size / 2, xpos + size / 2);
                        AutoZoomY(ch);      // need to give the autozoom a chance to operate as well
                    }
                }
                else
                {
                    if (ax.ScaleView.IsZoomed)
                    {
                        size *= 1.2;
                        if (size >= graphsize)                         // if the size has grown beyond, we reset x zoom
                            ax.ScaleView.ZoomReset(0);
                        else
                        {
                            if (xpos + size / 2 > ax.Maximum)                   // make sure we don't zoom off the left/right of the max/min
                                ax.ScaleView.Zoom(ax.Maximum - size, ax.Maximum);
                            else if (xpos - size / 2 < ax.Minimum)
                                ax.ScaleView.Zoom(ax.Minimum, ax.Minimum + size);
                            else
                                ax.ScaleView.Zoom(xpos - size / 2, xpos + size / 2);
                        }

                        AutoZoomY(ch);
                    }
                }
            }
        }

        //////////////////////////////////////////////////////////////////////// Context menu definition
        public void AddContextMenu(string[] text, Action<ToolStripMenuItem>[] actions, Action<ToolStripMenuItem[]> opening = null)
        {
            System.Diagnostics.Debug.Assert(text.Length == actions.Length);

            var ct = new ContextMenuStrip();
            var tms = new ToolStripMenuItem[text.Length];
            for( int i = 0; i < text.Length; i++)
            {
                tms[i] = new ToolStripMenuItem() { Name = text[i], Text = text[i], Tag = i };
                tms[i].Click += (s, e) => { actions[(int)(((ToolStripMenuItem)s).Tag)]?.Invoke(s as ToolStripMenuItem); };
                ct.Items.Add(tms[i]);
            }

            if (opening!=null)
            {
                ct.Opening += (s, e) => { opening.Invoke(tms); };
            }

            ContextMenuStrip = ct;
        }

        private HashSet<ChartArea> autozoomy = new HashSet<ChartArea>();
        private HashSet<ChartArea> mousewheelx = new HashSet<ChartArea>();

        #endregion
    }

    static class ChartExtensions
    {
        // in the current chart area, for each series in that chartarea, and all y points, find max/min
        static public Tuple<double, double> MinMaxYInChartArea(this ChartArea chart, SeriesCollection chartseries)
        {
            double min = chart.AxisX.ScaleView.ViewMinimum;
            double max = chart.AxisX.ScaleView.ViewMaximum;

            double ymin = double.MaxValue;
            double ymax = double.MinValue;

            System.Diagnostics.Debug.WriteLine($"MinMax {min}-{max}");

            foreach (var series in chartseries)
            {
                if (series.ChartArea == chart.Name)
                {
                    foreach (DataPoint dp in series.Points)
                    {
                        if (dp.XValue >= min && dp.XValue <= max)
                        {
                            foreach (var y in dp.YValues)
                            {
                                ymin = Math.Min(dp.YValues[0], ymin);
                                ymax = Math.Max(dp.YValues[0], ymax);
                            }
                        }
                    }
                }
            }

            return new Tuple<double, double>(ymin, ymax);
        }


    }

}
