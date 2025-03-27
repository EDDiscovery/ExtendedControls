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

using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ExtendedControls
{
    public partial class ExtPanelResizer : Panel, IThemeable
    {
        public DockStyle Movement { get { return movement; } set { SetMovement(value); } }

        private DockStyle movement = DockStyle.Top;

        private void SetMovement( DockStyle m)
        {
            if (Movement == DockStyle.Top || Movement == DockStyle.Bottom)
                Cursor = System.Windows.Forms.Cursors.SizeNS;
            else
                Cursor = System.Windows.Forms.Cursors.SizeWE;

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

        public bool Theme(Theme t, Font fnt)
        {
            BackColor = t.GroupBoxOverride(Parent, t.Form);
            Visible = !t.WindowsFrame;
            return true;
        }
    }
}
