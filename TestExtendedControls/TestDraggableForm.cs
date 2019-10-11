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

namespace DialogTest
{
    public partial class TestDraggableForm : ExtendedControls.DraggableForm
    {
        ThemeStandard theme;

        public TestDraggableForm()
        {
            theme = new ThemeStandard();
            ThemeableFormsInstance.Instance = theme;
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");
            theme.FontName = "Microsoft Sans Serif";
            theme.FontSize = 8.25f;
            theme.WindowsFrame = false;

            InitializeComponent();

       //     theme.ApplyStd(this);
            BackColor = Color.Black;

        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            OnCaptionMouseDown((Control)sender, e);
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            OnCaptionMouseUp((Control)sender, e);

        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            Close();           
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            extButton2.Text = Size.ToString();
        }

        Form two;
        private void extButton3_Click(object sender, EventArgs e)
        {
            theme = new ThemeStandard();
            ThemeableFormsInstance.Instance = theme;
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");
            theme.FontName = "Microsoft Sans Serif";
            theme.FontSize = 8.25f;
            theme.WindowsFrame = false;

            two = new DraggableForm();
            theme.ApplyStd(two);
            two.Show(this);
        }
    }
}
