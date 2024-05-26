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
    public partial class TestPipsAdvancedLabel : Form
    {

        ThemeList theme;

        public TestPipsAdvancedLabel()
        {
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;

            multiPipControl2.Font = multiPipControl3.Font = multiPipControl1.Font = new Font("Euro Caps", 12F);
            multiPipControl1.Add(multiPipControl2);
            multiPipControl1.Add(multiPipControl3);
            multiPipControl2.Add(multiPipControl1);
            multiPipControl2.Add(multiPipControl3);
            multiPipControl3.Add(multiPipControl1);
            multiPipControl3.Add(multiPipControl2);
        }


    }

}
