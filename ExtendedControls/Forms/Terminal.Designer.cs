
namespace ExtendedControls
{
    partial class Terminal
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
            this.panelTermWindow = new ExtendedControls.PanelTerminal();
            this.SuspendLayout();
            // 
            // panelTermWindow
            // 
            this.panelTermWindow.Location = new System.Drawing.Point(0, 0);
            this.panelTermWindow.Name = "panelTermWindow";
            this.panelTermWindow.Size = new System.Drawing.Size(200, 100);
            this.panelTermWindow.TabIndex = 0;
            // 
            // Terminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelTermWindow);
            this.Name = "Terminal";
            this.Size = new System.Drawing.Size(977, 602);
            this.ResumeLayout(false);

        }

        #endregion

        private PanelTerminal panelTermWindow;
    }
}
