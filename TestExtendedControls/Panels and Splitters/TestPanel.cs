using ExtendedControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestExtendedControls
{
    public partial class TestPanel : Form
    {

        int nolines = 240;

        public TestPanel()
        {
            InitializeComponent();

            for (int i = 0; i < 100; i++)
            {
                Button b = new Button();
                b.Bounds = new Rectangle(10, i * 30, 100, 24);
                b.Text = i.ToString();
                panel1.Controls.Add(b);
            }

            extPanelNewScroll.SuspendLayout();

            for (int i = 0; i < nolines; i++)
            {
                AddLine(i);
            }

            extPanelNewScroll.ResumeLayout();

        }

        public void AddLine(int i)
        {
            Rectangle bpos = new Rectangle(10, i * 30 - extPanelNewScroll.Value, 100, 24);
            System.Diagnostics.Debug.WriteLine($"Add line {i} : {bpos}");
            Button b = new Button();
            b.Bounds = bpos;
            b.Text = i.ToString() + "A";
            Button b2 = new Button();
            b2.Bounds = new Rectangle(200, i * 30 - extPanelNewScroll.Value, 100, 24);
            b2.Text = i.ToString() + "B";
            extPanelNewScroll.SuspendLayout();
            extPanelNewScroll.Controls.Add(b);
            extPanelNewScroll.Controls.Add(b2);
            extPanelNewScroll.ResumeLayout();
        }



        private void buttonDown_Click(object sender, EventArgs e)
        {
            ScrollPanel(0, -10);
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            ScrollPanel(10, 0);

        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            ScrollPanel(-10, 0);

        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            ScrollPanel(0, 10);
        }

        public void ScrollPanel(int x, int y)
        {
            Rectangle cr = panel1.ClientRectangle;
            BaseUtils.Win32.RECT rcClip = BaseUtils.Win32.RECT.FromXYWH(cr.X, cr.Y, cr.Width, cr.Height);
            BaseUtils.Win32.RECT rcUpdate = BaseUtils.Win32.RECT.FromXYWH(cr.X, cr.Y, cr.Width, cr.Height);

            BaseUtils.Win32.SafeNativeMethods.ScrollWindowEx(new HandleRef(panel1, panel1.Handle), x,y,
                                             null,
                                             ref rcClip,
                                             BaseUtils.Win32.NativeMethods.NullHandleRef,
                                             ref rcUpdate,
                                             BaseUtils.Win32.NativeMethods.SW_INVALIDATE
                                             | BaseUtils.Win32.NativeMethods.SW_ERASE
                                             | BaseUtils.Win32.NativeMethods.SW_SCROLLCHILDREN);

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //for( int i = extPanelNewScroll.Controls.Count-10; i < extPanelNewScroll.Controls.Count;i++)
            //{
            //    Control c = extPanelNewScroll.Controls[i];
            //    System.Diagnostics.Debug.WriteLine($"Control {c.Text} at {c.Top}");
            //}

            AddLine(nolines++);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            List<Control> remove = new List<Control>();
            for (int i = extPanelNewScroll.Controls.Count - 2; i < extPanelNewScroll.Controls.Count; i++)
            {
                remove.Add(extPanelNewScroll.Controls[i]);
            }

            System.Diagnostics.Debug.WriteLine($"Remove {remove[0].Text}");
            extPanelNewScroll.SuspendLayout();
            foreach (var x in remove)
                extPanelNewScroll.Controls.Remove(x);
            extPanelNewScroll.ResumeLayout();


        }
    }




}
