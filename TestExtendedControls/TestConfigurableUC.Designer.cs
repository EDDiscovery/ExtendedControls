namespace TestExtendedControls
{
    partial class TestConfigurableUC
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.extButtonResize = new ExtendedControls.ExtButton();
            this.extRichTextBox1 = new ExtendedControls.ExtRichTextBox();
            this.configurableUC1 = new ExtendedControls.ConfigurableUC();
            this.extButtonAdd = new ExtendedControls.ExtButton();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.SystemColors.Info;
            this.panelTop.Controls.Add(this.extButtonAdd);
            this.panelTop.Controls.Add(this.extButtonResize);
            this.panelTop.Controls.Add(this.extRichTextBox1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(792, 100);
            this.panelTop.TabIndex = 2;
            // 
            // extButtonResize
            // 
            this.extButtonResize.Location = new System.Drawing.Point(3, 0);
            this.extButtonResize.Name = "extButtonResize";
            this.extButtonResize.Size = new System.Drawing.Size(75, 23);
            this.extButtonResize.TabIndex = 1;
            this.extButtonResize.Text = "Resize";
            this.extButtonResize.UseVisualStyleBackColor = true;
            this.extButtonResize.Click += new System.EventHandler(this.extButtonResize_Click);
            // 
            // extRichTextBox1
            // 
            this.extRichTextBox1.BorderColor = System.Drawing.Color.Transparent;
            this.extRichTextBox1.BorderColorScaling = 0.5F;
            this.extRichTextBox1.DetectUrls = true;
            this.extRichTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.extRichTextBox1.HideScrollBar = true;
            this.extRichTextBox1.Location = new System.Drawing.Point(0, 27);
            this.extRichTextBox1.Name = "extRichTextBox1";
            this.extRichTextBox1.ReadOnly = false;
            this.extRichTextBox1.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang2057{\\fonttbl{\\f0\\fnil\\fcharset0 " +
    "Microsoft Sans Serif;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\par" +
    "d\\f0\\fs17\\par\r\n}\r\n";
            this.extRichTextBox1.ScrollBarArrowBorderColor = System.Drawing.Color.LightBlue;
            this.extRichTextBox1.ScrollBarArrowButtonColor = System.Drawing.Color.LightGray;
            this.extRichTextBox1.ScrollBarBackColor = System.Drawing.SystemColors.Info;
            this.extRichTextBox1.ScrollBarBorderColor = System.Drawing.Color.White;
            this.extRichTextBox1.ScrollBarFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extRichTextBox1.ScrollBarForeColor = System.Drawing.SystemColors.ControlText;
            this.extRichTextBox1.ScrollBarMouseOverButtonColor = System.Drawing.Color.Green;
            this.extRichTextBox1.ScrollBarMousePressedButtonColor = System.Drawing.Color.Red;
            this.extRichTextBox1.ScrollBarSliderColor = System.Drawing.Color.DarkGray;
            this.extRichTextBox1.ScrollBarThumbBorderColor = System.Drawing.Color.Yellow;
            this.extRichTextBox1.ScrollBarThumbButtonColor = System.Drawing.Color.DarkBlue;
            this.extRichTextBox1.ShowLineCount = false;
            this.extRichTextBox1.Size = new System.Drawing.Size(792, 73);
            this.extRichTextBox1.TabIndex = 0;
            this.extRichTextBox1.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.extRichTextBox1.TextBoxForeColor = System.Drawing.SystemColors.ControlText;
            // 
            // configurableUC1
            // 
            this.configurableUC1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.configurableUC1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.configurableUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configurableUC1.Location = new System.Drawing.Point(0, 100);
            this.configurableUC1.Name = "configurableUC1";
            this.configurableUC1.PanelBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.configurableUC1.Size = new System.Drawing.Size(792, 409);
            this.configurableUC1.SwallowReturn = true;
            this.configurableUC1.TabIndex = 3;
            // 
            // extButtonAdd
            // 
            this.extButtonAdd.Location = new System.Drawing.Point(84, 0);
            this.extButtonAdd.Name = "extButtonAdd";
            this.extButtonAdd.Size = new System.Drawing.Size(75, 23);
            this.extButtonAdd.TabIndex = 1;
            this.extButtonAdd.Text = "Add";
            this.extButtonAdd.UseVisualStyleBackColor = true;
            this.extButtonAdd.Click += new System.EventHandler(this.extButtonAdd_Click);
            // 
            // TestConfigurableUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 509);
            this.Controls.Add(this.configurableUC1);
            this.Controls.Add(this.panelTop);
            this.Name = "TestConfigurableUC";
            this.Text = "TestConfigurableUC";
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelTop;
        private ExtendedControls.ConfigurableUC configurableUC1;
        private ExtendedControls.ExtRichTextBox extRichTextBox1;
        private ExtendedControls.ExtButton extButtonResize;
        private ExtendedControls.ExtButton extButtonAdd;
    }
}