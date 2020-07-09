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
            this.extLabel2 = new ExtendedControls.ExtLabel();
            this.astroPlot = new ExtendedControls.Controls.AstroPlot();
            this.extLabel1 = new ExtendedControls.ExtLabel();
            this.extTextBox1 = new ExtendedControls.ExtTextBox();
            this.extButtonProjection = new ExtendedControls.ExtButton();
            this.SuspendLayout();
            // 
            // extLabel2
            // 
            this.extLabel2.AutoSize = true;
            this.extLabel2.BackColor = System.Drawing.Color.Black;
            this.extLabel2.Location = new System.Drawing.Point(8, 8);
            this.extLabel2.Name = "extLabel2";
            this.extLabel2.Size = new System.Drawing.Size(29, 13);
            this.extLabel2.TabIndex = 1;
            this.extLabel2.Text = "label";
            this.extLabel2.TextBackColor = System.Drawing.Color.Transparent;
            // 
            // astroPlot
            // 
            this.astroPlot.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.astroPlot.AxesLength = 10;
            this.astroPlot.AxesThickness = 1;
            this.astroPlot.Azimuth = -0.4D;
            this.astroPlot.CurrentColor = System.Drawing.Color.Red;
            this.astroPlot.Distance = 300D;
            this.astroPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.astroPlot.Elevation = -0.3D;
            this.astroPlot.Focus = 1000D;
            this.astroPlot.ForeColor = System.Drawing.Color.White;
            this.astroPlot.FramesLength = 20D;
            this.astroPlot.FramesThickness = 1;
            this.astroPlot.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(30)))), ((int)(((byte)(190)))), ((int)(((byte)(240)))));
            this.astroPlot.GridCount = 5;
            this.astroPlot.GridUnit = 10;
            this.astroPlot.HotSpotSize = 10D;
            this.astroPlot.IsObjectSelected = false;
            this.astroPlot.LargeDotSize = 15;
            this.astroPlot.Location = new System.Drawing.Point(0, 0);
            this.astroPlot.MediumDotSize = 10;
            this.astroPlot.MouseDrag_Multiply = 20D;
            this.astroPlot.MouseDrag_Resistance = 12D;
            this.astroPlot.MouseRotation_Multiply = 1D;
            this.astroPlot.MouseRotation_Resistance = 75D;
            this.astroPlot.MouseWheel_Multiply = 7D;
            this.astroPlot.MouseWheel_Resistance = 2D;
            this.astroPlot.Name = "astroPlot";
            this.astroPlot.Projection = ExtendedControls.Controls.AstroPlot.PlotProjection.Free;
            this.astroPlot.ShowAxesWidget = true;
            this.astroPlot.ShowFrameWidget = true;
            this.astroPlot.ShowGridWidget = true;
            this.astroPlot.Size = new System.Drawing.Size(484, 461);
            this.astroPlot.SmallDotSize = 5;
            this.astroPlot.TabIndex = 0;
            this.astroPlot.UnVisitedColor = System.Drawing.Color.Yellow;
            this.astroPlot.VisitedColor = System.Drawing.Color.Aqua;
            this.astroPlot.MouseHover += new System.EventHandler(this.astroPlot_MouseHover);
            // 
            // extLabel1
            // 
            this.extLabel1.Location = new System.Drawing.Point(0, 0);
            this.extLabel1.Name = "extLabel1";
            this.extLabel1.Size = new System.Drawing.Size(100, 23);
            this.extLabel1.TabIndex = 0;
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
            this.extTextBox1.Location = new System.Drawing.Point(0, 0);
            this.extTextBox1.Multiline = false;
            this.extTextBox1.Name = "extTextBox1";
            this.extTextBox1.ReadOnly = false;
            this.extTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.extTextBox1.SelectionLength = 0;
            this.extTextBox1.SelectionStart = 0;
            this.extTextBox1.Size = new System.Drawing.Size(0, 0);
            this.extTextBox1.TabIndex = 0;
            this.extTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.extTextBox1.WordWrap = true;
            // 
            // extButtonProjection
            // 
            this.extButtonProjection.Location = new System.Drawing.Point(397, 426);
            this.extButtonProjection.Name = "extButtonProjection";
            this.extButtonProjection.Size = new System.Drawing.Size(75, 23);
            this.extButtonProjection.TabIndex = 2;
            this.extButtonProjection.Text = "projection";
            this.extButtonProjection.UseVisualStyleBackColor = true;
            this.extButtonProjection.Click += new System.EventHandler(this.extButtonProjection_Click);
            // 
            // TestAstroPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.extButtonProjection);
            this.Controls.Add(this.extLabel2);
            this.Controls.Add(this.astroPlot);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Name = "TestAstroPlot";
            this.Text = "TestAstroPlot";
            this.Load += new System.EventHandler(this.TestAstroPlot_Load);
            this.MouseHover += new System.EventHandler(this.TestAstroPlot_MouseHover);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExtendedControls.Controls.AstroPlot astroPlot;        
        private ExtendedControls.ExtTextBox extTextBox1;
        private ExtendedControls.ExtLabel extLabel1;
        private ExtendedControls.ExtLabel extLabel2;
        private ExtendedControls.ExtButton extButtonProjection;
    }
}