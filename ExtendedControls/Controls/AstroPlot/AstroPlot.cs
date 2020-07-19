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
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using BaseUtils;
using ExtendedControls.AstroPlot;

namespace ExtendedControls.Controls
{
    public partial class AstroPlot : Control
    {
        private ExtPictureBox plotCanvas;

        private HotSpotMap hotSpotMap = new HotSpotMap();
        private readonly System.Timers.Timer mouseIdleTimer = new System.Timers.Timer();

        // Initialize the control
        private void InitializeComponent()
        {
            this.plotCanvas = new ExtendedControls.ExtPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.plotCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // plotCanvas
            // 
            this.plotCanvas.BackColor = System.Drawing.Color.Black;
            this.plotCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotCanvas.Location = new System.Drawing.Point(0, 0);
            this.plotCanvas.Name = "plotCanvas";
            this.plotCanvas.Size = new System.Drawing.Size(400, 400);
            this.plotCanvas.TabIndex = 0;
            this.plotCanvas.Paint += this.PlotCanvas_Paint;
            this.plotCanvas.MouseDown += this.PlotCanvas_MouseDown;
            this.plotCanvas.MouseMove += this.PlotCanvas_MouseMove;
            this.plotCanvas.MouseUp += this.PlotCanvas_MouseUp;
            this.plotCanvas.Resize += this.PlotCanvas_Resize;
            this.plotCanvas.MouseEnter += PlotCanvas_MouseEnter;
            this.plotCanvas.MouseLeave += PlotCanvas_MouseLeave;
            // 
            // AstroPlot
            // 
            this.Controls.Add(this.plotCanvas);
            this.ForeColor = System.Drawing.Color.White;
            this.Size = new System.Drawing.Size(400, 400);
            ((System.ComponentModel.ISupportInitialize)(this.plotCanvas)).EndInit();
            this.ResumeLayout(false);

        }
        
        // Create needed lists for anchor points
        private readonly List<AnchorPoint> axesAnchors = new List<AnchorPoint>();
        private readonly List<AnchorPoint> planesAnchors = new List<AnchorPoint>();
        private readonly List<AnchorPoint> framesAnchors = new List<AnchorPoint>();
        private readonly List<AnchorPoint[]> gridsAnchors = new List<AnchorPoint[]>();

        // Create a list for plot objects
        private readonly List<PlotObject> plotObjects = new List<PlotObject>();

        // It create an additional list for hotspot map creation
        private List<Tuple<Object, PointF>> plotHotSpots = new List<Tuple<Object, PointF>>();

        // Define additional attributes for properties
        public class MaxValue : Attribute
        {
            public double Max;

            public MaxValue(double max)
            {
                Max = max;
            }
        }

        public class MinValue : Attribute
        {
            public double Min;

            public MinValue(double min)
            {
                Min = min;
            }
        }

        private double NormalizePropertyValue(string propertyName, double propertyValue)
        {
            var memberInfo = this.GetType().GetMember(propertyName);
            if (memberInfo.Length > 0)
            {
                var _attr = this.GetType().GetMember(propertyName)[0];
                var attributes = Attribute.GetCustomAttributes(_attr);
                if (attributes.Length > 0)
                {
                    var minValueAttribute = (MinValue)attributes[0];
                    var maxValueAttribute = (MaxValue)attributes[1];
                    if (propertyValue > minValueAttribute.Min) { propertyValue = minValueAttribute.Min; }
                    if (propertyValue < maxValueAttribute.Max) { propertyValue = maxValueAttribute.Max; }
                }
            }
            return propertyValue;
        }

        #region Properties
                
        // Projection
        internal double[] cameraPosition = new double[3];
        internal double[] centerCoordinates = new double[3];
        internal double[] lastCenterCoordinates = new double[3];

        internal double[] dragCoords = new double[2];

        public double[] GetCenterCoordinates()
        {
            return centerCoordinates;
        }

        public void SetCenterCoordinates(double[] value)
        {
            centerCoordinates = value; SetFrameAnchors(FramesLength); SetAxesAnchors(AxesLength); SetGridAnchors(GridCount, GridUnit);
        }

        public void SetCenterCoordinates(double x, double y, double z)
        {
            var newcenter = new double[] { centerCoordinates[0] = x, centerCoordinates[1] = y, centerCoordinates[2] = z };
            SetCenterCoordinates(newcenter);
        }

