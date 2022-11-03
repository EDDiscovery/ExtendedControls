﻿/*
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

        // add a title. Minimum is text. 
        public void AddTitle(string text, Docking dck = Docking.Top, Color? titlecolor = null, Font font = null, 
                                Color? backcolor = null, ContentAlignment? alignment = null)
        {
            if (titlecolor != null && font != null)
            {
                Titles.Add(new Title(text, dck, font, titlecolor.Value));        // Can add multiple
            }
            else
                Titles.Add(new Title(text, dck));

            var t = Titles[Titles.Count - 1];

            if (backcolor.HasValue)
                t.BackColor = backcolor.Value;
            if (alignment.HasValue)
                t.Alignment = alignment.Value;
        }

        // set all titles to this colour, font and backcolor (transparent if not set)
        public void SetAllTitleColorFont(Color titlecolor, Font font, Color? backcolor = null)
        {
            foreach (var x in Titles)
            {
                x.Font = font;
                x.ForeColor = titlecolor;
                x.BackColor = backcolor ?? Color.Transparent;
            }
        }

        // Set chart border
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

        // set all legends to this colour, font and backcolor (transparent if not set)
        public void SetAllLegendsColorFont(Color legendtextcolor, Font font, Color? backcolor = null)
        {
            foreach (var x in Legends)
            {
                x.Font = font;
                x.ForeColor = legendtextcolor;
                x.BackColor = backcolor ?? Color.Transparent;
            }
        }


        //////////////////////////////////////////////////////////////////////////// Chart Area

        // make a chart area, needs a unique name
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

        //////////////////////////////////////////////////////////////////////////// X for CurrentChartArea

        // configure CurrentChartArea X axis type, its interval, its variable/fixed mode - affects where labels are placed on x axis
        public void SetXAxisInterval(DateTimeIntervalType type, int interval, IntervalAutoMode im = IntervalAutoMode.FixedCount,
                                      double offset = 0, DateTimeIntervalType offsettype = DateTimeIntervalType.Auto)
        {
            CurrentChartArea.AxisX.IntervalType = type;
            CurrentChartArea.AxisX.Interval = interval;
            CurrentChartArea.AxisX.IntervalAutoMode = im;
            CurrentChartArea.AxisX.IntervalOffset = offset;
            CurrentChartArea.AxisX.IntervalOffsetType = offsettype;
        }

        // Color and Font on the X axis labels
        public void SetXAxisLabelColorFont(Color color, Font font = null)
        {
            CurrentChartArea.AxisX.LabelStyle.ForeColor = color;
            if (font != null)
                CurrentChartArea.AxisX.LabelStyle.Font = font;
        }

        // axis format sets the way the axis labels are printed
        public void SetXAxisFormat(string format)
        {
            CurrentChartArea.AxisX.LabelStyle.Format = format;
        }

        // configure CurrentChartArea Major grid intervals
        public void SetXAxisMajorGridInterval(double interval, DateTimeIntervalType type = DateTimeIntervalType.Auto, double offsetinterval = 0, DateTimeIntervalType offsetintervaltype = DateTimeIntervalType.Auto)
        {
            CurrentChartArea.AxisX.MajorGrid.Interval = interval;
            CurrentChartArea.AxisX.MajorGrid.IntervalType = type;
            CurrentChartArea.AxisX.MajorGrid.IntervalOffset = offsetinterval;
            CurrentChartArea.AxisX.MajorGrid.IntervalOffsetType = offsetintervaltype;
        }

        // configure CurrentChartArea Minor grid intervals
        public void SetXAxisMinorGridInterval(double interval, DateTimeIntervalType type = DateTimeIntervalType.Auto, double offsetinterval = 0, DateTimeIntervalType offsetintervaltype = DateTimeIntervalType.Auto)
        {
            CurrentChartArea.AxisX.MinorGrid.Interval = interval;
            CurrentChartArea.AxisX.MinorGrid.IntervalType = type;
            CurrentChartArea.AxisX.MinorGrid.IntervalOffset = offsetinterval;
            CurrentChartArea.AxisX.MinorGrid.IntervalOffsetType = offsetintervaltype;
        }

        // configure CurrentChartArea X axis Major grid colors
        public void SetXAxisMajorGridWidthColor(int width, ChartDashStyle style, Color? major)
        {
            if (major.HasValue)
                CurrentChartArea.AxisX.MajorGrid.LineColor = major.Value;
            CurrentChartArea.AxisX.MajorGrid.LineWidth = width;
            CurrentChartArea.AxisX.MajorGrid.LineDashStyle = style;
        }
        // configure CurrentChartArea X axis Minor grid colors
        public void SetXAxisMinorGridWidthColor(int width, ChartDashStyle style, Color? minor)
        {
            if ( minor.HasValue)
                CurrentChartArea.AxisX.MinorGrid.LineColor = minor.Value;
            CurrentChartArea.AxisX.MinorGrid.LineWidth = width;
            CurrentChartArea.AxisX.MinorGrid.LineDashStyle = style;
            CurrentChartArea.AxisX.MinorGrid.Enabled = true;
        }

        // Enable/disable X cursor
        public void XCursorShown(bool enabled = true)
        {
            CurrentChartArea.CursorX.IsUserEnabled = enabled;
        }
        // Enable/disable X selection box and autoscrolling beyond area
        public void XCursorSelection(bool userallowed = true, bool autoscroll = true)
        {
            CurrentChartArea.CursorX.IsUserSelectionEnabled = userallowed;
            CurrentChartArea.CursorX.AutoScroll = autoscroll;
        }
        // Set selection colours, cursor line width and colour
        public void SetXCursorColors(Color lc, Color sc, int lw = 2)
        {
            CurrentChartArea.CursorX.SelectionColor = sc;
            CurrentChartArea.CursorX.LineColor = lc;
            CurrentChartArea.CursorX.LineWidth = lw;
        }

        // Cursor selection interval - resolution, type. Offset allows you only to select a specific point (6/Days for saturday only)
        public void SetXCursorInterval(double interval, DateTimeIntervalType intervaltype, double intervaloffset = 0, DateTimeIntervalType intervaloffsettype = DateTimeIntervalType.Auto)
        {
            CurrentChartArea.CursorX.Interval = interval;
            CurrentChartArea.CursorX.IntervalType = intervaltype;
            CurrentChartArea.CursorX.IntervalOffset = intervaloffset;
            CurrentChartArea.CursorX.IntervalOffsetType = intervaloffsettype;
        }

        // set cursor scroll bar colours
        public void SetXCursorScrollBarColors(Color scrollbarback, Color scrollbarbutton)
        {
            CurrentChartArea.AxisX.ScrollBar.BackColor = scrollbarback;
            CurrentChartArea.AxisX.ScrollBar.ButtonColor = scrollbarbutton;
        }

        // set the cursor position to d, and make sure cursor line is in view of the graph.
        public void SetXCursorPosition(double d)
        {
            if (d < CurrentChartArea.AxisX.ScaleView.ViewMinimum || d > CurrentChartArea.AxisX.ScaleView.ViewMaximum)
            {
                double viewsize = CurrentChartArea.AxisX.ScaleView.ViewMaximum - CurrentChartArea.AxisX.ScaleView.ViewMinimum;
                ZoomTo(CurrentChartArea.AxisX, d, viewsize);            // make sure d is within viewsize..
                AutoZoomY(CurrentChartArea);
            }
            CurrentChartArea.CursorX.Position = d;
        }
        public void SetXCursorPosition(DateTime d)
        {
            SetXCursorPosition(d.ToOADate());
        }

        // Zoom out 1 step
        public void ZoomOutX()
        {
            CurrentChartArea.AxisX.ScaleView.ZoomReset();
            AutoZoomY(CurrentChartArea);
        }

        // Zoom Reset
        public void ZoomResetX()
        {
            CurrentChartArea.AxisX.ScaleView.ZoomReset(0);
            AutoZoomY(CurrentChartArea);
        }

        // Zoom to..
        public void ZoomX(double min, double max)
        {
            CurrentChartArea.AxisX.ScaleView.Zoom(min, max);
            AutoZoomY(CurrentChartArea);
        }
        public void ZoomX(DateTime from, DateTime to)
        {
            CurrentChartArea.AxisX.ScaleView.Zoom(from.ToOADate(),to.ToOADate());
            AutoZoomY(CurrentChartArea);
        }

        public bool IsZoomedX { get { return CurrentChartArea.AxisX.ScaleView.IsZoomed; } }

        //////////////////////////////////////////////////////////////////////////// Y

        public void SetYAxisInterval(DateTimeIntervalType type, int interval, IntervalAutoMode im = IntervalAutoMode.FixedCount,
                                      double offset = 0, DateTimeIntervalType offsettype = DateTimeIntervalType.Auto)
        {
            CurrentChartArea.AxisY.IntervalType = type;
            CurrentChartArea.AxisY.Interval = interval;
            CurrentChartArea.AxisY.IntervalAutoMode = im;
            CurrentChartArea.AxisY.IntervalOffset = offset;
            CurrentChartArea.AxisY.IntervalOffsetType = offsettype;
        }

        public void SetYAxisLabelColorFont(Color color, Font font = null)
        {
            CurrentChartArea.AxisY.LabelStyle.ForeColor = color;
            if ( font != null)
                CurrentChartArea.AxisY.LabelStyle.Font = font;
        }

        public void SetYAxisFormat(string format)
        {
            CurrentChartArea.AxisY.LabelStyle.Format = format;
        }

        public void SetYAxisMajorGridInterval(double interval, DateTimeIntervalType type = DateTimeIntervalType.Auto, double offsetinterval = 0, DateTimeIntervalType offsetintervaltype = DateTimeIntervalType.Auto)
        {
            CurrentChartArea.AxisY.MajorGrid.Interval = interval;
            CurrentChartArea.AxisY.MajorGrid.IntervalType = type;
            CurrentChartArea.AxisY.MajorGrid.IntervalOffset = offsetinterval;
            CurrentChartArea.AxisY.MajorGrid.IntervalOffsetType = offsetintervaltype;
        }

        public void SetYAxisMinorGridInterval(double interval, DateTimeIntervalType type = DateTimeIntervalType.Auto, double offsetinterval = 0, DateTimeIntervalType offsetintervaltype = DateTimeIntervalType.Auto)
        {
            CurrentChartArea.AxisY.MinorGrid.Interval = interval;
            CurrentChartArea.AxisY.MinorGrid.IntervalType = type;
            CurrentChartArea.AxisY.MinorGrid.IntervalOffset = offsetinterval;
            CurrentChartArea.AxisY.MinorGrid.IntervalOffsetType = offsetintervaltype;
        }

        public void SetYAxisMajorGridWidthColor(int width, ChartDashStyle style, Color? major = null)
        {
            if ( major.HasValue)
                CurrentChartArea.AxisY.MajorGrid.LineColor = major.Value;
            CurrentChartArea.AxisY.MajorGrid.LineWidth = width;
            CurrentChartArea.AxisY.MajorGrid.LineDashStyle = style;
        }
        public void SetYAxisMinorGridWidthColor(int width, ChartDashStyle style, Color? minor = null)
        {
            if ( minor != null)
                CurrentChartArea.AxisY.MinorGrid.LineColor = minor.Value;
            CurrentChartArea.AxisY.MinorGrid.LineWidth = width;
            CurrentChartArea.AxisY.MinorGrid.LineDashStyle = style;
            CurrentChartArea.AxisY.MinorGrid.Enabled = true;
        }

        public void YCursorShown(bool enabled = true)
        {
            CurrentChartArea.CursorY.IsUserEnabled = enabled;
        }

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
        public void SetYCursorInterval(double interval, DateTimeIntervalType intervaltype, double intervaloffset = 0, DateTimeIntervalType intervaloffsettype = DateTimeIntervalType.Auto)
        {
            CurrentChartArea.CursorY.Interval = interval;
            CurrentChartArea.CursorY.IntervalType = intervaltype;
            CurrentChartArea.CursorY.IntervalOffset = intervaloffset;
            CurrentChartArea.CursorY.IntervalOffsetType = intervaloffsettype;
        }

        public void SetYCursorScrollBarColors(Color scrollbarback, Color scrollbarbutton)
        {
            CurrentChartArea.AxisY.ScrollBar.BackColor = scrollbarback;
            CurrentChartArea.AxisY.ScrollBar.ButtonColor = scrollbarbutton;
        }
        public void SetYCursorPosition(double d)
        {
            if (d < CurrentChartArea.AxisY.ScaleView.ViewMinimum || d > CurrentChartArea.AxisY.ScaleView.ViewMaximum)
            {
                ZoomResetY();       // could do better, could pick x to cover y co-ords, but for now, reset if out of range
            }
            CurrentChartArea.CursorY.Position = d;
        }
        public void SetYCursorPosition(DateTime d)
        {
            SetYCursorPosition(d.ToOADate());
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

        // Turn on/off Y Auto scale, which adjusted Y scale when X is zoomed in
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
        public double AutoScaleYAddedPercent { get; set; } = 5;         // added value to zoom to give spacing, in decimal percent

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

        // colour used in series for both its contents and the labels
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

        // show series markers on data points
        public Series ShowSeriesMarkers(MarkerStyle style)
        {
            CurrentSeries.MarkerStyle = style;
            return CurrentSeries;
        }

        // set series markers colours and properties
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

        // set series data labels colours
        public Series SetSeriesDataLabelsColorFont(Color labelcolor, Font fnt = null, Color? back = null)     // fore colour set by data point
        {
            CurrentSeries.LabelForeColor = labelcolor;
            CurrentSeries.LabelBackColor = back ?? Color.Transparent;
            if (fnt != null)
                CurrentSeries.Font = fnt;
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

        // set series data type, auto, date, int etc
        public Series SetSeriesXAxisLabelType(ChartValueType chartvaluetypex)
        {
            CurrentSeries.XValueType = chartvaluetypex;
            return CurrentSeries;
        }

        // set series data type, auto, date, int etc
        public Series SetSeriesYAxisLabelType(ChartValueType chartvaluetypey)
        {
            CurrentSeries.YValueType = chartvaluetypey;
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

        public double ZoomMouseWheelXMinimumInterval { get; set; } = 5;
        public double ZoomMouseWheelXZoomFactor { get; set; } = 1.5;

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
                    double graphsize = ch.AxisY.Maximum - ch.AxisY.Minimum;
                    double min = Math.Max(minmax.Item1 - graphsize * AutoScaleYAddedPercent / 100.0, ch.AxisY.Minimum);
                    double max = Math.Min(minmax.Item2 + graphsize * AutoScaleYAddedPercent / 100.0, ch.AxisY.Maximum);
                    //System.Diagnostics.Debug.WriteLine($"X Zoomed Min max {min} - {max}");
                    ch.AxisY.ScaleView.Zoom(min,max);
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

            // saw an exception in HitTest which made no sense, so lets just cover it up and see if it occurs via debug
            try
            { 
                var hitres = HitTest(e.X, e.Y);
                //System.Diagnostics.Debug.WriteLine($"Hit test {hitres.ChartElementType} ca {hitres.ChartArea?.Name} ax {hitres.Axis?.Name} pi {hitres.PointIndex} se {hitres.Series?.Name} so {hitres.SubObject}");

                bool grapharea = hitres.ChartElementType == ChartElementType.PlottingArea || hitres.ChartElementType == ChartElementType.Gridlines ||
                                hitres.ChartElementType == ChartElementType.DataPoint;

                ChartArea ch = hitres.ChartArea;

                // we have it enabled, and in graph area, or on x axis labels
                if (mousewheelx.Contains(ch) && (grapharea || (hitres.ChartElementType == ChartElementType.AxisLabels && hitres.Axis == ch.AxisX)))
                {
                    Axis ax = ch.AxisX;
                    var shift = (Control.ModifierKeys & Keys.Shift) != 0;
                    double size = ax.ScaleView.ViewMaximum - ax.ScaleView.ViewMinimum;
                    double xpos = shift ? ax.ScaleView.ViewMinimum + size / 2 : ax.PixelPositionToValue(e.Location.X);
                    //System.Diagnostics.Debug.WriteLine($"Zoom {ax.ScaleView.ViewMinimum} {ax.ScaleView.ViewMaximum} = {size} gs {ax.Maximum - ax.Minimum}");

                    if (e.Delta > 0)
                    {
                        size /= ZoomMouseWheelXZoomFactor;
                        if (size < ZoomMouseWheelXMinimumInterval)       // not we limit the zoom in
                            size = ZoomMouseWheelXMinimumInterval;

                        // although you can order something, you may get something bigger due to chart
                        //System.Diagnostics.Debug.WriteLine($".. ordered {xpos - size / 2} {xpos + size / 2} = {size}");
                        ax.ScaleView.Zoom(xpos - size / 2, xpos + size / 2);
                        //System.Diagnostics.Debug.WriteLine($".. got {ax.ScaleView.ViewMaximum-ax.ScaleView.ViewMinimum}");
                        AutoZoomY(ch);      // need to give the autozoom a chance to operate as well
                    }
                    else
                    {
                        if (ax.ScaleView.IsZoomed)
                        {
                            size *= ZoomMouseWheelXZoomFactor;
                            ZoomTo(ax, xpos, size);
                            AutoZoomY(ch);
                        }
                    }
                }
            }
            catch( Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"********Exception in chart mouse wheel {ex}");
            }
        }

        private void ZoomTo(Axis ax, double pos, double size)
        {
            double graphsize = ax.Maximum - ax.Minimum;
            if (size >= graphsize)                         // if the size has grown beyond, we reset x zoom
                ax.ScaleView.ZoomReset(0);
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

            //System.Diagnostics.Debug.WriteLine($"MinMax {min}-{max}");

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
