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
    public partial class TestImageControlScroll : Form
    {
        Font tf = new Font("Verdada", 16f);

        public TestImageControlScroll()
        {
            InitializeComponent();

            imageControl1.BackgroundImage = Properties.Resources.FleetCarrier;
            imageControl1.BackColor = Color.Green;

            imageControl1.ImageSize = new Size(Properties.Resources.FleetCarrier.Width, Properties.Resources.FleetCarrier.Height);
            imageControl1.ImageLayout = ImageLayout.Stretch;
            imageControl1.BackgroundImageLayout = ImageLayout.Stretch;
            imageControlScroll1.ImageControlMinimumHeight = imageControl1.ImageSize.Height;

            imageControl1.EnterMouseArea += (c, a,l,e) => { System.Diagnostics.Debug.WriteLine($"Enter {a.Location} @ {l} cp {e.Location}"); };
            imageControl1.LeaveMouseArea += (c, a,l,e) => { System.Diagnostics.Debug.WriteLine($"Leave {a.Location} @ {l}  cp {e.Location}"); };
            imageControl1.ClickOnMouseArea += (c, a, l,e) => { System.Diagnostics.Debug.WriteLine($"Click {a.Location} @ {l}  cp {e.Location}"); };
        }

        int vpos = 10;
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            var sizef1 = imageControl1.DrawMeasureText(new Rectangle(10, vpos, 30000, 30000), "First message", tf, Color.Yellow, null);
            vpos += 30;
            var sizef2 = imageControl1.DrawMeasureText(new Point(10,50), new Size(30000,30000), "Second message", tf, Color.Yellow, Color.Red);
            vpos += 30;

            imageControl1.AddMouseArea(sizef1,"Tooltip first message");
            imageControl1.AddMouseArea(sizef2,"Tooltip second message");

            //imageControl1.DrawImage(Properties.Resources.BookmarkManager, new Rectangle(200, 20, 200, 200));

            //string[] list = new string[] { "one", null, "three", "four", null, "six" };
            //imageControl1.DrawText(new Rectangle(500, 30, 100, 300), list, tf, 0, Color.Red, Color.White);
            //imageControl1.DrawText(new Point(700,30), new Size(30000,3000), list, tf, 0, Color.Red, Color.White);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            imageControl1.DrawText(new Rectangle(10, vpos, 30000, 30000), $"Message {vpos}", tf, Color.Yellow, null);
            imageControl1.Invalidate();
            vpos += 30;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            imageControl1.Clear();
            vpos = 10;
            imageControl1.DrawText(new Rectangle(10, vpos, 30000, 30000), "Cleared", tf, Color.Yellow, null);
            imageControl1.Invalidate();
            vpos += 30;
        }


        private void TestImageControl_Resize(object sender, EventArgs e)
        {
            imageControlScroll1.Size = new Size(ClientRectangle.Width - 100, ClientRectangle.Height - 20);
        }
    }
}
