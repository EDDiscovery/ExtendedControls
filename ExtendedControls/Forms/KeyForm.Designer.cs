﻿namespace ExtendedControls
{
    partial class KeyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyForm));
            this.checkBoxShift = new ExtendedControls.ExtCheckBox();
            this.checkBoxCtrl = new ExtendedControls.ExtCheckBox();
            this.checkBoxAlt = new ExtendedControls.ExtCheckBox();
            this.checkBoxKey = new ExtendedControls.ExtCheckBox();
            this.buttonReset = new ExtendedControls.ExtButton();
            this.buttonNext = new ExtendedControls.ExtButton();
            this.textBoxKeys = new ExtendedControls.ExtTextBox();
            this.labelKeys = new System.Windows.Forms.Label();
            this.buttonTest = new ExtendedControls.ExtButton();
            this.textBoxSendTo = new ExtendedControls.ExtTextBoxAutoComplete();
            this.labelSendTo = new System.Windows.Forms.Label();
            this.buttonOK = new ExtendedControls.ExtButton();
            this.buttonCancel = new ExtendedControls.ExtButton();
            this.labelCaption = new System.Windows.Forms.Label();
            this.panelOuter = new System.Windows.Forms.Panel();
            this.comboBoxKeySelector = new ExtendedControls.ExtComboBox();
            this.panelRadio = new System.Windows.Forms.Panel();
            this.radioButtonUp = new ExtendedControls.ExtRadioButton();
            this.radioButtonDown = new ExtendedControls.ExtRadioButton();
            this.radioButtonPress = new ExtendedControls.ExtRadioButton();
            this.textBoxNextDelay = new ExtendedControls.ExtTextBox();
            this.textBoxDelay = new ExtendedControls.ExtTextBox();
            this.labelNextDelay = new System.Windows.Forms.Label();
            this.labelSelKeys = new System.Windows.Forms.Label();
            this.labelDelay = new System.Windows.Forms.Label();
            this.buttonDelete = new ExtendedControls.ExtButton();
            this.panelOuter.SuspendLayout();
            this.panelRadio.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxShift
            // 
            this.checkBoxShift.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxShift.AutoCheck = false;
            this.checkBoxShift.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxShift.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxShift.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxShift.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxShift.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxShift.Location = new System.Drawing.Point(12, 15);
            this.checkBoxShift.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxShift.Name = "checkBoxShift";
            this.checkBoxShift.Size = new System.Drawing.Size(56, 56);
            this.checkBoxShift.TabIndex = 0;
            this.checkBoxShift.Text = "Shift";
            this.checkBoxShift.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxShift.TickBoxReductionRatio = 0.75f;
            this.checkBoxShift.UseVisualStyleBackColor = true;
            this.checkBoxShift.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkBoxsac_MouseDown);
            // 
            // checkBoxCtrl
            // 
            this.checkBoxCtrl.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxCtrl.AutoCheck = false;
            this.checkBoxCtrl.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxCtrl.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxCtrl.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxCtrl.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxCtrl.Location = new System.Drawing.Point(87, 15);
            this.checkBoxCtrl.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxCtrl.Name = "checkBoxCtrl";
            this.checkBoxCtrl.Size = new System.Drawing.Size(56, 56);
            this.checkBoxCtrl.TabIndex = 0;
            this.checkBoxCtrl.Text = "Ctrl";
            this.checkBoxCtrl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxCtrl.TickBoxReductionRatio = 0.75f;
            this.checkBoxCtrl.UseVisualStyleBackColor = true;
            this.checkBoxCtrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkBoxsac_MouseDown);
            // 
            // checkBoxAlt
            // 
            this.checkBoxAlt.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxAlt.AutoCheck = false;
            this.checkBoxAlt.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxAlt.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxAlt.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxAlt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxAlt.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxAlt.Location = new System.Drawing.Point(163, 15);
            this.checkBoxAlt.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxAlt.Name = "checkBoxAlt";
            this.checkBoxAlt.Size = new System.Drawing.Size(56, 56);
            this.checkBoxAlt.TabIndex = 0;
            this.checkBoxAlt.Text = "Alt";
            this.checkBoxAlt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxAlt.TickBoxReductionRatio = 0.75f;
            this.checkBoxAlt.UseVisualStyleBackColor = true;
            this.checkBoxAlt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkBoxsac_MouseDown);
            // 
            // checkBoxKey
            // 
            this.checkBoxKey.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxKey.AutoCheck = false;
            this.checkBoxKey.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxKey.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxKey.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxKey.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxKey.Location = new System.Drawing.Point(247, 15);
            this.checkBoxKey.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxKey.Name = "checkBoxKey";
            this.checkBoxKey.Size = new System.Drawing.Size(100, 56);
            this.checkBoxKey.TabIndex = 0;
            this.checkBoxKey.Text = "Press Key";
            this.checkBoxKey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxKey.TickBoxReductionRatio = 0.75f;
            this.checkBoxKey.UseVisualStyleBackColor = true;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(3, 233);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(100, 23);
            this.buttonReset.TabIndex = 1;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(247, 233);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(100, 23);
            this.buttonNext.TabIndex = 1;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // textBoxKeys
            // 
            this.textBoxKeys.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.textBoxKeys.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.textBoxKeys.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxKeys.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxKeys.BorderColorScaling = 0.5F;
            this.textBoxKeys.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxKeys.ClearOnFirstChar = false;
            this.textBoxKeys.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxKeys.EndButtonEnable = true;
            this.textBoxKeys.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("textBoxKeys.EndButtonImage")));
            this.textBoxKeys.EndButtonVisible = false;
            this.textBoxKeys.InErrorCondition = false;
            this.textBoxKeys.Location = new System.Drawing.Point(87, 203);
            this.textBoxKeys.Multiline = false;
            this.textBoxKeys.Name = "textBoxKeys";
            this.textBoxKeys.ReadOnly = true;
            this.textBoxKeys.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxKeys.SelectionLength = 0;
            this.textBoxKeys.SelectionStart = 0;
            this.textBoxKeys.Size = new System.Drawing.Size(260, 20);
            this.textBoxKeys.TabIndex = 2;
            this.textBoxKeys.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxKeys.WordWrap = true;
            this.textBoxKeys.Enter += new System.EventHandler(this.textBoxKeys_Enter);
            this.textBoxKeys.Leave += new System.EventHandler(this.textBoxKeys_Leave);
            // 
            // labelKeys
            // 
            this.labelKeys.AutoSize = true;
            this.labelKeys.Location = new System.Drawing.Point(3, 203);
            this.labelKeys.Name = "labelKeys";
            this.labelKeys.Size = new System.Drawing.Size(30, 13);
            this.labelKeys.TabIndex = 3;
            this.labelKeys.Text = "Keys";
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(247, 277);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(100, 23);
            this.buttonTest.TabIndex = 1;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // textBoxSendTo
            // 
            this.textBoxSendTo.AutoCompleteCommentMarker = null;
            this.textBoxSendTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.textBoxSendTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.textBoxSendTo.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxSendTo.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxSendTo.BorderColorScaling = 0.5F;
            this.textBoxSendTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSendTo.ClearOnFirstChar = false;
            this.textBoxSendTo.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxSendTo.DropDownBackgroundColor = System.Drawing.Color.Gray;
            this.textBoxSendTo.DropDownBorderColor = System.Drawing.Color.Gray;
            this.textBoxSendTo.DropDownMouseOverBackgroundColor = System.Drawing.Color.Red;
            this.textBoxSendTo.DropDownScrollBarButtonColor = System.Drawing.Color.LightGray;
            this.textBoxSendTo.DropDownScrollBarColor = System.Drawing.Color.LightGray;
            this.textBoxSendTo.EndButtonEnable = false;
            this.textBoxSendTo.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("textBoxSendTo.EndButtonImage")));
            this.textBoxSendTo.EndButtonVisible = false;
            this.textBoxSendTo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.textBoxSendTo.InErrorCondition = false;
            this.textBoxSendTo.Location = new System.Drawing.Point(125, 277);
            this.textBoxSendTo.Multiline = false;
            this.textBoxSendTo.Name = "textBoxSendTo";
            this.textBoxSendTo.ReadOnly = false;
            this.textBoxSendTo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxSendTo.SelectionLength = 0;
            this.textBoxSendTo.SelectionStart = 0;
            this.textBoxSendTo.Size = new System.Drawing.Size(116, 20);
            this.textBoxSendTo.TabIndex = 2;
            this.textBoxSendTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxSendTo.WordWrap = true;
            this.textBoxSendTo.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBoxSendTo.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // labelSendTo
            // 
            this.labelSendTo.AutoSize = true;
            this.labelSendTo.Location = new System.Drawing.Point(3, 277);
            this.labelSendTo.Name = "labelSendTo";
            this.labelSendTo.Size = new System.Drawing.Size(48, 13);
            this.labelSendTo.TabIndex = 3;
            this.labelSendTo.Text = "Send To";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(270, 355);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(164, 355);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelCaption
            // 
            this.labelCaption.AutoSize = true;
            this.labelCaption.Location = new System.Drawing.Point(3, 3);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(43, 13);
            this.labelCaption.TabIndex = 5;
            this.labelCaption.Text = "<code>";
            this.labelCaption.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelOuter_MouseDown);
            // 
            // panelOuter
            // 
            this.panelOuter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOuter.Controls.Add(this.comboBoxKeySelector);
            this.panelOuter.Controls.Add(this.panelRadio);
            this.panelOuter.Controls.Add(this.textBoxNextDelay);
            this.panelOuter.Controls.Add(this.textBoxDelay);
            this.panelOuter.Controls.Add(this.checkBoxShift);
            this.panelOuter.Controls.Add(this.checkBoxCtrl);
            this.panelOuter.Controls.Add(this.checkBoxAlt);
            this.panelOuter.Controls.Add(this.checkBoxKey);
            this.panelOuter.Controls.Add(this.labelNextDelay);
            this.panelOuter.Controls.Add(this.labelSelKeys);
            this.panelOuter.Controls.Add(this.labelDelay);
            this.panelOuter.Controls.Add(this.labelSendTo);
            this.panelOuter.Controls.Add(this.buttonReset);
            this.panelOuter.Controls.Add(this.labelKeys);
            this.panelOuter.Controls.Add(this.buttonDelete);
            this.panelOuter.Controls.Add(this.buttonNext);
            this.panelOuter.Controls.Add(this.textBoxSendTo);
            this.panelOuter.Controls.Add(this.buttonTest);
            this.panelOuter.Controls.Add(this.textBoxKeys);
            this.panelOuter.Location = new System.Drawing.Point(3, 24);
            this.panelOuter.Name = "panelOuter";
            this.panelOuter.Size = new System.Drawing.Size(367, 322);
            this.panelOuter.TabIndex = 6;
            this.panelOuter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelOuter_MouseDown);
            // 
            // comboBoxKeySelector
            // 
            this.comboBoxKeySelector.BorderColor = System.Drawing.Color.White;
            this.comboBoxKeySelector.ButtonColorScaling = 0.5F;
            this.comboBoxKeySelector.DataSource = null;
            this.comboBoxKeySelector.DisableBackgroundDisabledShadingGradient = false;
            this.comboBoxKeySelector.DisplayMember = "";
            this.comboBoxKeySelector.DropDownBackgroundColor = System.Drawing.Color.Gray;
            this.comboBoxKeySelector.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxKeySelector.Location = new System.Drawing.Point(185, 86);
            this.comboBoxKeySelector.MouseOverBackgroundColor = System.Drawing.Color.Silver;
            this.comboBoxKeySelector.Name = "comboBoxKeySelector";
            this.comboBoxKeySelector.ScrollBarButtonColor = System.Drawing.Color.LightGray;
            this.comboBoxKeySelector.ScrollBarColor = System.Drawing.Color.LightGray;
            this.comboBoxKeySelector.SelectedIndex = -1;
            this.comboBoxKeySelector.SelectedItem = null;
            this.comboBoxKeySelector.SelectedValue = null;
            this.comboBoxKeySelector.Size = new System.Drawing.Size(162, 21);
            this.comboBoxKeySelector.TabIndex = 6;
            this.comboBoxKeySelector.Text = "comboBoxCustom1";
            this.comboBoxKeySelector.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.comboBoxKeySelector.ValueMember = "";
            // 
            // panelRadio
            // 
            this.panelRadio.Controls.Add(this.radioButtonUp);
            this.panelRadio.Controls.Add(this.radioButtonDown);
            this.panelRadio.Controls.Add(this.radioButtonPress);
            this.panelRadio.Location = new System.Drawing.Point(247, 122);
            this.panelRadio.Name = "panelRadio";
            this.panelRadio.Size = new System.Drawing.Size(100, 72);
            this.panelRadio.TabIndex = 5;
            // 
            // radioButtonUp
            // 
            this.radioButtonUp.AutoSize = true;
            this.radioButtonUp.Location = new System.Drawing.Point(4, 46);
            this.radioButtonUp.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.radioButtonUp.Name = "radioButtonUp";
            this.radioButtonUp.RadioButtonColor = System.Drawing.Color.Gray;
            this.radioButtonUp.RadioButtonInnerColor = System.Drawing.Color.White;
            this.radioButtonUp.SelectedColor = System.Drawing.Color.DarkBlue;
            this.radioButtonUp.SelectedColorRing = System.Drawing.Color.Black;
            this.radioButtonUp.Size = new System.Drawing.Size(39, 17);
            this.radioButtonUp.TabIndex = 0;
            this.radioButtonUp.TabStop = true;
            this.radioButtonUp.Text = "Up";
            this.radioButtonUp.UseVisualStyleBackColor = true;
            this.radioButtonUp.CheckedChanged += new System.EventHandler(this.radioButtonPress_CheckedChanged);
            // 
            // radioButtonDown
            // 
            this.radioButtonDown.AutoSize = true;
            this.radioButtonDown.Location = new System.Drawing.Point(4, 24);
            this.radioButtonDown.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.radioButtonDown.Name = "radioButtonDown";
            this.radioButtonDown.RadioButtonColor = System.Drawing.Color.Gray;
            this.radioButtonDown.RadioButtonInnerColor = System.Drawing.Color.White;
            this.radioButtonDown.SelectedColor = System.Drawing.Color.DarkBlue;
            this.radioButtonDown.SelectedColorRing = System.Drawing.Color.Black;
            this.radioButtonDown.Size = new System.Drawing.Size(53, 17);
            this.radioButtonDown.TabIndex = 0;
            this.radioButtonDown.TabStop = true;
            this.radioButtonDown.Text = "Down";
            this.radioButtonDown.UseVisualStyleBackColor = true;
            this.radioButtonDown.CheckedChanged += new System.EventHandler(this.radioButtonPress_CheckedChanged);
            // 
            // radioButtonPress
            // 
            this.radioButtonPress.AutoSize = true;
            this.radioButtonPress.Location = new System.Drawing.Point(4, 3);
            this.radioButtonPress.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.radioButtonPress.Name = "radioButtonPress";
            this.radioButtonPress.RadioButtonColor = System.Drawing.Color.Gray;
            this.radioButtonPress.RadioButtonInnerColor = System.Drawing.Color.White;
            this.radioButtonPress.SelectedColor = System.Drawing.Color.DarkBlue;
            this.radioButtonPress.SelectedColorRing = System.Drawing.Color.Black;
            this.radioButtonPress.Size = new System.Drawing.Size(51, 17);
            this.radioButtonPress.TabIndex = 0;
            this.radioButtonPress.TabStop = true;
            this.radioButtonPress.Text = "Press";
            this.radioButtonPress.UseVisualStyleBackColor = true;
            this.radioButtonPress.CheckedChanged += new System.EventHandler(this.radioButtonPress_CheckedChanged);
            // 
            // textBoxNextDelay
            // 
            this.textBoxNextDelay.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.textBoxNextDelay.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.textBoxNextDelay.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxNextDelay.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxNextDelay.BorderColorScaling = 0.5F;
            this.textBoxNextDelay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxNextDelay.ClearOnFirstChar = false;
            this.textBoxNextDelay.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxNextDelay.EndButtonEnable = true;
            this.textBoxNextDelay.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("textBoxNextDelay.EndButtonImage")));
            this.textBoxNextDelay.EndButtonVisible = false;
            this.textBoxNextDelay.InErrorCondition = false;
            this.textBoxNextDelay.Location = new System.Drawing.Point(185, 148);
            this.textBoxNextDelay.Multiline = false;
            this.textBoxNextDelay.Name = "textBoxNextDelay";
            this.textBoxNextDelay.ReadOnly = false;
            this.textBoxNextDelay.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxNextDelay.SelectionLength = 0;
            this.textBoxNextDelay.SelectionStart = 0;
            this.textBoxNextDelay.Size = new System.Drawing.Size(50, 20);
            this.textBoxNextDelay.TabIndex = 4;
            this.textBoxNextDelay.Text = "50";
            this.textBoxNextDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxNextDelay.WordWrap = true;
            this.textBoxNextDelay.TextChanged += new System.EventHandler(this.textBoxNextDelay_TextChanged);
            this.textBoxNextDelay.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBoxNextDelay.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // textBoxDelay
            // 
            this.textBoxDelay.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.textBoxDelay.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.textBoxDelay.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxDelay.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxDelay.BorderColorScaling = 0.5F;
            this.textBoxDelay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDelay.ClearOnFirstChar = false;
            this.textBoxDelay.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxDelay.EndButtonEnable = true;
            this.textBoxDelay.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("textBoxDelay.EndButtonImage")));
            this.textBoxDelay.EndButtonVisible = false;
            this.textBoxDelay.InErrorCondition = false;
            this.textBoxDelay.Location = new System.Drawing.Point(185, 122);
            this.textBoxDelay.Multiline = false;
            this.textBoxDelay.Name = "textBoxDelay";
            this.textBoxDelay.ReadOnly = false;
            this.textBoxDelay.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxDelay.SelectionLength = 0;
            this.textBoxDelay.SelectionStart = 0;
            this.textBoxDelay.Size = new System.Drawing.Size(50, 20);
            this.textBoxDelay.TabIndex = 4;
            this.textBoxDelay.Text = "50";
            this.textBoxDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxDelay.WordWrap = true;
            this.textBoxDelay.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBoxDelay.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // labelNextDelay
            // 
            this.labelNextDelay.AutoSize = true;
            this.labelNextDelay.Location = new System.Drawing.Point(3, 148);
            this.labelNextDelay.Name = "labelNextDelay";
            this.labelNextDelay.Size = new System.Drawing.Size(71, 13);
            this.labelNextDelay.TabIndex = 3;
            this.labelNextDelay.Text = "Current Delay";
            // 
            // labelSelKeys
            // 
            this.labelSelKeys.AutoSize = true;
            this.labelSelKeys.Location = new System.Drawing.Point(3, 86);
            this.labelSelKeys.Name = "labelSelKeys";
            this.labelSelKeys.Size = new System.Drawing.Size(58, 13);
            this.labelSelKeys.TabIndex = 3;
            this.labelSelKeys.Text = "Select Key";
            // 
            // labelDelay
            // 
            this.labelDelay.AutoSize = true;
            this.labelDelay.Location = new System.Drawing.Point(3, 122);
            this.labelDelay.Name = "labelDelay";
            this.labelDelay.Size = new System.Drawing.Size(78, 13);
            this.labelDelay.TabIndex = 3;
            this.labelDelay.Text = "Start Delay(ms)";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(125, 233);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(100, 23);
            this.buttonDelete.TabIndex = 1;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // KeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 397);
            this.Controls.Add(this.panelOuter);
            this.Controls.Add(this.labelCaption);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Name = "KeyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Define Key Sequence";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.KeyForm_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KeyForm_MouseUp);
            this.panelOuter.ResumeLayout(false);
            this.panelOuter.PerformLayout();
            this.panelRadio.ResumeLayout(false);
            this.panelRadio.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExtendedControls.ExtCheckBox checkBoxShift;
        private ExtCheckBox checkBoxCtrl;
        private ExtCheckBox checkBoxAlt;
        private ExtCheckBox checkBoxKey;
        private ExtButton buttonReset;
        private ExtButton buttonNext;
        private ExtTextBox textBoxKeys;
        private System.Windows.Forms.Label labelKeys;
        private ExtButton buttonTest;
        private ExtTextBoxAutoComplete textBoxSendTo;
        private System.Windows.Forms.Label labelSendTo;
        private ExtButton buttonOK;
        private ExtButton buttonCancel;
        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.Panel panelOuter;
        private ExtButton buttonDelete;
        private ExtTextBox textBoxDelay;
        private System.Windows.Forms.Label labelDelay;
        private ExtTextBox textBoxNextDelay;
        private System.Windows.Forms.Label labelNextDelay;
        private System.Windows.Forms.Panel panelRadio;
        private ExtRadioButton radioButtonUp;
        private ExtRadioButton radioButtonDown;
        private ExtRadioButton radioButtonPress;
        private ExtComboBox comboBoxKeySelector;
        private System.Windows.Forms.Label labelSelKeys;
    }
}