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
    public partial class TestRollUpPanel : Form
    {

        ThemeStandard theme;

        public TestRollUpPanel()
        {
            InitializeComponent();

            theme = new ThemeStandard();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            theme.WindowsFrame = true;
        
            rolluppanel.HiddenMarkerWidth = -100;
            rolluppanel.SetPinState(true);
        }

        private void TestRollUpPanel_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Key down " + e.KeyCode + " " + e.KeyData);

        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            theme.FontSize = 12;
            theme.ApplyStd(this);

        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            theme.FontSize = 20;
            theme.ApplyStd(this);
        }



        private void flowLayoutPanel2_Resize(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine($"Panel outer.flow1 {flowLayoutPanel1.Location} {flowLayoutPanel1.Size}");
            //System.Diagnostics.Debug.WriteLine($"Panel mid {panelMid.Location} {panelMid.Size}");
            //System.Diagnostics.Debug.WriteLine($"Panel flow2 {flowLayoutPanel2.Location} {flowLayoutPanel2.Size}");
            //System.Diagnostics.Debug.WriteLine($"Panel bot {panelBot.Location} {panelBot.Size}");
            //System.Diagnostics.Debug.WriteLine($"");
            //PerformLayout();
        }
    }
}
