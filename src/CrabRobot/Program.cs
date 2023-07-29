using CrabRobot.Config;
using CrabRobot.DM;
using CrabRobot.Util;
using CrabRobot.Video;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrabRobot
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //UI线程异常
            Application.ThreadException += Application_ThreadException;
            //多线程异常
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //Thread.Sleep(15000);
            //BootstrapService service = new BootstrapService();
            //service.ScreenVideoService();
            //Console.WriteLine("录像服务已启动！");
            //Application.Run();

            if (AppArgsManager.Exist(Const.ArgsKey.Type, Const.ArgsValue.Module.ScreenVideo))
            {
                //录像服务子进程
                BootstrapService service = new BootstrapService();
                service.ScreenVideoService();
                Console.WriteLine("录像服务已启动！");
                Application.Run();
            }
            else
            {
                //主窗口程序
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Process instance = ProcessUtil.RunningInstance();
                if (instance == null)
                {
                    //启动录像子进程并注册到子进程管理器中
                    var path = AppDomain.CurrentDomain.BaseDirectory + @$"{Process.GetCurrentProcess().ProcessName}.exe";
                    var arguments = $"{Const.ArgsKey.Type}={Const.ArgsValue.Module.ScreenVideo}";
                    SubProcessRegister.GetInstance().StartProcess(path, arguments);
                    Application.Run(new MainForm());
                }
                else
                {
                    ProcessUtil.HandleRunningInstance(instance.MainWindowHandle);
                    Environment.Exit(0);
                }
            }
        }
        /// <summary>
        /// 多线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            MessageBox.Show($"程序线程出现未处理异常：{ex.Message}","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            Environment.Exit(0);
        }
        /// <summary>
        /// UI线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show($"程序UI线程出现未处理异常：{e.Exception.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(0);
        }
    }
}
