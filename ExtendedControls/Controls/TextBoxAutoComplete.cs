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
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace ExtendedControls
{
    public class ExtTextBoxAutoComplete : ExtTextBox
    {
        #region Public IF

        // either use TextChanged event
        // or ReturnPressed

        // programatic change of text does not make autocomplete execute.
        public override string Text { get { return base.Text; } set { tempdisableauto = true; base.Text = value; tempdisableauto = false; } }
        // unless done by this one
        public string TextChangedEvent { get { return base.Text; } set { base.Text = value; } }
        public int AutoCompleteTimeout { get { return waitforautotimer.Interval; } set { waitforautotimer.Interval = value; } }
        public Color DropDownBackgroundColor { get; set; } = Color.Gray;
        public Color DropDownBorderColor { get; set; } = Color.Green;
        public Color DropDownScrollBarColor { get; set; } = Color.LightGray;
        public Color DropDownScrollBarButtonColor { get; set; } = Color.LightGray;
        public Color DropDownMouseOverBackgroundColor { get; set; } = Color.Red;
        public FlatStyle FlatStyle { get; set; } = FlatStyle.System;

        public bool InDropDown { get { return cbdropdown != null; } }

        // use EndButtonEnable to turn on autocompleter button

        public delegate void PerformAutoComplete(string input, ExtTextBoxAutoComplete t, SortedSet<string> set);
        private PerformAutoComplete AutoCompleteFunction { get { return autoCompleteFunction; } set { autoCompleteFunction = value; EndButtonEnable = value != null; } }
        public void SetAutoCompletor(PerformAutoComplete p) { AutoCompleteFunction = p; }  // older interface
        public void SetAutoCompletor(PerformAutoComplete p, bool endbuttonvisible ) { AutoCompleteFunction = p; EndButtonVisible = endbuttonvisible; }
        public string AutoCompleteCommentMarker { get; set; } = null;       // text after this is comments not autocomplete text

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Image EndButtonImage { get { return base.EndButtonImage; } set { base.EndButtonImage = value; } }

        // OnReturnPressed is sent on return or on click

        public ExtTextBoxAutoComplete() : base()
        {
            TextChanged += TextChangeEventHandler;
            waitforautotimer = new System.Windows.Forms.Timer();
            waitforautotimer.Interval = 500;
            waitforautotimer.Tick += TimeOutTick;
            HandleDestroyed += AutoCompleteTextBox_HandleDestroyed;

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

            EndButtonImage = Properties.Resources.ArrowDown;
        }

        public void AutoComplete(string autocomplete)           // call to autocomplete this
        {
            autocompletestring = autocomplete;
            TimeOutTick(null, null);
        }

        public void CancelAutoComplete()                     // Sometimes, the user is quicker than the timer, and has commited to a selection before the results even come back.
        {
            System.Diagnostics.Debug.WriteLine("{0} Cancel autocomplete", Environment.TickCount % 10000);
            if (waitforautotimer.Enabled)
            {
                System.Diagnostics.Debug.WriteLine($".. timer running");
                waitforautotimer.Stop();
            }

            if (cbdropdown != null)
            {
                System.Diagnostics.Debug.WriteLine($".. drop down close");
                cbdropdown.Close();
                cbdropdown.Dispose();
                cbdropdown = null;
                Invalidate(true);
            }

            if (executingautocomplete)
            {
                System.Diagnostics.Debug.WriteLine($".. autocomplete happening, can't stop it, so ignore results");
                restartautocomplete = false;        // we may have been already in a restart..
                ignoreautocomplete = true;
            }
            else
            {
                ignoreautocomplete = false;
                restartautocomplete = false;
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
            //System.Diagnostics.Debug.WriteLine("AC {0} text change event", Environment.TickCount % 10000);
            if (autoCompleteFunction != null && !tempdisableauto)
            {
                if (!executingautocomplete)
                {
                    System.Diagnostics.Debug.WriteLine("{0} Text Change start timer", Environment.TickCount % 10000);
                    waitforautotimer.Stop();
                    waitforautotimer.Start();
                    autocompletestring = String.Copy(this.Text);    // a copy in case the text box changes it after complete starts
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("{0} Text change, AC active, change string and restart auto complete", Environment.TickCount % 10000);
                    autocompletestring = String.Copy(this.Text);
                    restartautocomplete = true;
                }
            }
        }

        private void TimeOutTick(object sender, EventArgs e)
        {
            waitforautotimer.Stop();
            executingautocomplete = true;
            EndButtonImage = Properties.Resources.Wait;

            System.Diagnostics.Debug.WriteLine("{0} Time out tick Start Autocomplete thread", Environment.TickCount % 10000);
            ThreadAutoComplete = new System.Threading.Thread(new System.Threading.ThreadStart(AutoComplete));
            ThreadAutoComplete.Name = "AutoComplete";
            ThreadAutoComplete.Start();
        }

        private void AutoComplete()
        {
            do
            {
                System.Diagnostics.Debug.WriteLine("{0} Begin AC", Environment.TickCount % 10000);
                restartautocomplete = false;
                autocompletestrings = new SortedSet<string>();
                autoCompleteFunction(string.Copy(autocompletestring), this, autocompletestrings);    // pass a copy, in case we change it out from under it
                System.Diagnostics.Debug.WriteLine("{0} AC Finished ret {1} restart {2}", Environment.TickCount % 10000, autocompletestrings.Count, restartautocomplete);
            } while (restartautocomplete == true);

            this.BeginInvoke((MethodInvoker)delegate { AutoCompleteFinished(); });
        }

        private void AutoCompleteFinished()
        {
            System.Diagnostics.Debug.WriteLine("{0} AC show dropdown results {1} ignore {2}", Environment.TickCount % 10000, autocompletestrings.Count, ignoreautocomplete);

            executingautocomplete = false;      // we have finished - do now since we call cancelautocomplete

            int count = autocompletestrings.Count;

            if (count > 0 && !ignoreautocomplete)
            {
                if (cbdropdown != null && (autocompletelastcount < count || autocompletelastcount > count + 5))
                {                               // close if the counts are wildly different
                    cbdropdown.Close();
                    cbdropdown.Dispose();
                    cbdropdown = null;
                }

                if (cbdropdown == null)
                {
                    cbdropdown = new ExtListBoxForm("", false);

                    cbdropdown.SelectionBackColor = this.DropDownBackgroundColor;
                    cbdropdown.ForeColor = this.ForeColor;
                    cbdropdown.BackColor = this.DropDownBorderColor;
                    cbdropdown.BorderColor = this.DropDownBorderColor;
                    cbdropdown.Items = autocompletestrings.ToList();
                    cbdropdown.SelectedIndex = 0;
                    cbdropdown.FlatStyle = this.FlatStyle;
                    cbdropdown.Font = this.Font;
                    cbdropdown.ScrollBarColor = this.DropDownScrollBarColor;
                    cbdropdown.ScrollBarButtonColor = this.DropDownScrollBarButtonColor;
                    cbdropdown.MouseOverBackgroundColor = this.DropDownMouseOverBackgroundColor;
                    cbdropdown.SelectedIndexChanged += cbdropdown_SelectedIndexChanged;
                    cbdropdown.PositionBelow(this);
                    EndButtonImage = Properties.Resources.ArrowUp;
                    cbdropdown.Show(FindForm());
                    Focus();                // Major change.. we now keep the focus at all times

                    if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                    {
                        cbdropdown.Activated += cbdropdown_Activated;
                    }
                }
                else
                {
                    cbdropdown.Items.Clear();
                    cbdropdown.Items = autocompletestrings.ToList();
                    cbdropdown.Refresh();
                }

                autocompletelastcount = count;
            }
            else
            {
                CancelAutoComplete();
            }

            ignoreautocomplete = false;
        }

        private void cbdropdown_Activated(object sender, EventArgs e)
        {
            Focus();
        }

        private void cbdropdown_SelectedIndexChanged(object sender, EventArgs e, bool key)
        {
            int selectedindex = cbdropdown.SelectedIndex;
            if (selectedindex >= 0 && selectedindex < cbdropdown.Items.Count)
            {
                string txt = cbdropdown.Items[selectedindex];

                if ( AutoCompleteCommentMarker != null )        // comment marker removes text after it
                {
                    int index = txt.IndexOf(AutoCompleteCommentMarker);
                    if (index >= 0)
                        txt = txt.Left(index).TrimEnd();
                }

                this.Text = txt;            // firing text changed
                this.Select(this.Text.Length, this.Text.Length);

                CancelAutoComplete();
                Focus();

                if ( !key )
                    OnReturnPressed();     // click hit if we did not do it via a key
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            CancelAutoComplete();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine($"AC Click");
            CancelAutoComplete();
        }

        // keys when WE have to focus, which we do all the time now, some need passing onto the control.

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine($"AC Key down Keypress {e.KeyCode}");

            if (cbdropdown != null)        // pass certain keys to the shown drop down
            {
                System.Diagnostics.Debug.WriteLine("{0} {1} hit with drop down", Environment.TickCount % 10000, e.KeyCode);

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
            else
            {
                if ( e.KeyCode == Keys.Return )
                {
                    System.Diagnostics.Debug.WriteLine("{0} {1} hit with no drop down", Environment.TickCount % 10000, e.KeyCode);
                    CancelAutoComplete();
                }
            }
        }

        #endregion

        #region Internal Vars

        private System.Windows.Forms.Timer waitforautotimer;
        private bool executingautocomplete = false;
        private bool ignoreautocomplete = false;
        private string autocompletestring;
        private bool restartautocomplete = false;
        private System.Threading.Thread ThreadAutoComplete;
        private SortedSet<string> autocompletestrings = null;
        ExtListBoxForm cbdropdown;
        int autocompletelastcount = 0;
        private bool tempdisableauto = false;
        private PerformAutoComplete autoCompleteFunction = null;

        #endregion
    }

}
