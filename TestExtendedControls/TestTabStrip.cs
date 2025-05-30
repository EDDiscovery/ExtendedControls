﻿using ExtendedControls;
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
    public partial class TestTabStrip : Form
    {
        ThemeList theme;

        public TestTabStrip()
        {
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;
            Theme.Current.FontSize = 15;
            
            InitializeComponent();
            tabStrip1.ImageList = new Bitmap[] {
                TestExtendedControls.Properties.Resources.galaxy_red, TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_black, TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_black, TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_black, TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_black, TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_black, TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white, TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_gray,TestExtendedControls.Properties.Resources.galaxy,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white,
                                            };
            tabStrip1.TextList = new string[] { "icon 0", "icon 1",
                "icon 2", "icon 3",
                "icon 4", "icon 5",
                "icon 6", "icon 7",
                "icon 8", "icon 9",
                "icon 10", "icon 11",
                "icon 12", "icon 13",
                "icon 14", "icon 15",
                "icon 16", "icon 17",
                "icon 18", "icon 19",
            };

            tabStrip1.EmptyColor = Color.Red;
            tabStrip1.StripMode = ExtendedControls.TabStrip.StripModeType.StripTop;
            tabStrip1.SetControlText("Ctext1");
            tabStrip1.OnPopOut += (t, i) => System.Diagnostics.Debug.WriteLine("Command pop out" + t + " " + i);
            tabStrip1.OnCreateTab += OnCreateTab;

            tabStrip1.HelpAction = (p) => { System.Diagnostics.Debug.WriteLine("Help at " + p); };

            tabStrip2.ImageList = new Bitmap[] {
                TestExtendedControls.Properties.Resources.galaxy_red,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white,
                                            };
            tabStrip2.TextList = new string[] { "icon 0", "icon 1",
                "icon 2", "icon 3",
                "icon 4", "icon 5",
            };

            tabStrip2.StripMode = ExtendedControls.TabStrip.StripModeType.StripTop;
            tabStrip2.SetControlText("Ctext2");
            tabStrip2.OnPopOut += (t, i) => System.Diagnostics.Debug.WriteLine("2 Command pop out" + t + " " + i);
            tabStrip2.OnCreateTab += OnCreateTab;

            tabStrip3.ImageList = new Bitmap[] {
                TestExtendedControls.Properties.Resources.galaxy_red,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white,
                                            };
            tabStrip3.TextList = new string[] { "3icon 0", "3icon 1",
                "3icon 2", "3icon 3",
                "3icon 4", "3icon 5",
            };

            tabStrip3.StripMode = ExtendedControls.TabStrip.StripModeType.ListSelection;
            tabStrip3.SetControlText("Ctext3");
            tabStrip3.OnPopOut += (t, i) => System.Diagnostics.Debug.WriteLine("3 Command pop out" + t + " " + i);
            tabStrip3.AllowClose += (t, i, c) => { return MessageBox.Show("Allow close", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes; };
            tabStrip3.OnCreateTab += OnCreateTab;

            tabStrip4.ImageList = new Bitmap[] {
                TestExtendedControls.Properties.Resources.galaxy_red,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white,
                TestExtendedControls.Properties.Resources.galaxy_gray,
                TestExtendedControls.Properties.Resources.galaxy_white,
                                            };
            tabStrip4.TextList = new string[] { "4icon 0", "4icon 1",
                "4icon 2", "4icon 3",
                "4icon 4", "4icon 5",
            };

            tabStrip4.StripMode = ExtendedControls.TabStrip.StripModeType.StripTopOpen;
            tabStrip4.SetControlText("Ctext3");
            tabStrip4.OnPopOut += (t, i) => System.Diagnostics.Debug.WriteLine("4 Command pop out" + t + " " + i);
            tabStrip4.OnCreateTab += OnCreateTab;

            tabStrip5.StripMode = ExtendedControls.TabStrip.StripModeType.ListSelection;
            tabStrip5.SetControlText("Ctext3");
            tabStrip5.OnPopOut += (t, i) => System.Diagnostics.Debug.WriteLine("3 Command pop out" + t + " " + i);
            tabStrip5.AllowClose += (t, i, c) => { return MessageBox.Show("Allow close", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes; };
            tabStrip5.OnCreateTab += OnCreateTab;
            tabStrip5.DropDownFitImagesToItemHeight = true;

            tabStrip5.ImageList = new Bitmap[] {
                TestExtendedControls.Properties.Resources.CaptainsLog,
                TestExtendedControls.Properties.Resources.BookmarkManager,
                TestExtendedControls.Properties.Resources.CombatPanel,
                TestExtendedControls.Properties.Resources.Commodities,
                TestExtendedControls.Properties.Resources.Compass,
                TestExtendedControls.Properties.Resources.Discoveries,
                                            };
            tabStrip5.TextList = new string[] { 
                "4icon 0", "4icon 1",
                "4icon 2", "4icon 3",
                "4icon 4", "4icon 5",
            };


            ListViewItem item1 = new ListViewItem("item1", 0);
            // Place a check mark next to the item.
            item1.Checked = true;
            item1.SubItems.Add("1");
            item1.SubItems.Add("2");
            item1.SubItems.Add("3");
            ListViewItem item2 = new ListViewItem("item2", 1);
            item2.SubItems.Add("4");
            item2.SubItems.Add("5");
            item2.SubItems.Add("6");
            ListViewItem item3 = new ListViewItem("item3", 0);
            // Place a check mark next to the item.
            item3.Checked = true;
            item3.SubItems.Add("7");
            item3.SubItems.Add("8");
            item3.SubItems.Add("9");

            // Create columns for the items and subitems.
            // Width of -2 indicates auto-size.
            listView1.Columns.Add("Item Column", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Column 2", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Column 3", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Column 4", -2, HorizontalAlignment.Center);

            //Add the items to the ListView.
            listView1.Items.AddRange(new ListViewItem[] { item1, item2, item3 });

            // Create two ImageList objects.
            ImageList imageListSmall = new ImageList();
            ImageList imageListLarge = new ImageList();

            // Initialize the ImageList objects with bitmaps.
            imageListSmall.Images.Add(TestExtendedControls.Properties.Resources.galaxy_gray);
            imageListSmall.Images.Add(TestExtendedControls.Properties.Resources.galaxy_black);
            imageListLarge.Images.Add(TestExtendedControls.Properties.Resources.galaxy_gray);
            imageListLarge.Images.Add(TestExtendedControls.Properties.Resources.galaxy_gray);

            //Assign the ImageList objects to the ListView.
            listView1.LargeImageList = imageListLarge;
            listView1.SmallImageList = imageListSmall;

            // Add the ListView to the control
            List<string> lv = new List<string>() { "one", "two", "three", "four" };
            List<Image> lvimages = new List<Image>() { TestExtendedControls.Properties.Resources.galaxy_gray, TestExtendedControls.Properties.Resources.galaxy_black, TestExtendedControls.Properties.Resources.galaxy_gray, TestExtendedControls.Properties.Resources.galaxy_black };

            listControlCustom1.Items = lv;
            listControlCustom1.BackColor = Color.Black;
            listControlCustom1.ForeColor = Color.Red;

            listControlCustom2.Items = lv;
            listControlCustom2.BackColor = Color.Black;
            listControlCustom2.ForeColor = Color.Red;
            listControlCustom2.ImageItems = lvimages;

            listControlCustom3.Items = lv;
            listControlCustom3.ImageItems = lvimages;


            listControlCustom4.Items = lv;
            listControlCustom4.FitImagesToItemHeight = true;
            listControlCustom4.BackColor = Color.Black;
            listControlCustom4.ForeColor = Color.Red;
            listControlCustom4.ImageItems = new List<Image>() 
            { TestExtendedControls.Properties.Resources.CaptainsLog,
                TestExtendedControls.Properties.Resources.Discoveries, 
                TestExtendedControls.Properties.Resources.Commodities, 
                TestExtendedControls.Properties.Resources.BookmarkManager };

            listBox1.Items.AddRange(lv.ToArray());

        }

        private Control OnCreateTab(ExtendedControls.TabStrip t, int no)
        {
            UserControl uc = new UserControl();
            uc.BackColor = Color.Cyan;
            Label lb = new Label();
            lb.Location = new Point(10, 10);
            lb.Size = new Size(200, 20);
            lb.Text = t.Name + " User Control " + (no+0);
            uc.Name = "UC " + no;
            uc.Dock = DockStyle.Fill;
            uc.Controls.Add(lb);
            t.SetControlText("CT<" + uc.Name + ">");
            return uc;
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            // Define the default color of the brush as black.
            Brush myBrush = Brushes.Black;

            // Determine the color of the brush to draw each item based 
            // on the index of the item to draw.
            switch (e.Index)
            {
                case 0:
                    myBrush = Brushes.Red;
                    break;
                case 1:
                    myBrush = Brushes.Orange;
                    break;
                case 2:
                    myBrush = Brushes.Purple;
                    break;
            }
            Bitmap bmp = TestExtendedControls.Properties.Resources.galaxy_gray;

            e.Graphics.DrawImage(bmp, new Point(0, e.Bounds.Top));

            Rectangle textarea = e.Bounds;
            textarea.X += bmp.Width;
            // Draw the current item text based on the current Font 
            // and the custom brush settings.
            e.Graphics.DrawString(listBox1.Items[e.Index].ToString(),
                e.Font, myBrush, textarea, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabStrip1.Close();
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 12;
            Theme.Current.ApplyStd(this);

        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 20;
            Theme.Current.ApplyStd(this);
        }

        private void extButton3_Click(object sender, EventArgs e)
        {
            Theme.Current.FontSize = 8.5f;
            Theme.Current.ApplyStd(this);

        }
    }
}
