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
        }

    }
}
