using AppTest.DM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppTest
{
    public partial class Form1 : Form
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + @"Resources";
        string zikupath = AppDomain.CurrentDomain.BaseDirectory + @"Resources\ziku.txt";
        const string displayModel = "dx.graphic.3d.10plus";
        const string keyboardModel = "dx.public.anti.api";
        const string mouseModel = "dx.mouse.position.lock.api";
        Dmsoft DM;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var processList = Process.GetProcessesByName("Wow").ToList();
            if (processList != null && processList.Count > 0)
            {
                for (int i = 0; i < processList.Count; i++)
                {
                    var task = new Thread((count) =>
                      {
                          Dmsoft dmsoft = new Dmsoft();
                          dmsoft.SetPath(AppDomain.CurrentDomain.BaseDirectory + @"Resources\");
                          var num = (int)count;
                          while (true)
                          {
                              try
                              {
                                  var shopX = 0;
                                  var shopY = 0;
                                  var process = processList[num];
                                  
                                  var hwnd = dmsoft.FindWindowByProcessId(process.Id, "", "魔兽世界");
                                  var dmbind = dmsoft.BindWindowEx(hwnd, displayModel, "dx.mouse.input.lock.api3", keyboardModel, "", 0);
                                  var fdsuiji = dmsoft.FindPic(0, 0, 2000, 2000, "xueticun.bmp", "000000", 0.6, 2, out shopX, out shopY);
                                  if (fdsuiji != -1)
                                  {
                                      dmsoft.MoveTo(1253/2, 706/2);
                                      dmsoft.RightClick();
                                      Thread.Sleep(2500);
                                      dmsoft.KeyPressChar("1");
                                  }
                              }
                              catch
                              {

                              }
                              finally
                              {
                                  Thread.Sleep(4*1000);
                              }
                          }
                      });
                    task.Start(i);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DmDynamicLoad.LoadDmDll();
            DM = new Dmsoft();
            var path = AppDomain.CurrentDomain.BaseDirectory + Process.GetCurrentProcess().ProcessName + ".exe";
            var result = DM.Reg("cx100115963871588f42fe33632fc733792e2ad125d", "kqOtu");
            if (result != 1)
            {
                throw new Exception("DM注册失败！");
            }
            result = DM.DmGuard(1, "memory2");
            if (result != 1)
            {
                throw new Exception("memory2 启动失败！");
            }
            result = DM.DmGuard(1, "hm 0 1");
            if (result != 1)
            {
                throw new Exception("hm 0 1 启动失败！");
            }
            var dun = @$"f2 <c:\windows\system32\calc.exe> <{path}>";
            result = DM.DmGuard(1, dun);
        }
    }
}
