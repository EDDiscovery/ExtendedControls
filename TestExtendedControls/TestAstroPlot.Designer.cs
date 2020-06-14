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
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAxesWidgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showBoundariesFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.setCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sys00ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sys01ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sys02ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sys03ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sys04ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sys05ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extAstroPlotTest = new ExtendedControls.Controls.ExtAstroPlot();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem,
            this.showAxesWidgetToolStripMenuItem,
            this.showBoundariesFrameToolStripMenuItem,
            this.setCenterToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(202, 92);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // showAxesWidgetToolStripMenuItem
            // 
            this.showAxesWidgetToolStripMenuItem.Name = "showAxesWidgetToolStripMenuItem";
            this.showAxesWidgetToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.showAxesWidgetToolStripMenuItem.Text = "Show Axes Widget";
            // 
            // showBoundariesFrameToolStripMenuItem
            // 
            this.showBoundariesFrameToolStripMenuItem.Name = "showBoundariesFrameToolStripMenuItem";
            this.showBoundariesFrameToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.showBoundariesFrameToolStripMenuItem.Text = "Show Boundaries Frame";
            // 
            // setCenterToolStripMenuItem
            // 
            this.setCenterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sys00ToolStripMenuItem,
            this.sys01ToolStripMenuItem,
            this.sys02ToolStripMenuItem,
            this.sys03ToolStripMenuItem,
            this.sys04ToolStripMenuItem,
            this.sys05ToolStripMenuItem});
            this.setCenterToolStripMenuItem.Name = "setCenterToolStripMenuItem";
            this.setCenterToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.setCenterToolStripMenuItem.Text = "Set Center";
            // 
            // sys00ToolStripMenuItem
            // 
            this.sys00ToolStripMenuItem.Name = "sys00ToolStripMenuItem";
            this.sys00ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sys00ToolStripMenuItem.Text = "sys00";
            this.sys00ToolStripMenuItem.Click += new System.EventHandler(this.sys00ToolStripMenuItem_Click);
            // 
            // sys01ToolStripMenuItem
            // 
            this.sys01ToolStripMenuItem.Name = "sys01ToolStripMenuItem";
            this.sys01ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sys01ToolStripMenuItem.Text = "sys01";
            this.sys01ToolStripMenuItem.Click += new System.EventHandler(this.sys01ToolStripMenuItem_Click);
            // 
            // sys02ToolStripMenuItem
            // 
            this.sys02ToolStripMenuItem.Name = "sys02ToolStripMenuItem";
            this.sys02ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sys02ToolStripMenuItem.Text = "sys02";
            this.sys02ToolStripMenuItem.Click += new System.EventHandler(this.sys02ToolStripMenuItem_Click);
            // 
            // sys03ToolStripMenuItem
            // 
            this.sys03ToolStripMenuItem.Name = "sys03ToolStripMenuItem";
            this.sys03ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sys03ToolStripMenuItem.Text = "sys03";
            this.sys03ToolStripMenuItem.Click += new System.EventHandler(this.sys03ToolStripMenuItem_Click);
            // 
            // sys04ToolStripMenuItem
            // 
            this.sys04ToolStripMenuItem.Name = "sys04ToolStripMenuItem";
            this.sys04ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sys04ToolStripMenuItem.Text = "sys04";
            this.sys04ToolStripMenuItem.Click += new System.EventHandler(this.sys04ToolStripMenuItem_Click);
            // 
            // sys05ToolStripMenuItem
            // 
            this.sys05ToolStripMenuItem.Name = "sys05ToolStripMenuItem";
            this.sys05ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sys05ToolStripMenuItem.Text = "sys05";
            this.sys05ToolStripMenuItem.Click += new System.EventHandler(this.sys05ToolStripMenuItem_Click);
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
            this.extAstroPlotTest.Location = new System.Drawing.Point(0, 0);
            this.extAstroPlotTest.MediumDotSize = 6;
            this.extAstroPlotTest.MouseSensitivity_Movement = 150;
            this.extAstroPlotTest.MouseSensitivity_Wheel = 300D;
            this.extAstroPlotTest.Name = "extAstroPlotTest";
            this.extAstroPlotTest.Size = new System.Drawing.Size(520, 494);
            this.extAstroPlotTest.SmallDotSize = 3;
            this.extAstroPlotTest.TabIndex = 0;
            this.extAstroPlotTest.MouseDown += new System.Windows.Forms.MouseEventHandler(this.extAstroPlotTest_MouseDown);
            // 
            // TestAstroPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 494);
            this.Controls.Add(this.extAstroPlotTest);
            this.Name = "TestAstroPlot";
            this.Text = "TestAstroPlot";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TestAstroPlot_MouseDown);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.Controls.ExtAstroPlot extAstroPlotTest;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAxesWidgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showBoundariesFrameToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem setCenterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sys00ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sys01ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sys02ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sys03ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sys04ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sys05ToolStripMenuItem;
    }
}