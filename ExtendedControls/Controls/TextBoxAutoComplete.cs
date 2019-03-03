/*
 * Copyright © 2016-2019 EDDiscovery development team
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
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ExtTextBoxAutoComplete : ExtTextBox
    {
        #region Public IF

        // programtic change of text does not make autocomplete execute.
        public override string Text { get { return base.Text; } set { tempdisableauto = true; base.Text = value; tempdisableauto = false; } }

        public int DropDownWidth { get; set; } = 0;     // means auto size
        public int DropDownHeight { get; set; } = 200;
        public int DropDownItemHeight { get; set; } = 13;
        public Color DropDownBackgroundColor { get; set; } = Color.Gray;
        public Color DropDownBorderColor { get; set; } = Color.Green;
        public Color DropDownScrollBarColor { get; set; } = Color.LightGray;
        public Color DropDownScrollBarButtonColor { get; set; } = Color.LightGray;
        public Color DropDownMouseOverBackgroundColor { get; set; } = Color.Red;
        public FlatStyle FlatStyle { get; set; } = FlatStyle.System;

        // use EndButtonEnable to turn on autocompleter button

        public delegate List<string> PerformAutoComplete(string input, ExtTextBoxAutoComplete t);
        private PerformAutoComplete AutoCompleteFunction { get { return autoCompleteFunction; } set { autoCompleteFunction = value; EndButtonEnable = value != null; } }
        public void SetAutoCompletor(PerformAutoComplete p) { AutoCompleteFunction = p; }  // older interface

        public ExtTextBoxAutoComplete() : base()
        {
            TextChanged += TextChangeEventHandler;
            waitforautotimer = new System.Windows.Forms.Timer();
            waitforautotimer.Interval = 200;
            waitforautotimer.Tick += TimeOutTick;
            HandleDestroyed += AutoCompleteTextBox_HandleDestroyed;

            this.Click += AutoCompleteTextBox_Click;
            this.KeyDown += AutoCompleteTextBox_KeyDown;

            EndButtonEnable = false;

            EndButtonClick = (s) =>                            // this is the default action if the user turns on the drop down button in text box
            {
                if ( cbdropdown == null )                           // if off, clear field and autocomplete nothing
                {
                    Text = "";
                    AutoComplete("");
                }
                else
                {
                    CancelAutoComplete();                           // rollup
                }
            };
        }

        public void AutoComplete(string autocomplete)           // call to autocomplete this
        {
            autocompletestring = autocomplete;
            TimeOutTick(null, null);
        }

        public void CancelAutoComplete()        // Sometimes, the user is quicker than the timer, and has commited to a selection before the results even come back.
        {
            if (waitforautotimer.Enabled)
                waitforautotimer.Stop();

            if (cbdropdown != null)
            {
                cbdropdown.Close();
                cbdropdown = null;
                Invalidate(true);
            }

            EndButtonImage = Properties.Resources.ArrowDown;
        }

        #endregion

        #region Implementation

        private void AutoCompleteTextBox_HandleDestroyed(object sender, EventArgs e)
        {
            if (autoCompleteFunction != null)
            {
                waitforautotimer.Stop();
                restartautocomplete = false;

                if (ThreadAutoComplete != null && ThreadAutoComplete.IsAlive)
                {
                    ThreadAutoComplete.Join();
                }
            }
        }

        private void TextChangeEventHandler(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("{0} text change event", Environment.TickCount % 10000);
            if (autoCompleteFunction != null && !tempdisableauto)
            {
                if (!executingautocomplete)
                {
                    //System.Diagnostics.Debug.WriteLine("{0} Start timer", Environment.TickCount % 10000);
                    waitforautotimer.Stop();
                    waitforautotimer.Start();
                    autocompletestring = String.Copy(this.Text);    // a copy in case the text box changes it after complete starts
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine("{0} in ac, go again", Environment.TickCount % 10000);
                    autocompletestring = String.Copy(this.Text);
                    restartautocomplete = true;
                }
            }
        }

        private void TimeOutTick(object sender, EventArgs e)
        {
            waitforautotimer.Stop();
            executingautocomplete = true;

            ThreadAutoComplete = new System.Threading.Thread(new System.Threading.ThreadStart(AutoComplete));
            ThreadAutoComplete.Name = "AutoComplete";
            ThreadAutoComplete.Start();
        }

        private void AutoComplete()
        {
            do
            {
                //System.Diagnostics.Debug.WriteLine("{0} Begin AC", Environment.TickCount % 10000);
                restartautocomplete = false;
                autocompletestrings = autoCompleteFunction(string.Copy(autocompletestring), this);    // pass a copy, in case we change it out from under it
                //System.Diagnostics.Debug.WriteLine("{0} finish func ret {1} restart {2}", Environment.TickCount % 10000, autocompletestrings.Count, restartautocomplete);
            } while (restartautocomplete == true);

            this.BeginInvoke((MethodInvoker)delegate { AutoCompleteFinished(); });
        }

        private void AutoCompleteFinished()
        {
            //System.Diagnostics.Debug.WriteLine("{0} Show results {1}", Environment.TickCount % 10000, autocompletestrings.Count);
            executingautocomplete = false;

            int count = autocompletestrings.Count;

            if ( count > 0)
            {
                if ( cbdropdown != null && (autocompletelastcount < count || autocompletelastcount > count+5))
                {                               // close if the counts are wildly different
                    cbdropdown.Close();
                    cbdropdown = null;
                }

                if (cbdropdown == null)
                {
                    cbdropdown = new ExtListBoxForm("", false);

                    int fittableitems = this.DropDownHeight / this.DropDownItemHeight;

                    if (fittableitems == 0)
                    {
                        fittableitems = 5;
                    }

                    if (fittableitems > autocompletestrings.Count())                             // no point doing more than we have..
                        fittableitems = autocompletestrings.Count();

                    cbdropdown.Size = new Size(this.DropDownWidth > 0 ? this.DropDownWidth : this.Width, fittableitems * this.DropDownItemHeight + 4);

                    cbdropdown.SelectionBackColor = this.DropDownBackgroundColor;
                    cbdropdown.ForeColor = this.ForeColor;
                    cbdropdown.BackColor = this.DropDownBorderColor;
                    cbdropdown.BorderColor = this.DropDownBorderColor;
                    cbdropdown.Items = autocompletestrings;
                    cbdropdown.ItemHeight = this.DropDownItemHeight;
                    cbdropdown.SelectedIndex = 0;
                    cbdropdown.FlatStyle = this.FlatStyle;
                    cbdropdown.Font = this.Font;
                    cbdropdown.ScrollBarColor = this.DropDownScrollBarColor;
                    cbdropdown.ScrollBarButtonColor = this.DropDownScrollBarButtonColor;
                    cbdropdown.MouseOverBackgroundColor = this.DropDownMouseOverBackgroundColor;
                    cbdropdown.Activated += cbdropdown_DropDown;
                    cbdropdown.SelectedIndexChanged += cbdropdown_SelectedIndexChanged;

                    EndButtonImage = Properties.Resources.ArrowUp;
                    cbdropdown.Show(FindForm());
                    Focus();                // Major change.. we now keep the focus at all times
                }
                else
                {
                    cbdropdown.Items.Clear();
                    cbdropdown.Items = autocompletestrings;
                    cbdropdown.Refresh();
                }

                autocompletelastcount = count;
            }
            else
            {
                CancelAutoComplete();
            }
        }

        private void cbdropdown_DropDown(object sender, EventArgs e)
        {
            Point location = this.PointToScreen(new Point(0, 0));
            cbdropdown.Location = new Point(location.X, location.Y + this.Height );
            this.Invalidate(true);
        }

        private void cbdropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedindex = cbdropdown.SelectedIndex;
            if (selectedindex >= 0 && selectedindex < cbdropdown.Items.Count)
            {
                this.Text = cbdropdown.Items[selectedindex];
                this.Select(this.Text.Length, this.Text.Length);

                CancelAutoComplete();
                Focus();
            }
        }

        private void AutoCompleteTextBox_Click(object sender, EventArgs e)      // clicking on the text box cancels autocomplete
        {
            CancelAutoComplete();
        }

        // keys when WE have to focus, which we do all the time now, some need passing onto the control.

        private void AutoCompleteTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("{0} Keypress {1}", Environment.TickCount % 10000 , e.KeyCode);

            if (cbdropdown != null)        // pass certain keys to the shown drop down
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                {
                    cbdropdown.KeyDownAction(e);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    CancelAutoComplete();
                }
            }
        }

        #endregion

        #region Internal Vars

        private System.Windows.Forms.Timer waitforautotimer;
        private bool executingautocomplete = false;
        private string autocompletestring;
        private bool restartautocomplete = false;
        private System.Threading.Thread ThreadAutoComplete;
        private List<string> autocompletestrings = null;
        ExtListBoxForm cbdropdown;
        int autocompletelastcount = 0;
        private bool tempdisableauto = false;
        private PerformAutoComplete autoCompleteFunction = null;

        #endregion
    }

}
