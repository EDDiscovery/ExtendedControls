namespace DialogTest
{
    partial class TestConditionUC
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
            this.buttonExt1 = new ExtendedControls.ExtButton();
            this.buttonExt2 = new ExtendedControls.ExtButton();
            this.buttonExt3 = new ExtendedControls.ExtButton();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.extButton2 = new ExtendedControls.ExtButton();
            this.extButton3 = new ExtendedControls.ExtButton();
            this.SuspendLayout();
            // 
            // buttonExt1
            // 
            this.buttonExt1.Location = new System.Drawing.Point(12, 1);
            this.buttonExt1.Name = "buttonExt1";
            this.buttonExt1.Size = new System.Drawing.Size(75, 23);
            this.buttonExt1.TabIndex = 0;
            this.buttonExt1.Text = "EList-T12";
            this.buttonExt1.UseVisualStyleBackColor = true;
            this.buttonExt1.Click += new System.EventHandler(this.buttonEvents);
            // 
            // buttonExt2
            // 
            this.buttonExt2.Location = new System.Drawing.Point(102, 1);
            this.buttonExt2.Name = "buttonExt2";
            this.buttonExt2.Size = new System.Drawing.Size(91, 23);
            this.buttonExt2.TabIndex = 1;
            this.buttonExt2.Text = "Condition-T12";
            this.buttonExt2.UseVisualStyleBackColor = true;
            this.buttonExt2.Click += new System.EventHandler(this.buttonCondition);
            // 
            // buttonExt3
            // 
            this.buttonExt3.Location = new System.Drawing.Point(210, 1);
            this.buttonExt3.Name = "buttonExt3";
            this.buttonExt3.Size = new System.Drawing.Size(142, 23);
            this.buttonExt3.TabIndex = 1;
            this.buttonExt3.Text = "ConditionL-T12";
            this.buttonExt3.UseVisualStyleBackColor = true;
            this.buttonExt3.Click += new System.EventHandler(this.buttonExt3_Click);
            // 
            // extButton1
            // 
            this.extButton1.Location = new System.Drawing.Point(12, 30);
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(75, 23);
            this.extButton1.TabIndex = 0;
            this.extButton1.Text = "EList-T20";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click);
            // 
            // extButton2
            // 
            this.extButton2.Location = new System.Drawing.Point(102, 30);
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(91, 23);
            this.extButton2.TabIndex = 1;
            this.extButton2.Text = "Condition-T20";
            this.extButton2.UseVisualStyleBackColor = true;
            this.extButton2.Click += new System.EventHandler(this.extButton2_Click);
            // 
            // extButton3
            // 
            this.extButton3.Location = new System.Drawing.Point(210, 30);
            this.extButton3.Name = "extButton3";
            this.extButton3.Size = new System.Drawing.Size(142, 23);
            this.extButton3.TabIndex = 1;
            this.extButton3.Text = "ConditionL-T20";
            this.extButton3.UseVisualStyleBackColor = true;
            this.extButton3.Click += new System.EventHandler(this.extButton3_Click);
            // 
            // TestConditionUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 726);
            this.Controls.Add(this.extButton3);
            this.Controls.Add(this.buttonExt3);
            this.Controls.Add(this.extButton2);
            this.Controls.Add(this.buttonExt2);
            this.Controls.Add(this.extButton1);
            this.Controls.Add(this.buttonExt1);
            this.Name = "TestConditionUC";
            this.Text = "Condition";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private ExtendedControls.ExtButton buttonExt1;
        private ExtendedControls.ExtButton buttonExt2;
        private ExtendedControls.ExtButton buttonExt3;
        private ExtendedControls.ExtButton extButton1;
        private ExtendedControls.ExtButton extButton2;
        private ExtendedControls.ExtButton extButton3;
    }
}