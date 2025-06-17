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
    public partial class TestPanelDrawnPanel : Form
    {
        public TestPanelDrawnPanel()
        {
            InitializeComponent();

            int hpos = 10, vpos = 10;

            int[] sizes = new int[] { 48, 32, 25, 24, 17, 16, 12, 8 };

            foreach( int size in sizes)
            {
                System.Diagnostics.Debug.WriteLine("Size " + size);
                hpos = 10;

                Label l = new Label();
                l.Location = new Point(hpos, vpos);
                l.Size = new Size(30, 24);
                l.Text = size.ToString();
                Controls.Add(l);
                hpos += l.Size.Width + 4;

                foreach (var x in Enum.GetValues(typeof(ExtButtonDrawn.ImageType)).Cast<ExtButtonDrawn.ImageType>())
                {
                    if (hpos > this.Width - 40)
                    {
                        hpos = 10;
                        vpos += size + 4;
                    }

                    ExtButtonDrawn p = new ExtButtonDrawn();
                    p.Location = new Point(hpos, vpos);
                    p.Size = new Size(size, size);
                    p.ImageSelected = x;
                    p.MouseOverColor = Color.Red;
                    p.Text = "Hej";
                    p.Padding = new Padding(0);
                    Controls.Add(p);
                    hpos += p.Size.Width+4;
                }

                vpos += Math.Max(32, size + 4);
            }
        }

    }
}
