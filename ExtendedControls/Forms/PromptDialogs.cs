/*
 * Copyright © 2016-2024 EDDiscovery development team
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
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    public static class PromptSingleLine
    {
        public static string ShowDialog(Form p,
                            string lab1, string defaultValue1, string caption, Icon ic, 
                            bool multiline = false, 
                            string tooltip = null, 
                            bool cursoratend = false,
                            int widthboxes = 200,           // sizes based on standard dialog size of 8, scaled up
                            int heightboxes = -1,
                            bool requireinput = false)
        {
            List<string> r = PromptMultiLine.ShowDialog(p, caption, ic, new string[] { lab1 }, 
                    new string[] { defaultValue1 }, multiline, tooltip != null ? new string[] { tooltip } : null , cursoratend, widthboxes, heightboxes, requireinput);

            return r?[0];
        }
    }

    public static class PromptMultiLine
    {
        // lab sets the items, def can be less or null
        public static List<string> ShowDialog(Form p, string caption, Icon ic, string[] lab, string[] def, 
                            bool multiline = false, 
                            string[] tooltips = null, 
                            bool cursoratend = false,
                            int widthboxes = 200,           // sizes based on standard dialog size of 12, scaled up
                            int heightboxes = -1,
                            bool requireinput = false)     // all boxes must have something for OK to be on
        {
            DraggableForm prompt = new DraggableForm()
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                Icon = ic
            };

            int lx = 10;

            Panel outer = new Panel() { Dock = DockStyle.Fill, BorderStyle = BorderStyle.FixedSingle };
            prompt.Controls.Add(outer);
            outer.MouseDown += (s, e) => { prompt.OnCaptionMouseDown(s as Control, e); };
            outer.MouseUp += (s, e) => { prompt.OnCaptionMouseUp(s as Control, e); };

            Label textLabel = new Label() { Left = lx, Top = 8, AutoSize = true, Text = caption };
            textLabel.MouseDown += (s, e) => { prompt.OnCaptionMouseDown(s as Control, e); };
            textLabel.MouseUp += (s, e) => { prompt.OnCaptionMouseUp(s as Control, e); };

            Theme theme = Theme.Current;
            System.Diagnostics.Debug.Assert(theme != null);

            if (!theme.WindowsFrame)
                outer.Controls.Add(textLabel);

            Label[] lbs = new Label[lab.Length];
            ExtRichTextBox[] tbs = new ExtRichTextBox[lab.Length];

            ToolTip tt = new ToolTip();
            tt.ShowAlways = true;

            int y = theme.WindowsFrame ? 20 : 40;
            if (heightboxes == -1)
                heightboxes = multiline ? 80 : 24;

            for (int i = 0; i < lab.Length; i++)
            {
                lbs[i] = new Label() { Left = lx, Top = y, AutoSize = true, Text = lab[i] };
                outer.Controls.Add(lbs[i]);

                tbs[i] = new ExtRichTextBox()
                {
                    Left = 0,       // will be set once we know the paras of all lines
                    Top = y,
                    Width = widthboxes,
                    Text = (def != null && i < def.Length) ? def[i] : "",
                    Height = heightboxes,
                };

                if (cursoratend)
                    tbs[i].Select(tbs[i].Text.Length, tbs[i].Text.Length);

                outer.Controls.Add(tbs[i]);

                if (tooltips != null && i < tooltips.Length)
                {
                    tt.SetToolTip(lbs[i], tooltips[i]);
                    tbs[i].SetTipDynamically(tt,tooltips[i]);      // no container here, set tool tip on text boxes using this
                }

                y += heightboxes + 20;
            }

            ExtButton confirmation = new ExtButton() { Text = "OK".TxID(ECIDs.MessageBoxTheme_OK), Left = 0, Width = 100, Top = y, DialogResult = DialogResult.OK };
            outer.Controls.Add(confirmation);
            confirmation.Click += (sender, e) => { prompt.Close(); };

            ExtButton cancel = new ExtButton() { Text = "Cancel".TxID(ECIDs.MessageBoxTheme_Cancel), Left = 0, Width = 100, Top = confirmation.Top, DialogResult = DialogResult.Cancel };
            outer.Controls.Add(cancel);
            cancel.Click += (sender, e) => { prompt.Close(); };

            if (!multiline)
                prompt.AcceptButton = confirmation;

            prompt.CancelButton = cancel;
            prompt.ShowInTaskbar = false;
            prompt.AutoScaleMode = AutoScaleMode.Font;

            theme.ApplyDialog(prompt);

            int controlleft = 0;
            for (int i = 0; i < lab.Length; i++)
                controlleft = Math.Max(controlleft, lbs[i].Right + 16);     // seems have to do this after sizing, confusingly

            for (int i = 0; i < lab.Length; i++)                            // all go here
            {
                tbs[i].Left = controlleft;
                tbs[i].TextBoxChanged += (s, e) => {
                    if (requireinput)
                        SetConfirmationState(tbs, confirmation);
                };

            }

            if ( requireinput )
                SetConfirmationState(tbs, confirmation);

            confirmation.Left = tbs[0].Right - confirmation.Width;          // cancel/confirm based on this
            cancel.Left = confirmation.Left - cancel.Width - 16;

            Size controlsize = outer.FindMaxSubControlArea(0, 0);
            prompt.Size = new Size(controlsize.Width + 40, controlsize.Height + (theme.WindowsFrame ? 50 : 8));

            if (prompt.ShowDialog(p) == DialogResult.OK)
            {
                var r = (from t in tbs select t.Text).ToList();
                return r;
            }
            else
                return null;
        }

        private static void PromptMultiLine_TextBoxChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        static private void SetConfirmationState(ExtRichTextBox[] tbs, ExtButton confirmation )
        {
            for (int jj = 0; jj < tbs.Length; jj++)
            {
                if (!tbs[jj].Text.HasChars())
                {
                    confirmation.Enabled = false;
                    return;
                }
            }

            confirmation.Enabled = true;
        }
    }

}
