﻿/*
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

using System.Drawing;

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

        public void SetAxesCoordinates(int length)
        {
            if (ShowAxesWidget)
            {
                Axes.Add(new Axis
                {
                    X = 0,
                    Y = 0,
                    Z = 0
                });
                Axes.Add(new Axis
                {
                    X = length,
                    Y = 0,
                    Z = 0
                });
                Axes.Add(new Axis
                {
                    X = 0,
                    Y = length * -1,
                    Z = 0
                });
                Axes.Add(new Axis
                {
                    X = 0,
                    Y = 0,
                    Z = length * -1
                });

                UpdateProjection();
            }
        }
    }
}