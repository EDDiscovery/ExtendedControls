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
            extProgressBar.Value = 100;
            extProgressBar.Limit = 100;
            extProgressBar.Maximum = 110;
            extProgressBar.Minimum = 0;
            extProgressBar.TrackSpeed = 5;
            extProgressBar.MarkerLineColor = Color.Yellow;
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            winprogressbar.Value = 0;
            extProgressBar.Value = 0;
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            winprogressbar.Value = 25;
            extProgressBar.Value = 25;
            extProgressBar.Marker1 = 12;
        }

        private void extButton3_Click(object sender, EventArgs e)
        {
            winprogressbar.Value = 50;
            extProgressBar.Value = 50;
            extProgressBar.Marker1 = 25;
        }

        private void extButton4_Click(object sender, EventArgs e)
        {
            winprogressbar.Value = 75;
            extProgressBar.Value = 75;

        }

        private void extButton5_Click(object sender, EventArgs e)
        {
            winprogressbar.Value = 100;
            extProgressBar.Value = 100;

        }

        private void extButton6_Click(object sender, EventArgs e)
        {
            extProgressBar.Value = 110;
            extProgressBar.Marker1 = 90;
            extProgressBar.Marker2 = 50;

        }
    }

}
