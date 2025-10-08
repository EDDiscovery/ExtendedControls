namespace TestExtendedControls
{
    partial class TestDrakeDockingPads
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
            this.buttonPlus = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button0 = new System.Windows.Forms.Button();
            this.fleetCarrierDockingPads1 = new ExtendedControls.FleetCarrierDockingPads();
            this.buttonFlip = new System.Windows.Forms.Button();
            this.panelControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.button11);
            this.panelControl.Controls.Add(this.button13);
            this.panelControl.Controls.Add(this.buttonFlip);
            this.panelControl.Controls.Add(this.buttonPlus);
            this.panelControl.Controls.Add(this.button1);
            this.panelControl.Controls.Add(this.button0);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(1227, 100);
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
            // buttonPlus
            // 
            this.buttonPlus.Location = new System.Drawing.Point(184, 13);
            this.buttonPlus.Name = "buttonPlus";
            this.buttonPlus.Size = new System.Drawing.Size(75, 23);
            this.buttonPlus.TabIndex = 1;
            this.buttonPlus.Text = "+";
            this.buttonPlus.UseVisualStyleBackColor = true;
            this.buttonPlus.Click += new System.EventHandler(this.buttonPlus_Click);
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
            // fleetCarrierDockingPads1
            // 
            this.fleetCarrierDockingPads1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fleetCarrierDockingPads1.ForeColor = System.Drawing.Color.Black;
            this.fleetCarrierDockingPads1.LargePad = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.fleetCarrierDockingPads1.Location = new System.Drawing.Point(0, 100);
            this.fleetCarrierDockingPads1.MediumPad = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.fleetCarrierDockingPads1.Name = "fleetCarrierDockingPads1";
            this.fleetCarrierDockingPads1.NonSelected = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.fleetCarrierDockingPads1.SelectedIndex = 0;
            this.fleetCarrierDockingPads1.Size = new System.Drawing.Size(1227, 598);
            this.fleetCarrierDockingPads1.SmallPad = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.fleetCarrierDockingPads1.TabIndex = 2;
            this.fleetCarrierDockingPads1.Text = "fleetCarrierDockingPads1";
            // 
            // buttonFlip
            // 
            this.buttonFlip.Location = new System.Drawing.Point(285, 13);
            this.buttonFlip.Name = "buttonFlip";
            this.buttonFlip.Size = new System.Drawing.Size(75, 23);
            this.buttonFlip.TabIndex = 1;
            this.buttonFlip.Text = "Flip";
            this.buttonFlip.UseVisualStyleBackColor = true;
            this.buttonFlip.Click += new System.EventHandler(this.buttonFlip_Click);
            // 
            // TestDrakeDockingPads
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 698);
            this.Controls.Add(this.fleetCarrierDockingPads1);
            this.Controls.Add(this.panelControl);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TestDrakeDockingPads";
            this.Text = "Docking Pads";
            this.panelControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button0;
        private System.Windows.Forms.Button button11;
        private ExtendedControls.FleetCarrierDockingPads fleetCarrierDockingPads1;
        private System.Windows.Forms.Button buttonPlus;
        private System.Windows.Forms.Button buttonFlip;
    }
}