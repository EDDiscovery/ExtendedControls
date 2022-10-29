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

using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExtendedControls
{
    // use to safely execute in MONO

    public class ExtSafeChart : Panel
    {
        private ExtChart chart;

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
                BackColor = Color.Red;
                Label l = new Label();
                l.AutoSize = true;
                l.Text = "Chart not enabled on this system";
                l.Location = new Point(10, 10);
                this.Controls.Add(l);
            }
        }

        public void AddTitle(string name, Color titlecolor, Font f = null)
        {
            chart?.AddTitle(name, titlecolor, f);
        }

        public void SetBorder(Color b, int width, ChartDashStyle style)
        {
            chart?.SetBorder(b, width, style);
        }

        // add legend to chart. Each legend seems to represent the series in order
        public void AddLegend(Color textcolor, Color? backcolor = null, Font f = null, string name = "default")
        {
            chart?.AddLegend(textcolor, backcolor, f, name);
        }

        // make a chart area
        public void AddChartArea(string name, Color back, Color b, int width, ChartDashStyle style)
        {
            chart?.AddChartArea(name, back, b, width, style);
        }

        // configure LastChartArea X axis Interval
        public void SetXAxisInterval(int interval, IntervalAutoMode im = IntervalAutoMode.FixedCount)
        {
            chart?.SetXAxisInterval(interval, im);
        }

        // label formatting on chart axis
        public void SetXAxisLabelColorFontFormat(Color r, Font f = null, string format = null)
        {
            chart?.SetXAxisLabelColorFontFormat(r, f, format);
        }

        // configure LastChartArea X axis Major grid
        public void SetXAxisMajorGrid(Color major, int width, ChartDashStyle style)
        {
            chart?.SetXAxisMajorGrid(major, width, style);
        }
        // configure LastChartArea X axis Minor grid
        public void SetXAxisMinorGrid(Color minor, int width, ChartDashStyle style)
        {
            chart?.SetXAxisMinorGrid(minor, width, style);
        }

        // configure LastChartArea Y axis Interval
        public void SetYAxisInterval(int interval, IntervalAutoMode im = IntervalAutoMode.FixedCount)
        {
            chart?.SetYAxisInterval(interval, im);
        }

        // label formatting on chart axis
        public void SetYAxisLabelColorFontFormat(Color r, Font f = null, string format = null)
        {
            chart?.SetYAxisLabelColorFontFormat(r, f, format);
        }

        // configure LastChartArea Y axis Major grid
        public void SetYAxisMajorGrid(Color major, int width, ChartDashStyle style)
        {
            chart?.SetYAxisMajorGrid(major, width, style);
        }
        // configure LastChartArea Y axis Minor grid
        public void SetYAxisMinorGrid(Color major, int width, ChartDashStyle style)
        {
            chart?.SetYAxisMinorGrid(major, width, style);
        }

        // configure LastChartArea X cursor
        public void EnableXCursor(Color lc, int lw, Color sc, Color scrollbarback, Color scrollbarbutton)
        {
            chart?.EnableXCursor(lc, lw, sc, scrollbarback, scrollbarbutton);
        }
        // configure LastChartArea Y cursor
        public void EnableYCursor(Color lc, int lw, Color sc, Color scrollbarback, Color scrollbarbutton)
        {
            chart?.EnableYCursor(lc, lw, sc, scrollbarback, scrollbarbutton);
        }

        public void AddSeries(string name, string chartarea, SeriesChartType type, Color seriescolor)
        {
            chart?.AddSeries(name, chartarea, type, seriescolor);
        }

        public void ShowSeriesMarkers(MarkerStyle style, Color color, int markersize = 1, Color? markerbordercolor = null, int markerborderwidth = 2)
        {
            chart?.ShowSeriesMarkers(style, color, markersize, markerbordercolor, markerborderwidth);
        }

        // configure LastSeries data labels
        public void ShowSeriesDataLabels(Color? back = null, Font fnt = null, Color? defaultlabelcolor = null)     // fore colour set by data point
        {
            chart?.ShowSeriesDataLabels(back, fnt, defaultlabelcolor);
        }

        // configure LastSeries labels borders
        public void ShowSeriesDataLabelsBorder(Color fore, int width, ChartDashStyle style)
        {
            chart?.ShowSeriesDataLabelsBorder(fore, width, style);
        }

        public void SetSeriesXAxisLabelType(ChartValueType chartvaluetypex)
        {
            chart?.SetSeriesXAxisLabelType(chartvaluetypex);
        }

        public void SetSeriesYAxisLabelType(ChartValueType chartvaluetypey)
        {
            chart?.SetSeriesYAxisLabelType(chartvaluetypey);
        }


        // Add point to LastSeries
        public void AddPoint(DataPoint d, Color? labelcolor = null)
        {
            chart?.AddPoint(d, labelcolor);
        }

        // Add xy point to LastSeries
        // for dates, the chart uses the day count as the X enumerator.
        public void AddXY(object x, object y, Color? labelcolor = null)
        {
            chart?.AddXY(x, y, labelcolor);
        }

        // Set label properties of LastDataPoint
        public void SetPointDataLabelCustomColorBorder(Color fore, Color back, Color borderfore, int width, ChartDashStyle style)
        {
            chart?.SetPointDataLabelCustomColorBorder(fore, back, borderfore, width, style);
        }

        // Set marker properties of LastDataPoint
        public void SetPointMarkerStyle(MarkerStyle style, Color color, int markersize = 1, Color? markerbordercolor = null, int markerborderwidth = 2)
        {
            chart?.SetPointMarkerStyle(style, color, markersize, markerbordercolor, markerborderwidth);
        }
    }
}
