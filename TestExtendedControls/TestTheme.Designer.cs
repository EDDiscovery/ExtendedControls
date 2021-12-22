namespace TestExtendedControls
{
    partial class TestTheme
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
            this.extButtonLoad = new ExtendedControls.ExtButton();
            this.extButtonSave = new ExtendedControls.ExtButton();
            this.extButtonEdit = new ExtendedControls.ExtButton();
            this.panel = new ExtendedControls.ExtPanelDataGridViewScroll();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extScrollBar1 = new ExtendedControls.ExtScrollBar();
            this.extButtonVerdana = new ExtendedControls.ExtButton();
            this.extButtonEuroCaps = new ExtendedControls.ExtButton();
            this.extButtonClose = new ExtendedControls.ExtButton();
            this.labelName = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // extButtonLoad
            // 
            this.extButtonLoad.Location = new System.Drawing.Point(202, 13);
            this.extButtonLoad.Name = "extButtonLoad";
            this.extButtonLoad.Size = new System.Drawing.Size(75, 23);
            this.extButtonLoad.TabIndex = 3;
            this.extButtonLoad.Text = "Load";
            this.extButtonLoad.UseVisualStyleBackColor = true;
            this.extButtonLoad.Click += new System.EventHandler(this.extButtonLoad_Click);
            // 
            // extButtonSave
            // 
            this.extButtonSave.Location = new System.Drawing.Point(108, 13);
            this.extButtonSave.Name = "extButtonSave";
            this.extButtonSave.Size = new System.Drawing.Size(75, 23);
            this.extButtonSave.TabIndex = 3;
            this.extButtonSave.Text = "Save";
            this.extButtonSave.UseVisualStyleBackColor = true;
            this.extButtonSave.Click += new System.EventHandler(this.extButtonSave_Click);
            // 
            // extButtonEdit
            // 
            this.extButtonEdit.Location = new System.Drawing.Point(13, 13);
            this.extButtonEdit.Name = "extButtonEdit";
            this.extButtonEdit.Size = new System.Drawing.Size(75, 23);
            this.extButtonEdit.TabIndex = 3;
            this.extButtonEdit.Text = "Edit";
            this.extButtonEdit.UseVisualStyleBackColor = true;
            this.extButtonEdit.Click += new System.EventHandler(this.extButtonEdit_Click);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.dataGridView);
            this.panel.Controls.Add(this.extScrollBar1);
            this.panel.InternalMargin = new System.Windows.Forms.Padding(0);
            this.panel.Location = new System.Drawing.Point(3, 170);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(754, 364);
            this.panel.TabIndex = 2;
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
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView.Size = new System.Drawing.Size(738, 364);
            this.dataGridView.TabIndex = 1;
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
            this.extScrollBar1.ArrowColorScaling = 0.5F;
            this.extScrollBar1.ArrowDownDrawAngle = 270F;
            this.extScrollBar1.ArrowUpDrawAngle = 90F;
            this.extScrollBar1.BorderColor = System.Drawing.Color.White;
            this.extScrollBar1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extScrollBar1.HideScrollBar = false;
            this.extScrollBar1.LargeChange = 1;
            this.extScrollBar1.Location = new System.Drawing.Point(738, 0);
            this.extScrollBar1.Maximum = 0;
            this.extScrollBar1.Minimum = 0;
            this.extScrollBar1.MouseOverButtonColor = System.Drawing.Color.Green;
            this.extScrollBar1.MousePressedButtonColor = System.Drawing.Color.Red;
            this.extScrollBar1.Name = "extScrollBar1";
            this.extScrollBar1.Size = new System.Drawing.Size(16, 364);
            this.extScrollBar1.SliderColor = System.Drawing.Color.DarkGray;
            this.extScrollBar1.SmallChange = 1;
            this.extScrollBar1.TabIndex = 0;
            this.extScrollBar1.Text = "extScrollBar1";
            this.extScrollBar1.ThumbBorderColor = System.Drawing.Color.Yellow;
            this.extScrollBar1.ThumbButtonColor = System.Drawing.Color.DarkBlue;
            this.extScrollBar1.ThumbColorScaling = 0.5F;
            this.extScrollBar1.ThumbDrawAngle = 0F;
            this.extScrollBar1.Value = 0;
            this.extScrollBar1.ValueLimited = 0;
            // 
            // extButtonVerdana
            // 
            this.extButtonVerdana.Location = new System.Drawing.Point(293, 13);
            this.extButtonVerdana.Name = "extButtonVerdana";
            this.extButtonVerdana.Size = new System.Drawing.Size(75, 23);
            this.extButtonVerdana.TabIndex = 3;
            this.extButtonVerdana.Text = "Verdana";
            this.extButtonVerdana.UseVisualStyleBackColor = true;
            this.extButtonVerdana.Click += new System.EventHandler(this.extButtonVerdana_Click);
            // 
            // extButtonEuroCaps
            // 
            this.extButtonEuroCaps.Location = new System.Drawing.Point(374, 13);
            this.extButtonEuroCaps.Name = "extButtonEuroCaps";
            this.extButtonEuroCaps.Size = new System.Drawing.Size(75, 23);
            this.extButtonEuroCaps.TabIndex = 3;
            this.extButtonEuroCaps.Text = "EuroCaps";
            this.extButtonEuroCaps.UseVisualStyleBackColor = true;
            this.extButtonEuroCaps.Click += new System.EventHandler(this.extButtonEuroCaps_Click);
            // 
            // extButtonClose
            // 
            this.extButtonClose.Location = new System.Drawing.Point(792, 12);
            this.extButtonClose.Name = "extButtonClose";
            this.extButtonClose.Size = new System.Drawing.Size(75, 23);
            this.extButtonClose.TabIndex = 3;
            this.extButtonClose.Text = "Close";
            this.extButtonClose.UseVisualStyleBackColor = true;
            this.extButtonClose.Click += new System.EventHandler(this.extButtonClose_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(13, 59);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(35, 13);
            this.labelName.TabIndex = 4;
            this.labelName.Text = "Name";
            // 
            // TestTheme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 666);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.extButtonClose);
            this.Controls.Add(this.extButtonEuroCaps);
            this.Controls.Add(this.extButtonVerdana);
            this.Controls.Add(this.extButtonLoad);
            this.Controls.Add(this.extButtonSave);
            this.Controls.Add(this.extButtonEdit);
            this.Controls.Add(this.panel);
            this.Name = "TestTheme";
            this.Text = "Form1";
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExtendedControls.ExtPanelDataGridViewScroll panel;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private ExtendedControls.ExtScrollBar extScrollBar1;
        private ExtendedControls.ExtButton extButtonEdit;
        private ExtendedControls.ExtButton extButtonSave;
        private ExtendedControls.ExtButton extButtonLoad;
        private ExtendedControls.ExtButton extButtonVerdana;
        private ExtendedControls.ExtButton extButtonEuroCaps;
        private ExtendedControls.ExtButton extButtonClose;
        private System.Windows.Forms.Label labelName;
    }
}