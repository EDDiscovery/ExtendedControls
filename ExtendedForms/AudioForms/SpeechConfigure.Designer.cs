﻿/*
 * Copyright © 2017 EDDiscovery development team
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this
 * file except in compliance with the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software distributed under
 * the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
 * ANY KIND, either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 * 
 * EDDiscovery is not affiliated with Frontier Developments plc.
 */
namespace ExtendedAudioForms
{
    partial class SpeechConfigure
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpeechConfigure));
            this.Title = new System.Windows.Forms.Label();
            this.labelVolume = new System.Windows.Forms.Label();
            this.labelRate = new System.Windows.Forms.Label();
            this.textBoxBorderText = new ExtendedControls.ExtTextBox();
            this.checkBoxCustomComplete = new ExtendedControls.ExtCheckBox();
            this.panelOuter = new System.Windows.Forms.Panel();
            this.textBoxBorderEndTrigger = new ExtendedControls.ExtTextBox();
            this.textBoxBorderStartTrigger = new ExtendedControls.ExtTextBox();
            this.comboBoxCustomPriority = new ExtendedControls.ExtComboBox();
            this.buttonExtTest = new ExtendedControls.ExtButton();
            this.buttonExtDevice = new ExtendedControls.ExtButton();
            this.buttonExtEffects = new ExtendedControls.ExtButton();
            this.trackBarVolume = new System.Windows.Forms.TrackBar();
            this.buttonExtCancel = new ExtendedControls.ExtButton();
            this.buttonExtOK = new ExtendedControls.ExtButton();
            this.comboBoxCustomVoice = new ExtendedControls.ExtComboBox();
            this.checkBoxCustomLiteral = new ExtendedControls.ExtCheckBox();
            this.textBoxBorderTest = new ExtendedControls.ExtTextBox();
            this.labelEndTrigger = new System.Windows.Forms.Label();
            this.labelStartTrigger = new System.Windows.Forms.Label();
            this.labelVoice = new System.Windows.Forms.Label();
            this.trackBarRate = new System.Windows.Forms.TrackBar();
            this.checkBoxCustomR = new ExtendedControls.ExtCheckBox();
            this.checkBoxCustomV = new ExtendedControls.ExtCheckBox();
            this.panelOuter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRate)).BeginInit();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.Location = new System.Drawing.Point(12, 12);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(376, 27);
            this.Title.TabIndex = 0;
            this.Title.Text = "Set Text to say (use ; to separate randomly selectable phrases and {} to group)";
            // 
            // labelVolume
            // 
            this.labelVolume.AutoSize = true;
            this.labelVolume.Location = new System.Drawing.Point(11, 261);
            this.labelVolume.Name = "labelVolume";
            this.labelVolume.Size = new System.Drawing.Size(42, 13);
            this.labelVolume.TabIndex = 0;
            this.labelVolume.Text = "Volume";
            // 
            // labelRate
            // 
            this.labelRate.AutoSize = true;
            this.labelRate.Location = new System.Drawing.Point(11, 319);
            this.labelRate.Name = "labelRate";
            this.labelRate.Size = new System.Drawing.Size(30, 13);
            this.labelRate.TabIndex = 0;
            this.labelRate.Text = "Rate";
            // 
            // textBoxBorderText
            // 
            this.textBoxBorderText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.textBoxBorderText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.textBoxBorderText.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxBorderText.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxBorderText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxBorderText.ClearOnFirstChar = false;
            this.textBoxBorderText.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxBorderText.EndButtonEnable = true;
            this.textBoxBorderText.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("textBoxBorderText.EndButtonImage")));
            this.textBoxBorderText.EndButtonSize16ths = 10;
            this.textBoxBorderText.EndButtonVisible = false;
            this.textBoxBorderText.InErrorCondition = false;
            this.textBoxBorderText.Location = new System.Drawing.Point(14, 46);
            this.textBoxBorderText.Multiline = true;
            this.textBoxBorderText.Name = "textBoxBorderText";
            this.textBoxBorderText.ReadOnly = false;
            this.textBoxBorderText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxBorderText.SelectionLength = 0;
            this.textBoxBorderText.SelectionStart = 0;
            this.textBoxBorderText.Size = new System.Drawing.Size(401, 74);
            this.textBoxBorderText.TabIndex = 0;
            this.textBoxBorderText.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxBorderText.TextNoChange = "";
            this.textBoxBorderText.WordWrap = false;
            // 
            // checkBoxCustomComplete
            // 
            this.checkBoxCustomComplete.AutoSize = true;
            this.checkBoxCustomComplete.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxCustomComplete.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxCustomComplete.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxCustomComplete.ImageIndeterminate = null;
            this.checkBoxCustomComplete.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxCustomComplete.ImageUnchecked = null;
            this.checkBoxCustomComplete.Location = new System.Drawing.Point(15, 132);
            this.checkBoxCustomComplete.Name = "checkBoxCustomComplete";
            this.checkBoxCustomComplete.Size = new System.Drawing.Size(159, 17);
            this.checkBoxCustomComplete.TabIndex = 1;
            this.checkBoxCustomComplete.Text = "Wait until speech completes";
            this.checkBoxCustomComplete.TickBoxReductionRatio = 0.75F;
            this.checkBoxCustomComplete.UseVisualStyleBackColor = true;
            // 
            // panelOuter
            // 
            this.panelOuter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOuter.Controls.Add(this.textBoxBorderEndTrigger);
            this.panelOuter.Controls.Add(this.textBoxBorderStartTrigger);
            this.panelOuter.Controls.Add(this.comboBoxCustomPriority);
            this.panelOuter.Controls.Add(this.buttonExtTest);
            this.panelOuter.Controls.Add(this.buttonExtDevice);
            this.panelOuter.Controls.Add(this.buttonExtEffects);
            this.panelOuter.Controls.Add(this.trackBarVolume);
            this.panelOuter.Controls.Add(this.buttonExtCancel);
            this.panelOuter.Controls.Add(this.buttonExtOK);
            this.panelOuter.Controls.Add(this.comboBoxCustomVoice);
            this.panelOuter.Controls.Add(this.checkBoxCustomLiteral);
            this.panelOuter.Controls.Add(this.checkBoxCustomComplete);
            this.panelOuter.Controls.Add(this.textBoxBorderTest);
            this.panelOuter.Controls.Add(this.textBoxBorderText);
            this.panelOuter.Controls.Add(this.labelEndTrigger);
            this.panelOuter.Controls.Add(this.labelStartTrigger);
            this.panelOuter.Controls.Add(this.labelRate);
            this.panelOuter.Controls.Add(this.labelVoice);
            this.panelOuter.Controls.Add(this.labelVolume);
            this.panelOuter.Controls.Add(this.trackBarRate);
            this.panelOuter.Controls.Add(this.checkBoxCustomR);
            this.panelOuter.Controls.Add(this.checkBoxCustomV);
            this.panelOuter.Controls.Add(this.Title);
            this.panelOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOuter.Location = new System.Drawing.Point(0, 0);
            this.panelOuter.Name = "panelOuter";
            this.panelOuter.Size = new System.Drawing.Size(433, 475);
            this.panelOuter.TabIndex = 0;
            this.panelOuter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CapMouseDown);
            this.panelOuter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CapMouseUp);
            // 
            // textBoxBorderEndTrigger
            // 
            this.textBoxBorderEndTrigger.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.textBoxBorderEndTrigger.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.textBoxBorderEndTrigger.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxBorderEndTrigger.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxBorderEndTrigger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxBorderEndTrigger.ClearOnFirstChar = false;
            this.textBoxBorderEndTrigger.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxBorderEndTrigger.EndButtonEnable = true;
            this.textBoxBorderEndTrigger.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("textBoxBorderEndTrigger.EndButtonImage")));
            this.textBoxBorderEndTrigger.EndButtonSize16ths = 10;
            this.textBoxBorderEndTrigger.EndButtonVisible = false;
            this.textBoxBorderEndTrigger.InErrorCondition = false;
            this.textBoxBorderEndTrigger.Location = new System.Drawing.Point(315, 165);
            this.textBoxBorderEndTrigger.Multiline = false;
            this.textBoxBorderEndTrigger.Name = "textBoxBorderEndTrigger";
            this.textBoxBorderEndTrigger.ReadOnly = false;
            this.textBoxBorderEndTrigger.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxBorderEndTrigger.SelectionLength = 0;
            this.textBoxBorderEndTrigger.SelectionStart = 0;
            this.textBoxBorderEndTrigger.Size = new System.Drawing.Size(100, 20);
            this.textBoxBorderEndTrigger.TabIndex = 12;
            this.textBoxBorderEndTrigger.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxBorderEndTrigger.TextNoChange = "";
            this.textBoxBorderEndTrigger.WordWrap = true;
            // 
            // textBoxBorderStartTrigger
            // 
            this.textBoxBorderStartTrigger.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.textBoxBorderStartTrigger.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.textBoxBorderStartTrigger.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxBorderStartTrigger.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxBorderStartTrigger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxBorderStartTrigger.ClearOnFirstChar = false;
            this.textBoxBorderStartTrigger.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxBorderStartTrigger.EndButtonEnable = true;
            this.textBoxBorderStartTrigger.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("textBoxBorderStartTrigger.EndButtonImage")));
            this.textBoxBorderStartTrigger.EndButtonSize16ths = 10;
            this.textBoxBorderStartTrigger.EndButtonVisible = false;
            this.textBoxBorderStartTrigger.InErrorCondition = false;
            this.textBoxBorderStartTrigger.Location = new System.Drawing.Point(93, 165);
            this.textBoxBorderStartTrigger.Multiline = false;
            this.textBoxBorderStartTrigger.Name = "textBoxBorderStartTrigger";
            this.textBoxBorderStartTrigger.ReadOnly = false;
            this.textBoxBorderStartTrigger.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxBorderStartTrigger.SelectionLength = 0;
            this.textBoxBorderStartTrigger.SelectionStart = 0;
            this.textBoxBorderStartTrigger.Size = new System.Drawing.Size(100, 20);
            this.textBoxBorderStartTrigger.TabIndex = 12;
            this.textBoxBorderStartTrigger.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxBorderStartTrigger.TextNoChange = "";
            this.textBoxBorderStartTrigger.WordWrap = true;
            // 
            // comboBoxCustomPriority
            // 
            this.comboBoxCustomPriority.BorderColor = System.Drawing.Color.White;
            this.comboBoxCustomPriority.DataSource = null;
            this.comboBoxCustomPriority.DisableBackgroundDisabledShadingGradient = false;
            this.comboBoxCustomPriority.DisplayMember = "";
            this.comboBoxCustomPriority.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxCustomPriority.Location = new System.Drawing.Point(305, 132);
            this.comboBoxCustomPriority.Name = "comboBoxCustomPriority";
            this.comboBoxCustomPriority.SelectedIndex = -1;
            this.comboBoxCustomPriority.SelectedItem = null;
            this.comboBoxCustomPriority.SelectedValue = null;
            this.comboBoxCustomPriority.Size = new System.Drawing.Size(110, 21);
            this.comboBoxCustomPriority.TabIndex = 11;
            this.comboBoxCustomPriority.Text = "comboBoxCustom1";
            this.comboBoxCustomPriority.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.comboBoxCustomPriority.ValueMember = "";
            // 
            // buttonExtTest
            // 
            this.buttonExtTest.Location = new System.Drawing.Point(340, 368);
            this.buttonExtTest.Name = "buttonExtTest";
            this.buttonExtTest.Size = new System.Drawing.Size(75, 23);
            this.buttonExtTest.TabIndex = 8;
            this.buttonExtTest.Text = "Test";
            this.buttonExtTest.UseVisualStyleBackColor = true;
            this.buttonExtTest.Click += new System.EventHandler(this.buttonExtTest_Click);
            // 
            // buttonExtDevice
            // 
            this.buttonExtDevice.Location = new System.Drawing.Point(340, 278);
            this.buttonExtDevice.Name = "buttonExtDevice";
            this.buttonExtDevice.Size = new System.Drawing.Size(75, 23);
            this.buttonExtDevice.TabIndex = 4;
            this.buttonExtDevice.Text = "Device";
            this.buttonExtDevice.UseVisualStyleBackColor = true;
            this.buttonExtDevice.Click += new System.EventHandler(this.buttonExtDevice_Click);
            // 
            // buttonExtEffects
            // 
            this.buttonExtEffects.Location = new System.Drawing.Point(340, 249);
            this.buttonExtEffects.Name = "buttonExtEffects";
            this.buttonExtEffects.Size = new System.Drawing.Size(75, 23);
            this.buttonExtEffects.TabIndex = 4;
            this.buttonExtEffects.Text = "Effects";
            this.buttonExtEffects.UseVisualStyleBackColor = true;
            this.buttonExtEffects.Click += new System.EventHandler(this.buttonExtEffects_Click);
            // 
            // trackBarVolume
            // 
            this.trackBarVolume.Location = new System.Drawing.Point(59, 249);
            this.trackBarVolume.Maximum = 100;
            this.trackBarVolume.Name = "trackBarVolume";
            this.trackBarVolume.Size = new System.Drawing.Size(203, 45);
            this.trackBarVolume.TabIndex = 5;
            this.trackBarVolume.TickFrequency = 5;
            this.trackBarVolume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarVolume.Value = 60;
            // 
            // buttonExtCancel
            // 
            this.buttonExtCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExtCancel.Location = new System.Drawing.Point(249, 433);
            this.buttonExtCancel.Name = "buttonExtCancel";
            this.buttonExtCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonExtCancel.TabIndex = 9;
            this.buttonExtCancel.Text = "%Cancel%";
            this.buttonExtCancel.UseVisualStyleBackColor = true;
            // 
            // buttonExtOK
            // 
            this.buttonExtOK.Location = new System.Drawing.Point(340, 433);
            this.buttonExtOK.Name = "buttonExtOK";
            this.buttonExtOK.Size = new System.Drawing.Size(75, 23);
            this.buttonExtOK.TabIndex = 10;
            this.buttonExtOK.Text = "%OK%";
            this.buttonExtOK.UseVisualStyleBackColor = true;
            this.buttonExtOK.Click += new System.EventHandler(this.buttonExtOK_Click);
            // 
            // comboBoxCustomVoice
            // 
            this.comboBoxCustomVoice.BorderColor = System.Drawing.Color.White;
            this.comboBoxCustomVoice.DataSource = null;
            this.comboBoxCustomVoice.DisableBackgroundDisabledShadingGradient = false;
            this.comboBoxCustomVoice.DisplayMember = "";
            this.comboBoxCustomVoice.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxCustomVoice.Location = new System.Drawing.Point(59, 211);
            this.comboBoxCustomVoice.Name = "comboBoxCustomVoice";
            this.comboBoxCustomVoice.SelectedIndex = -1;
            this.comboBoxCustomVoice.SelectedItem = null;
            this.comboBoxCustomVoice.SelectedValue = null;
            this.comboBoxCustomVoice.Size = new System.Drawing.Size(356, 21);
            this.comboBoxCustomVoice.TabIndex = 3;
            this.comboBoxCustomVoice.Text = "comboBoxCustom1";
            this.comboBoxCustomVoice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.comboBoxCustomVoice.ValueMember = "";
            // 
            // checkBoxCustomLiteral
            // 
            this.checkBoxCustomLiteral.AutoSize = true;
            this.checkBoxCustomLiteral.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxCustomLiteral.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxCustomLiteral.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxCustomLiteral.ImageIndeterminate = null;
            this.checkBoxCustomLiteral.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxCustomLiteral.ImageUnchecked = null;
            this.checkBoxCustomLiteral.Location = new System.Drawing.Point(197, 132);
            this.checkBoxCustomLiteral.Name = "checkBoxCustomLiteral";
            this.checkBoxCustomLiteral.Size = new System.Drawing.Size(54, 17);
            this.checkBoxCustomLiteral.TabIndex = 1;
            this.checkBoxCustomLiteral.Text = "Literal";
            this.checkBoxCustomLiteral.TickBoxReductionRatio = 0.75F;
            this.checkBoxCustomLiteral.UseVisualStyleBackColor = true;
            // 
            // textBoxBorderTest
            // 
            this.textBoxBorderTest.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.textBoxBorderTest.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.textBoxBorderTest.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxBorderTest.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxBorderTest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxBorderTest.ClearOnFirstChar = false;
            this.textBoxBorderTest.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxBorderTest.EndButtonEnable = true;
            this.textBoxBorderTest.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("textBoxBorderTest.EndButtonImage")));
            this.textBoxBorderTest.EndButtonSize16ths = 10;
            this.textBoxBorderTest.EndButtonVisible = false;
            this.textBoxBorderTest.InErrorCondition = false;
            this.textBoxBorderTest.Location = new System.Drawing.Point(11, 368);
            this.textBoxBorderTest.Multiline = true;
            this.textBoxBorderTest.Name = "textBoxBorderTest";
            this.textBoxBorderTest.ReadOnly = false;
            this.textBoxBorderTest.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxBorderTest.SelectionLength = 0;
            this.textBoxBorderTest.SelectionStart = 0;
            this.textBoxBorderTest.Size = new System.Drawing.Size(313, 38);
            this.textBoxBorderTest.TabIndex = 7;
            this.textBoxBorderTest.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxBorderTest.TextNoChange = "";
            this.textBoxBorderTest.WordWrap = true;
            // 
            // labelEndTrigger
            // 
            this.labelEndTrigger.AutoSize = true;
            this.labelEndTrigger.Location = new System.Drawing.Point(223, 168);
            this.labelEndTrigger.Name = "labelEndTrigger";
            this.labelEndTrigger.Size = new System.Drawing.Size(62, 13);
            this.labelEndTrigger.TabIndex = 0;
            this.labelEndTrigger.Text = "End Trigger";
            // 
            // labelStartTrigger
            // 
            this.labelStartTrigger.AutoSize = true;
            this.labelStartTrigger.Location = new System.Drawing.Point(12, 168);
            this.labelStartTrigger.Name = "labelStartTrigger";
            this.labelStartTrigger.Size = new System.Drawing.Size(65, 13);
            this.labelStartTrigger.TabIndex = 0;
            this.labelStartTrigger.Text = "Start Trigger";
            // 
            // labelVoice
            // 
            this.labelVoice.AutoSize = true;
            this.labelVoice.Location = new System.Drawing.Point(11, 214);
            this.labelVoice.Name = "labelVoice";
            this.labelVoice.Size = new System.Drawing.Size(34, 13);
            this.labelVoice.TabIndex = 0;
            this.labelVoice.Text = "Voice";
            // 
            // trackBarRate
            // 
            this.trackBarRate.LargeChange = 2;
            this.trackBarRate.Location = new System.Drawing.Point(59, 306);
            this.trackBarRate.Minimum = -10;
            this.trackBarRate.Name = "trackBarRate";
            this.trackBarRate.Size = new System.Drawing.Size(203, 45);
            this.trackBarRate.TabIndex = 6;
            this.trackBarRate.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // checkBoxCustomR
            // 
            this.checkBoxCustomR.AutoSize = true;
            this.checkBoxCustomR.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxCustomR.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxCustomR.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxCustomR.ImageIndeterminate = null;
            this.checkBoxCustomR.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxCustomR.ImageUnchecked = null;
            this.checkBoxCustomR.Location = new System.Drawing.Point(271, 319);
            this.checkBoxCustomR.Name = "checkBoxCustomR";
            this.checkBoxCustomR.Size = new System.Drawing.Size(66, 17);
            this.checkBoxCustomR.TabIndex = 8;
            this.checkBoxCustomR.Text = "Override";
            this.checkBoxCustomR.TickBoxReductionRatio = 0.75F;
            this.checkBoxCustomR.UseVisualStyleBackColor = true;
            this.checkBoxCustomR.CheckedChanged += new System.EventHandler(this.checkBoxCustomR_CheckedChanged);
            // 
            // checkBoxCustomV
            // 
            this.checkBoxCustomV.AutoSize = true;
            this.checkBoxCustomV.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxCustomV.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxCustomV.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxCustomV.ImageIndeterminate = null;
            this.checkBoxCustomV.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxCustomV.ImageUnchecked = null;
            this.checkBoxCustomV.Location = new System.Drawing.Point(271, 261);
            this.checkBoxCustomV.Name = "checkBoxCustomV";
            this.checkBoxCustomV.Size = new System.Drawing.Size(66, 17);
            this.checkBoxCustomV.TabIndex = 8;
            this.checkBoxCustomV.Text = "Override";
            this.checkBoxCustomV.TickBoxReductionRatio = 0.75F;
            this.checkBoxCustomV.UseVisualStyleBackColor = true;
            this.checkBoxCustomV.CheckedChanged += new System.EventHandler(this.checkBoxCustomV_CheckedChanged);
            // 
            // SpeechConfigure
            // 
            this.AcceptButton = this.buttonExtOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonExtCancel;
            this.ClientSize = new System.Drawing.Size(433, 475);
            this.Controls.Add(this.panelOuter);
            this.Name = "SpeechConfigure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure Speech";
            this.panelOuter.ResumeLayout(false);
            this.panelOuter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label labelVolume;
        private System.Windows.Forms.Label labelRate;
        private ExtendedControls.ExtTextBox textBoxBorderText;
        private ExtendedControls.ExtCheckBox checkBoxCustomComplete;
        private System.Windows.Forms.Panel panelOuter;
        private ExtendedControls.ExtComboBox comboBoxCustomVoice;
        private System.Windows.Forms.Label labelVoice;
        private ExtendedControls.ExtButton buttonExtCancel;
        private ExtendedControls.ExtButton buttonExtOK;
        private System.Windows.Forms.TrackBar trackBarVolume;
        private System.Windows.Forms.TrackBar trackBarRate;
        private ExtendedControls.ExtButton buttonExtEffects;
        private ExtendedControls.ExtCheckBox checkBoxCustomR;
        private ExtendedControls.ExtCheckBox checkBoxCustomV;
        private ExtendedControls.ExtButton buttonExtTest;
        private ExtendedControls.ExtTextBox textBoxBorderTest;
        private ExtendedControls.ExtComboBox comboBoxCustomPriority;
        private ExtendedControls.ExtTextBox textBoxBorderEndTrigger;
        private ExtendedControls.ExtTextBox textBoxBorderStartTrigger;
        private System.Windows.Forms.Label labelEndTrigger;
        private System.Windows.Forms.Label labelStartTrigger;
        private ExtendedControls.ExtButton buttonExtDevice;
        private ExtendedControls.ExtCheckBox checkBoxCustomLiteral;
    }
}