﻿namespace TestExtendedControls
{
    partial class TestTextBoxes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestTextBoxes));
            this.panel1 = new System.Windows.Forms.Panel();
            this.extComboBoxFont = new ExtendedControls.ExtComboBox();
            this.extComboBoxFontSize = new ExtendedControls.ExtComboBox();
            this.extButton2 = new ExtendedControls.ExtButton();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.textBoxDouble1 = new ExtendedControls.NumberBoxDouble();
            this.textBoxDouble2 = new ExtendedControls.NumberBoxDouble();
            this.numberBoxLong2 = new ExtendedControls.NumberBoxLong();
            this.numberBoxLong1 = new ExtendedControls.NumberBoxLong();
            this.extRichTextBox1 = new ExtendedControls.ExtRichTextBox();
            this.extTextBox1 = new ExtendedControls.ExtTextBox();
            this.buttonExt1 = new ExtendedControls.ExtButton();
            this.buttonExt2 = new ExtendedControls.ExtButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
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
            // extComboBoxFont
            // 
            this.extComboBoxFont.BorderColor = System.Drawing.Color.White;
            this.extComboBoxFont.DataSource = null;
            this.extComboBoxFont.DisableBackgroundDisabledShadingGradient = false;
            this.extComboBoxFont.DisplayMember = "";
            this.extComboBoxFont.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extComboBoxFont.Location = new System.Drawing.Point(443, 362);
            this.extComboBoxFont.Name = "extComboBoxFont";
            this.extComboBoxFont.SelectedIndex = -1;
            this.extComboBoxFont.SelectedItem = null;
            this.extComboBoxFont.SelectedValue = null;
            this.extComboBoxFont.Size = new System.Drawing.Size(75, 21);
            this.extComboBoxFont.TabIndex = 69;
            this.extComboBoxFont.Text = "font";
            this.extComboBoxFont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.extComboBoxFont.ValueMember = "";
            this.extComboBoxFont.SelectedIndexChanged += new System.EventHandler(this.extComboBoxFont_SelectedIndexChanged);
            // 
            // extComboBoxFontSize
            // 
            this.extComboBoxFontSize.BorderColor = System.Drawing.Color.White;
            this.extComboBoxFontSize.DataSource = null;
            this.extComboBoxFontSize.DisableBackgroundDisabledShadingGradient = false;
            this.extComboBoxFontSize.DisplayMember = "";
            this.extComboBoxFontSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extComboBoxFontSize.Location = new System.Drawing.Point(443, 321);
            this.extComboBoxFontSize.Name = "extComboBoxFontSize";
            this.extComboBoxFontSize.SelectedIndex = -1;
            this.extComboBoxFontSize.SelectedItem = null;
            this.extComboBoxFontSize.SelectedValue = null;
            this.extComboBoxFontSize.Size = new System.Drawing.Size(75, 21);
            this.extComboBoxFontSize.TabIndex = 69;
            this.extComboBoxFontSize.Text = "size";
            this.extComboBoxFontSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.extComboBoxFontSize.ValueMember = "";
            this.extComboBoxFontSize.SelectedIndexChanged += new System.EventHandler(this.extComboBoxFontSize_SelectedIndexChanged);
            // 
            // extButton2
            // 
            this.extButton2.Location = new System.Drawing.Point(443, 282);
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(75, 23);
            this.extButton2.TabIndex = 68;
            this.extButton2.Text = "clear";
            this.extButton2.UseVisualStyleBackColor = true;
            this.extButton2.Click += new System.EventHandler(this.extButton2_Click);
            // 
            // extButton1
            // 
            this.extButton1.Location = new System.Drawing.Point(443, 241);
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(75, 23);
            this.extButton1.TabIndex = 68;
            this.extButton1.Text = "add text";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click);
            // 
            // textBoxDouble1
            // 
            this.textBoxDouble1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.textBoxDouble1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.textBoxDouble1.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxDouble1.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxDouble1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDouble1.ClearOnFirstChar = false;
            this.textBoxDouble1.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxDouble1.DelayBeforeNotification = 1000;
            this.textBoxDouble1.EndButtonEnable = true;
            this.textBoxDouble1.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("textBoxDouble1.EndButtonImage")));
            this.textBoxDouble1.EndButtonSize16ths = 10;
            this.textBoxDouble1.EndButtonVisible = false;
            this.textBoxDouble1.Format = "0.###";
            this.textBoxDouble1.InErrorCondition = false;
            this.textBoxDouble1.Location = new System.Drawing.Point(12, 64);
            this.textBoxDouble1.Maximum = 1.7976931348623157E+308D;
            this.textBoxDouble1.Minimum = -1.7976931348623157E+308D;
            this.textBoxDouble1.Multiline = false;
            this.textBoxDouble1.Name = "textBoxDouble1";
            this.textBoxDouble1.NumberStyles = ((System.Globalization.NumberStyles)(((System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowDecimalPoint) 
            | System.Globalization.NumberStyles.AllowThousands)));
            this.textBoxDouble1.ReadOnly = false;
            this.textBoxDouble1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxDouble1.SelectionLength = 0;
            this.textBoxDouble1.SelectionStart = 0;
            this.textBoxDouble1.Size = new System.Drawing.Size(140, 20);
            this.textBoxDouble1.TabIndex = 67;
            this.textBoxDouble1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxDouble1.TextNoChange = "0";
            this.toolTip1.SetToolTip(this.textBoxDouble1, "Number Box Double");
            this.textBoxDouble1.Value = 0D;
            this.textBoxDouble1.WordWrap = true;
            this.textBoxDouble1.ValueChanged += new System.EventHandler(this.textBoxDouble1_ValueChanged);
            // 
            // textBoxDouble2
            // 
            this.textBoxDouble2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.textBoxDouble2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.textBoxDouble2.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxDouble2.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxDouble2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDouble2.ClearOnFirstChar = false;
            this.textBoxDouble2.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxDouble2.DelayBeforeNotification = 1000;
            this.textBoxDouble2.EndButtonEnable = true;
            this.textBoxDouble2.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("textBoxDouble2.EndButtonImage")));
            this.textBoxDouble2.EndButtonSize16ths = 10;
            this.textBoxDouble2.EndButtonVisible = false;
            this.textBoxDouble2.Format = "0.###";
            this.textBoxDouble2.InErrorCondition = false;
            this.textBoxDouble2.Location = new System.Drawing.Point(12, 101);
            this.textBoxDouble2.Maximum = 1.7976931348623157E+308D;
            this.textBoxDouble2.Minimum = -1.7976931348623157E+308D;
            this.textBoxDouble2.Multiline = false;
            this.textBoxDouble2.Name = "textBoxDouble2";
            this.textBoxDouble2.NumberStyles = ((System.Globalization.NumberStyles)(((System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowDecimalPoint) 
            | System.Globalization.NumberStyles.AllowThousands)));
            this.textBoxDouble2.ReadOnly = false;
            this.textBoxDouble2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxDouble2.SelectionLength = 0;
            this.textBoxDouble2.SelectionStart = 0;
            this.textBoxDouble2.Size = new System.Drawing.Size(140, 20);
            this.textBoxDouble2.TabIndex = 65;
            this.textBoxDouble2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxDouble2.TextNoChange = "0";
            this.toolTip1.SetToolTip(this.textBoxDouble2, "Number Box Double");
            this.textBoxDouble2.Value = 0D;
            this.textBoxDouble2.WordWrap = true;
            this.textBoxDouble2.ValueChanged += new System.EventHandler(this.textBoxDouble2_ValueChanged);
            // 
            // numberBoxLong2
            // 
            this.numberBoxLong2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.numberBoxLong2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.numberBoxLong2.BackErrorColor = System.Drawing.Color.Red;
            this.numberBoxLong2.BorderColor = System.Drawing.Color.Transparent;
            this.numberBoxLong2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numberBoxLong2.ClearOnFirstChar = false;
            this.numberBoxLong2.ControlBackground = System.Drawing.SystemColors.Control;
            this.numberBoxLong2.DelayBeforeNotification = 0;
            this.numberBoxLong2.EndButtonEnable = true;
            this.numberBoxLong2.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("numberBoxLong2.EndButtonImage")));
            this.numberBoxLong2.EndButtonSize16ths = 10;
            this.numberBoxLong2.EndButtonVisible = false;
            this.numberBoxLong2.Format = "D";
            this.numberBoxLong2.InErrorCondition = false;
            this.numberBoxLong2.Location = new System.Drawing.Point(12, 38);
            this.numberBoxLong2.Maximum = ((long)(9223372036854775807));
            this.numberBoxLong2.Minimum = ((long)(-9223372036854775808));
            this.numberBoxLong2.Multiline = false;
            this.numberBoxLong2.Name = "numberBoxLong2";
            this.numberBoxLong2.NumberStyles = ((System.Globalization.NumberStyles)((System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowThousands)));
            this.numberBoxLong2.ReadOnly = false;
            this.numberBoxLong2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.numberBoxLong2.SelectionLength = 0;
            this.numberBoxLong2.SelectionStart = 0;
            this.numberBoxLong2.Size = new System.Drawing.Size(75, 20);
            this.numberBoxLong2.TabIndex = 66;
            this.numberBoxLong2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.numberBoxLong2.TextNoChange = "2000";
            this.toolTip1.SetToolTip(this.numberBoxLong2, "Number Box Long");
            this.numberBoxLong2.Value = ((long)(2000));
            this.numberBoxLong2.WordWrap = true;
            this.numberBoxLong2.ValueChanged += new System.EventHandler(this.numberBoxLong2_ValueChanged);
            // 
            // numberBoxLong1
            // 
            this.numberBoxLong1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.numberBoxLong1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.numberBoxLong1.BackErrorColor = System.Drawing.Color.Red;
            this.numberBoxLong1.BorderColor = System.Drawing.Color.Transparent;
            this.numberBoxLong1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numberBoxLong1.ClearOnFirstChar = false;
            this.numberBoxLong1.ControlBackground = System.Drawing.SystemColors.Control;
            this.numberBoxLong1.DelayBeforeNotification = 0;
            this.numberBoxLong1.EndButtonEnable = true;
            this.numberBoxLong1.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("numberBoxLong1.EndButtonImage")));
            this.numberBoxLong1.EndButtonSize16ths = 10;
            this.numberBoxLong1.EndButtonVisible = false;
            this.numberBoxLong1.Format = "D";
            this.numberBoxLong1.InErrorCondition = false;
            this.numberBoxLong1.Location = new System.Drawing.Point(12, 12);
            this.numberBoxLong1.Maximum = ((long)(9223372036854775807));
            this.numberBoxLong1.Minimum = ((long)(-9223372036854775808));
            this.numberBoxLong1.Multiline = false;
            this.numberBoxLong1.Name = "numberBoxLong1";
            this.numberBoxLong1.NumberStyles = ((System.Globalization.NumberStyles)((System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowThousands)));
            this.numberBoxLong1.ReadOnly = false;
            this.numberBoxLong1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.numberBoxLong1.SelectionLength = 0;
            this.numberBoxLong1.SelectionStart = 0;
            this.numberBoxLong1.Size = new System.Drawing.Size(75, 20);
            this.numberBoxLong1.TabIndex = 66;
            this.numberBoxLong1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.numberBoxLong1.TextNoChange = "2000";
            this.toolTip1.SetToolTip(this.numberBoxLong1, "Number Box Long");
            this.numberBoxLong1.Value = ((long)(2000));
            this.numberBoxLong1.WordWrap = true;
            this.numberBoxLong1.ValueChanged += new System.EventHandler(this.numberBoxLong1_ValueChanged);
            // 
            // extRichTextBox1
            // 
            this.extRichTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.extRichTextBox1.BorderColor = System.Drawing.Color.OrangeRed;
            this.extRichTextBox1.DetectUrls = true;
            this.extRichTextBox1.HideScrollBar = true;
            this.extRichTextBox1.Location = new System.Drawing.Point(31, 146);
            this.extRichTextBox1.Name = "extRichTextBox1";
            this.extRichTextBox1.ReadOnly = false;
            this.extRichTextBox1.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang2057{\\fonttbl{\\f0\\fnil\\fcharset0 " +
    "Microsoft Sans Serif;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\par" +
    "d\\f0\\fs17\\par\r\n}\r\n";
            this.extRichTextBox1.ShowLineCount = false;
            this.extRichTextBox1.Size = new System.Drawing.Size(406, 439);
            this.extRichTextBox1.TabIndex = 30;
            this.extRichTextBox1.TextBoxBackColor = System.Drawing.Color.Red;
            this.extRichTextBox1.TextBoxForeColor = System.Drawing.SystemColors.ControlText;
            // 
            // extTextBox1
            // 
            this.extTextBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.extTextBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.extTextBox1.BackErrorColor = System.Drawing.Color.Red;
            this.extTextBox1.BorderColor = System.Drawing.Color.Gold;
            this.extTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.extTextBox1.ClearOnFirstChar = false;
            this.extTextBox1.ControlBackground = System.Drawing.SystemColors.Control;
            this.extTextBox1.EndButtonEnable = true;
            this.extTextBox1.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("extTextBox1.EndButtonImage")));
            this.extTextBox1.EndButtonSize16ths = 10;
            this.extTextBox1.EndButtonVisible = false;
            this.extTextBox1.InErrorCondition = false;
            this.extTextBox1.Location = new System.Drawing.Point(219, 12);
            this.extTextBox1.Multiline = false;
            this.extTextBox1.Name = "extTextBox1";
            this.extTextBox1.ReadOnly = false;
            this.extTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.extTextBox1.SelectionLength = 0;
            this.extTextBox1.SelectionStart = 0;
            this.extTextBox1.Size = new System.Drawing.Size(100, 26);
            this.extTextBox1.TabIndex = 13;
            this.extTextBox1.Text = "exttextbox";
            this.extTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.extTextBox1.TextNoChange = "exttextbox";
            this.extTextBox1.WordWrap = true;
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
            this.buttonExt2.Location = new System.Drawing.Point(134, 110);
            this.buttonExt2.Margin = new System.Windows.Forms.Padding(0);
            this.buttonExt2.Name = "buttonExt2";
            this.buttonExt2.Size = new System.Drawing.Size(48, 48);
            this.buttonExt2.TabIndex = 0;
            this.buttonExt2.Text = "Hello";
            this.buttonExt2.UseVisualStyleBackColor = false;
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // TestTextBoxes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(718, 635);
            this.Controls.Add(this.extComboBoxFont);
            this.Controls.Add(this.extComboBoxFontSize);
            this.Controls.Add(this.extButton2);
            this.Controls.Add(this.extButton1);
            this.Controls.Add(this.textBoxDouble1);
            this.Controls.Add(this.textBoxDouble2);
            this.Controls.Add(this.numberBoxLong2);
            this.Controls.Add(this.numberBoxLong1);
            this.Controls.Add(this.extRichTextBox1);
            this.Controls.Add(this.extTextBox1);
            this.Name = "TestTextBoxes";
            this.Text = "TestCompositeButton";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TestTextBoxes_Paint);
            this.ResumeLayout(false);

        }

        #endregion
        private ExtendedControls.ExtButton buttonExt1;
        private ExtendedControls.ExtButton buttonExt2;
        private System.Windows.Forms.Panel panel1;
        private ExtendedControls.NumberBoxLong numberBoxLong1;
        private ExtendedControls.NumberBoxLong numberBoxLong2;
        private ExtendedControls.NumberBoxDouble textBoxDouble2;
        private ExtendedControls.NumberBoxDouble textBoxDouble1;
        private ExtendedControls.ExtRichTextBox extRichTextBox1;
        private ExtendedControls.ExtButton extButton1;
        private ExtendedControls.ExtButton extButton2;
        private ExtendedControls.ExtTextBox extTextBox1;
        private ExtendedControls.ExtComboBox extComboBoxFontSize;
        private ExtendedControls.ExtComboBox extComboBoxFont;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}