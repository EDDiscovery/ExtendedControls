/*
 * Copyright 2022-2025 EDDiscovery development team
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
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls.Controls
{
    public partial class ImageControl : Control, IThemeable
    {
        public Size ImageSize { get; set; } = new Size(128, 128);               // changing this wipes the bitmap at the next draw
        public int ImageDepth { get; set; } = 1;                               // how many bitmaps to overlay
        public bool[] ImageVisible { get; set; } = new bool[1];
        public int ImageWidth { get { return ImageSize.Width; } }
        public int ImageHeight { get { return ImageSize.Height; } }
        public ImageLayout ImageLayout { get; set; } = ImageLayout.Stretch;     // only stretch/none at the moment
        public Color ImageBackgroundColor { get; set; } = Color.Transparent;

        public class ClickableArea
        {
            public Rectangle Location { get; set; }
            public string ToolTipText { get; set; }
            public object Tag { get; set; }
        }

        public Action<ImageControl, ClickableArea, Point, MouseEventArgs> LeaveMouseArea;
        public Action<ImageControl, ClickableArea, Point, MouseEventArgs> EnterMouseArea;
        public Action<ImageControl, ClickableArea, Point, MouseEventArgs> ClickOnMouseArea;

        public ImageControl()
        {
            hovertimer.Interval = 250;
            hovertimer.Tick += HoverEnd;
        }

        public void AddMouseArea(Rectangle area, string tooltiptext = null, object tag = null)
        {
            mouseareas.Add(new ClickableArea() { Location = area, ToolTipText = tooltiptext, Tag = tag });
        }
        public void AddMouseArea(RectangleF area, string tooltiptext = null, object tag = null)
        {
            mouseareas.Add(new ClickableArea() { Location = new Rectangle((int)area.X, (int)area.Y, (int)(area.Width+0.999), (int)(area.Height+0.999)), ToolTipText = tooltiptext, Tag = tag });
        }
        public void RemoveMouseArea(Rectangle area)
        {
            var entry = mouseareas.Find(x => x.Location == area);
            if (entry != null)
                mouseareas.Remove(entry);
        }
        
        // may be null
        public ClickableArea FindMouseArea(Rectangle area)
        {
            return mouseareas.Find(x => x.Location == area);
        }


        // manually invalidate afterwards all of these

        // Draw Text in rectangle. backcolor = null means don't draw backcolour.
        public void DrawText(Rectangle area, string text, Font dp, Color forecolor, Color? backcolor = null,
                                                    float backscale = 1.0F, StringFormat frmt = null, int angleback = 90, int bitmap = 0)
        {
            CheckBitmap();
            BaseUtils.BitMapHelpers.DrawTextIntoBitmap(bitmaps[bitmap], area, text, dp, forecolor, backcolor, backscale, frmt, angleback);
        }

        // Draw measure Text in rectangle. backcolor = null means don't draw backcolour.  Gives the actual drawing area
        public Rectangle DrawMeasureText(Rectangle area, string text, Font dp, Color forecolor, Color? backcolor = null,
                                                    float backscale = 1.0F, StringFormat frmt = null, int angleback = 90, int bitmap = 0)
        {
            DrawText(area, text, dp, forecolor, backcolor, backscale, frmt, angleback, bitmap);
            var size = BaseUtils.BitMapHelpers.MeasureStringInBitmap(text, dp, frmt, area.Size);
            return new Rectangle(area.X, area.Y, (int)(size.Width + 1), (int)(size.Height + 1));
        }

        // Draw Text in rectangle. backcolor = null means don't draw backcolour.
        public void DrawText(Point pos, Size maxsize, string text, Font dp, Color forecolor, Color? backcolor = null,
                                                    float backscale = 1.0F, StringFormat frmt = null, int angleback = 90, int bitmap = 0)
        {
            CheckBitmap();
            BaseUtils.BitMapHelpers.DrawTextIntoBitmap(bitmaps[bitmap], pos, maxsize, text, dp, forecolor, backcolor, backscale, frmt, angleback);
        }

        // Draw measure Text at pos maxsize. backcolor = null means don't draw backcolour.  Gives the actual drawing area
        public Rectangle DrawMeasureText(Point pos, Size maxsize, string text, Font dp, Color forecolor, Color? backcolor = null,
                                                    float backscale = 1.0F, StringFormat frmt = null, int angleback = 90, int bitmap = 0)
        {
            CheckBitmap();
            SizeF size = BaseUtils.BitMapHelpers.DrawTextIntoBitmap(bitmaps[bitmap], pos, maxsize, text, dp, forecolor, backcolor, backscale, frmt, angleback);
            if ( size == Size.Empty)        // only if backcolour was set does DrawTextintoBitmap return values.
                size = BaseUtils.BitMapHelpers.MeasureStringInBitmap(text, dp, frmt, maxsize);
            return new Rectangle(pos.X, pos.Y, (int)(size.Width + 1), (int)(size.Height + 1));
        }

        // draw lines. The area determines the part to draw the backcolour on, and to size any centre formatting against
        public Rectangle DrawText(Rectangle area, string[] text, Font dp, int linespacing, Color forecolor, Color? backcolor = null,
                                                    float backscale = 1.0F, StringFormat frmt = null, int angleback = 90, int bitmap = 0)
        {
            CheckBitmap();

            bool firstforback = true;       // only the first draw get the back colour, so we get the right size back

            foreach (var x in text)
            {
                if (x != null)
                {
                    BaseUtils.BitMapHelpers.DrawTextIntoBitmap(bitmaps[bitmap], area, x, dp, forecolor, firstforback ? backcolor: null, backscale, frmt, angleback);
                    area.Y += dp.Height + linespacing;
                    firstforback = false;
                }
            }

            return new Rectangle(area.Left, area.Y - linespacing, area.Width, area.Height);
        }

        // draw lines. Each area is individually back coloured
        public Point DrawText(Point pos, Size maxsize, string[] text, Font dp, int linespacing, Color forecolor, Color? backcolor = null,
                                                    float backscale = 1.0F, StringFormat frmt = null, int angleback = 90, int bitmap = 0)
        {
            CheckBitmap();

            foreach (var x in text)
            {
                if (x != null)
                {
                    BaseUtils.BitMapHelpers.DrawTextIntoBitmap(bitmaps[bitmap], pos, maxsize, x, dp, forecolor, backcolor, backscale, frmt, angleback);
                    pos.Y += dp.Height + linespacing;
                }
            }
            return new Point(pos.X, pos.Y - linespacing);
        }

        public void DrawImage(Image img, Rectangle area, int bitmap = 0)
        {
            CheckBitmap();
            using (Graphics gr = Graphics.FromImage(bitmaps[bitmap]))
                gr.DrawImage(img, area);
        }

        // remember to use Using to release the graphics
        public Graphics GetGraphics(int bitmap = 0)     
        {
            CheckBitmap();
            return Graphics.FromImage(bitmaps[bitmap]);      
        }

        // clear either all bitmaps, or a specific level
        public void Clear(int level = -1)
        {
            if (bitmaps != null)
            {
                if (level == -1)
                {
                    foreach (var b in bitmaps)
                    {
                        using (Graphics gr = Graphics.FromImage(b))
                            gr.Clear(ImageBackgroundColor);
                    }
                }
                else
                {
                    using (Graphics gr = Graphics.FromImage(bitmaps[level]))
                        gr.Clear(ImageBackgroundColor);
                }
            }
        }

        #region Implementation

        private void CheckBitmap()
        {
            if (bitmaps == null || bitmaps[0].Size != ImageSize || bitmaps.Length != ImageDepth)
            {
                if (bitmaps != null)
                {
                    foreach (var b in bitmaps)
                        b?.Dispose();      // previous one, gone

                    bitmaps = null;
                }

                ImageVisible = new bool[ImageDepth];

                bitmaps = new Bitmap[ImageDepth];
                for (int i = 0; i < bitmaps.Length; i++)
                {
                    bitmaps[i] = new Bitmap(ImageSize.Width, ImageSize.Height);
                    ImageVisible[i] = true;
                }

                if (ImageBackgroundColor != Color.Transparent)
                    Clear();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (BackgroundImage != null)
            {
                if (BackgroundImageLayout == ImageLayout.Stretch)
                {
                    pevent.Graphics.DrawImage(BackgroundImage, ClientRectangle);
                }
                else
                {
                    if (BackgroundImage.Width < Width || BackgroundImage.Height < Height)
                    {
                        using (Brush br = new SolidBrush(BackColor))
                        {
                            pevent.Graphics.FillRectangle(br, ClientRectangle);
                        }
                    }

                    pevent.Graphics.DrawImage(BackgroundImage, new Point(0, 0));
                }
            }
            else
            {
                using (Brush br = new SolidBrush(BackColor))
                {
                    pevent.Graphics.FillRectangle(br, ClientRectangle);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if ( bitmaps != null )
            {
                if (ImageLayout == ImageLayout.Stretch)
                {
                    for (int i = 0; i < bitmaps.Length; i++)
                    {
                        if (ImageVisible[i])
                            pe.Graphics.DrawImage(bitmaps[i], ClientRectangle);
                    }
                }
                else
                {
                    for (int i = 0; i < bitmaps.Length; i++)
                    {
                        if (ImageVisible[i])
                            pe.Graphics.DrawImage(bitmaps[i], new Point(0, 0));
                    }
                }
            }
        }

        #endregion

        #region Mouse

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            double xscale = (double)ImageSize.Width / Width;      // scale back to image size
            double yscale = (double)ImageSize.Height / Height;

            Point scaledpos = ImageLayout == ImageLayout.Stretch ? new Point((int)(e.X * xscale), (int)(e.Y * yscale)) : e.Location;

            if (elementin != null && !elementin.Location.Contains(scaledpos))       // go out..
            {
                //System.Diagnostics.Debug.WriteLine($"leave {elementin.Location}");
                LeaveMouseArea?.Invoke(this, elementin, scaledpos, e);
                elementin = null;
            }

            if (elementin == null)      // is in?
            {
                foreach (var i in mouseareas)
                {
                    if (i.Location.Contains(scaledpos))
                    {
                        elementin = i;

                        //System.Diagnostics.Debug.WriteLine($"enter {elementin.Location}");

                        EnterMouseArea?.Invoke(this, elementin,scaledpos,e);
                    }
                }
            }

            if (Math.Abs(e.X - hoverpos.X) + Math.Abs(e.Y - hoverpos.Y) > 8 || elementin == null)
            {
                //System.Diagnostics.Debug.WriteLine($"mouse move, clear hover timer");
                ClearHoverTip();
            }

            if (elementin != null && !hovertimer.Enabled && hovertip == null)
            {
                hoverpos = e.Location;
                hovertimer.Start();
                //System.Diagnostics.Debug.WriteLine($"start hover timer {elementin.Location}");
            }
        }

        void ClearHoverTip()
        {
            hovertimer.Stop();

            if (hovertip != null)
            {
                hovertip.Dispose();
                hovertip = null;
            }
        }

        void HoverEnd(object sender, EventArgs e)
        {
            hovertimer.Stop();

            //System.Diagnostics.Debug.WriteLine($"hover timed out {elementin?.Location}");
            if (elementin != null && elementin.ToolTipText.HasChars())
            {
                hovertip = new ToolTip();

                hovertip.InitialDelay = 0;
                hovertip.AutoPopDelay = 30000;
                hovertip.ReshowDelay = 0;
                hovertip.IsBalloon = true;
                hovertip.ShowAlways = true;
                hovertip.SetToolTip(this, elementin.ToolTipText);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            Focus();

            ClearHoverTip();

            //System.Diagnostics.Debug.WriteLine($"Mouse clicked on {elementin?.Location} @ {e.Location}");

            double xscale = (double)ImageSize.Width / Width;      // scale back to image size
            double yscale = (double)ImageSize.Height / Height;

            Point scaledpos = ImageLayout == ImageLayout.Stretch ? new Point((int)(e.X * xscale), (int)(e.Y * yscale)) : e.Location;

            ClickOnMouseArea?.Invoke(this, elementin, scaledpos, e);          // elementin = null if no element clicked
        }

        public bool Theme(Theme t, Font fnt)
        {
            return false; // no children, no action
        }


        #endregion

        #region Vars

        private Bitmap[] bitmaps = null;
        private List<ClickableArea> mouseareas = new List<ClickableArea>();

        private ClickableArea elementin = null;
        private Timer hovertimer = new Timer();
        private ToolTip hovertip = null;
        private Point hoverpos;     // window co-ords, not scaled

        #endregion
    }
}
