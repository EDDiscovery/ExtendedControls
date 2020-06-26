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
            this.extPanelPinned1 = new ExtendedControls.ExtPanelPinned(this.components);
            this.extComboBox1 = new ExtendedControls.ExtComboBox();
            this.extCube = new ExtendedControls.ExtRadioButton();
            this.extSphere = new ExtendedControls.ExtRadioButton();
            this.extAzes = new ExtendedControls.ExtCheckBox();
            this.extShowFrame = new ExtendedControls.ExtCheckBox();
            this.astroPlot = new ExtendedControls.Controls.AstroPlot();
            this.extLabel1 = new ExtendedControls.ExtLabel();
            this.extTextBox1 = new ExtendedControls.ExtTextBox();
            this.extPanelPinned1.SuspendLayout();
            this.SuspendLayout();
            // 
            // extPanelPinned1
            // 
            this.extPanelPinned1.Controls.Add(this.extComboBox1);
            this.extPanelPinned1.Controls.Add(this.extCube);
            this.extPanelPinned1.Controls.Add(this.extSphere);
            this.extPanelPinned1.Controls.Add(this.extAzes);
            this.extPanelPinned1.Controls.Add(this.extShowFrame);
            this.extPanelPinned1.Location = new System.Drawing.Point(0, 0);
            this.extPanelPinned1.Name = "extPanelPinned1";
            this.extPanelPinned1.PinSize = 24;
            this.extPanelPinned1.Size = new System.Drawing.Size(24, 24);
            this.extPanelPinned1.TabIndex = 1;
            // 
            // extComboBox1
            // 
            this.extComboBox1.BackColor = System.Drawing.Color.Black;
            this.extComboBox1.BorderColor = System.Drawing.Color.Transparent;
            this.extComboBox1.ButtonColorScaling = 0.5F;
            this.extComboBox1.DataSource = null;
            this.extComboBox1.DisableBackgroundDisabledShadingGradient = false;
            this.extComboBox1.DisplayMember = "";
            this.extComboBox1.DropDownBackgroundColor = System.Drawing.Color.Gray;
            this.extComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extComboBox1.Location = new System.Drawing.Point(432, 1);
            this.extComboBox1.MouseOverBackgroundColor = System.Drawing.Color.Silver;
            this.extComboBox1.Name = "extComboBox1";
            this.extComboBox1.ScrollBarButtonColor = System.Drawing.Color.LightGray;
            this.extComboBox1.ScrollBarColor = System.Drawing.Color.LightGray;
            this.extComboBox1.SelectedIndex = -1;
            this.extComboBox1.SelectedItem = null;
            this.extComboBox1.SelectedValue = null;
            this.extComboBox1.Size = new System.Drawing.Size(52, 21);
            this.extComboBox1.TabIndex = 6;
            this.extComboBox1.Text = "extComboBox1";
            this.extComboBox1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.extComboBox1.ValueMember = "";
            this.extComboBox1.TextChanged += new System.EventHandler(this.extComboBox1_TextChanged);
            // 
            // extCube
            // 
            this.extCube.AutoSize = true;
            this.extCube.BackColor = System.Drawing.Color.Transparent;
            this.extCube.Checked = true;
            this.extCube.Location = new System.Drawing.Point(146, 3);
            this.extCube.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.extCube.Name = "extCube";
            this.extCube.RadioButtonColor = System.Drawing.Color.Gray;
            this.extCube.RadioButtonInnerColor = System.Drawing.Color.White;
            this.extCube.SelectedColor = System.Drawing.Color.DarkBlue;
            this.extCube.SelectedColorRing = System.Drawing.Color.Black;
            this.extCube.Size = new System.Drawing.Size(50, 17);
            this.extCube.TabIndex = 5;
            this.extCube.TabStop = true;
            this.extCube.Text = "Cube";
            this.extCube.UseVisualStyleBackColor = false;
            this.extCube.CheckedChanged += new System.EventHandler(this.extCube_CheckedChanged);
            // 
            // extSphere
            // 
            this.extSphere.AutoSize = true;
            this.extSphere.BackColor = System.Drawing.Color.Transparent;
            this.extSphere.Location = new System.Drawing.Point(197, 3);
            this.extSphere.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.extSphere.Name = "extSphere";
            this.extSphere.RadioButtonColor = System.Drawing.Color.Gray;
            this.extSphere.RadioButtonInnerColor = System.Drawing.Color.White;
            this.extSphere.SelectedColor = System.Drawing.Color.DarkBlue;
            this.extSphere.SelectedColorRing = System.Drawing.Color.Black;
            this.extSphere.Size = new System.Drawing.Size(59, 17);
            this.extSphere.TabIndex = 4;
            this.extSphere.TabStop = true;
            this.extSphere.Text = "Sphere";
            this.extSphere.UseVisualStyleBackColor = false;
            this.extSphere.CheckedChanged += new System.EventHandler(this.extSphere_CheckedChanged);
            // 
            // extAzes
            // 
            this.extAzes.AutoSize = true;
            this.extAzes.BackColor = System.Drawing.Color.Transparent;
            this.extAzes.CheckBoxColor = System.Drawing.Color.Gray;
            this.extAzes.CheckBoxDisabledScaling = 0.5F;
            this.extAzes.CheckBoxInnerColor = System.Drawing.Color.White;
            this.extAzes.CheckColor = System.Drawing.Color.DarkBlue;
            this.extAzes.ImageButtonDisabledScaling = 0.5F;
            this.extAzes.ImageIndeterminate = null;
            this.extAzes.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.extAzes.ImageUnchecked = null;
            this.extAzes.Location = new System.Drawing.Point(33, 3);
            this.extAzes.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.extAzes.Name = "extAzes";
            this.extAzes.Size = new System.Drawing.Size(49, 17);
            this.extAzes.TabIndex = 3;
            this.extAzes.Text = "Axes";
            this.extAzes.TickBoxReductionRatio = 0.75F;
            this.extAzes.UseVisualStyleBackColor = false;
            this.extAzes.CheckedChanged += new System.EventHandler(this.extAzes_CheckedChanged);
            // 
            // extShowFrame
            // 
            this.extShowFrame.AutoEllipsis = true;
            this.extShowFrame.BackColor = System.Drawing.Color.Transparent;
            this.extShowFrame.CheckBoxColor = System.Drawing.Color.Gray;
            this.extShowFrame.CheckBoxDisabledScaling = 0.5F;
            this.extShowFrame.CheckBoxInnerColor = System.Drawing.Color.White;
            this.extShowFrame.CheckColor = System.Drawing.Color.DarkBlue;
            this.extShowFrame.ImageButtonDisabledScaling = 0.5F;
            this.extShowFrame.ImageIndeterminate = null;
            this.extShowFrame.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.extShowFrame.ImageUnchecked = null;
            this.extShowFrame.Location = new System.Drawing.Point(88, 2);
            this.extShowFrame.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.extShowFrame.Name = "extShowFrame";
            this.extShowFrame.Size = new System.Drawing.Size(59, 21);
            this.extShowFrame.TabIndex = 2;
            this.extShowFrame.Text = "Frame";
            this.extShowFrame.TickBoxReductionRatio = 0.75F;
            this.extShowFrame.UseVisualStyleBackColor = false;
            this.extShowFrame.CheckedChanged += new System.EventHandler(this.extShowFrame_CheckedChanged);
            // 
            // astroPlot
            // 
            this.astroPlot.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.astroPlot.AxesLength = 10;
            this.astroPlot.AxesThickness = 1;
            this.astroPlot.Azimuth = -0.4D;
            this.astroPlot.CenterCoordinates = new double[] {
        0D,
        0D,
        0D};
            this.astroPlot.CurrentColor = System.Drawing.Color.Red;
            this.astroPlot.Distance = 150D;
            this.astroPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.astroPlot.Elevation = -0.3D;
            this.astroPlot.Focus = 1000D;
            this.astroPlot.ForeColor = System.Drawing.Color.White;
            this.astroPlot.FramesLength = 20D;
            this.astroPlot.FramesThickness = 1;
            this.astroPlot.HotSpotSize = 10;
            this.astroPlot.LargeDotSize = 15;
            this.astroPlot.Location = new System.Drawing.Point(0, 0);
            this.astroPlot.MediumDotSize = 10;
            this.astroPlot.Mouse_Sensitivity = 150;
            this.astroPlot.MouseDragSensitivity = 5D;
            this.astroPlot.MouseWheel_Multiply = 2D;
            this.astroPlot.MouseWheel_Resistance = 100D;
            this.astroPlot.Name = "astroPlot";
            this.astroPlot.ShowAxesWidget = true;
            this.astroPlot.ShowFrameWidget = true;
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
            this.Controls.Add(this.extPanelPinned1);
            this.Controls.Add(this.astroPlot);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Name = "TestAstroPlot";
            this.Text = "TestAstroPlot";
            this.Load += new System.EventHandler(this.TestAstroPlot_Load);
            this.extPanelPinned1.ResumeLayout(false);
            this.extPanelPinned1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.Controls.AstroPlot astroPlot;        
        private ExtendedControls.ExtTextBox extTextBox1;
        private ExtendedControls.ExtLabel extLabel1;
        private ExtendedControls.ExtPanelPinned extPanelPinned1;
        private ExtendedControls.ExtCheckBox extShowFrame;
        private ExtendedControls.ExtCheckBox extAzes;
        private ExtendedControls.ExtRadioButton extCube;
        private ExtendedControls.ExtRadioButton extSphere;
        private ExtendedControls.ExtComboBox extComboBox1;
    }
}