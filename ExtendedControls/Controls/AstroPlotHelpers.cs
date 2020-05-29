using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExtendedControls.Controls
{
    static partial class AstroPlot
    {
        static public PointF[] Update(List<double[]> points, double x, double y, double f, double[] camera, double azimuth, double elevation)
        {
            Matrix<double> _interaction = Rotate(azimuth, elevation, camera);
            Matrix<double> _data = DataMatrix(x, y, f);
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

        static Matrix<double> DataMatrix(double x, double y, double f)
        {
            Matrix<double> _matrix = new Matrix<double>(3, 3);
            double _x = x / 2;
            double _y = y / 2;
            double _a = 1;
            _matrix.SetMatrix(new double[] { f, 0, _x, 0, f * _a, _y, 0, 0, 1 });
            return _matrix;
        }

        static Matrix<double> Rotate(double azimuth, double elevation, double[] camera)
        {
            Matrix<double> R = Rotation(azimuth, elevation);
            Matrix<double> lens = new Matrix<double>(3, 1);
            lens.SetMatrix(camera);
            Matrix<double> _matrix = R | (-R * lens);
            return _matrix;
        }

        static Matrix<double> Rotation(double azimuth, double elevation)
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

        // interaction
        public static class MouseWheelHandler
        {
            public static void Add(Control ctrl, Action<MouseEventArgs> onMouseWheel)
            {
                if (ctrl == null || onMouseWheel == null)
                    throw new ArgumentNullException();

                var filter = new MouseWheelMessageFilter(ctrl, onMouseWheel);
                Application.AddMessageFilter(filter);
                ctrl.Disposed += (s, e) => Application.RemoveMessageFilter(filter);
            }

            class MouseWheelMessageFilter
                : IMessageFilter
            {
                private readonly Control _ctrl;
                private readonly Action<MouseEventArgs> _onMouseWheel;

                public MouseWheelMessageFilter(Control ctrl, Action<MouseEventArgs> onMouseWheel)
                {
                    _ctrl = ctrl;
                    _onMouseWheel = onMouseWheel;
                }

                public bool PreFilterMessage(ref Message m)
                {
                    var parent = _ctrl.Parent;
                    if (parent != null && m.Msg == 0x20a) // WM_MOUSEWHEEL, find the control at screen position m.LParam
                    {
                        var pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);

                        var clientPos = _ctrl.PointToClient(pos);

                        if (_ctrl.ClientRectangle.Contains(clientPos)
                         && ReferenceEquals(_ctrl, parent.GetChildAtPoint(parent.PointToClient(pos))))
                        {
                            var wParam = m.WParam.ToInt32();
                            Func<int, MouseButtons, MouseButtons> getButton =
                                (flag, button) => ((wParam & flag) == flag) ? button : MouseButtons.None;

                            var buttons = getButton(wParam & 0x0001, MouseButtons.Left)
                                        | getButton(wParam & 0x0010, MouseButtons.Middle)
                                        | getButton(wParam & 0x0002, MouseButtons.Right)
                                        | getButton(wParam & 0x0020, MouseButtons.XButton1)
                                        | getButton(wParam & 0x0040, MouseButtons.XButton2)
                                        ; // Not matching for these /*MK_SHIFT=0x0004;MK_CONTROL=0x0008*/

                            var delta = wParam >> 16;
                            var e = new MouseEventArgs(buttons, 0, clientPos.X, clientPos.Y, delta);
                            _onMouseWheel(e);

                            return true;
                        }
                    }
                    return false;
                }
            }
        }
    }
}
