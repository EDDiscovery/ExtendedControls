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
        ThemeList theme;

        public TestPictureBox()
        {
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;
          //  Theme.Current.ApplyStd(this);

            extScrollBarForPanel2.HideScrollBar = true;
            extScrollBarForPanel2.SmallChange = 16;
            extScrollBarForPanel3.HideScrollBar = true;
            extPictureBoxScroll3.ScrollBarEnabled = false;

            extPictureBox4.AddTextAutoSize(new Point(0, 0), new Size(2000, 2000), "Hello", Font, Color.Red, Color.White,1);
            ExtPictureBox.CheckBox chk1 = new ExtPictureBox.CheckBox();
            chk1.Location = new Rectangle(20, 20, 150, 32);
            chk1.Font = new Font("MS sans serif", 8.75f);
            chk1.Text = "tickbox";
            chk1.CheckState = CheckState.Checked;
            extPictureBox4.Add(chk1);
            extPictureBox4.FillColor = Color.AliceBlue;
            extPictureBox4.Render(minsize: extPictureBox4.Size);

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
            extPictureBoxScroll2.Render();
            extPictureBoxScroll3.Render();
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            extPictureBox1.AddTextAutoSize(new Point(5, vpos), new Size(1000, 1000), "Text to render " + vpos, Font, Color.Red, Color.White, 0.8f);
            extPictureBox2.AddTextAutoSize(new Point(5, vpos), new Size(1000, 1000), "Text to render " + vpos, Font, Color.Red, Color.White, 0.8f);
            extPictureBox3.AddTextAutoSize(new Point(5, vpos), new Size(1000, 1000), "Text to render " + vpos, Font, Color.Red, Color.White, 0.8f);
            vpos += 30;

            extPictureBox1.Render();
            extPictureBoxScroll2.Render();
            extPictureBoxScroll3.Render();
            extButton1.Text = vpos.ToString();
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            extPictureBox1.Render();
            extPictureBox1.AddTextAutoSize(new Point(5, vpos), new Size(1000, 1000), "Text to render " + vpos, Font, Color.Red, Color.White, 0.8f);
            vpos += 30;
        }
    }
}
