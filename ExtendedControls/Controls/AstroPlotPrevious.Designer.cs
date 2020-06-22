namespace ExtendedControls.Controls
{
    partial class ExtAstroPlotPrevious
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.plot = new ExtendedControls.ExtPictureBox();
            this.systemLabel = new ExtendedControls.ExtLabel();
            ((System.ComponentModel.ISupportInitialize)(this.plot)).BeginInit();
            this.SuspendLayout();
            // 
            // plot
            // 
            this.plot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plot.Location = new System.Drawing.Point(0, 0);
            this.plot.Name = "plot";
            this.plot.Size = new System.Drawing.Size(256, 256);
            this.plot.TabIndex = 2;
            this.plot.SizeChanged += new System.EventHandler(this.plot_SizeChanged_1);
            this.plot.Paint += new System.Windows.Forms.PaintEventHandler(this.Plot_Paint);
            this.plot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Plot_MouseDown);
            this.plot.MouseLeave += new System.EventHandler(this.Plot_MouseLeave);
            this.plot.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Plot_MouseMove);
            this.plot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Plot_MouseUp);
            // 
            // systemLabel
            // 
            this.systemLabel.AutoSize = true;
            this.systemLabel.Location = new System.Drawing.Point(8, 8);
            this.systemLabel.Name = "systemLabel";
            this.systemLabel.Size = new System.Drawing.Size(35, 13);
            this.systemLabel.TabIndex = 1;
            this.systemLabel.Text = "tooltip";
            this.systemLabel.TextBackColor = System.Drawing.Color.Transparent;
            // 
            // ExtAstroPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.systemLabel);
            this.Controls.Add(this.plot);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ExtAstroPlot";
            this.Size = new System.Drawing.Size(256, 256);
            ((System.ComponentModel.ISupportInitialize)(this.plot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ExtLabel systemLabel;
        private ExtPictureBox plot;
    }
}
