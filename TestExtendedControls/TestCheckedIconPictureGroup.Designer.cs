namespace TestExtendedControls
{
    partial class TestCheckedIconPictureGroup
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
            this.checkedIconPictureBoxUserControl1 = new ExtendedControls.CheckedIconGroupUserControl();
            this.SuspendLayout();
            // 
            // checkedIconPictureBoxUserControl1
            // 
            this.checkedIconPictureBoxUserControl1.ArrowBorderColor = System.Drawing.Color.LightBlue;
            this.checkedIconPictureBoxUserControl1.ArrowButtonColor = System.Drawing.Color.LightGray;
            this.checkedIconPictureBoxUserControl1.ArrowUpDrawAngle = 90F;
            this.checkedIconPictureBoxUserControl1.BorderColor = System.Drawing.Color.White;
            this.checkedIconPictureBoxUserControl1.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkedIconPictureBoxUserControl1.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkedIconPictureBoxUserControl1.CheckBoxSize = new System.Drawing.Size(0, 0);
            this.checkedIconPictureBoxUserControl1.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkedIconPictureBoxUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedIconPictureBoxUserControl1.HorizontalSpacing = 4;
            this.checkedIconPictureBoxUserControl1.ImageSize = new System.Drawing.Size(0, 0);
            this.checkedIconPictureBoxUserControl1.LargeChange = 509;
            this.checkedIconPictureBoxUserControl1.Location = new System.Drawing.Point(0, 0);
            this.checkedIconPictureBoxUserControl1.MouseOverButtonColor = System.Drawing.Color.Green;
            this.checkedIconPictureBoxUserControl1.MouseOverCheckboxColor = System.Drawing.Color.CornflowerBlue;
            this.checkedIconPictureBoxUserControl1.MousePressedButtonColor = System.Drawing.Color.Red;
            this.checkedIconPictureBoxUserControl1.MultipleColumns = false;
            this.checkedIconPictureBoxUserControl1.Name = "checkedIconPictureBoxUserControl1";
            this.checkedIconPictureBoxUserControl1.SettingsSplittingChar = ';';
            this.checkedIconPictureBoxUserControl1.Size = new System.Drawing.Size(792, 509);
            this.checkedIconPictureBoxUserControl1.SliderColor = System.Drawing.Color.DarkGray;
            this.checkedIconPictureBoxUserControl1.TabIndex = 0;
            this.checkedIconPictureBoxUserControl1.ThumbBorderColor = System.Drawing.Color.Yellow;
            this.checkedIconPictureBoxUserControl1.ThumbButtonColor = System.Drawing.Color.DarkBlue;
            this.checkedIconPictureBoxUserControl1.ThumbDrawAngle = 0F;
            this.checkedIconPictureBoxUserControl1.TickBoxReductionRatio = 0.75F;
            this.checkedIconPictureBoxUserControl1.VerticalSpacing = 4;
            // 
            // TestCheckedIconPictureGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 509);
            this.Controls.Add(this.checkedIconPictureBoxUserControl1);
            this.Name = "TestCheckedIconPictureGroupForm";
            this.Text = "TestCheckedIconPictureGroup";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private ExtendedControls.CheckedIconGroupUserControl checkedIconPictureBoxUserControl1;
    }
}