namespace ExtendedControls
{
    partial class ImportExportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportExportForm));
            this.panelBottom = new System.Windows.Forms.Panel();
            this.buttonCancel = new ExtendedControls.ExtButton();
            this.buttonExport = new ExtendedControls.ExtButton();
            this.labelCVSSep = new System.Windows.Forms.Label();
            this.radioButtonSemiColon = new ExtendedControls.ExtRadioButton();
            this.radioButtonComma = new ExtendedControls.ExtRadioButton();
            this.checkBoxIncludeHeader = new ExtendedControls.ExtCheckBox();
            this.checkBoxCustomAutoOpen = new ExtendedControls.ExtCheckBox();
            this.customDateTimePickerFrom = new ExtendedControls.ExtDateTimePicker();
            this.customDateTimePickerTo = new ExtendedControls.ExtDateTimePicker();
            this.comboBoxSelectedType = new ExtendedControls.ExtComboBox();
            this.panel_close = new ExtendedControls.ExtButtonDrawn();
            this.panel_minimize = new ExtendedControls.ExtButtonDrawn();
            this.label_index = new System.Windows.Forms.Label();
            this.labelUTCEnd = new System.Windows.Forms.Label();
            this.labelUTCStart = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelCombo = new System.Windows.Forms.Panel();
            this.panelDate = new System.Windows.Forms.Panel();
            this.panelIncludeOpen = new System.Windows.Forms.Panel();
            this.panelCSV = new System.Windows.Forms.Panel();
            this.extRadioButtonTab = new ExtendedControls.ExtRadioButton();
            this.panelOuter = new System.Windows.Forms.Panel();
            this.panelSaveImportAs = new System.Windows.Forms.Panel();
            this.labelSaveImport = new System.Windows.Forms.Label();
            this.extTextBoxSaveImport = new ExtendedControls.ExtTextBox();
            this.panelPasteImport = new System.Windows.Forms.Panel();
            this.buttonPasteData = new ExtendedControls.ExtButton();
            this.extRichTextBoxPaste = new ExtendedControls.ExtRichTextBox();
            this.labelPaste = new System.Windows.Forms.Label();
            this.panelImportExclude = new System.Windows.Forms.Panel();
            this.extCheckBoxExcludeHeader = new ExtendedControls.ExtCheckBox();
            this.panelBottom.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelCombo.SuspendLayout();
            this.panelDate.SuspendLayout();
            this.panelIncludeOpen.SuspendLayout();
            this.panelCSV.SuspendLayout();
            this.panelOuter.SuspendLayout();
            this.panelSaveImportAs.SuspendLayout();
            this.panelPasteImport.SuspendLayout();
            this.panelImportExclude.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.SystemColors.Control;
            this.panelBottom.Controls.Add(this.buttonCancel);
            this.panelBottom.Controls.Add(this.buttonExport);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBottom.Location = new System.Drawing.Point(0, 418);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(470, 36);
            this.panelBottom.TabIndex = 0;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(252, 6);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "%Cancel%";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(358, 6);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(100, 23);
            this.buttonExport.TabIndex = 5;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // labelCVSSep
            // 
            this.labelCVSSep.AutoSize = true;
            this.labelCVSSep.Location = new System.Drawing.Point(5, 7);
            this.labelCVSSep.Name = "labelCVSSep";
            this.labelCVSSep.Size = new System.Drawing.Size(77, 13);
            this.labelCVSSep.TabIndex = 2;
            this.labelCVSSep.Text = "CSV Separator";
            // 
            // radioButtonSemiColon
            // 
            this.radioButtonSemiColon.AutoSize = true;
            this.radioButtonSemiColon.Location = new System.Drawing.Point(180, 25);
            this.radioButtonSemiColon.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.radioButtonSemiColon.Name = "radioButtonSemiColon";
            this.radioButtonSemiColon.RadioButtonColor = System.Drawing.Color.Gray;
            this.radioButtonSemiColon.RadioButtonInnerColor = System.Drawing.Color.White;
            this.radioButtonSemiColon.SelectedColor = System.Drawing.Color.DarkBlue;
            this.radioButtonSemiColon.SelectedColorRing = System.Drawing.Color.Black;
            this.radioButtonSemiColon.Size = new System.Drawing.Size(74, 17);
            this.radioButtonSemiColon.TabIndex = 1;
            this.radioButtonSemiColon.TabStop = true;
            this.radioButtonSemiColon.Text = "Semicolon";
            this.radioButtonSemiColon.UseVisualStyleBackColor = true;
            // 
            // radioButtonComma
            // 
            this.radioButtonComma.AutoSize = true;
            this.radioButtonComma.Location = new System.Drawing.Point(180, 5);
            this.radioButtonComma.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.radioButtonComma.Name = "radioButtonComma";
            this.radioButtonComma.RadioButtonColor = System.Drawing.Color.Gray;
            this.radioButtonComma.RadioButtonInnerColor = System.Drawing.Color.White;
            this.radioButtonComma.SelectedColor = System.Drawing.Color.DarkBlue;
            this.radioButtonComma.SelectedColorRing = System.Drawing.Color.Black;
            this.radioButtonComma.Size = new System.Drawing.Size(60, 17);
            this.radioButtonComma.TabIndex = 0;
            this.radioButtonComma.TabStop = true;
            this.radioButtonComma.Text = "Comma";
            this.radioButtonComma.UseVisualStyleBackColor = true;
            // 
            // checkBoxIncludeHeader
            // 
            this.checkBoxIncludeHeader.AutoSize = true;
            this.checkBoxIncludeHeader.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxIncludeHeader.CheckBoxDisabledScaling = 0.5F;
            this.checkBoxIncludeHeader.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxIncludeHeader.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxIncludeHeader.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxIncludeHeader.ImageIndeterminate = null;
            this.checkBoxIncludeHeader.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxIncludeHeader.ImageUnchecked = null;
            this.checkBoxIncludeHeader.Location = new System.Drawing.Point(8, 5);
            this.checkBoxIncludeHeader.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxIncludeHeader.Name = "checkBoxIncludeHeader";
            this.checkBoxIncludeHeader.Size = new System.Drawing.Size(99, 17);
            this.checkBoxIncludeHeader.TabIndex = 1;
            this.checkBoxIncludeHeader.Text = "Include Header";
            this.checkBoxIncludeHeader.TickBoxReductionRatio = 0.75F;
            this.checkBoxIncludeHeader.UseVisualStyleBackColor = true;
            // 
            // checkBoxCustomAutoOpen
            // 
            this.checkBoxCustomAutoOpen.AutoSize = true;
            this.checkBoxCustomAutoOpen.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxCustomAutoOpen.CheckBoxDisabledScaling = 0.5F;
            this.checkBoxCustomAutoOpen.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxCustomAutoOpen.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxCustomAutoOpen.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxCustomAutoOpen.ImageIndeterminate = null;
            this.checkBoxCustomAutoOpen.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxCustomAutoOpen.ImageUnchecked = null;
            this.checkBoxCustomAutoOpen.Location = new System.Drawing.Point(180, 5);
            this.checkBoxCustomAutoOpen.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxCustomAutoOpen.Name = "checkBoxCustomAutoOpen";
            this.checkBoxCustomAutoOpen.Size = new System.Drawing.Size(52, 17);
            this.checkBoxCustomAutoOpen.TabIndex = 1;
            this.checkBoxCustomAutoOpen.Text = "Open";
            this.checkBoxCustomAutoOpen.TickBoxReductionRatio = 0.75F;
            this.checkBoxCustomAutoOpen.UseVisualStyleBackColor = true;
            // 
            // customDateTimePickerFrom
            // 
            this.customDateTimePickerFrom.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.customDateTimePickerFrom.BorderColorScaling = 0.5F;
            this.customDateTimePickerFrom.Checked = false;
            this.customDateTimePickerFrom.CustomFormat = "dd MMMM yyyy    HH:mm:ss";
            this.customDateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.customDateTimePickerFrom.Location = new System.Drawing.Point(3, 5);
            this.customDateTimePickerFrom.Name = "customDateTimePickerFrom";
            this.customDateTimePickerFrom.SelectedColor = System.Drawing.Color.Yellow;
            this.customDateTimePickerFrom.ShowCheckBox = false;
            this.customDateTimePickerFrom.ShowUpDown = false;
            this.customDateTimePickerFrom.Size = new System.Drawing.Size(270, 23);
            this.customDateTimePickerFrom.TabIndex = 2;
            this.customDateTimePickerFrom.TextBackColor = System.Drawing.SystemColors.Control;
            this.customDateTimePickerFrom.Value = new System.DateTime(2017, 9, 15, 14, 17, 6, 509);
            // 
            // customDateTimePickerTo
            // 
            this.customDateTimePickerTo.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.customDateTimePickerTo.BorderColorScaling = 0.5F;
            this.customDateTimePickerTo.Checked = false;
            this.customDateTimePickerTo.CustomFormat = "dd MMMM yyyy    HH:mm:ss";
            this.customDateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.customDateTimePickerTo.Location = new System.Drawing.Point(3, 35);
            this.customDateTimePickerTo.Name = "customDateTimePickerTo";
            this.customDateTimePickerTo.SelectedColor = System.Drawing.Color.Yellow;
            this.customDateTimePickerTo.ShowCheckBox = false;
            this.customDateTimePickerTo.ShowUpDown = false;
            this.customDateTimePickerTo.Size = new System.Drawing.Size(270, 23);
            this.customDateTimePickerTo.TabIndex = 3;
            this.customDateTimePickerTo.TextBackColor = System.Drawing.SystemColors.Control;
            this.customDateTimePickerTo.Value = new System.DateTime(2017, 9, 15, 14, 17, 10, 468);
            // 
            // comboBoxSelectedType
            // 
            this.comboBoxSelectedType.BorderColor = System.Drawing.Color.White;
            this.comboBoxSelectedType.ButtonColorScaling = 0.5F;
            this.comboBoxSelectedType.DataSource = null;
            this.comboBoxSelectedType.DisableBackgroundDisabledShadingGradient = false;
            this.comboBoxSelectedType.DisplayMember = "";
            this.comboBoxSelectedType.DropDownBackgroundColor = System.Drawing.Color.Gray;
            this.comboBoxSelectedType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxSelectedType.Location = new System.Drawing.Point(3, 5);
            this.comboBoxSelectedType.MouseOverBackgroundColor = System.Drawing.Color.Silver;
            this.comboBoxSelectedType.Name = "comboBoxSelectedType";
            this.comboBoxSelectedType.ScrollBarButtonColor = System.Drawing.Color.LightGray;
            this.comboBoxSelectedType.ScrollBarColor = System.Drawing.Color.LightGray;
            this.comboBoxSelectedType.SelectedIndex = -1;
            this.comboBoxSelectedType.SelectedItem = null;
            this.comboBoxSelectedType.SelectedValue = null;
            this.comboBoxSelectedType.Size = new System.Drawing.Size(270, 21);
            this.comboBoxSelectedType.TabIndex = 4;
            this.comboBoxSelectedType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.comboBoxSelectedType.ValueMember = "";
            // 
            // panel_close
            // 
            this.panel_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_close.AutoEllipsis = false;
            this.panel_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel_close.BorderColor = System.Drawing.Color.Orange;
            this.panel_close.BorderWidth = 1;
            this.panel_close.Image = null;
            this.panel_close.ImageSelected = ExtendedControls.ExtButtonDrawn.ImageType.Close;
            this.panel_close.Location = new System.Drawing.Point(445, 2);
            this.panel_close.MouseOverColor = System.Drawing.Color.White;
            this.panel_close.MouseSelectedColor = System.Drawing.Color.Green;
            this.panel_close.MouseSelectedColorEnable = true;
            this.panel_close.Name = "panel_close";
            this.panel_close.Padding = new System.Windows.Forms.Padding(6);
            this.panel_close.PanelDisabledScaling = 0.25F;
            this.panel_close.Selectable = false;
            this.panel_close.Size = new System.Drawing.Size(24, 24);
            this.panel_close.TabIndex = 30;
            this.panel_close.TabStop = false;
            this.panel_close.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.panel_close.UseMnemonic = true;
            this.panel_close.Click += new System.EventHandler(this.panel_close_Click);
            // 
            // panel_minimize
            // 
            this.panel_minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_minimize.AutoEllipsis = false;
            this.panel_minimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel_minimize.BorderColor = System.Drawing.Color.Orange;
            this.panel_minimize.BorderWidth = 1;
            this.panel_minimize.Image = null;
            this.panel_minimize.ImageSelected = ExtendedControls.ExtButtonDrawn.ImageType.Minimize;
            this.panel_minimize.Location = new System.Drawing.Point(421, 2);
            this.panel_minimize.MouseOverColor = System.Drawing.Color.White;
            this.panel_minimize.MouseSelectedColor = System.Drawing.Color.Green;
            this.panel_minimize.MouseSelectedColorEnable = true;
            this.panel_minimize.Name = "panel_minimize";
            this.panel_minimize.Padding = new System.Windows.Forms.Padding(6);
            this.panel_minimize.PanelDisabledScaling = 0.25F;
            this.panel_minimize.Selectable = false;
            this.panel_minimize.Size = new System.Drawing.Size(24, 24);
            this.panel_minimize.TabIndex = 29;
            this.panel_minimize.TabStop = false;
            this.panel_minimize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.panel_minimize.UseMnemonic = true;
            this.panel_minimize.Click += new System.EventHandler(this.panel_minimize_Click);
            // 
            // label_index
            // 
            this.label_index.AutoSize = true;
            this.label_index.Location = new System.Drawing.Point(3, 3);
            this.label_index.Name = "label_index";
            this.label_index.Size = new System.Drawing.Size(43, 13);
            this.label_index.TabIndex = 28;
            this.label_index.Text = "<code>";
            this.label_index.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_index_MouseDown);
            this.label_index.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label_index_MouseUp);
            // 
            // labelUTCEnd
            // 
            this.labelUTCEnd.AutoSize = true;
            this.labelUTCEnd.Location = new System.Drawing.Point(282, 40);
            this.labelUTCEnd.Name = "labelUTCEnd";
            this.labelUTCEnd.Size = new System.Drawing.Size(29, 13);
            this.labelUTCEnd.TabIndex = 5;
            this.labelUTCEnd.Text = "UTC";
            // 
            // labelUTCStart
            // 
            this.labelUTCStart.AutoSize = true;
            this.labelUTCStart.Location = new System.Drawing.Point(282, 9);
            this.labelUTCStart.Name = "labelUTCStart";
            this.labelUTCStart.Size = new System.Drawing.Size(29, 13);
            this.labelUTCStart.TabIndex = 5;
            this.labelUTCStart.Text = "UTC";
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.label_index);
            this.panelTop.Controls.Add(this.panel_minimize);
            this.panelTop.Controls.Add(this.panel_close);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(472, 32);
            this.panelTop.TabIndex = 32;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.panelTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // panelCombo
            // 
            this.panelCombo.Controls.Add(this.comboBoxSelectedType);
            this.panelCombo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCombo.Location = new System.Drawing.Point(0, 0);
            this.panelCombo.Name = "panelCombo";
            this.panelCombo.Size = new System.Drawing.Size(470, 34);
            this.panelCombo.TabIndex = 7;
            // 
            // panelDate
            // 
            this.panelDate.Controls.Add(this.customDateTimePickerFrom);
            this.panelDate.Controls.Add(this.labelUTCEnd);
            this.panelDate.Controls.Add(this.customDateTimePickerTo);
            this.panelDate.Controls.Add(this.labelUTCStart);
            this.panelDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDate.Location = new System.Drawing.Point(0, 34);
            this.panelDate.Name = "panelDate";
            this.panelDate.Size = new System.Drawing.Size(470, 64);
            this.panelDate.TabIndex = 5;
            // 
            // panelIncludeOpen
            // 
            this.panelIncludeOpen.Controls.Add(this.checkBoxIncludeHeader);
            this.panelIncludeOpen.Controls.Add(this.checkBoxCustomAutoOpen);
            this.panelIncludeOpen.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelIncludeOpen.Location = new System.Drawing.Point(0, 144);
            this.panelIncludeOpen.Name = "panelIncludeOpen";
            this.panelIncludeOpen.Size = new System.Drawing.Size(470, 25);
            this.panelIncludeOpen.TabIndex = 8;
            // 
            // panelCSV
            // 
            this.panelCSV.Controls.Add(this.labelCVSSep);
            this.panelCSV.Controls.Add(this.radioButtonComma);
            this.panelCSV.Controls.Add(this.extRadioButtonTab);
            this.panelCSV.Controls.Add(this.radioButtonSemiColon);
            this.panelCSV.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCSV.Location = new System.Drawing.Point(0, 98);
            this.panelCSV.Name = "panelCSV";
            this.panelCSV.Size = new System.Drawing.Size(470, 46);
            this.panelCSV.TabIndex = 9;
            // 
            // extRadioButtonTab
            // 
            this.extRadioButtonTab.AutoSize = true;
            this.extRadioButtonTab.Location = new System.Drawing.Point(285, 5);
            this.extRadioButtonTab.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.extRadioButtonTab.Name = "extRadioButtonTab";
            this.extRadioButtonTab.RadioButtonColor = System.Drawing.Color.Gray;
            this.extRadioButtonTab.RadioButtonInnerColor = System.Drawing.Color.White;
            this.extRadioButtonTab.SelectedColor = System.Drawing.Color.DarkBlue;
            this.extRadioButtonTab.SelectedColorRing = System.Drawing.Color.Black;
            this.extRadioButtonTab.Size = new System.Drawing.Size(44, 17);
            this.extRadioButtonTab.TabIndex = 1;
            this.extRadioButtonTab.TabStop = true;
            this.extRadioButtonTab.Text = "Tab";
            this.extRadioButtonTab.UseVisualStyleBackColor = true;
            // 
            // panelOuter
            // 
            this.panelOuter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOuter.Controls.Add(this.panelBottom);
            this.panelOuter.Controls.Add(this.panelSaveImportAs);
            this.panelOuter.Controls.Add(this.panelPasteImport);
            this.panelOuter.Controls.Add(this.panelImportExclude);
            this.panelOuter.Controls.Add(this.panelIncludeOpen);
            this.panelOuter.Controls.Add(this.panelCSV);
            this.panelOuter.Controls.Add(this.panelDate);
            this.panelOuter.Controls.Add(this.panelCombo);
            this.panelOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOuter.Location = new System.Drawing.Point(0, 32);
            this.panelOuter.Name = "panelOuter";
            this.panelOuter.Size = new System.Drawing.Size(472, 456);
            this.panelOuter.TabIndex = 5;
            // 
            // panelSaveImportAs
            // 
            this.panelSaveImportAs.BackColor = System.Drawing.SystemColors.Control;
            this.panelSaveImportAs.Controls.Add(this.labelSaveImport);
            this.panelSaveImportAs.Controls.Add(this.extTextBoxSaveImport);
            this.panelSaveImportAs.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSaveImportAs.Location = new System.Drawing.Point(0, 382);
            this.panelSaveImportAs.Name = "panelSaveImportAs";
            this.panelSaveImportAs.Size = new System.Drawing.Size(470, 36);
            this.panelSaveImportAs.TabIndex = 13;
            // 
            // labelSaveImport
            // 
            this.labelSaveImport.AutoSize = true;
            this.labelSaveImport.Location = new System.Drawing.Point(5, 10);
            this.labelSaveImport.Name = "labelSaveImport";
            this.labelSaveImport.Size = new System.Drawing.Size(79, 13);
            this.labelSaveImport.TabIndex = 2;
            this.labelSaveImport.Text = "Save Import As";
            // 
            // extTextBoxSaveImport
            // 
            this.extTextBoxSaveImport.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.extTextBoxSaveImport.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.extTextBoxSaveImport.BackErrorColor = System.Drawing.Color.Red;
            this.extTextBoxSaveImport.BorderColor = System.Drawing.Color.Transparent;
            this.extTextBoxSaveImport.BorderColorScaling = 0.5F;
            this.extTextBoxSaveImport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.extTextBoxSaveImport.ClearOnFirstChar = false;
            this.extTextBoxSaveImport.ControlBackground = System.Drawing.SystemColors.Control;
            this.extTextBoxSaveImport.EndButtonEnable = true;
            this.extTextBoxSaveImport.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("extTextBoxSaveImport.EndButtonImage")));
            this.extTextBoxSaveImport.EndButtonSize16ths = 10;
            this.extTextBoxSaveImport.EndButtonVisible = false;
            this.extTextBoxSaveImport.InErrorCondition = false;
            this.extTextBoxSaveImport.Location = new System.Drawing.Point(90, 7);
            this.extTextBoxSaveImport.Multiline = false;
            this.extTextBoxSaveImport.Name = "extTextBoxSaveImport";
            this.extTextBoxSaveImport.ReadOnly = false;
            this.extTextBoxSaveImport.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.extTextBoxSaveImport.SelectionLength = 0;
            this.extTextBoxSaveImport.SelectionStart = 0;
            this.extTextBoxSaveImport.Size = new System.Drawing.Size(341, 23);
            this.extTextBoxSaveImport.TabIndex = 12;
            this.extTextBoxSaveImport.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.extTextBoxSaveImport.TextNoChange = "";
            this.extTextBoxSaveImport.WordWrap = true;
            // 
            // panelPasteImport
            // 
            this.panelPasteImport.Controls.Add(this.buttonPasteData);
            this.panelPasteImport.Controls.Add(this.extRichTextBoxPaste);
            this.panelPasteImport.Controls.Add(this.labelPaste);
            this.panelPasteImport.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPasteImport.Location = new System.Drawing.Point(0, 194);
            this.panelPasteImport.Name = "panelPasteImport";
            this.panelPasteImport.Size = new System.Drawing.Size(470, 188);
            this.panelPasteImport.TabIndex = 10;
            // 
            // buttonPasteData
            // 
            this.buttonPasteData.Image = global::ExtendedControls.Properties.Resources.Paste;
            this.buttonPasteData.Location = new System.Drawing.Point(389, 6);
            this.buttonPasteData.Name = "buttonPasteData";
            this.buttonPasteData.Size = new System.Drawing.Size(28, 28);
            this.buttonPasteData.TabIndex = 7;
            this.buttonPasteData.UseVisualStyleBackColor = true;
            this.buttonPasteData.Click += new System.EventHandler(this.buttonPasteData_Click);
            // 
            // extRichTextBoxPaste
            // 
            this.extRichTextBoxPaste.AllowDrop = true;
            this.extRichTextBoxPaste.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.extRichTextBoxPaste.BorderColorScaling = 0.5F;
            this.extRichTextBoxPaste.DetectUrls = true;
            this.extRichTextBoxPaste.HideScrollBar = true;
            this.extRichTextBoxPaste.Location = new System.Drawing.Point(8, 40);
            this.extRichTextBoxPaste.Name = "extRichTextBoxPaste";
            this.extRichTextBoxPaste.ReadOnly = false;
            this.extRichTextBoxPaste.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang2057{\\fonttbl{\\f0\\fnil\\fcharset0 " +
    "Microsoft Sans Serif;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\par" +
    "d\\f0\\fs17\\par\r\n}\r\n";
            this.extRichTextBoxPaste.ScrollBarArrowBorderColor = System.Drawing.Color.LightBlue;
            this.extRichTextBoxPaste.ScrollBarArrowButtonColor = System.Drawing.Color.LightGray;
            this.extRichTextBoxPaste.ScrollBarBackColor = System.Drawing.SystemColors.Control;
            this.extRichTextBoxPaste.ScrollBarBorderColor = System.Drawing.Color.White;
            this.extRichTextBoxPaste.ScrollBarFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extRichTextBoxPaste.ScrollBarForeColor = System.Drawing.SystemColors.ControlText;
            this.extRichTextBoxPaste.ScrollBarMouseOverButtonColor = System.Drawing.Color.Green;
            this.extRichTextBoxPaste.ScrollBarMousePressedButtonColor = System.Drawing.Color.Red;
            this.extRichTextBoxPaste.ScrollBarSliderColor = System.Drawing.Color.DarkGray;
            this.extRichTextBoxPaste.ScrollBarThumbBorderColor = System.Drawing.Color.Yellow;
            this.extRichTextBoxPaste.ScrollBarThumbButtonColor = System.Drawing.Color.DarkBlue;
            this.extRichTextBoxPaste.ShowLineCount = false;
            this.extRichTextBoxPaste.Size = new System.Drawing.Size(423, 145);
            this.extRichTextBoxPaste.TabIndex = 6;
            this.extRichTextBoxPaste.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.extRichTextBoxPaste.TextBoxForeColor = System.Drawing.SystemColors.ControlText;
            this.extRichTextBoxPaste.TextBoxChanged += new ExtendedControls.ExtRichTextBox.OnTextBoxChanged(this.extRichTextBoxPaste_TextBoxChanged);
            // 
            // labelPaste
            // 
            this.labelPaste.AutoSize = true;
            this.labelPaste.Location = new System.Drawing.Point(5, 13);
            this.labelPaste.Name = "labelPaste";
            this.labelPaste.Size = new System.Drawing.Size(207, 13);
            this.labelPaste.TabIndex = 5;
            this.labelPaste.Text = "Click Paste or drag text or files to input box";
            // 
            // panelImportExclude
            // 
            this.panelImportExclude.BackColor = System.Drawing.SystemColors.Control;
            this.panelImportExclude.Controls.Add(this.extCheckBoxExcludeHeader);
            this.panelImportExclude.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelImportExclude.Location = new System.Drawing.Point(0, 169);
            this.panelImportExclude.Name = "panelImportExclude";
            this.panelImportExclude.Size = new System.Drawing.Size(470, 25);
            this.panelImportExclude.TabIndex = 11;
            // 
            // extCheckBoxExcludeHeader
            // 
            this.extCheckBoxExcludeHeader.AutoSize = true;
            this.extCheckBoxExcludeHeader.CheckBoxColor = System.Drawing.Color.Gray;
            this.extCheckBoxExcludeHeader.CheckBoxDisabledScaling = 0.5F;
            this.extCheckBoxExcludeHeader.CheckBoxInnerColor = System.Drawing.Color.White;
            this.extCheckBoxExcludeHeader.CheckColor = System.Drawing.Color.DarkBlue;
            this.extCheckBoxExcludeHeader.Checked = true;
            this.extCheckBoxExcludeHeader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.extCheckBoxExcludeHeader.ImageButtonDisabledScaling = 0.5F;
            this.extCheckBoxExcludeHeader.ImageIndeterminate = null;
            this.extCheckBoxExcludeHeader.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.extCheckBoxExcludeHeader.ImageUnchecked = null;
            this.extCheckBoxExcludeHeader.Location = new System.Drawing.Point(8, 5);
            this.extCheckBoxExcludeHeader.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.extCheckBoxExcludeHeader.Name = "extCheckBoxExcludeHeader";
            this.extCheckBoxExcludeHeader.Size = new System.Drawing.Size(127, 17);
            this.extCheckBoxExcludeHeader.TabIndex = 1;
            this.extCheckBoxExcludeHeader.Text = "Exclude Header Row";
            this.extCheckBoxExcludeHeader.TickBoxReductionRatio = 0.75F;
            this.extCheckBoxExcludeHeader.UseVisualStyleBackColor = true;
            // 
            // ImportExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 488);
            this.Controls.Add(this.panelOuter);
            this.Controls.Add(this.panelTop);
            this.Name = "ImportExportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export data";
            this.panelBottom.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelCombo.ResumeLayout(false);
            this.panelDate.ResumeLayout(false);
            this.panelDate.PerformLayout();
            this.panelIncludeOpen.ResumeLayout(false);
            this.panelIncludeOpen.PerformLayout();
            this.panelCSV.ResumeLayout(false);
            this.panelCSV.PerformLayout();
            this.panelOuter.ResumeLayout(false);
            this.panelSaveImportAs.ResumeLayout(false);
            this.panelSaveImportAs.PerformLayout();
            this.panelPasteImport.ResumeLayout(false);
            this.panelPasteImport.PerformLayout();
            this.panelImportExclude.ResumeLayout(false);
            this.panelImportExclude.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBottom;
        private ExtendedControls.ExtRadioButton radioButtonSemiColon;
        private ExtendedControls.ExtRadioButton radioButtonComma;
        private System.Windows.Forms.Label labelCVSSep;
        private ExtendedControls.ExtCheckBox checkBoxIncludeHeader;
        private ExtendedControls.ExtDateTimePicker customDateTimePickerFrom;
        private ExtendedControls.ExtDateTimePicker customDateTimePickerTo;
        private ExtendedControls.ExtComboBox comboBoxSelectedType;
        private ExtendedControls.ExtButton buttonCancel;
        private ExtendedControls.ExtButtonDrawn panel_close;
        private ExtendedControls.ExtButtonDrawn panel_minimize;
        private System.Windows.Forms.Label label_index;
        private ExtendedControls.ExtCheckBox checkBoxCustomAutoOpen;
        private ExtendedControls.ExtButton buttonExport;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelUTCEnd;
        private System.Windows.Forms.Label labelUTCStart;
        private System.Windows.Forms.Panel panelCombo;
        private System.Windows.Forms.Panel panelDate;
        private System.Windows.Forms.Panel panelIncludeOpen;
        private System.Windows.Forms.Panel panelCSV;
        private System.Windows.Forms.Panel panelOuter;
        private System.Windows.Forms.Panel panelPasteImport;
        private System.Windows.Forms.Label labelPaste;
        private ExtendedControls.ExtRichTextBox extRichTextBoxPaste;
        private ExtendedControls.ExtRadioButton extRadioButtonTab;
        private ExtendedControls.ExtButton buttonPasteData;
        private System.Windows.Forms.Panel panelImportExclude;
        private ExtendedControls.ExtCheckBox extCheckBoxExcludeHeader;
        private ExtendedControls.ExtTextBox extTextBoxSaveImport;
        private System.Windows.Forms.Label labelSaveImport;
        private System.Windows.Forms.Panel panelSaveImportAs;
    }
}