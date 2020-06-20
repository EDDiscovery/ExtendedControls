namespace DialogTest
{
    partial class TestAstroPlot
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private global::System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new global::System.ComponentModel.Container();
            this.contextMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolTip1 = new global::System.Windows.Forms.ToolTip(this.components);
            this.extAstroPlotTest = new ExtendedControls.Controls.ExtAstroPlot();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new global::System.Drawing.Size(61, 4);
            // 
            // extAstroPlotTest
            // 
            this.extAstroPlotTest.AxesLength = 100;
            this.extAstroPlotTest.AxesThickness = 1;
            this.extAstroPlotTest.AxesWidget = true;
            this.extAstroPlotTest.Azimuth = 0.3D;
            this.extAstroPlotTest.BackColor = global::System.Drawing.Color.Black;
            this.extAstroPlotTest.FramesThickness = 1;
            this.extAstroPlotTest.FramesRadius = 0.8D;
            this.extAstroPlotTest.FramesWidget = true;
            this.extAstroPlotTest.Camera = new double[] {
        -1.0163564521110631D,
        1.063872743980822D,
        -3.2856041068374195D};
            this.extAstroPlotTest.ContextMenuStrip = this.contextMenuStrip;
            this.extAstroPlotTest.CoordsCenter = new double[] {
        0D,
        0D,
        0D};
            this.extAstroPlotTest.Distance = 3.5999999999999988D;
            this.extAstroPlotTest.Dock = global::System.Windows.Forms.DockStyle.Fill;
            this.extAstroPlotTest.Elevation = 0.3D;
            this.extAstroPlotTest.Focus = 900D;
            this.extAstroPlotTest.ForeColor = global::System.Drawing.Color.White;
            this.extAstroPlotTest.HotSpotSize = 10;
            this.extAstroPlotTest.LargeDotSize = 9;
            this.extAstroPlotTest.Location = new global::System.Drawing.Point(0, 0);
            this.extAstroPlotTest.MediumDotSize = 6;
            this.extAstroPlotTest.MouseSensitivity_Movement = 150;
            this.extAstroPlotTest.MouseSensitivity_Wheel = 300D;
            this.extAstroPlotTest.Name = "extAstroPlotTest";
            this.extAstroPlotTest.Size = new global::System.Drawing.Size(406, 391);
            this.extAstroPlotTest.SmallDotSize = 3;
            this.extAstroPlotTest.TabIndex = 0;
            this.extAstroPlotTest.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.extAstroPlotTest_MouseDown);
            this.extAstroPlotTest.MouseHover += new global::System.EventHandler(this.extAstroPlotTest_MouseHover);
            // 
            // TestAstroPlot
            // 
            this.AutoScaleDimensions = new global::System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new global::System.Drawing.Size(406, 391);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.extAstroPlotTest);
            this.Name = "TestAstroPlot";
            this.Text = "TestAstroPlot";
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.Controls.ExtAstroPlot extAstroPlotTest;
        private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private global::System.Windows.Forms.ToolTip toolTip1;
    }
}