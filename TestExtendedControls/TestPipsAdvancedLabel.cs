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
    public partial class TestPipsAdvancedLabel : Form
    {

        ThemeList theme;

        public TestPipsAdvancedLabel()
        {
            InitializeComponent();
            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana");
            Theme.Current.WindowsFrame = true;

            multiPipControl2.Font = multiPipControl3.Font = multiPipControl1.Font = new Font("Euro Caps", 12F);
            multiPipControl1.Add(multiPipControl2);
            multiPipControl1.Add(multiPipControl3);
            multiPipControl2.Add(multiPipControl1);
            multiPipControl2.Add(multiPipControl3);
            multiPipControl3.Add(multiPipControl1);
            multiPipControl3.Add(multiPipControl2);

            Theme.Current.ApplyStd(this);

            labelData1.Font = new Font("Arial", 10.5f, FontStyle.Italic);
            labelData1.DataFont = new Font("Arial", 10.5f, FontStyle.Regular);
            labelData1.BorderColor = labelData1.ForeColor.MultiplyBrightness(0.4f);
            labelData1.BoxStyle = LabelData.DataBoxStyle.Underline;
            labelData1.TabSpacingData = 150;
            labelData1.Text = "Raw {0.0} Kin {0.#}+% ={0.#} Thm {0.#}+% ={0.#} end text";
            labelData1.Data = new object[] { 10.2, 20, 30.4, 40.2, 50.2, 60.2 };

            labelData2.Font = new Font("Arial", 10.5f, FontStyle.Italic);
            labelData2.DataFont = new Font("Arial", 10.5f, FontStyle.Regular);
            labelData2.BorderColor = labelData1.ForeColor.MultiplyBrightness(0.4f);
            labelData2.TabSpacingData = 150;
            labelData2.Text = "Raw {0.0} Kin {0.#}+% ={0.#} Thm {0.#}+% ={0.#} Exp {0.#}+% ={0.#} end text";
            labelData2.Data = new object[] { 10.2, 20, 30.4, 40.2, 50.2, 60.2, 70.2 };
        }


    }

}
