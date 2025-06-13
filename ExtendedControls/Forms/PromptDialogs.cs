/*
 * Copyright 2016-2025 EDDiscovery development team
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
using System.Runtime.CompilerServices;
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
                            int heightscrollarea = 800,
                            bool requireinput = false)
        {
            List<string> r = PromptMultiLine.ShowDialog(p, caption, ic, new string[] { lab1 }, 
                    new string[] { defaultValue1 }, multiline, tooltip != null ? new string[] { tooltip } : null , cursoratend, widthboxes, heightboxes, heightscrollarea, requireinput);

            return r?[0];
        }
    }

    public static class PromptMultiLine
    {
        // Older interface
        public static List<string> ShowDialog(Form p, string caption, Icon ic,
                     string[] labels,
                     string[] values,
                     bool multiline = false,
                     string[] tooltips = null,
                     bool cursoratend = false,
                     int widthboxes = 200,           
                     int heightboxes = -1,
                     int heightscrollarea = 800,
                     bool requireinput = false)     
        {
            int[] heightboxesa = new int[labels.Length];
            for (int i = 0; i < labels.Length; i++)
            {
                heightboxesa[i] = heightboxes;
            }

            return ShowDialog(p, caption, ic, labels, values, heightboxesa, tooltips, multiline, cursoratend, widthboxes, heightscrollarea, 20, requireinput);
        }


        // lab sets the items, def can be less or null
        public static List<string> ShowDialog(Form p, string caption, Icon ic, 
                            string[] labels, 
                            string[] values, 
                            int[] boxheight,
                            string[] tooltips = null,
                            bool multiline = true,
                            bool cursoratend = false,
                            int widthboxes = 200,           
                            int heightscrollarea = 800,     // set to -1 for auto scaling to screen h
                            int boxspacing = 16,
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

            Theme theme = Theme.Current;
            System.Diagnostics.Debug.Assert(theme != null);

            Label textLabel = null;

            if (!theme.WindowsFrame)
            {
                textLabel = new Label() { Left = lx, Top = 8, AutoSize = true, Text = caption };
                textLabel.MouseDown += (s, e) => { prompt.OnCaptionMouseDown(s as Control, e); };
                textLabel.MouseUp += (s, e) => { prompt.OnCaptionMouseUp(s as Control, e); };
                outer.Controls.Add(textLabel);
            }

            ExtPanelVertScrollWithBar vscroll = new ExtPanelVertScrollWithBar();

            ExtPanelVertScroll contentpanel = new ExtPanelVertScroll();
            contentpanel.Dock = DockStyle.Fill;
            vscroll.Controls.Add(contentpanel);

            outer.Controls.Add(vscroll);

            Label[] lbs = new Label[labels.Length];
            ExtRichTextBox[] tbs = new ExtRichTextBox[labels.Length];

            ToolTip tt = new ToolTip();
            tt.ShowAlways = true;

            int contentvpos = 0;

            for (int i = 0; i < labels.Length; i++)
            {
                lbs[i] = new Label() { Left = lx, Top = contentvpos, AutoSize = true, Text = labels[i] };
                contentpanel.Controls.Add(lbs[i]);

                tbs[i] = new ExtRichTextBox()
                {
                    Left = 0,       // will be set once we know the paras of all lines
                    Top = contentvpos,
                    Width = widthboxes,
                    Text = (values != null && i < values.Length) ? values[i] : "",
                    Height = boxheight[i],
                };

                if (cursoratend)
                    tbs[i].Select(tbs[i].Text.Length, tbs[i].Text.Length);

                contentpanel.Controls.Add(tbs[i]);

                if (tooltips != null && i < tooltips.Length)
                {
                    tt.SetToolTip(lbs[i], tooltips[i]);
                    tbs[i].SetTipDynamically(tt,tooltips[i]);      // no container here, set tool tip on text boxes using this
                }

                contentvpos += tbs[i].Height + boxspacing;
            }

            ExtButton confirmation = new ExtButton() { Text = "OK".TxID(ECIDs.MessageBoxTheme_OK), Width = 100, DialogResult = DialogResult.OK };
            outer.Controls.Add(confirmation);
            confirmation.Click += (sender, e) => { prompt.Close(); };

            ExtButton cancel = new ExtButton() { Text = "Cancel".TxID(ECIDs.MessageBoxTheme_Cancel), Width = 100, DialogResult = DialogResult.Cancel };
            outer.Controls.Add(cancel);
            cancel.Click += (sender, e) => { prompt.Close(); };

            theme.ApplyDialog(prompt);

            int controlleft = 0;
            for (int i = 0; i < labels.Length; i++)
                controlleft = Math.Max(controlleft, lbs[i].Right + 16);     // seems have to do this after sizing, confusingly

            Screen scr = Screen.FromPoint(p.Location);
            if (heightscrollarea < 0)
                heightscrollarea = scr.WorkingArea.Height * 14 / 16;
            else
                heightscrollarea = Math.Min(scr.WorkingArea.Height - 128, heightscrollarea);

            vscroll.Bounds = new Rectangle(0, textLabel != null ? textLabel.Bottom + lx : lx,
                                    controlleft + widthboxes + vscroll.ScrollBarWidth + 4, Math.Min(heightscrollarea, Math.Max(20, contentvpos - 18)));
            confirmation.Location = new Point(vscroll.Right - 100, vscroll.Bottom + 8);
            cancel.Location = new Point(confirmation.Left - 120, vscroll.Bottom + 8);

            for (int i = 0; i < labels.Length; i++)                            // all go here
            {
                tbs[i].Left = controlleft;
                tbs[i].TextBoxChanged += (s, e) => {  if (requireinput) SetConfirmationState(tbs, confirmation); };
            }

            if ( requireinput )
                SetConfirmationState(tbs, confirmation);

            if (!multiline)
                prompt.AcceptButton = confirmation;

            prompt.CancelButton = cancel;
            prompt.ShowInTaskbar = false;
            prompt.AutoScaleMode = AutoScaleMode.Font;

            prompt.ClientSize = new Size(vscroll.Width+lx,confirmation.Bottom+8);

            contentpanel.Recalcuate();

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
