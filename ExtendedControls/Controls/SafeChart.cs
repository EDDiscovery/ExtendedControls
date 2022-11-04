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
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExtendedControls
{
    // use to safely execute in MONO. See ExtChart for help on functions
    // using the return structures (ChartArea etc) it may be null due to chart not being supported - beware

    public class ExtSafeChart : Panel
    {
        public override Color BackColor { get { return chart?.BackColor ?? base.BackColor; }
                            set { if (chart != null) chart.BackColor = value; else base.BackColor = value; } }

        public bool Active { get { return chart != null; } }

        public Action<ExtSafeChart, string, AxisName, double> CursorPositionChanged = null;

        public ExtSafeChart()
        {
            try
            { 
                chart = new ExtChart();
                chart.Dock = DockStyle.Fill;
                this.Controls.Add(chart);
                chart.CursorPositionChanged += (s, e) =>
                {
                    CursorPositionChanged?.Invoke(this, e.ChartArea.Name, e.Axis.AxisName, e.ChartArea.CursorX.Position);
                };
            }
            catch
            {
                Label l = new Label();
                l.AutoSize = true;
                l.Text = "This OS does not support charts";
                l.Location = new Point(10, 10);
                this.Controls.Add(l);
            }
        }

        public Title AddTitle(string name, Docking dck = Docking.Top, Color? titlecolor = null, Font f = null, Color? backcolor = null, ContentAlignment? alignment = null,
                                ElementPosition position = null)
        {
            return chart?.AddTitle(name, dck, titlecolor, f, backcolor, alignment,position) ?? null; 
        }

        public void SetAllTitleColorFont(Color titlecolor, Font font, Color? backcolor = null)
        {
            chart?.SetAllTitleColorFont(titlecolor, font, backcolor);
        }

        public void SetBorder(int width, ChartDashStyle style, Color? b = null)
        {
            chart?.SetBorder(width, style,b);
        }

        //////////////////////////////////////////////////////////////////////////// Legend

        public Legend AddLegend(string name, Color? textcolor, Color? backcolor = null, Font font = null, ElementPosition position = null)
        {
            return chart?.AddLegend(name, textcolor, backcolor, font, position) ?? null;
        }

        public void SetAllLegendsColorFont(Color legendtextcolor, Font font, Color? backcolor = null, int shadowoffset = 0, Color? shadowcolor = null)
        {
            chart?.SetAllLegendsColorFont(legendtextcolor, font, backcolor, shadowoffset, shadowcolor);
        }

        //////////////////////////////////////////////////////////////////////////// Chart Area

        public ChartArea AddChartArea(string name, ElementPosition position = null)
        {
            return chart?.AddChartArea(name,position) ?? null;
        }
        public ChartArea SetCurrentChartArea(int i)
        {
            return chart?.SetCurrentChartArea(i) ?? null;
        }
        public ChartArea SetCurrentChartArea(string name)
        {
            return chart?.SetCurrentChartArea(name) ?? null;
        }
        public int ChartAreaCount { get { return chart?.ChartAreas.Count ?? 0; } }

        public void SetChartAreaColors(Color? back = null, Color? bordercolor = null, int width = 2, ChartDashStyle style = ChartDashStyle.Solid)
        {
            chart?.SetChartAreaColors(back, bordercolor, width, style);
        }
        
        public void SetChartArea3DStyle(ChartArea3DStyle style)
        {
            chart?.SetChartArea3DStyle(style);
        }

        //////////////////////////////////////////////////////////////////////////// X

        public void SetXAxisInterval(DateTimeIntervalType type, int interval, IntervalAutoMode im = IntervalAutoMode.FixedCount,
                                       double offset = 0, DateTimeIntervalType offsettype = DateTimeIntervalType.Auto)
        {
            chart?.SetXAxisInterval(type, interval, im, offset, offsettype);
        }

        public void SetXAxisLabelColorFont(Color r, Font f = null)
        {
            chart?.SetXAxisLabelColorFont(r, f);
        }
        public void SetXAxisLabelDisabled()
        {
            chart?.SetXAxisLabelColorFont(Color.Empty);
        }

        public void SetXAxisFormat(string format)
        {
            chart?.SetXAxisFormat(format);
        }
        public void SetXAxisMajorGridInterval(double interval, DateTimeIntervalType type = DateTimeIntervalType.Auto, double offsetinterval = 0, DateTimeIntervalType offsetintervaltype = DateTimeIntervalType.Auto)
        {
            chart?.SetXAxisMajorGridInterval(interval, type, offsetinterval, offsetintervaltype);
        }
        public void SetXAxisMinorGridInterval(double interval, DateTimeIntervalType type = DateTimeIntervalType.Auto, double offsetinterval = 0, DateTimeIntervalType offsetintervaltype = DateTimeIntervalType.Auto)
        {
            chart?.SetXAxisMinorGridInterval(interval, type, offsetinterval, offsetintervaltype);
        }
        public void SetXAxisMajorGridWidthColor(int width, ChartDashStyle style, Color? major = null )
        {
            chart?.SetXAxisMajorGridWidthColor(width, style, major);
        }
        public void SetXAxisMinorGridWidthColor(int width, ChartDashStyle style, Color? minor = null)
        {
            chart?.SetXAxisMinorGridWidthColor(width, style, minor);
        }

        // Cursor
        public void XCursorShown(bool enabled = true)
        {
            chart?.XCursorShown(enabled);
        }
        public void XCursorSelection(bool userallowed = true, bool autoscroll = true)
        {
            chart?.XCursorSelection(userallowed, autoscroll);
        }
        public void SetXCursorInterval(double interval, DateTimeIntervalType intervaltype, double intervaloffset = 0, DateTimeIntervalType intervaloffsettype = DateTimeIntervalType.Auto)
        {
            chart?.SetXCursorInterval(interval, intervaltype, intervaloffset, intervaloffsettype);
        }
        public void SetXCursorColors(Color lc, Color sc, int lw = 2)
        {
            chart?.SetXCursorColors(lc, sc, lw);
        }

        public void SetXCursorScrollBarColors(Color scrollbarback, Color scrollbarbutton)
        {
            chart?.SetXCursorScrollBarColors(scrollbarback, scrollbarbutton);
        }

        public void SetXCursorPosition(double d)
        {
            chart?.SetXCursorPosition(d);
        }
        public void SetXCursorPosition(DateTime d)
        {
            chart?.SetXCursorPosition(d);
        }

        public void ZoomOutX()
        {
            chart?.ZoomOutX();
        }
        public void ZoomResetX()
        {
            chart?.ZoomResetX();
        }
        public void ZoomX(double min, double max)
        {
            chart?.ZoomX(min, max);
        }
        public bool IsZoomedX { get { return chart?.IsZoomedX ?? false; } }

        //////////////////////////////////////////////////////////////////////////// Y
        public void SetYAxisInterval(DateTimeIntervalType type, int interval, IntervalAutoMode im = IntervalAutoMode.FixedCount,
                                       double offset = 0, DateTimeIntervalType offsettype = DateTimeIntervalType.Auto)
        {
            chart?.SetYAxisInterval(type, interval, im, offset, offsettype);
        }
        public void SetYAxisLabelColorFont(Color r, Font f = null)
        {
            chart?.SetYAxisLabelColorFont(r, f);
        }
        public void SetYAxisLabelDisabled()
        {
            chart?.SetYAxisLabelColorFont(Color.Empty);
        }
        public void SetYAxisFormat(string format)
        {
            chart?.SetYAxisFormat(format);
        }

        public void SetYAxisMajorGridInterval(double interval, DateTimeIntervalType type = DateTimeIntervalType.Auto, double offsetinterval = 0, DateTimeIntervalType offsetintervaltype = DateTimeIntervalType.Auto)
        {
            chart?.SetYAxisMajorGridInterval(interval, type, offsetinterval, offsetintervaltype);
        }
        public void SetYAxisMinorGridInterval(double interval, DateTimeIntervalType type = DateTimeIntervalType.Auto, double offsetinterval = 0, DateTimeIntervalType offsetintervaltype = DateTimeIntervalType.Auto)
        {
            chart?.SetYAxisMinorGridInterval(interval, type, offsetinterval, offsetintervaltype);
        }

        public void SetYAxisMajorGridWidthColor(int width, ChartDashStyle style, Color? major = null)
        {
            chart?.SetYAxisMajorGridWidthColor(width, style, major);
        }
        public void SetYAxisMinorGridWidthColor(int width, ChartDashStyle style, Color? minor = null)
        {
            chart?.SetYAxisMinorGridWidthColor(width, style, minor);
        }

        public void YCursorShown(bool enabled = true)
        {
            chart?.YCursorShown(enabled);
        }
        public void YCursorSelection(bool userallowed = true, bool autoscroll = true)
        {
            chart?.YCursorSelection(userallowed, autoscroll);
        }
        public void SetYCursorInterval(double interval, DateTimeIntervalType intervaltype, double intervaloffset = 0, DateTimeIntervalType intervaloffsettype = DateTimeIntervalType.Auto)
        {
            chart?.SetYCursorInterval(interval, intervaltype, intervaloffset, intervaloffsettype);
        }
        public void SetYCursorColors(Color lc, Color sc, int lw = 2)
        {
            chart?.SetYCursorColors(lc, sc, lw);
        }
        public void SetYCursorScrollBarColors(Color scrollbarback, Color scrollbarbutton)
        {
            chart?.SetYCursorScrollBarColors(scrollbarback, scrollbarbutton);
        }
        public void SetYCursorPosition(double d)
        {
            chart?.SetYCursorPosition(d);
        }
        public void SetYCursorPosition(DateTime d)
        {
            chart?.SetYCursorPosition(d);
        }

        public void ZoomOutY()
        {
            chart?.ZoomOutY();
        }
        public void ZoomResetY()
        {
            chart?.ZoomResetY();
        }
        public void ZoomY(double min, double max)
        {
            chart?.ZoomY(min, max);
        }

        public bool IsZoomedY { get { return chart?.IsZoomedY ?? false; } }

        public void YAutoScale(bool on = true, bool enableyscrollbar = true)
        {
            chart?.YAutoScale(on, enableyscrollbar);
        }
        public double AutoScaleYAddedPercent { get { return chart?.AutoScaleYAddedPercent ?? 0; } set { if (chart != null) chart.AutoScaleYAddedPercent = value; } }

        //////////////////////////////////////////////////////////////////////////// Series

        public Series AddSeries(string name, string chartarea, SeriesChartType type, Color? seriescolor = null)
        {
            return chart?.AddSeries(name, chartarea, type, seriescolor) ?? null;
        }
        public Series SetCurrentSeries(int i)
        {
            return chart?.SetCurrentSeries(i) ?? null;
        }
        public Series SetCurrentSeries(string name)
        {
            return chart?.SetCurrentSeries(name) ?? null;
        }

        public int SeriesCount { get { return chart?.Series.Count ?? 0; } }

        public void SetSeriesShadowOffset(int offset)
        {
            chart?.SetSeriesShadowOffset(offset);
        }
        public void SetSeriesColor(Color seriescolor, Color? shadowcolor = null)
        {
            chart?.SetSeriesColor(seriescolor,shadowcolor);
        }

        public void ShowSeriesMarkers(MarkerStyle style)
        {
            chart?.ShowSeriesMarkers(style);
        }
            
        public void SetSeriesMarkersColorSize(Color color, int markersize = 1, Color? markerbordercolor = null, int markerborderwidth = 2)
        {
            chart?.SetSeriesMarkersColorSize(color, markersize, markerbordercolor, markerborderwidth);
        }

        public void ShowSeriesDataLabels()
        {
            chart?.ShowSeriesDataLabels();
        }
        public void SetSeriesDataLabelsColorFont(Color labelcolor, Font fnt = null, Color? back = null)
        {
            chart?.SetSeriesDataLabelsColorFont(labelcolor,fnt,back);
        }

        public void SetSeriesDataLabelsBorder(Color fore, int width, ChartDashStyle style)
        {
            chart?.SetSeriesDataLabelsBorder(fore, width, style);
        }

        public void SetSeriesXAxisLabelType(ChartValueType chartvaluetypex)
        {
            chart?.SetSeriesXAxisLabelType(chartvaluetypex);
        }

        public void SetSeriesYAxisLabelType(ChartValueType chartvaluetypey)
        {
            chart?.SetSeriesYAxisLabelType(chartvaluetypey);
        }

        //////////////////////////////////////////////////////////////////////////// Points

        public void ClearPoints()
        {
            chart?.ClearPoints();
        }
        public DataPoint AddPoint(DataPoint d, string label = null)
        {
            return chart?.AddPoint(d,label);
        }

        public DataPoint AddXY(object x, object y, string label = null)
        {
            return chart?.AddXY(x, y, label);
        }

        public void SetCurrentPoint(int i)
        {
            chart?.SetCurrentPoint(i);
        }

        public void SetPointLabel(string label)
        {
            chart?.SetPointLabel(label);
        }

        public void SetPointDataLabelCustomColorBorder(Color fore, Color back, Color borderfore, int width, ChartDashStyle style)
        {
            chart?.SetPointLabelCustomColorBorder(fore, back, borderfore, width, style);
        }

        public void SetPointMarkerStyle(MarkerStyle style, Color color, int markersize = 1, Color? markerbordercolor = null, int markerborderwidth = 2)
        {
            chart?.SetPointMarkerStyle(style, color, markersize, markerbordercolor, markerborderwidth);
        }

        public int FindIndexOfPoint(double target, AxisName axis = AxisName.X)
        {
            return chart?.FindIndexOfPoint(target, axis) ?? -1;
        }

        public int FindIndexOfNearestPoint(double target, AxisName axis = AxisName.X)
        {
            return chart?.FindIndexOfNearestPoint(target, axis) ?? -1;
        }

        public void AddContextMenu(string[] text, Action<ToolStripMenuItem>[] actions, Action<ToolStripMenuItem[]> opening = null)
        {
            chart?.AddContextMenu(text, actions, opening);
        }

        public void EnableZoomMouseWheelX(bool on = true)
        {
            chart?.EnableZoomMouseWheelX(on);
        }

        public double ZoomMouseWheelXMinimumInterval { get { return chart?.ZoomMouseWheelXMinimumInterval ?? 0; } set { if (chart != null) chart.ZoomMouseWheelXMinimumInterval = value; } }
        public double ZoomMouseWheelXZoomFactor { get { return chart?.ZoomMouseWheelXZoomFactor ?? 0; } set { if (chart != null) chart.ZoomMouseWheelXZoomFactor = value; } }

        private ExtChart chart;
    }
}
