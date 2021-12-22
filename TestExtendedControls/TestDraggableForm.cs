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
    public partial class TestDraggableForm : ExtendedControls.DraggableForm
    {
        ThemeList theme;

        public TestDraggableForm()
        {
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");
            Theme.Current.SetColor(Theme.CI.form, Color.FromArgb(50, 50, 0));
            Theme.Current.FontName = "Microsoft Sans Serif";
            Theme.Current.FontSize = 8.25f;
            Theme.Current.WindowsFrame = false;

            InitializeComponent();

            Theme.Current.ApplyStd(this);

        }

        private void CaptureDown(object sender, MouseEventArgs e)
        {
            OnCaptionMouseDown((Control)sender, e);
        }

        private void CaptureUp(object sender, MouseEventArgs e)
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
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");
            Theme.Current.FontName = "Microsoft Sans Serif";
            Theme.Current.FontSize = 8.25f;
            Theme.Current.WindowsFrame = false;

            two = new DraggableForm();
            Theme.Current.ApplyStd(two);
            two.Show(this);
        }

        private void topresize_MouseDown(object sender, MouseEventArgs e)
        {
            PerformResizeMouseDown((Control)sender, e, DockStyle.Top);
        }

        private void topresize_MouseUp(object sender, MouseEventArgs e)
        {
            PerformResizeMouseUp((Control)sender, e, DockStyle.Top);

        }
    }
}
