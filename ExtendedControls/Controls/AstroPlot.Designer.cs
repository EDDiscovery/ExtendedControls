namespace ExtendedControls.Controls
{
    partial class ExtAstroPlot
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
            this.components = new System.ComponentModel.Container();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.plot = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.plot)).BeginInit();
            this.SuspendLayout();
            // 
            // plot
            // 
            this.plot.BackColor = System.Drawing.Color.Transparent;
            this.plot.Location = new System.Drawing.Point(17, 15);
            this.plot.Name = "plot";
            this.plot.Size = new System.Drawing.Size(224, 224);
            this.plot.TabIndex = 0;
            this.plot.TabStop = false;
            this.plot.SizeChanged += new System.EventHandler(this.plot_SizeChanged);
            this.plot.Paint += new System.Windows.Forms.PaintEventHandler(this.plot_Paint);
            this.plot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.plot_MouseDown);
            this.plot.MouseMove += new System.Windows.Forms.MouseEventHandler(this.plot_MouseMove);
            this.plot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.plot_MouseUp);
            // 
            // ExtAstroPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.plot);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ExtAstroPlot";
            this.Size = new System.Drawing.Size(256, 256);
            this.SizeChanged += new System.EventHandler(this.ExtAstroPlot_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.plot)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox plot;
    }
}
