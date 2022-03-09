using HBNiuBi.DM;
using HBNiuBi.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinAuth;

namespace HBNiuBi
{
    public partial class MainForm : Form
    {
        const string path = @"C:\HanJunJun\GitHub\HBNiuBi\dist\Debug\Resources\";
        const string displayModel = "dx.graphic.3d.10plus";
        const string keyboardModel = "dx.public.anti.api";
        const string mouseModel = "dx.mouse.position.lock.api";
        dmsoft dm;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dm = new dmsoft();
            long s = dm.Reg("cx100115963871588f42fe33632fc733792e2ad125d", "kqOtu");
            var result = dm.DmGuard(1, "memory2");
            result = dm.DmGuard(1, "hm 0 1");
            var path = AppDomain.CurrentDomain.BaseDirectory + Process.GetCurrentProcess().ProcessName + ".exe";
            result = dm.DmGuard(1, @$"f2 <c:\windows\system32\calc.exe> <{path}>");
            //dm = new CDmSoft();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //dm.Dispose();
            dm = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (File.Exists("test.bmp"))
                File.Delete("test.bmp");
            dm.Capture(100, 100, 500, 500, "test.bmp");
        }
        public Authenticator AuthenticatorData { get; set; }
        int shopX, shopY, playX, playY;
        private void button4_Click(object sender, EventArgs e)
        {
            string serial = this.restoreSerialNumberField.Text.Trim();
            string restore = this.restoreRestoreCodeField.Text.Trim();
            if (serial.Length == 0 || restore.Length == 0)
            {
                MessageBox.Show(this.Owner, "请输入序列号和还原密码！");
            }

            try
            {
                BattleNetAuthenticator authenticator = new BattleNetAuthenticator();
                authenticator.Restore(serial, restore);
                this.AuthenticatorData = authenticator;
                this.dynamicCode.Text = this.AuthenticatorData.CurrentCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Owner, $"Unable to restore the authenticator:{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "MoveWindow")]
        public static extern bool MoveWindow(int hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        private void button8_Click(object sender, EventArgs e)
        {
            var result = -1;
            dm.SetPath(path);
            var hwnd = dm.FindWindow("", "魔兽世界");
            var dmbind = dm.BindWindowEx(hwnd, "dx2", "dx.mouse.input.lock.api3", keyboardModel, "", 0);
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
                SendMessage(hwnd, WM_CLOSE, 0, 0);
                Thread.Sleep(10000);
            }).Wait();
            hwnd = dm.FindWindow("", "魔兽世界");
            dmbind = dm.BindWindowEx(hwnd, "dx2", "dx.mouse.input.lock.api3", keyboardModel, "", 0);
            //移动窗口
            var results = MoveWindow(hwnd, 0, 0, 1253, 706, true);
            //result = dm.MoveWindow(hwnd, 0, 0);
            Debug.WriteLine($"MoveWindow:{results}");
            result = dm.SetWindowSize(hwnd, 1253, 706);
            Debug.WriteLine($"SetWindowSize:{result}");
            result = dm.MoveTo(623, 605);
            Debug.WriteLine($"MoveTo:{result}");
            result = dm.LeftDoubleClick();
            Debug.WriteLine($"LeftDoubleClick:{result}");
            Thread.Sleep(5000);
            result = dm.MoveTo(619, 504);
            Debug.WriteLine($"MoveTo:{result}");
            result = dm.LeftDoubleClick();
            Debug.WriteLine($"LeftDoubleClick:{result}");
            Thread.Sleep(5000);
        }
        const int WM_CLOSE = 0x0010;
        [DllImport("User32.dll", EntryPoint = "SendMessage")]

        private static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);
        private void button9_Click(object sender, EventArgs e)
        {
            dm.SetPath(path);
            var hwnd = dm.FindWindow("", "魔兽世界");
            var dmbind = dm.BindWindowEx(hwnd, "dx2", "dx.mouse.input.lock.api3", keyboardModel, "", 0);
            var fdsuiji = dm.FindPic(0, 0, 2000, 2000, "login2.bmp", "000000", 0.4, 0, out shopX, out shopY);
            //dm.MoveTo(shopX + 5, shopY + 5);
            dm.MoveTo(619, 504);
            dm.LeftDoubleClick();
            Thread.Sleep(3000);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dm.SetPath(path);
            var hwnd = dm.FindWindow("", "魔兽世界");
            SendMessage(hwnd, WM_CLOSE, 0, 0);
            var dmbind = dm.BindWindowEx(hwnd, "dx2", "dx.mouse.input.lock.api3", keyboardModel, "", 0);
            var fdsuiji = dm.FindPic(0, 0, 2000, 2000, "login2.bmp", "000000", 0.4, 0, out shopX, out shopY);
            //dm.MoveTo(shopX + 5, shopY + 5);
            dm.MoveTo(370, 212);
            dm.LeftDoubleClick();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            dm.SetPath(path);
            var hwnd = dm.FindWindow("", "魔兽世界");
            var result = dm.MoveTo(623, 605);
            Debug.WriteLine($"MoveTo:{result}");
            result = dm.LeftDoubleClick();
            Debug.WriteLine($"LeftDoubleClick:{result}");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var email = txtEmailNumber.Text;
            var password = txtPassword.Text;
            var dynamicCode = this.AuthenticatorData.CurrentCode;
            var result = -1;
            dm.SetPath(path);
            var hwnd = dm.FindWindow("", "魔兽世界");
            var dmbind = dm.BindWindowEx(hwnd, "dx2", "dx.mouse.input.lock.api3", keyboardModel, "", 0);
            Task.Factory.StartNew(() =>
            {
                //账号
                var result = dm.MoveTo(552, 349);
                Debug.WriteLine($"MoveTo:{result}");
                result = dm.LeftDoubleClick();
                Debug.WriteLine($"LeftDoubleClick:{result}");
                result = dm.KeyPressStr(email, 40);
                //密码
                //result = dm.MoveTo(552, 349);
                result = dm.MoveTo(552, 413);
                Debug.WriteLine($"MoveTo:{result}");
                result = dm.LeftDoubleClick();
                Debug.WriteLine($"LeftDoubleClick:{result}");
                result = dm.KeyPressStr(password, 40);
                //确定
                result = dm.MoveTo(552, 497);
                Debug.WriteLine($"MoveTo:{result}");
                result = dm.LeftDoubleClick();
                Debug.WriteLine($"LeftDoubleClick:{result}");
                Thread.Sleep(2000);
                //输入动态码
                result = dm.MoveTo(576, 350);
                Debug.WriteLine($"MoveTo:{result}");
                result = dm.LeftDoubleClick();
                Debug.WriteLine($"LeftDoubleClick:{result}");
                result = dm.KeyPressStr(dynamicCode, 100);
                Thread.Sleep(2000);
                //选wow1
                result = dm.MoveTo(541, 442);
                Debug.WriteLine($"MoveTo:{result}");
                result = dm.LeftDoubleClick();
                Debug.WriteLine($"LeftDoubleClick:{result}");
                Thread.Sleep(5000);
                //进入wow
                result = dm.KeyPressChar("enter");
            }).Wait();
        }

