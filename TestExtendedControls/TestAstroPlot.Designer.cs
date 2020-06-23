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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestAstroPlot));
            this.extPanelMinimal1 = new ExtendedControls.ExtPanelMinimal(this.components);
            this.astroPlot = new ExtendedControls.Controls.AstroPlot();
            this.extLabel1 = new ExtendedControls.ExtLabel();
            this.extTextBox1 = new ExtendedControls.ExtTextBox();
            this.extTextMaxRange = new ExtendedControls.ExtTextBox();
            this.extTextMinRange = new ExtendedControls.ExtTextBox();
            this.extCheckBoxCube = new ExtendedControls.ExtCheckBox();
            this.extPanelMinimal1.SuspendLayout();
            this.SuspendLayout();
            // 
            // extPanelMinimal1
            // 
            this.extPanelMinimal1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.extPanelMinimal1.BackColor = System.Drawing.SystemColors.Control;
            this.extPanelMinimal1.Controls.Add(this.extCheckBoxCube);
            this.extPanelMinimal1.Controls.Add(this.extTextMinRange);
            this.extPanelMinimal1.Controls.Add(this.extTextMaxRange);
            this.extPanelMinimal1.Location = new System.Drawing.Point(0, 0);
            this.extPanelMinimal1.Name = "extPanelMinimal1";
            this.extPanelMinimal1.PinSize = 32;
            this.extPanelMinimal1.Size = new System.Drawing.Size(32, 32);
            this.extPanelMinimal1.TabIndex = 1;
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
            this.astroPlot.Size = new System.Drawing.Size(484, 461);
            this.astroPlot.SmallDotSize = 8;
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
            // extTextMaxRange
            // 
            this.extTextMaxRange.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.extTextMaxRange.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.extTextMaxRange.BackErrorColor = System.Drawing.Color.Red;
            this.extTextMaxRange.BorderColor = System.Drawing.Color.Transparent;
            this.extTextMaxRange.BorderColorScaling = 0.5F;
            this.extTextMaxRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.extTextMaxRange.ClearOnFirstChar = false;
            this.extTextMaxRange.ControlBackground = System.Drawing.SystemColors.Control;
            this.extTextMaxRange.EndButtonEnable = true;
            this.extTextMaxRange.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("extTextMaxRange.EndButtonImage")));
            this.extTextMaxRange.EndButtonVisible = false;
            this.extTextMaxRange.InErrorCondition = false;
            this.extTextMaxRange.Location = new System.Drawing.Point(100, 5);
            this.extTextMaxRange.Multiline = false;
            this.extTextMaxRange.Name = "extTextMaxRange";
            this.extTextMaxRange.ReadOnly = false;
            this.extTextMaxRange.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.extTextMaxRange.SelectionLength = 0;
            this.extTextMaxRange.SelectionStart = 0;
            this.extTextMaxRange.Size = new System.Drawing.Size(50, 23);
            this.extTextMaxRange.TabIndex = 0;
            this.extTextMaxRange.Text = "50";
            this.extTextMaxRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.extTextMaxRange.WordWrap = true;
            // 
            // extTextMinRange
            // 
            this.extTextMinRange.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.extTextMinRange.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.extTextMinRange.BackErrorColor = System.Drawing.Color.Red;
            this.extTextMinRange.BorderColor = System.Drawing.Color.Transparent;
            this.extTextMinRange.BorderColorScaling = 0.5F;
            this.extTextMinRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.extTextMinRange.ClearOnFirstChar = false;
            this.extTextMinRange.ControlBackground = System.Drawing.SystemColors.Control;
            this.extTextMinRange.EndButtonEnable = true;
            this.extTextMinRange.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("extTextMinRange.EndButtonImage")));
            this.extTextMinRange.EndButtonVisible = false;
            this.extTextMinRange.InErrorCondition = false;
            this.extTextMinRange.Location = new System.Drawing.Point(40, 5);
            this.extTextMinRange.Multiline = false;
            this.extTextMinRange.Name = "extTextMinRange";
            this.extTextMinRange.ReadOnly = false;
            this.extTextMinRange.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.extTextMinRange.SelectionLength = 0;
            this.extTextMinRange.SelectionStart = 0;
            this.extTextMinRange.Size = new System.Drawing.Size(50, 23);
            this.extTextMinRange.TabIndex = 1;
            this.extTextMinRange.Text = "0";
            this.extTextMinRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.extTextMinRange.WordWrap = true;
            // 
            // extCheckBoxCube
            // 
            this.extCheckBoxCube.AutoSize = true;
            this.extCheckBoxCube.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.extCheckBoxCube.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.extCheckBoxCube.CheckBoxColor = System.Drawing.Color.Gray;
            this.extCheckBoxCube.CheckBoxDisabledScaling = 1F;
            this.extCheckBoxCube.CheckBoxInnerColor = System.Drawing.Color.White;
            this.extCheckBoxCube.CheckColor = System.Drawing.Color.DarkBlue;
            this.extCheckBoxCube.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.extCheckBoxCube.ImageButtonDisabledScaling = 0.5F;
            this.extCheckBoxCube.ImageIndeterminate = null;
            this.extCheckBoxCube.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.extCheckBoxCube.ImageUnchecked = null;
            this.extCheckBoxCube.Location = new System.Drawing.Point(165, 10);
            this.extCheckBoxCube.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.extCheckBoxCube.Name = "extCheckBoxCube";
            this.extCheckBoxCube.Size = new System.Drawing.Size(12, 11);
            this.extCheckBoxCube.TabIndex = 2;
            this.extCheckBoxCube.TickBoxReductionRatio = 1F;
            this.extCheckBoxCube.UseCompatibleTextRendering = true;
            this.extCheckBoxCube.UseVisualStyleBackColor = true;
            // 
            // TestAstroPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.extPanelMinimal1);
            this.Controls.Add(this.astroPlot);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Name = "TestAstroPlot";
            this.Text = "TestAstroPlot";
            this.Load += new System.EventHandler(this.TestAstroPlot_Load);
            this.extPanelMinimal1.ResumeLayout(false);
            this.extPanelMinimal1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.Controls.AstroPlot astroPlot;        
        private ExtendedControls.ExtTextBox extTextBox1;
        private ExtendedControls.ExtLabel extLabel1;
        private ExtendedControls.ExtPanelMinimal extPanelMinimal1;
        private ExtendedControls.ExtCheckBox extCheckBoxCube;
        private ExtendedControls.ExtTextBox extTextMinRange;
        private ExtendedControls.ExtTextBox extTextMaxRange;
    }
}