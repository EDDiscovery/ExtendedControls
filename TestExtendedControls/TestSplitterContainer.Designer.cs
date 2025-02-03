namespace TestExtendedControls
{
    partial class TestSplitterContainer
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
            ExtendedControls.TabStyleSquare tabStyleSquare1 = new ExtendedControls.TabStyleSquare();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonExt1 = new ExtendedControls.ExtButton();
            this.buttonExt2 = new ExtendedControls.ExtButton();
            this.panelTop = new System.Windows.Forms.Panel();
            this.extButton3 = new ExtendedControls.ExtButton();
            this.extButton2 = new ExtendedControls.ExtButton();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.extTabControl1 = new ExtendedControls.ExtTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panelTop.SuspendLayout();
            this.extTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::TestExtendedControls.Properties.Resources.edsm32x32;
            this.panel1.Location = new System.Drawing.Point(106, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(48, 48);
            this.panel1.TabIndex = 0;
            // 
            // buttonExt1
            // 
            this.buttonExt1.BackColor = System.Drawing.Color.BurlyWood;
            this.buttonExt1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonExt1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.buttonExt1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonExt1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonExt1.Image = global::TestExtendedControls.Properties.Resources.galaxy;
            this.buttonExt1.Location = new System.Drawing.Point(78, 110);
            this.buttonExt1.Name = "buttonExt1";
            this.buttonExt1.Size = new System.Drawing.Size(48, 48);
            this.buttonExt1.TabIndex = 0;
            this.buttonExt1.UseVisualStyleBackColor = false;
            // 
            // buttonExt2
            // 
            this.buttonExt2.BackColor = System.Drawing.Color.NavajoWhite;
            this.buttonExt2.FlatAppearance.BorderColor = System.Drawing.Color.LightSalmon;
            this.buttonExt2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.buttonExt2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonExt2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonExt2.Image = global::TestExtendedControls.Properties.Resources.galaxy_gray;
            this.buttonExt2.Location = new System.Drawing.Point(134, 110);
            this.buttonExt2.Margin = new System.Windows.Forms.Padding(0);
            this.buttonExt2.Name = "buttonExt2";
            this.buttonExt2.Size = new System.Drawing.Size(48, 48);
            this.buttonExt2.TabIndex = 0;
            this.buttonExt2.Text = "Hello";
            this.buttonExt2.UseVisualStyleBackColor = false;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.extButton3);
            this.panelTop.Controls.Add(this.extButton2);
            this.panelTop.Controls.Add(this.extButton1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(731, 42);
            this.panelTop.TabIndex = 0;
            // 
            // extButton3
            // 
            this.extButton3.Location = new System.Drawing.Point(185, 13);
            this.extButton3.Name = "extButton3";
            this.extButton3.Size = new System.Drawing.Size(75, 23);
            this.extButton3.TabIndex = 1;
            this.extButton3.Text = "T16";
            this.extButton3.UseVisualStyleBackColor = true;
            this.extButton3.Click += new System.EventHandler(this.extButton2_Click);
            // 
            // extButton2
            // 
            this.extButton2.Location = new System.Drawing.Point(104, 13);
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(75, 23);
            this.extButton2.TabIndex = 1;
            this.extButton2.Text = "T14";
            this.extButton2.UseVisualStyleBackColor = true;
            this.extButton2.Click += new System.EventHandler(this.extButton2_Click_1);
            // 
            // extButton1
            // 
            this.extButton1.Location = new System.Drawing.Point(13, 13);
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(75, 23);
            this.extButton1.TabIndex = 0;
            this.extButton1.Text = "T12";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click);
            // 
            // extTabControl1
            // 
            this.extTabControl1.AllowDragReorder = false;
            this.extTabControl1.Controls.Add(this.tabPage1);
            this.extTabControl1.Controls.Add(this.tabPage2);
            this.extTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extTabControl1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extTabControl1.Location = new System.Drawing.Point(0, 42);
            this.extTabControl1.Name = "extTabControl1";
            this.extTabControl1.SelectedIndex = 0;
            this.extTabControl1.Size = new System.Drawing.Size(731, 449);
            this.extTabControl1.TabColorScaling = 0.5F;
            this.extTabControl1.TabControlBorderBrightColor = System.Drawing.Color.LightGray;
            this.extTabControl1.TabControlBorderColor = System.Drawing.Color.DarkGray;
            this.extTabControl1.TabDisabledScaling = 0.5F;
            this.extTabControl1.TabIndex = 1;
            this.extTabControl1.TabMouseOverColor = System.Drawing.Color.White;
            this.extTabControl1.TabNotSelectedBorderColor = System.Drawing.Color.Gray;
            this.extTabControl1.TabNotSelectedColor = System.Drawing.Color.Gray;
            this.extTabControl1.TabSelectedColor = System.Drawing.Color.LightGray;
            this.extTabControl1.TabStyle = tabStyleSquare1;
            this.extTabControl1.TextNotSelectedColor = System.Drawing.SystemColors.ControlText;
            this.extTabControl1.TextSelectedColor = System.Drawing.SystemColors.ControlText;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(723, 423);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // TestSplitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(731, 491);
            this.Controls.Add(this.extTabControl1);
            this.Controls.Add(this.panelTop);
            this.Name = "TestSplitter";
            this.Text = "Splitter";
            this.panelTop.ResumeLayout(false);
            this.extTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ExtendedControls.ExtButton buttonExt1;
        private ExtendedControls.ExtButton buttonExt2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelTop;
        private ExtendedControls.ExtButton extButton2;
        private ExtendedControls.ExtButton extButton1;
        private ExtendedControls.ExtButton extButton3;
        private ExtendedControls.ExtTabControl extTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}