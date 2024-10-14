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
        public bool Set(string controlname, string value)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase));
            if (t != null)
            {
                Control c = t.Control;
                if (c is ExtTextBox)
                {
                    (c as ExtTextBox).Text = value;     // keep caste in case overridden
                    return true;
                }
                else if (c is ExtRichTextBox)
                {
                    (c as ExtRichTextBox).Text = value;
                    return true;
                }
                else if (c is Label)
                {
                    (c as Label).Text = value;
                    return true;
                }
                else if (c is ExtCheckBox)
                {
                    (c as ExtCheckBox).Checked = !value.Equals("0");
                    return true;
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
            }

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
        // in JSON or in text 
        // see action doc for format

        public string AddSetRows(string controlname, string rowstring)
        {
            ConfigurableEntryList.Entry t = Entries.Find(x => x.Name.Equals(controlname, StringComparison.InvariantCultureIgnoreCase) && x.Control is ExtPanelDataGridViewScroll);
            if (t != null)
            {
                var cn = t.Control as ExtPanelDataGridViewScroll;
                var dgv = cn.DGV;

                if (rowstring.StartsWith("["))     // JSON
                {
                    JArray ja = JArray.Parse(rowstring, JToken.ParseOptions.CheckEOL);
                    if (ja != null)
                    {
                        foreach (JObject jo in ja)
                        {
                            int? rownumber = jo["Row"].IntNull();

                            DataGridViewRow rw = null;

                            bool insert = rownumber < 0;            // -1 insert at start, -2 add at end, else rownumber
                            if (insert)
                                rw = dgv.RowTemplate.Clone() as DataGridViewRow;
                            else if (rownumber < dgv.Rows.Count)    // else check in range
                                rw = dgv.Rows[rownumber.Value];
                            else
                            {
                                return "Incorrect row number in JSON";
                            }

                            JArray jcells = jo["Cells"].Array();

                            int cellno = 0;
                            foreach (JObject cell in jcells.DefaultIfEmpty())
                            {
                                string type = cell["Type"].Str().ToLowerInvariant();
                                string tooltip = cell["ToolTip"].Str();

                                int? cellsetnogiven = cell["Cell"].IntNull();      // we can override the cell number (only when replacing)
                                if (cellsetnogiven != null)                        // set the cell set number to the count if not set
                                    cellno = cellsetnogiven.Value;

                                if (type == "text")
                                {
                                    string value = cell["Value"].Str();

                                    if (insert)
                                        rw.Cells.Add(new DataGridViewTextBoxCell() { Value = value });
                                    else
                                        rw.Cells[cellno].Value = value;
                                }

                                if (tooltip != null)
                                    rw.Cells[cellno].ToolTipText = tooltip;

                                cellno++;
                            }

                            if (insert)
                            {
                                if (rownumber == -1 && dgv.Rows.Count > 0)
                                    dgv.Rows.Insert(0, rw);
                                else
                                    dgv.Rows.Add(rw);
                            }
                            cellno++;
                        }

                        return null;
                    }
                    else
                        return "Bad JSON";

                }
                else
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

                            DataGridViewRow rw = null;

                            bool insert = rownumber < 0;            // -1 insert at start, -2 add at end, else rownumber
                            if (insert)
                                rw = dgv.RowTemplate.Clone() as DataGridViewRow;
                            else if (rownumber < dgv.Rows.Count)    // else check in range
                                rw = dgv.Rows[rownumber.Value];
                            else
                            {
                                return "Incorrect row format in addsetrows";
                            }

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
            }
            else
                return "Not a DGV";
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

        public bool SetPosition(string controlname, Rectangle r, SizeF autoscalefactor)
        {
            var entry = Find(controlname);
            if (entry != null)
            {
                r = new Rectangle((int)(r.Left * autoscalefactor.Width), (int)(r.Top * autoscalefactor.Height),
                                  (int)(r.Width * autoscalefactor.Width), (int)(r.Height * autoscalefactor.Height)
                    );
                entry.Control.Bounds = r;
                return true;
            }
            else
            {
                r = Rectangle.Empty;
                return false;
            }
        }

    }
}
