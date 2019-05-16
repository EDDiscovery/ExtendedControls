using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExtendedControls
{
    public class ExtListBoxForm : Form
    {
        private ExtListBox listcontrol;

        public event EventHandler SelectedIndexChanged;
        public event KeyPressEventHandler KeyPressed;
        public event KeyEventHandler OtherKeyPressed;

        public List<string> Items { get { return listcontrol.Items; } set { listcontrol.Items = value; } }
        public int[] ItemSeperators { get { return listcontrol.ItemSeperators; } set { listcontrol.ItemSeperators = value; } }     // set to array giving index of each separator
        public List<Image> ImageItems { get { return listcontrol.ImageItems; } set { listcontrol.ImageItems = value; } }
        public Color MouseOverBackgroundColor { get { return listcontrol.MouseOverBackgroundColor; } set { listcontrol.MouseOverBackgroundColor = value; } }
        public int SelectedIndex { get { return listcontrol.SelectedIndex; } set { listcontrol.SelectedIndex = value; } }
        public Color SelectionBackColor { get { return listcontrol.SelectionBackColor; } set { listcontrol.SelectionBackColor = value; this.BackColor = value; } }
        public Color BorderColor { get { return listcontrol.BorderColor; } set { listcontrol.BorderColor = value; } }
        public Color ScrollBarColor { get { return listcontrol.ScrollBarColor; } set { listcontrol.ScrollBarColor = value; } }
        public Color ScrollBarButtonColor { get { return listcontrol.ScrollBarButtonColor; } set { listcontrol.ScrollBarButtonColor = value; } }
        public FlatStyle FlatStyle { get { return listcontrol.FlatStyle; } set { listcontrol.FlatStyle = value; } }
        public new Font Font { get { return base.Font; } set { base.Font = value; listcontrol.Font = value; } }
        public bool FitToItemsHeight { get { return listcontrol.FitToItemsHeight; } set { listcontrol.FitToItemsHeight = value; } }
        public float GradientColorScaling { get { return listcontrol.GradientColorScaling; } set { listcontrol.GradientColorScaling = value; } }
        public bool FitImagesToItemHeight { get { return listcontrol.FitImagesToItemHeight; } set { listcontrol.FitImagesToItemHeight = value; } }                    // if set images need to fit within item height
        public Color ItemSeperatorColor { get { return listcontrol.ItemSeperatorColor; } set { listcontrol.ItemSeperatorColor = value; } }

        public Point SetLocation { get; set; } = new Point(int.MinValue, -1);     // force to this location.
        public void PositionBelow(Control c) { SetLocation = c.PointToScreen(new Point(0, c.Height)); }
        public bool RightAlignedToLocation { get; set; } = false;

        private bool closeondeactivateselected;

        public ExtListBoxForm(string name = "", bool closeondeact = true)
        {
            closeondeactivateselected = closeondeact;

            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.listcontrol = new ExtListBox();
            this.Name = this.listcontrol.Name = name;
            this.listcontrol.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.listcontrol.Dock = DockStyle.Fill;
            this.listcontrol.Visible = true;
            this.listcontrol.SelectedIndexChanged += listcontrol_SelectedIndexChanged;
            this.listcontrol.KeyPressed += listcontrol_KeyPressed;
            this.listcontrol.OtherKeyPressed += listcontrol_OtherKeyPressed;
            this.listcontrol.Margin = new Padding(0);
            this.listcontrol.FitToItemsHeight = false;
            this.Padding = new Padding(0);
            this.Controls.Add(this.listcontrol);
            this.Activated += new System.EventHandler(this.FormActivated);
        }

        private void FormActivated(object sender, EventArgs e)
        {
            if ( SetLocation.X != int.MinValue)
            {
                Location = SetLocation;
            }

            int ih = (int)Font.GetHeight() + 2;
            int hw = ih * Items.Count + 4;

          //  System.Diagnostics.Debug.WriteLine("Set LBF loc " + Location + " Font " + Font + " ih " + ih + " hw " + hw);

            using (Graphics g = this.CreateGraphics())
            {
                Size max = listcontrol.MeasureItems(g);
                this.PositionSizeWithinScreen(max.Width + 16 + listcontrol.ScrollBarWidth, hw, true, 64, RightAlignedToLocation);    // keep it on the screen. 
            }

            //            System.Diagnostics.Debug.WriteLine(".. now " + Location + " " + Size + " Items " + Items.Count + " ih "  + ih + " hw" + hw);
        }

        public void KeyDownAction(KeyEventArgs e)
        {
            listcontrol.KeyDownAction(e);
        }


        private void listcontrol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (closeondeactivateselected)
                this.Close();

            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, e);

        }

        private void listcontrol_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (KeyPressed != null)
                KeyPressed(this, e);
        }

        private void listcontrol_OtherKeyPressed(object sender, KeyEventArgs e)
        {
            if (OtherKeyPressed != null)
                OtherKeyPressed(this, e);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);

            if ( closeondeactivateselected)
                this.Close();
        }


    }

}
