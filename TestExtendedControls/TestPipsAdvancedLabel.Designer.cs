namespace TestExtendedControls
{
    partial class TestPipsAdvancedLabel
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelData1 = new ExtendedControls.LabelData();
            this.extButtonDrawn1 = new ExtendedControls.ExtButtonDrawn();
            this.multiPipControl3 = new ExtendedControls.MultiPipControl();
            this.multiPipControl2 = new ExtendedControls.MultiPipControl();
            this.multiPipControl1 = new ExtendedControls.MultiPipControl();
            this.labelData2 = new ExtendedControls.LabelData();
            this.SuspendLayout();
            // 
            // labelData1
            // 
            this.labelData1.BorderColor = System.Drawing.Color.Orange;
            this.labelData1.BorderWidth = 2;
            this.labelData1.BoxStyle = ExtendedControls.LabelData.DataBoxStyle.AllAround;
            this.labelData1.Data = null;
            this.labelData1.DataFont = null;
            this.labelData1.ForeColor = System.Drawing.Color.DarkOrange;
            this.labelData1.InterSpacing = 1;
            this.labelData1.Location = new System.Drawing.Point(12, 377);
            this.labelData1.Name = "labelData1";
            this.labelData1.Size = new System.Drawing.Size(828, 23);
            this.labelData1.TabIndex = 2;
            this.labelData1.TabSpacingData = 0;
            this.labelData1.Text = "DataLab1";
            // 
            // extButtonDrawn1
            // 
            this.extButtonDrawn1.AutoEllipsis = false;
            this.extButtonDrawn1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.extButtonDrawn1.BorderColor = System.Drawing.Color.Orange;
            this.extButtonDrawn1.BorderWidth = 1;
            this.extButtonDrawn1.Image = null;
            this.extButtonDrawn1.ImageSelected = ExtendedControls.ExtButtonDrawn.ImageType.TextBorder;
            this.extButtonDrawn1.Location = new System.Drawing.Point(74, 273);
            this.extButtonDrawn1.MouseOverColor = System.Drawing.Color.White;
            this.extButtonDrawn1.MouseSelectedColor = System.Drawing.Color.Green;
            this.extButtonDrawn1.MouseSelectedColorEnable = true;
            this.extButtonDrawn1.Name = "extButtonDrawn1";
            this.extButtonDrawn1.PanelDisabledScaling = 0.25F;
            this.extButtonDrawn1.Selectable = true;
            this.extButtonDrawn1.Size = new System.Drawing.Size(85, 47);
            this.extButtonDrawn1.TabIndex = 1;
            this.extButtonDrawn1.Text = "RST";
            this.extButtonDrawn1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.extButtonDrawn1.UseMnemonic = true;
            // 
            // multiPipControl3
            // 
            this.multiPipControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.multiPipControl3.BorderColor = System.Drawing.Color.Orange;
            this.multiPipControl3.BorderWidth = 2;
            this.multiPipControl3.HalfPipColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(124)))), ((int)(((byte)(0)))));
            this.multiPipControl3.InterSpacing = 1;
            this.multiPipControl3.Location = new System.Drawing.Point(74, 181);
            this.multiPipControl3.MaxValue = 8;
            this.multiPipControl3.Name = "multiPipControl3";
            this.multiPipControl3.PipColor = System.Drawing.Color.Orange;
            this.multiPipControl3.PipsPerClick = 2;
            this.multiPipControl3.PipsTakenPerCLickFromOthers = 1;
            this.multiPipControl3.Size = new System.Drawing.Size(188, 54);
            this.multiPipControl3.TabIndex = 0;
            this.multiPipControl3.Text = "Wep";
            this.multiPipControl3.Value = 4;
            this.multiPipControl3.ValueChanged = null;
            // 
            // multiPipControl2
            // 
            this.multiPipControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.multiPipControl2.BorderColor = System.Drawing.Color.Orange;
            this.multiPipControl2.BorderWidth = 2;
            this.multiPipControl2.HalfPipColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(124)))), ((int)(((byte)(0)))));
            this.multiPipControl2.InterSpacing = 1;
            this.multiPipControl2.Location = new System.Drawing.Point(74, 116);
            this.multiPipControl2.MaxValue = 8;
            this.multiPipControl2.Name = "multiPipControl2";
            this.multiPipControl2.PipColor = System.Drawing.Color.Orange;
            this.multiPipControl2.PipsPerClick = 2;
            this.multiPipControl2.PipsTakenPerCLickFromOthers = 1;
            this.multiPipControl2.Size = new System.Drawing.Size(188, 54);
            this.multiPipControl2.TabIndex = 0;
            this.multiPipControl2.Text = "Eng";
            this.multiPipControl2.Value = 4;
            this.multiPipControl2.ValueChanged = null;
            // 
            // multiPipControl1
            // 
            this.multiPipControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.multiPipControl1.BorderColor = System.Drawing.Color.Orange;
            this.multiPipControl1.BorderWidth = 2;
            this.multiPipControl1.HalfPipColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(124)))), ((int)(((byte)(0)))));
            this.multiPipControl1.InterSpacing = 1;
            this.multiPipControl1.Location = new System.Drawing.Point(74, 42);
            this.multiPipControl1.MaxValue = 8;
            this.multiPipControl1.Name = "multiPipControl1";
            this.multiPipControl1.PipColor = System.Drawing.Color.Orange;
            this.multiPipControl1.PipsPerClick = 2;
            this.multiPipControl1.PipsTakenPerCLickFromOthers = 1;
            this.multiPipControl1.Size = new System.Drawing.Size(188, 54);
            this.multiPipControl1.TabIndex = 0;
            this.multiPipControl1.Text = "Sys";
            this.multiPipControl1.Value = 4;
            this.multiPipControl1.ValueChanged = null;
            // 
            // labelData2
            // 
            this.labelData2.BorderColor = System.Drawing.Color.Orange;
            this.labelData2.BorderWidth = 2;
            this.labelData2.BoxStyle = ExtendedControls.LabelData.DataBoxStyle.AllAround;
            this.labelData2.Data = null;
            this.labelData2.DataFont = null;
            this.labelData2.ForeColor = System.Drawing.Color.DarkOrange;
            this.labelData2.InterSpacing = 1;
            this.labelData2.Location = new System.Drawing.Point(12, 406);
            this.labelData2.Name = "labelData2";
            this.labelData2.Size = new System.Drawing.Size(828, 23);
            this.labelData2.TabIndex = 2;
            this.labelData2.TabSpacingData = 0;
            this.labelData2.Text = "DataLab2";
            // 
            // TestPipsAdvancedLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 509);
            this.Controls.Add(this.labelData2);
            this.Controls.Add(this.labelData1);
            this.Controls.Add(this.extButtonDrawn1);
            this.Controls.Add(this.multiPipControl3);
            this.Controls.Add(this.multiPipControl2);
            this.Controls.Add(this.multiPipControl1);
            this.Name = "TestPipsAdvancedLabel";
            this.Text = "TestPipsAdvancedLabel";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private ExtendedControls.MultiPipControl multiPipControl1;
        private ExtendedControls.MultiPipControl multiPipControl2;
        private ExtendedControls.MultiPipControl multiPipControl3;
        private ExtendedControls.ExtButtonDrawn extButtonDrawn1;
        private ExtendedControls.LabelData labelData1;
        private ExtendedControls.LabelData labelData2;
    }
}