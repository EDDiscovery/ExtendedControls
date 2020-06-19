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
            this.plot = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.plot)).BeginInit();
            this.SuspendLayout();
            // 
            // plot
            // 
            this.plot.BackColor = System.Drawing.Color.Transparent;
            this.plot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plot.Location = new System.Drawing.Point(0, 0);
            this.plot.Name = "plot";
            this.plot.Size = new System.Drawing.Size(256, 256);
            this.plot.TabIndex = 0;
            this.plot.TabStop = false;
            this.plot.SizeChanged += new System.EventHandler(this.Plot_SizeChanged);
            this.plot.Paint += new System.Windows.Forms.PaintEventHandler(this.Plot_Paint);
            this.plot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Plot_MouseDown);
            this.plot.MouseEnter += new System.EventHandler(this.Plot_MouseEnter);
            this.plot.MouseLeave += new System.EventHandler(this.Plot_MouseLeave);
            this.plot.MouseHover += new System.EventHandler(this.Plot_MouseHover);
            this.plot.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Plot_MouseMove);
            this.plot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Plot_MouseUp);
            // 
            // toolTip
            // 
            this.toolTip.Active = false;
            this.toolTip.AutomaticDelay = 200;
            this.toolTip.IsBalloon = true;
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
        private System.Windows.Forms.PictureBox plot;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
