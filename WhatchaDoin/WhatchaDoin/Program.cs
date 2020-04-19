using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatchaDoin
{


    static class Program
    {
        private const int SW_SHOWMAXIMIZED = 3;

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static bool isProcessExisted()
        {
            int cnt = 0;
            Process[] processRunning = Process.GetProcesses();
            foreach (Process proc in processRunning)
            {
                if (proc.ProcessName == "WhatchaDoin")
                {
                    cnt++;
                    if (cnt == 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void OpenNewProcessIfExisted()
        {
            Process[] processRunning = Process.GetProcesses();
            if (isProcessExisted())
            {
                foreach (Process proc in processRunning)
                {
                    if (proc.ProcessName == "WhatchaDoin" && (proc.Id != Process.GetCurrentProcess().Id))
                    {
                        proc.Kill();
                    }
                }
            }
        }


        static void Main()
        {
            OpenNewProcessIfExisted();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmTodoList());
        }
    }
}
