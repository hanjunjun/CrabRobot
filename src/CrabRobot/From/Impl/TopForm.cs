using CrabRobot.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrabRobot.From.Impl
{
    public class TopForm : CrabRobotBaseForm
    {
        public TopForm()
        {
            Load += (send, args) =>
            {
#if DEBUG
#else
                this.SetFormTop(this.Handle);
#endif
            };
        }
        /// <summary>
        /// 设置窗体最前端
        /// </summary>
        /// <param name="hWnd"></param>
        protected void SetFormTop(IntPtr hWnd)
        {
            Rectangle rect = new Rectangle();
            WinApi.GetWindowRect(hWnd, ref rect);
            WinApi.SetWindowPos(hWnd, (IntPtr)WinApi.HWND_TOPMOST, rect.Left, rect.Top, Width, Height, 0);
        }

        /// <summary>
        /// 取消窗体最前端
        /// </summary>
        /// <param name="hWnd"></param>
        protected void SetFormNoTop(IntPtr hWnd)
        {
            Rectangle rect = new Rectangle();
            WinApi.GetWindowRect(hWnd, ref rect);
            WinApi.SetWindowPos(hWnd, (IntPtr)WinApi.HWND_NOTOPMOST, 0, 0, 0, 0, WinApi.SWP_NOMOVE | WinApi.SWP_NOSIZE);
        }
    }
}
