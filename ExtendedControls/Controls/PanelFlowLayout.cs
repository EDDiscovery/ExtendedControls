/*
 * Copyright 2023-2023 EDDiscovery development team
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
    /// <summary>
    /// This automatically sizes the parent container to the flow container.
    /// set the flow container to autosize and dock top.  The parent container will be set to the size needed by this
    /// supports only one flow layout panel in a container such as a group box or a panel
    /// Margin.Bottom allows for extra space under if required
    /// </summary>
    public class ExtFlowLayoutPanel : FlowLayoutPanel
    {
        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            if (  Dock == DockStyle.Top )
            {
                //System.Diagnostics.Debug.WriteLine($"Parent {Parent.Name} {Parent.Bounds} cr {Parent.ClientRectangle} {Parent.ClientSize}");
                //System.Diagnostics.Debug.WriteLine($"this {Bounds} cr {ClientRectangle} {ClientSize}");
                
                // controls under group boxes seem to have client areas starting above 0. Add it on
                Parent.ClientSize = new Size(Parent.Width, Bounds.Y + Bounds.Height + Margin.Bottom);
            }
        }
    }
}
