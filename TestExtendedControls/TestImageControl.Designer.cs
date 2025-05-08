namespace TestExtendedControls
{
    partial class TestImageControl
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
            this.button1 = new System.Windows.Forms.Button();
            this.imageControl1 = new ExtendedControls.ImageControl();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(857, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // imageControl1
            // 
            this.imageControl1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.imageControl1.BackgroundImage = global::TestExtendedControls.Properties.Resources.FleetCarrier;
            this.imageControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imageControl1.ImageBackgroundColor = System.Drawing.Color.Transparent;
            this.imageControl1.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imageControl1.ImageSize = new System.Drawing.Size(128, 128);
            this.imageControl1.Location = new System.Drawing.Point(12, 12);
            this.imageControl1.Name = "imageControl1";
            this.imageControl1.Size = new System.Drawing.Size(799, 613);
            this.imageControl1.TabIndex = 0;
            this.imageControl1.Text = "imageControl1";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(857, 144);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TestImageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 666);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.imageControl1);
            this.Name = "TestImageControl";
            this.Text = "Form1";
            this.Resize += new System.EventHandler(this.TestImageControl_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.ImageControl imageControl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}