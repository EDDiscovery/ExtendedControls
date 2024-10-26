/*
 * Copyright © 2024-2024 EDDiscovery development team
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

using QuickJSON;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class ConfigurableEntryList
    {
        // Set value of control by string value
        public bool Set(string controlname, string value, bool replaceescapes)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            if (t != null)
            {
                // in same order as Control Definition in Action doc and in same order as MakeEntry

                Control c = t.Control;

                if (c is Label || c.GetType() == typeof(ExtButton))         // need the second due to button being a base class
                {
                    c.Text = value;
                    return true;
                }
                else if (c is ExtTextBox)
                {
                    if (replaceescapes)
                        value = value.ReplaceEscapeControlCharsFull();
                    (c as ExtTextBox).Text = value;     // keep caste in case overridden
                    return true;
                }
                else if (c is ExtRichTextBox)
                {
                    if (replaceescapes)
                        value = value.ReplaceEscapeControlCharsFull();
                    (c as ExtRichTextBox).Text = value;
                    return true;
                }
                else if (c is ExtCheckBox)
                {
                    (c as ExtCheckBox).Checked = value.InvariantParseInt(0) != 0;
                    return true;
                }
                else if (c is ExtDateTimePicker)
                {
                    if (DateTime.TryParse(value, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out DateTime tme))
                    {
                        (c as ExtDateTimePicker).Value = tme;
                        return true;
                    }
                }
                else if (c is NumberBoxLong)
                {
                    var cn = c as NumberBoxLong;
                    long? v = value.InvariantParseLongNull();
                    if (v.HasValue)
                    {
                        cn.Value = v.Value;
                        return true;
                    }
                }
                else if (c is NumberBoxDouble)
                {
                    var cn = c as NumberBoxDouble;
                    double? v = value.InvariantParseDoubleNull();
                    if (v.HasValue)
                    {
                        cn.Value = v.Value;
                        return true;
                    }
                }
                else if (c is NumberBoxInt)
                {
                    var cn = c as NumberBoxInt;
                    int? v = value.InvariantParseIntNull();
                    if (v.HasValue)
                    {
                        cn.Value = v.Value;
                        return true;
                    }
                }
                else if (c is ExtComboBox)
                {
                    ExtComboBox cb = c as ExtComboBox;
                    if (cb.Items.Contains(value))
                    {
                        cb.Enabled = false;
                        cb.SelectedItem = value;
                        cb.Enabled = true;
                        return true;
                    }
                }
                // panel, flowpanel, can't set
                else if (c is ExtPanelRollUp)
                {
                    var cn = c as ExtPanelRollUp;
                    bool? b = value.InvariantParseBoolNull();
                    if (b != null)
                    {
                        cn.PinState = b.Value;
                        return true;
                    }
                }
                // dgv can't set
                else if (c is ExtButtonWithNewCheckedListBox)
                {
                    var cn = c as ExtButtonWithNewCheckedListBox;
                    cn.Set(value);
                    return true;
                }
                else if (c is SplitContainer)
                {
                    double? v = value.InvariantParseDoubleNull();
                    if (v.HasValue)
                    {
                        var cn = c as SplitContainer;
                        cn.SplitterDistance(v.Value / 100.0);
                        return true;
                    }
                }
            }

            return false;
        }

        // add text to rich text box at bottom and scroll
        public bool AddText(string controlname, string text)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is ExtRichTextBox);
            if (t != null)
            {
                var tb = t.Control as ExtRichTextBox;
                text = text.ReplaceEscapeControlCharsFull();
                tb.AppendText(text);
                return true;
            }
            else
                return false;
        }

        // from controls starting with this name, set the names of the ones checked
        public void SetCheckedList(IEnumerable<string> controlnames, bool state)
        {
            var cnames = controlnames.ToArray();
            var elist = Entries.Where(x => x.Control is ExtCheckBox && Array.IndexOf(cnames, x.Name) >= 0).Select(x => x);
            foreach (var e in elist)
            {
                (e.Control as ExtCheckBox).Checked = state;
            }
        }

        // radio button this set, to 1 entry, or to N max
        public void RadioButton(string startingcontrolname, string controlhit, int max = 1)
        {
            var elist = Entries.Where(x => x.Control is ExtCheckBox && x.Name.StartsWith(startingcontrolname)).Select(x => x).ToArray();

            if (max == 1)
            {
                foreach (var x in elist)
                {
                    ((ExtCheckBox)x.Control).Checked = x.Name == controlhit;
                }
            }
            else
            {
                var numchecked = elist.Where(x => ((ExtCheckBox)x.Control).Checked).Count();
                if (numchecked > max)
                {
                    var chkbox = GetControl<ExtCheckBox>(controlhit);
                    chkbox.Checked = false;
                }
            }
        }


        // For DGV, add or reset the row cell values. 
        // see action doc for format of this text method

        public string AddSetRows(string controlname, string rowstring)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is ExtPanelDataGridViewScroll);
            if (t != null)
            {
                var cn = t.Control as ExtPanelDataGridViewScroll;
                var dgv = cn.DGV;

                cn.Suspend();
                string v = ProcessAddSetRows(dgv, rowstring);
                cn.Resume();
                return v;
            }
            else
                return "Not a DGV";
        }

        private string ProcessAddSetRows(DataGridView dgv, string rowstring)
        { 
            BaseUtils.StringParser sp = new BaseUtils.StringParser(rowstring);

            while (!sp.IsEOL)
            {
                int? rownumber = sp.NextIntComma(" ,");
                if (rownumber.HasValue)
                {
                    int cellno = 0;

                    if (sp.PeekChar() != '(')
                    {
                        int? cno = sp.NextIntComma(" ,");
                        if (cno != null)
                            cellno = cno.Value;
                        else
                            return "Missing cell number after row";
                    }

                    DataGridViewRow rw;

                    bool insert = rownumber < 0;            // -1 insert at start, -2 add at end, else rownumber
                    if (insert)
                        rw = dgv.RowTemplate.Clone() as DataGridViewRow;
                    else if (rownumber < dgv.Rows.Count)    // else check in range
                        rw = dgv.Rows[rownumber.Value];
                    else
                        return "Incorrect row format in addsetrows";

                    while (sp.IsCharMoveOn('('))
                    {
                        string coltype = sp.NextQuotedWordComma(System.Globalization.CultureInfo.InvariantCulture);      // text, etc, see below

                        if (coltype == "text")
                        {
                            string value = sp.NextQuotedWord(" ),");

                            if (insert)
                                rw.Cells.Add(new DataGridViewTextBoxCell() { Value = value });
                            else
                            {
                                if (cellno < dgv.Columns.Count)
                                    rw.Cells[cellno].Value = value;
                                else
                                    return "Cell number beyond column count";
                            }
                        }
                        else
                        {
                            return "Unsuported Column Type " + coltype;
                        }

                        if (sp.IsCharMoveOn(','))       // more stuff, tooltip
                        {
                            string tooltip = sp.NextQuotedWord(" ),");

                            if (tooltip != null)
                                rw.Cells[cellno].ToolTipText = tooltip;
                        }

                        if (!sp.IsCharMoveOn(')'))
                            return "Missing ) at end of cell definition";

                        if (sp.IsEOL || sp.IsCharMoveOn(';'))      // EOL or semicolon ends definition of this rows cells
                            break;

                        if (!sp.IsCharMoveOn(','))     // then must be comma
                        {
                            return "Incorrect cell format missing comma";
                        }

                        cellno++;
                    }

                    if (insert)
                    {
                        if (rownumber == -1 && dgv.Rows.Count > 0)
                            dgv.Rows.Insert(0, rw);
                        else
                            dgv.Rows.Add(rw);
                    }
                }
                else
                {
                    return "Incorrect row format in addsetrows";
                }
            }

            return null;
        }

        // Jarray of row commands in JObject {"Row",["CellStart"],Cells[ JObject { "Type",["ToolTip"], ["Cell"], "Value"] ]}
        public string AddSetRows(string controlname, object rowcommands)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is ExtPanelDataGridViewScroll);
            if (t != null && rowcommands is JArray)
            {
                var cn = t.Control as ExtPanelDataGridViewScroll;
                var dgv = cn.DGV;

                cn.Suspend();

                string v = ProcessAddSetRows(dgv, (JArray)rowcommands);

                cn.Resume();

                return v;
            }
            else
                return "Not a DGV or not a JArray";
        }

        private string ProcessAddSetRows(DataGridView dgv, JArray rowcommands)
        {
            foreach (JObject jo in rowcommands)
            {
                int? rownumber = jo["Row"].IntNull();

                DataGridViewRow rw = null;

                bool insert = rownumber < 0;            // -1 insert at start, -2 add at end, else rownumber
                if (insert)
                    rw = dgv.RowTemplate.Clone() as DataGridViewRow;
                else if (rownumber < dgv.Rows.Count)    // else check in range
                    rw = dgv.Rows[rownumber.Value];
                else
                    return "Incorrect row number in JSON";

                int cellno = jo["CellStart"].Int(0);

                JArray jcells = jo["cells"].Array();
                if (jcells == null)
                    return "Missing Cells definition";
                try
                {
                    foreach (JObject cell in jcells)
                    {
                        string coltype = cell["Type"].Str().ToLowerInvariant();
                        string tooltip = cell["ToolTip"].Str();

                        int? cellsetnogiven = cell["Cell"].IntNull();      // we can override the cell number (only when replacing)
                        if (cellsetnogiven != null)                        // set the cell set number to the count if not set
                            cellno = cellsetnogiven.Value;

                        if (coltype == "text")
                        {
                            // convert this way to allow for integers and strings to be passed
                            string value = Convert.ToString(cell["Value"].Value, System.Globalization.CultureInfo.InvariantCulture);

                            if (insert)
                                rw.Cells.Add(new DataGridViewTextBoxCell() { Value = value });
                            else
                                rw.Cells[cellno].Value = value;
                        }
                        else
                            return "Unsuported Column Type " + coltype;

                        if (tooltip != null)
                            rw.Cells[cellno].ToolTipText = tooltip;

                        cellno++;
                    }
                }
                catch
                {
                    return "JSON missing object in cells definitions";
                }

                if (insert)
                {
                    if (rownumber == -1 && dgv.Rows.Count > 0)
                        dgv.Rows.Insert(0, rw);
                    else
                        dgv.Rows.Add(rw);
                }
            }

            return null;
        }

        public bool Clear(string controlname)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is ExtPanelDataGridViewScroll);
            if (t != null)
            {
                var cn = t.Control as ExtPanelDataGridViewScroll;
                var dgv = cn.DGV;
                dgv.Rows.Clear();
                return true;
            }
            t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is ExtRichTextBox);
            if ( t != null )
            {
                var cn = t.Control as ExtRichTextBox;
                cn.Clear();
                return true;
            }

            return false;
        }

        // remove DGV,start row, count
        public int RemoveRows(string controlname, int rowstart, int count)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is ExtPanelDataGridViewScroll);
            if (t != null)
            {
                var cn = t.Control as ExtPanelDataGridViewScroll;
                var dgv = cn.DGV;

                cn.Suspend();

                if (rowstart < 0)
                    rowstart = dgv.Rows.Count + rowstart;       // so -1 will be last row

                int removed = 0;

                if (rowstart >= 0 && rowstart < dgv.Rows.Count) // if in range
                {
                    while (count > 0 && dgv.Rows.Count > rowstart)
                    {
                        dgv.Rows.RemoveAt(rowstart);
                        count--;
                        removed++;
                    }
                }

                cn.Resume();

                return removed;
            }

            return -1;
        }

        public void CloseDropDown()
        {
            foreach (var x in Entries.Where(x => x.Control is ExtButtonWithNewCheckedListBox))
            {
                var c = x.Control as ExtButtonWithNewCheckedListBox;
                if (c.IsDropDownActive)
                {
                    c.CloseDropDown();
                }
            }

        }

        public bool Enable(string controlname, bool enabled)
        {
            var entry = Find(controlname);
            if (entry != null)
            {
                entry.Control.Enabled = enabled;
                return true;
            }
            else
                return false;
        }

        public bool Visible(string controlname, bool visible)
        {
            var entry = Find(controlname);
            if (entry != null)
            {
                entry.Control.Visible = visible;
                return true;
            }
            else
                return false;
        }

        public bool SetPosition(string controlname, Point p, SizeF autoscalefactor)
        {
            var entry = Find(controlname);
            if (entry != null)
            {
                Point newp = new Point((int)(p.X * autoscalefactor.Width), (int)(p.Y * autoscalefactor.Height));
                entry.Control.Location = newp;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetSize(string controlname, Size s, SizeF autoscalefactor)
        {
            var entry = Find(controlname);
            if (entry != null)
            {
                Size news = new Size((int)(s.Width * autoscalefactor.Width), (int)(s.Height * autoscalefactor.Height));
                entry.Control.Size = news;
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
