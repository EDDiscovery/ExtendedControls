/*
 * Copyright © 2016 - 2020 EDDiscovery development team
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

using ExtendedControls.AstroPlot;
using System;
using System.Diagnostics;
using System.Drawing;

namespace ExtendedControls.Controls
{
    public partial class AstroPlot
    {
        /// <summary>
        /// Set the axes widget anchors point
        /// </summary>
        /// <param name="length"></param>
        internal void SetAxesAnchors(int length)
        {
            if (ShowAxesWidget)
            {
                if (centerCoordinates != null)
                {
                    ShowAxesWidget = false;
                    axesAnchors.Clear();

                    axesAnchors.Add(new AnchorPoint
                    {
                        X = GetCenterCoordinates()[0],
                        Y = GetCenterCoordinates()[1],
                        Z = GetCenterCoordinates()[2]
                    });
                    axesAnchors.Add(new AnchorPoint
                    {
                        X = GetCenterCoordinates()[0] + length,
                        Y = GetCenterCoordinates()[1],
                        Z = GetCenterCoordinates()[2]
                    });
                    axesAnchors.Add(new AnchorPoint
                    {
                        X = GetCenterCoordinates()[0],
                        Y = GetCenterCoordinates()[1] + (length * -1),
                        Z = GetCenterCoordinates()[2]
                    });
                    axesAnchors.Add(new AnchorPoint
                    {
                        X = GetCenterCoordinates()[0],
                        Y = GetCenterCoordinates()[1],
                        Z = GetCenterCoordinates()[2] + (length * -1)
                    });

                    ShowAxesWidget = true;
                }
            }
            else
            {
                return;
            }
        }
               
        /// <summary>
        /// Set the frame boundaries anchors points
        /// </summary>
        /// <param name="frameRadius"></param>
        public void SetFrameAnchors(double frameRadius)
        {
            if (ShowFrameWidget)
            {
                ShowFrameWidget = false;

                framesAnchors.Clear();
                framesAnchors.Add(new AnchorPoint { X = GetCenterCoordinates()[0] + frameRadius, Y = GetCenterCoordinates()[1] + frameRadius, Z = GetCenterCoordinates()[2] + frameRadius });
                framesAnchors.Add(new AnchorPoint { X = GetCenterCoordinates()[0] + frameRadius, Y = GetCenterCoordinates()[1] + frameRadius, Z = GetCenterCoordinates()[2] + -frameRadius });
                framesAnchors.Add(new AnchorPoint { X = GetCenterCoordinates()[0] + frameRadius, Y = GetCenterCoordinates()[1] + -frameRadius, Z = GetCenterCoordinates()[2] + frameRadius });
                framesAnchors.Add(new AnchorPoint { X = GetCenterCoordinates()[0] + -frameRadius, Y = GetCenterCoordinates()[1] + frameRadius, Z = GetCenterCoordinates()[2] + frameRadius });
                framesAnchors.Add(new AnchorPoint { X = GetCenterCoordinates()[0] + -frameRadius, Y = GetCenterCoordinates()[1] + -frameRadius, Z = GetCenterCoordinates()[2] + frameRadius });
                framesAnchors.Add(new AnchorPoint { X = GetCenterCoordinates()[0] + -frameRadius, Y = GetCenterCoordinates()[1] + frameRadius, Z = GetCenterCoordinates()[2] + -frameRadius });
                framesAnchors.Add(new AnchorPoint { X = GetCenterCoordinates()[0] + frameRadius, Y = GetCenterCoordinates()[1] + -frameRadius, Z = GetCenterCoordinates()[2] + -frameRadius });
                framesAnchors.Add(new AnchorPoint { X = GetCenterCoordinates()[0] + -frameRadius, Y = GetCenterCoordinates()[1] + -frameRadius, Z = GetCenterCoordinates()[2] + -frameRadius });

                planesAnchors.Clear();
                planesAnchors.Add(new AnchorPoint { X = GetCenterCoordinates()[0] + (frameRadius * 2), Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] });
                planesAnchors.Add(new AnchorPoint { X = GetCenterCoordinates()[0] - (frameRadius * 2), Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] });
                planesAnchors.Add(new AnchorPoint { X = GetCenterCoordinates()[0], Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] + (frameRadius * 2) });
                planesAnchors.Add(new AnchorPoint { X = GetCenterCoordinates()[0], Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] - (frameRadius * 2) });
                planesAnchors.Add(new AnchorPoint { X = GetCenterCoordinates()[0], Y = GetCenterCoordinates()[1] + (frameRadius * 2), Z = GetCenterCoordinates()[2] });
                planesAnchors.Add(new AnchorPoint { X = GetCenterCoordinates()[0], Y = GetCenterCoordinates()[1] - (frameRadius * 2), Z = GetCenterCoordinates()[2] });

                ShowFrameWidget = true;
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Set grid anchors points
        /// </summary>
        /// <param name="gc"></param>
        /// <param name="gu"></param>
        public void SetGridAnchors(int gc, int gu)
        {
            ShowGridWidget = false;

            gridsAnchors.Clear();

            var gcd = (int)gc * 2;
            var go = (int)gc * gu;

            for (int i = 0; i < gcd; i++)
            {
                gridsAnchors.Add(new AnchorPoint[]
                {
                    new AnchorPoint { X = GetCenterCoordinates()[0] + gcd - go, Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] + (gu * i) - go },
                    new AnchorPoint { X = GetCenterCoordinates()[0] + (gu * gcd) - go, Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] + (gu * i) - go}
                });
                gridsAnchors.Add(new AnchorPoint[]
                {
                    new AnchorPoint { X = GetCenterCoordinates()[0] + (gu * i) - go, Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] + (gu * gcd) - go},
                    new AnchorPoint { X = GetCenterCoordinates()[0] + (gu * i) - go, Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] + gcd - go}
                });
            }

            ShowGridWidget = true;
        }
    }
}