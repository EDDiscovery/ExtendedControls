/*
 * Copyright 2025-2025 EDDiscovery development team
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
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ExtendedControls
{
    // this is a coloured area, either solid (BackColor) or gradient filled
    public class FleetCarrierDockingPads : Control, IThemeable
    {
        #region Public

        public int SelectedIndex { get { return selectedIndex; } set { SetIndex(value); } }     // from 1 to 32 allowing squadron carriers

        // Font selected font family (not size, its autosized)
        // ForeColor is text colour
        public Color LargePad { get { return largePad; } set { largePad = value; Invalidate(); } }
        public Color MediumPad { get { return mediumPad; } set { mediumPad = value; Invalidate(); } }
        public Color SmallPad { get { return smallPad; } set { smallPad = value; Invalidate(); } }
        public Color NonSelected { get { return nonselectedPad; } set { nonselectedPad = value; Invalidate(); } }
        public bool IsVertical { get { return imagevertical; } }

        public FleetCarrierDockingPads()
        {
            ForeColor = Color.Black;
            timer.Tick += T_Tick;
            timer.Interval =50;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            sourceimage = Properties.Resources.Drake_carrier;
        }

        public void SetOrientation(bool vertical)
        {
            if (imagevertical)          // if was vertical, dispose
            {
                sourceimage.Dispose();
            }

            if (vertical)
            {
                sourceimage = new Bitmap(Properties.Resources.Drake_carrier);
                sourceimage.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            else
            {
                sourceimage = Properties.Resources.Drake_carrier;
            }

            image.Dispose();
            image = null;
            imagevertical = vertical;
            //sourceimage.Save(@"c:\code\image.png");
            PerformLayout();
            Invalidate();
        }


        // padsize 1 to 3, 0 invalid
        public int PadSize(int pad)
        {
            return pad >= 1 && pad < padsize.Length ? padsize[pad] : 0;
        }

        #endregion

        #region Implementation

        private void SetIndex(int index)
        {
            if (index != selectedIndex)     // only when changed
            {
                timer.Stop();

                selectedIndex = index;
                selectedintensity = 1.0f;

                if (selectedIndex > 0)
                {
                    timer.Start();
                    starttime = (uint)Environment.TickCount;
                }

                DrawImage();                // force a redraw and invalidate
                Invalidate();
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            pads = new List<GraphicsPath>();
            pads.Add(null);     // 0 is empty

            int imageh = (int)(Width * imageheight / imagewidth);       // scaling if horizontal for height. H/W is ratio of image
            int imagew = (int)(Height * imageheight / imagewidth);      // scaling if vertical for width

            // keep ratio of image and don't stretch
            imagepos =  imagevertical ? new Rectangle( (Width- imagew) / 2, 0, imagew, Height) : new Rectangle(0, (Height - imageh) / 2, Width, imageh);

            //System.Diagnostics.Debug.WriteLine($"In {Width}x{Height} image is {imagepos}");

            for (int pad = 1; pad < padsize.Length; pad++)
            {
                PointF size = padpixelsizes[PadSize(pad)];
                PointF centre = padpixelcentres[pad];

                if ( imagevertical)     // if vertical, our co-ords need some translation
                {
                    centre = new PointF(centre.Y, 1.0f-centre.X);       // flip axis, correct for orientation up
                    size = new PointF(size.Y, size.X);
                }

                Point xyc = new Point((int)(imagepos.Width * centre.X), (int)(imagepos.Height * centre.Y));
                Point sz = new Point((int)(imagepos.Width * size.X / 2.0f), (int)(imagepos.Height * size.Y / 2.0f));

                //System.Diagnostics.Debug.WriteLine($"Pad {pad} {size} {centre} = {xyc} {sz}");
                Point p1 = new Point(imagepos.X + xyc.X - sz.X, imagepos.Y + xyc.Y - sz.Y);
                Point p2 = new Point(imagepos.X + xyc.X + sz.X, imagepos.Y + xyc.Y - sz.Y);
                Point p3 = new Point(imagepos.X + xyc.X + sz.X, imagepos.Y + xyc.Y + sz.Y);
                Point p4 = new Point(imagepos.X + xyc.X - sz.X, imagepos.Y + xyc.Y + sz.Y);

                GraphicsPath path = new GraphicsPath();
                path.AddLines(new Point[] { p1, p2, p3, p4 });
                pads.Add(path);
            }
        }


        // Draw to bitmap, this is done to prevent flicker during indication of pad

        private void DrawImage()
        {
            image?.Dispose();
            image = new Bitmap(Math.Max(1,Width), Math.Max(1,Height));

            using (Graphics gr = Graphics.FromImage(image))
            {
                //System.Diagnostics.Debug.WriteLine("Draw image");
               //using (Brush br1 = new SolidBrush(Color.Yellow))  gr.FillRectangle(br1, imagepos);

               gr.DrawImage(sourceimage, imagepos, 0,0, sourceimage.Width, sourceimage.Height, GraphicsUnit.Pixel);

                if (selectedIndex > 0 )
                {
                    int selindexmod = (selectedIndex - 1) % 16 + 1;

                    for (int pad = 1; pad < padsize.Length; pad++)
                    {
                        Color fill = selindexmod != pad ? nonselectedPad : padsize[pad] == 3 ? LargePad : padsize[pad] == 2 ? MediumPad : SmallPad;
                        
                        byte intensity = (byte)(255 * (selindexmod == pad ? selectedintensity : nonselectedintensity));
                            
                        //System.Diagnostics.Debug.WriteLine($"Draw pad {pad} {intensity}");
                        fill = Color.FromArgb(intensity,fill.R,fill.G,fill.B);
                        using (Brush br = new SolidBrush(fill))
                        {
                            gr.FillPath(br, pads[pad]);
                        }
                    }

                    var bounds = pads[selindexmod].GetBounds();

                    using (Font fnt = new Font(Font.FontFamily, (float)Math.Max(2, bounds.Width / 3)))
                    {
                        using (Brush br = new SolidBrush(ForeColor))
                        {
                            using (StringFormat form = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                            {
                                //   System.Diagnostics.Debug.WriteLine($"Radius {pad} Bounds {bounds}");
                                gr.DrawString($"{selectedIndex}", fnt, br, bounds, form);
                            }
                        }

                        fnt.Dispose();
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (image == null)      // ensure we have an image, start up scenario
                DrawImage();

            e.Graphics.DrawImage(image,new Point(0,0));
        }

        private void T_Tick(object sender, EventArgs e)
        {
            uint timesince = (uint)Environment.TickCount - starttime;
            double timemod = Math.Abs(((int)(timesince % 2000) - 1000) / 1000.0);
            selectedintensity = 0.4f + 0.6f * (float)timemod;
            DrawImage();
            Invalidate();
        }

        // no children
        public bool Theme(Theme t, Font fnt)        
        {
            return false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            DrawImage();
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            DrawImage();
            Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                timer.Stop();
                if (imagevertical)
                    sourceimage.Dispose();
            }
        }

        private Color largePad = Color.FromArgb(255, 192, 1, 1), mediumPad = Color.FromArgb(255, 192, 192, 0), smallPad = Color.FromArgb(255, 0, 192, 192), nonselectedPad = Color.FromArgb(255, 64, 64, 64);
        private int selectedIndex = 0;
        private List<GraphicsPath> pads = new List<GraphicsPath>();
        private Rectangle imagepos;
        private bool imagevertical = false;
        private Image sourceimage;
        private Timer timer = new Timer();
        private uint starttime;
        private float selectedintensity = 1.0f;
        private float nonselectedintensity = 0.9f;

        private static int[] padsize = new int[] {0,    // entry 0 unused
                                         3,3,3,3,       // 1
                                         3,3,3,3,       // 5
                                         2,2,           // 9-10
                                         2,2,           // 11-12
                                         1,1,1,1,       // 13-16
                                            };

        const float imagewidth = 975.0f;
        const float imageheight = 243.0f;
        private static PointF[] padpixelcentres = new PointF[] {
                            PointF.Empty,
                            new PointF(356.0f/imagewidth,85.5f/imageheight),        //1
                            new PointF(356.0f/imagewidth,150.5f/imageheight),
                            new PointF(438.0f/imagewidth,85.5f/imageheight),        //3
                            new PointF(438.0f/imagewidth,150.5f/imageheight),
                            new PointF(519.0f/imagewidth,85.5f/imageheight),        //5
                            new PointF(519.0f/imagewidth,150.5f/imageheight),
                            new PointF(599.5f/imagewidth,85.5f/imageheight),        //7
                            new PointF(599.5f/imagewidth,150.5f/imageheight),

                            new PointF(352.0f/imagewidth,25.5f/imageheight),        //9-10
                            new PointF(414.0f/imagewidth,25.5f/imageheight),
                            new PointF(352.0f/imagewidth,210.0f/imageheight),       //11-12
                            new PointF(414.0f/imagewidth,210.0f/imageheight),
                            new PointF(463.5f/imagewidth,11.5f/imageheight),        //13
                            new PointF(463.5f/imagewidth,224.0f/imageheight),       //14
                            new PointF(463.5f/imagewidth,201.0f/imageheight),       //15
                            new PointF(463.5f/imagewidth,34.0f/imageheight),        //16
                            };

        private static PointF[] padpixelsizes = new PointF[]
        {
            PointF.Empty,
            new PointF(21.0f/imagewidth,16.0f/imageheight),        // small
            new PointF(41.0f/imagewidth,23.0f/imageheight),        // med
            new PointF(58.0f/imagewidth,36.0f/imageheight),        // large
        };

        private Bitmap image;

        #endregion
    }
}
