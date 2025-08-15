namespace TestExtendedControls
{
    partial class TestTabControlCustom
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
            ExtendedControls.TabStyleSquare tabStyleSquare5 = new ExtendedControls.TabStyleSquare();
            this.panel1 = new System.Windows.Forms.Panel();
            this.extButton2 = new ExtendedControls.ExtButton();
            this.extButton3 = new ExtendedControls.ExtButton();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.button1 = new ExtendedControls.ExtButton();
            this.button2 = new ExtendedControls.ExtButton();
            this.button3 = new ExtendedControls.ExtButton();
            this.buttonTCFont = new ExtendedControls.ExtButton();
            this.tabControl1 = new ExtendedControls.ExtTabControl();
            this.extButtonFormFont = new ExtendedControls.ExtButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.extButton2);
            this.panel1.Controls.Add(this.extButton3);
            this.panel1.Controls.Add(this.extButton1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.extButtonFormFont);
            this.panel1.Controls.Add(this.buttonTCFont);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1027, 100);
            this.panel1.TabIndex = 1;
            // 
            // extButton2
            // 
            this.extButton2.BackColor2 = System.Drawing.Color.Red;
            this.extButton2.ButtonDisabledScaling = 0.5F;
            this.extButton2.GradientDirection = 90F;
            this.extButton2.Location = new System.Drawing.Point(192, 12);
            this.extButton2.MouseOverScaling = 1.3F;
            this.extButton2.MouseSelectedScaling = 1.3F;
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(75, 23);
            this.extButton2.TabIndex = 1;
            this.extButton2.Text = "T20";
            this.extButton2.UseVisualStyleBackColor = true;
            this.extButton2.Click += new System.EventHandler(this.extButton2_Click);
            // 
            // extButton3
            // 
            this.extButton3.BackColor2 = System.Drawing.Color.Red;
            this.extButton3.ButtonDisabledScaling = 0.5F;
            this.extButton3.GradientDirection = 90F;
            this.extButton3.Location = new System.Drawing.Point(4, 12);
            this.extButton3.MouseOverScaling = 1.3F;
            this.extButton3.MouseSelectedScaling = 1.3F;
            this.extButton3.Name = "extButton3";
            this.extButton3.Size = new System.Drawing.Size(75, 23);
            this.extButton3.TabIndex = 0;
            this.extButton3.Text = "T8.5";
            this.extButton3.UseVisualStyleBackColor = true;
            this.extButton3.Click += new System.EventHandler(this.extButton3_Click);
            // 
            // extButton1
            // 
            this.extButton1.BackColor2 = System.Drawing.Color.Red;
            this.extButton1.ButtonDisabledScaling = 0.5F;
            this.extButton1.GradientDirection = 90F;
            this.extButton1.Location = new System.Drawing.Point(99, 12);
            this.extButton1.MouseOverScaling = 1.3F;
            this.extButton1.MouseSelectedScaling = 1.3F;
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(75, 23);
            this.extButton1.TabIndex = 0;
            this.extButton1.Text = "T12";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click);
            // 
            // button1
            // 
            this.button1.BackColor2 = System.Drawing.Color.Red;
            this.button1.ButtonDisabledScaling = 0.5F;
            this.button1.GradientDirection = 90F;
            this.button1.Location = new System.Drawing.Point(4, 59);
            this.button1.MouseOverScaling = 1.3F;
            this.button1.MouseSelectedScaling = 1.3F;
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Enable";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor2 = System.Drawing.Color.Red;
            this.button2.ButtonDisabledScaling = 0.5F;
            this.button2.GradientDirection = 90F;
            this.button2.Location = new System.Drawing.Point(99, 59);
            this.button2.MouseOverScaling = 1.3F;
            this.button2.MouseSelectedScaling = 1.3F;
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 21;
            this.button2.Text = "Tabstyle";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor2 = System.Drawing.Color.Red;
            this.button3.ButtonDisabledScaling = 0.5F;
            this.button3.GradientDirection = 90F;
            this.button3.Location = new System.Drawing.Point(192, 59);
            this.button3.MouseOverScaling = 1.3F;
            this.button3.MouseSelectedScaling = 1.3F;
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 20;
            this.button3.Text = "FlatStyle";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonTCFont
            // 
            this.buttonTCFont.BackColor2 = System.Drawing.Color.Red;
            this.buttonTCFont.ButtonDisabledScaling = 0.5F;
            this.buttonTCFont.GradientDirection = 90F;
            this.buttonTCFont.Location = new System.Drawing.Point(312, 59);
            this.buttonTCFont.MouseOverScaling = 1.3F;
            this.buttonTCFont.MouseSelectedScaling = 1.3F;
            this.buttonTCFont.Name = "buttonTCFont";
            this.buttonTCFont.Size = new System.Drawing.Size(75, 23);
            this.buttonTCFont.TabIndex = 22;
            this.buttonTCFont.Text = "Font to TC";
            this.buttonTCFont.UseVisualStyleBackColor = true;
            this.buttonTCFont.Click += new System.EventHandler(this.buttonTCFont_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDragReorder = false;
            this.tabControl1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.tabControl1.Location = new System.Drawing.Point(0, 100);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.PaintTransparentColor = System.Drawing.Color.Transparent;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 400);
            this.tabControl1.TabBackgroundGradientDirection = 0F;
            this.tabControl1.TabControlBorderColor = System.Drawing.Color.LightGray;
            this.tabControl1.TabControlBorderColor2 = System.Drawing.Color.DarkGray;
            this.tabControl1.TabDisabledScaling = 0.5F;
            this.tabControl1.TabGradientDirection = 90F;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabMouseOverColor = System.Drawing.Color.White;
            this.tabControl1.TabMouseOverColor2 = System.Drawing.Color.White;
            this.tabControl1.TabNotSelectedBorderColor = System.Drawing.Color.Gray;
            this.tabControl1.TabNotSelectedColor = System.Drawing.Color.Gray;
            this.tabControl1.TabNotSelectedColor2 = System.Drawing.Color.Gray;
            this.tabControl1.TabSelectedColor = System.Drawing.Color.LightGray;
            this.tabControl1.TabSelectedColor2 = System.Drawing.Color.Gray;
            this.tabControl1.TabStyle = tabStyleSquare5;
            this.tabControl1.TextNotSelectedColor = System.Drawing.SystemColors.ControlText;
            this.tabControl1.TextSelectedColor = System.Drawing.SystemColors.ControlText;
            this.tabControl1.ThemeColors = new System.Drawing.Color[] {
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.tabControl1.ThemeColorSet = -1;
            // 
            // extButtonFormFont
            // 
            this.extButtonFormFont.BackColor2 = System.Drawing.Color.Red;
            this.extButtonFormFont.ButtonDisabledScaling = 0.5F;
            this.extButtonFormFont.GradientDirection = 90F;
            this.extButtonFormFont.Location = new System.Drawing.Point(312, 12);
            this.extButtonFormFont.MouseOverScaling = 1.3F;
            this.extButtonFormFont.MouseSelectedScaling = 1.3F;
            this.extButtonFormFont.Name = "extButtonFormFont";
            this.extButtonFormFont.Size = new System.Drawing.Size(75, 23);
            this.extButtonFormFont.TabIndex = 22;
            this.extButtonFormFont.Text = "Font to Form";
            this.extButtonFormFont.UseVisualStyleBackColor = true;
            this.extButtonFormFont.Click += new System.EventHandler(this.extButtonFormFont_Click);
            // 
            // TestTabControlCustom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 602);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "TestTabControlCustom";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.ExtTabControl tabControl1;
        private System.Windows.Forms.Panel panel1;
        private ExtendedControls.ExtButton extButton2;
        private ExtendedControls.ExtButton extButton1;
        private ExtendedControls.ExtButton button1;
        private ExtendedControls.ExtButton button2;
        private ExtendedControls.ExtButton button3;
        private ExtendedControls.ExtButton buttonTCFont;
        private ExtendedControls.ExtButton extButton3;
        private ExtendedControls.ExtButton extButtonFormFont;
    }
}