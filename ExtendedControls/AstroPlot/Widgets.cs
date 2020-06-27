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

using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExtendedControls.Controls
{
    public partial class AstroPlot
    {
        internal class Axis
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
            public PointF Coords { get; set; } = new PointF(0, 0);
        }

        internal void SetAxesCoordinates(int length)
        {
            if (ShowAxesWidget)
            {
                if (centerCoordinates != null)
                {
                    ShowAxesWidget = false;
                    Axes.Clear();

                    Axes.Add(new Axis
                    {
                        X = GetCenterCoordinates()[0],
                        Y = GetCenterCoordinates()[1],
                        Z = GetCenterCoordinates()[2]
                    });
                    Axes.Add(new Axis
                    {
                        X = GetCenterCoordinates()[0] + length,
                        Y = GetCenterCoordinates()[1],
                        Z = GetCenterCoordinates()[2]
                    });
                    Axes.Add(new Axis
                    {
                        X = GetCenterCoordinates()[0],
                        Y = GetCenterCoordinates()[1] + (length * -1),
                        Z = GetCenterCoordinates()[2]
                    });
                    Axes.Add(new Axis
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

        internal class Corner
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
            public PointF Coords { get; set; } = new PointF(0, 0);
        }

        public void SetFrameCoordinates(double frameRadius)
        {
            if (ShowFrameWidget)
            {
                ShowFrameWidget = false;

                Frames.Clear();
                Frames.Add(new Corner { X = GetCenterCoordinates()[0] + frameRadius, Y = GetCenterCoordinates()[1] + frameRadius, Z = GetCenterCoordinates()[2] + frameRadius });
                Frames.Add(new Corner { X = GetCenterCoordinates()[0] + frameRadius, Y = GetCenterCoordinates()[1] + frameRadius, Z = GetCenterCoordinates()[2] + -frameRadius });
                Frames.Add(new Corner { X = GetCenterCoordinates()[0] + frameRadius, Y = GetCenterCoordinates()[1] + -frameRadius, Z = GetCenterCoordinates()[2] + frameRadius });
                Frames.Add(new Corner { X = GetCenterCoordinates()[0] + -frameRadius, Y = GetCenterCoordinates()[1] + frameRadius, Z = GetCenterCoordinates()[2] + frameRadius });
                Frames.Add(new Corner { X = GetCenterCoordinates()[0] + -frameRadius, Y = GetCenterCoordinates()[1] + -frameRadius, Z = GetCenterCoordinates()[2] + frameRadius });
                Frames.Add(new Corner { X = GetCenterCoordinates()[0] + -frameRadius, Y = GetCenterCoordinates()[1] + frameRadius, Z = GetCenterCoordinates()[2] + -frameRadius });
                Frames.Add(new Corner { X = GetCenterCoordinates()[0] + frameRadius, Y = GetCenterCoordinates()[1] + -frameRadius, Z = GetCenterCoordinates()[2] + -frameRadius });
                Frames.Add(new Corner { X = GetCenterCoordinates()[0] + -frameRadius, Y = GetCenterCoordinates()[1] + -frameRadius, Z = GetCenterCoordinates()[2] + -frameRadius });

                Planes.Clear();
                Planes.Add(new Corner { X = GetCenterCoordinates()[0] + (frameRadius * 2), Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] });
                Planes.Add(new Corner { X = GetCenterCoordinates()[0] - (frameRadius * 2), Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] });
                Planes.Add(new Corner { X = GetCenterCoordinates()[0], Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] + (frameRadius * 2) });
                Planes.Add(new Corner { X = GetCenterCoordinates()[0], Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] - (frameRadius * 2) });
                Planes.Add(new Corner { X = GetCenterCoordinates()[0], Y = GetCenterCoordinates()[1] + (frameRadius * 2), Z = GetCenterCoordinates()[2] });
                Planes.Add(new Corner { X = GetCenterCoordinates()[0], Y = GetCenterCoordinates()[1] - (frameRadius * 2), Z = GetCenterCoordinates()[2] });

                ShowFrameWidget = true;
            }
            else
            {
                return;
            }
        }

        public void SetGridCoordinates(int radius)
        {
            if (ShowGridWidget)
            {
                var gridRadius = radius;

                ShowGridWidget = false;
                Grids.Clear();

                //Grids.Add(new Corner { X = GetCenterCoordinates()[0] + gridRadius, Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] + gridRadius });
                //Grids.Add(new Corner { X = GetCenterCoordinates()[0] + gridRadius, Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] - gridRadius });
                Grids.Add(new Corner { X = GetCenterCoordinates()[0] - gridRadius, Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] + gridRadius });
                Grids.Add(new Corner { X = GetCenterCoordinates()[0] - gridRadius, Y = GetCenterCoordinates()[1], Z = GetCenterCoordinates()[2] - gridRadius });
                ShowGridWidget = true;
            }
        }
    }
}