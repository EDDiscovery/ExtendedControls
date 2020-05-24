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

namespace ExtendedControls.Controls
{
    public partial class ExtScatterPlot : UserControl
    {
        List<List<double[]>> Points = new List<List<double[]>>();
        List<PointF[]> DataPoints = new List<PointF[]>();
        private double focalLength = 1000;
        private double distance = 5;
        private double[] cameraPosition = new double[3];
        
        // Mouse 
        private bool leftMousePressed = false;
        private bool rightMousePressed = false;
        private PointF ptMouseClick;

        // Azymuth is the horizontal direction expressed as the angular distance between the direction of a fixed point (such as the observer's heading) and the direction of the object
        private double lastAzimuth, azimuth = 0;
        // Elevation is the angular distance of something (such as a celestial object) above the horizon
        private double lastElevation, elevation = 0;


        [Description("Set the distance at which the camera stands from the plot"), Category("Scatter Plot")]
        public double Distance
        {
            get { return distance; }
            set { distance = (value >= 0.1) ? distance = value : distance; UpdateProjection(); }
        }

        [Description("Focal length of the camera"), Category("Scatter Plot")]
        public new double Focus
        {
            get { return focalLength; }
            set { focalLength = value; UpdateProjection(); }
        }

        [Description("Camera position"), Category("Scatter Plot")]
        public double[] Camera
        {
            get { return cameraPosition; }
            set { cameraPosition = value; UpdateProjection(); }
        }

        [Description("Horizontal direction of the camera expressed as the angular distance"), Category("Scatter Plot")]
        public double Azimuth
        {
            get { return azimuth; }
            set { azimuth = value; UpdateProjection(); }
        }

        [Description("Vertical direction of the camera expressed as the angular distance"), Category("Scatter Plot")]
        public double Elevation
        {
            get { return elevation; }
            set { elevation = value; UpdateProjection(); }
        }

        public ExtScatterPlot()
        {
            InitializeComponent();
            ScatterPlotHelpers.MouseWheelHandler.Add(this, OnMouseWheel);
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

        Color[] colorIdx = new Color[] { Color.Blue, Color.Aqua, Color.White, Color.Yellow, Color.Orange, Color.DarkOrange, Color.Red, Color.Fuchsia, Color.Purple, Color.Gray, Color.DarkGray, Color.Black };
                
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Pick the background color defined in the designer
            SolidBrush backColor = new SolidBrush(BackColor);

            Graphics g = this.CreateGraphics();
            g.FillRectangle(backColor, new Rectangle(0, 0, this.Width, this.Height));
            if (DataPoints != null)
            {
                for (int i = 0; i < DataPoints.Count; i++)
                {
                    foreach (PointF p in DataPoints[i])
                    {
                        g.FillEllipse(new SolidBrush(colorIdx[i % colorIdx.Length]), new RectangleF(p.X, p.Y, 4, 4));
                    }
                }
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

        public void Clear()
        {
            DataPoints.Clear();
            Points.Clear();
            Azimuth = 0;
            Elevation = 0;
        }
                
        private void ExtScatterPlot_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                leftMousePressed = true;
                ptMouseClick = new PointF(e.X, e.Y);
                lastAzimuth = azimuth;
                lastElevation = elevation;
            }
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

        private void UpdateProjection()
        {
            if (DataPoints == null)
                return;
            double x = distance * Math.Cos(elevation) * Math.Cos(azimuth);
            double y = distance * Math.Cos(elevation) * Math.Sin(azimuth);
            double z = distance * Math.Sin(elevation);
            cameraPosition = new double[3] { -y, z, -x };
            for (int i = 0; i < DataPoints.Count; i++)
                DataPoints[i] = ScatterPlotHelpers.Projection.ProjectVector(Points[i], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation);
            this.Invalidate();
        }
    }
}