        private double distance;
        [Description("Set the distance at which the camera stands from the plot")]
        public double Distance
        {
            get => distance; set { distance = (value >= 0.1) ? distance = value : distance; UpdateProjection(); }
        }

        internal double lastAzimuth, azimuth;
        [Description("Horizontal position of the camera expressed as an angular distance")]
        public double Azimuth
        {
            get => azimuth;

            set
            {
                if (Projection == PlotProjection.Free)
                {
                    azimuth = value;
                }
                else
                {
                    azimuth = 0;
                }

                UpdateProjection();
            }
        }

        internal double lastElevation, elevation;
        [Description("Vertical position of the camera expressed as an angular distance")]
        public double Elevation
        {
            get => elevation;

            set
            {
                if (Projection == PlotProjection.Free)
                {
                    elevation = value;
                }
                else
                {
                    if (View == PlotPlainView.Perspective)
                    {
                        elevation = -0.4;
                    }
                    else
                    {
                        elevation = -1.59;
                    }
                }

                UpdateProjection();
            }
        }

        private double focalLength;
        [Description("Focal length of the camera")]
        public new double Focus
        {
            get => focalLength; set { focalLength = value; UpdateProjection(); }
        }

        // Look'n'Feel
        public enum PlotProjection { Free = 0, Fixed = 1 };
        private PlotProjection plotProjection;
        public PlotProjection Projection { get => plotProjection; set => plotProjection = value; }

        public enum PlotPlainView { Perspective = 0, Top = 1}
        private PlotPlainView plotView;
        public PlotPlainView View { get => plotView; set => plotView = value; }

        public int SmallDotSize { get; set; }
        public int MediumDotSize { get; set; }
        public int LargeDotSize { get; set; }
        public Color VisitedColor { get; set; }
        public Color UnVisitedColor { get; set; }
        public Color CurrentColor { get; set; }
        
        // Mouse
        private bool leftMousePressed = false;
        private bool rightMousePressed = false;
        private bool middleMousePressed = false;

        internal PointF ptMouseClick;
        internal Point mousePosition;

        [MinValue(1.0), MaxValue(100.0)]
        private double mouseRotationResistance;
        [Description("Set the resistance of the mouse rotation (1.0 to 100.0)")]
        public double MouseRotation_Resistance
        {
            get => mouseRotationResistance;

            set
            {
                mouseRotationResistance = NormalizePropertyValue(MouseRotation_Resistance.ToString(), value);
            }
        }

        [MinValue(1.0), MaxValue(100)]
        private double mouseRotationMultiply;
        [Description("Set the multiplier of the mouse rotation (1.0 to 100.0)")]
        public double MouseRotation_Multiply
        {
            get => mouseRotationMultiply;

            set
            {
                mouseRotationMultiply = NormalizePropertyValue(MouseRotation_Multiply.ToString(), value);
            }
        }

        [MinValue(1.0), MaxValue(100)]
        private double mouseWheelResistance;
        [Description("Set the resistance of the mouse wheel (1.0 to 100.0)")]
        public double MouseWheel_Resistance
        {
            get => mouseWheelResistance; set
            {
                mouseWheelResistance = NormalizePropertyValue(MouseWheel_Resistance.ToString(), value);
            }
        }

        [MinValue(1), MaxValue(10)]
        private double mouseWheelMultiply;
        [Description("Set the multiply of the mouse wheel (1.0 to 10.0)")]
        public double MouseWheel_Multiply
        {
            get => mouseWheelMultiply;

            set
            {
                mouseWheelMultiply = NormalizePropertyValue(MouseWheel_Multiply.ToString(), value);
            }
        }

        [MinValue(1.0), MaxValue(100)]
        private double mouseDragResistance;
        [Description("Set the resistance of the mouse drag (0.1 to 1.0)")]
        public double MouseDrag_Resistance
        {
            get => mouseDragResistance;
            
            set
            {
                mouseDragResistance = NormalizePropertyValue(MouseDrag_Resistance.ToString(), value);
            }
        }

        [MinValue(1), MaxValue(10)]
        private double mouseDragMultiply;
        [Description("Set the multiply of the mouse drag (1.0 to 10.0)")]
        public double MouseDrag_Multiply
        {
            get => mouseDragMultiply;
            
            set
            {
                mouseDragMultiply = NormalizePropertyValue(MouseDrag_Multiply.ToString(), value);
            }
        }

        [MinValue(5), MaxValue(30)]
        private double hotspotSize;