        private void keyMonitor_KeyDown(object sender, KeyEventArgs e)
        {
            //48,57,96,105
            Keys keys = e.Modifiers;
            if (keys.ToString() != "None")
                //keyMonitor.Text = keys.ToString() + "+" + e.KeyCode.ToString();
                keyMonitor.Text = keys.ToString();
            else
            {
                if (e.KeyValue >= 48 && e.KeyValue <= 57)
                {
                    keyMonitor.Text = e.KeyCode.ToString().Replace("D", "");
                }
                else if (e.KeyValue >= 96 && e.KeyValue <= 105)
                {
                    keyMonitor.Text = e.KeyCode.ToString().Replace("NumPad", "");
                }
                else
                {
                    keyMonitor.Text = e.KeyCode.ToString();
                }
            }
            //var key = e.KeyCode.ToString();
            //keyMonitor.Text = key;
        }
        int hwnd = -1;
        private void button13_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                //启动游戏
                var process = System.Diagnostics.Process.Start(wowGamePath.Text);
                Thread.Sleep(5000);
                txtPid.SetTextBox(() =>
                {
                    txtPid.Text = process.Id.ToString();
                });
                //生成动态令牌
                string serial = this.restoreSerialNumberField.Text.Trim();
                string restore = this.restoreRestoreCodeField.Text.Trim();
                if (serial.Length == 0 || restore.Length == 0)
                {
                    MessageBox.Show(this.Owner, "请输入序列号和还原密码！");
                }

