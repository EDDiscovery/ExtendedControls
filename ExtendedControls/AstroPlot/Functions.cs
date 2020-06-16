using System;
using System.Collections.Generic;
using System.Drawing;

namespace ExtendedControls.Controls
{
    internal static partial class AstroPlot
    {
        static public PointF[] Update(List<double[]> points, double x, double y, double z, double[] cameraPosition, double azimuth, double elevation)
        {
            Matrix<double> _interaction = Interaction(azimuth, elevation, cameraPosition);
            Matrix<double> _data = Coords(x, y, z);
            Matrix<double> X_h = new Matrix<double>(4, 1);

            PointF[] Coordinates = new PointF[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                X_h.SetMatrix(new double[] { points[i][0], points[i][1], points[i][2], 1.0 });
                Matrix<double> P = _data * _interaction * X_h;
                Coordinates[i] = new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
            }
            return Coordinates;
        }

        private static Matrix<double> Coords(double x, double y, double z)
        {
            Matrix<double> _matrix = new Matrix<double>(3, 3);
            double _x = x / 2;
            double _y = y / 2;
            double _a = 1;
            _matrix.SetMatrix(new double[] { z, 0, _x, 0, z * _a, _y, 0, 0, 1 });
            return _matrix;
        }

        private static Matrix<double> Interaction(double azimuth, double elevation, double[] camera)
        {
            Matrix<double> R = Rotate(azimuth, elevation);
            Matrix<double> lens = new Matrix<double>(3, 1);
            lens.SetMatrix(camera);
            Matrix<double> _matrix = R | (-R * lens);
            return _matrix;
        }

        private static Matrix<double> Rotate(double azimuth, double elevation)
        {
            Matrix<double> R = new Matrix<double>(3, 3);
            R.SetMatrix(new double[] { Math.Cos(azimuth), 0, -Math.Sin(azimuth),
                                       Math.Sin(azimuth)*Math.Sin(elevation),  Math.Cos(elevation), Math.Cos(azimuth)*Math.Sin(elevation),
                                       Math.Cos(elevation)*Math.Sin(azimuth), -Math.Sin(elevation), Math.Cos(azimuth)*Math.Cos(elevation) });
            return R;
        }        

        internal static double FindOrbitalElevation(double distance, double inclination)
        {
            var elevation = (inclination / 90) * distance;
            return elevation;
        }

        internal static double FindOrbitalRadius(double distance, double inclination)
        {         
            var angle = (inclination / 90) * distance;
            var radius = Math.Sqrt((distance * distance) - (angle * angle));
            return radius;
        }
    }
}
