/*
 * Copyright 2017-2024 EDDiscovery development team
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
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BaseUtils;

namespace ExtendedConditionsForms
{ 
    public partial class VariablesForm : ExtendedControls.DraggableForm
    {
        public Variables Result;      // only on OK
        public Dictionary<string, string> ResultAltOPs;

        public VariablesForm()
        {
            groups = new List<Group>();
            InitializeComponent();

        }

        // altops, if given, describes the operator of each variable.
        public void Init(string title, Icon ic, Variables vbs , Dictionary<string, string> altops = null,
                                                                bool showatleastoneentry = false ,
                                                                bool allowadd = false, 
                                                                bool allownoexpand = false, 
                                                                bool allowmultipleentries = true)
        {

            this.Icon = ic;

            var enumlist = new Enum[] { };
            BaseUtils.TranslatorMkII.Instance.TranslateControls(this);

            bool winborder = ExtendedControls.Theme.Current?.ApplyDialog(this) ?? true;
            statusStripCustom.Visible = panelTop.Visible = panelTop.Enabled = !winborder;
            this.Text = label_index.Text = title;

            showadd = allowadd;
            shownoexpand = allownoexpand;
            this.allowmultiple = allowmultipleentries;

            extPanelVertScrollWithBar.LargeChange = extPanelVertScrollWithBar.SmallChange = 32;

            if (vbs != null)
            {
                foreach (string key in vbs.NameEnumuerable)
                {
                    CreateEntry(key,vbs[key], (altops!= null) ? altops[key] : "=");
                }
            }

            if ( groups.Count == 0 && showatleastoneentry )
            {
                CreateEntry("", "", "=");
            }

            if (groups.Count >= 1)
                groups[0].var.Focus();
        }

        private Group CreateEntry(string var, string value, string op)
        {
            Group g = new Group();

            g.panel = new Panel();
            g.panel.BorderStyle = BorderStyle.FixedSingle;

            g.var = new ExtendedControls.ExtTextBox();
            g.var.Size = new Size(Font.ScalePixels(240), Font.ScalePixels(32));
            g.var.Location = new Point(panelmargin, panelmargin);
            g.var.Text = var;
            g.panel.Controls.Add(g.var);
            toolTip1.SetToolTip(g.var, "Variable name");

            int nextpos = g.var.Right;

            if (shownoexpand || showadd)
            {
                g.op = new ExtendedControls.ExtComboBox();
                g.op.Size = new Size(Font.ScalePixels(75), Font.ScalePixels(32));
                g.op.Location = new Point(g.var.Right + 4, panelmargin);

                string ttip="";
                if (showadd && shownoexpand)
                {
                    g.op.Items.AddRange(new string[] { "=", "$=", "+=", "$+=" });
                    ttip = "= assign, expand, $= assign, no expansion, += add, expand, $+= add, no expansion";
                }
                else if (showadd)
                {
                    g.op.Items.AddRange(new string[] { "=", "+=" });
                    ttip = "= assign, expand, += add, expand";
                }
                else
                {
                    g.op.Items.AddRange(new string[] { "=", "$=" });
                    ttip = "= assign, expand, $= add, no expansion";
                }

                toolTip1.SetToolTip(g.op, ttip);
                toolTip1.SetToolTip(g.op.GetInternalSystemControl, ttip);

                if (g.op.Items.Contains(op))
                    g.op.SelectedItem = op;

                g.panel.Controls.Add(g.op);

                nextpos = g.op.Right;
            }

            g.value = new ExtendedControls.ExtRichTextBox();
            g.value.Location = new Point(nextpos + 4, panelmargin);
            g.value.Text = value.ReplaceEscapeControlChars();
            toolTip1.SetToolTip(g.value, "Variable value");
            g.panel.Controls.Add(g.value);
            g.value.TextBoxChanged += (obj, e) => {
                if (g.value.Lines.Length > g.lines )    // grow only
                {
                    int scrheight = Screen.FromControl(this).WorkingArea.Height;
                    if ( g.panel.Height < scrheight * 2 / 4 )
                    { 
                        g.lines = g.value.Lines.Length;
                        PositionEntries(true, g.panel.Top + extPanelVertScroll.Value);
                    }
                }
            };

            g.del = new ExtendedControls.ExtButton();
            g.del.Size = new Size(Font.ScalePixels(24), Font.ScalePixels(24));
            g.del.Text = "X";
            g.del.Tag = g;
            g.del.Click += Del_Clicked;
            toolTip1.SetToolTip(g.del, "Delete entry");
            g.panel.Controls.Add(g.del);

            groups.Add(g);

            extPanelVertScroll.Controls.Add(g.panel);
            ExtendedControls.Theme.Current?.ApplyDialog(g.panel);

            return g;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            stopresizepositioning = false;
            PositionEntries();
        }

        private void PositionEntries(bool calminsize = true, int pos = -1)     // fixes and positions groups.
        {
            int y = panelmargin;
            int panelwidth = Math.Max(extPanelVertScroll.Width , 10);
            int curpos = extPanelVertScroll.BeingPosition();

            int scrheight = Screen.FromControl(this).WorkingArea.Height;

            foreach (Group g in groups)
            {
                int texth = Math.Min(g.value.EstimateVerticalSizeFromText(),scrheight * 2 / 4);
            //    System.Diagnostics.Debug.WriteLine($"{g.value.Font} {texth}");

                g.panel.Location = new Point(panelmargin, y);
                g.panel.Size = new Size(panelwidth-panelmargin*2, Math.Max(texth,g.var.Height) + 12);

                g.del.Location = new Point(g.panel.Width - g.del.Width - 8, panelmargin);

                g.value.Size = new Size(g.del.Left -g.value.Left-8, g.panel.Height - 8);

                y += g.panel.Height + 6;
            }

            buttonMore.Location = new Point(panelmargin, y);
            buttonMore.Visible = groups.Count == 0 || allowmultiple;

            Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
            int titleHeight = screenRectangle.Top - this.Top;

            y += buttonMore.Height + titleHeight + ((panelTop.Enabled) ? (panelTop.Height + statusStripCustom.Height) : 8) + 16 + panelOK.Height;

            if (calminsize) // stop circular relationsship with resize
            {
                stopresizepositioning = true;   // stop resize double firing
                
                this.MaximumSize = new Size(Screen.FromControl(this).WorkingArea.Width, Screen.FromControl(this).WorkingArea.Height-128);
                this.MinimumSize = new Size(600, Math.Min(y,this.MaximumSize.Height));

                if (Bottom > Screen.FromControl(this).WorkingArea.Height)
                    Top = Screen.FromControl(this).WorkingArea.Height - Height - 50;

                stopresizepositioning = false;
            }

            extPanelVertScroll.FinishedPosition(pos >= 0 ? pos : curpos);
            Update();
        }

        bool stopresizepositioning = true;
        private void ConditionVariablesFormResize(object sender, EventArgs e)
        {
            if (!stopresizepositioning)
                PositionEntries(false);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Result = new Variables();
            ResultAltOPs = new Dictionary<string, string>();

            foreach ( Group g in groups)
            {
                if (g.var.Text.Length > 0)      // only ones with names are considered
                {
                    Result[g.var.Text] = g.value.Text.EscapeControlChars();

                    if (g.op != null)
                        ResultAltOPs[g.var.Text] = g.op.Text;
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void Del_Clicked(object sender, EventArgs e)
        {
            ExtendedControls.ExtButton b = sender as ExtendedControls.ExtButton;
            Group g = (Group)b.Tag;

            g.panel.Controls.Clear();
            extPanelVertScroll.Controls.Remove(g.panel);
            groups.Remove(g);
            PositionEntries();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonMore_Click(object sender, EventArgs e)
        {
            CreateEntry("", "","=");
            PositionEntries(true,int.MaxValue);
        }

        private void label_index_MouseDown(object sender, MouseEventArgs e)
        {
            OnCaptionMouseDown((Control)sender, e);
        }

        private void label_index_MouseUp(object sender, MouseEventArgs e)
        {
            OnCaptionMouseUp((Control)sender, e);
        }

        private void panel_minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private List<Group> groups;
        const int panelmargin = 3;
        private bool showadd, shownoexpand;
        private bool allowmultiple;

        private class Group
        {
            public Panel panel;
            public ExtendedControls.ExtTextBox var;
            public ExtendedControls.ExtComboBox op;
            public ExtendedControls.ExtRichTextBox value;
            public ExtendedControls.ExtButton del;
            public int lines;
        }


    }
}
