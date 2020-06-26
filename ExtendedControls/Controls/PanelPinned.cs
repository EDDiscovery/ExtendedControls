/*
 * Copyright © 2016-2020 EDDiscovery development team
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class ExtPanelPinned : Panel
    {
        private bool docked;

        public int PinSize { get; set; }

        public ExtPanelPinned()
        {
            SuspendLayout();

            ResumeLayout(false);
            docked = false;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ExtPanelPinned
            //
            Dock = DockStyle.None;
            this.PinSize = 24;
            this.Size = new Size(24, 24);
            this.ResumeLayout(false);
            this.Location = new Point(0, 0);
        }

        public ExtPanelPinned(IContainer container)
        {
            container.Add(this);
        }

        protected override Size DefaultSize
        {
            get { return new Size(24, 24); }
        }

        [Browsable(false)]
        public override bool AutoSize
        {
            get { return false; }
            set { base.AutoSize = false; }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            Dock = DockStyle.None;
            PinSize = 24;
            ResumeLayout(false);
            this.Size = new System.Drawing.Size(24, 24);
            this.Location = new Point(0, 0);
            base.OnHandleCreated(e);
        }
        
        private void UnDockPanel()
        {
            this.Size = new System.Drawing.Size(PinSize, PinSize);
        }

        private void DockPanel()
        {
            this.Size = new System.Drawing.Size(this.Parent.Width, PinSize);            
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            using (var g = e.Graphics)
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                var dockedLine = new Pen(Color.DarkOrange);

                var undockedFill = new SolidBrush(Color.Orange);
                var dockedFill = new SolidBrush(Color.Black);
                var undockedBounds = new Rectangle(0, 0, PinSize / 3, PinSize);
                var dockedBounds = new Rectangle(0, -1, this.Width, this.Height + 1);

                g.FillRectangle(dockedFill, dockedBounds);
                g.FillRectangle(undockedFill, undockedBounds);
                g.DrawRectangle(dockedLine, dockedBounds);

                if (!docked)
                {
                    g.FillEllipse(new SolidBrush(Color.Red), new RectangleF(new PointF(PinSize / 9, PinSize / 9), new Size(PinSize / 5, PinSize / 5)));
                }
                else
                {
                    g.FillEllipse(new SolidBrush(Color.Blue), new RectangleF(new PointF(PinSize / 6, PinSize / 6), new Size(PinSize - PinSize / 4, PinSize - PinSize / 4)));
                    g.FillEllipse(new SolidBrush(Color.Orange), new RectangleF(new PointF(PinSize / 4, PinSize / 4), new Size(PinSize / 2, PinSize / 2)));
                }
            }
            base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        public void Reload()
        {
            Dock = DockStyle.None;
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Location.X < PinSize)
            {
                if (docked)
                {
                    docked = false;
                    UnDockPanel();
                    this.Invalidate();
                }
                else
                {
                    docked = true;
                    DockPanel();
                    this.Invalidate();
                }
            }
        }
    }
}