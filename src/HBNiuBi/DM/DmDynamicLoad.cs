using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HBNiuBi.DM
{
    public class DmDynamicLoad
    {
        [DllImport("DmReg.dll", SetLastError = true)]
        public static extern long SetDllPathA(string path, long mode);

        /// <summary>
        /// 免安装调用
        /// </summary>
        public static void LoadDmDll()
        {
            SetDllPathA(AppDomain.CurrentDomain.BaseDirectory + "dm.dll", 0);
        }
    }
}
