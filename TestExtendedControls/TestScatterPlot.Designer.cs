namespace DialogTest
{
    partial class TestScatterPlot
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
            this.extScatterPlot1 = new ExtendedControls.Controls.ExtScatterPlot();
            this.SuspendLayout();
            // 
            // extScatterPlot1
            // 
            this.extScatterPlot1.AxesWidget = true;
            this.extScatterPlot1.AxisLength = 10;
            this.extScatterPlot1.AxisThickness = 1;
            this.extScatterPlot1.Azimuth = 0D;
            this.extScatterPlot1.BackColor = System.Drawing.Color.Black;
            this.extScatterPlot1.Camera = new double[] {
        0D,
        0D,
        -6D};
            this.extScatterPlot1.Distance = 6D;
            this.extScatterPlot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extScatterPlot1.Elevation = 0D;
            this.extScatterPlot1.Focus = 900D;
            this.extScatterPlot1.Location = new System.Drawing.Point(0, 0);
            this.extScatterPlot1.Name = "extScatterPlot1";
            this.extScatterPlot1.PointsSize = 3;
            this.extScatterPlot1.Size = new System.Drawing.Size(520, 494);
            this.extScatterPlot1.TabIndex = 0;
            // 
            // TestScatterPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 494);
            this.Controls.Add(this.extScatterPlot1);
            this.Name = "TestScatterPlot";
            this.Text = "TestScatterPlot";
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.Controls.ExtScatterPlot extScatterPlot1;
    }
}