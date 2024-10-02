/*
 * Copyright © 2016 - 2019 EDDiscovery development team
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this
 * file except in compliance with the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software distributed under
 * the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
 * ANY KIND, either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 * 
 *
 */
namespace ExtendedControls
{
    partial class TabStrip
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
            this.panelStrip = new System.Windows.Forms.Panel();
            this.labelControlText = new System.Windows.Forms.Label();
            this.pimageListSelection = new ExtendedControls.ExtButtonDrawn();
            this.extButtonDrawnHelp = new ExtendedControls.ExtButtonDrawn();
            this.pimagePopOutIcon = new ExtendedControls.ExtButtonDrawn();
            this.panelArrowRight = new System.Windows.Forms.Panel();
            this.panelArrowLeft = new System.Windows.Forms.Panel();
            this.pimageSelectedIcon = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemPopOut = new System.Windows.Forms.ToolStripMenuItem();
            this.panelStrip.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelStrip
            // 
            this.panelStrip.AutoSize = true;
            this.panelStrip.Controls.Add(this.labelControlText);
            this.panelStrip.Controls.Add(this.pimageListSelection);
            this.panelStrip.Controls.Add(this.extButtonDrawnHelp);
            this.panelStrip.Controls.Add(this.pimagePopOutIcon);
            this.panelStrip.Controls.Add(this.panelArrowRight);
            this.panelStrip.Controls.Add(this.panelArrowLeft);
            this.panelStrip.Controls.Add(this.pimageSelectedIcon);
            this.panelStrip.Controls.Add(this.labelTitle);
            this.panelStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStrip.Location = new System.Drawing.Point(0, 322);
            this.panelStrip.Name = "panelStrip";
            this.panelStrip.Size = new System.Drawing.Size(562, 30);
            this.panelStrip.TabIndex = 0;
            this.panelStrip.MouseEnter += new System.EventHandler(this.MouseEnterPanelObjects);
            this.panelStrip.MouseLeave += new System.EventHandler(this.MouseLeavePanelObjects);
            // 
            // labelControlText
            // 
            this.labelControlText.AutoSize = true;
            this.labelControlText.Location = new System.Drawing.Point(440, 8);
            this.labelControlText.Name = "labelControlText";
            this.labelControlText.Size = new System.Drawing.Size(43, 13);
            this.labelControlText.TabIndex = 4;
            this.labelControlText.Text = "<code>";
            this.labelControlText.MouseEnter += new System.EventHandler(this.MouseEnterPanelObjects);
            this.labelControlText.MouseLeave += new System.EventHandler(this.MouseLeavePanelObjects);
            // 
            // pimageListSelection
            // 
            this.pimageListSelection.AutoEllipsis = false;
            this.pimageListSelection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pimageListSelection.Image = global::ExtendedControls.Properties.Resources.panels;
            this.pimageListSelection.ImageSelected = ExtendedControls.ExtButtonDrawn.ImageType.None;
            this.pimageListSelection.Location = new System.Drawing.Point(210, 3);
            this.pimageListSelection.MouseOverColor = System.Drawing.Color.White;
            this.pimageListSelection.MouseSelectedColor = System.Drawing.Color.Green;
            this.pimageListSelection.MouseSelectedColorEnable = true;
            this.pimageListSelection.Name = "pimageListSelection";
            this.pimageListSelection.PanelDisabledScaling = 0.25F;
            this.pimageListSelection.Selectable = true;
            this.pimageListSelection.Size = new System.Drawing.Size(24, 24);
            this.pimageListSelection.TabIndex = 3;
            this.pimageListSelection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pimageListSelection.UseMnemonic = true;
            this.pimageListSelection.Click += new System.EventHandler(this.drawnPanelListSelection_Click);
            this.pimageListSelection.MouseEnter += new System.EventHandler(this.MouseEnterPanelObjects);
            this.pimageListSelection.MouseLeave += new System.EventHandler(this.MouseLeavePanelObjects);
            // 
            // extButtonDrawnHelp
            // 
            this.extButtonDrawnHelp.AutoEllipsis = false;
            this.extButtonDrawnHelp.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.extButtonDrawnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.extButtonDrawnHelp.Dock = System.Windows.Forms.DockStyle.Right;
            this.extButtonDrawnHelp.Image = global::ExtendedControls.Properties.Resources.help;
            this.extButtonDrawnHelp.ImageSelected = ExtendedControls.ExtButtonDrawn.ImageType.None;
            this.extButtonDrawnHelp.Location = new System.Drawing.Point(538, 0);
            this.extButtonDrawnHelp.MouseOverColor = System.Drawing.Color.White;
            this.extButtonDrawnHelp.MouseSelectedColor = System.Drawing.Color.Green;
            this.extButtonDrawnHelp.MouseSelectedColorEnable = true;
            this.extButtonDrawnHelp.Name = "extButtonDrawnHelp";
            this.extButtonDrawnHelp.PanelDisabledScaling = 0.25F;
            this.extButtonDrawnHelp.Selectable = true;
            this.extButtonDrawnHelp.Size = new System.Drawing.Size(24, 30);
            this.extButtonDrawnHelp.TabIndex = 3;
            this.extButtonDrawnHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.extButtonDrawnHelp.UseMnemonic = true;
            this.extButtonDrawnHelp.Click += new System.EventHandler(this.extButtonDrawnHelp_Click);
            this.extButtonDrawnHelp.MouseEnter += new System.EventHandler(this.MouseEnterPanelObjects);
            this.extButtonDrawnHelp.MouseLeave += new System.EventHandler(this.MouseLeavePanelObjects);
            // 
            // pimagePopOutIcon
            // 
            this.pimagePopOutIcon.AutoEllipsis = false;
            this.pimagePopOutIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pimagePopOutIcon.Image = global::ExtendedControls.Properties.Resources.popout;
            this.pimagePopOutIcon.ImageSelected = ExtendedControls.ExtButtonDrawn.ImageType.None;
            this.pimagePopOutIcon.Location = new System.Drawing.Point(161, 3);
            this.pimagePopOutIcon.MouseOverColor = System.Drawing.Color.White;
            this.pimagePopOutIcon.MouseSelectedColor = System.Drawing.Color.Green;
            this.pimagePopOutIcon.MouseSelectedColorEnable = true;
            this.pimagePopOutIcon.Name = "pimagePopOutIcon";
            this.pimagePopOutIcon.PanelDisabledScaling = 0.25F;
            this.pimagePopOutIcon.Selectable = true;
            this.pimagePopOutIcon.Size = new System.Drawing.Size(24, 24);
            this.pimagePopOutIcon.TabIndex = 3;
            this.pimagePopOutIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.pimagePopOutIcon, "Click to pop out the current panel into another window");
            this.pimagePopOutIcon.UseMnemonic = true;
            this.pimagePopOutIcon.Click += new System.EventHandler(this.panelPopOut_Click);
            this.pimagePopOutIcon.MouseEnter += new System.EventHandler(this.MouseEnterPanelObjects);
            this.pimagePopOutIcon.MouseLeave += new System.EventHandler(this.MouseLeavePanelObjects);
            // 
            // panelArrowRight
            // 
            this.panelArrowRight.BackgroundImage = global::ExtendedControls.Properties.Resources.ArrowRight;
            this.panelArrowRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelArrowRight.Location = new System.Drawing.Point(304, 4);
            this.panelArrowRight.Name = "panelArrowRight";
            this.panelArrowRight.Size = new System.Drawing.Size(12, 20);
            this.panelArrowRight.TabIndex = 2;
            this.toolTip1.SetToolTip(this.panelArrowRight, "Click to scroll the list right");
            this.panelArrowRight.Visible = false;
            this.panelArrowRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelArrowRight_MouseDown);
            this.panelArrowRight.MouseEnter += new System.EventHandler(this.MouseEnterPanelObjects);
            this.panelArrowRight.MouseLeave += new System.EventHandler(this.MouseLeavePanelObjects);
            this.panelArrowRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelArrowRight_MouseUp);
            // 
            // panelArrowLeft
            // 
            this.panelArrowLeft.BackgroundImage = global::ExtendedControls.Properties.Resources.ArrowLeft;
            this.panelArrowLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelArrowLeft.Location = new System.Drawing.Point(272, 4);
            this.panelArrowLeft.Name = "panelArrowLeft";
            this.panelArrowLeft.Size = new System.Drawing.Size(12, 20);
            this.panelArrowLeft.TabIndex = 2;
            this.toolTip1.SetToolTip(this.panelArrowLeft, "Click to scroll the list left");
            this.panelArrowLeft.Visible = false;
            this.panelArrowLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelArrowLeft_MouseDown);
            this.panelArrowLeft.MouseEnter += new System.EventHandler(this.MouseEnterPanelObjects);
            this.panelArrowLeft.MouseLeave += new System.EventHandler(this.MouseLeavePanelObjects);
            this.panelArrowLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelArrowLeft_MouseUp);
            // 
            // pimageSelectedIcon
            // 
            this.pimageSelectedIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pimageSelectedIcon.Location = new System.Drawing.Point(3, 3);
            this.pimageSelectedIcon.Name = "pimageSelectedIcon";
            this.pimageSelectedIcon.Size = new System.Drawing.Size(24, 24);
            this.pimageSelectedIcon.TabIndex = 1;
            this.pimageSelectedIcon.MouseEnter += new System.EventHandler(this.MouseEnterPanelObjects);
            this.pimageSelectedIcon.MouseLeave += new System.EventHandler(this.MouseLeavePanelObjects);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(33, 8);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(43, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "<code>";
            this.labelTitle.MouseEnter += new System.EventHandler(this.MouseEnterPanelObjects);
            this.labelTitle.MouseLeave += new System.EventHandler(this.MouseLeavePanelObjects);
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemPopOut});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 26);
            this.contextMenuStrip1.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStrip1_Closed);
            this.contextMenuStrip1.Opened += new System.EventHandler(this.contextMenuStrip1_Opened);
            // 
            // toolStripMenuItemPopOut
            // 
            this.toolStripMenuItemPopOut.Name = "toolStripMenuItemPopOut";
            this.toolStripMenuItemPopOut.Size = new System.Drawing.Size(118, 22);
            this.toolStripMenuItemPopOut.Text = "Pop Out";
            this.toolStripMenuItemPopOut.Click += new System.EventHandler(this.toolStripMenuItemPopOut_Click);
            // 
            // TabStrip
            // 
            this.Controls.Add(this.panelStrip);
            this.Name = "TabStrip";
            this.Size = new System.Drawing.Size(562, 352);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.TabStrip_Layout);
            this.Resize += new System.EventHandler(this.TabStrip_Resize);
            this.panelStrip.ResumeLayout(false);
            this.panelStrip.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelStrip;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel pimageSelectedIcon;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panelArrowRight;
        private System.Windows.Forms.Panel panelArrowLeft;
        private ExtendedControls.ExtButtonDrawn pimagePopOutIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPopOut;
        private System.Windows.Forms.Label labelControlText;
        private ExtButtonDrawn pimageListSelection;
        private ExtButtonDrawn extButtonDrawnHelp;
    }
}
