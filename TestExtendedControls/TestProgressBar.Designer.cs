namespace TestExtendedControls
{
    partial class TestProgressBar
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.extProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.extButton2 = new ExtendedControls.ExtButton();
            this.extButton3 = new ExtendedControls.ExtButton();
            this.extButton4 = new ExtendedControls.ExtButton();
            this.extButton5 = new ExtendedControls.ExtButton();
            this.SuspendLayout();
            // 
            // extProgressBar1
            // 
            this.extProgressBar1.Location = new System.Drawing.Point(12, 92);
            this.extProgressBar1.Name = "extProgressBar1";
            this.extProgressBar1.Size = new System.Drawing.Size(532, 23);
            this.extProgressBar1.TabIndex = 0;
            // 
            // extButton1
            // 
            this.extButton1.Location = new System.Drawing.Point(28, 30);
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(75, 23);
            this.extButton1.TabIndex = 1;
            this.extButton1.Text = "0";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click);
            // 
            // extButton2
            // 
            this.extButton2.Location = new System.Drawing.Point(109, 30);
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(75, 23);
            this.extButton2.TabIndex = 2;
            this.extButton2.Text = "25";
            this.extButton2.UseVisualStyleBackColor = true;
            this.extButton2.Click += new System.EventHandler(this.extButton2_Click);
            // 
            // extButton3
            // 
            this.extButton3.Location = new System.Drawing.Point(190, 30);
            this.extButton3.Name = "extButton3";
            this.extButton3.Size = new System.Drawing.Size(75, 23);
            this.extButton3.TabIndex = 3;
            this.extButton3.Text = "50";
            this.extButton3.UseVisualStyleBackColor = true;
            this.extButton3.Click += new System.EventHandler(this.extButton3_Click);
            // 
            // extButton4
            // 
            this.extButton4.Location = new System.Drawing.Point(271, 30);
            this.extButton4.Name = "extButton4";
            this.extButton4.Size = new System.Drawing.Size(75, 23);
            this.extButton4.TabIndex = 4;
            this.extButton4.Text = "75";
            this.extButton4.UseVisualStyleBackColor = true;
            this.extButton4.Click += new System.EventHandler(this.extButton4_Click);
            // 
            // extButton5
            // 
            this.extButton5.Location = new System.Drawing.Point(352, 30);
            this.extButton5.Name = "extButton5";
            this.extButton5.Size = new System.Drawing.Size(75, 23);
            this.extButton5.TabIndex = 5;
            this.extButton5.Text = "100";
            this.extButton5.UseVisualStyleBackColor = true;
            this.extButton5.Click += new System.EventHandler(this.extButton5_Click);
            // 
            // TestProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 509);
            this.Controls.Add(this.extButton5);
            this.Controls.Add(this.extButton4);
            this.Controls.Add(this.extButton3);
            this.Controls.Add(this.extButton2);
            this.Controls.Add(this.extButton1);
            this.Controls.Add(this.extProgressBar1);
            this.Name = "TestProgressBar";
            this.Text = "TestAutoComplete";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar extProgressBar1;
        private ExtendedControls.ExtButton extButton1;
        private ExtendedControls.ExtButton extButton2;
        private ExtendedControls.ExtButton extButton3;
        private ExtendedControls.ExtButton extButton4;
        private ExtendedControls.ExtButton extButton5;
    }
}