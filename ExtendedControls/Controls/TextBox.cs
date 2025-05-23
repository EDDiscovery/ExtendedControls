﻿/*
 * Copyright 2016-2025 EDDiscovery development team
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ExtTextBox : Control, IThemeable
    {
        #region Public interface

        // BorderColour != transparent to use ours
        // BorderStyle to set textbox style..  None for off.  Can use both if you wish 
        public new string Name { get { return base.Name; } set { base.Name = value; textbox.Name = value + "_textbox"; endbutton.Name = value + "_button"; } }       // just so underlying control gets the same name

        public Color BorderColor { get { return bordercolor; } set { bordercolor = value; InternalPositionControls(); Invalidate(true); } }
        public Color BorderColor2 { get { return bordercolor2; } set { bordercolor2 = value; InternalPositionControls(); Invalidate(true); } }
        public System.Windows.Forms.BorderStyle BorderStyle { get { return textbox.BorderStyle; } set { textbox.BorderStyle = value; InternalPositionControls(); Invalidate(true); } }

        public override Color ForeColor { get { return textbox.ForeColor; } set { textbox.ForeColor = value; Invalidate(true); } }
        public override Color BackColor { get { return backnormalcolor; } set { backnormalcolor = value; if (!inerrorcondition) { textbox.BackColor = backnormalcolor; Invalidate(true); } } }
        public Color BackErrorColor { get { return backerrorcolor; } set { backerrorcolor = value; if (inerrorcondition) { textbox.BackColor = backerrorcolor; Invalidate(true); } } }
        public bool InErrorCondition { get { return inerrorcondition; } set { if (inerrorcondition != value) { inerrorcondition = value; textbox.BackColor = inerrorcondition ? backerrorcolor : backnormalcolor; Invalidate(true); } } }
        public Color ControlBackground { get { return controlbackcolor; } set { controlbackcolor = value; Invalidate(true); } } // colour of unfilled control area if border is on or button

        public bool WordWrap { get { return textbox.WordWrap; } set { textbox.WordWrap = value; } }
        public bool Multiline { get { return textbox.Multiline; } set { textbox.Multiline = value; InternalPositionControls(); } }
        public bool ReadOnly { get { return textbox.ReadOnly; } set { textbox.ReadOnly = value; } }
        public bool ClearOnFirstChar { get { return clearonfirstchar; } set { clearonfirstchar = value; if (clearonfirstchar) KeysPressed = 0; } } 

        public ScrollBars ScrollBars { get { return textbox.ScrollBars; } set { textbox.ScrollBars = value; } }

        public override string Text { get { return textbox.Text; } set { textbox.Text = value; } }
        public string TextNoChange { get { return textbox.Text; } set { nonreentrantchange = false;  textbox.Text = value; nonreentrantchange = true; } }

        public void SelectAll() { textbox.SelectAll(); }
        public int SelectionStart { get { return textbox.SelectionStart; } set { textbox.SelectionStart = value; } }
        public int SelectionLength { get { return textbox.SelectionLength; } set { textbox.SelectionLength = value; } }
        public void Select(int s, int e) { textbox.Select(s, e); }
        public void SelectEnd() { textbox.Select(textbox.Text.Length, textbox.Text.Length); }
        public string SelectedText { get { return textbox.SelectedText; } }

        public HorizontalAlignment TextAlign { get { return textbox.TextAlign; } set { textbox.TextAlign = value; } }

        // Must use ExtTextBoxAutoComplete.
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public AutoCompleteMode AutoCompleteMode { get { return textbox.AutoCompleteMode; } set { textbox.AutoCompleteMode = value; } }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public AutoCompleteSource AutoCompleteSource { get { return textbox.AutoCompleteSource; } set { textbox.AutoCompleteSource = value; } }

        public void SetTipDynamically(ToolTip t, string text) { t.SetToolTip(textbox, text); } // only needed for dynamic changes..

        public Func<ExtTextBox,bool> ReturnPressed;                          // fires if return pressed. Return true if supress return

        public int KeysPressed { get; private set; }  = 0;                     // count of key presses seen

        public bool EndButtonVisible                                         // extra visual control added within the bounds of the textbox border at end
        {
            get { return endbuttontoshow; }
            set
            {
                if (value != endbuttontoshow)
                {
                    endbutton.Visible = endbuttontoshow = value;
                    Invalidate(true);
                    Update();
                }
            }
        }
        public bool EndButtonEnable { get { return endbutton.Enabled; } set { endbutton.Enabled = value; this.Invalidate(true); Update(); } }
        public Image EndButtonImage { get { return endbutton.Image; } set { endbutton.Image = value; } }     // if you want something else.. keep it small
        public ExtButton EndButton { get { return endbutton; } }        // for themeing
        public int EndButtonSize16ths { get { return endbuttonsize; } set { endbuttonsize = value;Invalidate(); } }

        public new void Invalidate()
        {
            textbox.Invalidate();
            endbutton.Invalidate();
        }

        public Action<ExtTextBox> EndButtonClick = null;                              // if the button is pressed


        public ExtTextBox() : base()
        {
            this.GotFocus += TextBoxBorder_GotFocus;
            textbox = new TextBox();
            textbox.Name = "ExtTextBox_textbox";
            textbox.BorderStyle = BorderStyle.FixedSingle;
            backerrorcolor = Color.Red;
            backnormalcolor = textbox.BackColor;
            inerrorcondition = false;

            SuspendLayout();

            endbutton = new ExtButton();                               // we only add it to controls list if shown.. to limit the load on the GUI
            endbutton.Name = "ExtTextBox_EndButton";
            endbutton.Image = Properties.Resources.ArrowDown;
            endbutton.Click += Dropdownbutton_Click;
            endbutton.MouseMove += Textbox_MouseMove;
            endbutton.MouseEnter += Textbox_MouseEnter;
            endbutton.MouseLeave += Textbox_MouseLeave;
            endbutton.Visible = false;
            Controls.Add(endbutton);

            // Enter and Leave is handled by this wrapper control itself, since when we leave the textbox, we leave this
            textbox.Click += Textbox_Click;
            textbox.DoubleClick += Textbox_DoubleClick;
            textbox.KeyUp += Textbox_KeyUp;
            textbox.KeyDown += Textbox_KeyDown;
            textbox.KeyPress += Textbox_KeyPress;
            textbox.MouseClick += Textbox_MouseClick;
            textbox.MouseDoubleClick += Textbox_MouseDoubleClick;
            textbox.MouseUp += Textbox_MouseUp;
            textbox.MouseDown += Textbox_MouseDown;
            textbox.MouseMove += Textbox_MouseMove;
            textbox.MouseEnter += Textbox_MouseEnter;
            textbox.MouseLeave += Textbox_MouseLeave;
            textbox.TextChanged += Textbox_TextChanged;
            textbox.Validating += Textbox_Validating;
            textbox.Validated += Textbox_Validated;
            Controls.Add(textbox);

            ResumeLayout();
        }

        #endregion

        #region Implementation

        private void TextBoxBorder_GotFocus(object sender, EventArgs e)
        {
            textbox.Focus();
        }

        const int borderoffset = 3;

        private bool HasBorder { get { return !BorderColor.IsFullyTransparent(); } }

        private void InternalPositionControls()
        {
            if (ClientRectangle.Width > 0)
            {
                int bsize = HasBorder ? borderoffset : 0;
                int butwidth = endbuttontoshow ? (Height*endbuttonsize/16) : 0;
                int clientcentre = Height / 2;

                textbox.Size = new Size(ClientRectangle.Width - bsize * 2 - butwidth, ClientRectangle.Height - bsize * 2);

                textbox.Location = new Point(bsize, clientcentre - textbox.Height/2 );

                if (endbuttontoshow)
                {
                    endbutton.Location = new Point(ClientRectangle.Width - bsize - butwidth, clientcentre - butwidth/2);
                    endbutton.Size = new Size(butwidth, butwidth);
                }

               // System.Diagnostics.Debug.WriteLine("Repos " + Name + ":" + ClientRectangle.Size + " " + textbox.Location + " " + textbox.Size + " " + BorderColor + " " + textbox.BorderStyle + " dd " + endbutton.Size);
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            InternalPositionControls();
        }

        bool firstpaint = true;

        protected override void OnPaint(PaintEventArgs e)
        {
            if (firstpaint)
            {
                System.ComponentModel.IContainer ic = this.GetParentContainerComponents();

                ic?.CopyToolTips(this, new Control[] { textbox });

                firstpaint = false;
            }

            using (Brush highlight = new SolidBrush(controlbackcolor))
            {
                e.Graphics.FillRectangle(highlight, ClientRectangle);
            }

            base.OnPaint(e);

            //System.Diagnostics.Debug.WriteLine("Repaint" + Name + ":" + ClientRectangle.Size + " " + textbox.Location + " " + textbox.Size + " " + BorderColor + " " + textbox.BorderStyle);

            if (HasBorder)
            {
                Rectangle area = ClientRectangle;

                GraphicsPath g1 = DrawingHelpersStaticFunc.RectCutCorners(area.X + 1, area.Y + 1, area.Width - 2, area.Height - 1, 1, 1);
                using (Pen pc1 = new Pen(BorderColor, 1.0F))
                    e.Graphics.DrawPath(pc1, g1);

                GraphicsPath g2 = DrawingHelpersStaticFunc.RectCutCorners(area.X, area.Y, area.Width, area.Height - 1, 2, 2);
                using (Pen pc2 = new Pen(BorderColor2, 1.0F))
                    e.Graphics.DrawPath(pc2, g2);
            }
        }

        #endregion

        #region Supported Events

        // intercept most events and warn if used.. 
        // done this way because you can't hide events from the underlying control class (c# does not support protected inheritance), 
        // and we need to know if someone uses one we do not support
        // LEAVE in the commented out ones which we do support.. this list is going to be useful for other controls which we wish
        // to make

        public new event EventHandler BackColorChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler BackgroundImageChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler BackgroundImageLayout { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler BindingContextChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler CausesValidationChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler ChangeUICues { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        //public new event EventHandler Click { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler ClientSizeChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler ContextMenuStripChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler ControlAdded { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler ControlRemoved { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler CursorChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler DockChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        //public new event EventHandler DoubleClick { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler DragDrop { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler DragEnter { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler DragLeave { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler DragOver { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler EnabledChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        //public new event EventHandler Enter { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler FontChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler ForeColorChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler GiveFeedback { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler HelpRequested { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler ImeModeChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        //public new event KeyEventHandler KeyDown { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        //public new event KeyPressEventHandler KeyPress { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        //public new event KeyEventHandler KeyUp { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler Layout { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        //public new event EventHandler Leave { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler LocationChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler MarginChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler MouseCaptureChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        //public new event EventHandler MouseClick { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        //public new event EventHandler MouseDoubleClick { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        //public new event MouseEventHandler MouseDown { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        //public new event MouseEventHandler MouseEnter { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        //public new event MouseEventHandler MouseLeave { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        //public new event MouseEventHandler MouseMove { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        //public new event MouseEventHandler MouseUp { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler Move { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler PaddingChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler ParentChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler PreviewKeyDown { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler QueryAccessibilityHelp { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler QueryContinueDrag { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler RegionChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler Resize { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler RightToLeftChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler SizeChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler StyleChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler SystemColorsChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler TabIndexChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler TabStopChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        //public new event EventHandler TextChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        //public new event System.ComponentModel.CancelEventHandler Validating { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        //public new event EventHandler Validated { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }
        public new event EventHandler VisibleChanged { add { EventWarn(System.Reflection.MethodBase.GetCurrentMethod().Name); } remove { System.Diagnostics.Debug.Assert(true); } }

        void EventWarn(string method)
        {
            System.Diagnostics.Debug.WriteLine("*** Event " + method + " NOT SUPPORTED ");
            System.Diagnostics.Debug.Assert(false);
        }

        private void Textbox_Validated(object sender, EventArgs e)
        {
            OnValidated(e);
        }

        private void Textbox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OnValidating(e);
        }

        private void Textbox_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClick(e);
        }

        private void Textbox_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void Textbox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnMouseDoubleClick(e);
        }

        private void Textbox_MouseClick(object sender, MouseEventArgs e)
        {
            OnMouseClick(e);
        }

        private void Textbox_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void Textbox_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void Textbox_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        private void Textbox_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        private void Textbox_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        private void Textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            lastkey = e.KeyChar;
            KeysPressed++;

            if (e.Handled == false && e.KeyChar == '\r')
            {
                if (ReturnPressed != null)
                {
                    e.Handled = OnReturnPressed();
                }
            }

            OnKeyPress(e);
        }

        protected virtual bool OnReturnPressed()
        {
            return ReturnPressed?.Invoke(this) ?? false;
        }

        private void Textbox_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        private void Textbox_KeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }

        bool nonreentrantchange = true;
        protected virtual void Textbox_TextChanged(object sender, EventArgs e)
        {
            if (nonreentrantchange == true)
            {
              //  System.Diagnostics.Debug.WriteLine($"TB Text changed {KeysPressed} {ClearOnFirstChar} {lastkey} ");
                if (ClearOnFirstChar && KeysPressed == 1)
                {
                    nonreentrantchange = false;
                    if (char.IsLetter(lastkey))
                        textbox.Text = "" + lastkey;
                    else
                        textbox.Text = "";
                    textbox.Select(1, 1);
                    nonreentrantchange = true;
                }

                OnTextChanged(e);
            }
        }

        private void Dropdownbutton_Click(object sender, EventArgs e)
        {
            EndButtonClick?.Invoke(this);
        }

        public bool Theme(Theme t, Font fnt)
        {
            ForeColor = t.TextBlockForeColor;
            BackColor = t.TextBlockBackColor;
            ControlBackground = t.TextBlockBackColor; 

            BorderColor = t.IsTextBoxBorderColour ? t.TextBlockBorderColor : Color.Transparent;
            BorderColor2 = t.IsTextBoxBorderColour ? t.TextBlockBorderColor2 : Color.Transparent;
            BorderStyle = t.TextBoxStyle;

            BackErrorColor = t.TextBlockHighlightColor;
            AutoSize = true;

            if (t.IsTextBoxBorderNone)
                AutoSize = false;                                                 // with no border, the autosize clips the bottom of chars..

            if (this is ExtTextBoxAutoComplete || this is ExtDataGridViewColumnAutoComplete.CellEditControl) // derived from text box
            {
                ExtTextBoxAutoComplete actb = this as ExtTextBoxAutoComplete;

                actb.DropDownTheme.SetFromTextBlock(t);

                actb.FlatStyle = t.ButtonFlatStyle;
            }

            EndButton.Theme(t, fnt);
            EndButton.FlatAppearance.BorderColor = EndButton.BackColor = EndButton.BackColor2 = t.TextBlockBackColor;
            EndButton.ButtonDisabledScaling = t.DisabledScaling;

            Invalidate();

            return false;
        }

        #endregion

        #region Variables

        protected TextBox textbox;
        private Color bordercolor = Color.Transparent;
        private Color bordercolor2 = Color.Transparent;
        private Color controlbackcolor = SystemColors.Control;
        private Color backnormalcolor;        // normal back colour..
        private Color backerrorcolor;        // error back colour..
        private bool inerrorcondition;          // if in error condition

        private char lastkey;               // records key presses

        private ExtButton endbutton;
        private bool endbuttontoshow = false; // you can't trust visible
        private int endbuttonsize = 10;
        private bool clearonfirstchar = false;

        #endregion
    }
}
