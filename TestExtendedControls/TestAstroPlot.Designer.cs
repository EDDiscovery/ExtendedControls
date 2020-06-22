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
            this.astroPlot = new ExtendedControls.Controls.AstroPlot();
            this.SuspendLayout();
            // 
            // astroPlot
            // 
            this.astroPlot.AxesLength = 2;
            this.astroPlot.AxesThickness = 1;
            this.astroPlot.AxesWidget = true;
            this.astroPlot.Azimuth = -0.4D;
            this.astroPlot.Camera = new double[] {
        3.7202555194225959D,
        -2.9552020666133956D,
        -8.7992317628125711D};
            this.astroPlot.CoordsCenter = new double[] {
        0D,
        0D,
        0D};
            this.astroPlot.CurrentColor = System.Drawing.Color.Red;
            this.astroPlot.Distance = 10D;
            this.astroPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.astroPlot.Elevation = -0.3D;
            this.astroPlot.Focus = 1000D;
            this.astroPlot.ForeColor = System.Drawing.Color.White;
            this.astroPlot.FramesRadius = 20D;
            this.astroPlot.FramesThickness = 1;
            this.astroPlot.FramesWidget = false;
            this.astroPlot.HotSpotSize = 10;
            this.astroPlot.LargeDotSize = 16;
            this.astroPlot.Location = new System.Drawing.Point(0, 0);
            this.astroPlot.MediumDotSize = 12;
            this.astroPlot.MouseSensitivity_Movement = 150;
            this.astroPlot.MouseSensitivity_Wheel = 300D;
            this.astroPlot.Name = "astroPlot";
            this.astroPlot.SelectedObjectName = null;
            this.astroPlot.SelectedObjectX = 0D;
            this.astroPlot.SelectedObjectY = 0D;
            this.astroPlot.SelectedObjectZ = 0D;
            this.astroPlot.Size = new System.Drawing.Size(1401, 820);
            this.astroPlot.SmallDotSize = 8;
            this.astroPlot.TabIndex = 0;
            this.astroPlot.UnVisitedColor = System.Drawing.Color.Yellow;
            this.astroPlot.VisitedColor = System.Drawing.Color.Aqua;
            // 
            // TestAstroPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1401, 820);
            this.Controls.Add(this.astroPlot);
            this.Name = "TestAstroPlot";
            this.Text = "TestAstroPlot";
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.Controls.AstroPlot astroPlot;
    }
}