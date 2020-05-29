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
        List<List<double[]>> Coords = new List<List<double[]>>();
        List<PointF[]> AxesAnchors = new List<PointF[]>();

        // points used for the boundaries cube
        List<List<double[]>> BoundariesCorners = new List<List<double[]>>();
        List<PointF[]> BoundariesFrame = new List<PointF[]>();

        // Data points

        // MapPlot objects
        List<List<double[]>> MapBodies = new List<List<double[]>>();
        List<PointF[]> MapPoints = new List<PointF[]>();
            
        // Orrery objects
        List<List<double[]>> OrreryBodies = new List<List<double[]>>();        
        List<PointF[]> OrreryOrbits = new List<PointF[]>();

        private double focalLength = 900;
        private double distance = 6;
        private int smallDotSize = 3;
        private int mediumDotSize = 6;
        private int largeDotSize = 9;
        private double[] cameraPosition = new double[3];

        // Mouse 
        private bool leftMousePressed = false;
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
            AstroPlotHelpers.MouseWheelHandler.Add(this, OnMouseWheel);            
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
            SolidBrush axisAnchor = new SolidBrush(ForeColor);

            Pen AxisPen = new Pen(new SolidBrush(ForeColor));
            AxisPen.Width = 1;

            Pen BoundariesPen = new Pen(new SolidBrush(ForeColor));
            BoundariesPen.Width = 1;
            BoundariesPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;            

            Pen OrreryOrbitsPen = new Pen(new SolidBrush(Color.White));
            OrreryOrbitsPen.Width = 1;
            OrreryOrbitsPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            // center point            
            var center = new PointF(this.Width / 2, this.Height / 2);            
            
            Graphics g = this.CreateGraphics();

            // give some love to the renderint engine
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;            

            g.FillRectangle(backColor, new Rectangle(0, 0, this.Width, this.Height));

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
                        g.DrawLine(AxisPen, center, axisAnchorPoint);
                    }
                }
            }

            // boundaries
            if (BoundariesFrame != null)
            {
                if (BoundariesFrame.Count > 0)
                {
                    // bottom
                    g.DrawLine(BoundariesPen, BoundariesFrame[0][0], BoundariesFrame[0][1]);
                    g.DrawLine(BoundariesPen, BoundariesFrame[0][1], BoundariesFrame[0][5]);
                    g.DrawLine(BoundariesPen, BoundariesFrame[0][5], BoundariesFrame[0][3]);
                    g.DrawLine(BoundariesPen, BoundariesFrame[0][3], BoundariesFrame[0][0]);

                    // left
                    g.DrawLine(BoundariesPen, BoundariesFrame[0][0], BoundariesFrame[0][2]);
                    g.DrawLine(BoundariesPen, BoundariesFrame[0][2], BoundariesFrame[0][4]);
                    g.DrawLine(BoundariesPen, BoundariesFrame[0][4], BoundariesFrame[0][3]);                    

                    // right
                    g.DrawLine(BoundariesPen, BoundariesFrame[0][1], BoundariesFrame[0][6]);
                    g.DrawLine(BoundariesPen, BoundariesFrame[0][6], BoundariesFrame[0][7]);
                    g.DrawLine(BoundariesPen, BoundariesFrame[0][7], BoundariesFrame[0][5]);

                    // top
                    g.DrawLine(BoundariesPen, BoundariesFrame[0][2], BoundariesFrame[0][6]);
                    g.DrawLine(BoundariesPen, BoundariesFrame[0][4], BoundariesFrame[0][7]);
                }                
            }

                // map
                if (MapPoints != null)
            {
                for (int i = 0; i < MapPoints.Count; i++)
                {
                    foreach (PointF p in MapPoints[i])
                    {                        
                        g.FillEllipse(new SolidBrush(colors[i % colors.Length]), new RectangleF(p.X, p.Y, SmallDotSize, SmallDotSize));                        
                    }
                }
            }

            // orrery
            if (OrreryOrbits != null)
            {
                for (int i = 0; i < OrreryOrbits.Count; i++)
                {
                    var orreryCenter = new Point(this.Width / 2, this.Height / 2);

                    // draw a fake central star, just for fun - REALLY, IT'S JUST TEMPORARY!
                    g.FillEllipse(new SolidBrush(Color.Yellow), new RectangleF(orreryCenter.X - LargeDotSize / 2, orreryCenter.Y - LargeDotSize / 2, LargeDotSize, LargeDotSize));

                    // draw the orbit

                    if (OrreryOrbits[0].Length > 0)
                    for (int o = 0; o < OrreryOrbits[0].Length ; o++)
                    {
                        Point[] frameCorners = new Point[] {
                        new Point { X = (int)OrreryOrbits[1][o].X, Y = (int)OrreryOrbits[1][o].Y },
                        new Point { X = (int)OrreryOrbits[2][o].X, Y = (int)OrreryOrbits[2][o].Y },
                        new Point { X = (int)OrreryOrbits[3][o].X, Y = (int)OrreryOrbits[3][o].Y },
                        new Point { X = (int)OrreryOrbits[4][o].X, Y = (int)OrreryOrbits[4][o].Y }
                        };
                        g.DrawClosedCurve(OrreryOrbitsPen, frameCorners, (float)0.8, System.Drawing.Drawing2D.FillMode.Alternate);
                    }
                    
                    foreach (PointF p in OrreryOrbits[i])
                    {               
                        if (i == 0)
                            g.FillEllipse(new SolidBrush(colors[i % colors.Length]), new RectangleF(p.X - MediumDotSize / 2, p.Y - MediumDotSize / 2, MediumDotSize, MediumDotSize));                        
                        else g.FillEllipse(new SolidBrush(colors[i % colors.Length]), new RectangleF(p.X - MediumDotSize / 2, p.Y - MediumDotSize / 2, 3, 3));                            
                    }
                }
            }
        }

        #region Axes Widget
        private void AddAnchors(List<double[]> anchors)
        {
            List<double[]> _anchors = new List<double[]>(anchors);
            Coords.Add(_anchors);
            AxesAnchors.Add(AstroPlotHelpers.Projection.ProjectVector(Coords[Coords.Count - 1], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));
            UpdateProjection();
        }

        public void DrawAxesWidget(int length)
        {
            if (drawAxesWidget)
            {
                List<double[]> Coords = new List<double[]>();

                Coords.Add(new double[] { length * 0.5, 0.0, 0.0, 0 });
                Coords.Add(new double[] { 0.0, -(length * 0.5), 0.0, 1 });
                Coords.Add(new double[] { 0.0, 0.0, -(length * 0.5), 2 });

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
            BoundariesFrame.Add(AstroPlotHelpers.Projection.ProjectVector(BoundariesCorners[0], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));
            UpdateProjection();
        }

        public void DrawBoundariesWidget(double BoundariesRadius)
        {
            if (drawBoundariesWidget)
            {
                List<double[]> Corners = new List<double[]>();

                Corners.Add(new double[] { BoundariesRadius, BoundariesRadius, BoundariesRadius });
                Corners.Add(new double[] { BoundariesRadius, BoundariesRadius, -BoundariesRadius });
                Corners.Add(new double[] { BoundariesRadius, -BoundariesRadius, BoundariesRadius });
                Corners.Add(new double[] { -BoundariesRadius, BoundariesRadius, BoundariesRadius });
                Corners.Add(new double[] { -BoundariesRadius, -BoundariesRadius, BoundariesRadius });
                Corners.Add(new double[] { -BoundariesRadius, BoundariesRadius, -BoundariesRadius });
                Corners.Add(new double[] { BoundariesRadius, -BoundariesRadius, -BoundariesRadius });
                Corners.Add(new double[] { -BoundariesRadius, -BoundariesRadius, -BoundariesRadius });

                AddBoundaries(Corners);

                Corners.Clear();
            }
        }

        #endregion

        #region Map        
        public void AddPointsToMap(List<double[]> points)
        {
            List<double[]> _tmp = new List<double[]>(points);
            MapBodies.Add(_tmp);
            MapPoints.Add(AstroPlotHelpers.Projection.ProjectVector(MapBodies[MapBodies.Count - 1], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));                       
            UpdateProjection();
        }
        #endregion

        #region Orrery
        public void AddBodiesToOrrery(List<double[]> bodies)
        {            
            List<double[]> _orbits = new List<double[]>();

            for (int i = 0; i < bodies.Count; i++)
            {
                var radius = AstroPlotHelpers.Projection.FindOrbitalRadius(bodies[i][0], bodies[i][1]);
                var elevation = AstroPlotHelpers.Projection.FindOrbitalElevation(bodies[i][0], bodies[i][1]);
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
                OrreryOrbits.Add(AstroPlotHelpers.Projection.ProjectVector(OrreryBodies[i], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation));
            }
            
            UpdateProjection();
        }
        #endregion

        #region Functions

        private void UpdateProjection()
        {
            double x = distance * Math.Cos(elevation) * Math.Cos(azimuth);
            double y = distance * Math.Cos(elevation) * Math.Sin(azimuth);
            double z = distance * Math.Sin(elevation);
            cameraPosition = new double[3] { -y, z, -x };

            if (MapPoints == null)
                return;
            else
            {
                for (int i = 0; i < MapPoints.Count; i++)
                    MapPoints[i] = AstroPlotHelpers.Projection.ProjectVector(MapBodies[i], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation);                
            }

            if (OrreryOrbits == null)
                return;
            else
            {
                for (int i = 0; i < OrreryOrbits.Count; i++)
                    OrreryOrbits[i] = AstroPlotHelpers.Projection.ProjectVector(OrreryBodies[i], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation);
            }

            if (AxesAnchors == null)
                return;
            else
            {
                if (drawAxesWidget)
                {
                    for (int i = 0; i < AxesAnchors.Count; i++)
                    AxesAnchors[i] = AstroPlotHelpers.Projection.ProjectVector(Coords[i], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation);                    
                }
            }

            if (BoundariesCorners == null)
                return;
            else
            {
                if (drawBoundariesWidget)
                {
                    for (int i = 0; i < BoundariesFrame.Count; i++)
                        BoundariesFrame[i] = AstroPlotHelpers.Projection.ProjectVector(BoundariesCorners[i], this.Width, this.Height, focalLength, cameraPosition, azimuth, elevation);
                }
            }

            this.Invalidate();            
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
                azimuth = lastAzimuth - (ptMouseClick.X - e.X) / 150;
                elevation = lastElevation + (ptMouseClick.Y - e.Y) / 150;
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
            if (MapPoints != null)
                UpdateProjection();
        }

        private void OnMouseWheel(MouseEventArgs e)
        {
            Distance += -e.Delta / MouseSensitivity_Wheel;
        }
                
        #endregion
    }
}
