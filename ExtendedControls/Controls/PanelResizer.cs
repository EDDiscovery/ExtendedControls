/*
 * Copyright 2024-2025 EDDiscovery development team
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

using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{
    // a resizer panel, clicking on it causes a resize.
    public partial class ExtPanelResizer : ExtPanelGradientFill
    {
        public DockStyle Movement { get { return movement; } set { SetMovement(value); } }
        private DockStyle movement = DockStyle.Top;

        private void SetMovement(DockStyle m)
        {
            if (Movement == DockStyle.Top || Movement == DockStyle.Bottom)
                Cursor = Cursors.SizeNS;
            else
                Cursor = Cursors.SizeWE;

            movement = m;
        }

        public ExtPanelResizer()
        {
            SetMovement(DockStyle.Top);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Form f = FindForm();
            if (f != null && f is DraggableForm)
            {
                ((DraggableForm)f).PerformResizeMouseDown(this, e, Movement);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Form f = FindForm();
            if (f != null && f is DraggableForm)
            {
                ((DraggableForm)f).PerformResizeMouseUp(this, e, Movement);
            }
        }

        protected override bool ThemeDerived(Theme t, Font fnt)
        {
            Visible = !t.WindowsFrame;
            return base.ThemeDerived(t, fnt);
        }
    }

}
