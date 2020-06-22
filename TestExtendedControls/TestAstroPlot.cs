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
    public partial class TestAstroPlot : Form
    {
        private ThemeStandard theme;

        public TestAstroPlot()
        {
            theme = new ThemeStandard();
            ThemeableFormsInstance.Instance = theme;
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");
            theme.FontSize = 8.25f;
            theme.WindowsFrame = true;

            InitializeComponent();

            astroPlot.AxesWidget = true;
            astroPlot.FramesWidget = false;
        }
    }
}
