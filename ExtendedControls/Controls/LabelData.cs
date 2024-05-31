/*
 * Copyright © 2022-2024 EDDiscovery development team
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this
 * file except in compliance with the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required bylicable law or agreed to in writing, software distributed under
 * the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
 * ANY KIND, either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class LabelData : Control
    {
        [System.ComponentModel.Browsable(true)]
        public override string Text { get { return base.Text; } set { base.Text = value; Invalidate(); } }

        public Color BorderColor { get { return bordercolor; } set { bordercolor = value; Invalidate(); } }
        public int BorderWidth { get { return borderwidth; } set { borderwidth = value; Invalidate(); } }

        public int InterSpacing { get { return interspacegap; } set { interspacegap = value; Invalidate(); } }
        public int TabSpacingData { get { return tabspacing; } set { tabspacing = value; Invalidate(); } }      //0 = off

        public Font DataFont { get { return datafont; } set { datafont = value; Invalidate(); } }

        public string NoDataText { get { return nodatatext; } set { nodatatext = value; Invalidate(); } }         // if set, and Data == null, this is printed

        public enum DataBoxStyle { None, AllAround, TopBottom , Underline }

        public DataBoxStyle BoxStyle { get { return boxstyle; } set { boxstyle = value; Invalidate(); } }

        public object[] Data { get { return data; } set { data = value; Invalidate(); } }

        public LabelData()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |        // "Free" double-buffering (1/3).
                ControlStyles.OptimizedDoubleBuffer |       // "Free" double-buffering (2/3).
                ControlStyles.ResizeRedraw |                // Invalidate after a resize or if the Padding changes.
                ControlStyles.SupportsTransparentBackColor |// BackColor.A can be less than 255.
                ControlStyles.UserPaint |                   // "Free" double-buffering (3/3); OnPaintBackground and OnPaint are needed.
                ControlStyles.UseTextForAccessibility,      // Use Text for the mnemonic char (and accessibility) if not empty, else the previous Label in the tab order.
                true);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            string text = Data != null || NoDataText == null ? Text : NoDataText != null ? NoDataText : null;  

            if (text.HasChars())        // if we are painting text
            {
                int xpos = 0;
                int pos = 0;
                int datanum = 0;

                while( pos < text.Length)
                {
                    int brace = text.IndexOf('{', pos);

                    string textpart = brace >= 0 ? text.Substring(pos, brace-pos).Trim() : text.Substring(pos).Trim();

                    pos = brace >= 0 ? brace : text.Length;

                    string datavalue = "";

                    if (brace >= 0)
                    {
                        while (true)        // collect all the values into a string, allow {..}+separ{} format
                        {
                            int endbrace = text.IndexOf('}', pos);
                            pos = endbrace + 1;
                            string ctrl = text.Substring(brace + 1, endbrace - brace - 1);

                            datavalue += Data != null && Data.Length > datanum ? string.Format("{0:" + ctrl + "}", Data[datanum++]) : "???";

                            if (pos < text.Length - 1 && text[pos] == '+')
                            {
                                brace = text.IndexOf("{", pos);
                                if (brace > 0)
                                {
                                    datavalue += text.Substring(pos + 1, brace - pos - 1);
                                    pos = brace;
                                }
                                else
                                    break;
                            }
                            else
                                break;
                        }
                    }

                    if (textpart.HasChars())
                    {
                        var tsize = TextRenderer.MeasureText(textpart, Font);

                        if ( tabspacing != 0 && xpos != 0)      // try and align
                        {
                            int nexttab = (xpos / tabspacing + 1) * tabspacing;
                            int textpos = nexttab - tsize.Width - InterSpacing;
                            if (textpos > xpos)     // only if we don't butt into what we have already done
                                xpos = textpos;
                        }

                        //System.Diagnostics.Debug.WriteLine($"Output text `{textpart}` at {xpos}");
                        TextRenderer.DrawText(pe.Graphics, textpart, Font, new Rectangle(xpos, 1, tsize.Width, Font.Height), ForeColor);
                        xpos += tsize.Width + InterSpacing;
                    }

                    if (datavalue.HasChars())
                    {
                        var dfont = DataFont ?? Font;

                        var size = TextRenderer.MeasureText(datavalue, dfont);

                        if (boxstyle != DataBoxStyle.None)
                        {
                            using (Pen pen = new Pen(bordercolor, borderwidth))
                            {
                                if (boxstyle == DataBoxStyle.AllAround)
                                    pe.Graphics.DrawRectangle(pen, xpos + borderwidth - 1, borderwidth, size.Width - (BorderWidth - 1) - 1, dfont.Height);
                                else if (boxstyle == DataBoxStyle.TopBottom)
                                {
                                    pe.Graphics.DrawLine(pen, xpos + borderwidth - 1, borderwidth, xpos + size.Width - (BorderWidth - 1) - 1, borderwidth);
                                    pe.Graphics.DrawLine(pen, xpos + borderwidth - 1, dfont.Height, xpos + size.Width - (BorderWidth - 1) - 1, dfont.Height);
                                }
                                else
                                {
                                    pe.Graphics.DrawLine(pen, xpos + borderwidth - 1, dfont.Height, xpos + size.Width - (BorderWidth - 1) - 1, dfont.Height);
                                }
                            }
                        }

                        //System.Diagnostics.Debug.WriteLine($".. Output value `{datavalue}` at {xpos}");
                        TextRenderer.DrawText(pe.Graphics, datavalue, dfont, new Rectangle(xpos, 1, size.Width, Font.Height), ForeColor);
                        xpos += size.Width + InterSpacing;
                    }
                }
            }

        }



        private Color bordercolor = Color.Orange;
        private int borderwidth = 1;
        private int interspacegap = 4;
        private int tabspacing= 0;
        private Font datafont = null;
        private DataBoxStyle boxstyle = DataBoxStyle.AllAround;
        private object[] data = null;
        private string nodatatext = null;

    }
}

