
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConsoleTerminal
{
    public partial class Terminal : UserControl, ITerminal
    {
        public Color VTBackColor { get { return backcolor; } set { backcolor = value; }  }
        public Color VTForeColor { get { return forecolor; } set { forecolor = value; } }

        public Size TextSize { get; private set; }
        public Size TextArea { get; private set; }
        public Size CellSize { get; private set; }

        public enum CursorShapeType { Block, LineLeft };

        // all of these have active actions, they are not for the designer

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Point CursorPosition { get { return cursorpos; } set { WrapFunction(() => { cursorpos = new Point(value.X.Range(0, TextSize.Width - 1), value.Y.Range(0, TextSize.Height - 1)); }); } }

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
        public bool CursorFlashes { get { return cursorflash; } set { SetupCursor(ShowCursor, CursorColor, CursorShape, value); } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int CursorFlashRate { get { return cursortimer.Interval; } set { cursortimer.Stop(); cursortimer.Interval = value; cursortimer.Start(); } }

        public Terminal()
        {
            InitializeComponent();

            cursortimer = new Timer();
            cursortimer.Interval = 500;
            cursortimer.Tick += Cursortimer_Tick;

            bitmap = new Bitmap(1920, 1024);
            ClearBitmap();
            fmt.LineAlignment = StringAlignment.Center;
            fmt.Alignment = StringAlignment.Center;
        }

        public void AddTextClear(ref StringBuilder sb)
        {
            AddText(sb.ToString());
            sb.Clear();
        }

        // understands \r \n \b

        public void AddText(string str)
        {
            foreach (var ch in str)
            {
                bool lfit = false;
                if (ch == '\r')
                {
                    cursorpos.X = 0;
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
                        DrawCell(' ');
                    }
                }
                else
                {
                    DrawCell(ch);       // overwrites the cursor
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
                        ScrollBitmap(-1);
                        cursorpos.Y--;
                    }
                }
            }

            Refresh();
        }

        public void CR()
        {
            WrapFunction(() => { cursorpos.X = 0; });
        }

        public void LF()
        {
            WrapFunction(() =>
            {
                if (++cursorpos.Y >= TextSize.Height)
                {
                    cursorpos.Y--;
                    ScrollBitmap(-1);
                }
            });
        }

        public void CRLF()
        {
            WrapFunction(() =>
            {
                cursorpos.X = 0;
                if (++cursorpos.Y >= TextSize.Height)
                {
                    ScrollBitmap(-1);
                    cursorpos.Y--;
                }
            });
        }
        public void Home()
        {
            WrapFunction(() =>
            {
                CursorPosition = new Point(0, 0);
            });
        }

        public void BackSpace()
        {
            if (cursorpos.X > 0)
            {
                WrapFunction(() =>
                {
                    cursorpos.X--;
                    DrawCell(' ');
                });
            }
        }

        public void TabForward(int tabspacing)
        {
            WrapFunction(() => { cursorpos.X = Math.Min(TextSize.Width - 1, (cursorpos.X + tabspacing) / tabspacing * tabspacing); });
        }

        public void CursorUp(int n, bool scrollonedge = false)
        {
            WrapFunction(() =>
            {
                if (scrollonedge && cursorpos.Y - n < 0)        // if need to scroll
                {
                    int scrollby = n - cursorpos.Y;
                    ScrollBitmap(scrollby);
                    cursorpos.Y = 0;
                }
                else
                {
                    cursorpos.Y = Math.Max(0, cursorpos.Y - n);
                }
            });
        }
        public void CursorDown(int n, bool scrollonedge = false)
        {
            WrapFunction(() =>
            {
                if (scrollonedge && cursorpos.Y + n >= TextSize.Height)        // if need to scroll
                {
                    int scrollby = cursorpos.Y + n - TextSize.Height + 1;
                    ScrollBitmap(scrollby);
                    cursorpos.Y = TextSize.Height - 1;
                }
                else
                {
                    cursorpos.Y = Math.Min(TextSize.Height - 1, cursorpos.Y + n);
                }
            });
        }
        
        public void CursorBack(int n)
        {
            WrapFunction(() => { cursorpos.X = Math.Max(0, cursorpos.X - n); });
        }
        public void CursorForward(int n)
        {
            WrapFunction(() => { cursorpos.X = Math.Min(TextSize.Width - 1, cursorpos.X + n); });
        }
        public void CursorNextLine(int n, bool scrollonedge = false)
        {
            WrapFunction(() =>
            {
                if (scrollonedge && cursorpos.Y + n >= TextSize.Height)        // if need to scroll
                {
                    int scrollby = cursorpos.Y + n - TextSize.Height + 1;
                    ScrollBitmap(scrollby);
                    cursorpos.X = 0;
                    cursorpos.Y = TextSize.Height - 1;
                }
                else
                {
                    cursorpos.X = 0; cursorpos.Y = Math.Min(TextSize.Height - 1, cursorpos.Y + n);
                }
            });
        }

        public void CursorPreviousLine(int n)
        {
            WrapFunction(() => { cursorpos.X = 0; cursorpos.Y = Math.Max(0, cursorpos.Y - n); });
        }

        public void ClearScreen()
        {
            ClearBitmap();
            Refresh();
        }


        public void ClearCursorToScreenEnd()
        {
            WrapFunction(() =>
            {
                int y = cursorpos.Y;
                DrawBlank(y++, cursorpos.X, TextSize.Width - 1);
                while (y < TextSize.Height)
                    DrawBlank(y++, 0, TextSize.Width - 1);
            });
        }

        public void ClearCursorToScreenStart()
        {
            WrapFunction(() =>
            {
                int y = cursorpos.Y;
                DrawBlank(y--, 0, cursorpos.X);
                while (y >= 0)
                    DrawBlank(y--, 0, TextSize.Width - 1);
            });
        }
        public void ClearCursorToLineEnd()
        {
            WrapFunction(() =>
            {
                DrawBlank(cursorpos.Y, cursorpos.X, TextSize.Width - 1);
            });
        }
        public void ClearCursorToLineStart()
        {
            WrapFunction(() =>
            {
                DrawBlank(cursorpos.Y, 0, cursorpos.X);
            });
        }
        public void ClearLine()
        {
            WrapFunction(() =>
            {
                DrawBlank(cursorpos.Y, 0, TextSize.Width-1);
            });
        }

        public void ScrollUp(int count)
        {
            WrapFunction(() => { ScrollBitmap(-Math.Min(TextSize.Height - 1, count)); });
        }
        public void ScrollDown(int count)
        {
            WrapFunction(() => { ScrollBitmap(Math.Min(TextSize.Height - 1, count)); });
        }

        #region Painting

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height),
                            0, 0, Math.Min(ClientRectangle.Width, bitmap.Width), Math.Min(ClientRectangle.Height, bitmap.Height), GraphicsUnit.Pixel);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if ( showcursor && cursorperiodison )
            {
                Rectangle rect = new Rectangle(CellSize.Width * cursorpos.X, CellSize.Height * cursorpos.Y, CellSize.Width, CellSize.Height);
                if (cursorshape == CursorShapeType.LineLeft)
                {
                    using (var pen = new Pen(cursorcolor, 1))
                    {
                        e.Graphics.DrawLine(pen, new Point(rect.Left, rect.Top + 1), new Point(rect.Left, rect.Bottom - 1));
                    }
                }
                else
                {
                    rect.Inflate(-1, -1);

                    using (var br = new SolidBrush(Color.FromArgb(180, cursorcolor)))
                    {
                        e.Graphics.FillRectangle(br, rect);
                    }
                }
            }
        }

        // draw to cell
        private void DrawCell(char ch)
        {
            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                using (Brush fore = new SolidBrush(forecolor))
                {
                    using (Brush back = new SolidBrush(backcolor))
                    {
                        Rectangle rect = new Rectangle(CellSize.Width * cursorpos.X, CellSize.Height * cursorpos.Y, CellSize.Width, CellSize.Height);
                        string str = new string(ch, 1);
                        gr.FillRectangle(back, rect);
                        gr.DrawString(str, Font, fore, rect, fmt);
                    }
                }
            }
        }

        // draw blank area
        private void DrawBlank(int y, int xstartinc, int xendinc)
        {
            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                using (Brush back = new SolidBrush(Color.Blue))
                {
                    Rectangle rect = new Rectangle(CellSize.Width * xstartinc, CellSize.Height * y, CellSize.Width * (xendinc-xstartinc+1), CellSize.Height);
                    gr.FillRectangle(back, rect);
                }
            }
        }

        #endregion

        #region Cursor

        private void SetupCursor(bool onoff, Color colour, CursorShapeType shape, bool flsh)
        {
            showcursor = onoff;
            cursorperiodison = true;
            cursorcolor = colour;
            cursorshape = shape;
            cursorflash = flsh;

            Refresh();
        }

        private void Cursortimer_Tick(object sender, EventArgs e)
        {
            if (showcursor && cursorflash)
            {
                cursorperiodison = !cursorperiodison;
                Refresh();
            }
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
            ClearBitmap();
            SetSizes();
            Refresh();
        }

        private void SetSizes()
        {
            using (Bitmap bmp = new Bitmap(1, 1))
            {
                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    var size = gr.MeasureString("ABCDEFGHIJKLMNOPQRSTUVWXYZ", Font);
                    CellSize = new Size((int)(size.Width/26)+3, (int)(size.Height)+1);
                }
            }

            int xsize = Math.Min(ClientRectangle.Width / CellSize.Width, bitmap.Width/CellSize.Width);
            xsize = Math.Max(1, xsize);
            int ysize = Math.Min(ClientRectangle.Height / CellSize.Height, bitmap.Height/CellSize.Height);
            ysize = Math.Max(1, ysize);
            TextSize = new Size(xsize, ysize);
            TextArea = new Size(CellSize.Width * TextSize.Width, CellSize.Height * TextSize.Height);
            cursorpos = new Point(Math.Min(TextSize.Width - 1, cursorpos.X), Math.Min(TextSize.Height - 1, cursorpos.Y));
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

        // scroll screen in -N (up) or +N (down) direction
        private void ScrollBitmap(int dir)
        {
            int offset = CellSize.Height * Math.Abs(dir);

            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                using (Brush br = new SolidBrush(backcolor))
                {
                    if (dir < 0)
                    {
                        gr.DrawImage(bitmap, new Rectangle(0, 0, TextArea.Width, TextArea.Height - offset), new Rectangle(0, offset, TextArea.Width, TextArea.Height - offset), GraphicsUnit.Pixel);
                        gr.FillRectangle(br, new Rectangle(0, TextArea.Height - offset, TextArea.Width, offset));
                    }
                    else
                    {
                        gr.DrawImage(bitmap, new Rectangle(0, offset, TextArea.Width, TextArea.Height - offset), new Rectangle(0, 0, TextArea.Width, TextArea.Height - offset), GraphicsUnit.Pixel);
                        gr.FillRectangle(br, new Rectangle(0, 0, TextArea.Width, offset));
                    }
                }
            }

        }

        void ClearBitmap()
        {
            using (Graphics gr = Graphics.FromImage(bitmap))
                gr.Clear(backcolor);
        }

        // this is just so we can easily add more functionality to multiple funcs
        private void WrapFunction(Action op)
        {
            op();
            Refresh();
        }

        #endregion

        #region vars

        private Color backcolor = Color.Black;
        private Color forecolor = Color.White;
        private Timer cursortimer = null;

        private Point cursorpos;
        private bool showcursor = true;
        private bool cursorperiodison = true;
        private Color cursorcolor = Color.White;
        private bool cursorflash = true;
        private CursorShapeType cursorshape = CursorShapeType.LineLeft;

        private StringFormat fmt = new StringFormat();

        private Bitmap bitmap = new Bitmap(20, 20);

        #endregion
    }


}

