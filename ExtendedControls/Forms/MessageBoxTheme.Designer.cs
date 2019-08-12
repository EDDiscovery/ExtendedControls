namespace ExtendedControls
{
    partial class MessageBoxTheme
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
            this.panelOuter = new System.Windows.Forms.Panel();
            this.panelIconText = new System.Windows.Forms.Panel();
            this.themeTextBox = new ExtendedControls.ExtRichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelIcon = new System.Windows.Forms.Panel();
            this.panelButs = new System.Windows.Forms.Panel();
            this.buttonExt1 = new ExtendedControls.ExtButton();
            this.buttonExt2 = new ExtendedControls.ExtButton();
            this.buttonExt3 = new ExtendedControls.ExtButton();
            this.panelGap = new System.Windows.Forms.Panel();
            this.labelCaption = new System.Windows.Forms.Label();
            this.panelOuter.SuspendLayout();
            this.panelIconText.SuspendLayout();
            this.themeTextBox.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelButs.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelOuter
            // 
            this.panelOuter.AutoSize = true;
            this.panelOuter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOuter.Controls.Add(this.panelIconText);
            this.panelOuter.Controls.Add(this.panelButs);
            this.panelOuter.Controls.Add(this.panelGap);
            this.panelOuter.Controls.Add(this.labelCaption);
            this.panelOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOuter.Location = new System.Drawing.Point(0, 0);
            this.panelOuter.Margin = new System.Windows.Forms.Padding(0);
            this.panelOuter.Name = "panelOuter";
            this.panelOuter.Size = new System.Drawing.Size(667, 461);
            this.panelOuter.TabIndex = 5;
            this.panelOuter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveMouseDown);
            this.panelOuter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveMouseUp);
            // 
            // panelIconText
            // 
            this.panelIconText.Controls.Add(this.themeTextBox);
            this.panelIconText.Controls.Add(this.panelLeft);
            this.panelIconText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelIconText.Location = new System.Drawing.Point(0, 27);
            this.panelIconText.Name = "panelIconText";
            this.panelIconText.Size = new System.Drawing.Size(665, 384);
            this.panelIconText.TabIndex = 3;
            // 
            // themeTextBox
            // 
            this.themeTextBox.BorderColor = System.Drawing.Color.Transparent;
            this.themeTextBox.BorderColorScaling = 0.5F;
            this.themeTextBox.Controls.Add(this.button1);
            this.themeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.themeTextBox.HideScrollBar = true;
            this.themeTextBox.Location = new System.Drawing.Point(67, 0);
            this.themeTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.themeTextBox.Name = "themeTextBox";
            this.themeTextBox.ReadOnly = false;
            this.themeTextBox.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang2057{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft S" +
    "ans Serif;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs17\\par\r\n}\r\n";
            this.themeTextBox.ScrollBarArrowBorderColor = System.Drawing.Color.LightBlue;
            this.themeTextBox.ScrollBarArrowButtonColor = System.Drawing.Color.LightGray;
            this.themeTextBox.ScrollBarBackColor = System.Drawing.SystemColors.Control;
            this.themeTextBox.ScrollBarBorderColor = System.Drawing.Color.White;
            this.themeTextBox.ScrollBarFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.themeTextBox.ScrollBarForeColor = System.Drawing.SystemColors.ControlText;
            this.themeTextBox.ScrollBarMouseOverButtonColor = System.Drawing.Color.Green;
            this.themeTextBox.ScrollBarMousePressedButtonColor = System.Drawing.Color.Red;
            this.themeTextBox.ScrollBarSliderColor = System.Drawing.Color.DarkGray;
            this.themeTextBox.ScrollBarThumbBorderColor = System.Drawing.Color.Yellow;
            this.themeTextBox.ScrollBarThumbButtonColor = System.Drawing.Color.DarkBlue;
            this.themeTextBox.ShowLineCount = false;
            this.themeTextBox.Size = new System.Drawing.Size(598, 384);
            this.themeTextBox.TabIndex = 5;
            this.themeTextBox.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.themeTextBox.TextBoxForeColor = System.Drawing.SystemColors.ControlText;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 209);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.panelIcon);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(67, 384);
            this.panelLeft.TabIndex = 7;
            this.panelLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveMouseDown);
            this.panelLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveMouseUp);
            // 
            // panelIcon
            // 
            this.panelIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelIcon.Location = new System.Drawing.Point(5, 5);
            this.panelIcon.Name = "panelIcon";
            this.panelIcon.Size = new System.Drawing.Size(48, 48);
            this.panelIcon.TabIndex = 6;
            // 
            // panelButs
            // 
            this.panelButs.Controls.Add(this.buttonExt1);
            this.panelButs.Controls.Add(this.buttonExt2);
            this.panelButs.Controls.Add(this.buttonExt3);
            this.panelButs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButs.Location = new System.Drawing.Point(0, 411);
            this.panelButs.Margin = new System.Windows.Forms.Padding(10);
            this.panelButs.Name = "panelButs";
            this.panelButs.Size = new System.Drawing.Size(665, 48);
            this.panelButs.TabIndex = 6;
            // 
            // buttonExt1
            // 
            this.buttonExt1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExt1.Location = new System.Drawing.Point(576, 12);
            this.buttonExt1.Name = "buttonExt1";
            this.buttonExt1.Size = new System.Drawing.Size(75, 24);
            this.buttonExt1.TabIndex = 2;
            this.buttonExt1.Text = "OK";
            this.buttonExt1.UseVisualStyleBackColor = true;
            this.buttonExt1.Click += new System.EventHandler(this.buttonExt_Click);
            // 
            // buttonExt2
            // 
            this.buttonExt2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExt2.Location = new System.Drawing.Point(486, 12);
            this.buttonExt2.Name = "buttonExt2";
            this.buttonExt2.Size = new System.Drawing.Size(75, 24);
            this.buttonExt2.TabIndex = 3;
            this.buttonExt2.Text = "Cancel";
            this.buttonExt2.UseVisualStyleBackColor = true;
            this.buttonExt2.Click += new System.EventHandler(this.buttonExt_Click);
            // 
            // buttonExt3
            // 
            this.buttonExt3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExt3.Location = new System.Drawing.Point(395, 12);
            this.buttonExt3.Name = "buttonExt3";
            this.buttonExt3.Size = new System.Drawing.Size(75, 24);
            this.buttonExt3.TabIndex = 4;
            this.buttonExt3.Text = "Retry";
            this.buttonExt3.UseVisualStyleBackColor = true;
            this.buttonExt3.Click += new System.EventHandler(this.buttonExt_Click);
            // 
            // panelGap
            // 
            this.panelGap.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGap.Location = new System.Drawing.Point(0, 13);
            this.panelGap.Margin = new System.Windows.Forms.Padding(0);
            this.panelGap.Name = "panelGap";
            this.panelGap.Size = new System.Drawing.Size(665, 14);
            this.panelGap.TabIndex = 7;
            this.panelGap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveMouseDown);
            this.panelGap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveMouseUp);
            // 
            // labelCaption
            // 
            this.labelCaption.AutoSize = true;
            this.labelCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelCaption.Location = new System.Drawing.Point(0, 0);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(43, 13);
            this.labelCaption.TabIndex = 2;
            this.labelCaption.Text = "<code>";
            this.labelCaption.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveMouseDown);
            this.labelCaption.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveMouseUp);
            // 
            // MessageBoxTheme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 464);
            this.Controls.Add(this.panelOuter);
            this.Name = "MessageBoxTheme";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MessageBox";
            this.Shown += new System.EventHandler(this.MessageBoxTheme_Shown);
            this.panelOuter.ResumeLayout(false);
            this.panelOuter.PerformLayout();
            this.panelIconText.ResumeLayout(false);
            this.themeTextBox.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelButs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ExtendedControls.ExtButton buttonExt1;
        private ExtendedControls.ExtButton buttonExt2;
        private ExtendedControls.ExtButton buttonExt3;
        private System.Windows.Forms.Panel panelOuter;
        private ExtRichTextBox themeTextBox;
        private System.Windows.Forms.Panel panelButs;
        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.Panel panelGap;
        private System.Windows.Forms.Panel panelIcon;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelIconText;
        private System.Windows.Forms.Panel panelLeft;
    }
}