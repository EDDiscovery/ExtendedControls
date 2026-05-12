namespace TestExtendedControls
{
    partial class TestFontForm
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.labelFont = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Text1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Text2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extButton2 = new ExtendedControls.ExtButton();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.extButton20 = new ExtendedControls.ExtButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(13, 58);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(182, 213);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.richTextBox1.FontChanged += new System.EventHandler(this.richTextBox1_FontChanged);
            // 
            // labelFont
            // 
            this.labelFont.AutoSize = true;
            this.labelFont.Location = new System.Drawing.Point(229, 12);
            this.labelFont.Name = "labelFont";
            this.labelFont.Size = new System.Drawing.Size(35, 13);
            this.labelFont.TabIndex = 3;
            this.labelFont.Text = "label1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Text1,
            this.Text2});
            this.dataGridView1.Location = new System.Drawing.Point(308, 58);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(472, 315);
            this.dataGridView1.TabIndex = 4;
            // 
            // Text1
            // 
            this.Text1.HeaderText = "Column1";
            this.Text1.Name = "Text1";
            this.Text1.ReadOnly = true;
            // 
            // Text2
            // 
            this.Text2.HeaderText = "Column1";
            this.Text2.Name = "Text2";
            this.Text2.ReadOnly = true;
            // 
            // extButton2
            // 
            this.extButton2.BackColor2 = System.Drawing.Color.Red;
            this.extButton2.ButtonDisabledScaling = 0.5F;
            this.extButton2.GradientDirection = 90F;
            this.extButton2.Location = new System.Drawing.Point(548, 12);
            this.extButton2.MouseOverScaling = 1.3F;
            this.extButton2.MouseSelectedScaling = 1.3F;
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(94, 23);
            this.extButton2.TabIndex = 1;
            this.extButton2.Text = "Font Arial";
            this.extButton2.UseVisualStyleBackColor = true;
            this.extButton2.Click += new System.EventHandler(this.extButton2_Click);
            // 
            // extButton1
            // 
            this.extButton1.BackColor2 = System.Drawing.Color.Red;
            this.extButton1.ButtonDisabledScaling = 0.5F;
            this.extButton1.GradientDirection = 90F;
            this.extButton1.Location = new System.Drawing.Point(448, 12);
            this.extButton1.MouseOverScaling = 1.3F;
            this.extButton1.MouseSelectedScaling = 1.3F;
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(94, 23);
            this.extButton1.TabIndex = 1;
            this.extButton1.Text = "Font Zen";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click);
            // 
            // extButton20
            // 
            this.extButton20.BackColor2 = System.Drawing.Color.Red;
            this.extButton20.ButtonDisabledScaling = 0.5F;
            this.extButton20.GradientDirection = 90F;
            this.extButton20.Location = new System.Drawing.Point(12, 12);
            this.extButton20.MouseOverScaling = 1.3F;
            this.extButton20.MouseSelectedScaling = 1.3F;
            this.extButton20.Name = "extButton20";
            this.extButton20.Size = new System.Drawing.Size(183, 23);
            this.extButton20.TabIndex = 1;
            this.extButton20.Text = "Font Select";
            this.extButton20.UseVisualStyleBackColor = true;
            this.extButton20.Click += new System.EventHandler(this.extButton20_Click);
            // 
            // TestFontForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 558);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.labelFont);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.extButton2);
            this.Controls.Add(this.extButton1);
            this.Controls.Add(this.extButton20);
            this.Name = "TestFontForm";
            this.Text = "test Font Form";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ExtendedControls.ExtButton extButton20;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label labelFont;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Text1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Text2;
        private ExtendedControls.ExtButton extButton1;
        private ExtendedControls.ExtButton extButton2;
    }
}