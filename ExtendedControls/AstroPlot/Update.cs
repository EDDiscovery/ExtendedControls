using BaseUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace ExtendedControls.Controls
{
    public partial class AstroPlot
    {
        new internal static class Update
        {
            internal static void Projection(List<AnchorPoint> corners, int x, int y, double z, double[] cameraPosition, double azimuth, double elevation, double[] centerCoordinates)
            {
                var _interaction = Interaction(azimuth, elevation, cameraPosition);
                var _data = Coords(x, y, z);
                var X_h = new Tranform<double>(4, 1);

                foreach (var corner in corners)
                {
                    X_h.SetMatrix(new double[] { corner.X - centerCoordinates[0], corner.Y - centerCoordinates[1], corner.Z - centerCoordinates[2], 1.0 });
                    var P = _data * _interaction * X_h;
                    corner.Coords = new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
                }
            }

            internal static void Projection(List<PlotObjects> mapObjects, int x, int y, double z, double[] cameraPosition, double azimuth, double elevation, double[] centerCoordinates)
            {
                var _interaction = Interaction(azimuth, elevation, cameraPosition);
                var _data = Coords(x, y, z);
                var X_h = new Tranform<double>(4, 1);

                foreach (var item in mapObjects)
                {
                    if (centerCoordinates != null)
                    {
                        X_h.SetMatrix(new double[] { item.X - centerCoordinates[0], item.Y - centerCoordinates[1], item.Z - centerCoordinates[2], 1.0 });
                    }
                    else
                    {
                        X_h.SetMatrix(new double[] { item.X, item.Y, item.Z, 1.0 });
                    }

                    var P = _data * _interaction * X_h;
                    item.Coords = new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
                }
            }

            internal static Tranform<double> Coords(double x, double y, double z)
            {
                var _matrix = new Tranform<double>(3, 3);
                var _x = x / 2;
                var _y = y / 2;
                const double _a = 1;
                _matrix.SetMatrix(new double[] { z, 0, _x, 0, z * _a, _y, 0, 0, 1 });
                return _matrix;
            }

            internal static Tranform<double> Interaction(double azimuth, double elevation, double[] camera)
            {
                var R = Rotate(azimuth, elevation);
                var lens = new Tranform<double>(3, 1);
                lens.SetMatrix(camera);
                return R | -R * lens;
            }

            internal static Tranform<double> Rotate(double azimuth, double elevation)
            {
                var R = new Tranform<double>(3, 3);
                R.SetMatrix(new double[] { Math.Cos(azimuth), 0, -Math.Sin(azimuth),
                                       Math.Sin(azimuth)*Math.Sin(elevation),  Math.Cos(elevation), Math.Cos(azimuth)*Math.Sin(elevation),
                                       Math.Cos(elevation)*Math.Sin(azimuth), -Math.Sin(elevation), Math.Cos(azimuth)*Math.Cos(elevation) });
                return R;
            }
        }
    }
}
