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
    public partial class TestButtons : Form
    {
        ThemeList theme;

        public TestButtons()
        {
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            //theme.SetThemeByName("EDSM");

            Theme.Current.FontSize = 12f;
            Theme.Current.WindowsFrame = true;

            InitializeComponent();

            Theme.Current.ApplyStd(this);

            toolTip.Popup += ToolTip_Popup;
        }

        // demo a dynamic tooltip
        bool ignorepopup = false;
        private void ToolTip_Popup(object sender, PopupEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Pop up {e.AssociatedControl} Cancel {ignorepopup}");
            if ( ignorepopup == true)
            {
            }
            else if ( e.AssociatedControl == extButton1)
            {
                //e.ToolTipSize = new Size(200, 200);
                //toolTip.ToolTipTitle = "hello there\r\nHow are you\r\n" + DateTime.UtcNow.ToString();
                //                toolTip.SetToolTip(extButton1, );

                ignorepopup = true;
                toolTip.SetToolTip(extButton1, DateTime.UtcNow.ToString());     // this causes a new pop up, which we ignore setting again due to setting
                ignorepopup = false;
            }
            
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            extButton4.Text = "!" + this.WindowState.ToString();
        }

        private void extButton5_Click(object sender, EventArgs e)
        {
            if (groupBox1.FlatStyle == FlatStyle.System)
                groupBox1.FlatStyle = FlatStyle.Flat;
            else if (groupBox1.FlatStyle == FlatStyle.Flat)
                groupBox1.FlatStyle = FlatStyle.Popup;
            else if (groupBox1.FlatStyle == FlatStyle.Popup)
                groupBox1.FlatStyle = FlatStyle.Standard;
            else if (groupBox1.FlatStyle == FlatStyle.Standard)
                groupBox1.FlatStyle = FlatStyle.System;

            groupBox1.Invalidate();
        }

        private void extButton6_Click(object sender, EventArgs e)
        {
            groupBox1.BackColor = Color.DarkBlue;
            groupBox1.FlatStyle = FlatStyle.Flat;
            groupBox1.Font = new Font("Euro Caps", 12F);
        }

        private void upDown1_Selected(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Up/Down " + e.Delta);
        }

        private void extCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Click");
        }

        private void extButton11_Click(object sender, EventArgs e)
        {
            extCheckBoxEDSMSmall.Enabled = extCheckBoxButtonText.Enabled = extCheckBoxButtonIT.Enabled =

            extCheckBoxSys.Enabled = extCheckBoxPopUpNormal.Enabled = extCheckBoxPopupNormalImage.Enabled = !extCheckBoxPopupNormalImage.Enabled;
        }

        private void extButton12_Click(object sender, EventArgs e)
        {
            theme.SetThemeByName("Windows Default");
            Theme.Current.FontSize = 12;
            Theme.Current.ApplyStd(this);
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            theme.SetThemeByName("Elite EuroCaps");
            Theme.Current.FontSize = 15f;
            Theme.Current.ApplyStd(this);
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            theme.SetThemeByName("Elite EuroCaps");
            Theme.Current.FontSize = 8.25f;
            Theme.Current.ApplyStd(this);
        }

        private void TestButtons_MouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Click on form");
        }

        private void TestButtons_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Double Click on form");

        }

        private void extButton8_Click(object sender, EventArgs e)
        {

        }
    }
}
