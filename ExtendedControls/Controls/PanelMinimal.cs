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

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ExtendedControls
{
    public partial class ExtPanelMinimal : Panel
    {
        private bool dockedState;

        public int PinSize { get; set; }

        public ExtPanelMinimal()
        {
            SuspendLayout();

            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Size = new System.Drawing.Size(PinSize, PinSize);

            ResumeLayout(false);

            dockedState = false;
            PinSize = 32;
        }

        private void UnDockPanel()
        {
            this.Size = new System.Drawing.Size(PinSize, PinSize);
        }

        private void DockPanel()
        {
            this.Size = new System.Drawing.Size(this.Parent.Width, PinSize);
        }

        public ExtPanelMinimal(IContainer container)
        {
            container.Add(this);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
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
                var undockedBounds = new Rectangle(0, 0, PinSize, PinSize);
                var dockedBounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

                g.FillRectangle(dockedFill, dockedBounds);
                g.FillRectangle(undockedFill, undockedBounds);
                g.DrawRectangle(dockedLine, dockedBounds);

                var path = new GraphicsPath();
                path.AddArc(5, 5, 21, 21, 0, 360);
                using (var pen = new Pen(Color.Red, 2))
                {
                    g.DrawPath(pen, path);
                }
            }

            base.OnPaint(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Location.X < PinSize)
                {
                    if (dockedState)
                    {
                        dockedState = false;
                        UnDockPanel();
                    }
                    else
                    {
                        dockedState = true;
                        DockPanel();
                    }
                }
            }
        }
    }
}