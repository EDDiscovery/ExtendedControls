namespace DialogTest
{
    partial class TestSelectionPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestSelectionPanel));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBoxCustom1 = new ExtendedControls.ExtCheckBox();
            this.buttonExt1 = new ExtendedControls.ExtButton();
            this.comboBoxCustom1 = new ExtendedControls.ExtComboBox();
            this.autoCompleteTextBox1 = new ExtendedControls.ExtTextBoxAutoComplete();
            this.panelSelectionList1 = new ExtendedControls.ExtPanelDropDown();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.SuspendLayout();
            // 
            // checkBoxCustom1
            // 
            this.checkBoxCustom1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxCustom1.AutoSize = true;
            this.checkBoxCustom1.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxCustom1.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxCustom1.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxCustom1.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxCustom1.Location = new System.Drawing.Point(21, 280);
            this.checkBoxCustom1.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxCustom1.Name = "checkBoxCustom1";
            this.checkBoxCustom1.Size = new System.Drawing.Size(106, 23);
            this.checkBoxCustom1.TabIndex = 4;
            this.checkBoxCustom1.Text = "checkBoxCustom1";
            this.checkBoxCustom1.TickBoxReductionRatio = 0.75F;
            this.checkBoxCustom1.UseVisualStyleBackColor = true;
            // 
            // buttonExt1
            // 
            this.buttonExt1.Location = new System.Drawing.Point(21, 12);
            this.buttonExt1.Name = "buttonExt1";
            this.buttonExt1.Size = new System.Drawing.Size(75, 23);
            this.buttonExt1.TabIndex = 3;
            this.buttonExt1.Text = "T12";
            this.buttonExt1.UseVisualStyleBackColor = true;
            this.buttonExt1.Click += new System.EventHandler(this.buttonExt1_Click);
            // 
            // comboBoxCustom1
            // 
            this.comboBoxCustom1.BorderColor = System.Drawing.Color.White;
            this.comboBoxCustom1.ButtonColorScaling = 0.5F;
            this.comboBoxCustom1.DataSource = null;
            this.comboBoxCustom1.DisableBackgroundDisabledShadingGradient = false;
            this.comboBoxCustom1.DisplayMember = "";
            this.comboBoxCustom1.DropDownBackgroundColor = System.Drawing.Color.Gray;
            this.comboBoxCustom1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxCustom1.Location = new System.Drawing.Point(21, 234);
            this.comboBoxCustom1.MouseOverBackgroundColor = System.Drawing.Color.Silver;
            this.comboBoxCustom1.Name = "comboBoxCustom1";
            this.comboBoxCustom1.ScrollBarButtonColor = System.Drawing.Color.LightGray;
            this.comboBoxCustom1.ScrollBarColor = System.Drawing.Color.LightGray;
            this.comboBoxCustom1.SelectedIndex = -1;
            this.comboBoxCustom1.SelectedItem = null;
            this.comboBoxCustom1.SelectedValue = null;
            this.comboBoxCustom1.Size = new System.Drawing.Size(281, 21);
            this.comboBoxCustom1.TabIndex = 2;
            this.comboBoxCustom1.Text = "comboBoxCustom1";
            this.comboBoxCustom1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.comboBoxCustom1.ValueMember = "";
            // 
            // autoCompleteTextBox1
            // 
            this.autoCompleteTextBox1.AutoCompleteCommentMarker = null;
            this.autoCompleteTextBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.autoCompleteTextBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.autoCompleteTextBox1.BackErrorColor = System.Drawing.Color.Red;
            this.autoCompleteTextBox1.BorderColor = System.Drawing.Color.Transparent;
            this.autoCompleteTextBox1.BorderColorScaling = 0.5F;
            this.autoCompleteTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoCompleteTextBox1.ClearOnFirstChar = false;
            this.autoCompleteTextBox1.ControlBackground = System.Drawing.SystemColors.Control;
            this.autoCompleteTextBox1.DropDownBackgroundColor = System.Drawing.Color.Gray;
            this.autoCompleteTextBox1.DropDownBorderColor = System.Drawing.Color.Green;
            this.autoCompleteTextBox1.DropDownMouseOverBackgroundColor = System.Drawing.Color.Red;
            this.autoCompleteTextBox1.DropDownScrollBarButtonColor = System.Drawing.Color.LightGray;
            this.autoCompleteTextBox1.DropDownScrollBarColor = System.Drawing.Color.LightGray;
            this.autoCompleteTextBox1.EndButtonEnable = false;
            this.autoCompleteTextBox1.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("autoCompleteTextBox1.EndButtonImage")));
            this.autoCompleteTextBox1.EndButtonVisible = false;
            this.autoCompleteTextBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.autoCompleteTextBox1.InErrorCondition = false;
            this.autoCompleteTextBox1.Location = new System.Drawing.Point(21, 172);
            this.autoCompleteTextBox1.Multiline = false;
            this.autoCompleteTextBox1.Name = "autoCompleteTextBox1";
            this.autoCompleteTextBox1.ReadOnly = false;
            this.autoCompleteTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.autoCompleteTextBox1.SelectionLength = 0;
            this.autoCompleteTextBox1.SelectionStart = 0;
            this.autoCompleteTextBox1.Size = new System.Drawing.Size(210, 20);
            this.autoCompleteTextBox1.TabIndex = 1;
            this.autoCompleteTextBox1.Text = "autoCompleteTextBox1";
            this.autoCompleteTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoCompleteTextBox1.WordWrap = true;
            // 
            // panelSelectionList1
            // 
            this.panelSelectionList1.BorderColor = System.Drawing.Color.DarkRed;
            this.panelSelectionList1.FitToItemsHeight = true;
            this.panelSelectionList1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.panelSelectionList1.GradientColorScaling = 0.5F;
            this.panelSelectionList1.Items = ((System.Collections.Generic.List<string>)(resources.GetObject("panelSelectionList1.Items")));
            this.panelSelectionList1.Location = new System.Drawing.Point(21, 64);
            this.panelSelectionList1.MouseOverBackgroundColor = System.Drawing.Color.Silver;
            this.panelSelectionList1.Name = "panelSelectionList1";
            this.panelSelectionList1.ScrollBarButtonColor = System.Drawing.Color.LightGray;
            this.panelSelectionList1.ScrollBarColor = System.Drawing.Color.LightGray;
            this.panelSelectionList1.SelectionBackColor = System.Drawing.Color.Gray;
            this.panelSelectionList1.SelectionMarkColor = System.Drawing.Color.Blue;
            this.panelSelectionList1.SelectionSize = 8;
            this.panelSelectionList1.Size = new System.Drawing.Size(281, 78);
            this.panelSelectionList1.TabIndex = 0;
            // 
            // extButton1
            // 
            this.extButton1.Location = new System.Drawing.Point(182, 12);
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(75, 23);
            this.extButton1.TabIndex = 3;
            this.extButton1.Text = "T20";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click);
            // 
            // TestSelectionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 334);
            this.Controls.Add(this.checkBoxCustom1);
            this.Controls.Add(this.extButton1);
            this.Controls.Add(this.buttonExt1);
            this.Controls.Add(this.comboBoxCustom1);
            this.Controls.Add(this.autoCompleteTextBox1);
            this.Controls.Add(this.panelSelectionList1);
            this.Name = "TestSelectionPanel";
            this.Text = "TestSelectionPanel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private ExtendedControls.ExtPanelDropDown panelSelectionList1;
        private ExtendedControls.ExtTextBoxAutoComplete autoCompleteTextBox1;
        private ExtendedControls.ExtComboBox comboBoxCustom1;
        private ExtendedControls.ExtButton buttonExt1;
        private ExtendedControls.ExtCheckBox checkBoxCustom1;
        private ExtendedControls.ExtButton extButton1;
    }
}