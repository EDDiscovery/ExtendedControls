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
    public partial class TestSmartSystemForm : SmartSysMenuForm
    {
        ThemeList theme;

        public TestSmartSystemForm()
        {
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");

            InitializeComponent();

            Theme.Current.FontName = "Microsoft Sans Serif";
            Theme.Current.FontSize = 12f;
            Theme.Current.WindowsFrame = true;

            Theme.Current.ApplyStd(this);

        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var sysMenus = new List<SystemMenuItem>()
            {
                    new SystemMenuItem(),
                    new SystemMenuItem("One", x=>{ },x=>{ x.ToggleCheck(); }),
                    new SystemMenuItem("Two", x=>{ },(x)=>{x.ToggleCheck(); }),
                    new SystemMenuItem("Submenu",
                                new List<SystemMenuItem>() {
                                        new SystemMenuItem("s-one", x=>{ },x => { x.ToggleCheck(); }),
                                        new SystemMenuItem("s-two", x=>{ },x => { x.ToggleCheck(); }),
                                        new SystemMenuItem("Submenu-2",
                                                    new List<SystemMenuItem>() {
                                                            new SystemMenuItem("s2-one", x=>{ x.SetCheck(true); },x => { x.ToggleCheck(); }),
                                                            new SystemMenuItem("s2-two", x=>{ },x => { x.ToggleCheck(); })
                                                            }),
                                }),
            };

            InstallSystemMenuItems(sysMenus);

        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
        }

        private void extButton3_Click(object sender, EventArgs e)
        {
            Opacity = 0.5;
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            Opacity = 1;
        }
    }
}
