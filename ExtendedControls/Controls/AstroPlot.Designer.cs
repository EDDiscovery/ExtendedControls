namespace ExtendedControls.Controls
{
    partial class AstroPlot
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
            this.systemLabel = new ExtendedControls.ExtLabel();
            this.plotCanvas = new ExtendedControls.ExtPictureBox();
            this.plotObjectsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.plotCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // systemLabel
            // 
            this.systemLabel.AutoSize = true;
            this.systemLabel.Location = new System.Drawing.Point(8, 8);
            this.systemLabel.Name = "systemLabel";
            this.systemLabel.Size = new System.Drawing.Size(53, 13);
            this.systemLabel.TabIndex = 1;
            this.systemLabel.Text = "extLabel1";
            this.systemLabel.TextBackColor = System.Drawing.Color.Transparent;
            // 
            // plotCanvas
            // 
            this.plotCanvas.BackColor = System.Drawing.Color.Black;
            this.plotCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotCanvas.Location = new System.Drawing.Point(0, 0);
            this.plotCanvas.Name = "plotCanvas";
            this.plotCanvas.Size = new System.Drawing.Size(0, 0);
            this.plotCanvas.TabIndex = 0;
            this.plotCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.PlotCanvas_Paint);
            this.plotCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlotCanvas_MouseDown);
            this.plotCanvas.MouseLeave += new System.EventHandler(this.PlotCanvas_MouseLeave);
            this.plotCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PlotCanvas_MouseMove);
            this.plotCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PlotCanvas_MouseUp);
            // 
            // plotObjectsMenu
            // 
            this.plotObjectsMenu.Name = "plotObjectsMenu";
            this.plotObjectsMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // AstroPlot
            // 
            this.Controls.Add(this.systemLabel);
            this.Controls.Add(this.plotCanvas);
            ((System.ComponentModel.ISupportInitialize)(this.plotCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExtPictureBox plotCanvas;
        private ExtLabel systemLabel;
        private System.Windows.Forms.ContextMenuStrip plotObjectsMenu;
    }
}
