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
    public partial class TestFlowBox : Form
    {
        static List<string> list = new List<string>();
        ThemeList theme;

        public TestFlowBox()
        {
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;

            flowLayoutPanel2.Resize += FlowLayoutPanel2_Resize;
        }

        private void FlowLayoutPanel2_Resize(object sender, EventArgs e)
        {
           extGroupBox1.Size = new Size(extGroupBox1.Width, flowLayoutPanel2.Height + 30);
        }

        private void extButton23_Click(object sender, EventArgs e)
        {
            Control button = sender as Button;
            System.Diagnostics.Debug.WriteLine($"Parent {button.Parent.Bounds} cr {button.Parent.ClientRectangle} {button.Parent.ClientSize}");
        }
    }

}
