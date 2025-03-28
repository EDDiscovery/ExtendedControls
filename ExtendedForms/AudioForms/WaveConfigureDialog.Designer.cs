/*
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
    partial class WaveConfigureDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaveConfigureDialog));
            this.textBoxBorderText = new ExtendedControls.ExtTextBox();
            this.buttonExtBrowse = new ExtendedControls.ExtButton();
            this.trackBarVolume = new System.Windows.Forms.TrackBar();
            this.labelVolume = new System.Windows.Forms.Label();
            this.checkBoxCustomV = new ExtendedControls.ExtCheckBox();
            this.checkBoxCustomComplete = new ExtendedControls.ExtCheckBox();
            this.buttonExtTest = new ExtendedControls.ExtButton();
            this.buttonExtOK = new ExtendedControls.ExtButton();
            this.buttonExtCancel = new ExtendedControls.ExtButton();
            this.buttonExtEffects = new ExtendedControls.ExtButton();
            this.panelOuter = new System.Windows.Forms.Panel();
            this.buttonExtDevice = new ExtendedControls.ExtButton();
            this.textBoxBorderStartTrigger = new ExtendedControls.ExtTextBox();
            this.labelStartTrigger = new System.Windows.Forms.Label();
            this.textBoxBorderEndTrigger = new ExtendedControls.ExtTextBox();
            this.comboBoxCustomPriority = new ExtendedControls.ExtComboBox();
            this.labelEndTrigger = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
            this.panelOuter.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxBorderText
            // 
            this.textBoxBorderText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.textBoxBorderText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.textBoxBorderText.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxBorderText.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxBorderText.BorderColorScaling = 0.5F;
            this.textBoxBorderText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxBorderText.ClearOnFirstChar = false;
            this.textBoxBorderText.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxBorderText.EndButtonEnable = true;
            this.textBoxBorderText.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("textBoxBorderText.EndButtonImage")));
            this.textBoxBorderText.EndButtonSize16ths = 10;
            this.textBoxBorderText.EndButtonVisible = false;
            this.textBoxBorderText.InErrorCondition = false;
            this.textBoxBorderText.Location = new System.Drawing.Point(12, 37);
            this.textBoxBorderText.Multiline = true;
            this.textBoxBorderText.Name = "textBoxBorderText";
            this.textBoxBorderText.ReadOnly = false;
            this.textBoxBorderText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxBorderText.SelectionLength = 0;
            this.textBoxBorderText.SelectionStart = 0;
            this.textBoxBorderText.Size = new System.Drawing.Size(364, 24);
            this.textBoxBorderText.TabIndex = 0;
            this.textBoxBorderText.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxBorderText.TextNoChange = "";
            this.textBoxBorderText.WordWrap = false;
            // 
            // buttonExtBrowse
            // 
            this.buttonExtBrowse.Location = new System.Drawing.Point(382, 21);
            this.buttonExtBrowse.Name = "buttonExtBrowse";
            this.buttonExtBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonExtBrowse.TabIndex = 1;
            this.buttonExtBrowse.Text = "Browse";
            this.buttonExtBrowse.UseVisualStyleBackColor = true;
            this.buttonExtBrowse.Click += new System.EventHandler(this.buttonExtBrowse_Click);
            // 
            // trackBarVolume
            // 
            this.trackBarVolume.Location = new System.Drawing.Point(57, 147);
            this.trackBarVolume.Maximum = 100;
            this.trackBarVolume.Name = "trackBarVolume";
            this.trackBarVolume.Size = new System.Drawing.Size(203, 45);
            this.trackBarVolume.TabIndex = 4;
            this.trackBarVolume.TickFrequency = 5;
            this.trackBarVolume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarVolume.Value = 60;
            // 
            // labelVolume
            // 
            this.labelVolume.AutoSize = true;
            this.labelVolume.Location = new System.Drawing.Point(9, 163);
            this.labelVolume.Name = "labelVolume";
            this.labelVolume.Size = new System.Drawing.Size(42, 13);
            this.labelVolume.TabIndex = 43;
            this.labelVolume.Text = "Volume";
            // 
            // checkBoxCustomV
            // 
            this.checkBoxCustomV.AutoSize = true;
            this.checkBoxCustomV.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxCustomV.CheckBoxDisabledScaling = 0.5F;
            this.checkBoxCustomV.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxCustomV.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxCustomV.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxCustomV.ImageIndeterminate = null;
            this.checkBoxCustomV.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxCustomV.ImageUnchecked = null;
            this.checkBoxCustomV.Location = new System.Drawing.Point(276, 159);
            this.checkBoxCustomV.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxCustomV.Name = "checkBoxCustomV";
            this.checkBoxCustomV.Size = new System.Drawing.Size(66, 17);
            this.checkBoxCustomV.TabIndex = 5;
            this.checkBoxCustomV.Text = "Override";
            this.checkBoxCustomV.TickBoxReductionRatio = 0.75F;
            this.checkBoxCustomV.UseVisualStyleBackColor = true;
            this.checkBoxCustomV.CheckedChanged += new System.EventHandler(this.checkBoxCustomV_CheckedChanged);
            // 
            // checkBoxCustomComplete
            // 
            this.checkBoxCustomComplete.AutoSize = true;
            this.checkBoxCustomComplete.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxCustomComplete.CheckBoxDisabledScaling = 0.5F;
            this.checkBoxCustomComplete.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxCustomComplete.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxCustomComplete.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxCustomComplete.ImageIndeterminate = null;
            this.checkBoxCustomComplete.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxCustomComplete.ImageUnchecked = null;
            this.checkBoxCustomComplete.Location = new System.Drawing.Point(12, 82);
            this.checkBoxCustomComplete.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxCustomComplete.Name = "checkBoxCustomComplete";
            this.checkBoxCustomComplete.Size = new System.Drawing.Size(150, 17);
            this.checkBoxCustomComplete.TabIndex = 2;
            this.checkBoxCustomComplete.Text = "Wait until audio completes";
            this.checkBoxCustomComplete.TickBoxReductionRatio = 0.75F;
            this.checkBoxCustomComplete.UseVisualStyleBackColor = true;
            // 
            // buttonExtTest
            // 
            this.buttonExtTest.Location = new System.Drawing.Point(382, 50);
            this.buttonExtTest.Name = "buttonExtTest";
            this.buttonExtTest.Size = new System.Drawing.Size(75, 23);
            this.buttonExtTest.TabIndex = 7;
            this.buttonExtTest.Text = "Test";
            this.buttonExtTest.UseVisualStyleBackColor = true;
            this.buttonExtTest.Click += new System.EventHandler(this.buttonExtTest_Click);
            // 
            // buttonExtOK
            // 
            this.buttonExtOK.Location = new System.Drawing.Point(381, 198);
            this.buttonExtOK.Name = "buttonExtOK";
            this.buttonExtOK.Size = new System.Drawing.Size(75, 23);
            this.buttonExtOK.TabIndex = 8;
            this.buttonExtOK.Text = "%OK%";
            this.buttonExtOK.UseVisualStyleBackColor = true;
            this.buttonExtOK.Click += new System.EventHandler(this.buttonExtOK_Click);
            // 
            // buttonExtCancel
            // 
            this.buttonExtCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExtCancel.Location = new System.Drawing.Point(275, 198);
            this.buttonExtCancel.Name = "buttonExtCancel";
            this.buttonExtCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonExtCancel.TabIndex = 9;
            this.buttonExtCancel.Text = "%Cancel%";
            this.buttonExtCancel.UseVisualStyleBackColor = true;
            // 
            // buttonExtEffects
            // 
            this.buttonExtEffects.Location = new System.Drawing.Point(382, 154);
            this.buttonExtEffects.Name = "buttonExtEffects";
            this.buttonExtEffects.Size = new System.Drawing.Size(75, 23);
            this.buttonExtEffects.TabIndex = 6;
            this.buttonExtEffects.Text = "Effects";
            this.buttonExtEffects.UseVisualStyleBackColor = true;
            this.buttonExtEffects.Click += new System.EventHandler(this.buttonExtEffects_Click);
            // 
            // panelOuter
            // 
            this.panelOuter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOuter.Controls.Add(this.buttonExtDevice);
            this.panelOuter.Controls.Add(this.textBoxBorderStartTrigger);
            this.panelOuter.Controls.Add(this.labelStartTrigger);
            this.panelOuter.Controls.Add(this.textBoxBorderEndTrigger);
            this.panelOuter.Controls.Add(this.comboBoxCustomPriority);
            this.panelOuter.Controls.Add(this.labelEndTrigger);
            this.panelOuter.Controls.Add(this.labelTitle);
            this.panelOuter.Controls.Add(this.trackBarVolume);
            this.panelOuter.Controls.Add(this.textBoxBorderText);
            this.panelOuter.Controls.Add(this.buttonExtBrowse);
            this.panelOuter.Controls.Add(this.buttonExtOK);
            this.panelOuter.Controls.Add(this.buttonExtCancel);
            this.panelOuter.Controls.Add(this.checkBoxCustomComplete);
            this.panelOuter.Controls.Add(this.buttonExtEffects);
            this.panelOuter.Controls.Add(this.buttonExtTest);
            this.panelOuter.Controls.Add(this.checkBoxCustomV);
            this.panelOuter.Controls.Add(this.labelVolume);
            this.panelOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOuter.Location = new System.Drawing.Point(0, 0);
            this.panelOuter.Name = "panelOuter";
            this.panelOuter.Size = new System.Drawing.Size(475, 247);
            this.panelOuter.TabIndex = 50;
            this.panelOuter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CapMouseDown);
            this.panelOuter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CapMouseUp);
            // 
            // buttonExtDevice
            // 
            this.buttonExtDevice.Location = new System.Drawing.Point(381, 82);
            this.buttonExtDevice.Name = "buttonExtDevice";
            this.buttonExtDevice.Size = new System.Drawing.Size(75, 23);
            this.buttonExtDevice.TabIndex = 50;
            this.buttonExtDevice.Text = "Device";
            this.buttonExtDevice.UseVisualStyleBackColor = true;
            this.buttonExtDevice.Click += new System.EventHandler(this.buttonExtDevice_Click);
            // 
            // textBoxBorderStartTrigger
            // 
            this.textBoxBorderStartTrigger.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.textBoxBorderStartTrigger.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.textBoxBorderStartTrigger.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxBorderStartTrigger.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxBorderStartTrigger.BorderColorScaling = 0.5F;
            this.textBoxBorderStartTrigger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxBorderStartTrigger.ClearOnFirstChar = false;
            this.textBoxBorderStartTrigger.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxBorderStartTrigger.EndButtonEnable = true;
            this.textBoxBorderStartTrigger.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("textBoxBorderStartTrigger.EndButtonImage")));
            this.textBoxBorderStartTrigger.EndButtonSize16ths = 10;
            this.textBoxBorderStartTrigger.EndButtonVisible = false;
            this.textBoxBorderStartTrigger.InErrorCondition = false;
            this.textBoxBorderStartTrigger.Location = new System.Drawing.Point(116, 116);
            this.textBoxBorderStartTrigger.Multiline = false;
            this.textBoxBorderStartTrigger.Name = "textBoxBorderStartTrigger";
            this.textBoxBorderStartTrigger.ReadOnly = false;
            this.textBoxBorderStartTrigger.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxBorderStartTrigger.SelectionLength = 0;
            this.textBoxBorderStartTrigger.SelectionStart = 0;
            this.textBoxBorderStartTrigger.Size = new System.Drawing.Size(100, 20);
            this.textBoxBorderStartTrigger.TabIndex = 49;
            this.textBoxBorderStartTrigger.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxBorderStartTrigger.TextNoChange = "";
            this.textBoxBorderStartTrigger.WordWrap = true;
            // 
            // labelStartTrigger
            // 
            this.labelStartTrigger.AutoSize = true;
            this.labelStartTrigger.Location = new System.Drawing.Point(12, 119);
            this.labelStartTrigger.Name = "labelStartTrigger";
            this.labelStartTrigger.Size = new System.Drawing.Size(65, 13);
            this.labelStartTrigger.TabIndex = 48;
            this.labelStartTrigger.Text = "Start Trigger";
            // 
            // textBoxBorderEndTrigger
            // 
            this.textBoxBorderEndTrigger.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.textBoxBorderEndTrigger.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.textBoxBorderEndTrigger.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxBorderEndTrigger.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxBorderEndTrigger.BorderColorScaling = 0.5F;
            this.textBoxBorderEndTrigger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxBorderEndTrigger.ClearOnFirstChar = false;
            this.textBoxBorderEndTrigger.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxBorderEndTrigger.EndButtonEnable = true;
            this.textBoxBorderEndTrigger.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("textBoxBorderEndTrigger.EndButtonImage")));
            this.textBoxBorderEndTrigger.EndButtonSize16ths = 10;
            this.textBoxBorderEndTrigger.EndButtonVisible = false;
            this.textBoxBorderEndTrigger.InErrorCondition = false;
            this.textBoxBorderEndTrigger.Location = new System.Drawing.Point(357, 116);
            this.textBoxBorderEndTrigger.Multiline = false;
            this.textBoxBorderEndTrigger.Name = "textBoxBorderEndTrigger";
            this.textBoxBorderEndTrigger.ReadOnly = false;
            this.textBoxBorderEndTrigger.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxBorderEndTrigger.SelectionLength = 0;
            this.textBoxBorderEndTrigger.SelectionStart = 0;
            this.textBoxBorderEndTrigger.Size = new System.Drawing.Size(100, 20);
            this.textBoxBorderEndTrigger.TabIndex = 47;
            this.textBoxBorderEndTrigger.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxBorderEndTrigger.TextNoChange = "";
            this.textBoxBorderEndTrigger.WordWrap = true;
            // 
            // comboBoxCustomPriority
            // 
            this.comboBoxCustomPriority.BorderColor = System.Drawing.Color.White;
            this.comboBoxCustomPriority.ButtonColorScaling = 0.5F;
            this.comboBoxCustomPriority.DataSource = null;
            this.comboBoxCustomPriority.DisableBackgroundDisabledShadingGradient = false;
            this.comboBoxCustomPriority.DisplayMember = "";
            this.comboBoxCustomPriority.DropDownSelectionBackgroundColor = System.Drawing.Color.Gray;
            this.comboBoxCustomPriority.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxCustomPriority.Location = new System.Drawing.Point(226, 79);
            this.comboBoxCustomPriority.MouseOverBackgroundColor = System.Drawing.Color.Silver;
            this.comboBoxCustomPriority.Name = "comboBoxCustomPriority";
            this.comboBoxCustomPriority.SelectedIndex = -1;
            this.comboBoxCustomPriority.SelectedItem = null;
            this.comboBoxCustomPriority.SelectedValue = null;
            this.comboBoxCustomPriority.Size = new System.Drawing.Size(110, 21);
            this.comboBoxCustomPriority.TabIndex = 46;
            this.comboBoxCustomPriority.Text = "comboBoxCustom1";
            this.comboBoxCustomPriority.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.comboBoxCustomPriority.ValueMember = "";
            // 
            // labelEndTrigger
            // 
            this.labelEndTrigger.AutoSize = true;
            this.labelEndTrigger.Location = new System.Drawing.Point(246, 119);
            this.labelEndTrigger.Name = "labelEndTrigger";
            this.labelEndTrigger.Size = new System.Drawing.Size(62, 13);
            this.labelEndTrigger.TabIndex = 45;
            this.labelEndTrigger.Text = "End Trigger";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(12, 10);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(205, 13);
            this.labelTitle.TabIndex = 44;
            this.labelTitle.Text = "Select Default device, volume and effects";
            // 
            // WaveConfigureDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonExtCancel;
            this.ClientSize = new System.Drawing.Size(475, 247);
            this.Controls.Add(this.panelOuter);
            this.Name = "WaveConfigureDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure Wave";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
            this.panelOuter.ResumeLayout(false);
            this.panelOuter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.ExtTextBox textBoxBorderText;
        private ExtendedControls.ExtButton buttonExtBrowse;
        private System.Windows.Forms.TrackBar trackBarVolume;
        private System.Windows.Forms.Label labelVolume;
        private ExtendedControls.ExtCheckBox checkBoxCustomV;
        private ExtendedControls.ExtCheckBox checkBoxCustomComplete;
        private ExtendedControls.ExtButton buttonExtTest;
        private ExtendedControls.ExtButton buttonExtOK;
        private ExtendedControls.ExtButton buttonExtCancel;
        private ExtendedControls.ExtButton buttonExtEffects;
        private System.Windows.Forms.Panel panelOuter;
        private System.Windows.Forms.Label labelTitle;
        private ExtendedControls.ExtTextBox textBoxBorderEndTrigger;
        private ExtendedControls.ExtComboBox comboBoxCustomPriority;
        private System.Windows.Forms.Label labelEndTrigger;
        private ExtendedControls.ExtTextBox textBoxBorderStartTrigger;
        private System.Windows.Forms.Label labelStartTrigger;
        private ExtendedControls.ExtButton buttonExtDevice;
    }
}