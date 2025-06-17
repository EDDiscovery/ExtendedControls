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
    public partial class TestPanelScroll2 : Form
    {
        ThemeList theme;

        public TestPanelScroll2()
        {
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;
            extPanelScroll1.FlowControlsLeftToRight = true;
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 12;
            Theme.Current.ApplyStd(this);
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 20;
            Theme.Current.ApplyStd(this);
        }
    }
}
