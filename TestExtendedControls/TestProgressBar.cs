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
    public partial class TestProgressBar : Form
    {
        ThemeList theme;

        public TestProgressBar()
        {
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            extProgressBar1.Value = 0;
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            extProgressBar1.Value = 25;

        }

        private void extButton3_Click(object sender, EventArgs e)
        {
            extProgressBar1.Value = 50;

        }

        private void extButton4_Click(object sender, EventArgs e)
        {
            extProgressBar1.Value = 75;

        }

        private void extButton5_Click(object sender, EventArgs e)
        {
            extProgressBar1.Value = 100;

        }
    }

}
