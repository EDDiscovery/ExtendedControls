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
using System.Configuration;

namespace ExtendedControls.Controls
{
    public partial class ExtAstroPlot : UserControl
    {
        // points used to draw and orient the axes widget
        private List<List<double[]>> Coords = new List<List<double[]>>();
        private List<PointF[]> AxesAnchors = new List<PointF[]>();

        // points used for the boundaries cube
        public List<List<double[]>> BoundariesCorners = new List<List<double[]>>();
        private List<PointF[]> BoundariesFrame = new List<PointF[]>();

        // MapPlot objects
        private List<List<double[]>> MapBodies = new List<List<double[]>>();
        private List<PointF[]> MapPoints = new List<PointF[]>();
            
        // Orrery objects
        private List<List<double[]>> OrreryBodies = new List<List<double[]>>();
        private List<List<double[]>> OrreryCenters = new List<List<double[]>>();
        private List<PointF[]> OrreryOrbits = new List<PointF[]>();
        private List<PointF[]> OrreryMassCenters = new List<PointF[]>();

        private double focalLength = 900;
        private double distance = 6;
        private double[] cameraPosition = new double[3];

        private double[] centerCoordinates = new double[3];

        private int horizontalTranslation = 0;
        private int verticalTranslation = 0;
        
        // Objects
        private int smallDotSize = 4;
        private int mediumDotSize = 8;
        private int largeDotSize = 12;

        // Mouse 
        private bool leftMousePressed = false;
        private bool rightMousePressed = false;
        private bool middleMousePressed = false;
        private PointF ptMouseClick;
        private int mouseMovementSens = 150;
        private double mouseWheelSens = 300;

        // Axes Widget
        private bool drawAxesWidget = true;
        private int axesWidgetThickness = 3;
        private int axesWidgetLength = 50;

        // Boundaries Cube
        private bool drawBoundariesWidget = true;
        private double boundariesRadiusWidth = 0.8;
        private int boundariesWidgetThickness = 1;

        // Azymuth is the horizontal direction expressed as the angular distance between the direction of a fixed point (such as the observer's heading) and the direction of the object
        private double lastAzimuth, azimuth = 0.3;
        // Elevation is the angular distance of something (such as a celestial object) above the horizon
        private double lastElevation, elevation = 0.3;

        // Translation
        private double lastHorizontal, horizontal = 0.0;
        private double lastVertical, vertical = 0.0;

        #region Properties

        [Description("Set the distance at which the camera stands from the plot")]
        public int HorizontalTranslation 
        {
            get { return horizontalTranslation; }
            private set { horizontalTranslation = value; }
        }

        [Description("Set the distance at which the camera stands from the plot")]
        public int VerticalTranslation
        {
            get { return verticalTranslation; }
            private set { verticalTranslation = value; }
        }
        
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

