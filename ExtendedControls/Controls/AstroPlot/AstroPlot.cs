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
        private IContainer components;

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
            this.plotCanvas.MouseLeave += this.PlotCanvas_MouseLeave;
            this.plotCanvas.MouseMove += this.PlotCanvas_MouseMove;
            this.plotCanvas.MouseUp += this.PlotCanvas_MouseUp;
            this.plotCanvas.Resize += this.PlotCanvas_Resize;
            // 
            // AstroPlot
            // 
            this.Controls.Add(this.plotCanvas);
            this.ForeColor = System.Drawing.Color.White;
            this.Size = new System.Drawing.Size(400, 400);
            ((System.ComponentModel.ISupportInitialize)(this.plotCanvas)).EndInit();
            this.ResumeLayout(false);

        }

        private readonly List<AnchorPoint> _axes = new List<AnchorPoint>();
        private readonly List<AnchorPoint> _planes = new List<AnchorPoint>();
        private readonly List<AnchorPoint> _frames = new List<AnchorPoint>();
        private readonly List<AnchorPoint[]> _grids = new List<AnchorPoint[]>();
        private readonly List<PlotObject> _plotObjects = new List<PlotObject>();

        // Values normalization
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

        /// <summary>
        /// Properties
        /// </summary>

        // Projection
        internal double[] cameraPosition = new double[3];
        internal double[] centerCoordinates = new double[3];
        internal double[] lastCenterCoordinates = new double[3];

        internal double lateralDrag;
        internal double longitudinalDrag;
        internal double[] dragCoords = new double[2];

        public double[] GetCenterCoordinates()
        {
            return centerCoordinates;
        }

        public void SetCenterCoordinates(double[] value)
        { centerCoordinates = value; SetFrameAnchors(FramesLength); SetAxesAnchors(AxesLength); SetGridAnchors(GridCount, GridUnit); }

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
                if (PlotType == PlotProjection.Perspective)
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
                if (PlotType == PlotProjection.Perspective)
                {
                    elevation = value;
                }
                else
                {
                    elevation = -1.59;
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
        public enum PlotProjection { Plain = 0, Perspective = 1 };

        private PlotProjection plotType;
        public PlotProjection PlotType { get => plotType; set => plotType = value; }

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
        internal Point lastMousePosition;

        [MinValue(1.0), MaxValue(100.0)]
        private double mouseRotationResistance;
        [Description("Set the resistance of the mouse rotation (0.1 to 1.0)")]
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
        [Description("Set the resistance of the mouse wheel (0.1 to 1.0)")]
        public double MouseWheel_Resistance
        {
            get => mouseWheelResistance; set
            {
                mouseWheelResistance = NormalizePropertyValue(MouseWheel_Resistance.ToString(), value);
            }
        }

        [MinValue(1), MaxValue(10)]
        private double mouseWheelMultiply;
        [Description("Set the multiply of the mouse wheel (1.0 to 100.0)")]
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

        [MinValue(5), MaxValue(50)]
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

        protected string selectedObjectName;
        public string SelectedObjectName
        {
            get => selectedObjectName; private set { selectedObjectName = value; }
        }

        protected Point selectedObjectPoint;
        public Point SelectedObjectPoint
        {
            get => selectedObjectPoint; private set { selectedObjectPoint = value; }
        }

        protected double[] selectedObjectCoords;
        
        public double[] SelectedObjectCoords
        {
            get => selectedObjectCoords; private set { selectedObjectCoords = value; }
        }

        #endregion

        // Timer
        private readonly System.Timers.Timer _mouseIdleTimer = new System.Timers.Timer(); //add _mouseIdleTimer.Dispose(); to the Dispose method on another file.

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

        protected override void OnHandleCreated(EventArgs e)
        {
            PlotType = PlotProjection.Perspective;
            MouseRotation_Resistance = 75;
            MouseRotation_Multiply = 1;
            MouseDrag_Resistance = 12;
            MouseDrag_Multiply = 20;
            MouseWheel_Resistance = 2;
            MouseWheel_Multiply = 7;
            ShowAxesWidget = true;
            AxesLength = 10;
            AxesThickness = 1;
            ShowFrameWidget = true;
            FrameShape = Shape.Cube;
            FramesLength = 20;
            FramesThickness = 1;
            ShowGridWidget = true;
            GridCount = 5;
            GridUnit = 10;
            Distance = 300;
            Focus = 1000;
            Azimuth = -0.4;
            Elevation = -0.3;
            SmallDotSize = 5;
            MediumDotSize = 10;
            LargeDotSize = 15;
            VisitedColor = Color.Aqua;
            UnVisitedColor = Color.Yellow;
            CurrentColor = Color.Red;
            GridColor = Color.FromArgb(80, 30, 190, 240);
            base.OnHandleCreated(e);
        }

        public AstroPlot()
        {
            InitializeComponent();

            DoubleBuffered = true;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ContainerControl, true);

            selectedObjectName = "";
            isObjectSelected = false;

            _mouseIdleTimer.AutoReset = false;
            _mouseIdleTimer.Interval = 100;
            _mouseIdleTimer.Elapsed += MouseIdleTimer_Elapsed;
        }

        public void SetCenterOfMap(double[] coords)
        {
            if (coords != null)
            {
                centerCoordinates = new double[] { coords[0], coords[1], coords[2] };
                SetAxesAnchors(AxesLength);
                SetFrameAnchors(FramesLength);
                SetGridAnchors(GridCount, GridUnit);
            }
        }
                
        public void AddSystemsToMap(List<object[]> plotObjects)
        {
            for (int i = 0; i < plotObjects.Count; i++)
            {
                _plotObjects.Add(new PlotObject
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
            }
            
            UpdateProjection();
        }

        public void Clear()
        {
            Invalidate();
            _plotObjects.Clear();
        }

        private void CenterTo_Click(object sender, EventArgs e)
        {
            var selected = sender as ToolStripMenuItem;
            SetCenterCoordinates((double[])selected.Tag);
        }

        private void UpdateProjection()
        {        
            var x = (distance * Math.Cos(elevation) * Math.Cos(azimuth));
            var y = (distance * Math.Cos(elevation) * Math.Sin(azimuth));
            var z = (distance * Math.Sin(elevation));

            cameraPosition = new double[3] { -y, z, -x };

            if (_axes != null) // we calculate that even if the axes widget is hidden, because its center coordinates are used for other calculations
            {
                ExtendedControls.AstroPlot.View.Update(_axes, Width, Height, focalLength, cameraPosition, azimuth, elevation, centerCoordinates);
            }
                        
            if (ShowFrameWidget && _frames != null)
            {
                ExtendedControls.AstroPlot.View.Update(_frames, Width, Height, focalLength, cameraPosition, azimuth, elevation, centerCoordinates);
                ExtendedControls.AstroPlot.View.Update(_planes, Width, Height, focalLength, cameraPosition, azimuth, elevation, centerCoordinates);
            }

            if (ShowGridWidget && _grids != null)
            {
                ExtendedControls.AstroPlot.View.Update(_grids, Width, Height, focalLength, cameraPosition, azimuth, elevation, centerCoordinates);
            }

            if (_plotObjects != null)
            {
                ExtendedControls.AstroPlot.View.Update(_plotObjects, Width, Height, focalLength, cameraPosition, azimuth, elevation, centerCoordinates);                
            }

            Invalidate();
        }

        private void PlotCanvas_Resize(object sender, EventArgs e)
        {
            UpdateProjection();
        }

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
                if (ShowAxesWidget && _axes != null)
                {
                    for (int i = 0; i < _axes.Count; i++)
                    {
                        if (i == 1)
                        {
                            AxisPen.Color = Color.Red;
                            g.DrawLine(AxisPen, _axes[0].Coords, _axes[1].Coords);
                        }
                        if (i == 2)
                        {
                            AxisPen.Color = Color.Green;
                            g.DrawLine(AxisPen, _axes[0].Coords, _axes[2].Coords);
                        }

                        if (i == 3)
                        {
                            AxisPen.Color = Color.Blue;
                            g.DrawLine(AxisPen, _axes[0].Coords, _axes[3].Coords);
                        }
                    }
                }
            }

            /// Grid
            if (ShowGridWidget && PlotType == PlotProjection.Plain)
            {
                using (var GridPen = new Pen(new SolidBrush(Color.FromArgb(80, 40, 160, 220))) { Width = 1 })
                {
                    for (int i = 0; i < _grids.Count; i++)
                    {
                        g.DrawLine(GridPen, _grids[i][0].Coords, _grids[i][1].Coords);
                    }
                }
            }

            /// Frame
            if (ShowFrameWidget && PlotType == PlotProjection.Perspective)
            {
                using (var FramePen = new Pen(new SolidBrush(ForeColor))
                {
                    Width = 1,
                    DashStyle = DashStyle.Solid
                })
                {
                    /// Cubical frame
                    if (GetFrameShape() == Shape.Cube && _frames.Count > 0)
                    {
                        /// bottom
                        g.DrawLine(FramePen, _frames[0].Coords, _frames[1].Coords);
                        g.DrawLine(FramePen, _frames[1].Coords, _frames[5].Coords);
                        g.DrawLine(FramePen, _frames[5].Coords, _frames[3].Coords);
                        g.DrawLine(FramePen, _frames[3].Coords, _frames[0].Coords);

                        /// left
                        g.DrawLine(FramePen, _frames[0].Coords, _frames[2].Coords);
                        g.DrawLine(FramePen, _frames[2].Coords, _frames[4].Coords);
                        g.DrawLine(FramePen, _frames[4].Coords, _frames[3].Coords);

                        /// right
                        g.DrawLine(FramePen, _frames[1].Coords, _frames[6].Coords);
                        g.DrawLine(FramePen, _frames[6].Coords, _frames[7].Coords);
                        g.DrawLine(FramePen, _frames[7].Coords, _frames[5].Coords);

                        /// top
                        g.DrawLine(FramePen, _frames[2].Coords, _frames[6].Coords);
                        g.DrawLine(FramePen, _frames[4].Coords, _frames[7].Coords);
                    }

                    /// Spherical frame
                    if (GetFrameShape() == Shape.Planes && _frames.Count > 0)
                    {
                        var horizontalPlane = new PointF[] { _planes[0].Coords, _planes[2].Coords, _planes[1].Coords, _planes[3].Coords };
                        var verticalPlane = new PointF[] { _planes[0].Coords, _planes[4].Coords, _planes[1].Coords, _planes[5].Coords };
                        var longitudinalPlane = new PointF[] { _planes[2].Coords, _planes[4].Coords, _planes[3].Coords, _planes[5].Coords };

                        FramePen.Color = Color.Red;
                        g.DrawClosedCurve(FramePen, horizontalPlane, (float)0.85, FillMode.Winding);
                        FramePen.Color = Color.Green;
                        g.DrawClosedCurve(FramePen, verticalPlane, (float)0.85, FillMode.Winding);
                        FramePen.Color = Color.Blue;
                        g.DrawClosedCurve(FramePen, longitudinalPlane, (float)0.85, FillMode.Winding);
                    }
                }
            }

            if (_plotObjects != null)
            {
                for (int i = _plotObjects.Count - 1; i >= 0; i--)
                {
                    var FillColor = _plotObjects[i].IsVisited ? _plotObjects[i].IsCurrent ? CurrentColor : VisitedColor : UnVisitedColor;

                    using (var objectBrush = new SolidBrush(FillColor))
                    {
                        g.FillEllipse(objectBrush, new RectangleF(
                            _plotObjects[i].Coords.X - (SmallDotSize / 2),
                            _plotObjects[i].Coords.Y - (SmallDotSize / 2),
                            SmallDotSize,
                            SmallDotSize));
                    }

                    if (_plotObjects[i].IsWaypoint && i != _plotObjects.Count - 1)
                    {
                        using (var TravelMapPen = new Pen(new SolidBrush(ForeColor))
                        {
                            Width = 1,
                            DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
                        })
                        {
                            g.DrawLine(TravelMapPen, _plotObjects[i].Coords, _plotObjects[i + 1].Coords);
                        }
                    }
                }

            }
            base.OnPaint(e);
        }
                
        private void PlotCanvas_MouseLeave(object sender, EventArgs e)
        {
            _mouseIdleTimer.Stop();
        }

        private void PlotCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            _mouseIdleTimer.Stop();

            if (leftMousePressed)
            {
                if (PlotType == PlotProjection.Perspective)
                {
                    azimuth = lastAzimuth - (ptMouseClick.X - e.X) * (MouseRotation_Multiply * 0.3) / MouseRotation_Resistance;
                    elevation = lastElevation + (ptMouseClick.Y - e.Y) * (MouseRotation_Multiply * 0.2) / MouseRotation_Resistance;
                }
                else if (PlotType == PlotProjection.Plain)
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

            _mouseIdleTimer.Start();
        }

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

        private void PlotCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                leftMousePressed = false;
            if (e.Button == MouseButtons.Middle)
                middleMousePressed = false;
            if (e.Button == MouseButtons.Right)
                rightMousePressed = false;
        }

        private void MouseIdleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var hs = HotSpotSize;

            var labelPosition = new Point();

            string text = null;

            if (_plotObjects != null)
            {
                for (int i = 0; i < _plotObjects.Count; i++)
                {
                    if (mousePosition.X > (_plotObjects[i].Coords.X - hs) && mousePosition.X < (_plotObjects[i].Coords.X + hs) &&
                        mousePosition.Y > (_plotObjects[i].Coords.Y - hs) && mousePosition.Y > (_plotObjects[i].Coords.Y - hs))
                    {
                        text = _plotObjects[i].Name;
                        labelPosition = new Point((int)_plotObjects[i].Coords.X - (SmallDotSize * 4), (int)_plotObjects[i].Coords.Y - (SmallDotSize * 3));
                    }
                }
            }

            BeginInvoke(
                (Action)(
                    () =>
                    {
                        
                    }
                )
            );
        }
    
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
