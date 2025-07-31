/*
 * Copyright © 2019-2024 EDDiscovery development team
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

using ExtendedForms;
using System;
using System.Windows.Forms;

namespace ExtendedForms
{
    public partial class ImportExportForm : ExtendedControls.DraggableForm
    {
        public int SelectedIndex { get; private set; }      // item selected
        public DateTime StartTime { get { return customDateTimePickerFrom.Value; } }        // time picker here is Kind-less, its up to you do control that aspect
        public DateTime EndTime { get { return customDateTimePickerTo.Value; } }
        public string Delimiter { get { return extRadioButtonTab.Checked ? "\t" : radioButtonComma.Checked ? "," : ";"; } }
        public bool AutoOpen { get { return checkBoxCustomAutoOpen.Checked; } }
        public bool IncludeHeader { get { return checkBoxIncludeHeader.Checked; } }
        public bool ExcludeHeader { get { return extCheckBoxExcludeHeader.Checked; } }
        public string Path { get; private set; }                // for imports or export, path selected.   For imports, non null means it a file, else may be text..
        public string ImportText { get; private set; }        // for imports, with an import box, text if set, else Null meaning its a file
        public string SaveImportAs { get { return extTextBoxSaveImport.Text; } }    // non empty meaning wants to save
        public ImportExportForm()
        {
            InitializeComponent();
        }

        [Flags]
        public enum ShowFlags
        {
            None = 0,                // disable datetime, cvs, open/include, paste, etc

            ShowDateTime = 1,        // Date time picker
            ShowCSV = 2,             // CSV separ
            ShowOpenInclude = 4,     // the open and include options are shown
            ShowPaste = 8,           // paste box is shown
            ShowExcludeHeader = 16,  // for CSV output
            ShowImportSaveas = 32,   // for import data saving
            ShowUTCIndicators = 64,  // for date time

            ShowCSVOpenInclude = 6,
            ShowDateTimeOpenInclude = 5,
            ShowDateTimeCSVOpenInclude = 7,
            ShowDateTimeUTCCSVOpenInclude = 7+64,

            ShowImportOptions = 2+8+16,     // paste window, plus csv, exclude header
        }

        // Init, export or import
        // selectionlist: Is the text to present in the drop down. frm.SelectedIndex tells you what they picked
        // showflags : options per selection, see show flags above.  If less, they are modulo into the array so you can give 1. If null, its ExportCSV
        // outputext : per selection, the dialog formatted list of extensions ie. "JSON|*.json|All|*.*", or null = csv.  Modulo in. If null, its CSV 
        // suggestedfilenames : name given per selection as the suggested name. Modulo in. If null, blank

        public void Export(string[] selectionlistp, ShowFlags[] showflagsp = null, string[] outputextp = null, string[] suggestedfilenamesp = null,
                            DateTime? pickerfromtimesel = null, DateTime? pickertotimesel = null, bool includeheader = true, bool autoopen = true, System.Drawing.Icon deficon = null)
        {
            Init(false, selectionlistp, showflagsp, outputextp, suggestedfilenamesp,pickerfromtimesel,pickertotimesel,includeheader,autoopen,deficon);
        }
        public void Import(string[] selectionlistp, ShowFlags[] showflagsp = null, string[] inputextp = null, System.Drawing.Icon deficon = null)
        {
            Init(true, selectionlistp, showflagsp, inputextp, deficon:deficon);
        }

        // get text of source.. null if can't read.  reads from dragged file content, or from file
        public string ReadSource()      
        {
            if (ImportText.HasChars())
            {
                if (extRichTextBoxPaste.DraggedFile != null)
                {
                    Path = extRichTextBoxPaste.DraggedFile;
                }
                else
                    Path = "Text";

                return ImportText;
            }
            else
            {
                string str = BaseUtils.FileHelpers.TryReadAllTextFromFile(Path);
                return str;
            }
        }

        // import, read into CSVFile, error is in ErrorMessage and null is returned
        public BaseUtils.CSVFile CSVRead()
        {
            string str = ReadSource();
            if (str != null)
            {
                BaseUtils.CSVFile csv = new BaseUtils.CSVFile(Delimiter);
                if (csv.ReadString(str))
                    return csv;
            }

            return null;
        }

        #region Implementation

        protected void Init(bool import, string[] selectionlistp, ShowFlags[] showflagsp = null, string[] outputextp = null, string[] suggestedfilenamesp = null,
                                DateTime? pickerfromtimesel = null, DateTime? pickertotimesel = null, bool includeheader = true, bool autoopen = true, 
                                System.Drawing.Icon deficon = null)
        {
            if (deficon != null)
                Icon = deficon;

            if (showflagsp == null)
                showflagsp = new ShowFlags[] { ShowFlags.ShowDateTimeCSVOpenInclude };

            if (showflagsp.Length >= selectionlistp.Length)     // if we have at least as many flags as selection list
                showflags = showflagsp;
            else
            {
                showflags = new ShowFlags[selectionlistp.Length];
                for (int i = 0; i < showflags.Length; i++)
                    showflags[i] = showflagsp[i % showflagsp.Length];       // modulo it
            }

            if (outputextp == null)
                outputextp = new string[] { "CSV| *.csv" };

            if (outputextp.Length >= selectionlistp.Length)     // if we have at least as many..
                outputext = outputextp;
            else
            {
                outputext = new string[selectionlistp.Length];
                for (int i = 0; i < outputext.Length; i++)
                    outputext[i] = outputextp[i % outputextp.Length];       // modulo it
            }

            if (suggestedfilenamesp == null)
                suggestedfilenamesp = new string[] {""};

            if (suggestedfilenamesp.Length >= selectionlistp.Length)        // if we have at least as many..
                suggestedfilenames = suggestedfilenamesp;
            else
            {
                suggestedfilenames = new string[selectionlistp.Length];
                for (int i = 0; i < suggestedfilenames.Length; i++)
                    suggestedfilenames[i] = suggestedfilenamesp[i % suggestedfilenamesp.Length];       // modulo it
            }

            BaseUtils.TranslatorMkII.Instance.TranslateControls(this);

            label_index.Text = this.Text;

            comboBoxSelectedType.Items.AddRange(selectionlistp);
            comboBoxSelectedType.SelectedIndex = 0;
            comboBoxSelectedType.SelectedIndexChanged += ComboBoxSelectedType_SelectedIndexChanged;

            // note we don't care what the picker has as its Kind.. the convert functions at the top force it into the right mode
            customDateTimePickerFrom.Value = pickerfromtimesel.HasValue ? pickerfromtimesel.Value : new DateTime(2000, 1, 1);
            customDateTimePickerTo.Value = pickerfromtimesel.HasValue ? pickertotimesel.Value : DateTime.Now.EndOfDay();

            if (System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.Equals("."))
                radioButtonComma.Checked = true;
            else
                radioButtonSemiColon.Checked = true;

            checkBoxIncludeHeader.Checked = includeheader;
            checkBoxCustomAutoOpen.Checked = autoopen;

            var theme = ExtendedControls.Theme.Current;
            bool winborder = theme?.ApplyDialog(this) ?? true;

            panelTop.Visible = !winborder;                  // panel with open/close
            if (winborder)
                Height -= panelTop.Height;

            int merged = 0;
            foreach (var x in showflags.EmptyIfNull())      // merge all flags selected together so we can see the total flag status
                merged |= (int)x;

            if ((merged & (int)ShowFlags.ShowDateTime) == 0)
                Height -= panelDate.Height;
            if ((merged & (int)ShowFlags.ShowOpenInclude) == 0)
                Height -= panelIncludeOpen.Height;
            if ((merged & (int)ShowFlags.ShowCSV) == 0)
                Height -= panelCSV.Height;
            if ((merged & (int)ShowFlags.ShowPaste) == 0)
                Height -= panelPasteImport.Height;
            if ((merged & (int)ShowFlags.ShowExcludeHeader) == 0)
                Height -= panelImportExclude.Height;
            if ((merged & (int)ShowFlags.ShowImportSaveas) == 0)
                Height -= panelSaveImportAs.Height;

            labelUTCEnd.Visible = labelUTCStart.Visible = (merged & (int)ShowFlags.ShowUTCIndicators) != 0;

            SetVisibility();
            
            importdialog = import;
            if (import)
            {
                this.Text = "Import data".Tx();
                buttonExport.Text = "Import".Tx();
            }

            extRichTextBoxPaste.InstallStandardDragDrop();

            label_index.Text = this.Text;

            extButtonOptButton.Visible = false;
        }

        public void ShowOptionalButton(string text, Action pressed)
        {
            extButtonOptButton.Text = text;
            extButtonOptButton.Click += (s, e) => { pressed?.Invoke(); };
            extButtonOptButton.Visible = true;
        }

        private void ComboBoxSelectedType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetVisibility();
        }

        private void SetVisibility()
        {
            panelDate.Visible = (showflags[comboBoxSelectedType.SelectedIndex] & ShowFlags.ShowDateTime) != 0;
            panelIncludeOpen.Visible = (showflags[comboBoxSelectedType.SelectedIndex] & ShowFlags.ShowOpenInclude) != 0;
            panelCSV.Visible = (showflags[comboBoxSelectedType.SelectedIndex] & ShowFlags.ShowCSV) != 0;
            panelPasteImport.Visible = (showflags[comboBoxSelectedType.SelectedIndex] & ShowFlags.ShowPaste) != 0;
            panelImportExclude.Visible = (showflags[comboBoxSelectedType.SelectedIndex] & ShowFlags.ShowExcludeHeader) != 0;
            panelSaveImportAs.Visible = (showflags[comboBoxSelectedType.SelectedIndex] & ShowFlags.ShowImportSaveas) != 0;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            SelectedIndex = comboBoxSelectedType.SelectedIndex;

            if (importdialog)
            {
                if (!extRichTextBoxPaste.Text.HasChars())       // if no import text, need a file browse
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.Filter = outputext[SelectedIndex];
                    dlg.Title = "Import data".Tx() + " " + dlg.Filter;

                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        Path = dlg.FileName;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        DialogResult = DialogResult.Cancel;
                        Close();
                    }
                }
                else
                {
                    ImportText = extRichTextBoxPaste.Text;
                    DialogResult = DialogResult.OK;
                    Close();
                }

                OnImportClick();
            }
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();

                dlg.Filter = outputext[SelectedIndex];
                dlg.Title = "Export data".Tx() + " " + dlg.Filter;
                dlg.FileName = suggestedfilenames[SelectedIndex];

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    Path = dlg.FileName;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                }

                OnExportClick();
            }
        }

        protected virtual void OnExportClick()
        {

        }
        protected virtual void OnImportClick()
        {

        }

        private void extRichTextBoxPaste_TextBoxChanged(object sender, EventArgs e)
        {
            if (extRichTextBoxPaste.Text.Length > 0)
            {
                char delimitier = BaseUtils.CSVRead.FindDelimiter(extRichTextBoxPaste.Text);
                if (delimitier == ',')
                    radioButtonComma.Checked = true;
                else if (delimitier == ';')
                    radioButtonSemiColon.Checked = true;
                else if (delimitier == '\t')
                    extRadioButtonTab.Checked = true;
            }
        }
        private void buttonPasteData_Click(object sender, EventArgs e)
        {
            try
            {
                if (Clipboard.ContainsText())
                {
                    string text = Clipboard.GetText();
                    extRichTextBoxPaste.Text = text;
                }
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("Unable to access clipboard");
            }

        }

        private void label_index_MouseDown(object sender, MouseEventArgs e)
        {
            OnCaptionMouseDown((Control)sender, e);
        }

        private void label_index_MouseUp(object sender, MouseEventArgs e)
        {
            OnCaptionMouseUp((Control)sender, e);
        }

        private void panel_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel_minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            OnCaptionMouseDown((Control)sender, e);
        }

        private void panelTop_MouseUp(object sender, MouseEventArgs e)
        {
            OnCaptionMouseUp((Control)sender, e);
        }

        private bool importdialog;
        private string[] outputext;
        private string[] suggestedfilenames;
        private ShowFlags[] showflags;

        #endregion
    }
}
