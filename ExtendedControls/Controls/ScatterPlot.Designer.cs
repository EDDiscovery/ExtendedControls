﻿namespace ExtendedControls.Controls
{
    partial class ExtScatterPlot
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
            this.SuspendLayout();
            // 
            // ExtScatterPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Name = "ExtScatterPlot";
            this.SizeChanged += new System.EventHandler(this.ExtScatterPlot_SizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ExtScatterPlot_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ExtScatterPlot_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ExtScatterPlot_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
    }
}
