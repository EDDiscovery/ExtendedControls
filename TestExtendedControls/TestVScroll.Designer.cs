namespace DialogTest
{
    partial class TestVScroll
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
            this.extScrollBar1 = new ExtendedControls.ExtScrollBar();
            this.extScrollBar2 = new ExtendedControls.ExtScrollBar();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.SuspendLayout();
            // 
            // extScrollBar1
            // 
            this.extScrollBar1.ArrowBorderColor = System.Drawing.Color.YellowGreen;
            this.extScrollBar1.ArrowButtonColor = System.Drawing.Color.LightGray;
            this.extScrollBar1.ArrowColorScaling = 0.5F;
            this.extScrollBar1.ArrowDownDrawAngle = 270F;
            this.extScrollBar1.ArrowUpDrawAngle = 90F;
            this.extScrollBar1.BorderColor = System.Drawing.Color.White;
            this.extScrollBar1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extScrollBar1.HideScrollBar = false;
            this.extScrollBar1.LargeChange = 10;
            this.extScrollBar1.Location = new System.Drawing.Point(176, 43);
            this.extScrollBar1.Maximum = 100;
            this.extScrollBar1.Minimum = 0;
            this.extScrollBar1.MouseOverButtonColor = System.Drawing.Color.Green;
            this.extScrollBar1.MousePressedButtonColor = System.Drawing.Color.Red;
            this.extScrollBar1.Name = "extScrollBar1";
            this.extScrollBar1.Size = new System.Drawing.Size(20, 174);
            this.extScrollBar1.SliderColor = System.Drawing.Color.DarkGray;
            this.extScrollBar1.SmallChange = 1;
            this.extScrollBar1.TabIndex = 29;
            this.extScrollBar1.ThumbBorderColor = System.Drawing.Color.Yellow;
            this.extScrollBar1.ThumbButtonColor = System.Drawing.Color.LightSteelBlue;
            this.extScrollBar1.ThumbColorScaling = 0.5F;
            this.extScrollBar1.ThumbDrawAngle = 0F;
            this.extScrollBar1.Value = 0;
            this.extScrollBar1.ValueLimited = 0;
            this.extScrollBar1.ValueChanged += new System.EventHandler(this.extScrollBar1_ValueChanged);
            // 
            // extScrollBar2
            // 
            this.extScrollBar2.ArrowBorderColor = System.Drawing.Color.YellowGreen;
            this.extScrollBar2.ArrowButtonColor = System.Drawing.Color.LightGray;
            this.extScrollBar2.ArrowColorScaling = 0.5F;
            this.extScrollBar2.ArrowDownDrawAngle = 270F;
            this.extScrollBar2.ArrowUpDrawAngle = 90F;
            this.extScrollBar2.BorderColor = System.Drawing.Color.White;
            this.extScrollBar2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.extScrollBar2.HideScrollBar = false;
            this.extScrollBar2.LargeChange = 10;
            this.extScrollBar2.Location = new System.Drawing.Point(104, 43);
            this.extScrollBar2.Maximum = 100;
            this.extScrollBar2.Minimum = 0;
            this.extScrollBar2.MouseOverButtonColor = System.Drawing.Color.Green;
            this.extScrollBar2.MousePressedButtonColor = System.Drawing.Color.Red;
            this.extScrollBar2.Name = "extScrollBar2";
            this.extScrollBar2.Size = new System.Drawing.Size(20, 174);
            this.extScrollBar2.SliderColor = System.Drawing.Color.DarkGray;
            this.extScrollBar2.SmallChange = 1;
            this.extScrollBar2.TabIndex = 29;
            this.extScrollBar2.ThumbBorderColor = System.Drawing.Color.Yellow;
            this.extScrollBar2.ThumbButtonColor = System.Drawing.Color.LightSteelBlue;
            this.extScrollBar2.ThumbColorScaling = 0.5F;
            this.extScrollBar2.ThumbDrawAngle = 0F;
            this.extScrollBar2.Value = 0;
            this.extScrollBar2.ValueLimited = 0;
            this.extScrollBar2.ValueChanged += new System.EventHandler(this.extScrollBar2_ValueChanged);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(32, 77);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(20, 100);
            this.vScrollBar1.TabIndex = 8;
            this.vScrollBar1.ValueChanged += new System.EventHandler(this.vScrollBar1_ValueChanged);
            // 
            // extButton1
            // 
            this.extButton1.Location = new System.Drawing.Point(244, 94);
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(75, 23);
            this.extButton1.TabIndex = 30;
            this.extButton1.Text = "Th";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click);
            // 
            // TestVScroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 666);
            this.Controls.Add(this.extButton1);
            this.Controls.Add(this.extScrollBar1);
            this.Controls.Add(this.extScrollBar2);
            this.Controls.Add(this.vScrollBar1);
            this.Name = "TestVScroll";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.ExtScrollBar extScrollBar1;
        private ExtendedControls.ExtScrollBar extScrollBar2;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private ExtendedControls.ExtButton extButton1;
    }
}