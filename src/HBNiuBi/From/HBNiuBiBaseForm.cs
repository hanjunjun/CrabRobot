using HBNiuBi.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HBNiuBi.From
{
    public class HBNiuBiBaseForm : Form
    {
        public HBNiuBiBaseForm()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += (send, args) =>
            {
                this.SetFormTop(this.Handle);
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
            //WinApi.SetWindowPos(hWnd, (IntPtr)WinApi.HWND_TOPMOST, rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top, 0);
            WinApi.SetWindowPos(hWnd, (IntPtr)WinApi.HWND_TOPMOST, rect.Left, rect.Top, this.Width, this.Height, 0);
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
