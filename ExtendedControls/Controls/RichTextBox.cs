/*
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ExtRichTextBox : Panel, IThemeable
    {
        #region Public Functions
        public class RichTextBoxBack : RichTextBox
        {
            public IntPtr SendMessage(int msg, IntPtr wparam, IntPtr lparam)
            {
                Message message = Message.Create(this.Handle, msg, wparam, lparam);
                this.WndProc(ref message);
                return message.Result;
            }
        }

        // BackColor is the colour of the panel.
        // if BorderColor is set, BackColor gets shown, with BorderColor on top.
        // BorderStyle is also applied by windows around the control, set to None for BorderColor only

        public Color TextBoxForeColor { get { return textBox.ForeColor; } set { textBox.ForeColor = value; } }
        public Color TextBoxBackColor { get { return textBox.BackColor; } set { textBox.BackColor = value; } }
        public Color BorderColor { get; set; } = Color.Transparent;
        public Color BorderColor2 { get; set; } = Color.Transparent;
        public bool ShowLineCount { get; set; } = false;                // count lines
        public bool HideScrollBar { get; set; } = true;                   // hide if no scroll needed
        public ExtScrollBar ScrollBar { get; set; }

        public override string Text { get { return textBox.Text; } set { textBox.Text = value; EstimateLines(); UpdateScrollBar(); } }                // return only textbox text
        public string[] Lines {  get { return textBox.Lines; } }

        public string Rtf { get { return textBox.Rtf; } set { textBox.Rtf = value; UpdateScrollBar(); } }
        public int LineCount { get { return textBox.GetLineFromCharIndex(textBox.Text.Length) + 1; } }

        public int ScrollBarWidth { get { return Font.ScaleScrollbar(); } }

        public void SetTipDynamically(ToolTip t, string text) { t.SetToolTip(textBox, text); } // only needed for dynamic changes..

        // turning this on allows dropping of text or a file into the box to replace all text.
        public override bool AllowDrop { get { return textBox.AllowDrop; } set { textBox.AllowDrop = value; } }
        public void InstallStandardDragDrop()
        {
            DragEnter += ExtRichTextBox_DragEnter;
            DragDrop += ExtRichTextBox_DragDrop;
        }
        public string DraggedFile { get; private set; }     // null until a file is dragged to it

        public bool DetectUrls { get { return textBox.DetectUrls; } set { textBox.DetectUrls = value; } }
        public Action<LinkClickedEventArgs> LinkClicked;

        public delegate void OnTextBoxChanged(object sender, EventArgs e);
        public event OnTextBoxChanged TextBoxChanged;


        public void Clear()
        {
            textBox.Clear();
            PerformLayout();
        }

        public void AppendText(string s)
        {
            if (ShowLineCount)
            {
                s = linecounter + ":" + s;
                linecounter++;
            }
            textBox.AppendText(s);
            textBox.ScrollToCaret();
            EstimateLines();
            UpdateScrollBar();
        }

        public void AppendText(string s, Color c)
        {
            if (ShowLineCount)
            {
                s = linecounter + ":" + s;
                linecounter++;
            }

            textBox.SelectionStart = textBox.TextLength;
            textBox.SelectionLength = 0;
            textBox.SelectionColor = c;
            textBox.AppendText(s);
            textBox.SelectionColor = textBox.ForeColor;
            textBox.SelectionStart = textBox.TextLength;
            textBox.SelectionLength = 0;
            textBox.ScrollToCaret();
            EstimateLines();
            UpdateScrollBar();
        }

        public void CopyFrom(ExtRichTextBox other)
        {
            textBox.Rtf = other.textBox.Rtf;
            EstimateLines();
        }

        public void SetTabs(int[] array)
        {
            textBox.SelectionTabs = array;
        }

        public bool ReadOnly { get { return textBox.ReadOnly; } set { textBox.ReadOnly = value; } }

        public string SelectedText { get { return textBox.SelectedText; } }
        public void Select(int s, int e) { textBox.Select(s, e); }
        public void CursorToEnd() { textBox.Select(textBox.TextLength, textBox.TextLength); }
        public void ScrollToCaret() { textBox.ScrollToCaret(); }
        public new void Focus() { textBox.Focus(); }

        public override ContextMenuStrip ContextMenuStrip { get { return textBox.ContextMenuStrip; } set { textBox.ContextMenuStrip = value; } }

        #endregion

        #region Implementation

        private RichTextBoxBack textBox;                 // Use these with caution.
        private int linecounter = 1;

        public ExtRichTextBox() : base()
        {
            textBox = new RichTextBoxBack();
            textBox.Name = "ExtRichTextBox_RTB";
            ScrollBar = new ExtScrollBar();
            Controls.Add(textBox);
            Controls.Add(ScrollBar);
            textBox.ScrollBars = RichTextBoxScrollBars.None;
            textBox.BorderStyle = BorderStyle.None;
            textBox.BackColor = BackColor;
            textBox.ForeColor = ForeColor;
            textBox.MouseUp += TextBox_MouseUp;
            textBox.MouseDown += TextBox_MouseDown;
            textBox.MouseMove += TextBox_MouseMove;
            textBox.MouseEnter += TextBox_MouseEnter;
            textBox.MouseLeave += TextBox_MouseLeave;
            textBox.DragEnter += TextBox_DragEnter;
            textBox.DragDrop += TextBox_DragDrop;
            textBox.LinkClicked += TextBox_LinkClicked;
            textBox.Show();
            ScrollBar.Show();
            textBox.VScroll += OnVscrollChanged;
            textBox.MouseWheel += new MouseEventHandler(MWheel);        // richtextbox without scroll bars do not handle mouse wheels
            textBox.TextChanged += TextChangeEventHandler;
            ScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(OnScrollBarChanged);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!BorderColor.IsFullyTransparent())
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

                GraphicsPath g1 = DrawingHelpersStaticFunc.RectCutCorners(1, 1, ClientRectangle.Width - 2, ClientRectangle.Height - 1, 1, 1);
                using (Pen pc1 = new Pen(BorderColor, 1.0F))
                    e.Graphics.DrawPath(pc1, g1);

                GraphicsPath g2 = DrawingHelpersStaticFunc.RectCutCorners(0, 0, ClientRectangle.Width, ClientRectangle.Height - 1, 2, 2);
                using (Pen pc2 = new Pen(BorderColor2, 1.0F))
                    e.Graphics.DrawPath(pc2, g2);
            }
        }

        bool scrollbarvisibleonlayout = false;

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            int bordersize = (!BorderColor.IsFullyTransparent()) ? 3 : 0;
            int textboxclienth = ClientRectangle.Height - bordersize * 2;

            scrollbarvisibleonlayout = ScrollBar.IsScrollBarOn || DesignMode || !HideScrollBar;  // Hide must be on, or in design mode, or scroll bar is on due to values

            textBox.Location = new Point(bordersize, bordersize);
            textBox.Size = new Size(ClientRectangle.Width - (scrollbarvisibleonlayout ? ScrollBarWidth : 0) - bordersize * 2, textboxclienth);

            ScrollBar.Location = new Point(ClientRectangle.Width - ScrollBarWidth - bordersize, bordersize);
            ScrollBar.Size = new Size(ScrollBarWidth, textboxclienth);
        }

        protected override void OnFontChanged(EventArgs e)      // these events can change visible lines
        {
            base.OnFontChanged(e);
            foundlineheight = 0;
            EstimateLines();
            UpdateScrollBar();
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            if (Width > 0)
            {
                foundlineheight = 0;
                EstimateLines();
                UpdateScrollBar();
            }
        }

        int visiblelines = 1;
        int foundlineheight = 0;

        private void EstimateLines()
        {
            if (foundlineheight>0)      // already done
                return;

            int bordersize = (!BorderColor.IsFullyTransparent()) ? 3 : 0;
            int textboxclienth = ClientRectangle.Height - bordersize * 2;           // account for border

            if (Text.HasChars() && Text.Contains("\n"))                             // we need text to sense it
            {
                int sl = textBox.GetCharIndexFromPosition(new Point(0, 0));
                sl = textBox.GetLineFromCharIndex(sl);
                for (int i = 1; i < ClientRectangle.Height; i++)
                {
                    int nl = textBox.GetCharIndexFromPosition(new Point(0, i));     // look to see when line changes.. it may not if only one line
                    nl = textBox.GetLineFromCharIndex(nl);
                    if (sl != nl)
                    {
                        foundlineheight = i;
                        visiblelines = textboxclienth / i;                          // gotcha..
                        //System.Diagnostics.Debug.WriteLine("Found line h " + i + " giving " + visiblelines + " " + ((float)textboxclienth / i));
                        return;
                    }
                }
            }

            visiblelines = textboxclienth / (int)(Font.GetHeight() + 1);            // basic estimate
        }

        private void UpdateScrollBar()            // from the richtext, vscroll occurred, set the scroll bar
        {
            int firstVisibleLine;

            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                int firstVisibleChar = textBox.GetCharIndexFromPosition(new Point(0,0));
                firstVisibleLine = textBox.GetLineFromCharIndex(firstVisibleChar);
                //System.Diagnostics.Debug.WriteLine("USB first VL:" + firstVisibleLine + " lines " + LineCount + " " + visiblelines );
            }
            else
            {
                firstVisibleLine = unchecked((int)(long)textBox.SendMessage(BaseUtils.Win32Constants.EM.GETFIRSTVISIBLELINE, IntPtr.Zero, IntPtr.Zero));
            }

            ScrollBar.SetValueMaximumLargeChange(firstVisibleLine, LineCount - 1, visiblelines);
            if (ScrollBar.IsScrollBarOn != scrollbarvisibleonlayout)     // need to relayout if scroll bars pop on
                PerformLayout();
        }

        public int EstimateVerticalSizeFromText()
        {
            int lastselpos = this.Text.Length;
            int numberlines = (lastselpos >= 0) ? (textBox.GetLineFromCharIndex(lastselpos) + 1) : 0;
            int bordersize = (!BorderColor.IsFullyTransparent()) ? 3 : 0;
            int lineh = foundlineheight > 0 ? foundlineheight : (int)(Font.GetHeight() + 2);        // if we have an estimate, use it, else use Font Height
            int neededpixels = (int)(lineh * numberlines) + bordersize * 2 + 4;      // 4 extra for border area of this (bounds-client rect)
            return neededpixels;
        }

        protected virtual void OnVscrollChanged(object sender, EventArgs e) // comes from TextBox, update scroll..
        {
            UpdateScrollBar();
        }

        protected virtual void OnScrollBarChanged(object sender, ScrollEventArgs e) // comes from scroll bar, update text position
        {
            ScrollToBar();
        }

        int lastscrollto = 0;

        private void ScrollToBar()              // from the scrollbar, scroll first line to value
        {
            int scrollvalue = ScrollBar.Value;

            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                int line = scrollvalue + (lastscrollto<=scrollvalue ? visiblelines-1 : 0);
                int index = textBox.GetFirstCharIndexFromLine(line);         // MONO does not do EM.LiNESCROLL - this is the best we can do
                lastscrollto = scrollvalue;

                //System.Diagnostics.Debug.WriteLine("Scroll Bar:" + scrollvalue + " vl " + visiblelines + " goto " + line);
                textBox.Select(index , 0);
                textBox.ScrollToCaret();
            }
            else
            {
                int firstVisibleLine = unchecked((int)(long)textBox.SendMessage(BaseUtils.Win32Constants.EM.GETFIRSTVISIBLELINE, IntPtr.Zero, IntPtr.Zero));
                int delta = scrollvalue - firstVisibleLine;

                //Console.WriteLine("Scroll Bar:" + scrollvalue + " FVL: " + firstVisibleLine + " delta " + delta);
                if (delta != 0)
                {
                    textBox.SendMessage(BaseUtils.Win32Constants.EM.LINESCROLL, IntPtr.Zero, (IntPtr)(delta));
                }
            }
        }

        protected virtual void MWheel(object sender, MouseEventArgs e)  // mouse, we move then scroll to bar
        {
            if (e.Delta > 0)
                ScrollBar.ValueLimited--;                  // control takes care of end limits..
            else
                ScrollBar.ValueLimited++;           // end is UserLimit, not maximum

            ScrollToBar();                          // go to scroll position
        }

        protected override void OnGotFocus(EventArgs e)             // Focus on us is given to the text box.
        {
            base.OnGotFocus(e);
            textBox.Focus();
        }

        protected void TextChangeEventHandler(object sender, EventArgs e)
        {
            if ( TextBoxChanged!=null)
                TextBoxChanged(this, new EventArgs());
        }

        #endregion



        private void TextBox_MouseEnter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);
        }

        private void TextBox_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        private void TextBox_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        private void TextBox_MouseLeave(object sender, EventArgs e)             // using the text box mouse actions, pass thru to ours so registered handlers work
        {
            base.OnMouseLeave(e);
        }

        private void TextBox_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        protected virtual void TextBox_DragDrop(object sender, DragEventArgs e)
        {
            OnDragDrop(e);
        }

        protected virtual void TextBox_DragEnter(object sender, DragEventArgs e)
        {
            OnDragEnter(e);
        }

        private void ExtRichTextBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length > 0)
                {
                    string str = BaseUtils.FileHelpers.TryReadAllTextFromFile(files[0]);
                    if (str != null)
                    {
                        textBox.Text = str;
                        DraggedFile = files[0];
                    }

                }
            }
            else if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string text = (string)e.Data.GetData(DataFormats.Text);
                textBox.Text = text;
                DraggedFile = null;
            }
        }

        private void ExtRichTextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) || e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void TextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            LinkClicked?.Invoke(e);
        }

        public bool Theme(Theme t,Font fnt)
        {
            TextBoxForeColor = t.TextBlockForeColor;
            TextBoxBackColor = t.TextBlockBackColor;

            BorderColor = t.IsTextBoxBorderColour ? t.TextBlockBorderColor : Color.Transparent;
            BorderColor2 = t.IsTextBoxBorderColour ? t.TextBlockBorderColor2 : Color.Transparent;
            BorderStyle = t.TextBoxStyle;

            if (t.IsButtonSystemStyle)
            {
                ScrollBar.FlatStyle = FlatStyle.System;
            }
            else
            {
                ScrollBar.BorderColor = ScrollBar.ThumbBorderColor = ScrollBar.ArrowBorderColor = t.TextBlockBorderColor;
                ScrollBar.BackColor = t.TextBlockBackColor;
                ScrollBar.SliderColor = t.TextBlockSliderBack;
                ScrollBar.SliderColor2 = t.TextBlockSliderBack2;
                ScrollBar.SliderDrawAngle = t.TextBlockScrollButtonGradientDirection;
                ScrollBar.ForeColor = t.TextBlockScrollArrow;
                ScrollBar.ArrowButtonColor = t.TextBlockScrollArrowBack;
                ScrollBar.ArrowButtonColor2 = t.IsButtonGradientStyle ? t.TextBlockScrollArrowBack2 : t.TextBlockScrollArrowBack;
                ScrollBar.ThumbButtonColor = t.TextBlockScrollButtonBack;
                ScrollBar.ThumbButtonColor2 = t.IsButtonGradientStyle ? t.TextBlockScrollButtonBack2 : t.TextBlockScrollButtonBack;
                ScrollBar.ThumbDrawAngle = t.TextBlockScrollButtonGradientDirection;
                ScrollBar.MouseOverButtonColor = ScrollBar.ThumbButtonColor.Multiply(t.MouseOverScaling);
                ScrollBar.MouseOverButtonColor2 = ScrollBar.ThumbButtonColor.Multiply(t.MouseOverScaling);
                ScrollBar.MousePressedButtonColor = ScrollBar.ThumbButtonColor.Multiply(t.MouseSelectedScaling);
                ScrollBar.MousePressedButtonColor2 = ScrollBar.ThumbButtonColor.Multiply(t.MouseSelectedScaling);
                ScrollBar.FlatStyle = FlatStyle.Popup;
            }

            if (ContextMenuStrip != null)      // propegate font
                ContextMenuStrip.Font = fnt;

            Invalidate();
            PerformLayout();

            return false;
        }
    }
}
