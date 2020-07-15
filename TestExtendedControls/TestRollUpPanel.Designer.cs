namespace TestExtendedControls
{
    partial class TestRollUpPanel
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
            this.extButton1 = new ExtendedControls.ExtButton();
            this.rolluppanel = new ExtendedControls.ExtPanelRollUp();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.rolluppanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.extButton2);
            this.panel1.Controls.Add(this.extButton1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(941, 100);
            this.panel1.TabIndex = 1;
            // 
            // extButton2
            // 
            this.extButton2.Location = new System.Drawing.Point(94, 0);
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(75, 23);
            this.extButton2.TabIndex = 4;
            this.extButton2.Text = "t20";
            this.extButton2.UseVisualStyleBackColor = true;
            this.extButton2.Click += new System.EventHandler(this.extButton2_Click);
            // 
            // extButton1
            // 
            this.extButton1.Location = new System.Drawing.Point(0, 0);
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(75, 23);
            this.extButton1.TabIndex = 3;
            this.extButton1.Text = "t12";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click);
            // 
            // rolluppanel
            // 
            this.rolluppanel.AutoSize = true;
            this.rolluppanel.Controls.Add(this.flowLayoutPanel1);
            this.rolluppanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.rolluppanel.HiddenMarkerWidth = 0;
            this.rolluppanel.Location = new System.Drawing.Point(0, 0);
            this.rolluppanel.Name = "rolluppanel";
            this.rolluppanel.PinState = true;
            this.rolluppanel.RolledUpHeight = 5;
            this.rolluppanel.RollUpAnimationTime = 2000;
            this.rolluppanel.RollUpDelay = 1000;
            this.rolluppanel.SecondHiddenMarkerWidth = 0;
            this.rolluppanel.ShowHiddenMarker = true;
            this.rolluppanel.Size = new System.Drawing.Size(941, 40);
            this.rolluppanel.TabIndex = 0;
            this.rolluppanel.UnrollHoverDelay = 1000;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.Location = new System.Drawing.Point(106, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 40);
            this.label2.TabIndex = 3;
            this.label2.Text = "Just to show roll up";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Just to show roll up";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(941, 40);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // TestRollUpPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 518);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rolluppanel);
            this.Name = "TestRollUpPanel";
            this.Text = "TestForm2";
            this.panel1.ResumeLayout(false);
            this.rolluppanel.ResumeLayout(false);
            this.rolluppanel.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExtendedControls.ExtPanelRollUp rolluppanel;
        private ExtendedControls.ExtButton extButton2;
        private ExtendedControls.ExtButton extButton1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}