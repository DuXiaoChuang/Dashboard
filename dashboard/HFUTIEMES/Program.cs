using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Threading;

namespace HFUTIEMES
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Thread.Sleep(20000);
            //Application.Run(new LoginForm());
            //if (SystemLog.InOrOut == 1)
                Application.Run(new MainForm());
                Application.Exit();
            //else
            //    return;
        }
    }
}
