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
            this.extAstroPlotTest = new ExtendedControls.Controls.ExtAstroPlot();
            this.SuspendLayout();
            // 
            // extScatterPlot1
            // 
            this.extAstroPlotTest.AxesWidget = true;
            this.extAstroPlotTest.AxisLength = 100;
            this.extAstroPlotTest.AxisThickness = 1;
            this.extAstroPlotTest.Azimuth = 0.3D;
            this.extAstroPlotTest.BackColor = System.Drawing.Color.Black;
            this.extAstroPlotTest.Camera = new double[] {
        -1.693927420185106D,
        1.7731212399680372D,
        -5.4760068447290351D};
            this.extAstroPlotTest.Distance = 6D;
            this.extAstroPlotTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extAstroPlotTest.Elevation = 0.3D;
            this.extAstroPlotTest.Focus = 900D;
            this.extAstroPlotTest.Location = new System.Drawing.Point(0, 0);
            this.extAstroPlotTest.Name = "extScatterPlot1";
            this.extAstroPlotTest.PointsSize = 3;
            this.extAstroPlotTest.Size = new System.Drawing.Size(520, 494);
            this.extAstroPlotTest.TabIndex = 0;
            // 
            // TestAstroPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 494);
            this.Controls.Add(this.extAstroPlotTest);
            this.Name = "TestAstroPlot";
            this.Text = "TestAstroPlot";
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.Controls.ExtAstroPlot extAstroPlotTest;
    }
}