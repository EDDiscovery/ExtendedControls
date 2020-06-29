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
using System.Diagnostics;
using System.Drawing;

namespace ExtendedControls.Controls
{
    public partial class AstroPlot
    {
        internal void SetAxesAnchors(int length)
        {
            if (ShowAxesWidget)
            {
                if (centerCoordinates != null)
                {
                    ShowAxesWidget = false;
                    _axes.Clear();

                    _axes.Add(new AnchorPoint
                    {
                        X = GetCenterCoordinates()[0],
                        Y = GetCenterCoordinates()[1],
                        Z = GetCenterCoordinates()[2]
                    });
                    _axes.Add(new AnchorPoint
                    {
                        X = GetCenterCoordinates()[0] + length,
                        Y = GetCenterCoordinates()[1],
                        Z = GetCenterCoordinates()[2]
                    });
                    _axes.Add(new AnchorPoint
                    {
                        X = GetCenterCoordinates()[0],
                        Y = GetCenterCoordinates()[1] + (length * -1),
                        Z = GetCenterCoordinates()[2]
                    });
                    _axes.Add(new AnchorPoint
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
               
        public void SetFrameAnchors(double frameRadius)
        {
            if (ShowFrameWidget)
            {
                ShowFrameWidget = false;

                _frames.Clear();
                _frames.Add(new AnchorPoint { X = GetCenterCoordinates()[0] + frameRadius, Y = GetCenterCoordinates()[1] + frameRadius, Z = GetCenterCoordinates()[2] + frameRadius });
                _frames.Add(new AnchorPoint { X = GetCenterCoordinates()[0] + frameRadius, Y = GetCenterCoordinates()[1] + frameRadius, Z = GetCenterCoordinates()[2] + -frameRadius });
                _frames.Add(new AnchorPoint { X = GetCenterCoordinates()[0] + frameRadius, Y = GetCenterCoordinates()[1] + -frameRadius, Z = GetCenterCoordinates()[2] + frameRadius });
                _frames.Add(new AnchorPoint { X = GetCenterCoordinates()[0] + -frameRadius, Y = GetCenterCoordinates()[1] + frameRadius, Z = GetCenterCoordinates()[2] + frameRadius });
                _frames.Add(new AnchorPoint { X = GetCenterCoordinates()[0] + -frameRadius, Y = GetCenterCoordinates()[1] + -frameRadius, Z = GetCenterCoordinates()[2] + frameRadius });
                _frames.Add(new AnchorPoint { X = GetCenterCoordinates()[0] + -frameRadius, Y = GetCenterCoordinates()[1] + frameRadius, Z = GetCenterCoordinates()[2] + -frameRadius });
                _frames.Add(new AnchorPoint { X = GetCenterCoordinates()[0] + frameRadius, Y = GetCenterCoordinates()[1] + -frameRadius, Z = GetCenterCoordinates()[2] + -frameRadius });
                _frames.Add(new AnchorPoint { X = GetCenterCoordinates()[0] + -frameRadius, Y = GetCenterCoordinates()[1] + -frameRadius, Z = GetCenterCoordinates()[2] + -frameRadius });

                _planes.Clear();
                _planes.Add(new AnchorPoint { X = GetCenterCoordinates()[0] + (frameRadius * 2), Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] });
                _planes.Add(new AnchorPoint { X = GetCenterCoordinates()[0] - (frameRadius * 2), Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] });
                _planes.Add(new AnchorPoint { X = GetCenterCoordinates()[0], Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] + (frameRadius * 2) });
                _planes.Add(new AnchorPoint { X = GetCenterCoordinates()[0], Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] - (frameRadius * 2) });
                _planes.Add(new AnchorPoint { X = GetCenterCoordinates()[0], Y = GetCenterCoordinates()[1] + (frameRadius * 2), Z = GetCenterCoordinates()[2] });
                _planes.Add(new AnchorPoint { X = GetCenterCoordinates()[0], Y = GetCenterCoordinates()[1] - (frameRadius * 2), Z = GetCenterCoordinates()[2] });

                ShowFrameWidget = true;
            }
            else
            {
                return;
            }
        }

        public void SetGridAnchors(int gc, int gu)
        {
            var gcd = (int)gc * 2;
            var go = (int)gc * gu;

            ShowGridWidget = false;

            _grids.Clear();

            for (int i = 0; i < gcd; i++)
            {
                _grids.Add(new AnchorPoint[]
                {
                    new AnchorPoint { X = GetCenterCoordinates()[0] + gcd - go, Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] + (gu * i) - go },
                    new AnchorPoint { X = GetCenterCoordinates()[0] + (gu * gcd) - go, Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] + (gu * i) - go}
                });
                _grids.Add(new AnchorPoint[]
                {
                    new AnchorPoint { X = GetCenterCoordinates()[0] + (gu * i) - go, Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] + (gu * gcd) - go},
                    new AnchorPoint { X = GetCenterCoordinates()[0] + (gu * i) - go, Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] + gcd - go}
                });
            }

            ShowGridWidget = true;
        }
    }
}