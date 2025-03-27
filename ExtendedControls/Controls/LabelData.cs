/*
 * Copyright © 2022-2025 EDDiscovery development team
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

using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class LabelData : Control, IThemeable
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

                    string textpart = brace >= 0 ? text.Substring(pos, brace-pos) : text.Substring(pos);

                    pos = brace >= 0 ? brace : text.Length;     // move to brace pos or eot

                    string datavalue = null;
                    bool dataisnull = false;

                    while (brace >= 0)        // collect all the values into a string, allow {..}+separ{} format
                    {
                        int endpart = text.IndexOfAny(new char[] { '}', '|' }, brace + 1);

                        if (endpart > 0)
                        {
                            string ctrl = text.Substring(brace + 1, endpart - brace - 1);
                            try
                            {
                                if (Data != null && Data.Length > datanum)
                                {
                                    if (Data[datanum] != null)
                                    {
                                        datavalue += string.Format("{0:" + ctrl + "}", Data[datanum]);
                                    }
                                    else
                                        dataisnull = true;      // one null in the datavalue sequence means abort printing the text and all values

                                    datanum++;
                                }
                                else
                                    datavalue = "???";
                            }
                            catch
                            {
                                datavalue += $"Formatting error {ctrl}";
                            }

                            if (text[endpart] == '}')       // end of text..
                            {
                                pos = endpart + 1;
                                break;
                            }
                            else
                            {
                                if (endpart + 1 < text.Length && text[endpart + 1] != '|')  // if not ||
                                {
                                    int indext = text.IndexOfAny(new char[] { '}', '|' }, endpart + 1);

                                    if (indext >= 0)
                                    {
                                        datavalue += text.Substring(endpart + 1, indext - endpart - 1);

                                        if (text[indext] == '}')
                                        {
                                            pos = indext + 1;
                                            break;
                                        }
                                        else
                                        {
                                            brace = indext;
                                        }
                                    }
                                    else
                                    {
                                        pos = text.Length; // terminate
                                        break;
                                    }
                                }
                                else
                                    brace = endpart + 1;
                            }
                        }
                        else
                        {
                            pos = text.Length;      // terminate
                            break;
                        }
                    }

                    //     System.Diagnostics.Debug.WriteLine($"Part `{textpart}` data `{datavalue}`");

                    if (!dataisnull)        // flag means dump the text before and the value
                    {
                        pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        using (var brush = new SolidBrush(ForeColor))
                        {
                            if (textpart.HasChars())
                            {
                                //var tsize = TextRenderer.MeasureText(textpart, Font, new Size(1920, 50));
                                var tsize = pe.Graphics.MeasureString(textpart, Font, new Size(10000, 10000), StringFormat.GenericTypographic).ToSize();

                                if (tabspacing != 0 && xpos != 0)      // try and align
                                {
                                    int nexttab = (xpos / tabspacing + 1) * tabspacing;
                                    int textpos = nexttab - tsize.Width - InterSpacing;
                                    if (textpos > xpos)     // only if we don't butt into what we have already done
                                        xpos = textpos;
                                }

                                //System.Diagnostics.Debug.WriteLine($"Output text `{textpart}` at {xpos} - {xpos + tsize.Width} {tsize}");

                                pe.Graphics.DrawString(textpart, Font, brush, new Point(xpos, 1), StringFormat.GenericTypographic);
                                //TextRenderer.DrawText(pe.Graphics, textpart, Font, new Rectangle(xpos, 1, tsize.Width, Font.Height), ForeColor);

                                xpos += tsize.Width + InterSpacing;
                            }

                            if (datavalue.HasChars())
                            {
                                var dfont = DataFont ?? Font;

                                //var tsize = TextRenderer.MeasureText(datavalue, dfont, new Size(1920, 50));     // this overestimates, the bottom clips!
                                var tsize = pe.Graphics.MeasureString(datavalue, dfont, new Size(10000, 10000), StringFormat.GenericTypographic).ToSize();
                                tsize.Width += BorderWidth * 2;

                                if (boxstyle != DataBoxStyle.None)
                                {
                                    using (Pen pen = new Pen(bordercolor, borderwidth))
                                    {
                                        if (boxstyle == DataBoxStyle.AllAround)
                                        {
                                            pe.Graphics.DrawRectangle(pen, xpos + borderwidth - 1, borderwidth, tsize.Width - (BorderWidth - 1), dfont.Height);
                                        }
                                        else if (boxstyle == DataBoxStyle.TopBottom)
                                        {
                                            pe.Graphics.DrawLine(pen, xpos + borderwidth - 1, borderwidth, xpos + tsize.Width - (BorderWidth - 1) - 1, borderwidth);
                                            pe.Graphics.DrawLine(pen, xpos + borderwidth - 1, dfont.Height, xpos + tsize.Width - (BorderWidth - 1) - 1, dfont.Height);
                                        }
                                        else
                                        {
                                            pe.Graphics.DrawLine(pen, xpos + borderwidth - 1, dfont.Height, xpos + tsize.Width - (BorderWidth - 1) - 1, dfont.Height);
                                        }
                                    }
                                }

                                //System.Diagnostics.Debug.WriteLine($"..Output data `{datavalue}` at {xpos} - {xpos + tsize.Width} {tsize}");

                                pe.Graphics.DrawString(datavalue, dfont, brush, new Point(xpos + BorderWidth, 1), StringFormat.GenericTypographic);
                                //TextRenderer.DrawText(pe.Graphics, datavalue, dfont, new Rectangle(xpos + BorderWidth, 1, tsize.Width - BorderWidth*2, Font.Height), ForeColor);

                                xpos += tsize.Width + InterSpacing;
                            }
                        }
                    }
                }
            }

        }

        public bool Theme(Theme t, Font fnt)
        {
            BorderColor = t.TextBlockBorderColor;
            ForeColor = t.LabelColor;
            return false;
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

