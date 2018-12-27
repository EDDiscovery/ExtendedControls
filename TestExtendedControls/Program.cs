using BaseUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string [] stringargs)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CommandArgs args = new CommandArgs(stringargs);
            string arg1 = args.Next();
            Type t = Type.GetType("DialogTest." + arg1);

            if (t != null)
            {
                Application.Run((Form)Activator.CreateInstance(t));
            }
            else
            {
                Form sel = null;

                switch (arg1)
                {
                    case "keyform":
                        ExtendedControls.KeyForm f = new ExtendedControls.KeyForm();
                        f.Init(null, true, " ", "", "", -1, false);
                        sel = f;
                        break;

                    case "infoform":
                        ExtendedControls.ThemeStandard th = new ExtendedControls.ThemeStandard();
                        th.LoadBaseThemes();
                        th.SetThemeByName("Elite Verdana");
                        ExtendedControls.ThemeableFormsInstance.Instance = th;
                        ExtendedControls.InfoForm inf = new ExtendedControls.InfoForm();
                        inf.Info("Info form", Properties.Resources._3x3_grid, "This is a nice test\r\nOf the info form\r\n", new int[] { 0, 100, 200, 300, 400, 500, 600 });
                        sel = inf;
                        break;
                }

                Application.Run(sel);
            }
        }

    }
}
