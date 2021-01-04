namespace TestExtendedControls
{
    partial class TestTextBoxesRTF
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
            this.buttonExt1 = new ExtendedControls.ExtButton();
            this.buttonExt2 = new ExtendedControls.ExtButton();
            this.extRichTextBox1 = new ExtendedControls.ExtRichTextBox();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::TestExtendedControls.Properties.Resources.edsm32x32;
            this.panel1.Location = new System.Drawing.Point(106, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(48, 48);
            this.panel1.TabIndex = 0;
            // 
            // extButton2
            // 
            this.extButton2.Location = new System.Drawing.Point(733, 53);
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(75, 23);
            this.extButton2.TabIndex = 68;
            this.extButton2.Text = "clear";
            this.extButton2.UseVisualStyleBackColor = true;
            this.extButton2.Click += new System.EventHandler(this.extButton2_Click);
            // 
            // extButton1
            // 
            this.extButton1.Location = new System.Drawing.Point(733, 12);
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(75, 23);
            this.extButton1.TabIndex = 68;
            this.extButton1.Text = "add text";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click);
            // 
            // buttonExt1
            // 
            this.buttonExt1.BackColor = System.Drawing.Color.BurlyWood;
            this.buttonExt1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonExt1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.buttonExt1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonExt1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonExt1.Image = global::TestExtendedControls.Properties.Resources.galaxy;
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
            this.buttonExt2.Image = global::TestExtendedControls.Properties.Resources.galaxy_gray;
            this.buttonExt2.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonExt2.Location = new System.Drawing.Point(134, 110);
            this.buttonExt2.Margin = new System.Windows.Forms.Padding(0);
            this.buttonExt2.Name = "buttonExt2";
            this.buttonExt2.Size = new System.Drawing.Size(48, 48);
            this.buttonExt2.TabIndex = 0;
            this.buttonExt2.Text = "Hello";
            this.buttonExt2.UseVisualStyleBackColor = false;
            // 
            // extRichTextBox1
            // 
            this.extRichTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.extRichTextBox1.BorderColor = System.Drawing.Color.OrangeRed;
            this.extRichTextBox1.BorderColorScaling = 0.5F;
            this.extRichTextBox1.HideScrollBar = true;
            this.extRichTextBox1.Location = new System.Drawing.Point(41, 12);
            this.extRichTextBox1.Name = "extRichTextBox1";
            this.extRichTextBox1.ReadOnly = false;
            this.extRichTextBox1.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang2057{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft S" +
    "ans Serif;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs17\\par\r\n}\r\n";
            this.extRichTextBox1.ScrollBarArrowBorderColor = System.Drawing.Color.LightBlue;
            this.extRichTextBox1.ScrollBarArrowButtonColor = System.Drawing.Color.LightGray;
            this.extRichTextBox1.ScrollBarBackColor = System.Drawing.SystemColors.Control;
            this.extRichTextBox1.ScrollBarBorderColor = System.Drawing.Color.White;
            this.extRichTextBox1.ScrollBarFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extRichTextBox1.ScrollBarForeColor = System.Drawing.SystemColors.ControlText;
            this.extRichTextBox1.ScrollBarMouseOverButtonColor = System.Drawing.Color.Green;
            this.extRichTextBox1.ScrollBarMousePressedButtonColor = System.Drawing.Color.Red;
            this.extRichTextBox1.ScrollBarSliderColor = System.Drawing.Color.DarkGray;
            this.extRichTextBox1.ScrollBarThumbBorderColor = System.Drawing.Color.Yellow;
            this.extRichTextBox1.ScrollBarThumbButtonColor = System.Drawing.Color.DarkBlue;
            this.extRichTextBox1.ShowLineCount = false;
            this.extRichTextBox1.Size = new System.Drawing.Size(640, 419);
            this.extRichTextBox1.TabIndex = 30;
            this.extRichTextBox1.TextBoxBackColor = System.Drawing.Color.Red;
            this.extRichTextBox1.TextBoxForeColor = System.Drawing.SystemColors.ControlText;
            // 
            // TestTextBoxes2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(833, 524);
            this.Controls.Add(this.extButton2);
            this.Controls.Add(this.extButton1);
            this.Controls.Add(this.extRichTextBox1);
            this.Name = "TestTextBoxes2";
            this.Text = "TestCompositeButton";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TestTextBoxes_Paint);
            this.ResumeLayout(false);

        }

        #endregion
        private ExtendedControls.ExtButton buttonExt1;
        private ExtendedControls.ExtButton buttonExt2;
        private System.Windows.Forms.Panel panel1;
        private ExtendedControls.ExtButton extButton1;
        private ExtendedControls.ExtButton extButton2;
        private ExtendedControls.ExtRichTextBox extRichTextBox1;
    }
}