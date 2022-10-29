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
using System.Windows.Forms.DataVisualization.Charting;

namespace ExtendedControls
{
    // Does not work in MONO

    public class ExtChart : Chart
    {
        public void AddTitle(string name, Color titlecolor, Font f = null)
        {
            Titles.Add(new Title(name, Docking.Top, f ?? Font, titlecolor));        // Can add multiple
        }

        public void SetBorder(Color b, int width, ChartDashStyle style)
        {
            BorderlineDashStyle = style;
            BorderlineWidth = width;
            BorderlineColor = b;
        }

        // add legend to chart. Each legend seems to represent the series in order
        public Legend AddLegend(Color textcolor, Color? backcolor = null, Font f = null, string name = "default")
        {
            var legend = Legends.Add(name);
            legend.ForeColor = textcolor;
            legend.Font = f ?? Font;
            legend.BackColor = backcolor ?? Color.Transparent;
            return legend;
        }

        // make a chart area
        public ChartArea AddChartArea(string name, Color back, Color b, int width, ChartDashStyle style)
        {
            ChartArea ca = new ChartArea();
            ca.Name = name;
            ca.BorderColor = b;
            ca.BorderDashStyle = style;
            ca.BorderWidth = width;
            ca.BackColor = back;
            ChartAreas.Add(ca);
            LastChartArea = ca;
            return ca;
        }

        // configure LastChartArea X axis Interval
        public void SetXAxisInterval(int interval, IntervalAutoMode im = IntervalAutoMode.FixedCount)
        {
            LastChartArea.AxisX.Interval = interval;
            LastChartArea.AxisX.IntervalAutoMode = im;
        }

        // label formatting on chart axis
        public void SetXAxisLabelColorFontFormat(Color r, Font f = null, string format = null)
        {
            LastChartArea.AxisX.LabelStyle.ForeColor = r;
            LastChartArea.AxisX.LabelStyle.Font = f ?? Font;
            if (format != null)
                LastChartArea.AxisX.LabelStyle.Format = format;
        }

        // configure LastChartArea X axis Major grid
        public void SetXAxisMajorGrid(Color major, int width, ChartDashStyle style)
        {
            LastChartArea.AxisX.MajorGrid.LineColor = major;
            LastChartArea.AxisX.MajorGrid.LineWidth = width;
            LastChartArea.AxisX.MajorGrid.LineDashStyle = style;
        }
        // configure LastChartArea X axis Minor grid
        public void SetXAxisMinorGrid(Color minor, int width, ChartDashStyle style)
        {
            LastChartArea.AxisX.MinorGrid.LineColor = minor;
            LastChartArea.AxisX.MinorGrid.LineWidth = width;
            LastChartArea.AxisX.MinorGrid.LineDashStyle = style;
            LastChartArea.AxisX.MinorGrid.Enabled = true;
        }

        // configure LastChartArea Y axis Interval
        public void SetYAxisInterval(int interval, IntervalAutoMode im = IntervalAutoMode.FixedCount)
        {
            LastChartArea.AxisY.Interval = interval;
            LastChartArea.AxisY.IntervalAutoMode = im;
        }

        // label formatting on chart axis
        public void SetYAxisLabelColorFontFormat(Color r, Font f = null, string format = null)
        {
            LastChartArea.AxisY.LabelStyle.ForeColor = r;
            LastChartArea.AxisY.LabelStyle.Font = f ?? Font;
            if (format != null)
                LastChartArea.AxisY.LabelStyle.Format = format;
        }

        // configure LastChartArea Y axis Major grid
        public void SetYAxisMajorGrid(Color major, int width, ChartDashStyle style)
        {
            LastChartArea.AxisY.MajorGrid.LineColor = major;
            LastChartArea.AxisY.MajorGrid.LineWidth = width;
            LastChartArea.AxisY.MajorGrid.LineDashStyle = style;
        }
        // configure LastChartArea Y axis Minor grid
        public void SetYAxisMinorGrid(Color minor, int width, ChartDashStyle style)
        {
            LastChartArea.AxisY.MinorGrid.LineColor = minor;
            LastChartArea.AxisY.MinorGrid.LineWidth = width;
            LastChartArea.AxisY.MinorGrid.LineDashStyle = style;
            LastChartArea.AxisY.MinorGrid.Enabled = true;
        }

