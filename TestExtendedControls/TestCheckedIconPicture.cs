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
    public partial class TestCheckedIconPicture : Form
    {
        static List<string> list = new List<string>();
        ThemeList theme;

        public TestCheckedIconPicture()
        {
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;

            checkedIconPictureBoxUserControl1.BackColor = Color.AliceBlue;
            checkedIconPictureBoxUserControl1.MultipleColumns = true;

            for (int i = 0; i < 200; i++)
            {
                checkedIconPictureBoxUserControl1.Add($"t{i}", $"Text {i}", Properties.Resources.Addtab);
            }
            checkedIconPictureBoxUserControl1.Render(new Point(0, 0), checkedIconPictureBoxUserControl1.Size);
            checkedIconPictureBoxUserControl1.Resize += (s, e) => { checkedIconPictureBoxUserControl1.Render(new Point(0, 0), checkedIconPictureBoxUserControl1.Size); };

        }

    }

}
