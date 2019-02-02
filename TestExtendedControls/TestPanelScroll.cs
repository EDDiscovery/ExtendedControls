using ExtendedControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogTest
{
    public partial class TestPanelScroll : Form
    {
        ThemeStandard theme;
        public TestPanelScroll()
        {
            InitializeComponent();
            theme = new ThemeStandard();
            theme.LoadBaseThemes();

            for (int i = 0; i < 100; i++)
            {
                Label lx = new Label();
                lx.Location = new Point(44 + i % 10, i * 20);
                lx.Text = "Label " + i;
                lx.Size = new Size(200, 20);
                extPanelScroll1.Controls.Add(lx);

                Label lx2 = new Label();
                lx2.Location = new Point(24, i * 20);
                lx2.Text = "px" + i;
                lx2.Size = new Size(20, 20);
                extPanelScroll1.Controls.Add(lx2);

                Panel px = new Panel();
                px.Location = new Point(5, i * 20);
                px.BackgroundImage = Properties.Resources.edlogo24;
                px.Size = new Size(24, 24);
                extPanelScroll1.Controls.Add(px);
            }

            for (int i = 0; i < 100; i++)
            {
                Label lx = new Label();
                lx.Location = new Point(44 + i % 10, i * 20);
                lx.Text = "Label " + i;
                lx.Size = new Size(200, 20);
                panel1.Controls.Add(lx);

                Label lx2 = new Label();
                lx2.Location = new Point(24, i * 20);
                lx2.Text = "px" + i;
                lx2.Size = new Size(20, 20);
                panel1.Controls.Add(lx2);

                Panel px = new Panel();
                px.Location = new Point(5, i * 20);
                px.BackgroundImage = Properties.Resources.edlogo24;
                px.Size = new Size(24, 24);
                panel1.Controls.Add(px);
            }
        }

    }
}
