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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestAstroPlot));
            this.astroPlot = new ExtendedControls.Controls.AstroPlot();
            this.extLabel1 = new ExtendedControls.ExtLabel();
            this.extTextBox1 = new ExtendedControls.ExtTextBox();
            this.SuspendLayout();
            // 
            // astroPlot
            // 
            this.astroPlot.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.astroPlot.AxesLength = 10;
            this.astroPlot.AxesThickness = 1;
            this.astroPlot.Azimuth = -0.4D;
            this.astroPlot.CurrentColor = System.Drawing.Color.Red;
            this.astroPlot.Distance = 150D;
            this.astroPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.astroPlot.Elevation = -0.3D;
            this.astroPlot.Focus = 1000D;
            this.astroPlot.ForeColor = System.Drawing.Color.White;
            this.astroPlot.FramesLength = 20D;
            this.astroPlot.FramesThickness = 1;
            this.astroPlot.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(30)))), ((int)(((byte)(190)))), ((int)(((byte)(240)))));
            this.astroPlot.GridCount = 5;
            this.astroPlot.GridUnit = 10;
            this.astroPlot.HotSpotSize = 10;
            this.astroPlot.LargeDotSize = 15;
            this.astroPlot.Location = new System.Drawing.Point(0, 0);
            this.astroPlot.MediumDotSize = 10;
            this.astroPlot.MouseRotation_Resistance = 150;
            this.astroPlot.MouseDrag_Resistance = 5D;
            this.astroPlot.MouseWheel_Multiply = 2D;
            this.astroPlot.MouseWheel_Resistance = 100D;
            this.astroPlot.Name = "astroPlot";
            this.astroPlot.ShowAxesWidget = true;
            this.astroPlot.ShowFrameWidget = true;
            this.astroPlot.ShowGridWidget = true;
            this.astroPlot.Size = new System.Drawing.Size(484, 461);
            this.astroPlot.SmallDotSize = 5;
            this.astroPlot.TabIndex = 0;
            this.astroPlot.UnVisitedColor = System.Drawing.Color.Yellow;
            this.astroPlot.VisitedColor = System.Drawing.Color.Aqua;
            // 
            // extLabel1
            // 
            this.extLabel1.AutoSize = true;
            this.extLabel1.Location = new System.Drawing.Point(40, 9);
            this.extLabel1.Name = "extLabel1";
            this.extLabel1.Size = new System.Drawing.Size(53, 13);
            this.extLabel1.TabIndex = 1;
            this.extLabel1.Text = "extLabel1";
            this.extLabel1.TextBackColor = System.Drawing.Color.Transparent;
            // 
            // extTextBox1
            // 
            this.extTextBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.extTextBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.extTextBox1.BackErrorColor = System.Drawing.Color.Red;
            this.extTextBox1.BorderColor = System.Drawing.Color.Transparent;
            this.extTextBox1.BorderColorScaling = 0.5F;
            this.extTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.extTextBox1.ClearOnFirstChar = false;
            this.extTextBox1.ControlBackground = System.Drawing.SystemColors.Control;
            this.extTextBox1.EndButtonEnable = true;
            this.extTextBox1.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("extTextBox1.EndButtonImage")));
            this.extTextBox1.EndButtonVisible = false;
            this.extTextBox1.InErrorCondition = false;
            this.extTextBox1.Location = new System.Drawing.Point(90, 3);
            this.extTextBox1.Multiline = false;
            this.extTextBox1.Name = "extTextBox1";
            this.extTextBox1.ReadOnly = false;
            this.extTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.extTextBox1.SelectionLength = 0;
            this.extTextBox1.SelectionStart = 0;
            this.extTextBox1.Size = new System.Drawing.Size(75, 23);
            this.extTextBox1.TabIndex = 0;
            this.extTextBox1.Text = "extTextBox1";
            this.extTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.extTextBox1.WordWrap = true;
            // 
            // TestAstroPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.astroPlot);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Name = "TestAstroPlot";
            this.Text = "TestAstroPlot";
            this.Load += new System.EventHandler(this.TestAstroPlot_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.Controls.AstroPlot astroPlot;        
        private ExtendedControls.ExtTextBox extTextBox1;
        private ExtendedControls.ExtLabel extLabel1;
    }
}