                try
                {
                    BattleNetAuthenticator authenticator = new BattleNetAuthenticator();
                    authenticator.Restore(serial, restore);
                    this.AuthenticatorData = authenticator;
                    this.dynamicCode.SetTextBox(() =>
                    {
                        this.dynamicCode.Text = this.AuthenticatorData.CurrentCode;
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this.Owner, $"Unable to restore the authenticator:{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //显卡兼容问题会弹窗
                var result = -1;
                dm.SetPath(path);
                hwnd = dm.FindWindow("", "魔兽世界");
                var dmbind = dm.BindWindowEx(hwnd, "gdi", "dx.mouse.position.lock.api", "dx.keypad.raw.input", "", 0);
                Task.Factory.StartNew(() =>
                {
                    var i = 0;
                    while (true)
                    {
                        if (i >= 10)
                        {
                            Debug.WriteLine("没有找到显卡兼容弹窗！");
                            return;
                        }
                        var fdsuiji = dm.FindPic(0, 0, 2000, 2000, "login0.bmp", "000000", 0.4, 0, out shopX, out shopY);
                        if (fdsuiji != -1)
                        {
                            //dm.MoveTo(shopX + 2, shopY + 2);
                            //dm.LeftDoubleClick();
                            //var dmbind = dm.BindWindowEx(hwnd, "gdi2", "normal", keyboardModel, "", 0);
                            int childHwnd = FindWindowEx(hwnd, 0, null, "OK");//按钮控件标题
                            SendMessage(childHwnd, BM_CLICK, 0, 0);
                            return;
                        }
                        i++;
                        Thread.Sleep(1000);
                    }
                    //Thread.Sleep(5000);
                    //SendMessage(hwnd, WM_CLOSE, 0, 0);
                    //Thread.Sleep(5000);
                }).Wait();
                Thread.Sleep(8000);
                //句柄变更需要重新获取
                hwnd = dm.FindWindow("", "魔兽世界");
                dmbind = dm.BindWindowEx(hwnd, displayModel, "dx.mouse.input.lock.api3", keyboardModel, "", 0);
                //移动窗口
                var results = MoveWindow(hwnd, 0, 0, 1253, 706, true);
                //result = dm.MoveWindow(hwnd, 0, 0);
                Debug.WriteLine($"MoveWindow:{results}");
                //点协议
                result = dm.SetWindowSize(hwnd, 1253, 706);
                Debug.WriteLine($"SetWindowSize:{result}");
                result = dm.MoveTo(623, 605);
                Debug.WriteLine($"MoveTo:{result}");
                result = dm.LeftDoubleClick();
                Debug.WriteLine($"LeftDoubleClick:{result}");
                Thread.Sleep(1000);
                //输入账号密码等
                //输入账号密码等
                var email = txtEmailNumber.Text;
                var password = txtPassword.Text;
                var dynamicCode = this.AuthenticatorData.CurrentCode;
                Task.Factory.StartNew(() =>
                {
                    //账号
                    var result = dm.MoveTo(552, 349);
                    Debug.WriteLine($"MoveTo:{result}");
                    result = dm.LeftDoubleClick();
                    Debug.WriteLine($"LeftDoubleClick:{result}");
                    result = dm.KeyPressStr(email, 40);
                    Debug.WriteLine($"输入账号：{email}");
                    //密码
                    //result = dm.MoveTo(552, 349);
                    result = dm.MoveTo(552, 413);
                    Debug.WriteLine($"MoveTo:{result}");
                    result = dm.LeftDoubleClick();
                    Debug.WriteLine($"LeftDoubleClick:{result}");
                    result = dm.KeyPressStr(password, 40);
                    Debug.WriteLine($"输入账号：{password}");
                    //确定
                    result = dm.MoveTo(552, 497);
                    Debug.WriteLine($"MoveTo:{result}");
                    result = dm.LeftDoubleClick();
                    Debug.WriteLine($"LeftDoubleClick:{result}");
                    Debug.WriteLine($"确定");
                    Thread.Sleep(2000);
                    //输入动态码
                    result = dm.MoveTo(576, 350);
                    Debug.WriteLine($"MoveTo:{result}");
                    result = dm.LeftDoubleClick();
                    Debug.WriteLine($"LeftDoubleClick:{result}");
                    result = dm.KeyPressStr(dynamicCode, 100);
                    result = dm.MoveTo(518, 396);
                    Debug.WriteLine($"MoveTo:{result}");
                    result = dm.LeftDoubleClick();
                    Debug.WriteLine($"LeftDoubleClick:{result}");
                    Debug.WriteLine($"输入动态码：{dynamicCode}");
                    Thread.Sleep(2000);
                    //选wow1
                    //result = dm.MoveTo(535, 311);
                    //wow8测试
                    result = dm.MoveTo(539, 345);
                    Debug.WriteLine($"MoveTo:{result}");
                    result = dm.LeftDoubleClick();
                    Debug.WriteLine($"LeftDoubleClick:{result}");
                    Debug.WriteLine($"选取wow8");
                    Thread.Sleep(1000);
                    //同意
                    result = dm.MoveTo(541, 442);
                    Debug.WriteLine($"MoveTo:{result}");
                    result = dm.LeftDoubleClick();
                    Debug.WriteLine($"LeftDoubleClick:{result}");
                    Thread.Sleep(3000);
                    //进入wow
                    result = dm.KeyPressChar("enter");
                    Debug.WriteLine($"进入wow世界");
                }).Wait();
                //循环执行脚本
                var scriptTask = new Task((obj) =>
                {
                    while (true)
                    {
                        result = dm.KeyPressChar("f11");
                        Debug.WriteLine("按下了F11");
                        Thread.Sleep(4000);
                        result = dm.KeyPressChar("f12");
                        Debug.WriteLine("按下了F12");
                        Thread.Sleep(2000);
                    }
                }, null, TaskCreationOptions.LongRunning);
                scriptTask.Start();
                //角色检测是否掉线
                var downMonitorTask = new Task((obj) =>
                  {
                      while (true)
                      {
                          //var dmbind = dm.BindWindowEx(hwnd, displayModel, "dx.mouse.input.lock.api3", keyboardModel, "", 0);
                          var fdsuiji = dm.FindPic(0, 0, 2000, 2000, "wenhao.bmp", "000000", 0.5, 0, out shopX, out shopY);
                          if (fdsuiji != -1)
                          {
                              Debug.WriteLine("正常游戏中....");
                              Thread.Sleep(10000);
                              continue;
                          }
                          fdsuiji = dm.FindPic(0, 0, 2000, 2000, "chonglian.bmp", "000000", 0.5, 0, out shopX, out shopY);
                          if (fdsuiji == 0)
                          {
                              //有重连按钮
                              Debug.WriteLine("检测到游戏掉线....");
                              //var move = dm.MoveTo(shopX + 6, shopY + 6);
                              var move = dm.MoveTo(588, 343);
                              var click = dm.LeftDoubleClick();
                              Thread.Sleep(1500);
                              move = dm.MoveTo(601, 386);
                              click = dm.LeftDoubleClick();
                              Debug.WriteLine("正在操作重连中....");
                              Thread.Sleep(6000);
                              //判断有没有魔兽世界图标
                              fdsuiji = dm.FindPic(0, 0, 2000, 2000, "loginSelect.bmp", "000000", 0.5, 0, out shopX, out shopY);
                              if (fdsuiji == -1)
                              {
                                  Debug.WriteLine("重连失败....");
                                  //发邮件通知
                                  return;
                              }
                              fdsuiji = dm.FindPic(0, 0, 2000, 2000, "jinruwow.bmp", "000000", 0.5, 0, out shopX, out shopY);
                              if (fdsuiji == -1)
                              {
                                  Debug.WriteLine("进入魔兽世界失败....");
                                  //发邮件通知
                                  return;
                              }
                              move = dm.MoveTo(shopX + 2, shopY + 2);
                              click = dm.LeftDoubleClick();
                              Debug.WriteLine("进入游戏世界成功....");
                              continue;
                          }
                          //如果是退到首页
                          fdsuiji = dm.FindPic(0, 0, 2000, 2000, "shilingtishi.bmp", "000000", 0.5, 0, out shopX, out shopY);
                          if (fdsuiji == 0)
                          {
                              var move = dm.MoveTo(609, 344);
                              var click = dm.LeftDoubleClick();
                              Debug.WriteLine("退到首页准备重登....");
                              Thread.Sleep(500);
                              move = dm.MoveTo(601, 599);
                              click = dm.LeftDoubleClick();
                              //输入账号密码等
                              var email = txtEmailNumber.Text;
                              var password = txtPassword.Text;
                              var dynamicCode = this.AuthenticatorData.CurrentCode;
                              Task.Factory.StartNew(() =>
                              {
                                  //账号
                                  var result = dm.MoveTo(552, 349);
                                  Debug.WriteLine($"MoveTo:{result}");
                                  result = dm.LeftDoubleClick();
                                  Debug.WriteLine($"LeftDoubleClick:{result}");
                                  result = dm.KeyPressStr(email, 40);
                                  Debug.WriteLine($"输入账号：{email}");
                                  //密码
                                  //result = dm.MoveTo(552, 349);
                                  result = dm.MoveTo(552, 413);
                                  Debug.WriteLine($"MoveTo:{result}");
                                  result = dm.LeftDoubleClick();
                                  Debug.WriteLine($"LeftDoubleClick:{result}");
                                  result = dm.KeyPressStr(password, 40);
                                  Debug.WriteLine($"输入账号：{password}");
                                  //确定
                                  result = dm.MoveTo(552, 497);
                                  Debug.WriteLine($"MoveTo:{result}");
                                  result = dm.LeftDoubleClick();
                                  Debug.WriteLine($"LeftDoubleClick:{result}");
                                  Debug.WriteLine($"确定");
                                  Thread.Sleep(2000);
                                  //输入动态码
                                  result = dm.MoveTo(576, 350);
                                  Debug.WriteLine($"MoveTo:{result}");
                                  result = dm.LeftDoubleClick();
                                  Debug.WriteLine($"LeftDoubleClick:{result}");
                                  result = dm.KeyPressStr(dynamicCode, 100);
                                  result = dm.MoveTo(518, 396);
                                  Debug.WriteLine($"MoveTo:{result}");
                                  result = dm.LeftDoubleClick();
                                  Debug.WriteLine($"LeftDoubleClick:{result}");
                                  Debug.WriteLine($"输入动态码：{dynamicCode}");
                                  Thread.Sleep(2000);
                                  //选wow1
                                  //result = dm.MoveTo(535, 311);
                                  //wow8测试
                                  result = dm.MoveTo(539, 345);
                                  Debug.WriteLine($"MoveTo:{result}");
                                  result = dm.LeftDoubleClick();
                                  Debug.WriteLine($"LeftDoubleClick:{result}");
                                  Debug.WriteLine($"选取wow8");
                                  Thread.Sleep(1000);
                                  //同意
                                  result = dm.MoveTo(541, 442);
                                  Debug.WriteLine($"MoveTo:{result}");
                                  result = dm.LeftDoubleClick();
                                  Debug.WriteLine($"LeftDoubleClick:{result}");
                                  Thread.Sleep(3000);
                                  //进入wow
                                  result = dm.KeyPressChar("enter");
                                  Debug.WriteLine($"进入wow世界");
                              }).Wait();
                          }
                      }
                  }, null, TaskCreationOptions.LongRunning);
                downMonitorTask.Start();
            });
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    dm.SetPath(path);
                    var hwnd = dm.FindWindow("", "魔兽世界");
                    var dmbind = dm.BindWindowEx(hwnd, "dx.graphic.3d.10plus", "dx.mouse.input.lock.api3", keyboardModel, "", 0);
                    var fdsuiji = dm.FindPic(0, 0, 2000, 2000, "chonglian.bmp", "000000", 0.5, 0, out shopX, out shopY);
                    if (fdsuiji == -1)
                    {
                        Debug.WriteLine("正常游戏中....");
                        Thread.Sleep(10000);
                        continue;
                    }
                    Debug.WriteLine("检测到游戏掉线....");
                    //var move = dm.MoveTo(shopX + 6, shopY + 6);
                    var move = dm.MoveTo(588, 343);
                    var click = dm.LeftDoubleClick();
                    Thread.Sleep(1500);
                    move = dm.MoveTo(601, 386);
                    click = dm.LeftDoubleClick();
                    Debug.WriteLine("正在操作重连中....");
                    Thread.Sleep(6000);
                    //判断有没有魔兽世界图标
                    fdsuiji = dm.FindPic(0, 0, 2000, 2000, "loginSelect.bmp", "000000", 0.5, 0, out shopX, out shopY);
                    if (fdsuiji == -1)
                    {
                        Debug.WriteLine("重连失败....");
                        //发邮件通知
                        return;
                    }
                    fdsuiji = dm.FindPic(0, 0, 2000, 2000, "jinruwow.bmp", "000000", 0.5, 0, out shopX, out shopY);
                    if (fdsuiji == -1)
                    {
                        Debug.WriteLine("进入魔兽世界失败....");
                        //发邮件通知
                        return;
                    }
                    move = dm.MoveTo(shopX + 2, shopY + 2);
                    click = dm.LeftDoubleClick();
                    Debug.WriteLine("进入游戏世界成功....");
                }
            }).Wait();
        }
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        private extern static int FindWindowEx(int hwndParent, int hwndChildAfter, string lpszClass, string lpszWindow);
        private const int BM_CLICK = 0xF5;//点击
        private void button15_Click(object sender, EventArgs e)
        {
            var result = -1;
            dm.SetPath(path);
            var hwnd = dm.FindWindow("", "魔兽世界");
            //var dmbind = dm.BindWindowEx(hwnd, "gdi2", "normal", keyboardModel, "", 0);
            int childHwnd = FindWindowEx(hwnd, 0, null, "OK");//按钮控件标题
            SendMessage(childHwnd, BM_CLICK, 0, 0);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            var result = -1;
            dm.SetPath(path);
            var hwnd = dm.FindWindow("", "魔兽世界");
            var dmbind = dm.BindWindowEx(hwnd, displayModel, "dx.mouse.position.lock.api", "dx.keypad.raw.input", "", 0);
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    result = dm.KeyPressChar("f11");
                    Debug.WriteLine("按下了F11");
                    Thread.Sleep(1000);
                    result = dm.KeyPressChar("f12");
                    Debug.WriteLine("按下了F12");
                    Thread.Sleep(2000);
                }
            }).Wait();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.dynamicCode.Text = this.AuthenticatorData.CurrentCode;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var process = System.Diagnostics.Process.Start(wowGamePath.Text);
            txtPid.Text = process.Id.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dm.SetPath(path);
            var hwnd = dm.FindWindow("", "魔兽世界");
            var dmbind = dm.BindWindowEx(hwnd, "dx2", "dx.mouse.input.lock.api3", keyboardModel, "", 0);
            var fdsuiji = dm.FindPic(0, 0, 2000, 2000, "login1.bmp", "000000", 0.4, 0, out shopX, out shopY);
            //var move = dm.MoveTo(shopX + 6, shopY + 6);
            var move = dm.MoveTo(623, 605);
            var click = dm.LeftDoubleClick();
        }
    }
}
