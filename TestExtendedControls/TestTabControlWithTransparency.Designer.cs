namespace TestExtendedControls
{
    partial class TestTabControlWithTransparency
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
            ExtendedControls.TabStyleSquare tabStyleSquare1 = new ExtendedControls.TabStyleSquare();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataGridViewColumnHider1 = new BaseUtils.DataGridViewColumnControl();
            this.Column1 = new ExtendedControls.ExtDataGridViewColumnAutoComplete();
            this.Column2 = new ExtendedControls.ExtDataGridViewColumnAutoComplete();
            this.extDataGridViewColumnAutoComplete1 = new ExtendedControls.ExtDataGridViewColumnAutoComplete();
            this.extDataGridViewColumnAutoComplete2 = new ExtendedControls.ExtDataGridViewColumnAutoComplete();
            this.extTabControl1 = new ExtendedControls.ExtTabControl();
            this.extButton8 = new ExtendedControls.ExtButton();
            this.extButton10 = new ExtendedControls.ExtButton();
            this.extButton9 = new ExtendedControls.ExtButton();
            this.extButton7 = new ExtendedControls.ExtButton();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.extButton6 = new ExtendedControls.ExtButton();
            this.extButton2 = new ExtendedControls.ExtButton();
            this.extButton5 = new ExtendedControls.ExtButton();
            this.extButton4 = new ExtendedControls.ExtButton();
            this.extButton3 = new ExtendedControls.ExtButton();
            this.extButtontransparent = new ExtendedControls.ExtButton();
            this.extButtonBorder = new ExtendedControls.ExtButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumnHider1)).BeginInit();
            this.SuspendLayout();
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
            this.dataGridViewColumnHider1.Location = new System.Drawing.Point(13, 63);
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
            // extDataGridViewColumnAutoComplete1
            // 
            this.extDataGridViewColumnAutoComplete1.HeaderText = "Column1";
            this.extDataGridViewColumnAutoComplete1.Name = "extDataGridViewColumnAutoComplete1";
            this.extDataGridViewColumnAutoComplete1.Width = 298;
            // 
            // extDataGridViewColumnAutoComplete2
            // 
            this.extDataGridViewColumnAutoComplete2.HeaderText = "Column2";
            this.extDataGridViewColumnAutoComplete2.Name = "extDataGridViewColumnAutoComplete2";
            this.extDataGridViewColumnAutoComplete2.Width = 297;
            // 
            // extTabControl1
            // 
            this.extTabControl1.AllowDragReorder = false;
            this.extTabControl1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extTabControl1.Location = new System.Drawing.Point(17, 430);
            this.extTabControl1.Name = "extTabControl1";
            this.extTabControl1.SelectedIndex = 0;
            this.extTabControl1.Size = new System.Drawing.Size(763, 291);
            this.extTabControl1.TabBackgroundColor = System.Drawing.Color.Transparent;
            this.extTabControl1.TabColorScaling = 0.5F;
            this.extTabControl1.TabControlBorderBrightColor = System.Drawing.Color.LightGray;
            this.extTabControl1.TabControlBorderColor = System.Drawing.Color.DarkGray;
            this.extTabControl1.TabDisabledScaling = 0.5F;
            this.extTabControl1.TabIndex = 8;
            this.extTabControl1.TabMouseOverColor = System.Drawing.Color.White;
            this.extTabControl1.TabNotSelectedBorderColor = System.Drawing.Color.Gray;
            this.extTabControl1.TabNotSelectedColor = System.Drawing.Color.Gray;
            this.extTabControl1.TabSelectedColor = System.Drawing.Color.LightGray;
            this.extTabControl1.TabStyle = tabStyleSquare1;
            this.extTabControl1.TextNotSelectedColor = System.Drawing.SystemColors.ControlText;
            this.extTabControl1.TextSelectedColor = System.Drawing.SystemColors.ControlText;
            // 
            // extButton8
            // 
            this.extButton8.Location = new System.Drawing.Point(555, 400);
            this.extButton8.Name = "extButton8";
            this.extButton8.Size = new System.Drawing.Size(70, 23);
            this.extButton8.TabIndex = 4;
            this.extButton8.Text = "Singleline";
            this.extButton8.UseVisualStyleBackColor = true;
            this.extButton8.Click += new System.EventHandler(this.extButton8_Click);
            // 
            // extButton10
            // 
            this.extButton10.Location = new System.Drawing.Point(102, 371);
            this.extButton10.Name = "extButton10";
            this.extButton10.Size = new System.Drawing.Size(70, 23);
            this.extButton10.TabIndex = 4;
            this.extButton10.Text = "Theme2";
            this.extButton10.UseVisualStyleBackColor = true;
            this.extButton10.Click += new System.EventHandler(this.extButton10_Click);
            // 
            // extButton9
            // 
            this.extButton9.Location = new System.Drawing.Point(17, 371);
            this.extButton9.Name = "extButton9";
            this.extButton9.Size = new System.Drawing.Size(70, 23);
            this.extButton9.TabIndex = 4;
            this.extButton9.Text = "Theme1";
            this.extButton9.UseVisualStyleBackColor = true;
            this.extButton9.Click += new System.EventHandler(this.extButton9_Click);
            // 
            // extButton7
            // 
            this.extButton7.Location = new System.Drawing.Point(479, 400);
            this.extButton7.Name = "extButton7";
            this.extButton7.Size = new System.Drawing.Size(70, 23);
            this.extButton7.TabIndex = 4;
            this.extButton7.Text = "Multiline";
            this.extButton7.UseVisualStyleBackColor = true;
            this.extButton7.Click += new System.EventHandler(this.extButton7_Click);
            // 
            // extButton1
            // 
            this.extButton1.Location = new System.Drawing.Point(403, 399);
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(70, 23);
            this.extButton1.TabIndex = 4;
            this.extButton1.Text = "SizeMode";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click);
            // 
            // extButton6
            // 
            this.extButton6.Location = new System.Drawing.Point(327, 400);
            this.extButton6.Name = "extButton6";
            this.extButton6.Size = new System.Drawing.Size(70, 23);
            this.extButton6.TabIndex = 4;
            this.extButton6.Text = "Style";
            this.extButton6.UseVisualStyleBackColor = true;
            this.extButton6.Click += new System.EventHandler(this.extButton6_Click);
            // 
            // extButton2
            // 
            this.extButton2.Location = new System.Drawing.Point(251, 399);
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(70, 23);
            this.extButton2.TabIndex = 4;
            this.extButton2.Text = "Font 24pt";
            this.extButton2.UseVisualStyleBackColor = true;
            this.extButton2.Click += new System.EventHandler(this.extButton2_Click);
            // 
            // extButton5
            // 
            this.extButton5.Location = new System.Drawing.Point(181, 399);
            this.extButton5.Name = "extButton5";
            this.extButton5.Size = new System.Drawing.Size(70, 23);
            this.extButton5.TabIndex = 4;
            this.extButton5.Text = "Font 16pt";
            this.extButton5.UseVisualStyleBackColor = true;
            this.extButton5.Click += new System.EventHandler(this.extButton5_Click);
            // 
            // extButton4
            // 
            this.extButton4.Location = new System.Drawing.Point(108, 400);
            this.extButton4.Name = "extButton4";
            this.extButton4.Size = new System.Drawing.Size(64, 23);
            this.extButton4.TabIndex = 4;
            this.extButton4.Text = "Font 12pt";
            this.extButton4.UseVisualStyleBackColor = true;
            this.extButton4.Click += new System.EventHandler(this.extButton4_Click);
            // 
            // extButton3
            // 
            this.extButton3.Location = new System.Drawing.Point(16, 400);
            this.extButton3.Name = "extButton3";
            this.extButton3.Size = new System.Drawing.Size(77, 23);
            this.extButton3.TabIndex = 4;
            this.extButton3.Text = "Add Tab";
            this.extButton3.UseVisualStyleBackColor = true;
            this.extButton3.Click += new System.EventHandler(this.extButton3_Click);
            // 
            // extButtontransparent
            // 
            this.extButtontransparent.Location = new System.Drawing.Point(125, 12);
            this.extButtontransparent.Name = "extButtontransparent";
            this.extButtontransparent.Size = new System.Drawing.Size(106, 23);
            this.extButtontransparent.TabIndex = 4;
            this.extButtontransparent.Text = "Toggle transparent";
            this.extButtontransparent.UseVisualStyleBackColor = true;
            this.extButtontransparent.Click += new System.EventHandler(this.extButtontransparent_Click);
            // 
            // extButtonBorder
            // 
            this.extButtonBorder.Location = new System.Drawing.Point(13, 13);
            this.extButtonBorder.Name = "extButtonBorder";
            this.extButtonBorder.Size = new System.Drawing.Size(106, 23);
            this.extButtonBorder.TabIndex = 4;
            this.extButtonBorder.Text = "Toggle border";
            this.extButtonBorder.UseVisualStyleBackColor = true;
            this.extButtonBorder.Click += new System.EventHandler(this.extButtonBorder_Click);
            // 
            // TestTabControlWithTransparency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 733);
            this.Controls.Add(this.extTabControl1);
            this.Controls.Add(this.extButton8);
            this.Controls.Add(this.extButton10);
            this.Controls.Add(this.extButton9);
            this.Controls.Add(this.extButton7);
            this.Controls.Add(this.extButton1);
            this.Controls.Add(this.extButton6);
            this.Controls.Add(this.extButton2);
            this.Controls.Add(this.extButton5);
            this.Controls.Add(this.extButton4);
            this.Controls.Add(this.extButton3);
            this.Controls.Add(this.extButtontransparent);
            this.Controls.Add(this.extButtonBorder);
            this.Controls.Add(this.dataGridViewColumnHider1);
            this.Location = new System.Drawing.Point(100, 10);
            this.Name = "TestTabControlWithTransparency";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TestTransparency";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumnHider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private BaseUtils.DataGridViewColumnControl dataGridViewColumnHider1;
        private ExtendedControls.ExtDataGridViewColumnAutoComplete Column1;
        private ExtendedControls.ExtDataGridViewColumnAutoComplete Column2;
        private ExtendedControls.ExtButton extButtonBorder;
        private ExtendedControls.ExtButton extButtontransparent;
        private ExtendedControls.ExtButton extButton3;
        private ExtendedControls.ExtButton extButton4;
        private ExtendedControls.ExtButton extButton5;
        private ExtendedControls.ExtButton extButton6;
        private ExtendedControls.ExtButton extButton1;
        private ExtendedControls.ExtButton extButton2;
        private ExtendedControls.ExtButton extButton7;
        private ExtendedControls.ExtButton extButton8;
        private ExtendedControls.ExtButton extButton9;
        private ExtendedControls.ExtButton extButton10;
        private ExtendedControls.ExtTabControl extTabControl1;
        private ExtendedControls.ExtDataGridViewColumnAutoComplete extDataGridViewColumnAutoComplete1;
        private ExtendedControls.ExtDataGridViewColumnAutoComplete extDataGridViewColumnAutoComplete2;
    }
}