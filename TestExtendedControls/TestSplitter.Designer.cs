namespace DialogTest
{
    partial class TestSplitter
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
            this.buttonExt1 = new ExtendedControls.ExtButton();
            this.buttonExt2 = new ExtendedControls.ExtButton();
            this.panelPlayfield = new System.Windows.Forms.Panel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.extButton2 = new ExtendedControls.ExtButton();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::DialogTest.Properties.Resources.edsm32x32;
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
            this.buttonExt1.Image = global::DialogTest.Properties.Resources.galaxy;
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
            this.buttonExt2.Image = global::DialogTest.Properties.Resources.galaxy_gray;
            this.buttonExt2.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonExt2.Location = new System.Drawing.Point(134, 110);
            this.buttonExt2.Margin = new System.Windows.Forms.Padding(0);
            this.buttonExt2.Name = "buttonExt2";
            this.buttonExt2.Size = new System.Drawing.Size(48, 48);
            this.buttonExt2.TabIndex = 0;
            this.buttonExt2.Text = "Hello";
            this.buttonExt2.UseVisualStyleBackColor = false;
            // 
            // panelPlayfield
            // 
            this.panelPlayfield.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPlayfield.Location = new System.Drawing.Point(0, 42);
            this.panelPlayfield.Name = "panelPlayfield";
            this.panelPlayfield.Size = new System.Drawing.Size(406, 244);
            this.panelPlayfield.TabIndex = 2;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.extButton2);
            this.panelTop.Controls.Add(this.extButton1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(406, 42);
            this.panelTop.TabIndex = 0;
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
            // extButton2
            // 
            this.extButton2.Location = new System.Drawing.Point(104, 13);
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(75, 23);
            this.extButton2.TabIndex = 1;
            this.extButton2.Text = "T16";
            this.extButton2.UseVisualStyleBackColor = true;
            this.extButton2.Click += new System.EventHandler(this.extButton2_Click);
            // 
            // TestSplitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(406, 286);
            this.Controls.Add(this.panelPlayfield);
            this.Controls.Add(this.panelTop);
            this.Name = "TestSplitter";
            this.Text = "Splitter";
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ExtendedControls.ExtButton buttonExt1;
        private ExtendedControls.ExtButton buttonExt2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelPlayfield;
        private System.Windows.Forms.Panel panelTop;
        private ExtendedControls.ExtButton extButton2;
        private ExtendedControls.ExtButton extButton1;
    }
}