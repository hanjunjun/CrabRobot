using HBNiuBi.Native;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBNiuBi.Util
{
    public static class ProcessUtil
    {
        public static void ShowOrHideConsole(IntPtr hwnd, uint showModel)
        {
            if (hwnd != IntPtr.Zero)
            {
                WinApi.ShowWindow(hwnd, showModel); // 0 = SW_HIDE
            }
        }
        /// <summary>
        /// 显示进程并激活窗口
        /// </summary>
        /// <param name="handle"></param>
        public static void HandleRunningInstance(IntPtr handle)
        {
            WinApi.ShowWindow(handle, 1); //显示窗体
            WinApi.SwitchToThisWindow(handle, true); //切换到窗体
        }
        /// <summary>
        /// 显示进程
        /// </summary>
        /// <param name="handle"></param>
        public static void ShowSameExeOtherWindow(IntPtr handle)
        {
            WinApi.ShowWindow(handle, 1);
        }
        /// <summary> 
        /// 获取同名进程，其他实例
        /// </summary> 
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //Logger.Info($"current.ProcessName={current.ProcessName}", new string[] { "log" });
            foreach (Process otherProcess in processes)
            {
                if (otherProcess.Id != current.Id)
                {
                    //当前进程
                    var currentProcessName = Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath);
                    var currentProcessDirectoryName = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                    //Logger.Info($"currentProcessName={currentProcessName} currentProcessDirectoryName={currentProcessDirectoryName}", new string[] { "log" });
                    //其他同名进程
                    var otherProcessPath = GetMainModuleFileName(otherProcess);
                    var otherProcessName = Path.GetFileNameWithoutExtension(otherProcessPath);
                    var otherProcessDirectoryName = Path.GetDirectoryName(otherProcessPath);
                    //Logger.Info($"otherProcessName={otherProcessName} otherProcessDirectoryName={otherProcessDirectoryName}", new string[] { "log" });
                    if (currentProcessName == otherProcessName && currentProcessDirectoryName == otherProcessDirectoryName)
                    {
                        return otherProcess;
                    }
                }
            }
            return null;
        }
        public static string GetMainModuleFileName(this Process process, int buffer = 1024)
        {
            var fileNameBuilder = new StringBuilder(buffer);
            uint bufferLength = (uint)fileNameBuilder.Capacity + 1;
            return WinApi.QueryFullProcessImageName(process.Handle, 0, fileNameBuilder, ref bufferLength) ?
                fileNameBuilder.ToString() :
                null;
        }
    }
}
