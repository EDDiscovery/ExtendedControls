namespace TestExtendedControls
{
    partial class TestButtons2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.extButtonWarning = new ExtendedControls.ExtButton();
            this.extButtonImage = new ExtendedControls.ExtButton();
            this.extButtonEditTheme = new ExtendedControls.ExtButton();
            this.buttonExt1 = new ExtendedControls.ExtButton();
            this.buttonExt2 = new ExtendedControls.ExtButton();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::TestExtendedControls.Properties.Resources.edsm32x32;
            this.panel1.Location = new System.Drawing.Point(86, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(48, 48);
            this.panel1.TabIndex = 0;
            // 
            // extButtonWarning
            // 
            this.extButtonWarning.BackColor = System.Drawing.Color.DarkTurquoise;
            this.extButtonWarning.BackColor2 = System.Drawing.Color.White;
            this.extButtonWarning.ButtonDisabledScaling = 0.5F;
            this.extButtonWarning.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.extButtonWarning.GradientDirection = 90F;
            this.extButtonWarning.Location = new System.Drawing.Point(254, 13);
            this.extButtonWarning.MouseOverScaling = 1.3F;
            this.extButtonWarning.MouseSelectedScaling = 1.3F;
            this.extButtonWarning.Name = "extButtonWarning";
            this.extButtonWarning.Size = new System.Drawing.Size(150, 24);
            this.extButtonWarning.TabIndex = 3;
            this.extButtonWarning.Text = "Warning";
            this.extButtonWarning.UseVisualStyleBackColor = false;
            this.extButtonWarning.Click += new System.EventHandler(this.extButtonWarning_Click);
            // 
            // extButtonImage
            // 
            this.extButtonImage.BackColor = System.Drawing.Color.Cyan;
            this.extButtonImage.BackColor2 = System.Drawing.Color.Red;
            this.extButtonImage.ButtonDisabledScaling = 0.5F;
            this.extButtonImage.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extButtonImage.GradientDirection = 90F;
            this.extButtonImage.Image = global::TestExtendedControls.Properties.Resources.CaptainsLog;
            this.extButtonImage.Location = new System.Drawing.Point(48, 68);
            this.extButtonImage.MouseOverScaling = 1.3F;
            this.extButtonImage.MouseSelectedScaling = 1.3F;
            this.extButtonImage.Name = "extButtonImage";
            this.extButtonImage.Size = new System.Drawing.Size(100, 100);
            this.extButtonImage.TabIndex = 2;
            this.extButtonImage.UseVisualStyleBackColor = false;
            // 
            // extButtonEditTheme
            // 
            this.extButtonEditTheme.BackColor2 = System.Drawing.Color.RosyBrown;
            this.extButtonEditTheme.ButtonDisabledScaling = 0.5F;
            this.extButtonEditTheme.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.extButtonEditTheme.GradientDirection = 90F;
            this.extButtonEditTheme.Location = new System.Drawing.Point(35, 13);
            this.extButtonEditTheme.MouseOverScaling = 1.3F;
            this.extButtonEditTheme.MouseSelectedScaling = 1.3F;
            this.extButtonEditTheme.Name = "extButtonEditTheme";
            this.extButtonEditTheme.Size = new System.Drawing.Size(113, 24);
            this.extButtonEditTheme.TabIndex = 0;
            this.extButtonEditTheme.Text = "Edit Theme";
            this.extButtonEditTheme.UseVisualStyleBackColor = true;
            this.extButtonEditTheme.Click += new System.EventHandler(this.extButtonText_Click);
            // 
            // buttonExt1
            // 
            this.buttonExt1.BackColor = System.Drawing.Color.BurlyWood;
            this.buttonExt1.BackColor2 = System.Drawing.Color.Red;
            this.buttonExt1.ButtonDisabledScaling = 0.5F;
            this.buttonExt1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonExt1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.buttonExt1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonExt1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonExt1.GradientDirection = 90F;
            this.buttonExt1.Image = global::TestExtendedControls.Properties.Resources.galaxy;
            this.buttonExt1.Location = new System.Drawing.Point(82, 173);
            this.buttonExt1.MouseOverScaling = 1.3F;
            this.buttonExt1.MouseSelectedScaling = 1.3F;
            this.buttonExt1.Name = "buttonExt1";
            this.buttonExt1.Size = new System.Drawing.Size(24, 24);
            this.buttonExt1.TabIndex = 0;
            this.buttonExt1.UseVisualStyleBackColor = false;
            // 
            // buttonExt2
            // 
            this.buttonExt2.BackColor = System.Drawing.Color.NavajoWhite;
            this.buttonExt2.BackColor2 = System.Drawing.Color.Red;
            this.buttonExt2.ButtonDisabledScaling = 0.5F;
            this.buttonExt2.FlatAppearance.BorderColor = System.Drawing.Color.LightSalmon;
            this.buttonExt2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.buttonExt2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonExt2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonExt2.GradientDirection = 90F;
            this.buttonExt2.Image = global::TestExtendedControls.Properties.Resources.galaxy_gray;
            this.buttonExt2.Location = new System.Drawing.Point(114, 173);
            this.buttonExt2.Margin = new System.Windows.Forms.Padding(0);
            this.buttonExt2.MouseOverScaling = 1.3F;
            this.buttonExt2.MouseSelectedScaling = 1.3F;
            this.buttonExt2.Name = "buttonExt2";
            this.buttonExt2.Size = new System.Drawing.Size(24, 24);
            this.buttonExt2.TabIndex = 0;
            this.buttonExt2.Text = "Hello";
            this.buttonExt2.UseVisualStyleBackColor = false;
            // 
            // TestButtons2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(736, 664);
            this.Controls.Add(this.extButtonWarning);
            this.Controls.Add(this.extButtonImage);
            this.Controls.Add(this.extButtonEditTheme);
            this.Name = "TestButtons2";
            this.Text = "TestImageButtons";
            this.ResumeLayout(false);

        }

        #endregion
        private ExtendedControls.ExtButton buttonExt1;
        private ExtendedControls.ExtButton buttonExt2;
        private System.Windows.Forms.Panel panel1;
        private ExtendedControls.ExtButton extButtonEditTheme;
        private ExtendedControls.ExtButton extButtonImage;
        private ExtendedControls.ExtButton extButtonWarning;
    }
}