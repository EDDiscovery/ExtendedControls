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
    public partial class TestLabel : Form
    {
        static List<string> list = new List<string>();
        ThemeList theme;

        public TestLabel()
        {
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;

            Font fnt = new Font("MS Sans Serif", 12F);
            string text = extFixedWidthLabel1.Text.Substring(0, extFixedWidthLabel1.Text.Length);
            for( int width = 100; width < 800; width+=100)
            {
                using (StringFormat fmt = new StringFormat())
                {
                    SizeF sz = BaseUtils.BitMapHelpers.MeasureStringInBitmap(text, fnt, StringFormat.GenericTypographic, new Size(width, 1280));
                    System.Diagnostics.Debug.WriteLine($"For {width} measured {sz}");
                    int height = (int)(sz.Height + 1);
                    Bitmap mp = new Bitmap(width, height);
                    BaseUtils.BitMapHelpers.DrawTextIntoBitmap(mp, new Rectangle(0, 0, width+5, height), text, fnt, Color.White, Color.Black, 1, StringFormat.GenericTypographic);
                    mp.Save($"c:\\code\\text{width}.bmp");
                }
            }
        }


        private void extButton1_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"\r\nBegin resized");
            extFixedWidthLabel1.Width = extFixedWidthLabel1.Width > 400 ? 100 : extFixedWidthLabel1.Width + 100;
            System.Diagnostics.Debug.WriteLine($"Final size {extFixedWidthLabel1.Size} ");

        }

        int tno = 0;
        private void extButton2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"\r\nBegin Text change");
            extFixedWidthLabel1.Text = extFixedWidthLabel1.Text + $" anoth{tno++}";
            System.Diagnostics.Debug.WriteLine($"Final size {extFixedWidthLabel1.Size} ");
        }

        private void extButton3_Click(object sender, EventArgs e)
        {
            extFixedWidthLabel1.Font = new Font(extFixedWidthLabel1.Font.Name, extFixedWidthLabel1.Font.Size == 8.25f ? 12f : 8.25f);
        }
    }

}
