namespace TestExtendedControls
{
    partial class TestAutoComplete
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestAutoComplete));
            this.autoCompleteTextBox1 = new ExtendedControls.ExtTextBoxAutoComplete();
            this.comboBoxCustom1 = new ExtendedControls.ExtComboBox();
            this.autoCompleteTextBox2 = new ExtendedControls.ExtTextBoxAutoComplete();
            this.textBoxBorder1 = new ExtendedControls.ExtTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataGridViewColumnHider1 = new BaseUtils.DataGridViewColumnControl();
            this.Column1 = new ExtendedControls.ExtDataGridViewColumnAutoComplete();
            this.Column2 = new ExtendedControls.ExtDataGridViewColumnAutoComplete();
            this.extTextBox1 = new ExtendedControls.ExtTextBox();
            this.extTextBoxAutoComplete1 = new ExtendedControls.ExtTextBoxAutoComplete();
            this.extButton1 = new ExtendedControls.ExtButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumnHider1)).BeginInit();
            this.SuspendLayout();
            // 
            // autoCompleteTextBox1
            // 
            this.autoCompleteTextBox1.AutoCompleteCommentMarker = null;
            this.autoCompleteTextBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.autoCompleteTextBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.autoCompleteTextBox1.AutoCompleteTimeout = 500;
            this.autoCompleteTextBox1.BackErrorColor = System.Drawing.Color.Red;
            this.autoCompleteTextBox1.BorderColor = System.Drawing.Color.Transparent;
            this.autoCompleteTextBox1.BorderColorScaling = 0.5F;
            this.autoCompleteTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoCompleteTextBox1.ClearOnFirstChar = false;
            this.autoCompleteTextBox1.ControlBackground = System.Drawing.SystemColors.Control;
            this.autoCompleteTextBox1.DropDownBorderColor = System.Drawing.Color.Green;
            this.autoCompleteTextBox1.EndButtonEnable = false;
            this.autoCompleteTextBox1.EndButtonSize16ths = 10;
            this.autoCompleteTextBox1.EndButtonVisible = false;
            this.autoCompleteTextBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.autoCompleteTextBox1.InErrorCondition = false;
            this.autoCompleteTextBox1.Location = new System.Drawing.Point(40, 37);
            this.autoCompleteTextBox1.Multiline = false;
            this.autoCompleteTextBox1.Name = "autoCompleteTextBox1";
            this.autoCompleteTextBox1.ReadOnly = false;
            this.autoCompleteTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.autoCompleteTextBox1.SelectionLength = 0;
            this.autoCompleteTextBox1.SelectionStart = 0;
            this.autoCompleteTextBox1.Size = new System.Drawing.Size(316, 20);
            this.autoCompleteTextBox1.TabIndex = 0;
            this.autoCompleteTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoCompleteTextBox1.TextChangedEvent = "";
            this.autoCompleteTextBox1.TextNoChange = "";
            this.autoCompleteTextBox1.WordWrap = true;
            // 
            // comboBoxCustom1
            // 
            this.comboBoxCustom1.BorderColor = System.Drawing.Color.White;
            this.comboBoxCustom1.ButtonColorScaling = 0.5F;
            this.comboBoxCustom1.DataSource = null;
            this.comboBoxCustom1.DisableBackgroundDisabledShadingGradient = false;
            this.comboBoxCustom1.DisplayMember = "";
            this.comboBoxCustom1.DropDownSelectionBackgroundColor = System.Drawing.Color.Gray;
            this.comboBoxCustom1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxCustom1.Location = new System.Drawing.Point(40, 168);
            this.comboBoxCustom1.MouseOverBackgroundColor = System.Drawing.Color.Silver;
            this.comboBoxCustom1.Name = "comboBoxCustom1";
            this.comboBoxCustom1.SelectedIndex = -1;
            this.comboBoxCustom1.SelectedItem = null;
            this.comboBoxCustom1.SelectedValue = null;
            this.comboBoxCustom1.Size = new System.Drawing.Size(355, 21);
            this.comboBoxCustom1.TabIndex = 1;
            this.comboBoxCustom1.Text = "comboBoxCustom1";
            this.comboBoxCustom1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.comboBoxCustom1.ValueMember = "";
            // 
            // autoCompleteTextBox2
            // 
            this.autoCompleteTextBox2.AutoCompleteCommentMarker = null;
            this.autoCompleteTextBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.autoCompleteTextBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.autoCompleteTextBox2.AutoCompleteTimeout = 500;
            this.autoCompleteTextBox2.BackErrorColor = System.Drawing.Color.Red;
            this.autoCompleteTextBox2.BorderColor = System.Drawing.Color.Transparent;
            this.autoCompleteTextBox2.BorderColorScaling = 0.5F;
            this.autoCompleteTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoCompleteTextBox2.ClearOnFirstChar = false;
            this.autoCompleteTextBox2.ControlBackground = System.Drawing.SystemColors.Control;
            this.autoCompleteTextBox2.DropDownBorderColor = System.Drawing.Color.Green;
            this.autoCompleteTextBox2.EndButtonEnable = false;
            this.autoCompleteTextBox2.EndButtonSize16ths = 10;
            this.autoCompleteTextBox2.EndButtonVisible = true;
            this.autoCompleteTextBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.autoCompleteTextBox2.InErrorCondition = false;
            this.autoCompleteTextBox2.Location = new System.Drawing.Point(40, 87);
            this.autoCompleteTextBox2.Multiline = false;
            this.autoCompleteTextBox2.Name = "autoCompleteTextBox2";
            this.autoCompleteTextBox2.ReadOnly = false;
            this.autoCompleteTextBox2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.autoCompleteTextBox2.SelectionLength = 0;
            this.autoCompleteTextBox2.SelectionStart = 0;
            this.autoCompleteTextBox2.Size = new System.Drawing.Size(316, 20);
            this.autoCompleteTextBox2.TabIndex = 0;
            this.autoCompleteTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.autoCompleteTextBox2.TextChangedEvent = "";
            this.autoCompleteTextBox2.TextNoChange = "";
            this.autoCompleteTextBox2.WordWrap = true;
            // 
            // textBoxBorder1
            // 
            this.textBoxBorder1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.textBoxBorder1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.textBoxBorder1.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxBorder1.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxBorder1.BorderColorScaling = 0.5F;
            this.textBoxBorder1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxBorder1.ClearOnFirstChar = false;
            this.textBoxBorder1.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxBorder1.EndButtonEnable = true;
            this.textBoxBorder1.EndButtonImage = null;
            this.textBoxBorder1.EndButtonSize16ths = 10;
            this.textBoxBorder1.EndButtonVisible = false;
            this.textBoxBorder1.InErrorCondition = false;
            this.textBoxBorder1.Location = new System.Drawing.Point(40, 222);
            this.textBoxBorder1.Multiline = false;
            this.textBoxBorder1.Name = "textBoxBorder1";
            this.textBoxBorder1.ReadOnly = false;
            this.textBoxBorder1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxBorder1.SelectionLength = 0;
            this.textBoxBorder1.SelectionStart = 0;
            this.textBoxBorder1.Size = new System.Drawing.Size(75, 20);
            this.textBoxBorder1.TabIndex = 2;
            this.textBoxBorder1.Text = "textBoxBorder1";
            this.textBoxBorder1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBoxBorder1.TextNoChange = "textBoxBorder1";
            this.textBoxBorder1.WordWrap = true;
            // 
            // dataGridViewColumnHider1
            // 
            this.dataGridViewColumnHider1.AllowRowHeaderVisibleSelection = false;
            this.dataGridViewColumnHider1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewColumnHider1.AutoSortByColumnName = false;
            this.dataGridViewColumnHider1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewColumnHider1.ColumnReorder = true;
            this.dataGridViewColumnHider1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridViewColumnHider1.Location = new System.Drawing.Point(40, 300);
            this.dataGridViewColumnHider1.Name = "dataGridViewColumnHider1";
            this.dataGridViewColumnHider1.PerColumnWordWrapControl = true;
            this.dataGridViewColumnHider1.RowHeaderMenuStrip = null;
            this.dataGridViewColumnHider1.SingleRowSelect = true;
            this.dataGridViewColumnHider1.Size = new System.Drawing.Size(638, 150);
            this.dataGridViewColumnHider1.TabIndex = 3;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // extTextBox1
            // 
            this.extTextBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.extTextBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.extTextBox1.BackErrorColor = System.Drawing.Color.Red;
            this.extTextBox1.BorderColor = System.Drawing.Color.Transparent;
            this.extTextBox1.BorderColorScaling = 0.5F;
            this.extTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.extTextBox1.ClearOnFirstChar = false;
            this.extTextBox1.ControlBackground = System.Drawing.SystemColors.Control;
            this.extTextBox1.EndButtonEnable = true;
            this.extTextBox1.EndButtonImage = ((System.Drawing.Image)(resources.GetObject("extTextBox1.EndButtonImage")));
            this.extTextBox1.EndButtonSize16ths = 10;
            this.extTextBox1.EndButtonVisible = false;
            this.extTextBox1.InErrorCondition = false;
            this.extTextBox1.Location = new System.Drawing.Point(512, 52);
            this.extTextBox1.Multiline = false;
            this.extTextBox1.Name = "extTextBox1";
            this.extTextBox1.ReadOnly = false;
            this.extTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.extTextBox1.SelectionLength = 0;
            this.extTextBox1.SelectionStart = 0;
            this.extTextBox1.Size = new System.Drawing.Size(246, 81);
            this.extTextBox1.TabIndex = 4;
            this.extTextBox1.Text = "extTextBox1";
            this.extTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.extTextBox1.TextNoChange = "extTextBox1";
            this.extTextBox1.WordWrap = true;
            // 
            // extTextBoxAutoComplete1
            // 
            this.extTextBoxAutoComplete1.AutoCompleteCommentMarker = null;
            this.extTextBoxAutoComplete1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.extTextBoxAutoComplete1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.extTextBoxAutoComplete1.AutoCompleteTimeout = 500;
            this.extTextBoxAutoComplete1.BackErrorColor = System.Drawing.Color.Red;
            this.extTextBoxAutoComplete1.BorderColor = System.Drawing.Color.Transparent;
            this.extTextBoxAutoComplete1.BorderColorScaling = 0.5F;
            this.extTextBoxAutoComplete1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.extTextBoxAutoComplete1.ClearOnFirstChar = false;
            this.extTextBoxAutoComplete1.ControlBackground = System.Drawing.SystemColors.Control;
            this.extTextBoxAutoComplete1.DropDownBorderColor = System.Drawing.Color.Green;
            this.extTextBoxAutoComplete1.EndButtonEnable = false;
            this.extTextBoxAutoComplete1.EndButtonSize16ths = 10;
            this.extTextBoxAutoComplete1.EndButtonVisible = false;
            this.extTextBoxAutoComplete1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extTextBoxAutoComplete1.InErrorCondition = false;
            this.extTextBoxAutoComplete1.Location = new System.Drawing.Point(40, 128);
            this.extTextBoxAutoComplete1.Multiline = false;
            this.extTextBoxAutoComplete1.Name = "extTextBoxAutoComplete1";
            this.extTextBoxAutoComplete1.ReadOnly = false;
            this.extTextBoxAutoComplete1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.extTextBoxAutoComplete1.SelectionLength = 0;
            this.extTextBoxAutoComplete1.SelectionStart = 0;
            this.extTextBoxAutoComplete1.Size = new System.Drawing.Size(306, 32);
            this.extTextBoxAutoComplete1.TabIndex = 5;
            this.extTextBoxAutoComplete1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.extTextBoxAutoComplete1.TextChangedEvent = "";
            this.extTextBoxAutoComplete1.TextNoChange = "";
            this.extTextBoxAutoComplete1.WordWrap = true;
            // 
            // extButton1
            // 
            this.extButton1.Location = new System.Drawing.Point(375, 37);
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(75, 23);
            this.extButton1.TabIndex = 6;
            this.extButton1.Text = "SetCLRN";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click);
            // 
            // TestAutoComplete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 509);
            this.Controls.Add(this.extButton1);
            this.Controls.Add(this.extTextBoxAutoComplete1);
            this.Controls.Add(this.extTextBox1);
            this.Controls.Add(this.dataGridViewColumnHider1);
            this.Controls.Add(this.textBoxBorder1);
            this.Controls.Add(this.comboBoxCustom1);
            this.Controls.Add(this.autoCompleteTextBox2);
            this.Controls.Add(this.autoCompleteTextBox1);
            this.Name = "TestAutoComplete";
            this.Text = "TestAutoComplete";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumnHider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.ExtTextBoxAutoComplete autoCompleteTextBox1;
        private ExtendedControls.ExtComboBox comboBoxCustom1;
        private ExtendedControls.ExtTextBoxAutoComplete autoCompleteTextBox2;
        private ExtendedControls.ExtTextBox textBoxBorder1;
        private System.Windows.Forms.Timer timer1;
        private BaseUtils.DataGridViewColumnControl dataGridViewColumnHider1;
        private ExtendedControls.ExtDataGridViewColumnAutoComplete Column1;
        private ExtendedControls.ExtDataGridViewColumnAutoComplete Column2;
        private ExtendedControls.ExtTextBox extTextBox1;
        private ExtendedControls.ExtTextBoxAutoComplete extTextBoxAutoComplete1;
        private ExtendedControls.ExtButton extButton1;
    }
}