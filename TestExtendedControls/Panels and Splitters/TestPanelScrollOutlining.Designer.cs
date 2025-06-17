namespace TestExtendedControls
{
    partial class TestPanelScrollOutlining
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
            this.panel2 = new ExtendedControls.ExtPanelDataGridViewScroll();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extScrollBar2 = new ExtendedControls.ExtScrollBar();
            this.panel = new ExtendedControls.ExtPanelDataGridViewScroll();
            this.Outlining1 = new ExtendedControls.ExtPanelDataGridViewScrollOutlining();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extScrollBar1 = new ExtendedControls.ExtScrollBar();
            this.button2 = new ExtendedControls.ExtButton();
            this.button3 = new ExtendedControls.ExtButton();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.extButton2 = new ExtendedControls.ExtButton();
            this.extButton3 = new ExtendedControls.ExtButton();
            this.extButton4 = new ExtendedControls.ExtButton();
            this.extButton5 = new ExtendedControls.ExtButton();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Remove6-8";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView2);
            this.panel2.Controls.Add(this.extScrollBar2);
            this.panel2.InternalMargin = new System.Windows.Forms.Padding(0);
            this.panel2.Location = new System.Drawing.Point(723, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(463, 565);
            this.panel2.TabIndex = 3;
            this.panel2.VerticalScrollBarDockRight = false;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dataGridView2.Location = new System.Drawing.Point(16, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView2.Size = new System.Drawing.Size(447, 565);
            this.dataGridView2.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Column3";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // extScrollBar2
            // 
            this.extScrollBar2.ArrowBorderColor = System.Drawing.Color.LightBlue;
            this.extScrollBar2.ArrowButtonColor = System.Drawing.Color.LightGray;
            this.extScrollBar2.ArrowDownDrawAngle = 270F;
            this.extScrollBar2.ArrowUpDrawAngle = 90F;
            this.extScrollBar2.BorderColor = System.Drawing.Color.White;
            this.extScrollBar2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extScrollBar2.HideScrollBar = false;
            this.extScrollBar2.LargeChange = 1;
            this.extScrollBar2.Location = new System.Drawing.Point(0, 0);
            this.extScrollBar2.Maximum = 0;
            this.extScrollBar2.Minimum = 0;
            this.extScrollBar2.MouseOverButtonColor = System.Drawing.Color.Green;
            this.extScrollBar2.MousePressedButtonColor = System.Drawing.Color.Red;
            this.extScrollBar2.Name = "extScrollBar2";
            this.extScrollBar2.Size = new System.Drawing.Size(16, 565);
            this.extScrollBar2.SliderColor = System.Drawing.Color.DarkGray;
            this.extScrollBar2.SmallChange = 1;
            this.extScrollBar2.TabIndex = 0;
            this.extScrollBar2.Text = "extScrollBar2";
            this.extScrollBar2.ThumbBorderColor = System.Drawing.Color.Yellow;
            this.extScrollBar2.ThumbButtonColor = System.Drawing.Color.DarkBlue;
            this.extScrollBar2.ThumbDrawAngle = 0F;
            this.extScrollBar2.Value = 0;
            this.extScrollBar2.ValueLimited = 0;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.Outlining1);
            this.panel.Controls.Add(this.dataGridView1);
            this.panel.Controls.Add(this.extScrollBar1);
            this.panel.InternalMargin = new System.Windows.Forms.Padding(0);
            this.panel.Location = new System.Drawing.Point(12, 51);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(677, 565);
            this.panel.TabIndex = 1;
            this.panel.VerticalScrollBarDockRight = false;
            // 
            // Outlining1
            // 
            this.Outlining1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Outlining1.KeepLastEntriesVisibleOnRollUp = 1;
            this.Outlining1.Location = new System.Drawing.Point(0, 0);
            this.Outlining1.Name = "Outlining1";
            this.Outlining1.Size = new System.Drawing.Size(50, 565);
            this.Outlining1.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(66, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.Size = new System.Drawing.Size(611, 565);
            this.dataGridView1.TabIndex = 1;
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
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // extScrollBar1
            // 
            this.extScrollBar1.ArrowBorderColor = System.Drawing.Color.LightBlue;
            this.extScrollBar1.ArrowButtonColor = System.Drawing.Color.LightGray;
            this.extScrollBar1.ArrowUpDrawAngle = 90F;
            this.extScrollBar1.BorderColor = System.Drawing.Color.White;
            this.extScrollBar1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extScrollBar1.HideScrollBar = false;
            this.extScrollBar1.LargeChange = 1;
            this.extScrollBar1.Location = new System.Drawing.Point(50, 0);
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
            this.button2.Location = new System.Drawing.Point(723, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Remove2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(93, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Remove 6";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // extButton1
            // 
            this.extButton1.Location = new System.Drawing.Point(533, 12);
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(75, 23);
            this.extButton1.TabIndex = 4;
            this.extButton1.Text = "T12";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click);
            // 
            // extButton2
            // 
            this.extButton2.Location = new System.Drawing.Point(614, 11);
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(75, 23);
            this.extButton2.TabIndex = 5;
            this.extButton2.Text = "T20";
            this.extButton2.UseVisualStyleBackColor = true;
            this.extButton2.Click += new System.EventHandler(this.extButton2_Click);
            // 
            // extButton3
            // 
            this.extButton3.Location = new System.Drawing.Point(337, 11);
            this.extButton3.Name = "extButton3";
            this.extButton3.Size = new System.Drawing.Size(75, 23);
            this.extButton3.TabIndex = 2;
            this.extButton3.Text = "Clear";
            this.extButton3.UseVisualStyleBackColor = true;
            this.extButton3.Click += new System.EventHandler(this.extButton3_Click);
            // 
            // extButton4
            // 
            this.extButton4.Location = new System.Drawing.Point(174, 13);
            this.extButton4.Name = "extButton4";
            this.extButton4.Size = new System.Drawing.Size(75, 23);
            this.extButton4.TabIndex = 2;
            this.extButton4.Text = "Add 10";
            this.extButton4.UseVisualStyleBackColor = true;
            this.extButton4.Click += new System.EventHandler(this.extButton4_Click);
            // 
            // extButton5
            // 
            this.extButton5.Location = new System.Drawing.Point(255, 13);
            this.extButton5.Name = "extButton5";
            this.extButton5.Size = new System.Drawing.Size(75, 23);
            this.extButton5.TabIndex = 2;
            this.extButton5.Text = "Remove 10";
            this.extButton5.UseVisualStyleBackColor = true;
            this.extButton5.Click += new System.EventHandler(this.extButton5_Click);
            // 
            // TestPanelScrollOutlining
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1224, 640);
            this.Controls.Add(this.extButton2);
            this.Controls.Add(this.extButton1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.extButton5);
            this.Controls.Add(this.extButton4);
            this.Controls.Add(this.extButton3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel);
            this.Name = "TestPanelScrollOutlining";
            this.Text = "Form1";
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ExtendedControls.ExtPanelDataGridViewScroll panel;
        private ExtendedControls.ExtScrollBar extScrollBar1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private ExtendedControls.ExtPanelDataGridViewScrollOutlining Outlining1;
        private ExtendedControls.ExtButton button1;
        private ExtendedControls.ExtPanelDataGridViewScroll panel2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private ExtendedControls.ExtScrollBar extScrollBar2;
        private ExtendedControls.ExtButton button2;
        private ExtendedControls.ExtButton button3;
        private ExtendedControls.ExtButton extButton1;
        private ExtendedControls.ExtButton extButton2;
        private ExtendedControls.ExtButton extButton3;
        private ExtendedControls.ExtButton extButton4;
        private ExtendedControls.ExtButton extButton5;
    }
}