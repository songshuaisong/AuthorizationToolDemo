using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MandateTool
{
    /****************************************************************************************************************************************
    C# 实现程序只启动一次（多次运行激活第一个实例,使其获得焦点,并设置窗口在最前端显示）
    ****************************************************************************************************************************************/
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            /* 生成一个的随机数 */
            Random ran = new Random();

            //Get the running instance.  
            Process instance = RunningInstance();
            if (instance == null)
            {
                int romdata = ran.Next(0x00, 0xFF);
                Console.WriteLine(romdata + "");
                if (romdata % 2 == 0)
                {
                    //There isn't another instance, show our form.  
                    Application.Run(new Form1());
                }
                else
                {
                    //There isn't another instance, show our form.  
                    Application.Run(new Form2());
                }
            }
            else
            {
                //There is another instance of this process.  
                HandleRunningInstance(instance);
            }

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }

        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            //Loop through the running processes in with the same name  
            foreach (Process process in processes)
            {
                //Ignore the current process  
                if (process.Id != current.Id)
                {
                    //Make sure that the process is running from the exe file.  
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") ==
                    current.MainModule.FileName)
                    {
                        //Return the other process instance.  
                        return process;
                    }
                }
            }

            //No other instance was found, return null.  
            return null;
        }

        public static void HandleRunningInstance(Process instance)
        {
            //Make sure the window is not minimized or maximized  
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);
            //Set the real intance to foreground window  
            SetForegroundWindow(instance.MainWindowHandle);
        }

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;
    }
}
