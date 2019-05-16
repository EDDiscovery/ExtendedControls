namespace DialogTest
{
    partial class TestButtons
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
            this.extButton2 = new ExtendedControls.ExtButton();
            this.extButton3 = new ExtendedControls.ExtButton();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.compositeButton1 = new ExtendedControls.CompositeButton();
            this.buttonExt1 = new ExtendedControls.ExtButton();
            this.buttonExt2 = new ExtendedControls.ExtButton();
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
            // extButton2
            // 
            this.extButton2.Location = new System.Drawing.Point(127, 12);
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(75, 23);
            this.extButton2.TabIndex = 4;
            this.extButton2.Text = "400x400";
            this.extButton2.UseVisualStyleBackColor = true;
            this.extButton2.Click += new System.EventHandler(this.button2_Click);
            // 
            // extButton3
            // 
            this.extButton3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.extButton3.Image = global::DialogTest.Properties.Resources.galaxy;
            this.extButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.extButton3.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.extButton3.Location = new System.Drawing.Point(12, 212);
            this.extButton3.Name = "extButton3";
            this.extButton3.Size = new System.Drawing.Size(84, 45);
            this.extButton3.TabIndex = 3;
            this.extButton3.Text = "128x128";
            this.extButton3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.extButton3.UseVisualStyleBackColor = true;
            this.extButton3.Click += new System.EventHandler(this.button1_Click);
            // 
            // extButton1
            // 
            this.extButton1.Location = new System.Drawing.Point(12, 12);
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(75, 23);
            this.extButton1.TabIndex = 3;
            this.extButton1.Text = "128x128";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // compositeButton1
            // 
            this.compositeButton1.BackColor = System.Drawing.Color.Azure;
            this.compositeButton1.BackgroundImage = global::DialogTest.Properties.Resources.edlogo24;
            this.compositeButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.compositeButton1.Buttons = new ExtendedControls.ExtButton[] {
        this.buttonExt1,
        this.buttonExt2};
            this.compositeButton1.ButtonSpacing = 8;
            this.compositeButton1.Decals = new System.Windows.Forms.Panel[] {
        this.panel1};
            this.compositeButton1.DecalSpacing = 8;
            this.compositeButton1.Location = new System.Drawing.Point(252, 12);
            this.compositeButton1.MinimumDecalButtonVerticalSpacing = 8;
            this.compositeButton1.Name = "compositeButton1";
            this.compositeButton1.Padding = new System.Windows.Forms.Padding(10);
            this.compositeButton1.Size = new System.Drawing.Size(261, 168);
            this.compositeButton1.TabIndex = 1;
            this.compositeButton1.Text = "This is a test";
            this.compositeButton1.TextBackColor = System.Drawing.Color.Transparent;
            this.compositeButton1.TextBackground = System.Drawing.Color.Transparent;
            this.compositeButton1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // TestButtons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1060, 623);
            this.Controls.Add(this.extButton2);
            this.Controls.Add(this.extButton3);
            this.Controls.Add(this.extButton1);
            this.Controls.Add(this.compositeButton1);
            this.Name = "TestButtons";
            this.Text = "TestCompositeButton";
            this.ResumeLayout(false);

        }

        #endregion
        private ExtendedControls.CompositeButton compositeButton1;
        private ExtendedControls.ExtButton buttonExt1;
        private ExtendedControls.ExtButton buttonExt2;
        private System.Windows.Forms.Panel panel1;
        private ExtendedControls.ExtButton extButton1;
        private ExtendedControls.ExtButton extButton2;
        private ExtendedControls.ExtButton extButton3;
    }
}