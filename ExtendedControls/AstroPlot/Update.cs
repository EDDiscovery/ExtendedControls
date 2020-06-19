using ExtendedControls.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ExtendedControls.AstroPlot
{ 
    internal static class Update
    {
        internal static void MapSystems(List<ExtAstroPlot.MapObjects> mapSystems, int x, int y, double z, double[] cameraPosition, double azimuth, double elevation)
        {
            Matrix<double> _interaction = Interaction(azimuth, elevation, cameraPosition);
            Matrix<double> _data = Coords(x, y, z);
            Matrix<double> X_h = new Matrix<double>(4, 1);
                        
            for (int i = 0; i < mapSystems.Count; i++)
            {
                X_h.SetMatrix(new double[] { (double)mapSystems[i].X, (double)mapSystems[i].Y, (double)mapSystems[i].Z, 1.0 });
                Matrix<double> P = _data * _interaction * X_h;
                mapSystems[i].Coords = new PointF ((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
            }
        }

        internal static void MapAxes(List<ExtAstroPlot.Axis> widgets, int x, int y, double z, double[] cameraPosition, double azimuth, double elevation)
        {
            Matrix<double> _interaction = Interaction(azimuth, elevation, cameraPosition);
            Matrix<double> _data = Coords(x, y, z);
            Matrix<double> X_h = new Matrix<double>(4, 1);

            for (int i = 0; i < widgets.Count; i++)
            {
                X_h.SetMatrix(new double[] { (double)widgets[i].X, (double)widgets[i].Y, (double)widgets[i].Z, 1.0 });
                Matrix<double> P = _data * _interaction * X_h;
                widgets[i].Coords = new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
            }
        }

        internal static void MapFrames(List<ExtAstroPlot.Corner> frames, int x, int y, double z, double[] cameraPosition, double azimuth, double elevation)
        {
            Matrix<double> _interaction = Interaction(azimuth, elevation, cameraPosition);
            Matrix<double> _data = Coords(x, y, z);
            Matrix<double> X_h = new Matrix<double>(4, 1);

            for (int i = 0; i < frames.Count; i++)
            {
                X_h.SetMatrix(new double[] { (double)frames[i].X, (double)frames[i].Y, (double)frames[i].Z, 1.0 });
                Matrix<double> P = _data * _interaction * X_h;
                frames[i].Coords = new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
            }
        }

        internal static Matrix<double> Coords(double x, double y, double z)
        {
            Matrix<double> _matrix = new Matrix<double>(3, 3);
            double _x = x / 2;
            double _y = y / 2;
            const double _a = 1;
            _matrix.SetMatrix(new double[] { z, 0, _x, 0, z * _a, _y, 0, 0, 1 });
            return _matrix;
        }

        internal static Matrix<double> Interaction(double azimuth, double elevation, double[] camera)
        {
            Matrix<double> R = Rotate(azimuth, elevation);
            Matrix<double> lens = new Matrix<double>(3, 1);
            lens.SetMatrix(camera);
            return R | (-R * lens);
        }

        internal static Matrix<double> Rotate(double azimuth, double elevation)
        {
            Matrix<double> R = new Matrix<double>(3, 3);
            R.SetMatrix(new double[] { Math.Cos(azimuth), 0, -Math.Sin(azimuth),
                                       Math.Sin(azimuth)*Math.Sin(elevation),  Math.Cos(elevation), Math.Cos(azimuth)*Math.Sin(elevation),
                                       Math.Cos(elevation)*Math.Sin(azimuth), -Math.Sin(elevation), Math.Cos(azimuth)*Math.Cos(elevation) });
            return R;
        }

        internal static double FindOrbitalElevation(double distance, double inclination)
        {
            return (double)((inclination / 90) * distance);
        }

        internal static double FindOrbitalRadius(double distance, double inclination)
        {
            var angle = (inclination / 90) * distance;
            return (double)Math.Sqrt((distance * distance) - (angle * angle));
        }
    }
}
