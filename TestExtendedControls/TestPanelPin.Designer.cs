namespace DialogTest
{
    partial class TestPanelPin
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
            this.extPanelPin2 = new ExtendedControls.ExtPanelPin(this.components);
            this.extPanelPin1 = new ExtendedControls.ExtPanelPin(this.components);
            this.SuspendLayout();
            // 
            // extPanelPin2
            // 
            this.extPanelPin2.BackColor = System.Drawing.Color.LimeGreen;
            this.extPanelPin2.Location = new System.Drawing.Point(104, 54);
            this.extPanelPin2.Name = "extPanelPin2";
            this.extPanelPin2.Size = new System.Drawing.Size(200, 100);
            this.extPanelPin2.TabIndex = 0;
            // 
            // extPanelPin1
            // 
            this.extPanelPin1.BackColor = System.Drawing.Color.Red;
            this.extPanelPin1.Location = new System.Drawing.Point(0, 0);
            this.extPanelPin1.Name = "extPanelPin1";
            this.extPanelPin1.Size = new System.Drawing.Size(200, 100);
            this.extPanelPin1.TabIndex = 0;
            // 
            // TestPanelPin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.extPanelPin2);
            this.Controls.Add(this.extPanelPin1);
            this.Name = "TestPanelPin";
            this.Text = "TestPanelPin";
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.ExtPanelPin extPanelPin1;
        private ExtendedControls.ExtPanelPin extPanelPin2;
    }
}