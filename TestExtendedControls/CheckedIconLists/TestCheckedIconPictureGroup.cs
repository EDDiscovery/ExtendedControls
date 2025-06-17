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
    public partial class TestCheckedIconPictureGroup : Form
    {
        static List<string> list = new List<string>();
        ThemeList theme;

        public TestCheckedIconPictureGroup()
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

            checkedIconPictureBoxUserControl1.AddAllNone();
            checkedIconPictureBoxUserControl1.AddGroupItem("t1;t2;t3", "1-3");
            checkedIconPictureBoxUserControl1.AddGroupItem("t10;t11;t12;t13;t14", "10-14");
            checkedIconPictureBoxUserControl1.Render(new Point(0, 0), checkedIconPictureBoxUserControl1.Size);
            checkedIconPictureBoxUserControl1.Resize += (s, e) => { checkedIconPictureBoxUserControl1.Render(new Point(0, 0), checkedIconPictureBoxUserControl1.Size); };

        }

    }

}
