namespace TestExtendedControls
{
    partial class TestLabel
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.extButton3 = new ExtendedControls.ExtButton();
            this.extButton2 = new ExtendedControls.ExtButton();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.extFixedWidthLabel1 = new ExtendedControls.ExtLabelAutoHeight();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(42, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "This is an autosized one";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(321, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "This is an fixed size one";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // extButton3
            // 
            this.extButton3.Location = new System.Drawing.Point(597, 99);
            this.extButton3.Name = "extButton3";
            this.extButton3.Size = new System.Drawing.Size(75, 23);
            this.extButton3.TabIndex = 2;
            this.extButton3.Text = "toggle font";
            this.extButton3.UseVisualStyleBackColor = true;
            this.extButton3.Click += new System.EventHandler(this.extButton3_Click);
            // 
            // extButton2
            // 
            this.extButton2.Location = new System.Drawing.Point(678, 65);
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(75, 23);
            this.extButton2.TabIndex = 2;
            this.extButton2.Text = "toggle text";
            this.extButton2.UseVisualStyleBackColor = true;
            this.extButton2.Click += new System.EventHandler(this.extButton2_Click);
            // 
            // extButton1
            // 
            this.extButton1.Location = new System.Drawing.Point(597, 65);
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(75, 23);
            this.extButton1.TabIndex = 2;
            this.extButton1.Text = "toggle width";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click_1);
            // 
            // extFixedWidthLabel1
            // 
            this.extFixedWidthLabel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.extFixedWidthLabel1.Location = new System.Drawing.Point(45, 65);
            this.extFixedWidthLabel1.Name = "extFixedWidthLabel1";
            this.extFixedWidthLabel1.Size = new System.Drawing.Size(100, 91);
            this.extFixedWidthLabel1.TabIndex = 1;
            this.extFixedWidthLabel1.Text = "100 width label with a fixed width but delta height and another1 another2 another" +
    "3 another4 another5 another6 another7 endother";
            this.extFixedWidthLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TestLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 509);
            this.Controls.Add(this.extButton3);
            this.Controls.Add(this.extButton2);
            this.Controls.Add(this.extButton1);
            this.Controls.Add(this.extFixedWidthLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TestLabel";
            this.Text = "TestAutoComplete";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private ExtendedControls.ExtLabelAutoHeight extFixedWidthLabel1;
        private ExtendedControls.ExtButton extButton1;
        private ExtendedControls.ExtButton extButton2;
        private System.Windows.Forms.Label label2;
        private ExtendedControls.ExtButton extButton3;
    }
}