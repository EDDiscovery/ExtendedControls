/*
 * Copyright © 2016 - 2020 EDDiscovery development team
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;

namespace ExtendedControls.Controls
{
    public partial class ExtAstroPlot : UserControl
    {
        List<List<double[]>> Points = new List<List<double[]>>();
        List<PointF[]> DataPoints = new List<PointF[]>();

        List<List<double[]>> Coords = new List<List<double[]>>();
        List<PointF[]> AxesAnchors = new List<PointF[]>();

        List<List<double[]>> Ellipses = new List<List<double[]>>();
        List<PointF[]> DataEllipses = new List<PointF[]>();

        private double focalLength = 900;
        private double distance = 6;
        private int dotSize = 5;
        private double[] cameraPosition = new double[3];

        // Mouse 
        private bool leftMousePressed = false;        
        private PointF ptMouseClick;

        // Axes Widget
        private bool drawAxesWidget = true;
        private int axesWidgetThickness = 3;
        private int axesWidgetLength = 50;        

        // Azymuth is the horizontal direction expressed as the angular distance between the direction of a fixed point (such as the observer's heading) and the direction of the object
        private double lastAzimuth, azimuth = 0.3;
        // Elevation is the angular distance of something (such as a celestial object) above the horizon
        private double lastElevation, elevation = 0.3;
                
        #region Properties
        
        [Description("Set the distance at which the camera stands from the plot")]
        public double Distance
        {
            get { return distance; }
            set { distance = (value >= 0.1) ? distance = value : distance; UpdateProjection(); }
        }

        [Description("Focal length of the camera")]
        public new double Focus
        {
            get { return focalLength; }
            set { focalLength = value; UpdateProjection(); }
        }

        [Description("Camera position")]
        public double[] Camera
        {
            get { return cameraPosition; }
            set { cameraPosition = value; UpdateProjection(); }
        }

        [Description("Horizontal direction of the camera expressed as an angular distance")]
        public double Azimuth
        {
            get { return azimuth; }
            set { azimuth = value; UpdateProjection(); }
        }

        [Description("Vertical direction of the camera expressed as an angular distance")]
        public double Elevation
        {
            get { return elevation; }
            set { elevation = value; UpdateProjection(); }
        }

        [Description("Diameter of the dots")]
        public int PointsSize
        {
            get { return dotSize; }
            set { dotSize = value; UpdateProjection(); }
        }

        [Description("Toggle the axes widget display")]
        public bool AxesWidget
        {
            get { return drawAxesWidget; }
            set { drawAxesWidget = value; UpdateProjection(); }
        }

        [Description("Set the thickness of each axis in the axes widget")]
        public int AxisThickness
        {
            get { return axesWidgetThickness; }
            set { axesWidgetThickness = value; UpdateProjection(); }
        }

        [Description("Set the length of each axis in the axes widget")]
        public int AxisLength
        {
            get { return axesWidgetLength; }
            set { axesWidgetLength = value; UpdateProjection(); }
        }
        #endregion

        public ExtAstroPlot()
        {
            InitializeComponent();
            ScatterPlotHelpers.MouseWheelHandler.Add(this, OnMouseWheel);
            DrawAxesWidget();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        Color[] colors = new Color[] { Color.LightBlue, Color.Aqua,  Color.Yellow, Color.Orange, Color.DarkOrange, Color.White, Color.DarkViolet, Color.Gray, Color.DarkGray};
                
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Pick the background color defined in the designer
            SolidBrush backColor = new SolidBrush(BackColor);
            SolidBrush axisAnchor = new SolidBrush(Color.White);

            Pen AxisPen = new Pen(new SolidBrush(Color.White));
            AxisPen.Width = 1;

            // axes center point            
            var center = new PointF(this.Width / 2, this.Height / 2);            
            var dotDiameter = PointsSize;

            Graphics g = this.CreateGraphics();
            g.FillRectangle(backColor, new Rectangle(0, 0, this.Width, this.Height));
            if (DataPoints != null)
            {
                for (int i = 0; i < DataPoints.Count; i++)
                {
                    foreach (PointF p in DataPoints[i])
                    {
                        g.FillEllipse(new SolidBrush(colors[i % colors.Length]), new RectangleF(p.X, p.Y, dotDiameter, dotDiameter));                        
                    }
                }
            }
            if (AxesAnchors != null)
            {
                for (int i = 0; i < AxesAnchors.Count; i++)
                {
                    for (int c = 0; c < AxesAnchors[i].Length; c++)
                    {
                        PointF p = AxesAnchors[i][c];
                        if (c == 0)
                        {
                            axisAnchor.Color = Color.Red;
                            AxisPen.Color = Color.Red;
                        }
                        if (c == 1)
                        {
                            axisAnchor.Color = Color.Green;
                            AxisPen.Color = Color.Green;
                        }
                        if (c == 2)
                        {
                            axisAnchor.Color = Color.Blue;
                            AxisPen.Color = Color.Blue;
                        }

                        g.FillEllipse(axisAnchor, new RectangleF(p.X, p.Y, 2, 2));
                        var axisAnchorPoint = new PointF(p.X, p.Y);
                        g.DrawLine(AxisPen, center, axisAnchorPoint);
                    }
                }
            }
        }

        private void DrawAxesWidget()
        {
            if (drawAxesWidget)
            {
                List<double[]> Coords = new List<double[]>();

                Coords.Add(new double[] { AxisLength * 0.5, 0.0, 0.0, 0 });
                Coords.Add(new double[] { 0.0, AxisLength * 0.5, 0.0, 1 });
                Coords.Add(new double[] { 0.0, 0.0, AxisLength * 0.5, 2 });

                // draw the anchors points
                AddAnchors(Coords);
                
                Coords.Clear();                               
            }
        }

        public void AddPoint(double x, double y, double z, int series)
        {
            if (Points.Count - 1 < series)
            {
                Points.Add(new List<double[]>());
            }

            Points[series].Add(new double[] { x, y, z });

            foreach (List<double[]> ser in Points)
            {
                if (DataPoints.Count - 1 < series)
                    DataPoints.Add(ScatterPlotHelpers.Projection.ProjectVector(ser, this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));
                else
                    DataPoints[series] = ScatterPlotHelpers.Projection.ProjectVector(ser, this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation);
            }

            this.Invalidate();
        }

        public void AddPoints(List<double[]> points)
        {
            List<double[]> _tmp = new List<double[]>(points);
            Points.Add(_tmp);
            DataPoints.Add(ScatterPlotHelpers.Projection.ProjectVector(Points[Points.Count - 1], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));                       
            UpdateProjection();
        }

        public void AddCoords(double x, double y, double z, int series)
        {
            if (Coords.Count - 1 < series)
            {
                Coords.Add(new List<double[]>());
            }

            Coords[series].Add(new double[] { x, y, z });

            foreach (List<double[]> ser in Coords)
            {
                if (AxesAnchors.Count - 1 < series)
                    AxesAnchors.Add(ScatterPlotHelpers.Projection.ProjectVector(ser, this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));
                else
                    AxesAnchors[series] = ScatterPlotHelpers.Projection.ProjectVector(ser, this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation);
            }

            this.Invalidate();
        }

        public void AddAnchors(List<double[]> anchors)
        {
            List<double[]> _anchors = new List<double[]>(anchors);
            Coords.Add(_anchors);
            AxesAnchors.Add(ScatterPlotHelpers.Projection.ProjectVector(Coords[Coords.Count - 1], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));
            UpdateProjection();
        }

        private void UpdateProjection()
        {
            if (DataPoints == null)
                return;
            else
            {
                double x = distance * Math.Cos(elevation) * Math.Cos(azimuth);
                double y = distance * Math.Cos(elevation) * Math.Sin(azimuth);
                double z = distance * Math.Sin(elevation);
                cameraPosition = new double[3] { -y, z, -x };
                for (int i = 0; i < DataPoints.Count; i++)
                    DataPoints[i] = ScatterPlotHelpers.Projection.ProjectVector(Points[i], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation);                
            }

            if (AxesAnchors == null)
                return;
            else
            {
                if (drawAxesWidget)
                {
                    double x = distance * Math.Cos(elevation) * Math.Cos(azimuth);
                    double y = distance * Math.Cos(elevation) * Math.Sin(azimuth);
                    double z = distance * Math.Sin(elevation);
                    cameraPosition = new double[3] { -y, z, -x };
                    for (int i = 0; i < AxesAnchors.Count; i++)
                    AxesAnchors[i] = ScatterPlotHelpers.Projection.ProjectVector(Coords[i], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation);                    
                }
            }

            this.Invalidate();            
        }

        public void Clear()
        {
            DataPoints.Clear();
            Points.Clear();
            Reset();
        }

        public void Reset()
        {
            Azimuth = 0.3;
            Elevation = 0.3;
            Distance = distance;
        }

        #region Interaction
        private void ExtScatterPlot_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                leftMousePressed = true;
                ptMouseClick = new PointF(e.X, e.Y);
                lastAzimuth = azimuth;
                lastElevation = elevation;                
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Right)                
                Reset();
        }               

        private void ExtScatterPlot_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftMousePressed)
            {
                azimuth = lastAzimuth - (ptMouseClick.X - e.X) / 100;
                elevation = lastElevation + (ptMouseClick.Y - e.Y) / 100;
                UpdateProjection();
            }
        }

        private void ExtScatterPlot_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                leftMousePressed = false;            
        }

        private void ExtScatterPlot_SizeChanged(object sender, EventArgs e)
        {
            if (DataPoints != null)
                UpdateProjection();
        }

        private void OnMouseWheel(MouseEventArgs e)
        {
            Distance += -e.Delta / 500D;
        }
        #endregion
    }
}