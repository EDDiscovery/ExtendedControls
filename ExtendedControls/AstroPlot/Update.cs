using BaseUtils;
using ExtendedControls.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace ExtendedControls.Controls
{
    public partial class AstroPlot
    {
        new internal static class Update
        {
            internal static void PlotObjects(
                List<PlotObjects> mapSystems,
                int x,
                int y,
                double z,
                double[] cameraPosition,
                double azimuth,
                double elevation,
                double[] centerCoords)
            {
                var _interaction = Interaction(azimuth, elevation, cameraPosition);
                var _data = Coords(x, y, z);
                var X_h = new Tranform<double>(4, 1);

                foreach (var item in mapSystems)
                {
                    if (centerCoords != null)
                    {
                        X_h.SetMatrix(new double[] { item.X - centerCoords[0], item.Y - centerCoords[1], item.Z - centerCoords[2], 1.0 });
                    }
                    else
                    {
                        X_h.SetMatrix(new double[] { item.X, item.Y, item.Z, 1.0 });
                    }

                    var P = _data * _interaction * X_h;
                    item.Coords = new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
                }
            }

            internal static void AxesWidget(
                List<Axis> widgets,
                int x,
                int y,
                double z,
                double[] cameraPosition,
                double azimuth,
                double elevation,
                double[] centerCoords)
            {
                var _interaction = Interaction(azimuth, elevation, cameraPosition);
                var _data = Coords(x, y, z);
                var X_h = new Tranform<double>(4, 1);

                foreach (var anchor in widgets)
                {
                    X_h.SetMatrix(new double[] { anchor.X - centerCoords[0], anchor.Y - centerCoords[1], anchor.Z - centerCoords[2], 1.0 });
                    var P = _data * _interaction * X_h;
                    anchor.Coords = new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
                }
            }

            internal static void FrameWidget(
                List<Corner> frames,
                int x,
                int y,
                double z,
                double[] cameraPosition,
                double azimuth,
                double elevation,
                double[] centerCoords)
            {
                var _interaction = Interaction(azimuth, elevation, cameraPosition);
                var _data = Coords(x, y, z);
                var X_h = new Tranform<double>(4, 1);

                foreach (var corner in frames)
                {
                    X_h.SetMatrix(new double[] { corner.X - centerCoords[0], corner.Y - centerCoords[1], corner.Z - centerCoords[2], 1.0 });
                    var P = _data * _interaction * X_h;
                    corner.Coords = new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
                }
            }

            internal static void GridWidget(
                List<Corner> grid,
                int x,
                int y,
                double z,
                double[] cameraPosition,
                double azimuth,
                double elevation,
                double[] centerCoords)
            {
                var _interaction = Interaction(azimuth, elevation, cameraPosition);
                var _data = Coords(x, y, z);
                var X_h = new Tranform<double>(4, 1);

                foreach (var corner in grid)
                {
                    X_h.SetMatrix(new double[] { corner.X - centerCoords[0], corner.Y - centerCoords[1], corner.Z - centerCoords[2], 1.0 });
                    var P = _data * _interaction * X_h;
                    corner.Coords = new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
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
