namespace TestExtendedControls
{
    partial class TestPanelScroll
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
            this.extPanelScroll1 = new ExtendedControls.ExtPanelScroll();
            this.extScrollBar1 = new ExtendedControls.ExtScrollBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.extButton2 = new ExtendedControls.ExtButton();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.extPanelScroll1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // extPanelScroll1
            // 
            this.extPanelScroll1.Controls.Add(this.extScrollBar1);
            this.extPanelScroll1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extPanelScroll1.FlowControlsLeftToRight = false;
            this.extPanelScroll1.Location = new System.Drawing.Point(0, 56);
            this.extPanelScroll1.Name = "extPanelScroll1";
            this.extPanelScroll1.Size = new System.Drawing.Size(879, 610);
            this.extPanelScroll1.TabIndex = 1;
            this.extPanelScroll1.VerticalScrollBarDockRight = true;
            // 
            // extScrollBar1
            // 
            this.extScrollBar1.ArrowBorderColor = System.Drawing.Color.LightBlue;
            this.extScrollBar1.ArrowButtonColor = System.Drawing.Color.LightGray;
            this.extScrollBar1.ArrowColorScaling = 0.5F;
            this.extScrollBar1.ArrowDownDrawAngle = 270F;
            this.extScrollBar1.ArrowUpDrawAngle = 90F;
            this.extScrollBar1.BorderColor = System.Drawing.Color.White;
            this.extScrollBar1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extScrollBar1.HideScrollBar = false;
            this.extScrollBar1.LargeChange = 10;
            this.extScrollBar1.Location = new System.Drawing.Point(863, 0);
            this.extScrollBar1.Maximum = -600;
            this.extScrollBar1.Minimum = 0;
            this.extScrollBar1.MouseOverButtonColor = System.Drawing.Color.Green;
            this.extScrollBar1.MousePressedButtonColor = System.Drawing.Color.Red;
            this.extScrollBar1.Name = "extScrollBar1";
            this.extScrollBar1.Size = new System.Drawing.Size(16, 610);
            this.extScrollBar1.SliderColor = System.Drawing.Color.DarkGray;
            this.extScrollBar1.SmallChange = 1;
            this.extScrollBar1.TabIndex = 0;
            this.extScrollBar1.Text = "extScrollBar1";
            this.extScrollBar1.ThumbBorderColor = System.Drawing.Color.Yellow;
            this.extScrollBar1.ThumbButtonColor = System.Drawing.Color.DarkBlue;
            this.extScrollBar1.ThumbColorScaling = 0.5F;
            this.extScrollBar1.ThumbDrawAngle = 0F;
            this.extScrollBar1.Value = -600;
            this.extScrollBar1.ValueLimited = -600;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.extButton2);
            this.panel1.Controls.Add(this.extButton1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(879, 56);
            this.panel1.TabIndex = 1;
            // 
            // extButton2
            // 
            this.extButton2.Location = new System.Drawing.Point(209, 13);
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(75, 23);
            this.extButton2.TabIndex = 1;
            this.extButton2.Text = "T20";
            this.extButton2.UseVisualStyleBackColor = true;
            this.extButton2.Click += new System.EventHandler(this.extButton2_Click);
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
            // TestPanelScroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 666);
            this.Controls.Add(this.extPanelScroll1);
            this.Controls.Add(this.panel1);
            this.Name = "TestPanelScroll";
            this.Text = "Form1";
            this.extPanelScroll1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ExtendedControls.ExtPanelScroll extPanelScroll1;
        private ExtendedControls.ExtScrollBar extScrollBar1;
        private System.Windows.Forms.Panel panel1;
        private ExtendedControls.ExtButton extButton2;
        private ExtendedControls.ExtButton extButton1;
    }
}