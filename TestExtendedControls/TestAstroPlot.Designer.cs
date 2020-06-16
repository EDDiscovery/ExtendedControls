namespace DialogTest
{
    partial class TestAstroPlot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestAstroPlot));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.extAstroPlotTest = new ExtendedControls.Controls.ExtAstroPlot();
            this.extPanelDropDown1 = new ExtendedControls.ExtPanelDropDown();
            this.extButtonClear = new ExtendedControls.ExtButton();
            this.extButtonOrrery = new ExtendedControls.ExtButton();
            this.extButtonTravel = new ExtendedControls.ExtButton();
            this.extButtonLocal = new ExtendedControls.ExtButton();
            this.extPanelDropDown1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // extAstroPlotTest
            // 
            this.extAstroPlotTest.AxesLength = 100;
            this.extAstroPlotTest.AxesThickness = 1;
            this.extAstroPlotTest.AxesWidget = true;
            this.extAstroPlotTest.Azimuth = 0.3D;
            this.extAstroPlotTest.BackColor = System.Drawing.Color.Black;
            this.extAstroPlotTest.BoundariesFrameThickness = 1;
            this.extAstroPlotTest.BoundariesRadius = 0.8D;
            this.extAstroPlotTest.BoundariesWidget = true;
            this.extAstroPlotTest.Camera = new double[] {
        -1.0163564521110631D,
        1.063872743980822D,
        -3.2856041068374195D};
            this.extAstroPlotTest.ContextMenuStrip = this.contextMenuStrip;
            this.extAstroPlotTest.CoordsCenter = new double[] {
        0D,
        0D,
        0D};
            this.extAstroPlotTest.Distance = 3.5999999999999988D;
            this.extAstroPlotTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extAstroPlotTest.Elevation = 0.3D;
            this.extAstroPlotTest.Focus = 900D;
            this.extAstroPlotTest.ForeColor = System.Drawing.Color.White;
            this.extAstroPlotTest.LargeDotSize = 9;
            this.extAstroPlotTest.Location = new System.Drawing.Point(0, 45);
            this.extAstroPlotTest.MediumDotSize = 6;
            this.extAstroPlotTest.MouseSensitivity_Movement = 150;
            this.extAstroPlotTest.MouseSensitivity_Wheel = 300D;
            this.extAstroPlotTest.Name = "extAstroPlotTest";
            this.extAstroPlotTest.Size = new System.Drawing.Size(406, 346);
            this.extAstroPlotTest.SmallDotSize = 3;
            this.extAstroPlotTest.TabIndex = 0;
            this.extAstroPlotTest.MouseDown += new System.Windows.Forms.MouseEventHandler(this.extAstroPlotTest_MouseDown);            
            // 
            // extPanelDropDown1
            // 
            this.extPanelDropDown1.BorderColor = System.Drawing.Color.Red;
            this.extPanelDropDown1.Controls.Add(this.extButtonClear);
            this.extPanelDropDown1.Controls.Add(this.extButtonOrrery);
            this.extPanelDropDown1.Controls.Add(this.extButtonTravel);
            this.extPanelDropDown1.Controls.Add(this.extButtonLocal);
            this.extPanelDropDown1.Dock = System.Windows.Forms.DockStyle.Top;
            this.extPanelDropDown1.FitToItemsHeight = false;
            this.extPanelDropDown1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extPanelDropDown1.GradientColorScaling = 0.5F;
            this.extPanelDropDown1.Items = ((System.Collections.Generic.List<string>)(resources.GetObject("extPanelDropDown1.Items")));
            this.extPanelDropDown1.Location = new System.Drawing.Point(0, 0);
            this.extPanelDropDown1.MouseOverBackgroundColor = System.Drawing.Color.Silver;
            this.extPanelDropDown1.Name = "extPanelDropDown1";
            this.extPanelDropDown1.ScrollBarButtonColor = System.Drawing.Color.LightGray;
            this.extPanelDropDown1.ScrollBarColor = System.Drawing.Color.LightGray;
            this.extPanelDropDown1.SelectedIndex = -1;
            this.extPanelDropDown1.SelectionBackColor = System.Drawing.Color.Gray;
            this.extPanelDropDown1.SelectionMarkColor = System.Drawing.Color.Yellow;
            this.extPanelDropDown1.SelectionSize = 8;
            this.extPanelDropDown1.Size = new System.Drawing.Size(406, 45);
            this.extPanelDropDown1.TabIndex = 1;
            // 
            // extButtonClear
            // 
            this.extButtonClear.Location = new System.Drawing.Point(348, 3);
            this.extButtonClear.Name = "extButtonClear";
            this.extButtonClear.Size = new System.Drawing.Size(55, 39);
            this.extButtonClear.TabIndex = 3;
            this.extButtonClear.Text = "Clear";
            this.extButtonClear.UseVisualStyleBackColor = true;
            this.extButtonClear.Click += new System.EventHandler(this.extButtonClear_Click);
            // 
            // extButtonOrrery
            // 
            this.extButtonOrrery.Location = new System.Drawing.Point(178, 3);
            this.extButtonOrrery.Name = "extButtonOrrery";
            this.extButtonOrrery.Size = new System.Drawing.Size(75, 39);
            this.extButtonOrrery.TabIndex = 2;
            this.extButtonOrrery.Text = "Orrery";
            this.extButtonOrrery.UseVisualStyleBackColor = true;
            this.extButtonOrrery.Click += new System.EventHandler(this.extButtonOrrery_Click);
            // 
            // extButtonTravel
            // 
            this.extButtonTravel.Location = new System.Drawing.Point(97, 3);
            this.extButtonTravel.Name = "extButtonTravel";
            this.extButtonTravel.Size = new System.Drawing.Size(75, 39);
            this.extButtonTravel.TabIndex = 1;
            this.extButtonTravel.Text = "Travel Map";
            this.extButtonTravel.UseVisualStyleBackColor = true;
            this.extButtonTravel.Click += new System.EventHandler(this.extButtonTravel_Click);
            // 
            // extButtonLocal
            // 
            this.extButtonLocal.Location = new System.Drawing.Point(3, 3);
            this.extButtonLocal.Name = "extButtonLocal";
            this.extButtonLocal.Size = new System.Drawing.Size(88, 39);
            this.extButtonLocal.TabIndex = 0;
            this.extButtonLocal.Text = "Local Systems";
            this.extButtonLocal.UseVisualStyleBackColor = true;
            this.extButtonLocal.Click += new System.EventHandler(this.extButtonLocal_Click);
            // 
            // TestAstroPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 391);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.extAstroPlotTest);
            this.Controls.Add(this.extPanelDropDown1);
            this.Name = "TestAstroPlot";
            this.Text = "TestAstroPlot";
            this.extPanelDropDown1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.Controls.ExtAstroPlot extAstroPlotTest;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private ExtendedControls.ExtPanelDropDown extPanelDropDown1;
        private ExtendedControls.ExtButton extButtonOrrery;
        private ExtendedControls.ExtButton extButtonTravel;
        private ExtendedControls.ExtButton extButtonLocal;
        private ExtendedControls.ExtButton extButtonClear;
    }
}