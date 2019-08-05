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
    public partial class TestPanelScroll2 : Form
    {
        ThemeStandard theme;

        public TestPanelScroll2()
        {
            InitializeComponent();
            theme = new ThemeStandard();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            theme.WindowsFrame = true;
            extPanelScroll1.FlowControlsLeftToRight = true;
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
