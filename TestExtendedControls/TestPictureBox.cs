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
    public partial class TestPictureBox : Form
    {
        ThemeStandard theme;

        public TestPictureBox()
        {
            InitializeComponent();
            theme = new ThemeStandard();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            theme.WindowsFrame = true;
            theme.ApplyStd(this);

            extScrollBar1.HideScrollBar = true;
            extScrollBar1.SmallChange = 16;
            extScrollBar2.HideScrollBar = true;
            extPictureBoxScroll2.ScrollBarEnabled = false;
        }

        int vpos = 5;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            for (int i = 0; i < 10; i++)
            {
                extPictureBox1.AddTextAutoSize(new Point(5, vpos), new Size(1000, 1000), "Text to render " + vpos, Font, Color.Red, Color.White, 0.8f);
                extPictureBox2.AddTextAutoSize(new Point(5, vpos), new Size(1000, 1000), "Text to render " + vpos, Font, Color.Red, Color.White, 0.8f);
                extPictureBox3.AddTextAutoSize(new Point(5, vpos), new Size(1000, 1000), "Text to render " + vpos, Font, Color.Red, Color.White, 0.8f);
                vpos += 30;
            }
            extPictureBox1.Render();
            extPictureBoxScroll1.Render();
            extPictureBoxScroll2.Render();
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            extPictureBox1.AddTextAutoSize(new Point(5, vpos), new Size(1000, 1000), "Text to render " + vpos, Font, Color.Red, Color.White, 0.8f);
            extPictureBox2.AddTextAutoSize(new Point(5, vpos), new Size(1000, 1000), "Text to render " + vpos, Font, Color.Red, Color.White, 0.8f);
            extPictureBox3.AddTextAutoSize(new Point(5, vpos), new Size(1000, 1000), "Text to render " + vpos, Font, Color.Red, Color.White, 0.8f);
            vpos += 30;

            extPictureBox1.Render();
            extPictureBoxScroll1.Render();
            extPictureBoxScroll2.Render();
            extButton1.Text = vpos.ToString();
        }
    }
}
