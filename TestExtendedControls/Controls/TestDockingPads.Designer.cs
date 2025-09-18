namespace TestExtendedControls
{
    partial class TestDockingPads
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
            this.panelControl = new System.Windows.Forms.Panel();
            this.button11 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button0 = new System.Windows.Forms.Button();
            this.dockingPads1 = new ExtendedControls.DockingPads();
            this.panelControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.button11);
            this.panelControl.Controls.Add(this.button13);
            this.panelControl.Controls.Add(this.button1);
            this.panelControl.Controls.Add(this.button0);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(1085, 100);
            this.panelControl.TabIndex = 1;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(22, 52);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 1;
            this.button11.Text = "11";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(103, 52);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(75, 23);
            this.button13.TabIndex = 1;
            this.button13.Text = "13";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(103, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button0
            // 
            this.button0.Location = new System.Drawing.Point(22, 13);
            this.button0.Name = "button0";
            this.button0.Size = new System.Drawing.Size(75, 23);
            this.button0.TabIndex = 0;
            this.button0.Text = "0";
            this.button0.UseVisualStyleBackColor = true;
            this.button0.Click += new System.EventHandler(this.button0_Click);
            // 
            // dockingPads1
            // 
            this.dockingPads1.BorderColor = System.Drawing.Color.Black;
            this.dockingPads1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockingPads1.ForeColor = System.Drawing.Color.Black;
            this.dockingPads1.LargePad = System.Drawing.Color.Red;
            this.dockingPads1.Location = new System.Drawing.Point(0, 100);
            this.dockingPads1.MediumPad = System.Drawing.Color.Yellow;
            this.dockingPads1.Name = "dockingPads1";
            this.dockingPads1.NonSelectedIntensity = 0.4F;
            this.dockingPads1.SelectedIndex = 0;
            this.dockingPads1.Size = new System.Drawing.Size(1085, 764);
            this.dockingPads1.SmallPad = System.Drawing.Color.Blue;
            this.dockingPads1.TabIndex = 0;
            this.dockingPads1.Text = "dockingPads1";
            // 
            // TestDockingPads
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 864);
            this.Controls.Add(this.dockingPads1);
            this.Controls.Add(this.panelControl);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TestDockingPads";
            this.Text = "Docking Pads";
            this.panelControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.DockingPads dockingPads1;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button0;
        private System.Windows.Forms.Button button11;
    }
}