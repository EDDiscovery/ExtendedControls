using BaseUtils;
using ExtendedControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestExtendedControls
{
    public partial class TestTranslation : Form
    {
        ThemeList theme;
        public TestTranslation()
        {
            InitializeComponent();

            theme = new ThemeList();
            theme.LoadBaseThemes();
            theme.SetThemeByName("Elite Verdana Gradiant");
            Theme.Current.WindowsFrame = true;

            extPanelGradientFill1.ThemeColorSet = 1;
            Theme.Current.ApplyStd(this);

            // this is for running in debug mode

            var exefolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string translatorfolder = Path.GetFullPath(exefolder + @"\..\..\Translator");

            //BaseUtils.Translator.Instance.LoadTranslation("example-ex",
            //        CultureInfo.CurrentUICulture,
            //        new string[] { translatorfolder },
            //        1,
            //        ".",
            //        true
            //        );

            //string warning = Translator.Instance.Translate("Warning", "Warning");

            BaseUtils.TranslatorMkII.Instance.LoadTranslation("example-ex",
                    CultureInfo.CurrentUICulture,
                    new string[] { translatorfolder },
                    1,
                    ".",
                    null,
                    true,
                    true
                    );

            string warning = TranslatorMkII.Instance.Translate("Warning");
            string cancel = TranslatorMkII.Instance.Translate("Cancel");
            string ok = TranslatorMkII.Instance.Translate("OK");
            string ok1 = "OK".Tx();


            TranslatorMkII.Instance.TranslateControls(this);
            TranslatorMkII.Instance.TranslateTooltip(toolTip1, this);
            TranslatorMkII.Instance.TranslateToolstrip(contextMenuStrip1);

        }

    }
}
