namespace DialogTest
{
    partial class TestAstroPlot
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.extAstroPlotTest = new ExtendedControls.Controls.ExtAstroPlot();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // extAstroPlotTest
            // 
            this.extAstroPlotTest.AxesLength = 100;
            this.extAstroPlotTest.AxesThickness = 1;
            this.extAstroPlotTest.AxesWidget = true;
            this.extAstroPlotTest.Azimuth = 0.3D;
            this.extAstroPlotTest.BackColor = System.Drawing.Color.Black;
            this.extAstroPlotTest.BoundariesFrameThickness = 1;
            this.extAstroPlotTest.BoundariesRadius = 0.8D;
            this.extAstroPlotTest.BoundariesWidget = true;
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
            this.extAstroPlotTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extAstroPlotTest.Elevation = 0.3D;
            this.extAstroPlotTest.Focus = 900D;
            this.extAstroPlotTest.ForeColor = System.Drawing.Color.White;
            this.extAstroPlotTest.LargeDotSize = 9;
            this.extAstroPlotTest.Location = new System.Drawing.Point(0, 0);
            this.extAstroPlotTest.MediumDotSize = 6;
            this.extAstroPlotTest.MouseSensitivity_Movement = 150;
            this.extAstroPlotTest.MouseSensitivity_Wheel = 300D;
            this.extAstroPlotTest.Name = "extAstroPlotTest";
            this.extAstroPlotTest.Size = new System.Drawing.Size(259, 238);
            this.extAstroPlotTest.SmallDotSize = 3;
            this.extAstroPlotTest.TabIndex = 0;
            this.extAstroPlotTest.MouseDown += new System.Windows.Forms.MouseEventHandler(this.extAstroPlotTest_MouseDown);
            this.extAstroPlotTest.MouseMove += new System.Windows.Forms.MouseEventHandler(this.extAstroPlotTest_MouseMove);
            // 
            // TestAstroPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 238);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.extAstroPlotTest);
            this.Name = "TestAstroPlot";
            this.Text = "TestAstroPlot";
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.Controls.ExtAstroPlot extAstroPlotTest;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
    }
}