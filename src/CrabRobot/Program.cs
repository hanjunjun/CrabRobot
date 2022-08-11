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
            //UI�߳��쳣
            Application.ThreadException += Application_ThreadException;
            //���߳��쳣
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //Thread.Sleep(15000);
            //BootstrapService service = new BootstrapService();
            //service.ScreenVideoService();
            //Console.WriteLine("¼�������������");
            //Application.Run();

            if (AppArgsManager.Exist(Const.ArgsKey.Type, Const.ArgsValue.Module.ScreenVideo))
            {
                //¼������ӽ���
                BootstrapService service = new BootstrapService();
                service.ScreenVideoService();
                Console.WriteLine("¼�������������");
                Application.Run();
            }
            else
            {
                //�����ڳ���
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Process instance = ProcessUtil.RunningInstance();
                if (instance == null)
                {
                    //����¼���ӽ��̲�ע�ᵽ�ӽ��̹�������
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
        /// ���߳��쳣
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            MessageBox.Show($"�����̳߳���δ�����쳣��{ex.Message}","����",MessageBoxButtons.OK,MessageBoxIcon.Error);
            Environment.Exit(0);
        }
        /// <summary>
        /// UI�߳��쳣
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show($"����UI�̳߳���δ�����쳣��{e.Exception.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(0);
        }
    }
}
