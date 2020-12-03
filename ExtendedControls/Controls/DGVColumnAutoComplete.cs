/*
 * Copyright © 2017-2019 EDDiscovery development team
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
using System.ComponentModel;
using System.Windows.Forms;

namespace ExtendedControls
{
    /// <summary>
    /// A string-backed <see cref="DataGridViewColumn"/> capable of autocompletion inside of a <see cref="DataGridView"/> control.
    /// </summary>
    public class ExtDataGridViewColumnAutoComplete : DataGridViewColumn
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ExtTextBoxAutoComplete.PerformAutoComplete AutoCompleteGenerator { get; set; }

        public ExtDataGridViewColumnAutoComplete() : base(new DataGridViewTextBoxCellAutoComplete())  // column is this type of cell
        { }

        /// <summary>
        /// Gets or sets the template used to create new cells.
        /// </summary>
        /// <value>A <see cref="DataGridViewCell"/> that all other cells in the column are modeled after. The default is <c>null</c>.</value>
        /// <exception cref="InvalidCastException">raised when <paramref name="CellTemplate"/> does not inherit from <see cref="DataGridViewTextBoxCellAutoComplete"/>.</exception>
        [Browsable(false)]
        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewTextBoxCellAutoComplete)))
                    throw new InvalidCastException($"value is not a {nameof(DataGridViewTextBoxCellAutoComplete)}");
                base.CellTemplate = value;
            }
        }

        public override object Clone()
        {
            var c = base.Clone() as ExtDataGridViewColumnAutoComplete;
            c.AutoCompleteGenerator = AutoCompleteGenerator;
            return c;
        }

        protected override void Dispose(bool disposing)
        {
            AutoCompleteGenerator = null;
            base.Dispose(disposing);
        }

        #region new text box cell with auto complete

        /// <summary>
        /// Displays editable text information in a <see cref="DataGridViewTextBoxCell"/> control
        /// and provides autocompletion facilities for <see cref="CellEditControl"/>.
        /// </summary>
        private class DataGridViewTextBoxCellAutoComplete : DataGridViewTextBoxCell
        {
            private CellEditControl celleditcontrol;

            /// <summary>
            /// Initializes a new instance of the <see cref="DataGridViewTextBoxCellAutoComplete"/> class.
            /// </summary>
            public DataGridViewTextBoxCellAutoComplete() : base()
            {
            }

            /// <summary>
            /// Gets the default value for a cell in the row for new records.
            /// </summary>
            public override object DefaultNewRowValue { get { return string.Empty; } }

            /// <summary>
            /// Gets the type of the cell's hosted editing control.
            /// </summary>
            public override Type EditType { get { return typeof(CellEditControl); } }

            /// <summary>
            /// Gets the data type of the values in the cell.
            /// </summary>
            public override Type ValueType { get { return typeof(string); } }

            /// <summary>
            /// Attaches and initializes the hosted editing control.
            /// </summary>
            /// <param name="rowIndex">The index of the row being edited.</param>
            /// <param name="initialFormattedValue">The initial value to be displayed in the control.</param>
            /// <param name="dataGridViewCellStyle">A cell style that is used to determine the appearance of the hosted control.</param>
            public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
            {
                System.Diagnostics.Debug.WriteLine("Init editing control");
                base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
                if (DataGridView.EditingControl != null)    // This should not be needed, but just in case...
                {
                    System.Diagnostics.Debug.WriteLine("Cell value " + Value);
                    celleditcontrol = DataGridView.EditingControl as CellEditControl;
                    celleditcontrol.Text = (string)(Value ?? DefaultNewRowValue);

                    // hook autocompleter from column to cell
                    if (OwningColumn != null && ((ExtDataGridViewColumnAutoComplete)OwningColumn).AutoCompleteGenerator != null)
                        celleditcontrol.SetAutoCompletor((OwningColumn as ExtDataGridViewColumnAutoComplete).AutoCompleteGenerator);
                }
            }

            /// <summary>
            /// Removes the cell's editing control from the <see cref="DataGridView"/>.
            /// </summary>
            /// <remarks>The <see cref="DataGridView"/> calls this method when the current cell hosts an editing control and editing mode ends.
            /// The method removes the <see cref="CellEditControl"/> from the <see cref="DataGridView.EditingPanel"/>, and then removes the
            /// <see cref="DataGridView.EditingPanel"/> from the <see cref="Controls"/> collection of the <see cref="DataGridView"/>.</remarks>
            public override void DetachEditingControl()
            {
                // The user can commit to a selection before AC can even generate results. If so, we need to
                // abort it, otherwise the popup appears at a random location, and it is unbound from this DGV.
                if (celleditcontrol != null)
                    celleditcontrol.CancelAutoComplete();
                base.DetachEditingControl();
            }
        }

        #endregion // CellDisplay

        #region CellEditControl

        /// <summary>
        /// Provides for autocompletion capabilities when editing data in an <see cref="ExtDataGridViewColumnAutoComplete"/>.
        /// </summary>
        public class CellEditControl : ExtTextBoxAutoComplete, IDataGridViewEditingControl
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CellEditControl"/> class.
            /// </summary>
            public CellEditControl() : base()
            {
                this.TextChanged += Control_TextChanged;
            }

            #region IDataGridViewEditingControl implementation

            /// <summary>
            /// Gets or sets the <see cref="DataGridView"/> that contains the cell.
            /// </summary>
            public DataGridView EditingControlDataGridView { get; set; }

            /// <summary>
            /// Gets or sets the formatted value of the cell being modified by the editor.
            /// </summary>
            public object EditingControlFormattedValue
            {
                get { return Text; }
                set
                {
                    if (value == null || string.IsNullOrEmpty((string)value))
                        Text = string.Empty;
                    else
                        Text = ((string)value).Trim();
                }
            }

            /// <summary>
            /// Gets or sets the index of the hosting cell's parent row.
            /// </summary>
            public int EditingControlRowIndex { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether the value of the the editing control differs form the value of the hosting cell.
            /// </summary>
            public bool EditingControlValueChanged { get; set; }

            /// <summary>
            /// Gets the cursor used when the mouse pointer is over the <see cref="DataGridView.EditingPanel"/>, but not over the editing control.
            /// </summary>
            public Cursor EditingPanelCursor { get { return base.Cursor; } }

            /// <summary>
            /// Gets a value indicating whether the cell contents need to be repositioned whenever the value changes.
            /// </summary>
            public bool RepositionEditingControlOnValueChange { get { return false; } }

            /// <summary>
            /// Changes the control's user interface to be consistent with the user's desired theme.
            /// </summary>
            /// <param name="dgvStyle">The <see cref="DataGridViewCellStyle"/> to use as the model for the UI.</param>
            public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dgvStyle)
            {
                ExtendedControls.ThemeableFormsInstance.Instance.ApplyStd(Parent);
            }

            /// <summary>
            /// Determines whether the specified key is a regular input key that the editing control should process
            /// or a special key that the parent <see cref="DataGridView"/> should process.
            /// </summary>
            /// <param name="key">A <see cref="Keys"/> item that represents the key that was pressed.</param>
            /// <param name="dgvWantsInputKey"><c>true</c> when the <see cref="DataGridView"/> wants to process the keypress; <c>false</c> otherwise.</param>
            /// <returns><c>true</c> if the specified key is a regular input key that the edit control should handle; <c>false</c> otherwise.</returns>
            public bool EditingControlWantsInputKey(Keys key, bool dgvWantsInputKey)
            {
                System.Diagnostics.Debug.WriteLine("Key " + key + " wants " + dgvWantsInputKey);
                if (key == Keys.Left || key == Keys.Right || key == Keys.Home || key == Keys.End)
                    return true;
                else if ( InDropDown && ( key == Keys.Up || key == Keys.Down || key == Keys.Escape ))
                    return true;
                else
                    return false;
            }

            /// <summary>
            /// Retrieves the formatted value of the cell.
            /// </summary>
            /// <param name="context">A bitwise combination of <see cref="DataGridViewDataErrorContexts"/> values that specifies the context in which the data is needed.</param>
            /// <returns>An <see cref="object"/> that represents the formatted version of the cell contents.</returns>
            public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
            {
                return EditingControlFormattedValue;
            }

            /// <summary>
            /// Prepares the currently selected cell for editing.
            /// </summary>
            /// <param name="selectAll"><c>true</c> to select all of the cell's content; <c>false</c> otherwise.</param>
            public void PrepareEditingControlForEdit(bool selectAll)
            {
                // Don't draw the ExtendedControls.TextBoxBorder border
                BorderColor = System.Drawing.Color.Transparent;
                // Don't draw the System.Windows.Forms.TextBox border
                BorderStyle = BorderStyle.None;

                if (selectAll)
                    SelectAll();
                else
                    SelectionStart = SelectionLength = 0;
            }

            #endregion // IDataGridViewEditingControl implementation

            /// <summary>
            /// Raises the <see cref="Control.TextChanged"/> event.
            /// </summary>
            /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
            private void Control_TextChanged(object sender, EventArgs e)
            {
                EditingControlValueChanged = true;
                EditingControlDataGridView?.NotifyCurrentCellDirty(true);
            }

            // it appears, that due to the control being a control TextBox within an ExtTextBox, the TextBox
            // when the first key is hit only gets a KU, not a KD/KP.  Thus it misses the first character in this system
            // so lets see if it sees it, if it does not, push it in.
            
            int keypressedseencount;
            protected override void OnKeyDown(KeyEventArgs e)       // occurs before control press below
            {
                keypressedseencount = KeysPressed;
                base.OnKeyDown(e);
            }
            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                bool notseen = KeysPressed == keypressedseencount;
                //System.Diagnostics.Debug.WriteLine("TB-KP not seen " + notseen);
                if (notseen)
                {
                    TextChangedEvent += "" + e.KeyChar;
                    textbox.Select(Text.Length,Text.Length);
                }
                base.OnKeyPress(e);
            }

            // Return is owned by DGV, unless you use one with ProcessDialogKey
            // so to use this, the DataGridView should be from the baseutils to get this call

            public bool ReturnPressedInEditMode()       // in edit mode, return is pressed
            {
                //System.Diagnostics.Debug.WriteLine("Return pressed when in edit mode");
                var e = new KeyEventArgs(Keys.Return);
                AutoCompleteTextBox_KeyDown(this,e );
                return e.Handled;   // true if handled
            }
        }



        #endregion // CellEditControl
    }
}
