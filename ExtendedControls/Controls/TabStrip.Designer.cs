﻿/*
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
 * EDDiscovery is not affiliated with Frontier Developments plc.
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
            this.panelListSelection = new ExtendedControls.ExtButtonDrawn();
            this.panelPopOutIcon = new ExtendedControls.ExtButtonDrawn();
            this.panelArrowRight = new System.Windows.Forms.Panel();
            this.panelArrowLeft = new System.Windows.Forms.Panel();
            this.panelSelectedIcon = new System.Windows.Forms.Panel();
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
            this.panelStrip.Controls.Add(this.panelListSelection);
            this.panelStrip.Controls.Add(this.panelPopOutIcon);
            this.panelStrip.Controls.Add(this.panelArrowRight);
            this.panelStrip.Controls.Add(this.panelArrowLeft);
            this.panelStrip.Controls.Add(this.panelSelectedIcon);
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
            // panelListSelection
            // 
            this.panelListSelection.AutoEllipsis = false;
            this.panelListSelection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelListSelection.Image = global::ExtendedControls.Properties.Resources.panels;
            this.panelListSelection.ImageSelected = ExtendedControls.ExtButtonDrawn.ImageType.None;
            this.panelListSelection.Location = new System.Drawing.Point(210, 3);
            this.panelListSelection.MouseOverColor = System.Drawing.Color.White;
            this.panelListSelection.MouseSelectedColor = System.Drawing.Color.Green;
            this.panelListSelection.MouseSelectedColorEnable = true;
            this.panelListSelection.Name = "panelListSelection";
            this.panelListSelection.PanelDisabledScaling = 0.25F;
            this.panelListSelection.Selectable = true;
            this.panelListSelection.Size = new System.Drawing.Size(24, 24);
            this.panelListSelection.TabIndex = 3;
            this.panelListSelection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.panelListSelection.UseMnemonic = true;
            this.panelListSelection.Click += new System.EventHandler(this.drawnPanelListSelection_Click);
            this.panelListSelection.MouseEnter += new System.EventHandler(this.MouseEnterPanelObjects);
            this.panelListSelection.MouseLeave += new System.EventHandler(this.MouseLeavePanelObjects);
            // 
            // panelPopOutIcon
            // 
            this.panelPopOutIcon.AutoEllipsis = false;
            this.panelPopOutIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelPopOutIcon.Image = global::ExtendedControls.Properties.Resources.popout;
            this.panelPopOutIcon.ImageSelected = ExtendedControls.ExtButtonDrawn.ImageType.None;
            this.panelPopOutIcon.Location = new System.Drawing.Point(161, 3);
            this.panelPopOutIcon.MouseOverColor = System.Drawing.Color.White;
            this.panelPopOutIcon.MouseSelectedColor = System.Drawing.Color.Green;
            this.panelPopOutIcon.MouseSelectedColorEnable = true;
            this.panelPopOutIcon.Name = "panelPopOutIcon";
            this.panelPopOutIcon.PanelDisabledScaling = 0.25F;
            this.panelPopOutIcon.Selectable = true;
            this.panelPopOutIcon.Size = new System.Drawing.Size(24, 24);
            this.panelPopOutIcon.TabIndex = 3;
            this.panelPopOutIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.panelPopOutIcon, "Click to pop out the current panel into another window");
            this.panelPopOutIcon.UseMnemonic = true;
            this.panelPopOutIcon.Click += new System.EventHandler(this.panelPopOut_Click);
            this.panelPopOutIcon.MouseEnter += new System.EventHandler(this.MouseEnterPanelObjects);
            this.panelPopOutIcon.MouseLeave += new System.EventHandler(this.MouseLeavePanelObjects);
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
            // panelSelectedIcon
            // 
            this.panelSelectedIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelSelectedIcon.Location = new System.Drawing.Point(3, 3);
            this.panelSelectedIcon.Name = "panelSelectedIcon";
            this.panelSelectedIcon.Size = new System.Drawing.Size(24, 24);
            this.panelSelectedIcon.TabIndex = 1;
            this.panelSelectedIcon.MouseEnter += new System.EventHandler(this.MouseEnterPanelObjects);
            this.panelSelectedIcon.MouseLeave += new System.EventHandler(this.MouseLeavePanelObjects);
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
        private System.Windows.Forms.Panel panelSelectedIcon;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panelArrowRight;
        private System.Windows.Forms.Panel panelArrowLeft;
        private ExtendedControls.ExtButtonDrawn panelPopOutIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPopOut;
        private System.Windows.Forms.Label labelControlText;
        private ExtButtonDrawn panelListSelection;
    }
}
