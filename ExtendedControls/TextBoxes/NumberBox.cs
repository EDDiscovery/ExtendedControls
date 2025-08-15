/*g
 * Copyright 2016 - 2025 EDDiscovery development team
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
using System.ComponentModel;
using System.Windows.Forms;

namespace ExtendedControls
{
    public abstract class NumberBox<T> : ExtTextBox
    {
        public string Format { get { return format; } set { format = value; ignorechange = true; base.Text = ConvertToString(Value); ignorechange = false; Check();  } }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Globalization.CultureInfo FormatCulture { get { return culture; } set { culture = value; Check(); } }
        public System.Globalization.NumberStyles NumberStyles { get { return numstyles; } set { numstyles = value; Check(); } }

        public int DelayBeforeNotification { get; set; } = 0;
        public T Minimum { get { return minv; } set { minv = value; Check(); } }
        public T Maximum { get { return maxv; } set { maxv = value; Check(); } }

        public bool IsValid { get { T v;  return ConvertFromString(base.Text,out v); } }        // is the text a valid value?

        public void SetComparitor( NumberBox<T> other, int compare)         // aka -2 (<=) -1(<) 0 (=) 1 (>) 2 (>=)
        {
            othernumber = other;
            othercomparision = compare;
            Check();
        }

        public void SetBlank()          // Blanks it, but does not declare an error
        {
            ignorechange = true;
            base.Text = "";
            InErrorCondition = false;
            ignorechange = false;
        }

        public void SetNonBlank()       // restores it to its last value
        {
            ignorechange = true;
            base.Text = ConvertToString(Value);
            Check();
            ignorechange = false;
        }

        public event EventHandler ValueChanged              // fired (first) when value is changed to a new valid value. Can be delayed by DelayBeforeNotification
        {
            add { Events.AddHandler(EVENT_VALUECHANGED, value); }
            remove { Events.RemoveHandler(EVENT_VALUECHANGED, value); }
        }

        public Action<NumberBox<T>,bool> ValidityChanged;                    // fires (second) if validity changes

        // Finally, Use TextChanged to see all changes, then you can check for IsValid.  Fires after ValueChanged/ValidityChanged

        public T Value                                          // will fire a ValueChanged event
        {
            get { return number; }
            set
            {
                number = value;
                base.Text = ConvertToString(number);            // triggers change text event, which sets validity
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public T ValueNoChange                                  //will not fire a ValueChanged event
        {
            get { return number; }
            set
            {
                number = value;
                ignorechange = true;
                base.Text = ConvertToString(number);            // triggers change text event but its ignored
                ignorechange = false;
                InErrorCondition = !IsValid;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text { get { return base.Text; } set { System.Diagnostics.Debug.Assert(false, "Can't set Number box"); } }       // can't set Text, only read..

        #region Implementation

        private void Check()
        {
            InErrorCondition = !IsValid;
        }

        private T number;
        protected bool ignorechange = false;
        private string format = "N";
        private System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CurrentCulture;
        private System.Globalization.NumberStyles numstyles = System.Globalization.NumberStyles.None;
        private Timer timer;
        private static readonly object EVENT_VALUECHANGED = new object();
        private T minv;
        private T maxv;
        protected NumberBox<T> othernumber { get; set; } = null;             // attach to another box for validation
        protected int othercomparision { get; set; } = 0;              // aka -2 (<=) -1(<) 0 (=) 1 (>) 2 (>=)

        protected abstract string ConvertToString(T v);
        protected abstract bool ConvertFromString(string t, out T number);
        protected abstract bool AllowedChar(char c);

        public NumberBox()
        {
            timer = new Timer();
            timer.Tick += Timer_Tick;
        }

        public new void Dispose()
        {
            timer.Dispose();
            base.Dispose();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Text box " + Name + "  " + ignorechange);
            if (!ignorechange)
            {
                T newvalue;

                if (ConvertFromString(Text, out newvalue))
                {
                    number = newvalue;

                    if (DelayBeforeNotification <= 0)
                    {
                        EventHandler handler = (EventHandler)Events[EVENT_VALUECHANGED];
                        if (handler != null)
                        {
                            //System.Diagnostics.Debug.WriteLine($"Number Box {this.Name} changed {number}");
                            handler(this, new EventArgs());
                        }
                    }
                    else
                    {
                        timer.Interval = DelayBeforeNotification;
                        timer.Start();
                    }

                    if (InErrorCondition)
                        ValidityChanged?.Invoke(this,true);

                    InErrorCondition = false;
                }
                else
                {                               // Invalid, indicate
                    if (!InErrorCondition)
                        ValidityChanged?.Invoke(this,false);
                    InErrorCondition = true;
                }
            }

            base.OnTextChanged(e);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            EventHandler handler = (EventHandler)Events[EVENT_VALUECHANGED];
            if (handler != null)
            {
                //System.Diagnostics.Debug.WriteLine($"Number Box {this.Name} delay changed {number}");
                handler(this, new EventArgs());
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e) // limit keys to whats allowed for a double
        {
            if (AllowedChar(e.KeyChar))
            {
                base.OnKeyPress(e);
            }
            else
            {
                e.Handled = true;
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            if (!IsValid)           // if text box is not valid, go back to the original colour with no chanve event
                ValueNoChange = number;

            base.OnLeave(e);
        }

        protected bool CheckChar(char c, bool allowneg)
        {
            char numgroup = FormatCulture.NumberFormat.CurrencyGroupSeparator[0];
            if (numgroup == '\u00a0')       // fix non breaking space
                numgroup = ' ';

            //System.Diagnostics.Debug.WriteLine($"Char {c} allowneg {allowneg} {NumberStyles}");

            return char.IsDigit(c) || c == 8 ||

              (c == FormatCulture.NumberFormat.CurrencyDecimalSeparator[0] && Text.IndexOf(FormatCulture.NumberFormat.CurrencyDecimalSeparator, StringComparison.Ordinal) == -1 &&
                                  (NumberStyles & System.Globalization.NumberStyles.AllowDecimalPoint) != 0) ||

              (c == FormatCulture.NumberFormat.NegativeSign[0] && (SelectionStart == 0 || (SelectionStart == 1 && Text.Length > 0 && Text[0] == FormatCulture.NumberFormat.CurrencySymbol[0]))
                          && allowneg && (NumberStyles & System.Globalization.NumberStyles.AllowLeadingSign) != 0) ||

              (c == numgroup && (NumberStyles & System.Globalization.NumberStyles.AllowThousands) != 0) ||

              (c == FormatCulture.NumberFormat.CurrencySymbol[0] && SelectionStart == 0 && (NumberStyles & System.Globalization.NumberStyles.AllowCurrencySymbol) != 0) ||

              ((c == 'E' || c == 'e') && (NumberStyles & System.Globalization.NumberStyles.AllowExponent) != 0);
        }
        #endregion
    }

    public class NumberBoxFloat : NumberBox<float>
    {
        public NumberBoxFloat()
        {
            ValueNoChange = 0;
            Minimum = float.MinValue;
            Maximum = float.MaxValue;
            NumberStyles = System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowThousands;
            Format = "G";
        }

        protected override string ConvertToString(float v)
        {
            return v.ToString(Format, FormatCulture);
        }
        protected override bool ConvertFromString(string t, out float number)
        {
            bool ok = float.TryParse(t, NumberStyles, FormatCulture, out number) &&
                      number >= Minimum && number <= Maximum;
            if (ok && othernumber != null)
                ok = number.CompareTo(othernumber.Value, othercomparision);
            System.Diagnostics.Debug.WriteLine($"Number Box {ok}: {number}");
            return ok;
        }

        protected override bool AllowedChar(char c)
        {
            return CheckChar(c, Minimum < 0);
        }
    }

    public class NumberBoxDouble : NumberBox<double>
    {
        public NumberBoxDouble()
        {
            ValueNoChange = 0;
            NumberStyles = System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowThousands;
            Format = "G";
            Minimum = double.MinValue;
            Maximum = double.MaxValue;
        }

        protected override string ConvertToString(double v)
        {
            return v.ToString(Format, FormatCulture);
        }
        protected override bool ConvertFromString(string t, out double number)
        {
            bool ok = double.TryParse(t, NumberStyles, FormatCulture, out number) &&
                number >= Minimum && number <= Maximum;
            if (ok && othernumber != null)
                ok = number.CompareTo(othernumber.Value, othercomparision);
            return ok;
        }

        protected override bool AllowedChar(char c)
        {
            return CheckChar(c, Minimum < 0);
        }
    }

    public class NumberBoxLong : NumberBox<long>
    {
        public NumberBoxLong()
        {
            NumberStyles = System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign;
            Format = "D";
            ValueNoChange = 0;
            Minimum = long.MinValue;
            Maximum = long.MaxValue;
        }

        protected override string ConvertToString(long v)
        {
            return v.ToString(Format, FormatCulture);
        }
        protected override bool ConvertFromString(string t, out long number)
        {
            bool ok = long.TryParse(t, NumberStyles, FormatCulture, out number) &&
                            number >= Minimum && number <= Maximum;
            if (ok && othernumber != null)
                ok = number.CompareTo(othernumber.Value, othercomparision);
            return ok;
        }

        protected override bool AllowedChar(char c)
        {
            return CheckChar(c, Minimum < 0);
        }
    }

    public class NumberBoxInt : NumberBox<int>
    {
        public NumberBoxInt()
        {
            NumberStyles = System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowLeadingSign;
            Format = "D";
            ValueNoChange = 0;
            Minimum = int.MinValue;
            Maximum = int.MaxValue;
        }

        protected override string ConvertToString(int v)
        {
            return v.ToString(Format, FormatCulture);
        }
        protected override bool ConvertFromString(string t, out int number)
        {
            bool ok = int.TryParse(t, NumberStyles, FormatCulture, out number) &&
                            number >= Minimum && number <= Maximum;
            if (ok && othernumber != null)
                ok = number.CompareTo(othernumber.Value, othercomparision);
            return ok;
        }

        protected override bool AllowedChar(char c)
        {
            return CheckChar(c, Minimum < 0);
        }
    }
}

