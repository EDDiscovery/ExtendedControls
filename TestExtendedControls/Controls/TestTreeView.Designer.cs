namespace TestExtendedControls
{
    partial class TestTreeView
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
            this.extButton2 = new ExtendedControls.ExtButton();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.extTreeView1 = new ExtendedControls.ExtTreeView();
            this.SuspendLayout();
            // 
            // extButton2
            // 
            this.extButton2.BackColor2 = System.Drawing.Color.Red;
            this.extButton2.ButtonDisabledScaling = 0.5F;
            this.extButton2.GradientDirection = 90F;
            this.extButton2.Location = new System.Drawing.Point(157, 12);
            this.extButton2.MouseOverScaling = 1.3F;
            this.extButton2.MouseSelectedScaling = 1.3F;
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(123, 23);
            this.extButton2.TabIndex = 1;
            this.extButton2.Text = "Thick Scrollbars";
            this.extButton2.UseVisualStyleBackColor = true;
            this.extButton2.Click += new System.EventHandler(this.extButton2_Click);
            // 
            // extButton1
            // 
            this.extButton1.BackColor2 = System.Drawing.Color.Red;
            this.extButton1.ButtonDisabledScaling = 0.5F;
            this.extButton1.GradientDirection = 90F;
            this.extButton1.Location = new System.Drawing.Point(28, 12);
            this.extButton1.MouseOverScaling = 1.3F;
            this.extButton1.MouseSelectedScaling = 1.3F;
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(123, 23);
            this.extButton1.TabIndex = 1;
            this.extButton1.Text = "Skinny Scrollbars";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click);
            // 
            // extTreeView1
            // 
            this.extTreeView1.BorderColor = System.Drawing.Color.Transparent;
            this.extTreeView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.extTreeView1.HideScrollBar = true;
            this.extTreeView1.Location = new System.Drawing.Point(28, 70);
            this.extTreeView1.Name = "extTreeView1";
            this.extTreeView1.ScrollBarArrowBorderColor = System.Drawing.Color.LightBlue;
            this.extTreeView1.ScrollBarArrowButtonColor = System.Drawing.Color.LightGray;
            this.extTreeView1.ScrollBarBackColor = System.Drawing.SystemColors.Control;
            this.extTreeView1.ScrollBarBorderColor = System.Drawing.Color.White;
            this.extTreeView1.ScrollBarFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extTreeView1.ScrollBarForeColor = System.Drawing.SystemColors.ControlText;
            this.extTreeView1.ScrollBarMouseOverButtonColor = System.Drawing.Color.Green;
            this.extTreeView1.ScrollBarMousePressedButtonColor = System.Drawing.Color.Red;
            this.extTreeView1.ScrollBarSliderColor = System.Drawing.Color.DarkGray;
            this.extTreeView1.ScrollBarThumbBorderColor = System.Drawing.Color.Yellow;
            this.extTreeView1.ScrollBarThumbButtonColor = System.Drawing.Color.DarkBlue;
            this.extTreeView1.ScrollBarWidth = 48;
            this.extTreeView1.ShowLineCount = false;
            this.extTreeView1.ShowLines = true;
            this.extTreeView1.ShowPlusMinus = true;
            this.extTreeView1.ShowRootLines = true;
            this.extTreeView1.Size = new System.Drawing.Size(665, 393);
            this.extTreeView1.TabIndex = 0;
            this.extTreeView1.TreeViewBackColor = System.Drawing.SystemColors.Control;
            this.extTreeView1.TreeViewForeColor = System.Drawing.SystemColors.ControlText;
            // 
            // TestTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 509);
            this.Controls.Add(this.extButton2);
            this.Controls.Add(this.extButton1);
            this.Controls.Add(this.extTreeView1);
            this.Name = "TestTreeView";
            this.Text = "TestAutoComplete";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private ExtendedControls.ExtTreeView extTreeView1;
        private ExtendedControls.ExtButton extButton1;
        private ExtendedControls.ExtButton extButton2;
    }
}