/*
 * Copyright © 2023-2023 robby @ github
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
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class Terminal : UserControl
    {
        public Color VTBackColor { get { return backcolor; } set { backcolor = value; } }
        public Color VTForeColor { get { return forecolor; } set { forecolor = value; } }

        public Size TextSize { get; private set; }
        public Size TextArea { get; private set; }
        public Size CellSize { get; private set; }

        public enum CursorShapeType { Block, LineLeft };

        // all of these have active actions, they are not for the designer

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Point CursorPosition { get { return cursorpos; } set { WrapWithCursor(() => { cursorpos = new Point(value.X.Range(0, TextSize.Width - 1), value.Y.Range(0, TextSize.Height - 1)); }); } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShowCursor { get { return showcursor; } set { SetupCursor(value, CursorColor, CursorShape, cursorflash); } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Color CursorColor { get { return cursorcolor; } set { SetupCursor(ShowCursor, value, CursorShape, cursorflash); } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CursorShapeType CursorShape { get { return cursorshape; } set { SetupCursor(ShowCursor, CursorColor, value, cursorflash); } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool CursorFlashes { get { return cursorflash; } set { SetupCursor(ShowCursor, CursorColor, CursorShape, value); ; } }

        public Terminal()
        {
            InitializeComponent();

            Data = new CharCell[MaxTextHeight][];

            for (int y = 0; y < Data.Length; y++)
            {
                Data[y] = new CharCell[MaxTextWidth];
                MakeLine(y);
            }

            cursortimer = new Timer();
            cursortimer.Interval = 250;
            cursortimer.Tick += Cursortimer_Tick;

            panelTermWindow.Paint += PanelTermWindow_Paint;
        }

        public void AddTextClear(ref StringBuilder sb)
        {
            AddText(sb.ToString());
            sb.Clear();
        }

        // understands \r \n \b

        public void AddText(string str)
        {
            Rectangle invalidaterect = Rectangle.Empty;
            foreach (var ch in str)
            {
                bool lfit = false;
                if (ch == '\r')
                {
                    cursorpos.X = 0;
                    var rect = new Rectangle(0, cursorpos.Y * CellSize.Height, ClientRectangle.Width, CellSize.Height);
                    invalidaterect = invalidaterect.Add(rect);
                }
                else if (ch == '\n')
                {
                    lfit = true;
                }
                else if (ch == '\b')
                {
                    if (cursorpos.X > 0)
                    {
                        cursorpos.X--;
                        Data[cursorpos.Y][cursorpos.X].Set(' ', forecolor, backcolor);
                        var rect = new Rectangle(cursorpos.X * CellSize.Width, cursorpos.Y * CellSize.Height, CellSize.Width * 2, CellSize.Height);       // invalidate this backspace cell, and next one on
                        invalidaterect = invalidaterect.Add(rect);
                    }
                }
                else
                {
                    // invalidate the current and next cell
                    var rect = new Rectangle(cursorpos.X * CellSize.Width, cursorpos.Y * CellSize.Height, CellSize.Width * 2, CellSize.Height);
                    invalidaterect = invalidaterect.Add(rect);

                    Data[cursorpos.Y][cursorpos.X].Set(ch, forecolor, backcolor);
                    cursorpos.X++;
                    if (cursorpos.X >= TextSize.Width)
                    {
                        cursorpos.X = 0;
                        lfit = true;
                    }
                }

                if (lfit)
                {
                    if (++cursorpos.Y >= TextSize.Height)
                    {
                        cursorpos.Y--;
                        ScrollData(-1);

                        // we need to invalidate from the bottom to the previous row in not already set up
                        int top = invalidaterect.Width == 0 ? Math.Max(0, (cursorpos.Y - 1) * CellSize.Height) : Math.Max(0, invalidaterect.Top - CellSize.Height);
                        invalidaterect = new Rectangle(0, top, ClientRectangle.Width, ClientRectangle.Height - top);
                    }
                    else
                    {
                        // its not scrolled
                        // we need to invalidate the previous and the current lines
                        int top = invalidaterect.Width == 0 ? Math.Max(0, (cursorpos.Y - 1) * CellSize.Height) : invalidaterect.Top;
                        invalidaterect = new Rectangle(0, top, ClientRectangle.Width, CellSize.Height * 2);
                    }
                }
            }

            if ( debug)
                System.Diagnostics.Debug.WriteLine($"Write {str} invalidate {invalidaterect} x {invalidaterect.Left / CellSize.Width} - {(invalidaterect.Right - 1) / CellSize.Width} y {invalidaterect.Top / CellSize.Height} - {(invalidaterect.Bottom - 1) / CellSize.Height} ");
            if (invalidaterect.Width > 0)
                panelTermWindow.Invalidate(invalidaterect);
        }

        public void CR()
        {
            WrapWithCursor(() => { cursorpos.X = 0; });
        }

        public void LF()
        {
            if (++cursorpos.Y >= TextSize.Height)
            {
                cursorpos.Y--;
                ScrollData(-1);
                // we need to invalidate from the bottom to the previous row
                int top = Math.Max(0, (cursorpos.Y - 1) * CellSize.Height);
                Invalidate(new Rectangle(0, top, ClientRectangle.Width, ClientRectangle.Height - top));
            }
            else
            {
                // its not scrolled
                // we need to invalidate the previous and the current lines
                int top = Math.Max(0, (cursorpos.Y - 1) * CellSize.Height);
                Invalidate(new Rectangle(0, top, ClientRectangle.Width, CellSize.Height * 2));
            }
        }

        public void CRLF()
        {
            cursorpos.X = 0;
            LF();
        }
        public void BackSpace()
        {
            if (cursorpos.X > 0)
            {
                WrapWithCursor(() =>
                {
                    cursorpos.X--;
                    Data[cursorpos.Y][cursorpos.X].Set(' ', Color.Red, backcolor);
                    DrawCell(cursorpos.X, cursorpos.Y);
                });
            }
        }

        public void TabForward(int tabspacing)
        {
            WrapWithCursor(() => { cursorpos.X = Math.Min(TextSize.Width - 1, (cursorpos.X + tabspacing) / tabspacing * tabspacing); });
        }
        public void CursorUp(int n, bool scrollonedge = false)
        {
            if (scrollonedge && cursorpos.Y - n < 0)        // if need to scroll
            {
                CursorOff();
                int scrollby = n - cursorpos.Y;
                ScrollDown(scrollby);
                cursorpos.Y = 0;
            }
            else
                WrapWithCursor(() => { cursorpos.Y = Math.Max(0, cursorpos.Y - n); });
        }
        public void CursorDown(int n, bool scrollonedge  = false )
        {
            if (scrollonedge && cursorpos.Y + n >= TextSize.Height)        // if need to scroll
            {
                CursorOff();
                int scrollby = cursorpos.Y + n - TextSize.Height + 1;
                ScrollUp(scrollby);
                cursorpos.Y = TextSize.Height - 1;
            }
            else
                WrapWithCursor(() => { cursorpos.Y = Math.Min(TextSize.Height - 1, cursorpos.Y + n); });
        }
        public void CursorBack(int n)
        {
            WrapWithCursor(() => { cursorpos.X = Math.Max(0, cursorpos.X - n); });
        }
        public void CursorForward(int n)
        {
            WrapWithCursor(() => { cursorpos.X = Math.Min(TextSize.Width - 1, cursorpos.X + n); });
        }
        public void CursorNextLine(int n, bool scrollonedge = false)
        {
            if (scrollonedge && cursorpos.Y + n >= TextSize.Height)        // if need to scroll
            {
                CursorOff();
                int scrollby = cursorpos.Y + n - TextSize.Height + 1;
                ScrollUp(scrollby);
                cursorpos.X = 0;
                cursorpos.Y = TextSize.Height - 1;
            }
            else
                WrapWithCursor(() => { cursorpos.X = 0; cursorpos.Y = Math.Min(TextSize.Height - 1, cursorpos.Y + n); });
        }

        public void CursorPreviousLine(int n)
        {
            WrapWithCursor(() => { cursorpos.X = 0; cursorpos.Y = Math.Max(0, cursorpos.Y - n); });
        }

        public void ClearScreen()
        {
            for (int y = 0; y < TextSize.Height; y++)
                SetLineBlank(y);

            Refresh();
        }
        public void ClearCursorToScreenEnd()
        {
            int y = cursorpos.Y;
            SetLineBlank(y++, cursorpos.X);
            while (y <TextSize.Height)
                SetLineBlank(y++);
            Invalidate(GetTextRectangle(cursorpos.Y, TextSize.Height-1, 0, TextSize.Width - 1));
        }
        public void ClearCursorToScreenStart()
        {
            int y = cursorpos.Y;
            SetLineBlank(y--, 0, cursorpos.X);
            while (y >= 0)
                SetLineBlank(y--);
            Invalidate(GetTextRectangle(0, cursorpos.Y, 0, TextSize.Width - 1));
        }
        public void ClearCursorToLineEnd()
        {
            SetLineBlank(cursorpos.Y, cursorpos.X);
            Invalidate(GetTextRectangle(cursorpos.Y, cursorpos.Y, cursorpos.X, TextSize.Width - 1));
        }
        public void ClearCursorToLineStart()
        {
            SetLineBlank(cursorpos.Y, 0, cursorpos.X);      // inclusive of .X
            Invalidate(GetTextRectangle(cursorpos.Y, cursorpos.Y, 0, cursorpos.X));
        }
        public void ClearLine()
        {
            SetLineBlank(cursorpos.Y);
            Invalidate(GetTextRectangle(cursorpos.Y, cursorpos.Y, 0, TextSize.Width - 1));
        }

        public void ScrollUp(int count)
        {
            ScrollData(-count);
            Invalidate(GetTextRectangle(TextSize.Height - count, TextSize.Height + 0, 0, TextSize.Width));      // include extra line and char
        }
        public void ScrollDown(int count)
        {
            ScrollData(count);
            Invalidate(GetTextRectangle(0,count-1, 0, TextSize.Width));     // include extra char
        }

        #region Painting

        private void PanelTermWindow_Paint(object sender, PaintEventArgs e)
        {
            if (DesignMode)
            {
                using (var br = new SolidBrush(Color.Black))
                    e.Graphics.DrawString("Terminal text display in design mode", Font, br, new Point(10, 10));
                return;
            }

            Dictionary<Color, Brush> brushes = new Dictionary<Color, Brush>();

            int startx = Math.Min(MaxTextWidth-1, e.ClipRectangle.Left / CellSize.Width);                     // inclusive
            int stopx = Math.Min(MaxTextWidth-1, (e.ClipRectangle.Right - 1) / CellSize.Width);
            int starty = Math.Min(MaxTextHeight-1, e.ClipRectangle.Top / CellSize.Height);
            int stopy = Math.Min(MaxTextHeight-1, (e.ClipRectangle.Bottom - 1) / CellSize.Height);

            if (debug)
                System.Diagnostics.Debug.WriteLine($"Paint {e.ClipRectangle} client {ClientRectangle} paint x {startx}-{stopx} y {starty}-{stopy}");

            for (int y = starty; y <= stopy; y++)
            {
                bool drawn = false;

                for (int x = startx; x <= stopx; x++)
                {
                    CharCell c = Data[y][x];
                    var rect = new Rectangle(x * CellSize.Width, y * CellSize.Height, CellSize.Width, CellSize.Height);

                    if (!drawn && debug)
                    {
                        System.Diagnostics.Debug.Write($"Row {y}:{x}:");
                        drawn = true;
                    }

                    if (!brushes.TryGetValue(c.ForeColor, out Brush fore))
                    {
                        fore = brushes[c.ForeColor] = new SolidBrush(c.ForeColor);
                    }

                    if (!brushes.TryGetValue(c.BackColor, out Brush back))
                    {
                        back = brushes[c.BackColor] = new SolidBrush(c.BackColor);
                    }

                    DrawCell(e.Graphics, rect, fore, back, c.Char, showcursor && cursoron && x == cursorpos.X && y == cursorpos.Y);

                    if (drawn)
                    {
                        //System.Diagnostics.Debug.Write($"{x} ");
                        System.Diagnostics.Debug.Write($"{c.Char}");
                    }
                }
                if (drawn)
                {
                    System.Diagnostics.Debug.WriteLine($"");
                }
            }

            foreach (var kvp in brushes)
                kvp.Value.Dispose();
        }

        // immediate draw to cell
        private void DrawCell(int x, int y)
        {
            using (Graphics gr = panelTermWindow.CreateGraphics())
            {
                var cell = Data[y][x];
                using (Brush fore = new SolidBrush(cell.ForeColor))
                {
                    using (Brush back = new SolidBrush(cell.BackColor))
                    {
                        DrawCell(gr, new Rectangle(x * CellSize.Width, y * CellSize.Height, CellSize.Width, CellSize.Height),
                                    fore, back, cell.Char, showcursor && cursoron);
                    }
                }
            }
        }

        // draw a cell and the cursor if selected
        private void DrawCell(Graphics gr, Rectangle rect, Brush fore, Brush back, char ch, bool drawcursor)
        {
            gr.FillRectangle(back, rect);

            string str = new string(ch, 1);
            gr.DrawString(str, Font, fore, rect);

            if (drawcursor)
            {
                if (cursorshape == CursorShapeType.LineLeft)
                {
                    using (var pen = new Pen(cursorcolor, 1))
                    {
                        gr.DrawLine(pen, new Point(rect.Left, rect.Top + 1), new Point(rect.Left, rect.Bottom - 1));
                    }
                }
                else
                {
                    rect.Inflate(-1, -1);

                    using (var br = new SolidBrush(Color.FromArgb(180, cursorcolor)))
                    {
                        gr.FillRectangle(br, rect);
                    }
                }
            }
        }


        #endregion

        #region Cursor

        private void SetupCursor(bool onoff, Color colour, CursorShapeType shape, bool flsh)
        {
            if (showcursor && cursoron)       // remove from screen
            {
                cursoron = false;
                DrawCursorCell();
            }

            showcursor = onoff;
            cursoron = true;
            cursorcolor = colour;
            cursorshape = shape;
            cursorflash = flsh;

            if (showcursor)       // remove from screen
            {
                DrawCursorCell();
            }
        }

        private void Cursortimer_Tick(object sender, EventArgs e)
        {
            if (showcursor && cursorflash)
            {
                cursoron = !cursoron;
                DrawCursorCell();
            }
        }

        // immediate draw to cursor cell
        private void DrawCursorCell()
        {
            DrawCell(cursorpos.X, cursorpos.Y);
        }

        #endregion

        #region Overrides

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!DesignMode)
            {
                cursortimer.Start();
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            cursortimer.Stop();
            base.OnHandleDestroyed(e);
        }

        // recalc text and cell size. 
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetSizes();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnResize(e);
            SetSizes();
        }

        private void SetSizes()
        {
            CellSize = new Size(Font.Height * 3 / 4, Font.Height);
            int xsize = Math.Min(ClientRectangle.Width / CellSize.Width, MaxTextWidth);
            xsize = Math.Max(1, xsize);
            int ysize = Math.Min(ClientRectangle.Height / CellSize.Height, MaxTextHeight);
            ysize = Math.Max(1, ysize);
            TextSize = new Size(xsize, ysize);
            TextArea = new Size(CellSize.Width * TextSize.Width, CellSize.Height * TextSize.Height);
            cursorpos = new Point(Math.Min(TextSize.Width - 1, cursorpos.X), Math.Min(TextSize.Height - 1, cursorpos.Y));
            panelTermWindow.Size = TextArea;
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Tab:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        #endregion

        #region Helpers

        // inclusive of line start/end and x start/end
        private Rectangle GetTextRectangle(int linestart, int lineend, int xstart, int xend)
        {
            return new Rectangle(xstart * CellSize.Width, linestart * CellSize.Height, (xend - xstart + 1) * CellSize.Width, (lineend - linestart + 1) * CellSize.Height);
        }

        // inclusive of X start and end
        private void SetLineBlank(int y, int xstart = 0, int xend = MaxTextWidth - 1)
        {
            for (int x = xstart; x <= xend; x++)
            {
                if ( debug)
                    Data[y][x].Set("|.-.-"[x % 5], forecolor, backcolor);
                else
                    Data[y][x].Set(' ', forecolor, backcolor);
            }
        }

        private void MakeLine(int y)
        {
            for (int x = 0; x < MaxTextWidth; x++)
                Data[y][x] = new CharCell();
            SetLineBlank(y);
        }

        // scroll screen in -N (up) or +N (down) direction
        private void ScrollData(int dir)
        {
            if (dir < 0)      // scroll up
            {
                for (int y = 0; y < TextSize.Height; y++)
                {
                    int othery = y - dir;
                    if (othery >= TextSize.Height)
                    {
                        Data[y] = new CharCell[MaxTextWidth];
                        MakeLine(y);
                    }
                    else
                        Data[y] = Data[othery];
                }

                //int top = CellSize.Height * (TextSize.Height - 1 + dir); inv = new Rectangle(0, top, ClientRectangle.Width, ClientRectangle.Height - top);
            }
            else
            {
                for (int y = TextSize.Height - 1; y >= 0; y--)
                {
                    int othery = y - dir;
                    if (othery < 0)
                    {
                        Data[y] = new CharCell[MaxTextWidth];
                        MakeLine(y);
                    }
                    else
                        Data[y] = Data[othery];
                }

                //inv = new Rectangle(0, 0, ClientRectangle.Width, CellSize.Height * dir);
            }

            // we scroll the screen, and paint in the background of the part we moved, to minimise flicker

            using (Graphics gr = panelTermWindow.CreateGraphics())
            {
                using (Brush br = new SolidBrush(backcolor))
                {
                    if (dir < 0)
                    {
                        int offset = CellSize.Height * -dir;
                        int h = TextArea.Height - offset;
                        //int h = ClientRectangle.Height - offset;
                        //                        gr.CopyFromScreen(this.PointToScreen(new Point(0, offset)), new Point(0, 0), new Size(ClientRectangle.Width, h));
                        //gr.FillRectangle(br, new Rectangle(0, h,ClientRectangle.Width, offset));
             
                        gr.CopyFromScreen(panelTermWindow.PointToScreen(new Point(0, offset)), new Point(0, 0), new Size(TextArea.Width, h));
                        gr.FillRectangle(br, new Rectangle(0, h,TextArea.Width, offset));
                    }
                    else if (dir > 0)
                    {
                        int offset = CellSize.Height * dir;
                        //                        gr.CopyFromScreen(this.PointToScreen(new Point(0, 0)), new Point(0, offset), new Size(ClientRectangle.Width, ClientRectangle.Height - offset));
                        //                      gr.FillRectangle(br, new Rectangle(0, 0, ClientRectangle.Width, offset));
                        gr.CopyFromScreen(panelTermWindow.PointToScreen(new Point(0, 0)), new Point(0, offset), new Size(TextArea.Width, TextArea.Height - offset));
                        gr.FillRectangle(br, new Rectangle(0, 0, TextArea.Width, offset));
                    }
                }
            }
        }

        private void CursorOff()
        {
            if (showcursor && cursoron)       // if cursor visible
            {
                cursoron = false;
                DrawCursorCell();         // draw off
                cursoron = true;
            }
        }

        private void WrapWithCursor(Action op)
        {
            if (showcursor && cursoron)       // if cursor visible
            {
                cursoron = false;
                DrawCursorCell();         // draw off
            }

            op();

            if (showcursor)
            {
                cursoron = true;            // now immediately on
                cursortimer?.Stop(); cursortimer?.Start();
                DrawCursorCell();
            }

        }

        #endregion

        #region vars

        private bool debug = false;

        private Color backcolor = Color.Black;
        private Color forecolor = Color.White;
        private Timer cursortimer = null;

        private Point cursorpos;
        private bool showcursor = true;
        private bool cursoron = true;
        private Color cursorcolor = Color.White;
        private bool cursorflash = true;
        private CursorShapeType cursorshape = CursorShapeType.LineLeft;

        private CharCell[][] Data;

        const int MaxTextWidth = 256;
        const int MaxTextHeight = 128;

        [System.Diagnostics.DebuggerDisplay("Event '{Char}' {ForeColor} {BackColor}")]
        private class CharCell
        {
            public char Char;
            public Color ForeColor;
            public Color BackColor;

            public CharCell()
            {
                Char = ' '; ForeColor = Color.Red; BackColor = Color.Black;
            }
            public void Set(char ch, Color fore, Color back) { Char = ch; ForeColor = fore; BackColor = back; }
        }

        #endregion
    }

    public class PanelTerminal : Panel
    {
        protected override void OnPaintBackground(PaintEventArgs e)     // stop panel painting the background so we don't get a flick
        {
            if (DesignMode)
            {
                base.OnPaintBackground(e);  // does not appear to work perfectly, as design mode is not set
            }
        }
    }
}

