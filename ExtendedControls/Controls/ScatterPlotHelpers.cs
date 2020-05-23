using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExtendedControls
{    
    class ScatterAlgebra
    {
        public class Matrix<T>
        {
            int rows;
            int columns;

            private T[,] matrix;

            public Matrix(int n, int m)
            {
                matrix = new T[n, m];
                rows = n;
                columns = m;
            }

            public void SetValByIdx(int m, int n, T x)
            {
                matrix[n, m] = x;
            }

            public T GetValByIndex(int n, int m)
            {
                return matrix[n, m];
            }

            public void SetMatrix(T[] arr)
            {
                for (int r = 0; r < rows; r++)
                    for (int c = 0; c < columns; c++)
                        matrix[r, c] = arr[r * columns + c];
            }

            public static Matrix<T> operator |(Matrix<T> m1, Matrix<T> m2)
            {
                Matrix<T> m = new Matrix<T>(m1.rows, m1.columns + m2.columns);
                for (int r = 0; r < m1.rows; r++)
                {
                    for (int c = 0; c < m1.columns; c++)
                        m.matrix[r, c] = m1.matrix[r, c];
                    for (int c = 0; c < m2.columns; c++)
                        m.matrix[r, c + m1.columns] = m2.matrix[r, c];
                }
                return m;
            }

            public static Matrix<T> operator *(Matrix<T> m1, Matrix<T> m2)
            {
                Matrix<T> m = new Matrix<T>(m1.rows, m2.columns);
                for (int r = 0; r < m.rows; r++)
                    for (int c = 0; c < m.columns; c++)
                    {
                        T tmp = (dynamic)0;
                        for (int i = 0; i < m2.rows; i++)
                            tmp += (dynamic)m1.matrix[r, i] * (dynamic)m2.matrix[i, c];
                        m.matrix[r, c] = tmp;
                    }
                return m;
            }

            public static Matrix<T> operator ~(Matrix<T> m)
            {
                Matrix<T> tmp = new Matrix<T>(m.columns, m.rows);
                for (int r = 0; r < m.rows; r++)
                    for (int c = 0; c < m.columns; c++)
                        tmp.matrix[c, r] = m.matrix[r, c];
                return tmp;
            }

            public static Matrix<T> operator -(Matrix<T> m)
            {
                Matrix<T> tmp = new Matrix<T>(m.columns, m.rows);
                for (int r = 0; r < m.rows; r++)
                    for (int c = 0; c < m.columns; c++)
                        tmp.matrix[r, c] = -(dynamic)m.matrix[r, c];
                return tmp;
            }

            public override string ToString()
            {
                String output = "";
                for (int r = 0; r < rows; r++)
                {
                    output += "[\t";
                    for (int c = 0; c < columns; c++)
                    {
                        output += matrix[r, c].ToString();
                        if (c < columns - 1) output += ",\t";
                    }
                    output += "]\n";
                }
                return output;
            }
        }
    }

    class ScatterProjection
    {
        static public PointF Project(double[] x, double s_x, double s_y, double f, double[] d_w, double azimuth, double elevation)
        {
            ScatterAlgebra.Matrix<double> Mext = GetMext(azimuth, elevation, d_w);
            ScatterAlgebra.Matrix<double> Mint = GetMint(s_x, s_y, f);
            ScatterAlgebra.Matrix<double> X_h = new ScatterAlgebra.Matrix<double>(4, 1);
            X_h.SetMatrix(new double[] { x[0], x[1], x[2], 1.0 });
            //Debug.Print((Mint * Mext).ToString());
            ScatterAlgebra.Matrix<double> P = Mint * Mext * X_h;
            return new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
        }

        static public PointF[] ProjectVector(List<double[]> x, double s_x, double s_y, double f, double[] d_w, double azimuth, double elevation)
        {
            ScatterAlgebra.Matrix<double> Mext = GetMext(azimuth, elevation, d_w);
            ScatterAlgebra.Matrix<double> Mint = GetMint(s_x, s_y, f);
            ScatterAlgebra.Matrix<double> X_h = new ScatterAlgebra.Matrix<double>(4, 1);

            PointF[] Pvec = new PointF[x.Count];
            for (int i = 0; i < x.Count; i++)
            {
                X_h.SetMatrix(new double[] { x[i][0], x[i][1], x[i][2], 1.0 });
                ScatterAlgebra.Matrix<double> P = Mint * Mext * X_h;
                Pvec[i] = new PointF((float)(P.GetValByIndex(0, 0) / P.GetValByIndex(2, 0)), (float)(P.GetValByIndex(1, 0) / P.GetValByIndex(2, 0)));
            }
            return Pvec;
        }

        static ScatterAlgebra.Matrix<double> GetMint(double s_x, double s_y, double f)
        {
            ScatterAlgebra.Matrix<double> Mint = new ScatterAlgebra.Matrix<double>(3, 3);
            double o_x = s_x / 2;
            double o_y = s_y / 2;
            double a = 1;
            Mint.SetMatrix(new double[] { f, 0, o_x, 0, f * a, o_y, 0, 0, 1 });
            return Mint;
        }

        static ScatterAlgebra.Matrix<double> GetMext(double azimuth, double elevation, double[] d_w)
        {
            ScatterAlgebra.Matrix<double> R = RotationMatrix(azimuth, elevation);
            ScatterAlgebra.Matrix<double> dw = new ScatterAlgebra.Matrix<double>(3, 1);
            dw.SetMatrix(d_w);
            ScatterAlgebra.Matrix<double> Mext = R | (-R * dw);
            return Mext;
        }

        static ScatterAlgebra.Matrix<double> RotationMatrix(double azimuth, double elevation)
        {
            ScatterAlgebra.Matrix<double> R = new ScatterAlgebra.Matrix<double>(3, 3);
            R.SetMatrix(new double[] { Math.Cos(azimuth), 0, -Math.Sin(azimuth),
                                       Math.Sin(azimuth)*Math.Sin(elevation),  Math.Cos(elevation), Math.Cos(azimuth)*Math.Sin(elevation),
                                       Math.Cos(elevation)*Math.Sin(azimuth), -Math.Sin(elevation), Math.Cos(azimuth)*Math.Cos(elevation) });
            return R;
        }
    }

    public class MouseWheelHandler
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
