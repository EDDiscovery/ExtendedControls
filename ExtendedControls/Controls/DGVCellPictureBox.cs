/*
 * Copyright © 2024-2024 EDDiscovery development team
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

using BaseUtils;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls
{

    // ExtPictureBox inside a DGV Cell
    public class DataGridViewPictureBoxCell : DataGridViewExtImageCellBase
    {
        // add content then render
        // render with resizecontrol=true so it sets the picturebox size to the contents, or with false and set PictureBox size manually
        public ExtPictureBox PictureBox { get; private set; }

        public ContentAlignment Alignment { get; set; } = ContentAlignment.MiddleCenter;

        public DataGridViewPictureBoxCell()
        {
            this.ValueType = typeof(string);
            PictureBox = new ExtPictureBox();
        }

        // clone our values as well, as recommended by https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.datagridviewimagecell?view=windowsdesktop-8.0        
        public override object Clone()
        {
            var n = base.Clone() as DataGridViewPictureBoxCell;
            n.Alignment = this.Alignment;
            return n;
        }

        // override and tell it the PictureBox size.
        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            return PictureBox.Image != null ? PictureBox.Size : new Size(1,1);      // only if rendered, return size
        }

        protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex,
                        DataGridViewElementStates cellState, object value, object formattedValue, string errorText,
                        DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            // Draws the cell grid with an empty image
            base.Paint(g, clipBounds, cellBounds,
                rowIndex, cellState, value, formattedValue, errorText,
                cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));

            if (PictureBox.Image != null)
            {
                Rectangle p = Alignment.ImagePositionFromContentAlignment(cellBounds, PictureBox.Image.Size);
                g.DrawImage(PictureBox.Image, p);
            }
        }
    }

}
