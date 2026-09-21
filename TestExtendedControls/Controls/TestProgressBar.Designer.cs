namespace TestExtendedControls
{
    partial class TestProgressBar
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.winprogressbar = new System.Windows.Forms.ProgressBar();
            this.extProgressBarMultiSegment1 = new ExtendedControls.ExtProgressBarMultiSegment();
            this.extProgressBar = new ExtendedControls.ExtProgressBar();
            this.extButton6 = new ExtendedControls.ExtButton();
            this.extButton5 = new ExtendedControls.ExtButton();
            this.extButton4 = new ExtendedControls.ExtButton();
            this.extButton3 = new ExtendedControls.ExtButton();
            this.extButton2 = new ExtendedControls.ExtButton();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.extNumericUpDownSeg0 = new ExtendedControls.ExtNumericUpDown();
            this.extNumericUpDownSeg1 = new ExtendedControls.ExtNumericUpDown();
            this.extNumericUpDownSeg2 = new ExtendedControls.ExtNumericUpDown();
            this.extButtonSeg0_Zero = new ExtendedControls.ExtButton();
            this.extButtonSeg1_Zero = new ExtendedControls.ExtButton();
            this.extButtonSeg2_Zero = new ExtendedControls.ExtButton();
            this.extButtonSeg0_30 = new ExtendedControls.ExtButton();
            this.extButtonSeg1_30 = new ExtendedControls.ExtButton();
            this.extButtonSeg2_30 = new ExtendedControls.ExtButton();
            this.SuspendLayout();
            // 
            // winprogressbar
            // 
            this.winprogressbar.Location = new System.Drawing.Point(12, 92);
            this.winprogressbar.Name = "winprogressbar";
            this.winprogressbar.Size = new System.Drawing.Size(532, 23);
            this.winprogressbar.TabIndex = 0;
            // 
            // extProgressBarMultiSegment1
            // 
            this.extProgressBarMultiSegment1.BarBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.extProgressBarMultiSegment1.BarHeightReserve = 25D;
            this.extProgressBarMultiSegment1.BarMaximumPercent = 400;
            this.extProgressBarMultiSegment1.BarMaximumPercentNoUpdate = 300;
            this.extProgressBarMultiSegment1.BarWidthMargin = 4;
            this.extProgressBarMultiSegment1.BorderColor = System.Drawing.Color.Black;
            this.extProgressBarMultiSegment1.HighlightColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.extProgressBarMultiSegment1.HighlightColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.extProgressBarMultiSegment1.Limit = 90;
            this.extProgressBarMultiSegment1.LimitLineColor = System.Drawing.Color.Cyan;
            this.extProgressBarMultiSegment1.Location = new System.Drawing.Point(12, 333);
            this.extProgressBarMultiSegment1.Marker1 = -1;
            this.extProgressBarMultiSegment1.Marker2 = -1;
            this.extProgressBarMultiSegment1.MarkerLineColor = System.Drawing.Color.Cyan;
            this.extProgressBarMultiSegment1.MarkerWidth = 2;
            this.extProgressBarMultiSegment1.Maximum = 110;
            this.extProgressBarMultiSegment1.Name = "extProgressBarMultiSegment1";
            this.extProgressBarMultiSegment1.SegmentColors = null;
            this.extProgressBarMultiSegment1.SegmentValues = null;
            this.extProgressBarMultiSegment1.Size = new System.Drawing.Size(531, 38);
            this.extProgressBarMultiSegment1.TabIndex = 7;
            this.extProgressBarMultiSegment1.Text = "extProgressBarMultiSegment1";
            this.extProgressBarMultiSegment1.TrackSpeed = 5;
            // 
            // extProgressBar
            // 
            this.extProgressBar.BarBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.extProgressBar.BarColor = System.Drawing.Color.Green;
            this.extProgressBar.BarHeightReserve = 25D;
            this.extProgressBar.BarHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.extProgressBar.BarMaximumPercent = 400;
            this.extProgressBar.BarMaximumPercentNoUpdate = 300;
            this.extProgressBar.BarWidthMargin = 4;
            this.extProgressBar.BorderColor = System.Drawing.Color.Black;
            this.extProgressBar.Limit = 90;
            this.extProgressBar.LimitColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.extProgressBar.LimitHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.extProgressBar.LimitLineColor = System.Drawing.Color.Cyan;
            this.extProgressBar.Location = new System.Drawing.Point(13, 161);
            this.extProgressBar.Marker1 = -1;
            this.extProgressBar.Marker2 = -1;
            this.extProgressBar.MarkerLineColor = System.Drawing.Color.Cyan;
            this.extProgressBar.MarkerWidth = 2;
            this.extProgressBar.Maximum = 100;
            this.extProgressBar.Minimum = 0;
            this.extProgressBar.Name = "extProgressBar";
            this.extProgressBar.Size = new System.Drawing.Size(531, 32);
            this.extProgressBar.TabIndex = 6;
            this.extProgressBar.TrackSpeed = 5;
            this.extProgressBar.Value = 0;
            // 
            // extButton6
            // 
            this.extButton6.BackColor2 = System.Drawing.Color.Red;
            this.extButton6.ButtonDisabledScaling = 0.5F;
            this.extButton6.GradientDirection = 90F;
            this.extButton6.Location = new System.Drawing.Point(433, 30);
            this.extButton6.MouseOverScaling = 1.3F;
            this.extButton6.MouseSelectedScaling = 1.3F;
            this.extButton6.Name = "extButton6";
            this.extButton6.Size = new System.Drawing.Size(75, 23);
            this.extButton6.TabIndex = 5;
            this.extButton6.Text = "110";
            this.extButton6.UseVisualStyleBackColor = true;
            this.extButton6.Click += new System.EventHandler(this.extButton6_Click);
            // 
            // extButton5
            // 
            this.extButton5.BackColor2 = System.Drawing.Color.Red;
            this.extButton5.ButtonDisabledScaling = 0.5F;
            this.extButton5.GradientDirection = 90F;
            this.extButton5.Location = new System.Drawing.Point(352, 30);
            this.extButton5.MouseOverScaling = 1.3F;
            this.extButton5.MouseSelectedScaling = 1.3F;
            this.extButton5.Name = "extButton5";
            this.extButton5.Size = new System.Drawing.Size(75, 23);
            this.extButton5.TabIndex = 5;
            this.extButton5.Text = "100";
            this.extButton5.UseVisualStyleBackColor = true;
            this.extButton5.Click += new System.EventHandler(this.extButton5_Click);
            // 
            // extButton4
            // 
            this.extButton4.BackColor2 = System.Drawing.Color.Red;
            this.extButton4.ButtonDisabledScaling = 0.5F;
            this.extButton4.GradientDirection = 90F;
            this.extButton4.Location = new System.Drawing.Point(271, 30);
            this.extButton4.MouseOverScaling = 1.3F;
            this.extButton4.MouseSelectedScaling = 1.3F;
            this.extButton4.Name = "extButton4";
            this.extButton4.Size = new System.Drawing.Size(75, 23);
            this.extButton4.TabIndex = 4;
            this.extButton4.Text = "75";
            this.extButton4.UseVisualStyleBackColor = true;
            this.extButton4.Click += new System.EventHandler(this.extButton4_Click);
            // 
            // extButton3
            // 
            this.extButton3.BackColor2 = System.Drawing.Color.Red;
            this.extButton3.ButtonDisabledScaling = 0.5F;
            this.extButton3.GradientDirection = 90F;
            this.extButton3.Location = new System.Drawing.Point(190, 30);
            this.extButton3.MouseOverScaling = 1.3F;
            this.extButton3.MouseSelectedScaling = 1.3F;
            this.extButton3.Name = "extButton3";
            this.extButton3.Size = new System.Drawing.Size(75, 23);
            this.extButton3.TabIndex = 3;
            this.extButton3.Text = "50";
            this.extButton3.UseVisualStyleBackColor = true;
            this.extButton3.Click += new System.EventHandler(this.extButton3_Click);
            // 
            // extButton2
            // 
            this.extButton2.BackColor2 = System.Drawing.Color.Red;
            this.extButton2.ButtonDisabledScaling = 0.5F;
            this.extButton2.GradientDirection = 90F;
            this.extButton2.Location = new System.Drawing.Point(109, 30);
            this.extButton2.MouseOverScaling = 1.3F;
            this.extButton2.MouseSelectedScaling = 1.3F;
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(75, 23);
            this.extButton2.TabIndex = 2;
            this.extButton2.Text = "25";
            this.extButton2.UseVisualStyleBackColor = true;
            this.extButton2.Click += new System.EventHandler(this.extButton2_Click);
            // 
            // extButton1
            // 
            this.extButton1.BackColor2 = System.Drawing.Color.Red;
            this.extButton1.ButtonDisabledScaling = 0.5F;
            this.extButton1.GradientDirection = 90F;
            this.extButton1.Location = new System.Drawing.Point(28, 30);
            this.extButton1.MouseOverScaling = 1.3F;
            this.extButton1.MouseSelectedScaling = 1.3F;
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(75, 23);
            this.extButton1.TabIndex = 1;
            this.extButton1.Text = "0";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click);
            // 
            // extNumericUpDownSeg0
            // 
            this.extNumericUpDownSeg0.AutoSizeTextBox = true;
            this.extNumericUpDownSeg0.BorderColor = System.Drawing.Color.Transparent;
            this.extNumericUpDownSeg0.BorderColor2 = System.Drawing.Color.Transparent;
            this.extNumericUpDownSeg0.Location = new System.Drawing.Point(12, 239);
            this.extNumericUpDownSeg0.Maximum = 100;
            this.extNumericUpDownSeg0.Minimum = 0;
            this.extNumericUpDownSeg0.Name = "extNumericUpDownSeg0";
            this.extNumericUpDownSeg0.Size = new System.Drawing.Size(125, 23);
            this.extNumericUpDownSeg0.TabIndex = 8;
            this.extNumericUpDownSeg0.Text = "0";
            this.extNumericUpDownSeg0.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.extNumericUpDownSeg0.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.extNumericUpDownSeg0.Value = 0;
            // 
            // extNumericUpDownSeg1
            // 
            this.extNumericUpDownSeg1.AutoSizeTextBox = true;
            this.extNumericUpDownSeg1.BorderColor = System.Drawing.Color.Transparent;
            this.extNumericUpDownSeg1.BorderColor2 = System.Drawing.Color.Transparent;
            this.extNumericUpDownSeg1.Location = new System.Drawing.Point(172, 239);
            this.extNumericUpDownSeg1.Maximum = 100;
            this.extNumericUpDownSeg1.Minimum = 0;
            this.extNumericUpDownSeg1.Name = "extNumericUpDownSeg1";
            this.extNumericUpDownSeg1.Size = new System.Drawing.Size(125, 23);
            this.extNumericUpDownSeg1.TabIndex = 8;
            this.extNumericUpDownSeg1.Text = "0";
            this.extNumericUpDownSeg1.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.extNumericUpDownSeg1.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.extNumericUpDownSeg1.Value = 0;
            // 
            // extNumericUpDownSeg2
            // 
            this.extNumericUpDownSeg2.AutoSizeTextBox = true;
            this.extNumericUpDownSeg2.BorderColor = System.Drawing.Color.Transparent;
            this.extNumericUpDownSeg2.BorderColor2 = System.Drawing.Color.Transparent;
            this.extNumericUpDownSeg2.Location = new System.Drawing.Point(318, 239);
            this.extNumericUpDownSeg2.Maximum = 100;
            this.extNumericUpDownSeg2.Minimum = 0;
            this.extNumericUpDownSeg2.Name = "extNumericUpDownSeg2";
            this.extNumericUpDownSeg2.Size = new System.Drawing.Size(125, 23);
            this.extNumericUpDownSeg2.TabIndex = 8;
            this.extNumericUpDownSeg2.Text = "0";
            this.extNumericUpDownSeg2.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.extNumericUpDownSeg2.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.extNumericUpDownSeg2.Value = 0;
            // 
            // extButtonSeg0_Zero
            // 
            this.extButtonSeg0_Zero.BackColor2 = System.Drawing.Color.Red;
            this.extButtonSeg0_Zero.ButtonDisabledScaling = 0.5F;
            this.extButtonSeg0_Zero.GradientDirection = 90F;
            this.extButtonSeg0_Zero.Location = new System.Drawing.Point(12, 269);
            this.extButtonSeg0_Zero.MouseOverScaling = 1.3F;
            this.extButtonSeg0_Zero.MouseSelectedScaling = 1.3F;
            this.extButtonSeg0_Zero.Name = "extButtonSeg0_Zero";
            this.extButtonSeg0_Zero.Size = new System.Drawing.Size(75, 23);
            this.extButtonSeg0_Zero.TabIndex = 9;
            this.extButtonSeg0_Zero.Text = "Seg0 0";
            this.extButtonSeg0_Zero.UseVisualStyleBackColor = true;
            this.extButtonSeg0_Zero.Click += new System.EventHandler(this.extButtonSeg0_Zero_Click);
            // 
            // extButtonSeg1_Zero
            // 
            this.extButtonSeg1_Zero.BackColor2 = System.Drawing.Color.Red;
            this.extButtonSeg1_Zero.ButtonDisabledScaling = 0.5F;
            this.extButtonSeg1_Zero.GradientDirection = 90F;
            this.extButtonSeg1_Zero.Location = new System.Drawing.Point(172, 268);
            this.extButtonSeg1_Zero.MouseOverScaling = 1.3F;
            this.extButtonSeg1_Zero.MouseSelectedScaling = 1.3F;
            this.extButtonSeg1_Zero.Name = "extButtonSeg1_Zero";
            this.extButtonSeg1_Zero.Size = new System.Drawing.Size(75, 23);
            this.extButtonSeg1_Zero.TabIndex = 9;
            this.extButtonSeg1_Zero.Text = "Seg1 0";
            this.extButtonSeg1_Zero.UseVisualStyleBackColor = true;
            this.extButtonSeg1_Zero.Click += new System.EventHandler(this.extButtonSeg1_Zero_Click);
            // 
            // extButtonSeg2_Zero
            // 
            this.extButtonSeg2_Zero.BackColor2 = System.Drawing.Color.Red;
            this.extButtonSeg2_Zero.ButtonDisabledScaling = 0.5F;
            this.extButtonSeg2_Zero.GradientDirection = 90F;
            this.extButtonSeg2_Zero.Location = new System.Drawing.Point(318, 269);
            this.extButtonSeg2_Zero.MouseOverScaling = 1.3F;
            this.extButtonSeg2_Zero.MouseSelectedScaling = 1.3F;
            this.extButtonSeg2_Zero.Name = "extButtonSeg2_Zero";
            this.extButtonSeg2_Zero.Size = new System.Drawing.Size(75, 23);
            this.extButtonSeg2_Zero.TabIndex = 9;
            this.extButtonSeg2_Zero.Text = "Seg2 0";
            this.extButtonSeg2_Zero.UseVisualStyleBackColor = true;
            this.extButtonSeg2_Zero.Click += new System.EventHandler(this.extButtonSeg2_Zero_Click);
            // 
            // extButtonSeg0_30
            // 
            this.extButtonSeg0_30.BackColor2 = System.Drawing.Color.Red;
            this.extButtonSeg0_30.ButtonDisabledScaling = 0.5F;
            this.extButtonSeg0_30.GradientDirection = 90F;
            this.extButtonSeg0_30.Location = new System.Drawing.Point(12, 298);
            this.extButtonSeg0_30.MouseOverScaling = 1.3F;
            this.extButtonSeg0_30.MouseSelectedScaling = 1.3F;
            this.extButtonSeg0_30.Name = "extButtonSeg0_30";
            this.extButtonSeg0_30.Size = new System.Drawing.Size(75, 23);
            this.extButtonSeg0_30.TabIndex = 9;
            this.extButtonSeg0_30.Text = "Seg0 30";
            this.extButtonSeg0_30.UseVisualStyleBackColor = true;
            this.extButtonSeg0_30.Click += new System.EventHandler(this.extButtonSeg0_30_Click);
            // 
            // extButtonSeg1_30
            // 
            this.extButtonSeg1_30.BackColor2 = System.Drawing.Color.Red;
            this.extButtonSeg1_30.ButtonDisabledScaling = 0.5F;
            this.extButtonSeg1_30.GradientDirection = 90F;
            this.extButtonSeg1_30.Location = new System.Drawing.Point(172, 297);
            this.extButtonSeg1_30.MouseOverScaling = 1.3F;
            this.extButtonSeg1_30.MouseSelectedScaling = 1.3F;
            this.extButtonSeg1_30.Name = "extButtonSeg1_30";
            this.extButtonSeg1_30.Size = new System.Drawing.Size(75, 23);
            this.extButtonSeg1_30.TabIndex = 9;
            this.extButtonSeg1_30.Text = "Seg1 30";
            this.extButtonSeg1_30.UseVisualStyleBackColor = true;
            this.extButtonSeg1_30.Click += new System.EventHandler(this.extButtonSeg1_30_Click);
            // 
            // extButtonSeg2_30
            // 
            this.extButtonSeg2_30.BackColor2 = System.Drawing.Color.Red;
            this.extButtonSeg2_30.ButtonDisabledScaling = 0.5F;
            this.extButtonSeg2_30.GradientDirection = 90F;
            this.extButtonSeg2_30.Location = new System.Drawing.Point(318, 298);
            this.extButtonSeg2_30.MouseOverScaling = 1.3F;
            this.extButtonSeg2_30.MouseSelectedScaling = 1.3F;
            this.extButtonSeg2_30.Name = "extButtonSeg2_30";
            this.extButtonSeg2_30.Size = new System.Drawing.Size(75, 23);
            this.extButtonSeg2_30.TabIndex = 9;
            this.extButtonSeg2_30.Text = "Seg2 30";
            this.extButtonSeg2_30.UseVisualStyleBackColor = true;
            this.extButtonSeg2_30.Click += new System.EventHandler(this.extButtonSeg2_30_Click);
            // 
            // TestProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 509);
            this.Controls.Add(this.extButtonSeg2_30);
            this.Controls.Add(this.extButtonSeg2_Zero);
            this.Controls.Add(this.extButtonSeg1_30);
            this.Controls.Add(this.extButtonSeg1_Zero);
            this.Controls.Add(this.extButtonSeg0_30);
            this.Controls.Add(this.extButtonSeg0_Zero);
            this.Controls.Add(this.extNumericUpDownSeg2);
            this.Controls.Add(this.extNumericUpDownSeg1);
            this.Controls.Add(this.extNumericUpDownSeg0);
            this.Controls.Add(this.extProgressBarMultiSegment1);
            this.Controls.Add(this.extProgressBar);
            this.Controls.Add(this.extButton6);
            this.Controls.Add(this.extButton5);
            this.Controls.Add(this.extButton4);
            this.Controls.Add(this.extButton3);
            this.Controls.Add(this.extButton2);
            this.Controls.Add(this.extButton1);
            this.Controls.Add(this.winprogressbar);
            this.Name = "TestProgressBar";
            this.Text = "TestAutoComplete";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar winprogressbar;
        private ExtendedControls.ExtButton extButton1;
        private ExtendedControls.ExtButton extButton2;
        private ExtendedControls.ExtButton extButton3;
        private ExtendedControls.ExtButton extButton4;
        private ExtendedControls.ExtButton extButton5;
        private ExtendedControls.ExtProgressBar extProgressBar;
        private ExtendedControls.ExtButton extButton6;
        private ExtendedControls.ExtProgressBarMultiSegment extProgressBarMultiSegment1;
        private ExtendedControls.ExtNumericUpDown extNumericUpDownSeg0;
        private ExtendedControls.ExtNumericUpDown extNumericUpDownSeg1;
        private ExtendedControls.ExtNumericUpDown extNumericUpDownSeg2;
        private ExtendedControls.ExtButton extButtonSeg0_Zero;
        private ExtendedControls.ExtButton extButtonSeg1_Zero;
        private ExtendedControls.ExtButton extButtonSeg2_Zero;
        private ExtendedControls.ExtButton extButtonSeg0_30;
        private ExtendedControls.ExtButton extButtonSeg1_30;
        private ExtendedControls.ExtButton extButtonSeg2_30;
    }
}