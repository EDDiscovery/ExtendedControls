using BaseUtils;
using ExtendedControls.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;

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
                double elevation)
            {
                var _interaction = Interaction(azimuth, elevation, cameraPosition);
                var _data = Coords(x, y, z);
                var X_h = new Matrices<double>(4, 1);

                for (int i = 0; i < mapSystems.Count; i++)
                {
                    X_h.SetMatrix(new double[] { mapSystems[i].X, mapSystems[i].Y, mapSystems[i].Z, 1.0 });
                    var P = _data * _interaction * X_h;
                    mapSystems[i].Coords = new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
                }
            }

            internal static void AxesWidget(
                List<Axis> widgets,
                int x,
                int y,
                double z,
                double[] cameraPosition,
                double azimuth,
                double elevation)
            {
                var _interaction = Interaction(azimuth, elevation, cameraPosition);
                var _data = Coords(x, y, z);
                var X_h = new Matrices<double>(4, 1);

                for (int i = 0; i < widgets.Count; i++)
                {
                    X_h.SetMatrix(new double[] { widgets[i].X, widgets[i].Y, widgets[i].Z, 1.0 });
                    var P = _data * _interaction * X_h;
                    widgets[i].Coords = new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
                }
            }

            internal static void FrameWidget(
                List<Corner> frames,
                int x,
                int y,
                double z,
                double[] cameraPosition,
                double azimuth,
                double elevation)
            {
                var _interaction = Interaction(azimuth, elevation, cameraPosition);
                var _data = Coords(x, y, z);
                var X_h = new Matrices<double>(4, 1);

                for (int i = 0; i < frames.Count; i++)
                {
                    X_h.SetMatrix(new double[] { frames[i].X, frames[i].Y, frames[i].Z, 1.0 });
                    var P = _data * _interaction * X_h;
                    frames[i].Coords = new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
                }
            }

            internal static Matrices<double> Coords(double x, double y, double z)
            {
                var _matrix = new Matrices<double>(3, 3);
                var _x = x / 2;
                var _y = y / 2;
                const double _a = 1;
                _matrix.SetMatrix(new double[] { z, 0, _x, 0, z * _a, _y, 0, 0, 1 });
                return _matrix;
            }

            internal static Matrices<double> Interaction(double azimuth, double elevation, double[] camera)
            {
                var R = Rotate(azimuth, elevation);
                var lens = new Matrices<double>(3, 1);
                lens.SetMatrix(camera);
                return R | -R * lens;
            }

            internal static Matrices<double> Rotate(double azimuth, double elevation)
            {
                var R = new Matrices<double>(3, 3);
                R.SetMatrix(new double[] { Math.Cos(azimuth), 0, -Math.Sin(azimuth),
                                       Math.Sin(azimuth)*Math.Sin(elevation),  Math.Cos(elevation), Math.Cos(azimuth)*Math.Sin(elevation),
                                       Math.Cos(elevation)*Math.Sin(azimuth), -Math.Sin(elevation), Math.Cos(azimuth)*Math.Cos(elevation) });
                return R;
            }
        }
    }
}
