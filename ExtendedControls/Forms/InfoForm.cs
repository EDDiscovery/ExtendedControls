/*
 * Copyright © 2016-2023 EDDiscovery development team
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
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class InfoForm : DraggableForm
    {
        public bool EnableClose { get { return buttonOK.Enabled; } set { buttonOK.Enabled = panel_close.Enabled = value; } }
        public Action<LinkClickedEventArgs> LinkClicked;

        public InfoForm()
        {
            InitializeComponent();

            BaseUtils.Translator.Instance.AddExcludedControls(new Type[]
             {   typeof(ExtendedControls.ExtComboBox), typeof(ExtendedControls.NumberBoxDouble),typeof(ExtendedControls.NumberBoxFloat),typeof(ExtendedControls.NumberBoxLong),
                typeof(ExtendedControls.ExtScrollBar),typeof(ExtendedControls.ExtStatusStrip),typeof(ExtendedControls.ExtRichTextBox),typeof(ExtendedControls.ExtTextBox),
                typeof(ExtendedControls.ExtTextBoxAutoComplete),typeof(ExtendedControls.ExtDateTimePicker),typeof(ExtendedControls.ExtNumericUpDown) });

        }

        public void Info(string title, Icon ic, string info , int[] array = null, float pointsize= -1, 
                            Action<Object> acknowledgeaction = null, Object acknowledgedata = null, bool usedialogfont= false, bool enableurls = false)    
        {
            Icon = ic;

            var enumlist = new Enum[] { ECIDs.InfoForm_buttonAcknowledge };
            BaseUtils.Translator.Instance.TranslateControls(this, enumlist);

            textBoxInfo.SetTabs(array ?? new int[] { 0, 100, 200, 300, 400, 500, 600, 800,900,1000,1100,1200 });
            textBoxInfo.ReadOnly = true;
            textBoxInfo.Select(0, 0);

            ackaction = acknowledgeaction;
            ackdata = acknowledgedata;

            Theme theme = Theme.Current;

            if (theme != null)
            {
                bool winborder = usedialogfont ? theme.ApplyDialog(this) : theme.ApplyStd(this);
                if (winborder)
                    panelTop.Visible = false;
                if (pointsize != -1)
                    textBoxInfo.Font = usedialogfont ? theme.GetDialogScaledFont(pointsize/12f) : theme.GetScaledFont(pointsize/12f);       // 12 is standard size..
            }
            else
            {
                panelTop.Visible = false;
            }

            buttonAcknowledge.Visible = ackaction != null;

            textBoxInfo.Text = info;
            labelCaption.Text = title;
            Text = title;
            textBoxInfo.DetectUrls = enableurls;
            textBoxInfo.LinkClicked += (e) => LinkClicked?.Invoke(e);
        }
        public void AddText(string text)
        {
            textBoxInfo.Text += text;
            textBoxInfo.Select(textBoxInfo.Text.Length, textBoxInfo.Text.Length);
            textBoxInfo.ScrollToCaret();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAcknowledge_Click(object sender, EventArgs e)
        {
            ackaction?.Invoke(ackdata);
            Close();
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

        private void InfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !buttonOK.Enabled;
        }

        private void InfoForm_Layout(object sender, LayoutEventArgs e)
        {
            textBoxInfo.Location = new Point(3, panelTop.Bottom + 1);
            textBoxInfo.Size = new Size(ClientRectangle.Width-6, panelBottom.Top-textBoxInfo.Top);
        }

        private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            string s = textBoxInfo.SelectedText;
            if (s.Length == 0)
                s = textBoxInfo.Text;
            //System.Diagnostics.Debug.WriteLine("Sel " + s);
            try
            {
                if (!String.IsNullOrWhiteSpace(s))
                    Clipboard.SetText(s, TextDataFormat.Text);
            }
            catch
            {
                MessageBox.Show(this, "Copying text to clipboard failed".TxID(ECIDs.InfoForm_Copyingtextfailed));
            }
        }

        private Action<Object> ackaction = null;
        private Object ackdata = null;
    }
}
