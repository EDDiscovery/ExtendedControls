﻿using AudioExtensions;
using BaseUtils;
using ExtendedAudioForms;
using ExtendedConditionsForms;
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

namespace DialogTest
{
    public partial class TestExtendedForms : Form
    {
        ThemeStandard theme;

        public TestExtendedForms()
        {
            InitializeComponent();
            theme = new ThemeStandard();
            ThemeableFormsInstance.Instance = theme;
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite EuroCaps");
            theme.FontSize = 12;
        }


        // icon list box

        private void extButton1_Click(object sender, EventArgs e)
        {
            theme.FontSize = 12;
            AudioDriverCSCore ad = new AudioDriverCSCore();
            AudioDeviceConfigure f = new AudioDeviceConfigure();
            f.Init("Config check", ad);
            f.ShowDialog(this);
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            theme.FontSize = 20;
            AudioDriverCSCore ad = new AudioDriverCSCore();
            AudioDeviceConfigure f = new AudioDeviceConfigure();
            f.Init("Config check", ad);
            f.ShowDialog(this);
        }

        private void extButton4_Click(object sender, EventArgs e)
        {
            SC(12,true);
        }

        private void SC(float size, bool mode)
        { 
            theme.FontSize = size;
            AudioDriverCSCore ad = new AudioDriverCSCore();
            AudioQueue q = new AudioQueue(ad);
            WindowsSpeechEngine wse = new WindowsSpeechEngine();
            SpeechSynthesizer ss = new SpeechSynthesizer(wse);

            SpeechConfigure c = new SpeechConfigure();

            Variables ef = new Variables();

            c.Init(q, ss, "Check SC", "Caption title", this.Icon, mode ? "Text to do" : null,
                    true, true, AudioQueue.Priority.High,
                    "sn", "en", "Sheila", "100", "Default", ef);

            c.ShowDialog(this);

        }

        private void extButton5_Click(object sender, EventArgs e)
        {
            SC(20,false);
        }

        private void extButton3_Click(object sender, EventArgs e)
        {
            theme.WindowsFrame = true;
        }

        private void extButton6_Click(object sender, EventArgs e)
        {
            WC(12);

        }

        private void WC(float size)
        {
            theme.FontSize = size;
            AudioDriverCSCore ad = new AudioDriverCSCore();
            AudioQueue q = new AudioQueue(ad);

            WaveConfigureDialog c = new WaveConfigureDialog();

            Variables ef = new Variables();

            c.Init(q, false, "Check SC", "Caption title", this.Icon,
                    @"c:\",
                    true, AudioQueue.Priority.High,
                    "sn", "en", "100", ef);

            c.ShowDialog(this);

        }

        private void extButton7_Click(object sender, EventArgs e)
        {
            WC(20);
        }

        private void extButton8_Click(object sender, EventArgs e)
        {
            VAR(12,true);
        }

        private void VAR(float s, bool pop, bool ops = false)
        {
            theme.FontSize = s;
            VariablesForm f = new VariablesForm();

            Variables v = new Variables();
            if (pop)
            {
                v["Fred"] = "F1";
                v["George"] = "F1";
            }
            f.Init("Var test", this.Icon, v, showrunatrefreshcheckbox:true, allowadd:ops, allownoexpand:ops);
            f.ShowDialog();
        }

        private void extButton9_Click(object sender, EventArgs e)
        {
            VAR(20, true);

        }

        private void extButton10_Click(object sender, EventArgs e)
        {
            VAR(20, false);
        }

        private void extButton11_Click(object sender, EventArgs e)
        {
            VAR(24, true, true);

        }
    }
}
