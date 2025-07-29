namespace TestExtendedControls
{
    partial class TestImageControlScroll
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
            this.button2 = new System.Windows.Forms.Button();
            this.imageControlScroll1 = new ExtendedControls.ImageControlScroll();
            this.extScrollBar1 = new ExtendedControls.ExtScrollBar();
            this.imageControl1 = new ExtendedControls.ImageControl();
            this.imageControlScroll1.SuspendLayout();
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
            // imageControlScroll1
            // 
            this.imageControlScroll1.Controls.Add(this.extScrollBar1);
            this.imageControlScroll1.Controls.Add(this.imageControl1);
            this.imageControlScroll1.Location = new System.Drawing.Point(12, 12);
            this.imageControlScroll1.Name = "imageControlScroll1";
            this.imageControlScroll1.ScrollBarEnabled = true;
            this.imageControlScroll1.Size = new System.Drawing.Size(839, 602);
            this.imageControlScroll1.TabIndex = 2;
            this.imageControlScroll1.VerticalScrollBarDockRight = true;
            // 
            // extScrollBar1
            // 
            this.extScrollBar1.ArrowBorderColor = System.Drawing.Color.LightBlue;
            this.extScrollBar1.ArrowButtonColor = System.Drawing.Color.LightGray;
            this.extScrollBar1.ArrowDownDrawAngle = 270F;
            this.extScrollBar1.ArrowUpDrawAngle = 90F;
            this.extScrollBar1.BorderColor = System.Drawing.Color.White;
            this.extScrollBar1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extScrollBar1.HideScrollBar = false;
            this.extScrollBar1.LargeChange = 602;
            this.extScrollBar1.Location = new System.Drawing.Point(823, 0);
            this.extScrollBar1.Maximum = 270;
            this.extScrollBar1.Minimum = 0;
            this.extScrollBar1.MouseOverButtonColor = System.Drawing.Color.Green;
            this.extScrollBar1.MousePressedButtonColor = System.Drawing.Color.Red;
            this.extScrollBar1.Name = "extScrollBar1";
            this.extScrollBar1.Size = new System.Drawing.Size(16, 602);
            this.extScrollBar1.SliderColor = System.Drawing.Color.DarkGray;
            this.extScrollBar1.SmallChange = 1;
            this.extScrollBar1.TabIndex = 1;
            this.extScrollBar1.Text = "";
            this.extScrollBar1.ThumbBorderColor = System.Drawing.Color.Yellow;
            this.extScrollBar1.ThumbButtonColor = System.Drawing.Color.DarkBlue;
            this.extScrollBar1.ThumbDrawAngle = 0F;
            this.extScrollBar1.Value = 0;
            this.extScrollBar1.ValueLimited = 0;
            // 
            // imageControl1
            // 
            this.imageControl1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.imageControl1.BackgroundImage = global::TestExtendedControls.Properties.Resources.FleetCarrier;
            this.imageControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imageControl1.ImageBackgroundColor = System.Drawing.Color.Transparent;
            this.imageControl1.ImageDepth = 1;
            this.imageControl1.ImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imageControl1.ImageSize = new System.Drawing.Size(128, 128);
            this.imageControl1.ImageVisible = new bool[] {
        false};
            this.imageControl1.Location = new System.Drawing.Point(0, 0);
            this.imageControl1.Name = "imageControl1";
            this.imageControl1.Size = new System.Drawing.Size(823, 271);
            this.imageControl1.TabIndex = 0;
            this.imageControl1.Text = "imageControl1";
            // 
            // TestImageControlScroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 666);
            this.Controls.Add(this.imageControlScroll1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "TestImageControlScroll";
            this.Text = "Form1";
            this.Resize += new System.EventHandler(this.TestImageControl_Resize);
            this.imageControlScroll1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.ImageControl imageControl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private ExtendedControls.ImageControlScroll imageControlScroll1;
        private ExtendedControls.ExtScrollBar extScrollBar1;
    }
}