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
    public partial class TestAutoScaleCompositeButtons : Form
    {
        ThemeList theme;

        public TestAutoScaleCompositeButtons()
        {
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");

            InitializeComponent();

            int vpos = 10;
            int hpos = 10;
            for( int size = 40; size <= 240; size += 40)
            {
                CompositeAutoScaleButton b2 = CompositeAutoScaleButton.QuickInit(Properties.Resources.Selector,
                                                               "Suits & Weapons", Color.Yellow, Color.Blue,
                                                               new Image[] { Properties.Resources.edsm32x32 },
                                                               new Image[] { Properties.Resources.edlogo24, Properties.Resources.galaxy_black },
                                                               (o, p) => { System.Diagnostics.Debug.WriteLine("CB " + o + " " + p); });
                b2.AutoScaleFontSizeToWidth = 15;
                b2.Name = "CB2 " + size;
                this.Controls.Add(b2);

                if ( vpos + size > ClientRectangle.Height)
                {
                    vpos = 10;
                    hpos += size + 10;
                }

                b2.Bounds = new Rectangle(hpos, vpos, size,size);
                vpos += size + 10;
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
