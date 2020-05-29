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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.picturePlot = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picturePlot)).BeginInit();
            this.SuspendLayout();
            // 
            // picturePlot
            // 
            this.picturePlot.BackColor = System.Drawing.Color.Transparent;
            this.picturePlot.Location = new System.Drawing.Point(17, 15);
            this.picturePlot.Name = "picturePlot";
            this.picturePlot.Size = new System.Drawing.Size(224, 224);
            this.picturePlot.TabIndex = 0;
            this.picturePlot.TabStop = false;
            this.picturePlot.SizeChanged += new System.EventHandler(this.picturePlot_SizeChanged);
            this.picturePlot.Paint += new System.Windows.Forms.PaintEventHandler(this.picturePlot_Paint);
            this.picturePlot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picturePlot_MouseDown);
            this.picturePlot.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picturePlot_MouseMove);
            this.picturePlot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picturePlot_MouseUp);
            // 
            // ExtAstroPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.picturePlot);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ExtAstroPlot";
            this.Size = new System.Drawing.Size(256, 256);
            this.SizeChanged += new System.EventHandler(this.ExtAstroPlot_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.picturePlot)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox picturePlot;
    }
}
