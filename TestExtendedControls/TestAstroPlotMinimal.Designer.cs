namespace DialogTest
{
    partial class TestAstroPlotMinimal
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
            this.astroPlot1 = new ExtendedControls.Controls.AstroPlot();
            this.SuspendLayout();
            // 
            // astroPlot1
            // 
            this.astroPlot1.AxesLength = 10;
            this.astroPlot1.AxesThickness = 1;
            this.astroPlot1.Azimuth = -0.4D;
            this.astroPlot1.CenterCoordinates = new double[] {
        0D,
        0D,
        0D};
            this.astroPlot1.CurrentColor = System.Drawing.Color.Red;
            this.astroPlot1.Distance = 100D;
            this.astroPlot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.astroPlot1.Elevation = -0.3D;
            this.astroPlot1.Focus = 900D;
            this.astroPlot1.ForeColor = System.Drawing.Color.White;
            this.astroPlot1.FramesLength = 20D;
            this.astroPlot1.FramesThickness = 1;
            this.astroPlot1.HotSpotSize = 0;
            this.astroPlot1.LargeDotSize = 15;
            this.astroPlot1.Location = new System.Drawing.Point(0, 0);
            this.astroPlot1.MediumDotSize = 10;
            this.astroPlot1.Mouse_Sensitivity = 0;
            this.astroPlot1.MouseWheel_Resistance = 0D;
            this.astroPlot1.Name = "astroPlot1";
            this.astroPlot1.ShowAxesWidget = true;
            this.astroPlot1.ShowFrameWidget = true;
            this.astroPlot1.Size = new System.Drawing.Size(484, 461);
            this.astroPlot1.SmallDotSize = 5;
            this.astroPlot1.TabIndex = 0;
            this.astroPlot1.Text = "astroPlot1";
            this.astroPlot1.UnVisitedColor = System.Drawing.Color.Yellow;
            this.astroPlot1.VisitedColor = System.Drawing.Color.Aqua;
            // 
            // TestAstroPlotMinimal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.astroPlot1);
            this.Name = "TestAstroPlotMinimal";
            this.Text = "TestAstroPlotMinimal";
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.Controls.AstroPlot astroPlot1;
    }
}