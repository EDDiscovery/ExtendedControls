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
    public partial class TestButtons2 : Form
    {
        ThemeList theme;

        public TestButtons2()
        {
            InitializeComponent();

            //buttonStd.ImageList = new ImageList() { ImageSize = new Size(32, 32) };
            //buttonStd.ImageList.Images.Add(Properties.Resources.CaptainsLog);
            //buttonStd.ImageIndex = 0;

            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            theme.SetThemeByName("EDSM");
            Theme.Current.FontSize = 8.5f;
            Theme.Current.WindowsFrame = true;


          //  Theme.Current.ButtonStyle = Theme.ButtonstyleGradient;
            //Theme.Current.ButtonStyle = Theme.ButtonstyleFlat;
            Theme.Current.ButtonStyle = Theme.ButtonstyleSystem;
//
            Theme.Current.ApplyStd(this);

        }

    }
}
