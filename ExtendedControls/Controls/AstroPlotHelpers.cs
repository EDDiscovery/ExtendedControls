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

        static public PointF Project(double[] x, double s_x, double s_y, double f, double[] camera, double azimuth, double elevation)
        {
            Matrix<double> Mext = GetMext(azimuth, elevation, camera);
            Matrix<double> Mint = GetMint(s_x, s_y, f);
            Matrix<double> X_h = new Matrix<double>(4, 1);
            X_h.SetMatrix(new double[] { x[0], x[1], x[2], 1.0 });
            //Debug.Print((Mint * Mext).ToString());
            Matrix<double> P = Mint * Mext * X_h;
            return new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
        }

        static public PointF[] ProjectVector(List<double[]> x, double s_x, double s_y, double f, double[] camera, double azimuth, double elevation)
        {
            Matrix<double> Mext = GetMext(azimuth, elevation, camera);
            Matrix<double> Mint = GetMint(s_x, s_y, f);
            Matrix<double> X_h = new Matrix<double>(4, 1);

            PointF[] Pvec = new PointF[x.Count];
            for (int i = 0; i < x.Count; i++)
            {
                X_h.SetMatrix(new double[] { x[i][0], x[i][1], x[i][2], 1.0 });
                Matrix<double> P = Mint * Mext * X_h;
                Pvec[i] = new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
            }
            return Pvec;
        }

        static Matrix<double> GetMint(double s_x, double s_y, double f)
        {
            Matrix<double> Mint = new Matrix<double>(3, 3);
            double o_x = s_x / 2;
            double o_y = s_y / 2;
            double a = 1;
            Mint.SetMatrix(new double[] { f, 0, o_x, 0, f * a, o_y, 0, 0, 1 });
            return Mint;
        }

        static Matrix<double> GetMext(double azimuth, double elevation, double[] camera)
        {
            Matrix<double> R = RotationMatrix(azimuth, elevation);
            Matrix<double> dw = new Matrix<double>(3, 1);
            dw.SetMatrix(camera);
            Matrix<double> Mext = R | (-R * dw);
            return Mext;
        }

        static Matrix<double> RotationMatrix(double azimuth, double elevation)
        {
            Matrix<double> R = new Matrix<double>(3, 3);
            R.SetMatrix(new double[] { Math.Cos(azimuth), 0, -Math.Sin(azimuth),
                                       Math.Sin(azimuth)*Math.Sin(elevation),  Math.Cos(elevation), Math.Cos(azimuth)*Math.Sin(elevation),
                                       Math.Cos(elevation)*Math.Sin(azimuth), -Math.Sin(elevation), Math.Cos(azimuth)*Math.Cos(elevation) });
            return R;
        }

        internal static double FindOrbitalElevation(double distance, double inclination)
        {            
            inclination = (inclination / 90) * distance;
            var elevation = inclination;
            return elevation;
        }

        internal static double FindOrbitalRadius(double distance, double inclination)
        {         
            inclination = (inclination / 90) * distance;
            var radius = Math.Sqrt((distance * distance) - (inclination * inclination));
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