        // configure LastChartArea X cursor
        public void EnableXCursor(Color lc, int lw, Color sc, Color scrollbarback, Color scrollbarbutton)
        {
            LastChartArea.CursorX.IsUserEnabled = true;
            LastChartArea.CursorX.IsUserSelectionEnabled = true;
            LastChartArea.CursorX.LineColor = lc;
            LastChartArea.CursorX.LineWidth = lw;
            LastChartArea.CursorX.SelectionColor = sc;
            LastChartArea.CursorX.AutoScroll = true;
            LastChartArea.AxisX.ScrollBar.BackColor = scrollbarback;
            LastChartArea.AxisX.ScrollBar.ButtonColor = scrollbarbutton;
        }
        // configure LastChartArea Y cursor
        public void EnableYCursor(Color lc, int lw, Color sc, Color scrollbarback, Color scrollbarbutton)
        {
            LastChartArea.CursorY.IsUserEnabled = true;
            LastChartArea.CursorY.IsUserSelectionEnabled = true;
            LastChartArea.CursorY.AutoScroll = true;
            LastChartArea.CursorY.LineColor = lc;
            LastChartArea.CursorY.LineWidth = lw;
            LastChartArea.CursorY.SelectionColor = sc;
            LastChartArea.AxisY.ScrollBar.BackColor = scrollbarback;
            LastChartArea.AxisY.ScrollBar.ButtonColor = scrollbarbutton;
        }


        public Series AddSeries(string name, string chartarea, SeriesChartType type, Color seriescolor)
        {
            Series series = new Series();
            series.Name = name;
            series.ChartArea = chartarea;
            series.Color = seriescolor;
            series.ChartType = type;
            series.MarkerColor = Color.Purple;
            series.MarkerStyle = MarkerStyle.Cross;
            LastSeries = series;
            Series.Add(series);
            return series;
        }

        public Series ShowSeriesMarkers(MarkerStyle style, Color color, int markersize = 1, Color? markerbordercolor = null, int markerborderwidth = 2)
        {
            LastSeries.MarkerStyle = style;
            LastSeries.MarkerColor = color;
            LastSeries.MarkerSize = markersize;
            if (markerbordercolor.HasValue)
            {
                LastSeries.MarkerBorderColor = markerbordercolor.Value;
                LastSeries.MarkerBorderWidth = markerborderwidth;
            }
            return LastSeries;
        }

        // configure LastSeries data labels
        public Series ShowSeriesDataLabels(Color? back = null, Font fnt = null, Color? defaultlabelcolor = null)     // fore colour set by data point
        {
            LastSeries.IsValueShownAsLabel = true;
            LastSeries.LabelBackColor = back ?? Color.Transparent;
            LastSeries.Font = fnt ?? Font;
            LastSeries.LabelForeColor = defaultlabelcolor ?? Color.Black;
            return LastSeries;
        }

        // configure LastSeries labels borders
        public Series ShowSeriesDataLabelsBorder(Color fore, int width, ChartDashStyle style)
        {
            LastSeries.LabelBorderColor = fore;
            LastSeries.LabelBorderDashStyle = style;
            LastSeries.LabelBorderWidth = width;
            return LastSeries;
        }

        public Series SetSeriesXAxisLabelType(ChartValueType chartvaluetypex)
        {
            LastSeries.XValueType = chartvaluetypex;
            return LastSeries;
        }

        public Series SetSeriesYAxisLabelType(ChartValueType chartvaluetypey)
        {
            LastSeries.XValueType = chartvaluetypey;
            return LastSeries;
        }


        // Add point to LastSeries
        public DataPoint AddPoint(DataPoint d, Color? labelcolor = null)
        {
            LastSeries.Points.Add(d);
            if (labelcolor.HasValue)
                d.LabelForeColor = labelcolor.Value;
            LastDataPoint = d;
            return d;
        }

        // Add xy point to LastSeries
        // for dates, the chart uses the day count as the X enumerator.
        public DataPoint AddXY(object x, object y, Color? labelcolor = null)
        {
            LastSeries.Points.AddXY(x, y);
            LastDataPoint = LastSeries.Points[LastSeries.Points.Count - 1];
            if (labelcolor.HasValue)
                LastDataPoint.LabelForeColor = labelcolor.Value;
            return LastDataPoint;
        }

        // Set label properties of LastDataPoint
        public void SetPointDataLabelCustomColorBorder(Color fore, Color back, Color borderfore, int width, ChartDashStyle style)
        {
            SetPointDataLabelCustomColorBorder(LastDataPoint, fore, back, borderfore, width, style);
        }

        // Set marker properties of LastDataPoint
        public void SetPointMarkerStyle(MarkerStyle style, Color color, int markersize = 1, Color? markerbordercolor = null, int markerborderwidth = 2)
        {
            SetPointMarkerStyle(LastDataPoint, style, color, markersize, markerbordercolor, markerborderwidth);
        }

        // markers

        // Set label properties of a point
        static public void SetPointDataLabelCustomColorBorder(DataPoint dp, Color fore, Color back, Color borderfore, int width, ChartDashStyle style)
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

        public ChartArea LastChartArea { set; get; }
        public Series LastSeries { set; get; }
        public DataPoint LastDataPoint { get; set; }

    }

}
