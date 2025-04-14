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

namespace ExtendedControls
{
    public class ToolStripCustomColourTable : ProfessionalColorTable
    {
        public ToolStripCustomColourTable()
        {
            this.UseSystemColors = false;
        }

        //----------------------------------------------------------------- MENUS

        // horz menu

        public Color colMenuBarBackground = Color.Yellow;       // horz menu strip background
        public override Color MenuStripGradientBegin { get { return colMenuBarBackground; } } // Left of menustrip render.. horizontal back
        public override Color MenuStripGradientEnd { get { return colMenuBarBackground; } } // Right of menu

        // NOT in table, additions, used by Renderer
        public Color colMenuText = Color.White;     // normal menu text
        public Color colMenuSelectedOrPressedText = Color.DeepSkyBlue;  // When hovering over


        public Color colMenuTopLevelHoveredBack = Color.Blue;      // menu top level selected highlight colour
        public override Color MenuItemSelectedGradientBegin { get { return colMenuTopLevelHoveredBack; } }
        public override Color MenuItemSelectedGradientEnd { get { return colMenuTopLevelHoveredBack; } }


        public Color colMenuTopLevelSelectedBack = Color.Green;
        public override Color MenuItemPressedGradientBegin { get { return colMenuTopLevelSelectedBack; } } // ToolStripMenuItem
        public override Color MenuItemPressedGradientEnd { get { return colMenuTopLevelSelectedBack; } } // ToolStripMenuItem
        ////Middle

        // dropdown menu

        public Color colMenuDropDownBackground = Color.Yellow;  // drop down menu colour of both menus and pop up menus
        public override Color ToolStripDropDownBackground { get { return colMenuDropDownBackground; } } // Background of menu or toolstrip dropdown

        public Color colMenuDropDownBorder = Color.Yellow; // All menu borders on Menu Strip
        public override Color MenuBorder { get { return colMenuDropDownBorder; } }


        public Color colMenuItemSelectedBack = Color.Pink;    // drop down background of selected item
        public override Color MenuItemSelected { get { return colMenuItemSelectedBack; } } // ToolStripMenuItem Background of hovered menu or toolstrip dropdown item

        public Color colMenuHighlightedPartBorder = Color.Yellow; // around the selector
        public override Color MenuItemBorder { get { return colMenuHighlightedPartBorder; } } // ToolStripMenuItem


        public Color colMenuLeftMarginBackground = Color.Yellow;      // the left margin colour on the drop down menu
        public override Color ImageMarginGradientBegin { get { return colMenuLeftMarginBackground; } }
        public override Color ImageMarginGradientEnd { get { return colMenuLeftMarginBackground; } }
        public override Color ImageMarginGradientMiddle { get { return colMenuLeftMarginBackground; } }

        
        public Color colMenuItemCheckedBackground = Color.Green;    // Colour of checkboxes background when checked
        public override Color CheckBackground { get { return colMenuItemCheckedBackground; } }      


        public Color colMenuItemHoverOverCheckmarkBackground = Color.Blue;     // Color of checkboxed background when hovered over
        public override Color CheckSelectedBackground { get { return colMenuItemHoverOverCheckmarkBackground; } }

        //------------------------------------------------------------------ ToolStrips


        public Color colToolStripBackground = Color.Lavender;   // background of a tool strip.  make sure you don't assign to back color (set it to control)
        public override Color ToolStripGradientBegin { get { return colToolStripBackground; } } // MS: Top of toolstrip; Mono: Top of toolstrip or active top-level menu item, Left of dropdown margin
        public override Color ToolStripGradientEnd { get { return colToolStripBackground; } } // MS: Bottom of toolstrip; Mono: Bottom of toolstrip or active top-level menu item, Right of dropdown margin
        public override Color ToolStripGradientMiddle { get { return colToolStripBackground; } } // MS: Middle of toolstrip; Mono: Unused


        public Color colToolStripBorder = Color.Pink;
        public override Color ToolStripBorder { get { return colToolStripBorder; } } // Toolstrip border at bottom of strip

        public Color colToolStripSeparator = Color.White;
        public override Color SeparatorDark { get { return colToolStripSeparator; } } // Left edge of vertical separator, colour of horizontal separator
        public override Color SeparatorLight { get { return colToolStripSeparator; } } // Right edge of vertical separator

        // buttons

        public Color colToolStripButtonSelectedBack = Color.Pink; // MS: Top of button or dropdown.. unchecked and hovered; Mono: Top of button or menu item, unchecked and hovered
        public override Color ButtonSelectedGradientBegin { get { return colToolStripButtonSelectedBack; } } 
        public override Color ButtonSelectedGradientEnd { get { return colToolStripButtonSelectedBack; } }
        public override Color ButtonSelectedGradientMiddle { get { return colToolStripButtonSelectedBack; } } // Unused

        
        public Color colToolStripButtonPressedBack = Color.Yellow;  // When pressed, applies to menu check boxes as well
        public override Color ButtonPressedGradientBegin { get { return colToolStripButtonPressedBack; } } // Top of button, when pressed
        public override Color ButtonPressedGradientEnd { get { return colToolStripButtonPressedBack; } } // Bottom of button
        public override Color ButtonPressedGradientMiddle { get { return colToolStripButtonPressedBack; } } // Unused


        public Color colToolStripButtonPressedSelectedBorder = Color.Yellow;    // border when pressed or selected
        public override Color ButtonSelectedBorder { get { return colToolStripButtonPressedSelectedBorder; } } // MS: Border of button, checked or hovered; Mono: Border of button, hovered
        public override Color ButtonPressedBorder { get { return colToolStripButtonPressedSelectedBorder; } } // MS: Unused; Mono: Border of button, checked


        public Color colToolStripOverflowButton = Color.White; // Overflow button, if the items are bigger than the toolstrip[
        public override Color OverflowButtonGradientBegin { get { return colToolStripOverflowButton; } } // Top of overflow button
        public override Color OverflowButtonGradientEnd { get { return colToolStripOverflowButton; } } // Bottom of overflow button
        public override Color OverflowButtonGradientMiddle { get { return colToolStripOverflowButton; } } // MS: Middle of overflow button; Mono: Unused


        public Color colToolBarGripper = Color.Yellow;    // gripper on the left of a toolbar
        public override Color GripDark { get { return colToolBarGripper; } } // Top-left dots of toolstrip grip
        public override Color GripLight { get { return colToolBarGripper; } } // Bottom-right dots of toolstrip grip
    }


    public class ThemeToolStripRenderer : ToolStripProfessionalRenderer
    {
        public ToolStripCustomColourTable colortable;

        public ThemeToolStripRenderer() : base(new ToolStripCustomColourTable())
        {
            this.colortable = (ToolStripCustomColourTable)ColorTable;
            this.RoundedEdges = true;
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)            // called to determine text colour..
        {
            if (e.Item.Selected || e.Item.Pressed)
                e.TextColor = colortable.colMenuSelectedOrPressedText;
            else
                e.TextColor = colortable.colMenuText;

            //System.Diagnostics.Debug.WriteLine($"Menu Item Text {e.Item} = {e.TextColor}");
            base.OnRenderItemText(e);
        }
    }

}
