using ExtendedControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestExtendedControls
{
    public partial class TestAutoScaleCompositeButtons : Form
    {
        ThemeList theme;

        public TestAutoScaleCompositeButtons()
        {
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");
            theme.SetThemeByName("Elite Verdana Small");

            InitializeComponent();

            int vpos = 10;
            int hpos = 10;
            for (int size = 40; size <= 200; size += 40)
            {
                CompositeAutoScaleButton b2 = CompositeAutoScaleButton.QuickInit(Properties.Resources.Selector,
                                                               "Suits & Weapons",
                                                               new Image[] { Properties.Resources.galaxy },
                                                               new Image[] { Properties.Resources.Popout, Properties.Resources.Addtab },
                                                               (o, p) => { System.Diagnostics.Debug.WriteLine("CB " + o + " " + p); });
                b2.AutoScaleFontSizeToWidth = 15;
                b2.Name = "CB2 " + size;
                this.Controls.Add(b2);

                if (vpos + size > ClientRectangle.Height)
                {
                    vpos = 10;
                    hpos += size + 10;
                }

                b2.Bounds = new Rectangle(hpos, vpos, size, size);
                vpos += size + 10;
            }

            {
                int size = 100;
                CompositeAutoScaleButton b2 = CompositeAutoScaleButton.QuickInit(Properties.Resources.Selector,
                                                           "Single",
                                                           new Image[] { Properties.Resources.galaxy },
                                                           new Image[] { Properties.Resources.Popout },
                                                           (o, p) => { System.Diagnostics.Debug.WriteLine("CB " + o + " " + p); },
                                                           2);
                b2.AutoScaleFontSizeToWidth = 15;
                b2.Name = "CB2 " + size;
                this.Controls.Add(b2);
                b2.Bounds = new Rectangle(hpos, vpos, size, size);
            }

            Theme.Current.FontName = "Microsoft Sans Serif";
            Theme.Current.FontSize = 12f;
            Theme.Current.WindowsFrame = true;
            Theme.Current.Opacity = 100;

            System.Diagnostics.Debug.WriteLine($"Theme");
            Theme.Current.ApplyStd(this);
            System.Diagnostics.Debug.WriteLine($"Theme End");
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

    }
}