        [Description("Set the coordinates of the center of the plot")]
        public double[] CoordsCenter
        {
            get { return centerCoordinates; }
            set { centerCoordinates = value; UpdateProjection(); }
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

        [Description("Diameter of the smaller dots")]
        public int SmallDotSize
        {
            get { return smallDotSize; }
            set { smallDotSize = value; UpdateProjection(); }
        }

        [Description("Diameter of the smaller dots")]
        public int MediumDotSize
        {
            get { return mediumDotSize; }
            set { mediumDotSize = value; UpdateProjection(); }
        }

        [Description("Diameter of the smaller dots")]
        public int LargeDotSize
        {
            get { return largeDotSize; }
            set { largeDotSize = value; UpdateProjection(); }
        }

        [Description("Toggle the axes widget display")]
        public bool AxesWidget
        {
            get { return drawAxesWidget; }
            set { drawAxesWidget = value; UpdateProjection(); }
        }

        [Description("Set the thickness of each axis in the axes widget")]
        public int AxesThickness
        {
            get { return axesWidgetThickness; }
            set { axesWidgetThickness = value; UpdateProjection(); }
        }

        [Description("Set the length of each axis in the axes widget")]
        public int AxesLength
        {
            get { return axesWidgetLength; }
            set { axesWidgetLength = value; UpdateProjection(); }
        }
                
        [Description("Toggle the boundaries cube")]
        public bool BoundariesWidget
        {
            get { return drawBoundariesWidget; }
            set { drawBoundariesWidget = value; UpdateProjection(); }
        }

        public double BoundariesRadius
        {
            get { return boundariesRadiusWidth; }
            set { boundariesRadiusWidth = value; UpdateProjection(); }
        }

        [Description("Set the boundaries cube frame thickness")]
        public int BoundariesFrameThickness
        {
            get { return boundariesWidgetThickness; }
            set { boundariesWidgetThickness = value; UpdateProjection(); }
        }

        [Description("Set the sensitivity of the mouse movement")]
        public int MouseSensitivity_Movement
        {
            get { return mouseMovementSens; }
            set { mouseMovementSens = value; UpdateProjection(); }
        }

        [Description("Set the sensisitivy of the mouse wheel")]
        public double MouseSensitivity_Wheel
        {
            get { return mouseWheelSens; }
            set { mouseWheelSens = value; UpdateProjection(); }
        }
        #endregion

        public ExtAstroPlot()
        {
            InitializeComponent();
            AstroPlot.MouseWheel.Add(this, OnMouseWheel);                        
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

        private void plot_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);

            // Pick the background color defined in the designer            
            _ = new SolidBrush(ForeColor);

            Pen AxisPen = new Pen(new SolidBrush(ForeColor))
            {
                Width = 1
            };

            Pen BoundariesPen = new Pen(new SolidBrush(ForeColor))
            {
                Width = 1,
                DashStyle = System.Drawing.Drawing2D.DashStyle.Dot
            };

            Pen OrreryOrbitsPen = new Pen(new SolidBrush(Color.White))
            {
                Width = 1,
                DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
            };

            // center point            
            var center = new PointF((int)(this.Width / 2), (int)(this.Height / 2));

            // give some love to the renderint engine
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

            // axes
            if (AxesAnchors != null)
            {
                for (int i = 0; i < AxesAnchors.Count; i++)
                {
                    for (int c = 0; c < AxesAnchors[i].Length; c++)
                    {
                        PointF p = AxesAnchors[i][c];
                        if (c == 0) // x axis
                        {
                            AxisPen.Color = Color.Red;
                        }
                        if (c == 1) // y axys
                        {
                            AxisPen.Color = Color.Green;
                        }
                        if (c == 2) // x axis
                        {
                            AxisPen.Color = Color.Blue;
                        }

                        var axisAnchorPoint = new PointF(p.X, p.Y);
                        e.Graphics.DrawLine(AxisPen, center, axisAnchorPoint);
                    }
                }
            }

            // boundaries
            if (BoundariesFrame != null)
            {
                if (BoundariesFrame.Count > 0)
                {
                    // bottom
                    e.Graphics.DrawLine(BoundariesPen, BoundariesFrame[0][0], BoundariesFrame[0][1]);
                    e.Graphics.DrawLine(BoundariesPen, BoundariesFrame[0][1], BoundariesFrame[0][5]);
                    e.Graphics.DrawLine(BoundariesPen, BoundariesFrame[0][5], BoundariesFrame[0][3]);
                    e.Graphics.DrawLine(BoundariesPen, BoundariesFrame[0][3], BoundariesFrame[0][0]);

                    // left
                    e.Graphics.DrawLine(BoundariesPen, BoundariesFrame[0][0], BoundariesFrame[0][2]);
                    e.Graphics.DrawLine(BoundariesPen, BoundariesFrame[0][2], BoundariesFrame[0][4]);
                    e.Graphics.DrawLine(BoundariesPen, BoundariesFrame[0][4], BoundariesFrame[0][3]);

                    // right
                    e.Graphics.DrawLine(BoundariesPen, BoundariesFrame[0][1], BoundariesFrame[0][6]);
                    e.Graphics.DrawLine(BoundariesPen, BoundariesFrame[0][6], BoundariesFrame[0][7]);
                    e.Graphics.DrawLine(BoundariesPen, BoundariesFrame[0][7], BoundariesFrame[0][5]);

                    // top
                    e.Graphics.DrawLine(BoundariesPen, BoundariesFrame[0][2], BoundariesFrame[0][6]);
                    e.Graphics.DrawLine(BoundariesPen, BoundariesFrame[0][4], BoundariesFrame[0][7]);
                }
            }

            // map
            if (MapPoints != null)
            {
                for (int i = 0; i < MapPoints.Count; i++)
                {
                    foreach (PointF p in MapPoints[i])
                    {
                        e.Graphics.FillEllipse(new SolidBrush(colors[i % colors.Length]), new RectangleF(p.X - SmallDotSize / 2, p.Y - SmallDotSize / 2, SmallDotSize, SmallDotSize));
                    }
                }
            }

            // orrery
            if (OrreryOrbits != null)
            {
                var orreryCenter = new Point(this.Width / 2, this.Height / 2);

                for (int i = 0; i < OrreryMassCenters.Count; i++)
                {
                    foreach (PointF p in OrreryMassCenters[i])
                    {
                        e.Graphics.FillEllipse(new SolidBrush(Color.Orange), new RectangleF(p.X - LargeDotSize / 2, p.Y - LargeDotSize / 2, LargeDotSize, LargeDotSize));
                    }
                }

                for (int i = 0; i < OrreryOrbits.Count; i++)
                {

                    // draw a fake central star, just for fun - REALLY, IT'S JUST TEMPORARY!
                    e.Graphics.FillEllipse(new SolidBrush(Color.Yellow), new RectangleF(orreryCenter.X - LargeDotSize / 2, orreryCenter.Y - LargeDotSize / 2, LargeDotSize, LargeDotSize));

                    // draw the orbit

                    if (OrreryOrbits[0].Length > 0)
                        for (int o = 0; o < OrreryOrbits[0].Length; o++)
                        {
                            Point[] frameCorners = new Point[] {
                        new Point { X = (int)OrreryOrbits[1][o].X, Y = (int)OrreryOrbits[1][o].Y },
                        new Point { X = (int)OrreryOrbits[2][o].X, Y = (int)OrreryOrbits[2][o].Y },
                        new Point { X = (int)OrreryOrbits[3][o].X, Y = (int)OrreryOrbits[3][o].Y },
                        new Point { X = (int)OrreryOrbits[4][o].X, Y = (int)OrreryOrbits[4][o].Y }
                        };
                            e.Graphics.DrawClosedCurve(OrreryOrbitsPen, frameCorners, (float)0.8, System.Drawing.Drawing2D.FillMode.Alternate);
                        }

                    foreach (PointF p in OrreryOrbits[i])
                    {
                        if (i == 0)
                            e.Graphics.FillEllipse(new SolidBrush(colors[i % colors.Length]), new RectangleF(p.X - MediumDotSize / 2, p.Y - MediumDotSize / 2, MediumDotSize, MediumDotSize));
                        //debug only:
                        //else g.FillEllipse(new SolidBrush(colors[i % colors.Length]), new RectangleF(p.X - MediumDotSize / 2, p.Y - MediumDotSize / 2, 3, 3));                            
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Pick the background color defined in the designer
            var backColor = new SolidBrush(BackColor);            

            Graphics g = this.CreateGraphics();                       
            g.FillRectangle(backColor, new Rectangle(0, 0, this.Width, this.Height));
        }

        #region Axes Widget
        private void AddAnchors(List<double[]> anchors)
        {
            List<double[]> _anchors = new List<double[]>(anchors);
            Coords.Add(_anchors);
            AxesAnchors.Add(AstroPlot.Update(Coords[Coords.Count - 1], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));
            UpdateProjection();
        }

        public void DrawAxesWidget(int length)
        {
            if (drawAxesWidget)
            {
                List<double[]> Coords = new List<double[]>
                {
                    new double[] { length * 0.5, 0.0, 0.0, 0 },
                    new double[] { 0.0, -(length * 0.5), 0.0, 1 },
                    new double[] { 0.0, 0.0, -(length * 0.5), 2 }
                };

                // draw the anchors points
                AddAnchors(Coords);

                Coords.Clear();
            }
        }
        #endregion

        #region Boundaries Frame        
        private void AddBoundaries(List<double[]> corners)
        {
            List<double[]> _corners = new List<double[]>(corners);
            BoundariesCorners.Add(_corners);
            BoundariesFrame.Add(AstroPlot.Update(BoundariesCorners[0], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));
            UpdateProjection();
        }

        public void DrawBoundariesWidget(double BoundariesRadius)
        {
            if (drawBoundariesWidget)
            {
                List<double[]> Corners = new List<double[]>
                {
                    new double[] { BoundariesRadius, BoundariesRadius, BoundariesRadius },
                    new double[] { BoundariesRadius, BoundariesRadius, -BoundariesRadius },
                    new double[] { BoundariesRadius, -BoundariesRadius, BoundariesRadius },
                    new double[] { -BoundariesRadius, BoundariesRadius, BoundariesRadius },
                    new double[] { -BoundariesRadius, -BoundariesRadius, BoundariesRadius },
                    new double[] { -BoundariesRadius, BoundariesRadius, -BoundariesRadius },
                    new double[] { BoundariesRadius, -BoundariesRadius, -BoundariesRadius },
                    new double[] { -BoundariesRadius, -BoundariesRadius, -BoundariesRadius }
                };

                AddBoundaries(Corners);

                Corners.Clear();
            }
        }

        #endregion

        #region Map        
        public void AddPointsToMap(List<double[]> points)
        {
            List<double[]> _tmp = new List<double[]>();
            for (int i = 0; i < points.Count; i++)
            {
                // normalize the coordinates to allow for center translation
                _tmp.Add(new double[] { points[i][0] - centerCoordinates[0], points[i][1] - centerCoordinates[1], points[i][2] - centerCoordinates[2] });
            }
            MapBodies.Add(_tmp);
            MapPoints.Add(AstroPlot.Update(MapBodies[MapBodies.Count - 1], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));
            UpdateProjection();
        }
        #endregion

        #region Orrery
        public void AddMassCentersToOrrery(List<double[]> centers)
        {
            List<double[]> _massCenters = new List<double[]>();

            for (int i = 0; i < centers.Count; i++)
            {
                var radius = AstroPlot.FindOrbitalRadius(centers[i][0], centers[i][1]);
                var elevation = AstroPlot.FindOrbitalElevation(centers[i][0], centers[i][1]);
                _massCenters.Add(new double[] { radius, elevation, radius });
            }

            OrreryCenters.Add(_massCenters);

            for (int i = 0; i < OrreryCenters.Count; i++)
            {
                OrreryMassCenters.Add(AstroPlot.Update(OrreryCenters[i], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));
            }
            UpdateProjection();
        }

        public void AddBodiesToOrrery(List<double[]> bodies)
        {            
            List<double[]> _orbits = new List<double[]>();

            for (int i = 0; i < bodies.Count; i++)
            {
                var radius = AstroPlot.FindOrbitalRadius(bodies[i][0], bodies[i][1]);
                var elevation = AstroPlot.FindOrbitalElevation(bodies[i][0], bodies[i][1]);
                _orbits.Add(new double[] { radius, elevation, radius });
            }
            
            OrreryBodies.Add(_orbits);

            ///  
            // create a frame to support the orbit drawing
            ///

            // initialize four empty series, one for each frame's corner
            List<double[]> _f1 = new List<double[]>();
            List<double[]> _f2 = new List<double[]>();
            List<double[]> _f3 = new List<double[]>();
            List<double[]> _f4 = new List<double[]>();

            // then, add a translated copy of each body coordinates
            if (bodies.Count > 0)
            {
                for (int i = 0; i < _orbits.Count; i++)
                {
                    double _fc = _orbits[i][0];
                    double _fi = _orbits[i][1];

                    _f1.Add(new double[] { _fc, _fi, _fc });
                    _f2.Add(new double[] { _fc, _fi, _fc * -1 });
                    _f3.Add(new double[] { _fc * -1, (_fi * -1), _fc * -1 });
                    _f4.Add(new double[] { _fc * -1, (_fi * -1), _fc });
                }
                OrreryBodies.Add(_f1);
                OrreryBodies.Add(_f2);
                OrreryBodies.Add(_f3);
                OrreryBodies.Add(_f4);
            }

            for (int i = 0; i < OrreryBodies.Count; i++)
            {
                OrreryOrbits.Add(AstroPlot.Update(OrreryBodies[i], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));
            }
            
            UpdateProjection();
        }
        #endregion

        #region Projection

        private void UpdateProjection()
        {
            plot.Location = new Point((int)lastHorizontal, (int)lastVertical);
            
            Debug.WriteLine("***");
            Debug.WriteLine("Camera position: " + cameraPosition[0] + ", " + cameraPosition[1] + ", " + cameraPosition[2]);
            Debug.WriteLine("Plot center: " + centerCoordinates[0] + ", " + centerCoordinates[1] + ", " + centerCoordinates[2]);

            double x = (distance * Math.Cos(elevation) * Math.Cos(azimuth));
            double y = (distance * Math.Cos(elevation) * Math.Sin(azimuth));
            double z = (distance * Math.Sin(elevation));

            cameraPosition = new double[3] { -y, z, -x };

            if (MapPoints == null)
            {
                return;
            }
            else
            {
                for (int i = 0; i < MapPoints.Count; i++)
                    MapPoints[i] = AstroPlot.Update(MapBodies[i], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation);                
            }

            if (OrreryOrbits == null)
            {
                return;
            }
            else
            {
                for (int i = 0; i < OrreryOrbits.Count; i++)
                    OrreryOrbits[i] = AstroPlot.Update(OrreryBodies[i], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation);
                for (int i = 0; i < OrreryCenters.Count; i++)                
                    OrreryMassCenters[i] = AstroPlot.Update(OrreryCenters[i], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation);                
            }

            if (AxesAnchors == null)
            {
                return;
            }
            else
            {
                if (drawAxesWidget)
                {
                    for (int i = 0; i < AxesAnchors.Count; i++)
                    AxesAnchors[i] = AstroPlot.Update(Coords[i], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation);                    
                }
            }

            if (BoundariesCorners == null)
            {
                return;
            }
            else
            {
                if (drawBoundariesWidget)
                {
                    for (int i = 0; i < BoundariesFrame.Count; i++)
                        BoundariesFrame[i] = AstroPlot.Update(BoundariesCorners[i], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation);
                }
            }

            Invalidate();
        }

        public void Clear()
        {
            MapPoints.Clear();
            MapBodies.Clear();
            OrreryBodies.Clear();
            OrreryOrbits.Clear();
        }
        #endregion

        #region Interaction

        private void plot_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // rotate
                leftMousePressed = true;
                ptMouseClick = new PointF(e.X, e.Y);
                lastAzimuth = azimuth;
                lastElevation = elevation;

                
            }
            if (e.Button == MouseButtons.Middle)
            {
                middleMousePressed = true;
                ptMouseClick = new PointF(e.X, e.Y);
                lastHorizontal = horizontal;
                lastVertical = vertical;
            }
        }

        private void ExtAstroPlot_SizeChanged(object sender, EventArgs e)
        {
            plot.Height = this.Height;
            plot.Width = this.Width;
            plot.Top = this.Top;
            plot.Left = this.Left;
        }

        private void plot_SizeChanged(object sender, EventArgs e)
        {
            if (MapPoints != null)
                UpdateProjection();
        }

        private void plot_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                leftMousePressed = false;
            if (e.Button == MouseButtons.Middle)
                middleMousePressed = false;
        }

        private void plot_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftMousePressed)
            {
                azimuth = lastAzimuth - (ptMouseClick.X - e.X) / 150;
                elevation = lastElevation + (ptMouseClick.Y - e.Y) / 150;
                UpdateProjection();
            }
            if (middleMousePressed)
            {
                // we want to be able to translate the camera
                CoordsCenter[0] += -e.X;
                CoordsCenter[1] += -e.Y;
                UpdatePointsCoordinates();
                UpdateProjection();
            }
        }

        private void UpdatePointsCoordinates()
        {
            List<double[]> Coordinates = new List<double[]>();

        }

        private new void OnMouseWheel(MouseEventArgs e)
        {
            if (!middleMousePressed)
            {
                // zoom
                Distance += -e.Delta / MouseSensitivity_Wheel;
            }
            else
            {
                // plot center z translation
                CoordsCenter[2] -= -e.Delta / MouseSensitivity_Wheel;
            }
        }
                
        #endregion
    }
}
