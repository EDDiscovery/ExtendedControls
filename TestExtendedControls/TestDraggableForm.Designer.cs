namespace DialogTest
{
    partial class TestDraggableForm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelp2 = new System.Windows.Forms.Label();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.extButton2 = new ExtendedControls.ExtButton();
            this.extButton3 = new ExtendedControls.ExtButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelp1 = new System.Windows.Forms.Label();
            this.topresize = new ExtendedControls.ExtPanelResizer();
            this.extPanelResizer1 = new ExtendedControls.ExtPanelResizer();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.labelp2);
            this.panel2.Location = new System.Drawing.Point(49, 44);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(171, 27);
            this.panel2.TabIndex = 0;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CaptureDown);
            this.panel2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CaptureUp);
            // 
            // labelp2
            // 
            this.labelp2.AutoSize = true;
            this.labelp2.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.labelp2.Location = new System.Drawing.Point(12, 4);
            this.labelp2.Name = "labelp2";
            this.labelp2.Size = new System.Drawing.Size(67, 13);
            this.labelp2.TabIndex = 0;
            this.labelp2.Text = "MoveControl";
            // 
            // extButton1
            // 
            this.extButton1.Location = new System.Drawing.Point(122, 97);
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(75, 23);
            this.extButton1.TabIndex = 1;
            this.extButton1.Text = "extButton1";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click);
            // 
            // extButton2
            // 
            this.extButton2.Location = new System.Drawing.Point(39, 142);
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(293, 23);
            this.extButton2.TabIndex = 1;
            this.extButton2.Text = "size?";
            this.extButton2.UseVisualStyleBackColor = true;
            this.extButton2.Click += new System.EventHandler(this.extButton2_Click);
            // 
            // extButton3
            // 
            this.extButton3.Location = new System.Drawing.Point(58, 266);
            this.extButton3.Name = "extButton3";
            this.extButton3.Size = new System.Drawing.Size(75, 23);
            this.extButton3.TabIndex = 3;
            this.extButton3.Text = "extButton3";
            this.extButton3.UseVisualStyleBackColor = true;
            this.extButton3.Click += new System.EventHandler(this.extButton3_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelp1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 27);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CaptureDown);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CaptureUp);
            // 
            // labelp1
            // 
            this.labelp1.AutoSize = true;
            this.labelp1.Location = new System.Drawing.Point(12, 4);
            this.labelp1.Name = "labelp1";
            this.labelp1.Size = new System.Drawing.Size(19, 13);
            this.labelp1.TabIndex = 0;
            this.labelp1.Text = "p1";
            // 
            // topresize
            // 
            this.topresize.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.topresize.Dock = System.Windows.Forms.DockStyle.Top;
            this.topresize.Location = new System.Drawing.Point(0, 0);
            this.topresize.Movement = System.Windows.Forms.DockStyle.Top;
            this.topresize.Name = "topresize";
            this.topresize.Size = new System.Drawing.Size(684, 14);
            this.topresize.TabIndex = 4;
            // 
            // extPanelResizer1
            // 
            this.extPanelResizer1.BackColor = System.Drawing.Color.BlueViolet;
            this.extPanelResizer1.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.extPanelResizer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.extPanelResizer1.Location = new System.Drawing.Point(0, 406);
            this.extPanelResizer1.Movement = System.Windows.Forms.DockStyle.Bottom;
            this.extPanelResizer1.Name = "extPanelResizer1";
            this.extPanelResizer1.Size = new System.Drawing.Size(684, 55);
            this.extPanelResizer1.TabIndex = 5;
            // 
            // TestDraggableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Fuchsia;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.extPanelResizer1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.topresize);
            this.Controls.Add(this.extButton3);
            this.Controls.Add(this.extButton2);
            this.Controls.Add(this.extButton1);
            this.Location = new System.Drawing.Point(200, 200);
            this.Name = "TestDraggableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TestCompositeButton";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelp2;
        private ExtendedControls.ExtButton extButton1;
        private ExtendedControls.ExtButton extButton2;
        private ExtendedControls.ExtButton extButton3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelp1;
        private ExtendedControls.ExtPanelResizer topresize;
        private ExtendedControls.ExtPanelResizer extPanelResizer1;
    }
}