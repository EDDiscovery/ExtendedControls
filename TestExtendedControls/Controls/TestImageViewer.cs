using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TestExtendedControls
{
    public partial class TestImageViewer : Form
    {
        System.Windows.Forms.Timer t = new Timer();
        string imagefilename;
        DateTime imagetime;

        public TestImageViewer()
        {
            InitializeComponent();
            t.Interval = 50;
            t.Tick += T_Tick;
            imageViewer.BackColor = Color.FromArgb(50, 50, 50);
            imageViewer.AutoPan = true;

//            LoadImage(@"c:\code\md.bmp");
        }

        public void LoadImage(string filename)
        {
            if (File.Exists(filename))
            {
                imagefilename = filename;
                imagetime = File.GetLastWriteTimeUtc(imagefilename);
                imageViewer.Image?.Dispose();
                imageViewer.Image = new Bitmap(imagefilename);
                this.Text = filename;
            }
        }

        private void T_Tick(object sender, EventArgs e)
        {
            if ( imagefilename != null && File.GetLastWriteTimeUtc(imagefilename) > imagetime)
            {
                LoadImage(imagefilename);
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "BMP|*.bmp|JPG|*.jpg|PNG|*.png";
            dlg.Title = "Display image";
            if ( dlg.ShowDialog() == DialogResult.OK)
            {
                LoadImage(dlg.FileName);
            }
        }
    }
}
