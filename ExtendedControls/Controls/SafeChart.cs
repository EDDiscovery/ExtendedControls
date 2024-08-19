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
        public static Color RequestTheme = ExtChart.RequestTheme;
        public static Color Disable = ExtChart.Disable;

        public override Color BackColor { get { return chart?.BackColor ?? base.BackColor; }
                            set { if (chart != null) chart.BackColor = value; else base.BackColor = value; } }

        public bool Active { get { return chart != null; } }

        public Action<ExtSafeChart, string, AxisName, double> CursorPositionChanged = null;

        // setting position will turn it on. 
        public ElementPosition LeftArrowPosition { get { return leftarrowposition; } set { if (chart != null) { leftarrowposition = value; leftArrowButton.Visible = !leftarrowposition.Size.IsEmpty; PerformLayout(); Invalidate(); } } }
        public ElementPosition RightArrowPosition { get { return rightarrowposition; } set { if (chart != null) { rightarrowposition = value; rightArrowButton.Visible = !rightarrowposition.Size.IsEmpty; PerformLayout(); Invalidate(); } } }

        public bool LeftArrowEnable { get { return leftArrowButton?.Enabled ?? false; } set { if (leftArrowButton != null) leftArrowButton.Enabled = value; } }
        public bool RightArrowEnable { get { return rightArrowButton?.Enabled ?? false; } set { if (rightArrowButton != null) rightArrowButton.Enabled = value; } }

        public Action<bool> ArrowButtonPressed = null;

        public ExtSafeChart()
        {
            bool made = false;

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                try
                {
                    chart = new ExtChart();
                    chart.Dock = DockStyle.Fill;
                    chart.CursorPositionChanged += (s, e) =>
                    {
                        CursorPositionChanged?.Invoke(this, e.ChartArea.Name, e.Axis.AxisName, e.ChartArea.CursorX.Position);
                    };

                    leftArrowButton = new ExtButton() { Name = "SCL", Visible = false, Image = Properties.Resources.ArrowLeft };
                    leftArrowButton.Click += (s, e) => { ArrowButtonPressed?.Invoke(false); };
                    rightArrowButton = new ExtButton() { Name = "SCR", Visible = false, Image = Properties.Resources.ArrowRight };
                    rightArrowButton.Click += (s, e) => { ArrowButtonPressed?.Invoke(true); };

                    this.Controls.Add(leftArrowButton);
                    this.Controls.Add(rightArrowButton);
                    this.Controls.Add(chart);

                    made = true;
                }
                catch
                {
                }
            }

            if ( !made )
            { 
                Label l = new Label();
                l.AutoSize = true;
                l.Text = "<code This OS does not support charts>";
                l.Location = new Point(10, 10);
                l.ForeColor = Color.Red;
                this.Controls.Add(l);
            }
        }

        public void Theme(Theme thm)
        {
            if (chart != null && thm != null)
                thm.ThemeChart(thm.GetFont,chart);
        }

        public void SetBorder(int width, ChartDashStyle style, Color? b = null)
        {
            chart?.SetBorder(width, style, b);
        }

        public void BeginInit()
        {
            chart?.BeginInit();
        }
        public void EndInit()
        {
            chart?.EndInit();
        }

        public Title AddTitle(string name, string text,
                                 Color? textcolor = null, Color? backcolor = null, Font font = null,
                                 Docking dockingpos = Docking.Top, ContentAlignment? alignment = null, ElementPosition position = null,
                                 Color? bordercolor = null, int borderwidth = 1, ChartDashStyle borderdashstyle = ChartDashStyle.Solid,
                                 Color? shadowcolor = null, int shadowoffset = 0)
        {
            return chart?.AddTitle(name, text, textcolor, backcolor, font, dockingpos, alignment,position, bordercolor,borderwidth,borderdashstyle,shadowcolor,shadowoffset) ?? null; 
        }
        public Title SetCurrentTitle(string name)
        {
            return chart?.SetCurrentTitle(name);
        }
        public Title SetCurrentTitle(int i)
        {
            return chart?.SetCurrentTitle(i);
        }
        public void SetTitleText(string text)
        {
            chart?.SetTitleText(text);
        }
        public void SetTitlePosition(ElementPosition p)
        {
            chart?.SetTitlePosition(p);
        }

        //////////////////////////////////////////////////////////////////////////// Legend

        public Legend AddLegend(string name, Color? textcolor =null, Color? backcolor = null, Font font = null,
                                LegendStyle legendstyle = LegendStyle.Column,
                                ElementPosition position = null,
                                Docking dock = Docking.Right,       // dock only used if position = null
                                Color? bordercolor = null, ChartDashStyle borderdashstyle = ChartDashStyle.Solid, int borderwidth = 1,
                                Color? shadowcolor = null, int shadowoffset = 0,
                                int textautowrap = 30, int minfontsize = 5)
        {
            return chart?.AddLegend(name, textcolor, backcolor, font, legendstyle, position, dock, bordercolor, borderdashstyle, borderwidth, shadowcolor, shadowoffset, textautowrap, minfontsize) ?? null;
        }

        public Legend SetCurrentLegend(string name)
        {
            return chart?.SetCurrentLegend(name);
        }
        public Legend SetCurrentLegend(int i)
        {
            return chart?.SetCurrentLegend(i);
        }

        public void SetLegendTitle(string title, Color? forecolor = null, Color? backcolor = null, Font font = null, StringAlignment? alignment = null,
                                            LegendSeparatorStyle? sep = null, Color? seperatorcolor = null)
        {
            chart?.SetLegendTitle(title, forecolor, backcolor, font, alignment, sep, seperatorcolor);
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

        public bool IsStartedFromZeroX { get { return chart?.IsStartedFromZeroX ?? false; } set { if (chart != null) chart.IsStartedFromZeroX = value; } }
        public bool IsStartedFromZeroY { get { return chart?.IsStartedFromZeroY ?? false; } set { if (chart != null) chart.IsStartedFromZeroY = value; } }

        public void SetChartAreaPlotArea(ElementPosition pos)
        {
            chart?.SetChartAreaPlotArea(pos);
        }

        public void SetChartAreaVisible(bool visible)
        {
            if (chart != null)
                chart.CurrentChartArea.Visible = visible;
        }

        public void SetChartAreaVisible(int chartnumber, bool visible)
        {
            if (chart != null)
                chart.ChartAreas[chartnumber].Visible = visible;
        }

        //////////////////////////////////////////////////////////////////////////// X

        public void SetXAxisInterval(DateTimeIntervalType type, int interval, IntervalAutoMode im = IntervalAutoMode.FixedCount,
                                       double offset = 0, DateTimeIntervalType offsettype = DateTimeIntervalType.Auto)
        {
            chart?.SetXAxisInterval(type, interval, im, offset, offsettype);
        }

        public void SetXAxisMaxMin(double min, double max)
        {
            chart?.SetXAxisMaxMin(min, max);
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
        public void SetXAxisTitle(string format, Font font = null, Color? forecolor = null, StringAlignment? alignment = null)
        {
            chart?.SetXAxisTitle(format, font, forecolor, alignment);
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
        public void SetYAxisMaxMin(double min, double max)
        {
            chart?.SetYAxisMaxMin(min, max);
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
        public void SetYAxisTitle(string format, Font font = null, Color? forecolor = null, StringAlignment? alignment = null)
        {
            chart?.SetYAxisTitle(format, font, forecolor, alignment);
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

        public Series AddSeries(string name, string chartarea, SeriesChartType type, Color? seriescolor = null, string legend = null)
        {
            return chart?.AddSeries(name, chartarea, type, seriescolor,legend) ?? null;
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

        //////////////////////////////////////////////////////////////////////////// Points in CurrentSeries

        public void ClearSeriesPoints()
        {
            chart?.ClearSeriesPoints();
        }
        public void ClearAllSeriesPoints()
        {
            chart?.ClearAllSeriesPoints();
        }
        public DataPoint AddPoint(double d, string label = null, string legendtext = null, Color? pointcolor = null, bool showvalue = false, string graphtooltip = null, string legendtooltip = null, string axislabel = null)
        {
            return chart?.AddPoint(d, label, legendtext, pointcolor, showvalue, graphtooltip, legendtooltip, axislabel);
        }

        public DataPoint AddPoint(DataPoint d, string label = null, string legendtext = null, Color? pointcolor = null, bool showvalue = false, string graphtooltip = null, string legendtooltip = null, string axislabel = null)
        {
            return chart?.AddPoint(d, label, legendtext, pointcolor, showvalue, graphtooltip, legendtooltip, axislabel);
        }
        public DataPoint AddXY(object x, object y, string label = null, string legendtext = null, Color? pointcolor = null, bool showvalue = false, string graphtooltip = null, string legendtooltip = null, string axislabel = null)
        {
            return chart?.AddXY(x, y, label, legendtext, pointcolor, showvalue, graphtooltip, legendtooltip, axislabel);
        }

        public bool CompareYPoints(double[] values, int yentry = 0)
        {
            return chart?.CompareYPoints(values, yentry) ?? true;       // true, no action, if no chart
        }

        public void SetCurrentPoint(int i)
        {
            chart?.SetCurrentPoint(i);
        }

        public void SetPointLabel(string label)
        {
            chart?.SetPointLabel(label);
        }

        public int GetNumberPoints()
        {
            return chart?.CurrentSeries.Points.Count ?? 0;
        }

        public DataPoint GetPoint(int i)
        {
            return chart?.CurrentSeries.Points[i] ?? null;
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

        public void ReportOnMouseDown(Action<HitTestResult> action)
        {
            chart?.ReportOnMouseDown(action);
        }

        public void EnableZoomMouseWheelX(bool on = true)
        {
            chart?.EnableZoomMouseWheelX(on);
        }

        public double ZoomMouseWheelXMinimumInterval { get { return chart?.ZoomMouseWheelXMinimumInterval ?? 0; } set { if (chart != null) chart.ZoomMouseWheelXMinimumInterval = value; } }
        public double ZoomMouseWheelXZoomFactor { get { return chart?.ZoomMouseWheelXZoomFactor ?? 0; } set { if (chart != null) chart.ZoomMouseWheelXZoomFactor = value; } }


        #region Internal

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (!LeftArrowPosition.Size.IsEmpty)
            {
                Rectangle area = chart.GetArea(LeftArrowPosition);
                leftArrowButton.Bounds = area;

                if (chart.Titles.Count > 0 && leftArrowButton.BackColor != chart.Titles[0].BackColor)       // so here, we want to override the themeing of a button to make it the same as titles
                {
                    leftArrowButton.BackColor = chart.Titles[0].BackColor;
                    leftArrowButton.ButtonColorScaling = 1;
                }

                //                System.Diagnostics.Debug.WriteLine($"LA size = {leftArrowButton.Bounds} on {Width} {Height}");
            }
            if (!RightArrowPosition.Size.IsEmpty)
            {
                Rectangle area = chart.GetArea(RightArrowPosition);
                rightArrowButton.Bounds = area;
                if (chart.Titles.Count > 0 && rightArrowButton.BackColor != chart.Titles[0].BackColor)
                {
                    rightArrowButton.BackColor = chart.Titles[0].BackColor;
                    rightArrowButton.ButtonColorScaling = 1;
                }
                
            }
        }



        private ExtChart chart;
        private ElementPosition leftarrowposition = new ElementPosition();
        private ElementPosition rightarrowposition = new ElementPosition();
        private ExtButton leftArrowButton;
        private ExtButton rightArrowButton;

        #endregion
    }
}
