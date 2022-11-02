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
    // use to safely execute in MONO

    public class ExtSafeChart : Panel
    {
        public override Color BackColor { get { return chart?.BackColor ?? base.BackColor; }
                            set { if (chart != null) chart.BackColor = value; else base.BackColor = value; } }

        public bool Active { get { return chart != null; } }

        public ExtSafeChart()
        {
            try
            {
                chart = new ExtChart();
                chart.Dock = DockStyle.Fill;
                this.Controls.Add(chart);
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

        public void AddTitle(string name, Docking dck = Docking.Top, Color? titlecolor = null, Font f = null, Color? backcolor = null, ContentAlignment? alignment = null)
        {
            chart?.AddTitle(name, dck, titlecolor, f, backcolor, alignment);
        }

        public void SetAllTitleColorFont(Color titlecolor, Font f, Color? backcolor = null)
        {
            chart?.SetAllTitleColorFont(titlecolor, f, backcolor);
        }


        public void SetBorder(int width, ChartDashStyle style, Color? b = null)
        {
            chart?.SetBorder(width, style,b);
        }

        //////////////////////////////////////////////////////////////////////////// Legend

        // add legend to chart. Each legend seems to represent the series in order
        public void AddLegend(Color? textcolor, Color? backcolor = null, Font f = null, string name = "default")
        {
            chart?.AddLegend(textcolor, backcolor, f, name);
        }

        //////////////////////////////////////////////////////////////////////////// Chart Area
        ///
        // make a chart area
        public void AddChartArea(string name)
        {
            chart?.AddChartArea(name);
        }
        public void SetCurrentChartArea(int i)
        {
            chart?.SetCurrentChartArea(i);
        }
        public void SetChartAreaColors(Color? back = null, Color? bordercolor = null, int width = 2, ChartDashStyle style = ChartDashStyle.Solid)
        {
            chart?.SetChartAreaColors(back, bordercolor, width, style);
        }
        public int ChartAreaCount { get { return chart?.ChartAreas.Count ?? 0; } }

        //////////////////////////////////////////////////////////////////////////// X

        // configure LastChartArea X axis Interval
        public void SetXAxisInterval(int interval, IntervalAutoMode im = IntervalAutoMode.FixedCount)
        {
            chart?.SetXAxisInterval(interval, im);
        }

        // label formatting on chart axis
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

        // configure LastChartArea X axis Major grid
        public void SetXAxisMajorGrid(int width, ChartDashStyle style, Color? major = null )
        {
            chart?.SetXAxisMajorGrid(width, style, major);
        }
        // configure LastChartArea X axis Minor grid
        public void SetXAxisMinorGrid(int width, ChartDashStyle style, Color? minor = null)
        {
            chart?.SetXAxisMinorGrid(width, style, minor);
        }

        // Cursor
        public void XCursor(bool enabled = true)
        {
            chart?.XCursor(enabled);
        }
        public void XCursorSelection(bool userallowed = true, bool autoscroll = true)
        {
            chart?.XCursorSelection(userallowed, autoscroll);
        }
        public void SetXCursorColors(Color lc, Color sc, int lw = 2)
        {
            chart?.SetXCursorColors(lc, sc, lw);
        }
        public void SetXCursorScrollBarColors(Color scrollbarback, Color scrollbarbutton)
        {
            chart?.SetXCursorScrollBarColors(scrollbarback, scrollbarbutton);
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

        // configure LastChartArea Y axis Interval
        public void SetYAxisInterval(int interval, IntervalAutoMode im = IntervalAutoMode.FixedCount)
        {
            chart?.SetYAxisInterval(interval, im);
        }

        // label formatting on chart axis
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

        // configure LastChartArea Y axis Major grid
        public void SetYAxisMajorGrid(int width, ChartDashStyle style, Color? major = null)
        {
            chart?.SetYAxisMajorGrid(width, style, major);
        }
        // configure LastChartArea Y axis Minor grid
        public void SetYAxisMinorGrid(int width, ChartDashStyle style, Color? minor = null)
        {
            chart?.SetYAxisMinorGrid(width, style, minor);
        }

        // cursor
        public void YCursor(bool enabled = true)
        {
            chart?.YCursor(enabled);
        }
        public void YCursorSelection(bool userallowed = true, bool autoscroll = true)
        {
            chart?.YCursorSelection(userallowed, autoscroll);
        }
        public void SetYCursorColors(Color lc, Color sc, int lw = 2)
        {
            chart?.SetYCursorColors(lc, sc, lw);
        }
        public void SetYCursorScrollBarColors(Color scrollbarback, Color scrollbarbutton)
        {
            chart?.SetYCursorScrollBarColors(scrollbarback, scrollbarbutton);
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

        public void AddSeries(string name, string chartarea, SeriesChartType type, Color? seriescolor = null)
        {
            chart?.AddSeries(name, chartarea, type, seriescolor);
        }

        public int SeriesCount { get { return chart?.Series.Count ?? 0; } }

        public void SetSeriesColor(Color seriescolor)
        {
            chart?.SetSeriesColor(seriescolor);
        }

        public void SetCurrentSeries(int i)
        {
            chart?.SetCurrentSeries(i);
        }

        public void ClearSeriesPoints(int i = 0)
        {
            chart?.ClearSeriesPoints(i);
        }

        public void ShowSeriesMarkers(MarkerStyle style)
        {
            chart?.ShowSeriesMarkers(style);
        }
            
        public void SetSeriesMarkersColorSize(Color color, int markersize = 1, Color? markerbordercolor = null, int markerborderwidth = 2)
        {
            chart?.SetSeriesMarkersColorSize(color, markersize, markerbordercolor, markerborderwidth);
        }

        // configure LastSeries data labels
        public void ShowSeriesDataLabels()
        {
            chart?.ShowSeriesDataLabels();
        }
        public void SetSeriesDataLabelsColor(Color? back = null, Font fnt = null, Color? defaultlabelcolor = null)     // fore colour set by data point
        {
            chart?.SetSeriesDataLabels(back, fnt, defaultlabelcolor);
        }

        // configure LastSeries labels borders
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

        // Add point to LastSeries
        public void AddPoint(DataPoint d, string label = null)
        {
            chart?.AddPoint(d,label);
        }

        // Add xy point to LastSeries
        // for dates, the chart uses the day count as the X enumerator.
        public void AddXY(object x, object y, string label = null)
        {
            chart?.AddXY(x, y, label);
        }

        public void SetCurrentPoint(int i)
        {
            chart?.SetCurrentPoint(i);
        }

        public void SetPointLabel(string label)
        {
            chart?.SetPointLabel(label);
        }

        // Set label properties of LastDataPoint
        public void SetPointDataLabelCustomColorBorder(Color fore, Color back, Color borderfore, int width, ChartDashStyle style)
        {
            chart?.SetPointLabelCustomColorBorder(fore, back, borderfore, width, style);
        }

        // Set marker properties of LastDataPoint
        public void SetPointMarkerStyle(MarkerStyle style, Color color, int markersize = 1, Color? markerbordercolor = null, int markerborderwidth = 2)
        {
            chart?.SetPointMarkerStyle(style, color, markersize, markerbordercolor, markerborderwidth);
        }

        /// Context menu

        public void AddContextMenu(string[] text, Action<ToolStripMenuItem>[] actions, Action<ToolStripMenuItem[]> opening = null)
        {
            chart?.AddContextMenu(text, actions, opening);
        }

        // Wheelo
        public void EnableZoomMouseWheelX(bool on = true)
        {
            chart?.EnableZoomMouseWheelX(on);
        }

        public double ZoomMouseWheelXMinimumPercent { get { return chart?.ZoomMouseWheelXMinimumPercent ?? 0; } set { if (chart != null) chart.ZoomMouseWheelXMinimumPercent = value; } }

        private ExtChart chart;
    }
}
