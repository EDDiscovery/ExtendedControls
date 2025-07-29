/*
 * Copyright 2024-2024 EDDiscovery development team
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

using BaseUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class ConfigurableEntryList
    {
        #region Create any new entries in the list

        // create entry in contentpanel/toppanel (may be null)/bottompanel (may be null)
        // tooltips go to tooltipcontrol
        // autosize factor if set causes a scaling of the location/size
        // YScollPos = scroll vertical setting, which is autosized

        public void CreateEntries(ExtPanelVertScroll contentpanel, Panel toppanel, Panel bottompanel, ToolTip tooltipcontrol,
                                SizeF? autosizefactor = null, Font themefont = null, int Yscrollpos = 0)
        {
            foreach (var ent in Entries)
            {
                if (ent.Created)        // don't double create
                    continue;

                Control c = ent.ControlType != null ? (Control)Activator.CreateInstance(ent.ControlType) : ent.Control;

                //System.Diagnostics.Debug.WriteLine($"Add Control {ent.Name} of {c.GetType()} at {ent.Location} {ent.Size} {ent.TextValue}");

                ent.Control = c;
                c.Size = ent.Size;
                c.Margin = ent.Margin;

                // if we have autosizeing on, the scroll position which is added to take account of current scrolling needs to be scaled
                // the scroll position is in terms of the expanded autosize factor (ie. increased) so we need to move it back to non expanded
                // then the AutoScale below will apply anything back

                if (autosizefactor != null)
                    Yscrollpos = (int)(Yscrollpos / autosizefactor.Value.Height);

                c.Location = new Point(ent.Location.X, ent.Location.Y + YOffset + Yscrollpos);
                c.Dock = ent.Dock;  // dock style

                c.Name = ent.Name;
                c.Enabled = ent.Enabled;
                c.Tag = ent;     // point control tag at ent structure

                if (ent.InPanel != null)     // if place in panel
                {
                    // find by name, first the total name, second panel 1/2 of a splitter.

                    Control find = Entries.Where(x => x.Name == ent.InPanel && x.Control != null).Select(y => y.Control).FirstOrDefault();
                    if (find == null)
                        find = Entries.Where(x => x.Name + ".1" == ent.InPanel && x.Control is SplitContainer).Select(y => ((SplitContainer)y.Control).Panel1).FirstOrDefault();
                    if (find == null)
                        find = Entries.Where(x => x.Name + ".2" == ent.InPanel && x.Control is SplitContainer).Select(y => ((SplitContainer)y.Control).Panel2).FirstOrDefault();

                    if (find != null)
                    {
                        if (find is ExtPanelVertScrollWithBar)    // if its in this one, we actually want to place in child (its wrapping an ExtPanelVertScroll)
                            find.Controls[0].Controls.Add(c);
                        else
                            find.Controls.Add(c);
                    }
                    else
                    {
                        System.Diagnostics.Trace.WriteLine($"ConfigurableEntries cannot find {ent.Name} in {ent.InPanel}, ensure panel is named correctly and already made before this entry");
                        contentpanel.Controls.Add(c);       // add to content panel so we don't crash the rest of the system
                    }
                }
                else
                {
                    Panel toaddto = (ent.PlacedInPanel == ConfigurableEntryList.Entry.PanelType.Top && toppanel != null) ? toppanel :
                                    (ent.PlacedInPanel == ConfigurableEntryList.Entry.PanelType.Bottom && bottompanel != null) ? bottompanel :
                                    contentpanel;

                    toaddto.Controls.Add(c);
                }

                if (ent.ToolTip.HasChars() && tooltipcontrol != null)
                    tooltipcontrol.SetToolTip(c, ent.ToolTip);

                //        //System.Diagnostics.Debug.WriteLine($".. Control {ent.Name} of {c.GetType()} at {c.Location} {c.Size}");

                if (c is Label)
                {
                    Label l = c as Label;
                    if (ent.TextValue != null)      // may be externally set up
                        l.Text = ent.TextValue;
                    if (ent.TextAlign.HasValue)
                        l.TextAlign = ent.TextAlign.Value;
                    l.MouseDown += (md1, md2) => { MouseUpDownOnLabelOrPanel?.Invoke(true, (Control)md1, md2); };
                    l.MouseUp += (md1, md2) => { MouseUpDownOnLabelOrPanel?.Invoke(false, (Control)md1, md2); };
                }
                else if (c is ExtButton)
                {
                    ExtButton b = c as ExtButton;

                    if (ent.TextValue != null)     // it may be an external button which has set up text/image, if so, ent.TextValue = null
                        SetTextOrImage(ent, b);

                    if (ent.TextAlign.HasValue)
                        b.TextAlign = ent.TextAlign.Value;

                    if (c is ExtButtonWithNewCheckedListBox)    // this is an extended button
                    {
                        if (ent.DropDownButtonList != null)     // if we are creating using ent, do it.  Else we may be creating externally so don't re-init
                        {
                            ExtButtonWithNewCheckedListBox cb = c as ExtButtonWithNewCheckedListBox;
                            cb.Init(ent.DropDownButtonList, ent.ButtonSettings,
                                    (s, b1) => { SendTrigger(ent.Name, "DropDownButtonClosed:" + s, s); },
                                    ent.AllOrNoneShown, ent.AllOrNoneBack,
                                    null, //disabled
                                    ent.ImageSize,
                                    new Size(16, 16), // screenmargin
                                    new Size(64, 64), // close boundary
                                    ent.MultiColumns,
                                    null, // group, not needed
                                    ent.SortItems,
                                    (s, eb1) => { SendTrigger(ent.Name, "DropDownButtonPressed:" + s, s); });
                        }
                    }
                    else
                    {
                        b.Click += (sender, ev) =>
                        {
                            SendTrigger(ent.Name);
                        };
                    }
                }
                else if (c is NumberBoxDouble)
                {
                    NumberBoxDouble cb = c as NumberBoxDouble;
                    if (ent.NumberBoxDoubleMinimum.HasValue)
                        cb.Minimum = ent.NumberBoxDoubleMinimum.Value;
                    if (ent.NumberBoxDoubleMaximum.HasValue)
                        cb.Maximum = ent.NumberBoxDoubleMaximum.Value;
                    if (ent.DoubleValue.HasValue)
                        cb.Value = ent.DoubleValue.Value;
                    else if (ent.TextValue != null)
                        cb.Value = ent.TextValue.InvariantParseDoubleNull() ?? cb.Minimum;

                    if (ent.NumberBoxFormat != null)
                        cb.Format = ent.NumberBoxFormat;
                    cb.ReturnPressed += (box) =>
                    {
                        SwallowReturn = false;
                        SendTrigger(ent.Name, "Return", cb.Value);
                        return SwallowReturn;
                    };
                    cb.ValidityChanged += (box, s) =>
                    {
                        SendTrigger(ent.Name, "Validity:" + s.ToString(), cb.Value);
                    };
                    cb.ValueChanged += (box, s) =>
                    {
                        SendTrigger(ent.Name, "ValueChanged:" + cb.Value.ToString(), cb.Value);
                    };
                }
                else if (c is NumberBoxLong)
                {
                    NumberBoxLong cb = c as NumberBoxLong;
                    if (ent.NumberBoxLongMinimum.HasValue)
                        cb.Minimum = ent.NumberBoxLongMinimum.Value;
                    if (ent.NumberBoxLongMaximum.HasValue)
                        cb.Maximum = ent.NumberBoxLongMaximum.Value;
                    if (ent.LongValue.HasValue)
                        cb.Value = ent.LongValue.Value;
                    else if (ent.TextValue != null)
                        cb.Value = ent.TextValue.InvariantParseLongNull() ?? cb.Minimum;
                    if (ent.NumberBoxFormat != null)
                        cb.Format = ent.NumberBoxFormat;
                    cb.ReturnPressed += (box) =>
                    {
                        SwallowReturn = false;
                        SendTrigger(ent.Name, "Return", cb.Value);
                        return SwallowReturn;
                    };
                    cb.ValidityChanged += (box, s) =>
                    {
                        SendTrigger(ent.Name, "Validity:" + s.ToString(), cb.Value);
                    };
                    cb.ValueChanged += (box, s) =>
                    {
                        SendTrigger(ent.Name, "ValueChanged:" + cb.Value.ToString(), cb.Value);
                    };
                }
                else if (c is NumberBoxInt)
                {
                    NumberBoxInt cb = c as NumberBoxInt;
                    if (ent.NumberBoxLongMinimum.HasValue)
                        cb.Minimum = ent.NumberBoxLongMinimum == long.MinValue ? int.MinValue : (int)ent.NumberBoxLongMinimum;
                    if (ent.NumberBoxLongMaximum.HasValue)
                        cb.Maximum = ent.NumberBoxLongMaximum == long.MaxValue ? int.MaxValue : (int)ent.NumberBoxLongMaximum;
                    if (ent.LongValue.HasValue)
                        cb.Value = (int)ent.LongValue.Value;
                    else if (ent.TextValue != null)
                        cb.Value = ent.TextValue.InvariantParseIntNull() ?? cb.Minimum;
                    if (ent.NumberBoxFormat != null)
                        cb.Format = ent.NumberBoxFormat;
                    cb.ReturnPressed += (box) =>
                    {
                        SwallowReturn = false;
                        SendTrigger(ent.Name, "Return", cb.Value);
                        return SwallowReturn;
                    };
                    cb.ValidityChanged += (box, s) =>
                    {
                        SendTrigger(ent.Name, "Validity:" + s.ToString(), cb.Value);
                    };
                    cb.ValueChanged += (box, s) =>
                    {
                        SendTrigger(ent.Name, "ValueChanged:" + cb.Value.ToString(), cb.Value);
                    };
                }
                else if (c is ExtTextBox)
                {
                    ExtTextBox tb = c as ExtTextBox;
                    if (ent.TextValue != null) // may be externally set up
                        tb.Text = ent.TextValue;

                    if (ent.TextBoxMultiline.HasValue)
                        tb.Multiline = tb.WordWrap = ent.TextBoxMultiline.Value;

                    // this was here, but no idea why. removing as the multiline instances seem good
                    //tb.Size = ent.Size;     

                    if (ent.TextBoxClearOnFirstChar.HasValue)
                        tb.ClearOnFirstChar = ent.TextBoxClearOnFirstChar.Value;

                    tb.ReturnPressed += (box) =>
                    {
                        SwallowReturn = false;
                        SendTrigger(ent.Name, "Return", tb.Text);
                        return SwallowReturn;
                    };

                    if (tb.ClearOnFirstChar)
                        tb.SelectEnd();
                }
                else if (c is ExtRichTextBox)
                {
                    ExtRichTextBox tb = c as ExtRichTextBox;
                    if (ent.TextValue != null) // may be externally set up
                        tb.Text = ent.TextValue;
                }
                else if (c is ExtCheckBox)
                {
                    ExtCheckBox cb = c as ExtCheckBox;

                    if (ent.TextValue != null) // may be externally set up
                    {
                        if (SetTextOrImage(ent, cb))
                            cb.Appearance = Appearance.Button;
                    }

                    cb.Checked = ent.CheckBoxChecked;
                    cb.CheckAlign = ent.ContentAlign;
                    cb.Click += (sender, ev) =>
                    {
                        SendTrigger(ent.Name, null, cb.CheckState, cb.Checked.ToStringIntValue());       // backwards compatible, trigger does not send value
                    };
                }
                else if (c is ExtRadioButton)
                {
                    ExtRadioButton cb = c as ExtRadioButton;

                    cb.Text = ent.TextValue ?? "Unknown";
                    cb.Checked = ent.CheckBoxChecked;
                    cb.CheckAlign = ent.ContentAlign;
                    cb.Click += (sender, ev) =>
                    {
                        SendTrigger(ent.Name, null, cb.Checked, cb.Checked.ToStringIntValue());       // backwards compatible, trigger does not send value
                    };
                }
                else if (c is ExtDateTimePicker)
                {
                    ExtDateTimePicker dt = c as ExtDateTimePicker;

                    if (ent.TextValue != null)      // if text set, use it to set value
                    {
                        if (DateTime.TryParse(ent.TextValue, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out DateTime t))     // assume local, so no conversion
                            dt.Value = t;
                        else
                            dt.Value = DateTime.MinValue;
                    }
                    else if (ent.DateTimeValue.HasValue)    // else if this is set, use this
                        dt.Value = ent.DateTimeValue.Value;

                    if (ent.CustomDateFormat != null)
                    {
                        switch (ent.CustomDateFormat.ToLowerInvariant())
                        {
                            case "short":
                                dt.Format = DateTimePickerFormat.Short;
                                break;
                            case "long":
                                dt.Format = DateTimePickerFormat.Long;
                                break;
                            case "time":
                                dt.Format = DateTimePickerFormat.Time;
                                break;
                            default:
                                dt.CustomFormat = ent.CustomDateFormat;
                                break;
                        }
                    }

                    dt.ValueChanged += (s, e) => { SendTrigger(ent.Name, "ValueChanged:" + dt.Value.ToStringZuluInvariant(), dt.Value); };
                }
                else if (c is ExtComboBox)
                {
                    ExtComboBox cb = c as ExtComboBox;

                    if (ent.ComboBoxItems != null)
                        cb.Items.AddRange(ent.ComboBoxItems);

                    if (ent.TextValue != null)
                    {
                        if (cb.Items.Contains(ent.TextValue))
                            cb.SelectedItem = ent.TextValue;
                    }
                    cb.SelectedIndexChanged += (sender, ev) =>
                    {
                        SendTrigger(ent.Name, null, cb.SelectedItem);     // again backwards compatible
                    };
                }
                else if (c is ExtPanelDataGridViewScroll)
                {
                    ExtPanelDataGridViewScroll dgvs = c as ExtPanelDataGridViewScroll;
                    BaseUtils.DataGridViewColumnControl dgv = new BaseUtils.DataGridViewColumnControl();

                    if (ent.DGVRowHeaderWidth.HasValue)
                    {
                        dgv.RowHeadersVisible = ent.DGVRowHeaderWidth > 4;
                        if (dgv.RowHeadersVisible)
                            dgv.RowHeadersWidth = ent.DGVRowHeaderWidth.Value;
                    }

                    dgv.AllowUserToAddRows = false;
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgv.ScrollBars = ScrollBars.None;

                    if (ent.DGVColumns != null)
                    {
                        for (int i = 0; i < ent.DGVColumns.Count; i++)
                        {
                            dgv.Columns.Add(ent.DGVColumns[i]);
                            dgv.Columns[i].SortMode = ent.DGVSortMode[i].EqualsIIC("Off") ? DataGridViewColumnSortMode.NotSortable : DataGridViewColumnSortMode.Automatic;
                        }
                    }

                    if (ent.DGVSortMode != null)
                    {
                        dgv.SortCompare += (s, e) =>
                        {
                            string sortmode = ent.DGVSortMode[e.Column.Index];
                            if (sortmode.EqualsIIC("Alpha"))
                                e.SortDataGridViewColumnAlpha();
                            else if (sortmode.EqualsIIC("Date"))
                                e.SortDataGridViewColumnDate();
                            else if (sortmode.EqualsIIC("Numeric"))
                                e.SortDataGridViewColumnNumeric();
                            else if (sortmode.EqualsIIC("NumericAlpha"))
                                e.SortDataGridViewColumnNumericThenAlpha();
                            else if (sortmode.EqualsIIC("AlphaInt"))
                                e.SortDataGridViewColumnAlphaInt();
                            else
                                e.SortDataGridViewColumnAlpha();
                        };
                    }

                    dgvs.Controls.Add(dgv);
                    ExtScrollBar sb = new ExtScrollBar();
                    dgvs.Controls.Add(sb);

                    dgv.Tag = ent;
                    dgv.SelectionChanged += Dgv_SelectionChanged;
                    dgv.Sorted += (s1, e1) => { SendTrigger(ent.Name, "SortColumn:" + dgv.SortedColumn.Index.ToStringInvariant(), dgv.SortedColumn.Index); };
                }
                else if (c is ExtPanelVertScrollWithBar)
                {
                    ExtPanelVertScroll pinnervscroll = new ExtPanelVertScroll();
                    pinnervscroll.Dock = DockStyle.Fill;
                    c.Controls.Add(pinnervscroll);          // add inner scroll panel
                }
                else if (c is Panel)
                {
                    if (c is FlowLayoutPanel)
                    {
                        FlowLayoutPanel fp = c as FlowLayoutPanel;
                        if (ent.Horizontal.HasValue)
                            fp.FlowDirection = ent.Horizontal.Value ? FlowDirection.LeftToRight : FlowDirection.TopDown;
                    }
                    else if (c is ExtPanelRollUp)
                    {
                        ExtPanelRollUp pr = c as ExtPanelRollUp;
                        if (ent.Pinned.HasValue)
                            pr.PinState = ent.Pinned.Value;
                    }

                    c.MouseDown += (md1, md2) => { MouseUpDownOnLabelOrPanel?.Invoke(true, (Control)md1, md2); };
                    c.MouseUp += (md1, md2) => { MouseUpDownOnLabelOrPanel?.Invoke(false, (Control)md1, md2); };
                }
                else if (c is SplitContainer splitter)
                {
                    if (ent.Horizontal.HasValue)
                        splitter.Orientation = ent.Horizontal.Value ? Orientation.Horizontal : Orientation.Vertical;

                    try
                    {
                        if (ent.Panel1MinPixelSize > 0)
                            splitter.Panel1MinSize = ent.Panel1MinPixelSize;
                        if (ent.Panel2MinPixelSize > 0)
                            splitter.Panel2MinSize = ent.Panel2MinPixelSize;
                        if (ent.DoubleValue.HasValue)
                            splitter.SplitterDistance(ent.DoubleValue.Value / 100.0);
                    }
                    catch { }

                    splitter.SplitterMoved += (s2, e2) => { SendTrigger(ent.Name, "SplitterMoved:" + (splitter.GetSplitterDistance() * 100.0).ToStringInvariant(), splitter.GetSplitterDistance() * 100.0); };
                }
                else if ( c is ExtNumericUpDown nud)
                {
                    nud.Minimum = (int)ent.NumberBoxLongMinimum;
                    nud.Maximum = (int)ent.NumberBoxLongMaximum;
                    if (ent.LongValue.HasValue)
                        nud.Value = (int)ent.LongValue.Value;
                    else if (ent.TextValue != null)
                        nud.Value = ent.TextValue.InvariantParseIntNull() ?? nud.Minimum;

                    nud.ValueChanged += (s, e) => { SendTrigger(ent.Name, nud.Value.ToStringInvariant(), nud.Value); };
                }
                else
                {
                    if (ent.TextValue != null)
                        c.Text = ent.TextValue;     // rest get text value
                }

                if (autosizefactor != null)     // when adding, form scaling has already been done, so we need to scale manually
                {
                    //System.Diagnostics.Debug.WriteLine($"Auto size {autosizefactor} Current {c.Bounds}");
                    c.Scale(autosizefactor.Value);
                    //System.Diagnostics.Debug.WriteLine($"......... Now {c.Bounds}");
                }

                if (themefont != null)          // if themeing, apply it
                    Theme.Current.Apply(c, themefont);

                ent.Created = true;
            }
        }

        public bool SetTextOrImage(Entry ent, ButtonBase b)
        {
            if (ent.TextValue.StartsWith("Resource:"))
            {
                Image img = BaseUtils.ResourceHelpers.GetResourceAsImage(ent.TextValue.Substring(9));
                if (img != null)
                {
                    b.Text = "";
                    b.Image = img;
                    return true;
                }
                else
                    b.Text = "Failed load";
            }
            else if (ent.TextValue.StartsWith("File:"))
            {
                try
                {
                    b.Text = "";
                    b.Image = ent.TextValue.Substring(5).LoadBitmapNoLock();    // So the file can be released
                    return true;
                }
                catch
                {
                    b.Text = "Failed load";
                }
            }
            else
            {
                b.Image = null;
                b.Text = ent.TextValue;
            }

            return false;
        }

        private void Dgv_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            Entry ent = dgv.Tag as Entry;
            var rows = dgv.SelectedRows;
            if (rows.Count > 0)
            {
                // System.Diagnostics.Debug.WriteLine($"Entry {ent.Name} changed selection Rows {rows.Count}");
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DataGridViewRow r in rows)
                    sb.AppendPrePad(r.Index.ToStringInvariant(), ";");
                SendTrigger(ent.Name, "RowSelection:" + sb.ToString(), sb.ToString());
            }
            else
            {
                var cells = dgv.SelectedCells;
                //  System.Diagnostics.Debug.WriteLine($"Entry {ent.Name} changed selection Cells {cells.Count}");
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DataGridViewCell c in cells)
                    sb.AppendPrePad(c.RowIndex.ToStringInvariant() + "," + c.ColumnIndex.ToStringInvariant(), ";");
                SendTrigger(ent.Name, "CellSelection:" + sb.ToString(), sb.ToString());
            }
        }

        #endregion

    }
}
