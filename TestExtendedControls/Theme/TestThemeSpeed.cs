using BaseUtils;
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
    // Demonstrating that CB system combo box is much slower than our owner draw version

    public partial class TestThemeSpeed : Form
    {
        static List<string> list = new List<string>();
        ThemeList theme;

        public TestThemeSpeed()
        {
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
           // Theme.Current.ButtonStyle = "System";           // if removed, its faster
            Theme.Current.WindowsFrame = true;

            System.Diagnostics.Debug.WriteLine($"start {AppTicks.TickCountLap("AP", true)}");

            for ( int  i = 0; i < 1000; i++ )
            {
                ExtComboBox cb = new ExtComboBox();
                cb.Location = new Point(1,30*i);
                cb.Size = new Size(200, 24);
                cb.Items = new string[] { "one", "two" };
                cb.SelectedIndex = 0;
                panelMain.Controls.Add(cb);
            }

            Theme.Current.ApplyStd(this);

            System.Diagnostics.Debug.WriteLine($"added {AppTicks.TickCountLap("AP")}");
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            string r = AppTicks.TickCountLap("AP");
            System.Diagnostics.Debug.WriteLine($"onshown {r}");
        }


    }

}
