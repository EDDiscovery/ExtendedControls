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
    public partial class TestProgressBar : Form
    {
        ThemeList theme;

        public TestProgressBar()
        {
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;
            extProgressBar.Value = 100;
            extProgressBar.Limit = 100;
            extProgressBar.Maximum = 110;
            extProgressBar.Minimum = 0;
            extProgressBar.TrackSpeed = 5;
            extProgressBar.MarkerLineColor = Color.Yellow;

            extProgressBarMultiSegment1.SegmentValues = new double[] { 10, 20, 30 };
            extProgressBarMultiSegment1.SegmentColors = new Color[] { Color.Red, Color.Green, Color.Blue, Color.Cyan, Color.Magenta };
            extProgressBarMultiSegment1.Limit = 100;

            extNumericUpDownSeg0.Value = (int)extProgressBarMultiSegment1.SegmentValues[0];
            extNumericUpDownSeg1.Value = (int)extProgressBarMultiSegment1.SegmentValues[1];
            extNumericUpDownSeg2.Value = (int)extProgressBarMultiSegment1.SegmentValues[2];
            this.extNumericUpDownSeg0.ValueChanged += new System.EventHandler(this.extNumericUpDownSeg0_ValueChanged);
            this.extNumericUpDownSeg1.ValueChanged += new System.EventHandler(this.extNumericUpDownSeg1_ValueChanged);
            this.extNumericUpDownSeg2.ValueChanged += new System.EventHandler(this.extNumericUpDownSeg2_ValueChanged);
            extProgressBarMultiSegment1.TrackSpeed = 1;
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            winprogressbar.Value = 0;
            extProgressBar.Value = 0;
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            winprogressbar.Value = 25;
            extProgressBar.Value = 25;
            extProgressBar.Marker1 = 12;
        }

        private void extButton3_Click(object sender, EventArgs e)
        {
            winprogressbar.Value = 50;
            extProgressBar.Value = 50;
            extProgressBar.Marker1 = 25;
        }

        private void extButton4_Click(object sender, EventArgs e)
        {
            winprogressbar.Value = 75;
            extProgressBar.Value = 75;

        }

        private void extButton5_Click(object sender, EventArgs e)
        {
            winprogressbar.Value = 100;
            extProgressBar.Value = 100;

        }

        private void extButton6_Click(object sender, EventArgs e)
        {
            extProgressBar.Value = 110;
            extProgressBar.Marker1 = 90;
            extProgressBar.Marker2 = 50;

        }

        private void extNumericUpDownSeg0_ValueChanged(object sender, EventArgs e)
        {
            extProgressBarMultiSegment1.ChangeValue(0, extNumericUpDownSeg0.Value);

        }

        private void extNumericUpDownSeg1_ValueChanged(object sender, EventArgs e)
        {
            extProgressBarMultiSegment1.ChangeValue(1, extNumericUpDownSeg1.Value);

        }

        private void extNumericUpDownSeg2_ValueChanged(object sender, EventArgs e)
        {
            extProgressBarMultiSegment1.ChangeValue(2, extNumericUpDownSeg2.Value);

        }

        private void extButtonSeg0_Zero_Click(object sender, EventArgs e)
        {
            extProgressBarMultiSegment1.ChangeValue(0, 0);

        }

        private void extButtonSeg1_Zero_Click(object sender, EventArgs e)
        {
            extProgressBarMultiSegment1.ChangeValue(1, 0);

        }

        private void extButtonSeg2_Zero_Click(object sender, EventArgs e)
        {
            extProgressBarMultiSegment1.ChangeValue(2, 0);

        }

        private void extButtonSeg0_30_Click(object sender, EventArgs e)
        {
            extProgressBarMultiSegment1.ChangeValue(0, 30);

        }

        private void extButtonSeg1_30_Click(object sender, EventArgs e)
        {
            extProgressBarMultiSegment1.ChangeValue(1, 30);

        }

        private void extButtonSeg2_30_Click(object sender, EventArgs e)
        {
            extProgressBarMultiSegment1.ChangeValue(2, 30);

        }
    }

}
