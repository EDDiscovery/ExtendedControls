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

namespace TestExtendedControls
{
    public partial class TestPanelScroll : Form
    {
        ThemeStandard theme;

        public TestPanelScroll()
        {
            InitializeComponent();
            theme = new ThemeStandard();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            theme.WindowsFrame = true;

            int spacing = 30;

            for (int i = 0; i < 30; i++)
            {
                Label lx = new Label();
                lx.Location = new Point(44 + i % 10, i * spacing);
                lx.Text = "Label " + i;
                lx.Size = new Size(200, 20);
                extPanelScroll1.Controls.Add(lx);

                Label lx2 = new Label();
                lx2.Location = new Point(24, i * spacing);
                lx2.Text = "px" + i;
                lx2.Size = new Size(20, 20);
                extPanelScroll1.Controls.Add(lx2);

                Panel px = new Panel();
                px.Location = new Point(5, i * spacing);
                px.BackgroundImage = Properties.Resources.edlogo24;
                px.Size = new Size(24, 24);
                px.BackgroundImageLayout = ImageLayout.Stretch;
                extPanelScroll1.Controls.Add(px);
            }

        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            theme.FontSize = 12;
            theme.ApplyStd(this);
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            theme.FontSize = 20;
            theme.ApplyStd(this);
        }
    }
}
