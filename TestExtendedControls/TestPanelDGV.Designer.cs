namespace TestExtendedControls
{
    partial class TestPanelDGV
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
            this.button1 = new ExtendedControls.ExtButton();
            this.panel = new ExtendedControls.ExtPanelDataGridViewScroll();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extScrollBar1 = new ExtendedControls.ExtScrollBar();
            this.button2 = new ExtendedControls.ExtButton();
            this.extButton3 = new ExtendedControls.ExtButton();
            this.extButton4 = new ExtendedControls.ExtButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Clr Rows";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.Column3});
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView.Size = new System.Drawing.Size(1208, 565);
            this.dataGridView.TabIndex = 1;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnWidthChanged);
            this.dataGridView.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridView1_SortCompare);
            this.dataGridView.Resize += new System.EventHandler(this.dataGridView1_Resize);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 10F;
            this.Column1.HeaderText = "Column1";
            this.Column1.MinimumWidth = 50;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 30F;
            this.Column2.HeaderText = "Column2";
            this.Column2.MinimumWidth = 50;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.FillWeight = 40F;
            this.Column3.HeaderText = "Column3";
            this.Column3.MinimumWidth = 50;
            this.Column3.Name = "Column3";
            // 
            // extScrollBar1
            // 
            this.extScrollBar1.ArrowBorderColor = System.Drawing.Color.LightBlue;
            this.extScrollBar1.ArrowButtonColor = System.Drawing.Color.LightGray;
            this.extScrollBar1.ArrowDownDrawAngle = 270F;
            this.extScrollBar1.ArrowUpDrawAngle = 90F;
            this.extScrollBar1.BorderColor = System.Drawing.Color.White;
            this.extScrollBar1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extScrollBar1.HideScrollBar = false;
            this.extScrollBar1.LargeChange = 1;
            this.extScrollBar1.Location = new System.Drawing.Point(1208, 0);
            this.extScrollBar1.Maximum = 0;
            this.extScrollBar1.Minimum = 0;
            this.extScrollBar1.MouseOverButtonColor = System.Drawing.Color.Green;
            this.extScrollBar1.MousePressedButtonColor = System.Drawing.Color.Red;
            this.extScrollBar1.Name = "extScrollBar1";
            this.extScrollBar1.Size = new System.Drawing.Size(16, 565);
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
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(105, 17);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "add text";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // extButton3
            // 
            this.extButton3.Location = new System.Drawing.Point(186, 17);
            this.extButton3.Name = "extButton3";
            this.extButton3.Size = new System.Drawing.Size(75, 23);
            this.extButton3.TabIndex = 4;
            this.extButton3.Text = "Remove text";
            this.extButton3.UseVisualStyleBackColor = true;
            this.extButton3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // extButton4
            // 
            this.extButton4.Location = new System.Drawing.Point(346, 17);
            this.extButton4.Name = "extButton4";
            this.extButton4.Size = new System.Drawing.Size(75, 23);
            this.extButton4.TabIndex = 5;
            this.extButton4.Text = "B4";
            this.extButton4.UseVisualStyleBackColor = true;
            this.extButton4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.extButton4);
            this.panel1.Controls.Add(this.extButton3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1224, 55);
            this.panel1.TabIndex = 6;
            // 
            // TestPanelDGV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1224, 640);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.panel1);
            this.Name = "TestPanelDGV";
            this.Text = "Form1";
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ExtendedControls.ExtPanelDataGridViewScroll panel;
        private ExtendedControls.ExtScrollBar extScrollBar1;
        private System.Windows.Forms.DataGridView dataGridView;
        private ExtendedControls.ExtButton button1;
        private ExtendedControls.ExtButton button2;
        private ExtendedControls.ExtButton extButton3;
        private ExtendedControls.ExtButton extButton4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}