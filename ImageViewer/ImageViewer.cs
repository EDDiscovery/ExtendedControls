using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ImageViewer
{
    public partial class ImageViewer : Form
    {
        public ImageViewer()
        {
            InitializeComponent();
            timer.Interval = 100;
            timer.Tick += T_Tick;
            imageViewer.BackColor = Color.FromArgb(50, 50, 50);
            imageViewer.AutoPan = true;
        }

        System.Windows.Forms.Timer timer = new Timer();
        string imagefilename;
        DateTime imagetime;

        public void LoadImage(string filename, bool warn = true)
        {
            if (File.Exists(filename))
            {
                imagefilename = filename;
                imagetime = File.GetLastWriteTimeUtc(imagefilename);
                try
                {
                    var bytes = File.ReadAllBytes(imagefilename);
                    var ms = new MemoryStream(bytes);
                    imageViewer.Image = Image.FromStream(ms);
                    ms.Dispose();
                    //Bitmap bitmap = (Bitmap)Image.FromStream(new Bitmap(imagefilename);
                    //Bitmap clone = bitmap.Clone(new Rectangle(0,0,bitmap.Width,bitmap.Height),bitmap.PixelFormat);
                    //bitmap.Dispose();
                    this.Text = filename;
                    timer.Start();
                }
                catch
                {
                    if (warn)
                    {
                        MessageBox.Show("Bad file");
                        timer.Stop();
                        imagefilename = null;
                        imageViewer.Image?.Dispose();
                    }
                }
            }
        }

        private void T_Tick(object sender, EventArgs e)
        {
            if (imagefilename != null && File.GetLastWriteTimeUtc(imagefilename) > imagetime)
            {
                LoadImage(imagefilename, false);        // may fail if half way thru, try again
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "BMP|*.bmp|JPG|*.jpg|PNG|*.png";
            dlg.Title = "Display image";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                LoadImage(dlg.FileName);
            }
        }

    }
}
