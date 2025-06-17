namespace TestExtendedControls
{
    partial class TestPanelDGVOtherCells
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonClearRows = new ExtendedControls.ExtButton();
            this.panel = new ExtendedControls.ExtPanelDataGridViewScroll();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.extScrollBar1 = new ExtendedControls.ExtScrollBar();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonClearRows);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1224, 55);
            this.panel1.TabIndex = 6;
            // 
            // buttonClearRows
            // 
            this.buttonClearRows.Location = new System.Drawing.Point(3, 17);
            this.buttonClearRows.Name = "buttonClearRows";
            this.buttonClearRows.Size = new System.Drawing.Size(75, 23);
            this.buttonClearRows.TabIndex = 2;
            this.buttonClearRows.Text = "Clr Rows";
            this.buttonClearRows.UseVisualStyleBackColor = true;
            this.buttonClearRows.Click += new System.EventHandler(this.buttonClearRows_Click);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.dataGridView);
            this.panel.Controls.Add(this.extScrollBar1);
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.InternalMargin = new System.Windows.Forms.Padding(0);
            this.panel.Location = new System.Drawing.Point(0, 55);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1224, 565);
            this.panel.TabIndex = 1;
            this.panel.VerticalScrollBarDockRight = true;
            // 
            // dataGridView
            // 
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView.Size = new System.Drawing.Size(1202, 565);
            this.dataGridView.TabIndex = 1;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnWidthChanged);
            this.dataGridView.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridView1_SortCompare);
            this.dataGridView.Resize += new System.EventHandler(this.dataGridView1_Resize);
            // 
            // extScrollBar1
            // 
            this.extScrollBar1.AlwaysHideScrollBar = false;
            this.extScrollBar1.ArrowBorderColor = System.Drawing.Color.LightBlue;
            this.extScrollBar1.ArrowButtonColor = System.Drawing.Color.LightGray;
            this.extScrollBar1.ArrowDownDrawAngle = 270F;
            this.extScrollBar1.ArrowUpDrawAngle = 90F;
            this.extScrollBar1.BorderColor = System.Drawing.Color.White;
            this.extScrollBar1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extScrollBar1.HideScrollBar = false;
            this.extScrollBar1.LargeChange = 1;
            this.extScrollBar1.Location = new System.Drawing.Point(1202, 0);
            this.extScrollBar1.Maximum = 0;
            this.extScrollBar1.Minimum = 0;
            this.extScrollBar1.MouseOverButtonColor = System.Drawing.Color.Green;
            this.extScrollBar1.MousePressedButtonColor = System.Drawing.Color.Red;
            this.extScrollBar1.Name = "extScrollBar1";
            this.extScrollBar1.Size = new System.Drawing.Size(22, 565);
            this.extScrollBar1.SliderColor = System.Drawing.Color.DarkGray;
            this.extScrollBar1.SmallChange = 1;
            this.extScrollBar1.TabIndex = 0;
            this.extScrollBar1.Text = "extScrollBar1";
            this.extScrollBar1.ThumbBorderColor = System.Drawing.Color.Yellow;
            this.extScrollBar1.ThumbButtonColor = System.Drawing.Color.DarkBlue;
            this.extScrollBar1.ThumbDrawAngle = 0F;
            this.extScrollBar1.Value = 0;
            this.extScrollBar1.ValueLimited = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.MinimumWidth = 50;
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.MinimumWidth = 50;
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.MinimumWidth = 50;
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "TextCol";
            this.Column4.Name = "Column4";
            // 
            // TestPanelDGV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1224, 640);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.panel1);
            this.Name = "TestPanelDGV2";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ExtendedControls.ExtPanelDataGridViewScroll panel;
        private ExtendedControls.ExtScrollBar extScrollBar1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Panel panel1;
        private ExtendedControls.ExtButton buttonClearRows;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.DataGridViewImageColumn Column2;
        private System.Windows.Forms.DataGridViewImageColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}