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
                            int heightboxes = -1)
        {
            List<string> r = PromptMultiLine.ShowDialog(p, caption, ic, new string[] { lab1 }, 
                    new string[] { defaultValue1 }, multiline, tooltip != null ? new string[] { tooltip } : null , cursoratend, widthboxes, heightboxes);

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
                            int widthboxes = 200,           // sizes based on standard dialog size of 8, scaled up
                            int heightboxes = -1)
        {
            ITheme theme = ThemeableFormsInstance.Instance;

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

            if (!theme.WindowsFrame)
                outer.Controls.Add(textLabel);

            Label[] lbs = new Label[lab.Length];
            ExtendedControls.ExtRichTextBox[] tbs = new ExtendedControls.ExtRichTextBox[lab.Length];

            ToolTip tt = new ToolTip();
            tt.ShowAlways = true;

            int y = theme.WindowsFrame ? 20 : 40;
            if (heightboxes == -1)
                heightboxes = multiline ? 80 : 24;

            int tx = 120;

            for (int i = 0; i < lab.Length; i++)
            {
                lbs[i] = new Label() { Left = lx, Top = y, AutoSize = true, Text = lab[i] };
                tbs[i] = new ExtendedControls.ExtRichTextBox()
                {
                    Left = tx,
                    Top = y,
                    Width = widthboxes,
                    Text = (def != null && i < def.Length) ? def[i] : "",
                    Height = heightboxes,
                };

                if (cursoratend)
                    tbs[i].Select(tbs[i].Text.Length, tbs[i].Text.Length);

                outer.Controls.Add(lbs[i]);
                outer.Controls.Add(tbs[i]);

                if (tooltips != null && i < tooltips.Length)
                {
                    tt.SetToolTip(lbs[i], tooltips[i]);
                    tbs[i].SetTipDynamically(tt,tooltips[i]);      // no container here, set tool tip on text boxes using this
                }

                y += heightboxes + 20;
            }

            ExtendedControls.ExtButton confirmation = new ExtendedControls.ExtButton() { Text = "OK".Tx(), Left = tbs[0].Right - 100, Width = 100, Top = y, DialogResult = DialogResult.OK };
            outer.Controls.Add(confirmation);
            confirmation.Click += (sender, e) => { prompt.Close(); };

            ExtendedControls.ExtButton cancel = new ExtendedControls.ExtButton() { Text = "Cancel".Tx(), Left = confirmation.Location.X - 120, Width = 100, Top = confirmation.Top, DialogResult = DialogResult.Cancel };
            outer.Controls.Add(cancel);
            cancel.Click += (sender, e) => { prompt.Close(); };

            if (!multiline)
                prompt.AcceptButton = confirmation;

            prompt.CancelButton = cancel;
            prompt.ShowInTaskbar = false;
            prompt.AutoScaleMode = AutoScaleMode.Font;

            theme.ApplyDialog(prompt);

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
    }

}
