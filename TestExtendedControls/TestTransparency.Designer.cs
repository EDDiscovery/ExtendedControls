namespace TestExtendedControls
{
    partial class TestTransparency
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
            this.extButtonBorder = new ExtendedControls.ExtButton();
            this.extTabControl1 = new ExtendedControls.ExtTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.extButton2 = new ExtendedControls.ExtButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.extButton3 = new ExtendedControls.ExtButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumnHider1)).BeginInit();
            this.extTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
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
            // extTabControl1
            // 
            this.extTabControl1.AllowDragReorder = false;
            this.extTabControl1.Controls.Add(this.tabPage1);
            this.extTabControl1.Controls.Add(this.tabPage2);
            this.extTabControl1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extTabControl1.Location = new System.Drawing.Point(13, 232);
            this.extTabControl1.Name = "extTabControl1";
            this.extTabControl1.SelectedIndex = 0;
            this.extTabControl1.Size = new System.Drawing.Size(638, 100);
            this.extTabControl1.TabColorScaling = 0.5F;
            this.extTabControl1.TabControlBorderBrightColor = System.Drawing.Color.LightGray;
            this.extTabControl1.TabControlBorderColor = System.Drawing.Color.DarkGray;
            this.extTabControl1.TabDisabledScaling = 0.5F;
            this.extTabControl1.TabIndex = 5;
            this.extTabControl1.TabMouseOverColor = System.Drawing.Color.White;
            this.extTabControl1.TabNotSelectedBorderColor = System.Drawing.Color.Gray;
            this.extTabControl1.TabNotSelectedColor = System.Drawing.Color.Gray;
            this.extTabControl1.TabOpaque = 100F;
            this.extTabControl1.TabSelectedColor = System.Drawing.Color.LightGray;
            this.extTabControl1.TabStyle = tabStyleSquare1;
            this.extTabControl1.TextNotSelectedColor = System.Drawing.SystemColors.ControlText;
            this.extTabControl1.TextSelectedColor = System.Drawing.SystemColors.ControlText;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.extButton1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(630, 74);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.extButton2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(630, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // extButton1
            // 
            this.extButton1.Location = new System.Drawing.Point(24, 17);
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(75, 23);
            this.extButton1.TabIndex = 0;
            this.extButton1.Text = "extButton1";
            this.extButton1.UseVisualStyleBackColor = true;
            // 
            // extButton2
            // 
            this.extButton2.Location = new System.Drawing.Point(18, 16);
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(75, 23);
            this.extButton2.TabIndex = 0;
            this.extButton2.Text = "extButton2";
            this.extButton2.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(17, 359);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(634, 100);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.extButton3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(626, 74);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(192, 74);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // extButton3
            // 
            this.extButton3.Location = new System.Drawing.Point(7, 19);
            this.extButton3.Name = "extButton3";
            this.extButton3.Size = new System.Drawing.Size(75, 23);
            this.extButton3.TabIndex = 0;
            this.extButton3.Text = "extButton3";
            this.extButton3.UseVisualStyleBackColor = true;
            // 
            // TestTransparency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 509);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.extTabControl1);
            this.Controls.Add(this.extButtonBorder);
            this.Controls.Add(this.dataGridViewColumnHider1);
            this.Name = "TestTransparency";
            this.Text = "TestTransparency";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumnHider1)).EndInit();
            this.extTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private BaseUtils.DataGridViewColumnControl dataGridViewColumnHider1;
        private ExtendedControls.ExtDataGridViewColumnAutoComplete Column1;
        private ExtendedControls.ExtDataGridViewColumnAutoComplete Column2;
        private ExtendedControls.ExtButton extButtonBorder;
        private ExtendedControls.ExtTabControl extTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ExtendedControls.ExtButton extButton1;
        private ExtendedControls.ExtButton extButton2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private ExtendedControls.ExtButton extButton3;
        private System.Windows.Forms.TabPage tabPage4;
    }
}