using BaseUtils;
using BaseUtils.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestExtendedControls
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

            NativeMethods.STARTUPINFO_I si2 = new NativeMethods.STARTUPINFO_I();
            UnsafeNativeMethods.GetStartupInfo(si2);

            CommandArgs args = new CommandArgs(stringargs);
            string arg1 = args.Next();
            Type t = Type.GetType("TestExtendedControls." + arg1);

            if (t != null)
            {
                Application.Run((Form)Activator.CreateInstance(t));
            }
            else
                Application.Run(new TestButtons());
        }

    }
}
