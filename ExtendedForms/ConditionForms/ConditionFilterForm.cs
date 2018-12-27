/*
 * Copyright © 2017 EDDiscovery development team
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
using BaseUtils.Win32Constants;
using System;
using System.Collections.Generic;
using System.Drawing;
using BaseUtils;

using System.Windows.Forms;

namespace ExtendedConditionsForms
{
    public partial class ConditionFilterForm : ExtendedControls.DraggableForm
    {
        public ConditionLists Result { get { return conditionFilterUC.Result; } }

        string initialtitle;

        public ConditionFilterForm()
        {
            InitializeComponent();
        }

        // used to start when just filtering.. uses a fixed event list .. must provide a call back to obtain names associated with an event

        public void InitFilter(string t, Icon ic, List<string> events, ConditionFilterUC.AdditionalNames a, List<string> varfields,  ConditionLists j = null)
        {
            InitThis(t, ic);
            conditionFilterUC.InitFilter(events, a, varfields, j);
        }

        // used to start when inside a condition of an IF of a program action (does not need additional names, already resolved)

        public void InitCondition(string t, Icon ic, List<string> varfields, ConditionLists j = null)
        {
            InitThis(t, ic);
            conditionFilterUC.InitCondition(varfields,j);
        }

        // used to start for a condition on an action form (does not need additional names, already resolved)

        public void InitCondition(string t, Icon ic, List<string> varfields, Condition j)
        {
            InitThis(t, ic);
            conditionFilterUC.InitCondition(varfields, j);
        }

        // This start

        private void InitThis(string t, Icon ic)
        {
            initialtitle = t;
            this.Icon = ic;
            bool winborder = ExtendedControls.ThemeableFormsInstance.Instance?.ApplyToForm(this, SystemFonts.DefaultFont) ?? false;
            statusStripCustom.Visible = panelTop.Visible = panelTop.Enabled = !winborder;

            BaseUtils.Translator.Instance?.Translate(this, new Control[] { label_index });

            SetTitle(conditionFilterUC.Groups);

            conditionFilterUC.onChangeInGroups += (g) => SetTitle(g);
            conditionFilterUC.onCalcMinsize += (y) => 
            {
                Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
                int titleHeight = screenRectangle.Top - this.Top;

                // tbd
                y += titleHeight + ((panelTop.Enabled) ? (panelTop.Height + statusStripCustom.Height) : 8) + 16 + panelOK.Height;

                this.MinimumSize = new Size(1000, y);
                this.MaximumSize = new Size(Screen.FromControl(this).WorkingArea.Width - 100, Screen.FromControl(this).WorkingArea.Height - 100);

                if (Bottom > Screen.FromControl(this).WorkingArea.Height)
                    Top = Screen.FromControl(this).WorkingArea.Height - Height - 50;
            };
        }

        public void LoadConditions( ConditionLists clist )
        {
            conditionFilterUC.LoadConditions(clist);
        }

        private void SetTitle(int g)
        {
            this.Text = label_index.Text = initialtitle + (g>0 ? (" (" + g.ToString() + ")") : "");
        }
        
#region OK Cancel

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult dr = conditionFilterUC.CheckAndAsk();
            if ( dr == DialogResult.OK || dr == DialogResult.Cancel)
            {
                DialogResult = dr;
                Close();
            }
            else if (dr == DialogResult.Ignore)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else if (dr == DialogResult.Abort)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        #endregion

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

    }
}