        [Description("Define the size of the hotspot area for the map points")]
        public double HotSpotSize
        {
            get => hotspotSize;

            set
            {
                hotspotSize = NormalizePropertyValue(HotSpotSize.ToString(), value); UpdateProjection();
            }
        }
                
        // Axes Widget
        private bool showAxesWidget;
        public bool ShowAxesWidget
        {
            get => showAxesWidget; set { showAxesWidget = value; UpdateProjection(); }
        }

        private int axesThickness;
        [Description("Set the thickness of each axis in the axes widget")]
        public int AxesThickness
        {
            get => axesThickness; set { axesThickness = value; UpdateProjection(); }
        }

        private int axesLength;
        [Description("Set the length of each axis in the axes widget")]
        public int AxesLength
        {
            get => axesLength; set { axesLength = value; SetAxesAnchors(AxesLength); UpdateProjection(); }
        }

        // Grid Widget
        private bool showGridWidget;
        public bool ShowGridWidget
        {
            get => showGridWidget; set { showGridWidget = value; UpdateProjection(); }
        }

        private int gridUnit;
        public int GridUnit
        {
            get => gridUnit; set { gridUnit = value; UpdateProjection(); }
        }

        private int gridCount;
        public int GridCount
        {
            get => gridCount; set { gridCount = value; UpdateProjection(); }
        }

        private Color gridColor;
        public Color GridColor
        {
            get => gridColor; set { gridColor = value; UpdateProjection(); }
        }
        
        // Frame Widget
        private bool showFrameWidget;
        public bool ShowFrameWidget
        {
            get => showFrameWidget; set { showFrameWidget = value; UpdateProjection(); }
        }

        public enum Shape { Cube = 0, Planes = 1 };

        public Shape FrameShape;
        
        public Shape GetFrameShape()
        {
            return FrameShape;
        }
        public void SetFrameShape(Shape value)
        {
            ShowFrameWidget = false;
            FrameShape = value;
            ShowFrameWidget = true;
        }

        private double framesLength;
        [Description("Set the boundaries frame radius")]
        public double FramesLength
        {
            get => framesLength; set { framesLength = value; SetFrameAnchors(FramesLength); UpdateProjection(); }
        }

        private int framesThickness;
        [Description("Set the boundaries frame thickness")]
        public int FramesThickness
        {
            get => framesThickness; set { framesThickness = value; UpdateProjection(); }
        }

        // Output
        protected bool isObjectSelected;
        public bool IsObjectSelected
        {
            get => isObjectSelected; set { isObjectSelected = value; }
        }

        public delegate void SystemSelected();
        public event SystemSelected OnSystemSelected;

        protected string selectedObjectName;
        public string SelectedObjectName
        {
            get => selectedObjectName; private set { selectedObjectName = value; { OnSystemSelected(); } }
        }

        protected Point selectedObjectPoint;
        public Point SelectedObjectLocation
        {
            get => selectedObjectPoint; private set { selectedObjectPoint = value; }
        }                
        #endregion

