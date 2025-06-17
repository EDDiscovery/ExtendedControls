
namespace TestExtendedControls
{
    partial class TestChart
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extButton1 = new ExtendedControls.ExtButton();
            this.extButton2 = new ExtendedControls.ExtButton();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fredToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(96, 26);
            // 
            // fredToolStripMenuItem
            // 
            this.fredToolStripMenuItem.Name = "fredToolStripMenuItem";
            this.fredToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.fredToolStripMenuItem.Text = "fred";
            // 
            // extButton1
            // 
            this.extButton1.Location = new System.Drawing.Point(12, 12);
            this.extButton1.Name = "extButton1";
            this.extButton1.Size = new System.Drawing.Size(75, 23);
            this.extButton1.TabIndex = 1;
            this.extButton1.Text = "Pos1";
            this.extButton1.UseVisualStyleBackColor = true;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click);
            // 
            // extButton2
            // 
            this.extButton2.Location = new System.Drawing.Point(12, 53);
            this.extButton2.Name = "extButton2";
            this.extButton2.Size = new System.Drawing.Size(75, 23);
            this.extButton2.TabIndex = 1;
            this.extButton2.Text = "Pos2";
            this.extButton2.UseVisualStyleBackColor = true;
            this.extButton2.Click += new System.EventHandler(this.extButton2_Click);
            // 
            // TestChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1407, 965);
            this.Controls.Add(this.extButton2);
            this.Controls.Add(this.extButton1);
            this.Name = "TestChart";
            this.Text = "TestOldSplitter";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fredToolStripMenuItem;
        private ExtendedControls.ExtButton extButton1;
        private ExtendedControls.ExtButton extButton2;
    }
}