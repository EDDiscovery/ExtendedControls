/*
 * Copyright © 2016-2019 EDDiscovery development team
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
    partial class InfoForm
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
            this.buttonOK = new ExtendedControls.ExtButton();
            this.textBoxInfo = new ExtendedControls.ExtRichTextBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelCaption = new System.Windows.Forms.Label();
            this.panel_close = new ExtendedControls.ExtButtonDrawn();
            this.panel_minimize = new ExtendedControls.ExtButtonDrawn();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.buttonAcknowledge = new ExtendedControls.ExtButton();
            this.statusStripBottom = new ExtendedControls.ExtStatusStrip();
            this.contextMenuStrip.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(697, 5);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "%OK%";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxInfo.ContextMenuStrip = this.contextMenuStrip;
            this.textBoxInfo.DetectUrls = true;
            this.textBoxInfo.HideScrollBar = true;
            this.textBoxInfo.Location = new System.Drawing.Point(304, 115);
            this.textBoxInfo.Margin = new System.Windows.Forms.Padding(10);
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.ReadOnly = false;
            this.textBoxInfo.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang2057{\\fonttbl{\\f0\\fnil\\fcharset0 " +
    "Microsoft Sans Serif;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\par" +
    "d\\f0\\fs17\\par\r\n}\r\n";
            this.textBoxInfo.ShowLineCount = false;
            this.textBoxInfo.Size = new System.Drawing.Size(273, 228);
            this.textBoxInfo.TabIndex = 2;
            this.textBoxInfo.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.textBoxInfo.TextBoxForeColor = System.Drawing.SystemColors.ControlText;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopy,
            this.copyAllToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(181, 70);
            // 
            // toolStripMenuItemCopy
            // 
            this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
            this.toolStripMenuItemCopy.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemCopy.Text = "Copy";
            this.toolStripMenuItemCopy.Click += new System.EventHandler(this.toolStripMenuItemCopy_Click);
            // 
            // copyAllToolStripMenuItem
            // 
            this.copyAllToolStripMenuItem.Name = "copyAllToolStripMenuItem";
            this.copyAllToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyAllToolStripMenuItem.Text = "Copy All";
            this.copyAllToolStripMenuItem.Click += new System.EventHandler(this.copyAllToolStripMenuItem_Click);
            // 
            // labelCaption
            // 
            this.labelCaption.AutoSize = true;
            this.labelCaption.Location = new System.Drawing.Point(3, 3);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(43, 13);
            this.labelCaption.TabIndex = 6;
            this.labelCaption.Text = "<code>";
            this.labelCaption.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.labelCaption.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // panel_close
            // 
            this.panel_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_close.AutoEllipsis = false;
            this.panel_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel_close.BorderColor = System.Drawing.Color.Orange;
            this.panel_close.BorderWidth = 1;
            this.panel_close.Image = null;
            this.panel_close.ImageSelected = ExtendedControls.ExtButtonDrawn.ImageType.Close;
            this.panel_close.Location = new System.Drawing.Point(796, 3);
            this.panel_close.MouseOverColor = System.Drawing.Color.White;
            this.panel_close.MouseSelectedColor = System.Drawing.Color.Green;
            this.panel_close.MouseSelectedColorEnable = true;
            this.panel_close.Name = "panel_close";
            this.panel_close.Padding = new System.Windows.Forms.Padding(6);
            this.panel_close.ButtonDisabledScaling = 0.25F;
            this.panel_close.Selectable = false;
            this.panel_close.Size = new System.Drawing.Size(24, 24);
            this.panel_close.TabIndex = 29;
            this.panel_close.TabStop = false;
            this.panel_close.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.panel_close.UseMnemonic = true;
            this.panel_close.Click += new System.EventHandler(this.panel_close_Click);
            // 
            // panel_minimize
            // 
            this.panel_minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_minimize.AutoEllipsis = false;
            this.panel_minimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel_minimize.BorderColor = System.Drawing.Color.Orange;
            this.panel_minimize.BorderWidth = 1;
            this.panel_minimize.Image = null;
            this.panel_minimize.ImageSelected = ExtendedControls.ExtButtonDrawn.ImageType.Minimize;
            this.panel_minimize.Location = new System.Drawing.Point(766, 3);
            this.panel_minimize.MouseOverColor = System.Drawing.Color.White;
            this.panel_minimize.MouseSelectedColor = System.Drawing.Color.Green;
            this.panel_minimize.MouseSelectedColorEnable = true;
            this.panel_minimize.Name = "panel_minimize";
            this.panel_minimize.Padding = new System.Windows.Forms.Padding(6);
            this.panel_minimize.ButtonDisabledScaling = 0.25F;
            this.panel_minimize.Selectable = false;
            this.panel_minimize.Size = new System.Drawing.Size(24, 24);
            this.panel_minimize.TabIndex = 28;
            this.panel_minimize.TabStop = false;
            this.panel_minimize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.panel_minimize.UseMnemonic = true;
            this.panel_minimize.Click += new System.EventHandler(this.panel_minimize_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.panel_minimize);
            this.panelTop.Controls.Add(this.labelCaption);
            this.panelTop.Controls.Add(this.panel_close);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(824, 30);
            this.panelTop.TabIndex = 30;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.panelTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.buttonAcknowledge);
            this.panelBottom.Controls.Add(this.buttonOK);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 538);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(824, 34);
            this.panelBottom.TabIndex = 31;
            // 
            // buttonAcknowledge
            // 
            this.buttonAcknowledge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAcknowledge.Location = new System.Drawing.Point(529, 5);
            this.buttonAcknowledge.Name = "buttonAcknowledge";
            this.buttonAcknowledge.Size = new System.Drawing.Size(140, 23);
            this.buttonAcknowledge.TabIndex = 1;
            this.buttonAcknowledge.Text = "Acknowledge";
            this.buttonAcknowledge.UseVisualStyleBackColor = true;
            this.buttonAcknowledge.Click += new System.EventHandler(this.buttonAcknowledge_Click);
            // 
            // statusStripBottom
            // 
            this.statusStripBottom.Location = new System.Drawing.Point(0, 572);
            this.statusStripBottom.Name = "statusStripBottom";
            this.statusStripBottom.Size = new System.Drawing.Size(824, 22);
            this.statusStripBottom.TabIndex = 27;
            this.statusStripBottom.Text = "statusStripCustom1";
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 594);
            this.Controls.Add(this.textBoxInfo);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.statusStripBottom);
            this.Name = "InfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "<code>";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InfoForm_FormClosing);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.InfoForm_Layout);
            this.contextMenuStrip.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ExtendedControls.ExtButton buttonOK;
        private ExtendedControls.ExtRichTextBox textBoxInfo;
        private System.Windows.Forms.Label labelCaption;
        private ExtButtonDrawn panel_close;
        private ExtButtonDrawn panel_minimize;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBottom;
        private ExtStatusStrip statusStripBottom;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
        private ExtButton buttonAcknowledge;
        private System.Windows.Forms.ToolStripMenuItem copyAllToolStripMenuItem;
    }
}