using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatchaDoin
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]


        static void Main()
        {
            Process[] cntRunningProcesses = Process.GetProcessesByName("WhatchaDoin");
            if (cntRunningProcesses.Length>=2)
            {
                MessageBox.Show("This application is still running.");
                Environment.Exit(0);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmTodoList());
        }
    }
}
