using ExtendedControls;

namespace ExtendedForms
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
            this.textBoxDefaultDelay = new ExtendedControls.NumberBoxLong();
            this.comboBoxKeySelector = new ExtendedControls.ExtComboBox();
            this.panelRadio = new System.Windows.Forms.Panel();
            this.radioButtonUp = new ExtendedControls.ExtRadioButton();
            this.radioButtonDown = new ExtendedControls.ExtRadioButton();
            this.radioButtonPress = new ExtendedControls.ExtRadioButton();
            this.textBoxCurrentKeyDelay = new ExtendedControls.ExtTextBox();
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
            this.checkBoxShift.ButtonGradientDirection = 90F;
            this.checkBoxShift.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxShift.CheckBoxGradientDirection = 225F;
            this.checkBoxShift.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxShift.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxShift.CheckColor2 = System.Drawing.Color.DarkBlue;
            this.checkBoxShift.DisabledScaling = 0.5F;
            this.checkBoxShift.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxShift.ImageIndeterminate = null;
            this.checkBoxShift.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxShift.ImageUnchecked = null;
            this.checkBoxShift.Location = new System.Drawing.Point(12, 15);
            this.checkBoxShift.MouseOverScaling = 1.3F;
            this.checkBoxShift.MouseSelectedScaling = 1.3F;
            this.checkBoxShift.Name = "checkBoxShift";
            this.checkBoxShift.Size = new System.Drawing.Size(56, 56);
            this.checkBoxShift.TabIndex = 0;
            this.checkBoxShift.Text = "Shift";
            this.checkBoxShift.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxShift.TickBoxReductionRatio = 0.75F;
            this.checkBoxShift.UseVisualStyleBackColor = true;
            this.checkBoxShift.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkBoxsac_MouseDown);
            // 
            // checkBoxCtrl
            // 
            this.checkBoxCtrl.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxCtrl.AutoCheck = false;
            this.checkBoxCtrl.ButtonGradientDirection = 90F;
            this.checkBoxCtrl.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxCtrl.CheckBoxGradientDirection = 225F;
            this.checkBoxCtrl.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxCtrl.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxCtrl.CheckColor2 = System.Drawing.Color.DarkBlue;
            this.checkBoxCtrl.DisabledScaling = 0.5F;
            this.checkBoxCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxCtrl.ImageIndeterminate = null;
            this.checkBoxCtrl.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxCtrl.ImageUnchecked = null;
            this.checkBoxCtrl.Location = new System.Drawing.Point(87, 15);
            this.checkBoxCtrl.MouseOverScaling = 1.3F;
            this.checkBoxCtrl.MouseSelectedScaling = 1.3F;
            this.checkBoxCtrl.Name = "checkBoxCtrl";
            this.checkBoxCtrl.Size = new System.Drawing.Size(56, 56);
            this.checkBoxCtrl.TabIndex = 0;
            this.checkBoxCtrl.Text = "Ctrl";
            this.checkBoxCtrl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxCtrl.TickBoxReductionRatio = 0.75F;
            this.checkBoxCtrl.UseVisualStyleBackColor = true;
            this.checkBoxCtrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkBoxsac_MouseDown);
            // 
            // checkBoxAlt
            // 
            this.checkBoxAlt.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxAlt.AutoCheck = false;
            this.checkBoxAlt.ButtonGradientDirection = 90F;
            this.checkBoxAlt.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxAlt.CheckBoxGradientDirection = 225F;
            this.checkBoxAlt.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxAlt.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxAlt.CheckColor2 = System.Drawing.Color.DarkBlue;
            this.checkBoxAlt.DisabledScaling = 0.5F;
            this.checkBoxAlt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxAlt.ImageIndeterminate = null;
            this.checkBoxAlt.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxAlt.ImageUnchecked = null;
            this.checkBoxAlt.Location = new System.Drawing.Point(163, 15);
            this.checkBoxAlt.MouseOverScaling = 1.3F;
            this.checkBoxAlt.MouseSelectedScaling = 1.3F;
            this.checkBoxAlt.Name = "checkBoxAlt";
            this.checkBoxAlt.Size = new System.Drawing.Size(56, 56);
            this.checkBoxAlt.TabIndex = 0;
            this.checkBoxAlt.Text = "Alt";
            this.checkBoxAlt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxAlt.TickBoxReductionRatio = 0.75F;
            this.checkBoxAlt.UseVisualStyleBackColor = true;
            this.checkBoxAlt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkBoxsac_MouseDown);
            // 
            // checkBoxKey
            // 
            this.checkBoxKey.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxKey.AutoCheck = false;
            this.checkBoxKey.ButtonGradientDirection = 90F;
            this.checkBoxKey.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxKey.CheckBoxGradientDirection = 225F;
            this.checkBoxKey.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxKey.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxKey.CheckColor2 = System.Drawing.Color.DarkBlue;
            this.checkBoxKey.DisabledScaling = 0.5F;
            this.checkBoxKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxKey.ImageIndeterminate = null;
            this.checkBoxKey.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxKey.ImageUnchecked = null;
            this.checkBoxKey.Location = new System.Drawing.Point(247, 15);
            this.checkBoxKey.MouseOverScaling = 1.3F;
            this.checkBoxKey.MouseSelectedScaling = 1.3F;
            this.checkBoxKey.Name = "checkBoxKey";
            this.checkBoxKey.Size = new System.Drawing.Size(100, 56);
            this.checkBoxKey.TabIndex = 0;
            this.checkBoxKey.Text = "Press Key";
            this.checkBoxKey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxKey.TickBoxReductionRatio = 0.75F;
            this.checkBoxKey.UseVisualStyleBackColor = true;
            // 
            // buttonReset
            // 
            this.buttonReset.BackColor2 = System.Drawing.Color.Red;
            this.buttonReset.ButtonDisabledScaling = 0.5F;
            this.buttonReset.GradientDirection = 90F;
            this.buttonReset.Location = new System.Drawing.Point(3, 233);
            this.buttonReset.MouseOverScaling = 1.3F;
            this.buttonReset.MouseSelectedScaling = 1.3F;
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(100, 23);
            this.buttonReset.TabIndex = 1;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.BackColor2 = System.Drawing.Color.Red;
            this.buttonNext.ButtonDisabledScaling = 0.5F;
            this.buttonNext.GradientDirection = 90F;
            this.buttonNext.Location = new System.Drawing.Point(247, 233);
            this.buttonNext.MouseOverScaling = 1.3F;
            this.buttonNext.MouseSelectedScaling = 1.3F;
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(100, 23);
            this.buttonNext.TabIndex = 1;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // textBoxKeys
            // 
            this.textBoxKeys.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxKeys.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxKeys.BorderColor2 = System.Drawing.Color.Transparent;
            this.textBoxKeys.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxKeys.ClearOnFirstChar = false;
            this.textBoxKeys.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxKeys.EndButtonEnable = true;
            this.textBoxKeys.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("textBoxKeys.EndButtonImage")));
            this.textBoxKeys.EndButtonSize16ths = 10;
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
            this.textBoxKeys.TextNoChange = "";
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
            this.buttonTest.BackColor2 = System.Drawing.Color.Red;
            this.buttonTest.ButtonDisabledScaling = 0.5F;
            this.buttonTest.GradientDirection = 90F;
            this.buttonTest.Location = new System.Drawing.Point(247, 277);
            this.buttonTest.MouseOverScaling = 1.3F;
            this.buttonTest.MouseSelectedScaling = 1.3F;
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
            this.textBoxSendTo.AutoCompleteTimeout = 500;
            this.textBoxSendTo.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxSendTo.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxSendTo.BorderColor2 = System.Drawing.Color.Transparent;
            this.textBoxSendTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSendTo.ClearOnFirstChar = false;
            this.textBoxSendTo.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxSendTo.EndButtonEnable = false;
            this.textBoxSendTo.EndButtonSize16ths = 10;
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
            this.textBoxSendTo.TextChangedEvent = "";
            this.textBoxSendTo.TextNoChange = "";
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
            this.buttonOK.BackColor2 = System.Drawing.Color.Red;
            this.buttonOK.ButtonDisabledScaling = 0.5F;
            this.buttonOK.GradientDirection = 90F;
            this.buttonOK.Location = new System.Drawing.Point(270, 355);
            this.buttonOK.MouseOverScaling = 1.3F;
            this.buttonOK.MouseSelectedScaling = 1.3F;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor2 = System.Drawing.Color.Red;
            this.buttonCancel.ButtonDisabledScaling = 0.5F;
            this.buttonCancel.GradientDirection = 90F;
            this.buttonCancel.Location = new System.Drawing.Point(164, 355);
            this.buttonCancel.MouseOverScaling = 1.3F;
            this.buttonCancel.MouseSelectedScaling = 1.3F;
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
            this.panelOuter.Controls.Add(this.textBoxDefaultDelay);
            this.panelOuter.Controls.Add(this.comboBoxKeySelector);
            this.panelOuter.Controls.Add(this.panelRadio);
            this.panelOuter.Controls.Add(this.textBoxCurrentKeyDelay);
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
            // textBoxDefaultDelay
            // 
            this.textBoxDefaultDelay.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxDefaultDelay.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxDefaultDelay.BorderColor2 = System.Drawing.Color.Transparent;
            this.textBoxDefaultDelay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDefaultDelay.ClearOnFirstChar = false;
            this.textBoxDefaultDelay.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxDefaultDelay.DelayBeforeNotification = 0;
            this.textBoxDefaultDelay.EndButtonEnable = true;
            this.textBoxDefaultDelay.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("textBoxDefaultDelay.EndButtonImage")));
            this.textBoxDefaultDelay.EndButtonSize16ths = 10;
            this.textBoxDefaultDelay.EndButtonVisible = false;
            this.textBoxDefaultDelay.Format = "D";
            this.textBoxDefaultDelay.InErrorCondition = false;
            this.textBoxDefaultDelay.Location = new System.Drawing.Point(201, 122);
            this.textBoxDefaultDelay.Maximum = ((long)(5000));
            this.textBoxDefaultDelay.Minimum = ((long)(0));
            this.textBoxDefaultDelay.Multiline = false;
            this.textBoxDefaultDelay.Name = "textBoxDefaultDelay";
            this.textBoxDefaultDelay.NumberStyles = ((System.Globalization.NumberStyles)((System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowThousands)));
            this.textBoxDefaultDelay.ReadOnly = false;
            this.textBoxDefaultDelay.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxDefaultDelay.SelectionLength = 0;
            this.textBoxDefaultDelay.SelectionStart = 0;
            this.textBoxDefaultDelay.Size = new System.Drawing.Size(50, 23);
            this.textBoxDefaultDelay.TabIndex = 7;
            this.textBoxDefaultDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxDefaultDelay.TextNoChange = "0";
            this.textBoxDefaultDelay.Value = ((long)(0));
            this.textBoxDefaultDelay.WordWrap = true;
            this.textBoxDefaultDelay.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBoxDefaultDelay.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // comboBoxKeySelector
            // 
            this.comboBoxKeySelector.BackColor2 = System.Drawing.Color.Red;
            this.comboBoxKeySelector.BorderColor = System.Drawing.Color.White;
            this.comboBoxKeySelector.ControlBackground = System.Drawing.SystemColors.Control;
            this.comboBoxKeySelector.DataSource = null;
            this.comboBoxKeySelector.DisableBackgroundDisabledShadingGradient = false;
            this.comboBoxKeySelector.DisabledScaling = 0.5F;
            this.comboBoxKeySelector.DisplayMember = "";
            this.comboBoxKeySelector.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxKeySelector.GradientDirection = 90F;
            this.comboBoxKeySelector.Location = new System.Drawing.Point(185, 86);
            this.comboBoxKeySelector.MouseOverScalingColor = 1.3F;
            this.comboBoxKeySelector.Name = "comboBoxKeySelector";
            this.comboBoxKeySelector.SelectedIndex = -1;
            this.comboBoxKeySelector.SelectedItem = null;
            this.comboBoxKeySelector.SelectedValue = null;
            this.comboBoxKeySelector.Size = new System.Drawing.Size(162, 21);
            this.comboBoxKeySelector.TabIndex = 6;
            this.comboBoxKeySelector.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.comboBoxKeySelector.ValueMember = "";
            // 
            // panelRadio
            // 
            this.panelRadio.Controls.Add(this.radioButtonUp);
            this.panelRadio.Controls.Add(this.radioButtonDown);
            this.panelRadio.Controls.Add(this.radioButtonPress);
            this.panelRadio.Location = new System.Drawing.Point(266, 122);
            this.panelRadio.Name = "panelRadio";
            this.panelRadio.Size = new System.Drawing.Size(81, 72);
            this.panelRadio.TabIndex = 5;
            // 
            // radioButtonUp
            // 
            this.radioButtonUp.AutoSize = true;
            this.radioButtonUp.DisabledScaling = 0.5F;
            this.radioButtonUp.GradientDirection = 225F;
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
            this.radioButtonDown.DisabledScaling = 0.5F;
            this.radioButtonDown.GradientDirection = 225F;
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
            this.radioButtonPress.DisabledScaling = 0.5F;
            this.radioButtonPress.GradientDirection = 225F;
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
            // textBoxCurrentKeyDelay
            // 
            this.textBoxCurrentKeyDelay.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxCurrentKeyDelay.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxCurrentKeyDelay.BorderColor2 = System.Drawing.Color.Transparent;
            this.textBoxCurrentKeyDelay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCurrentKeyDelay.ClearOnFirstChar = true;
            this.textBoxCurrentKeyDelay.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxCurrentKeyDelay.EndButtonEnable = true;
            this.textBoxCurrentKeyDelay.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("textBoxCurrentKeyDelay.EndButtonImage")));
            this.textBoxCurrentKeyDelay.EndButtonSize16ths = 10;
            this.textBoxCurrentKeyDelay.EndButtonVisible = false;
            this.textBoxCurrentKeyDelay.InErrorCondition = false;
            this.textBoxCurrentKeyDelay.Location = new System.Drawing.Point(201, 148);
            this.textBoxCurrentKeyDelay.Multiline = false;
            this.textBoxCurrentKeyDelay.Name = "textBoxCurrentKeyDelay";
            this.textBoxCurrentKeyDelay.ReadOnly = false;
            this.textBoxCurrentKeyDelay.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxCurrentKeyDelay.SelectionLength = 0;
            this.textBoxCurrentKeyDelay.SelectionStart = 0;
            this.textBoxCurrentKeyDelay.Size = new System.Drawing.Size(50, 20);
            this.textBoxCurrentKeyDelay.TabIndex = 4;
            this.textBoxCurrentKeyDelay.Text = "50";
            this.textBoxCurrentKeyDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxCurrentKeyDelay.TextNoChange = "50";
            this.textBoxCurrentKeyDelay.WordWrap = true;
            this.textBoxCurrentKeyDelay.TextChanged += new System.EventHandler(this.textBoxNextDelay_TextChanged);
            this.textBoxCurrentKeyDelay.Enter += new System.EventHandler(this.textBoxCurrentKeyDelay_Enter);
            this.textBoxCurrentKeyDelay.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // labelNextDelay
            // 
            this.labelNextDelay.AutoSize = true;
            this.labelNextDelay.Location = new System.Drawing.Point(3, 148);
            this.labelNextDelay.Name = "labelNextDelay";
            this.labelNextDelay.Size = new System.Drawing.Size(122, 13);
            this.labelNextDelay.TabIndex = 3;
            this.labelNextDelay.Text = "Current Key Delay String";
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
            this.labelDelay.Size = new System.Drawing.Size(157, 13);
            this.labelDelay.TabIndex = 3;
            this.labelDelay.Text = "Default Down Delay (0=Default)";
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor2 = System.Drawing.Color.Red;
            this.buttonDelete.ButtonDisabledScaling = 0.5F;
            this.buttonDelete.GradientDirection = 90F;
            this.buttonDelete.Location = new System.Drawing.Point(125, 233);
            this.buttonDelete.MouseOverScaling = 1.3F;
            this.buttonDelete.MouseSelectedScaling = 1.3F;
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
        private System.Windows.Forms.Label labelDelay;
        private ExtTextBox textBoxCurrentKeyDelay;
        private System.Windows.Forms.Label labelNextDelay;
        private System.Windows.Forms.Panel panelRadio;
        private ExtRadioButton radioButtonUp;
        private ExtRadioButton radioButtonDown;
        private ExtRadioButton radioButtonPress;
        private ExtComboBox comboBoxKeySelector;
        private System.Windows.Forms.Label labelSelKeys;
        private NumberBoxLong textBoxDefaultDelay;
    }
}