        private const int WS_EX_COMPOSITED = 0x02000000;
        private const int WS_EX_TRANSPARENT = 0x20;
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= WS_EX_COMPOSITED;    // Turn on double buffering
                cp.ExStyle |= WS_EX_TRANSPARENT;    // Turn on transparencies
                return cp;
            }
        }

        public AstroPlot()
        {
            InitializeComponent();

            DoubleBuffered = true;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            mouseIdleTimer.AutoReset = false;
            mouseIdleTimer.Interval = 300;
            mouseIdleTimer.Elapsed += mouseIdleTimer_Elapsed;

            MouseRotation_Resistance = 75;
            MouseRotation_Multiply = 1;
            MouseDrag_Resistance = 12;
            MouseDrag_Multiply = 20;
            MouseWheel_Resistance = 2;
            MouseWheel_Multiply = 7;
            ShowAxesWidget = true;
            AxesLength = 10;
            AxesThickness = 1;
            ShowFrameWidget = false;
            FrameShape = Shape.Cube;
            FramesLength = 20;
            FramesThickness = 1;
            ShowGridWidget = true;
            GridCount = 5;
            GridUnit = 10;
            Distance = 300;
            Focus = 1000;
            SmallDotSize = 5;
            MediumDotSize = 10;
            LargeDotSize = 15;
            VisitedColor = Color.Aqua;
            UnVisitedColor = Color.Yellow;
            CurrentColor = Color.Red;
            GridColor = Color.FromArgb(80, 30, 190, 240);

            selectedObjectName = "";
            isObjectSelected = false;

            hotSpotMap.OnHotSpot += HotSpotMap_OnHotSpot;
        }

        // Changes SelectedObject Name and Loction when OnHotSpot event from HotSpotMap is triggered
        private void HotSpotMap_OnHotSpot(Object o,PointF p)
        {
            SelectedObjectName = (string)o;
            SelectedObjectLocation = new Point((int)p.X,(int)p.Y);
            Debug.WriteLine(SelectedObjectName + SelectedObjectLocation);
        }

        // Set a new center of the plot
        public void SetCenterOfMap(double[] coords)
        {
            if (coords != null)
            {
                centerCoordinates = new double[] { coords[0], coords[1], coords[2] };
                SetAxesAnchors(AxesLength);
                SetFrameAnchors(FramesLength);
                if (View == PlotPlainView.Perspective)
                    SetGridAnchors((int)GridCount / 3, GridUnit);
                else
                    SetGridAnchors(GridCount, GridUnit);
            }
        }
               
        
        // Add given list to the object list to be plotted
        public void AddSystemsToMap(List<object[]> plotObjects)
        {
            for (int i = 0; i < plotObjects.Count; i++)
            {
                this.plotObjects.Add(new PlotObject
                {
                    Name = plotObjects[i][0].ToString(),
                    X = (double)plotObjects[i][1],
                    Y = (double)plotObjects[i][2],
                    Z = (double)plotObjects[i][3],
                    IsVisited = (bool)plotObjects[i][4],
                    IsWaypoint = (bool)plotObjects[i][5],
                    IsCurrent = (bool)plotObjects[i][6],
                    Coords = new PointF(0, 0)
                });

                plotHotSpots.Add(new Tuple<Object, PointF>(plotObjects[i][0].ToString(), new PointF(0, 0)));
            }
            
            UpdateProjection();
        }

        public void Clear()
        {
            Invalidate();
            plotObjects.Clear();
            plotHotSpots.Clear();
        }

        private void CenterTo_Click(object sender, EventArgs e)
        {
            var selected = sender as ToolStripMenuItem;
            SetCenterCoordinates((double[])selected.Tag);
        }

        /// <summary>
        /// Update coordinates according to user interaction
        /// </summary>
        private void UpdateProjection()
        {
            if (Projection == PlotProjection.Fixed)
            {
                if (View == PlotPlainView.Perspective)
                {
                    elevation = -0.4;
                }
                else
                {
                    elevation = -1.59;
                }
            }

            var x = (distance * Math.Cos(elevation) * Math.Cos(azimuth));
            var y = (distance * Math.Cos(elevation) * Math.Sin(azimuth));
            var z = (distance * Math.Sin(elevation));

            cameraPosition = new double[3] { -y, z, -x };

            if (axesAnchors != null) // we calculate that even if the axes widget is hidden, because its center coordinates are used for other calculations
            {
                ExtendedControls.AstroPlot.View.Update(axesAnchors, Width, Height, focalLength, cameraPosition, azimuth, elevation, centerCoordinates);
            }
                        
            if (ShowFrameWidget && framesAnchors != null)
            {
                ExtendedControls.AstroPlot.View.Update(framesAnchors, Width, Height, focalLength, cameraPosition, azimuth, elevation, centerCoordinates);
                ExtendedControls.AstroPlot.View.Update(planesAnchors, Width, Height, focalLength, cameraPosition, azimuth, elevation, centerCoordinates);
            }

            if (ShowGridWidget && gridsAnchors != null)
            {
                ExtendedControls.AstroPlot.View.Update(gridsAnchors, Width, Height, focalLength, cameraPosition, azimuth, elevation, centerCoordinates);
            }

            if (plotObjects != null)
            {
                plotHotSpots.Clear();
                ExtendedControls.AstroPlot.View.Update(plotObjects, plotHotSpots, Width, Height, focalLength, cameraPosition, azimuth, elevation, centerCoordinates);
                hotSpotMap.CalculateHotSpotRegions(plotHotSpots, (int)HotSpotSize);
            }

            Invalidate();
        }

        private void PlotCanvas_Resize(object sender, EventArgs e)
        {
            UpdateProjection();
        }

        /// <summary>
        /// Paint the plot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlotCanvas_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            // give some love to the rendering engine
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.CompositingMode = CompositingMode.SourceOver;

            /// axes
            using (var AxisPen = new Pen(new SolidBrush(ForeColor))
            {
                Width = 2,
                DashStyle = DashStyle.Solid
            })
            {
                if (ShowAxesWidget && axesAnchors != null)
                {
                    for (int i = 0; i < axesAnchors.Count; i++)
                    {
                        if (i == 1)
                        {
                            AxisPen.Color = Color.Red;
                            g.DrawLine(AxisPen, axesAnchors[0].Coords, axesAnchors[1].Coords);
                        }
                        if (i == 2)
                        {
                            AxisPen.Color = Color.Green;
                            g.DrawLine(AxisPen, axesAnchors[0].Coords, axesAnchors[2].Coords);
                        }

                        if (i == 3)
                        {
                            AxisPen.Color = Color.Blue;
                            g.DrawLine(AxisPen, axesAnchors[0].Coords, axesAnchors[3].Coords);
                        }
                    }
                }
            }

            /// Grid
            if (ShowGridWidget && Projection == PlotProjection.Fixed)
            {
                using (var GridPen = new Pen(new SolidBrush(Color.FromArgb(80, 40, 160, 220))) { Width = 1 })
                {
                    for (int i = 0; i < gridsAnchors.Count; i++)
                    {
                        g.DrawLine(GridPen, gridsAnchors[i][0].Coords, gridsAnchors[i][1].Coords);
                    }
                }
            }

            /// Frame
            if (ShowFrameWidget && Projection == PlotProjection.Free)
            {
                using (var FramePen = new Pen(new SolidBrush(ForeColor))
                {
                    Width = 1,
                    DashStyle = DashStyle.Solid
                })
                {
                    /// Cubical frame
                    if (GetFrameShape() == Shape.Cube && framesAnchors.Count > 0)
                    {
                        /// bottom
                        g.DrawLine(FramePen, framesAnchors[0].Coords, framesAnchors[1].Coords);
                        g.DrawLine(FramePen, framesAnchors[1].Coords, framesAnchors[5].Coords);
                        g.DrawLine(FramePen, framesAnchors[5].Coords, framesAnchors[3].Coords);
                        g.DrawLine(FramePen, framesAnchors[3].Coords, framesAnchors[0].Coords);

                        /// left
                        g.DrawLine(FramePen, framesAnchors[0].Coords, framesAnchors[2].Coords);
                        g.DrawLine(FramePen, framesAnchors[2].Coords, framesAnchors[4].Coords);
                        g.DrawLine(FramePen, framesAnchors[4].Coords, framesAnchors[3].Coords);

                        /// right
                        g.DrawLine(FramePen, framesAnchors[1].Coords, framesAnchors[6].Coords);
                        g.DrawLine(FramePen, framesAnchors[6].Coords, framesAnchors[7].Coords);
                        g.DrawLine(FramePen, framesAnchors[7].Coords, framesAnchors[5].Coords);

                        /// top
                        g.DrawLine(FramePen, framesAnchors[2].Coords, framesAnchors[6].Coords);
                        g.DrawLine(FramePen, framesAnchors[4].Coords, framesAnchors[7].Coords);
                    }

                    /// Spherical frame
                    if (GetFrameShape() == Shape.Planes && framesAnchors.Count > 0)
                    {
                        var horizontalPlane = new PointF[] { planesAnchors[0].Coords, planesAnchors[2].Coords, planesAnchors[1].Coords, planesAnchors[3].Coords };
                        var verticalPlane = new PointF[] { planesAnchors[0].Coords, planesAnchors[4].Coords, planesAnchors[1].Coords, planesAnchors[5].Coords };
                        var longitudinalPlane = new PointF[] { planesAnchors[2].Coords, planesAnchors[4].Coords, planesAnchors[3].Coords, planesAnchors[5].Coords };

                        FramePen.Color = Color.Red;
                        g.DrawClosedCurve(FramePen, horizontalPlane, (float)0.85, FillMode.Winding);
                        FramePen.Color = Color.Green;
                        g.DrawClosedCurve(FramePen, verticalPlane, (float)0.85, FillMode.Winding);
                        FramePen.Color = Color.Blue;
                        g.DrawClosedCurve(FramePen, longitudinalPlane, (float)0.85, FillMode.Winding);
                    }
                }
            }

            if (plotObjects != null)
            {
                for (int i = plotObjects.Count - 1; i >= 0; i--)
                {
                    var FillColor = plotObjects[i].IsVisited ? plotObjects[i].IsCurrent ? CurrentColor : VisitedColor : UnVisitedColor;

                    using (var objectBrush = new SolidBrush(FillColor))
                    {
                        g.FillEllipse(objectBrush, new RectangleF(
                            plotObjects[i].Coords.X - (SmallDotSize / 2),
                            plotObjects[i].Coords.Y - (SmallDotSize / 2),
                            SmallDotSize,
                            SmallDotSize));
                    }

                    if (plotObjects[i].IsWaypoint && i != plotObjects.Count - 1)
                    {
                        using (var TravelMapPen = new Pen(new SolidBrush(ForeColor))
                        {
                            Width = 1,
                            DashStyle = DashStyle.Solid
                        })
                        {
                            g.DrawLine(TravelMapPen, plotObjects[i].Coords, plotObjects[i + 1].Coords);
                        }
                    }
                }

            }
            base.OnPaint(e);
        }

        /// <summary>
        /// Stop timer when mouse leave the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlotCanvas_MouseLeave(object sender, EventArgs e)
        {
            mouseIdleTimer.Stop();
        }

        /// <summary>
        /// Start the timer when mouse enter the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlotCanvas_MouseEnter(object sender, EventArgs e)
        {
            mouseIdleTimer.Start();
        }

        /// <summary>
        /// Do somethign when the timer tick passed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseIdleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            hotSpotMap.CheckForMouseInHotSpot(mousePosition);
        }

        /// <summary>
        /// Actiosn taken when mouse moves
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlotCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            mouseIdleTimer.Stop();
            
            mousePosition = e.Location;

            if (leftMousePressed)
            {
                if (Projection == PlotProjection.Free)
                {
                    azimuth = lastAzimuth - (ptMouseClick.X - e.X) * (MouseRotation_Multiply * 0.3) / MouseRotation_Resistance;
                    elevation = lastElevation + (ptMouseClick.Y - e.Y) * (MouseRotation_Multiply * 0.2) / MouseRotation_Resistance;
                }
                else if (Projection == PlotProjection.Fixed)
                {
                    azimuth = lastAzimuth - ((ptMouseClick.X - e.X) * (MouseRotation_Multiply * 0.3)) / MouseRotation_Resistance;
                }
            }

            if (middleMousePressed)
            {
                centerCoordinates[0] = lastCenterCoordinates[0] + (ptMouseClick.X - e.X) * MouseDrag_Multiply / MouseDrag_Resistance;                
                centerCoordinates[2] = lastCenterCoordinates[2] - (ptMouseClick.Y - e.Y) * MouseDrag_Multiply / MouseDrag_Resistance;
                SetCenterCoordinates(GetCenterCoordinates());
            }

            UpdateProjection();

            mouseIdleTimer.Start();
        }

        /// <summary>
        /// When mouse is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlotCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            ptMouseClick = new PointF(e.X, e.Y);
            lastAzimuth = azimuth;
            lastElevation = elevation;
            lastCenterCoordinates = centerCoordinates;

            if (e.Button == MouseButtons.Left)
            {
                // rotate
                leftMousePressed = true;
            }
            if (e.Button == MouseButtons.Middle)
            {
                // drag
                middleMousePressed = true;
            }
            if (e.Button == MouseButtons.Right)
            {
                rightMousePressed = true;
            }
        }

        /// <summary>
        /// When mouse is released
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlotCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                leftMousePressed = false;
                // refresh the hotspot region map
                hotSpotMap.CalculateHotSpotRegions(plotHotSpots, (int)HotSpotSize);
            }

            if (e.Button == MouseButtons.Middle)
            {
                middleMousePressed = false;
                // refresh the hotspot region map
                hotSpotMap.CalculateHotSpotRegions(plotHotSpots, (int)HotSpotSize);
            }

            if (e.Button == MouseButtons.Right)
                rightMousePressed = false;
        }
   
        /// <summary>
        /// On mouse wheel
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (!middleMousePressed)
            {
                // zoom
                Distance += (-e.Delta * MouseWheel_Multiply) / MouseWheel_Resistance;
            }
            else
            {
                centerCoordinates[1] = lastCenterCoordinates[1] - (e.Delta * MouseWheel_Multiply) / MouseWheel_Resistance;
                SetCenterCoordinates(GetCenterCoordinates());
            }
        }
    }
}
