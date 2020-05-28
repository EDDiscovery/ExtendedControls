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
            this.extAstroPlotTest = new ExtendedControls.Controls.ExtAstroPlot();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAxesWidgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showBoundariesFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.extAstroPlotTest.Size = new System.Drawing.Size(520, 494);
            this.extAstroPlotTest.SmallDotSize = 3;
            this.extAstroPlotTest.TabIndex = 0;
            this.extAstroPlotTest.MouseDown += new System.Windows.Forms.MouseEventHandler(this.extAstroPlotTest_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem,
            this.showAxesWidgetToolStripMenuItem,
            this.showBoundariesFrameToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(202, 92);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // showAxesWidgetToolStripMenuItem
            // 
            this.showAxesWidgetToolStripMenuItem.Name = "showAxesWidgetToolStripMenuItem";
            this.showAxesWidgetToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.showAxesWidgetToolStripMenuItem.Text = "Show Axes Widget";
            // 
            // showBoundariesFrameToolStripMenuItem
            // 
            this.showBoundariesFrameToolStripMenuItem.Name = "showBoundariesFrameToolStripMenuItem";
            this.showBoundariesFrameToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.showBoundariesFrameToolStripMenuItem.Text = "Show Boundaries Frame";
            // 
            // TestAstroPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 494);
            this.Controls.Add(this.extAstroPlotTest);
            this.Name = "TestAstroPlot";
            this.Text = "TestAstroPlot";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.Controls.ExtAstroPlot extAstroPlotTest;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAxesWidgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showBoundariesFrameToolStripMenuItem;
    }
}