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

using BaseUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExtendedControls.Controls.AstroPlot;

namespace ExtendedControls.AstroPlot
{
    /// <summary>
    /// Update coordinates
    /// </summary>
    internal static class View
    {
        /// <summary>
        /// Used to update coordinates of axes and frames
        /// </summary>
        /// <param name="anchors"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="cameraPosition"></param>
        /// <param name="azimuth"></param>
        /// <param name="elevation"></param>
        /// <param name="centerCoordinates"></param>
        internal static void Update(List<AnchorPoint> anchors, int x, int y, double z, double[] cameraPosition, double azimuth, double elevation, double[] centerCoordinates)
        {
            var interaction = Interaction(azimuth, elevation, cameraPosition);
            var data = Coords(x, y, z);
            var X_h = new Matrix<double>(4, 1);

            foreach (var anchor in anchors)
            {
                X_h.SetMatrix(new double[] { anchor.X - centerCoordinates[0], anchor.Y - centerCoordinates[1], anchor.Z - centerCoordinates[2], 1.0 });
                var P = data * interaction * X_h;
                anchor.Coords = new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
            }
        }

        /// <summary>
        /// Used to update grids corodinates, which are created as a list of array
        /// </summary>
        /// <param name="anchors"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="cameraPosition"></param>
        /// <param name="azimuth"></param>
        /// <param name="elevation"></param>
        /// <param name="centerCoordinates"></param>
        internal static void Update(List<AnchorPoint[]> anchors, int x, int y, double z, double[] cameraPosition, double azimuth, double elevation, double[] centerCoordinates)
        {
            var interaction = Interaction(azimuth, elevation, cameraPosition);
            var data = Coords(x, y, z);
            var X_h = new Matrix<double>(4, 1);

            foreach (var anchor in anchors)
            {
                for (int i = 0; i < anchor.Length; i++)
                {
                    X_h.SetMatrix(new double[] { anchor[i].X - centerCoordinates[0], anchor[i].Y - centerCoordinates[1], anchor[i].Z - centerCoordinates[2], 1.0 });
                    var P = data * interaction * X_h;
                    anchor[i].Coords = new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
                }
            }
        }

        /// <summary>
        /// Update plot objects coordinates and hotspot list
        /// </summary>
        /// <param name="mapObjects"></param>
        /// <param name="plotHotSpot"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="cameraPosition"></param>
        /// <param name="azimuth"></param>
        /// <param name="elevation"></param>
        /// <param name="centerCoordinates"></param>
        internal static void Update(List<PlotObject> mapObjects, List<Tuple<Object,PointF>> plotHotSpot, int x, int y, double z, double[] cameraPosition, double azimuth, double elevation, double[] centerCoordinates)
        {
            plotHotSpot.Clear();

            var interaction = Interaction(azimuth, elevation, cameraPosition);
            var data = Coords(x, y, z);
            var X_h = new Matrix<double>(4, 1);

            for (int i = 0; i < mapObjects.Count; i++)
            {
                if (centerCoordinates != null)
                {
                    X_h.SetMatrix(new double[] { mapObjects[i].X - centerCoordinates[0], mapObjects[i].Y - centerCoordinates[1], mapObjects[i].Z - centerCoordinates[2], 1.0 });
                }
                else
                {
                    X_h.SetMatrix(new double[] { mapObjects[i].X, mapObjects[i].Y, mapObjects[i].Z, 1.0 });
                }

                var P = data * interaction * X_h;
                mapObjects[i].Coords = new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
                plotHotSpot.Add(new Tuple<object, PointF>(mapObjects[i].Name, new PointF(mapObjects[i].Coords.X, mapObjects[i].Coords.Y)));
            }
        }

        /// <summary>
        /// Create a matrix with plot object coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        internal static Matrix<double> Coords(double x, double y, double z)
        {
            var matrix = new Matrix<double>(3, 3);
            var nx = x / 2;
            var ny = y / 2;
            const double _a = 1;
            matrix.SetMatrix(new double[] { z, 0, nx, 0, z * _a, ny, 0, 0, 1 });
            return matrix;
        }

        /// <summary>
        /// Create a matrix with interaction coordinates
        /// </summary>
        /// <param name="azimuth"></param>
        /// <param name="elevation"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        internal static Matrix<double> Interaction(double azimuth, double elevation, double[] camera)
        {
            var R = Rotate(azimuth, elevation);
            var lens = new Matrix<double>(3, 1);
            lens.SetMatrix(camera);
            return R | -R * lens;
        }

        /// <summary>
        /// Matrix rotation parameters
        /// </summary>
        /// <param name="azimuth"></param>
        /// <param name="elevation"></param>
        /// <returns></returns>
        internal static Matrix<double> Rotate(double azimuth, double elevation)
        {
            var R = new Matrix<double>(3, 3);
            R.SetMatrix(new double[] { Math.Cos(azimuth), 0, -Math.Sin(azimuth),
                                       Math.Sin(azimuth)*Math.Sin(elevation),  Math.Cos(elevation), Math.Cos(azimuth)*Math.Sin(elevation),
                                       Math.Cos(elevation)*Math.Sin(azimuth), -Math.Sin(elevation), Math.Cos(azimuth)*Math.Cos(elevation) });
            return R;
        }
    }
}
