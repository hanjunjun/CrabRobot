using HBNiuBi.Config;
using HBNiuBi.Model;
using HBNiuBi.Native;
using HBNiuBi.ScriptTask.Impl;
using HBNiuBi.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinAuth;
using static HBNiuBi.Const;

namespace HBNiuBi.ScriptTask
{
    public class ScriptTaskManager : IDisposable
    {
        private bool destroy = false;
        /// <summary>
        /// 当前状态
        /// </summary>
        private IScriptTaskState currentState;
        /// <summary>
        /// 未开始状态
        /// </summary>
        private IScriptTaskState notStartedState;
        /// <summary>
        /// 运行中状态
        /// </summary>
        private IScriptTaskState runningState;
        /// <summary>
        /// 停止状态
        /// </summary>
        private IScriptTaskState stoppedState;
        /// <summary>
        /// 暂停状态
        /// </summary>
        private IScriptTaskState suspendState;
        const string displayModel = "dx.graphic.3d.10plus";
        const string keyboardModel = "dx.public.anti.api";
        const string mouseModel = "dx.mouse.position.lock.api";
        /// <summary>
        /// 游戏进程对象
        /// </summary>
        private Process gameProcess;
        private int hwnd;
        private int pid;
        /// <summary>
        /// 脚本信息
        /// </summary>
        public ScriptItemModel scriptItemModel;
        /// <summary>
        /// dm对象
        /// </summary>
        public Dmsoft DM = new Dmsoft();
        /// <summary>
        /// 脚本动作
        /// </summary>
        private Action<Dmsoft, Action<string, Color?>> scriptAction;
        /// <summary>
        /// 注册wow动态码自动刷新服务 7秒一次
        /// </summary>
        private Task registerWowDynamicCodeTask;
        /// <summary>
        /// 注册脚本执行任务
        /// </summary>
        public Task RegisterExecuteScriptTask;
        /// <summary>
        /// 注册进程死亡监控，拉起进程，让自动登录脚本去执行
        /// </summary>
        public Task RegisterMonitorGameDieTask;
        public Task RegisterGameStateTask;
        /// <summary>
        /// 进程是否存活的线程信号
        /// </summary>
        private readonly ManualResetEvent gameDiePause = new ManualResetEvent(false);
        /// <summary>
        /// 游戏是否开始信号
        /// </summary>
        private readonly ManualResetEvent gameStartWait = new ManualResetEvent(false);
        /// <summary>
        /// 动态码生成器
        /// </summary>
        private readonly BattleNetAuthenticator battleNetAuthenticator = new BattleNetAuthenticator();
        /// <summary>
        /// 子账号选择列表
        /// </summary>
        private Dictionary<string, string> SubAccounts;
        /// <summary>
        /// 游戏当前状态
        /// </summary>
        private GameState gameState;
        /// <summary>
        /// 是否首次启动在线成功
        /// </summary>
        private bool FirstStartSuccess = false;
        /// <summary>
        /// 是否在要塞，true=在，false=不在要塞
        /// </summary>
        private bool InYaoSai = false;
        /// <summary>
        /// 游戏掉线计数阈值
        /// </summary>
        //private int gameDownLine = 5;
        /// <summary>
        /// 游戏掉线计数
        /// </summary>
        private int gameDown = 0;
        /// <summary>
        /// 不在要塞计数阈值
        /// </summary>
        //private int yaosaiOutLine = 5;
        /// <summary>
        /// 不在要塞计数
        /// </summary>
        private int yaosaiOut = 0;
        /// <summary>
        /// 重试登录角色次数
        /// </summary>
        private int retryLoginCount = 0;
        /// <summary>
        /// 最大登录重试次数
        /// </summary>
        //private int maxRetryLoginCount = 15;
        private TimeSleepSchedulerExecutor timeSleepSchedulerExecutor;
        public ScriptTaskManager(ScriptItemModel scriptItemModel)
        {
            //初始化定时器调度器
            timeSleepSchedulerExecutor = new TimeSleepSchedulerExecutor();
            SubAccounts = new Dictionary<string, string>();
            SubAccounts.Add("WOW1", "wow1.bmp");
            SubAccounts.Add("WOW2", "wow2.bmp");
            SubAccounts.Add("WOW3", "wow3.bmp");
            SubAccounts.Add("WOW4", "wow4.bmp");
            SubAccounts.Add("WOW5", "wow5.bmp");
            SubAccounts.Add("WOW6", "wow6.bmp");
            SubAccounts.Add("WOW7", "wow7.bmp");
            SubAccounts.Add("WOW8", "wow8.bmp");
            //初始化状态
            this.notStartedState = new ScriptTaskNotStartedState(this);
            this.runningState = new ScriptTaskRunningState(this);
            this.stoppedState = new ScriptTaskStoppedState(this);
            this.suspendState = new ScriptTaskSuspendState(this);
            //当前状态
            this.currentState = this.notStartedState;
            //脚本任务配置
            this.scriptItemModel = scriptItemModel;
            RegisterGameState();
            //注册任务
            RegisterMonitorGameDie();
            //注册wow动态码获取任务
            RegisterWowDynamicCode();
            //注册脚本执行任务
            RegisterExecuteScript();
        }
        public void Log(string msg, Color? color = null)
        {
            if (color == null) color = Color.Blue;
            ConsoleLog.AddMessage(scriptItemModel.ConsoleMessageFormModel.RichTextBox, msg, Color.Green);
        }
        /// <summary>
        /// 设置任务状态
        /// </summary>
        /// <param name="scriptTaskState"></param>
        public void SetState(IScriptTaskState scriptTaskState)
        {
            this.currentState = scriptTaskState;
        }
        /// <summary>
        /// 初始化任务
        /// </summary>
        public void Init()
        {

        }
        /// <summary>
        /// 启动任务
        /// </summary>
        public void Start()
        {
            DM.SetShowErrorMsg(0);
            //全局资源路径
            DM.SetPath(scriptItemModel.MyAppResourcesPath);
            DM.SetDict(0, scriptItemModel.ZikuPath);
            //全局字库配置
            DM.EnableShareDict(1);
            //启动游戏进程，更新pid和hwnd
            //GameProcessStart();
            //注册游戏存活检测任务
            RegisterMonitorGameDieTask.Start();
            //RegisterGameStateTask.Start();
            //注册wow动态码任务
            //registerWowDynamicCodeTask.Start();
            //注册脚本执行器任务
            RegisterExecuteScriptTask.Start();
        }
        /// <summary>
        /// 暂停任务
        /// </summary>
        public void Pause()
        {

        }
        /// <summary>
        /// 停止任务
        /// </summary>
        public void Stop()
        {
            Dispose();
        }
        /// <summary>
        /// 注册wow动态码自动刷新服务 7秒一次
        /// </summary>
        private void RegisterWowDynamicCode()
        {
            registerWowDynamicCodeTask = new Task((obj) =>
            {
                Log("注册码刷新任务");
                var time = timeSleepSchedulerExecutor.GenTimeSleep();
                while (true)
                {
                    if (destroy) return;
                    //生成动态令牌
                    string serial = this.scriptItemModel.SerialNumber;
                    string restore = this.scriptItemModel.RestoreCode;
                    if (serial.Length == 0 || restore.Length == 0)
                    {
                        Log("请输入序列号和还原密码！");
                    }
                    try
                    {
                        battleNetAuthenticator.Restore(serial, restore);
                        scriptItemModel.DynamicCode = battleNetAuthenticator.CurrentCode;
                        //激活页面刷新
                    }
                    catch (Exception ex)
                    {
                        Log($"Unable to restore the authenticator:{ex.Message}");
                    }
                    finally
                    {
                        time.Sleep(10 * 1000);
                    }
                }
            }, null
            , TaskCreationOptions.LongRunning);
        }
        /// <summary>
        /// 游戏状态检测任务
        /// </summary>
        private void RegisterGameState()
        {
            RegisterGameStateTask = new Task((obj) =>
            {
                var time = timeSleepSchedulerExecutor.GenTimeSleep();
                while (true)
                {
                    if (destroy) return;
                    try
                    {
                        if (this.pid == 0)
                        {
                            this.gameState = GameState.ProcessDied;
                            goto complete;
                        }
                        var processList = Process.GetProcesses();
                        var exist = processList.Any(x => x.Id == this.pid);
                        hwnd = DM.FindWindowByProcessId(this.pid, "", "魔兽世界");
                        if (this.pid != 0 && exist && hwnd != 0)
                        {
                            //进程存在的前提下去检查其他状态

                        }
                    }
                    catch (Exception ex)
                    {
                        Log($"游戏状态检测任务出错：{ex.Message}");
                    }
                complete:
                    time.Sleep(10 * 1000);
                }
            }, null
            , TaskCreationOptions.LongRunning);
        }
        /// <summary>
        /// 游戏角色登录任务
        /// </summary>
        private void RegisterMonitorGameDie()
        {
            RegisterMonitorGameDieTask = new Task((obj) =>
            {
                var time = timeSleepSchedulerExecutor.GenTimeSleep();
                while (true)
                {
                    if (destroy) return;
                    Log("检测游戏进程是否存活...");
                    try
                    {
                        Log($"当前重试登录第{retryLoginCount}次,最大限制{scriptItemModel.MaxRetryLoginCount}");
                        if (retryLoginCount > scriptItemModel.MaxRetryLoginCount)
                        {
                            //重置等待次数
                            retryLoginCount = 0;
                            Log($"超过最大登录次数限制{scriptItemModel.MaxRetryLoginCount}，等待{scriptItemModel.WaitTimeMinute}分钟");
                            //发送预警
                            WarnScanSchedulerExecutor.GetInstance().ReportWarn(new WarnReportModel
                            {
                                MailSubject = $"【{scriptItemModel.ScriptName}】【掉线恢复】通知",
                                MailBody = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}【{scriptItemModel.ScriptName}】超过最大登录次数限制{scriptItemModel.MaxRetryLoginCount}，等待{scriptItemModel.WaitTimeMinute}分钟...",
                                ImagePath = ScreenGame()
                            });
                            //结束游戏
                            DM.TerminateProcess(this.pid);
                            this.pid = 0;
                            this.hwnd = 0;
                            //等待一段时间，不然容易被验证码
                            time.Sleep(scriptItemModel.WaitTimeMinute * 60 * 1000);
                        }
                        if (this.pid == 0)
                        {
                            Log("检测到游戏进程已kill");
                            //进程已死亡，游戏掉线
                            //让任务线程暂停
                            gameDiePause.Reset();
                            Log("暂停游戏脚本执行任务");
                            //启动游戏
                            GameProcessStart();
                            //登录角色
                            GameLogin(time);
                            continue;
                        }
                        var processList = Process.GetProcesses();
                        var exist = processList.Any(x => x.Id == this.pid);
                        hwnd = DM.FindWindowByProcessId(this.pid, "", "魔兽世界");
                        Log($"进程pid={pid}hwnd={hwnd}");
                        if (this.pid != 0 && exist && hwnd != 0)
                        {
                            Log($"检测到游戏进程存活pid={pid}hwnd={hwnd}");
                            //进程存活
                            //DM.BindWindowEx(hwnd, displayModel, "dx.mouse.input.lock.api3", keyboardModel, "", 0);
                            //判断游戏角色是否在线，在线则执行脚本
                            var online = DM.FindPic(0, 0, 2000, 2000, "wenhao.bmp", "000000", 0.7, 3, out var x, out var y);
                            if (online != -1)
                            {
                                gameDown = 0;
                                //重置重新登录计数
                                retryLoginCount = 0;
                                //游戏在线
                                Log("正常游戏中....");
                                //激活脚本执行线程
                                gameDiePause.Set();
                                //首次启动成功之后，发生掉线事件才发邮件
                                if (FirstStartSuccess && !scriptItemModel.GameOnline)
                                {
                                    Log($"掉线恢复");
                                    WarnScanSchedulerExecutor.GetInstance().ReportWarn(new WarnReportModel
                                    {
                                        MailSubject = $"【{scriptItemModel.ScriptName}】【掉线恢复】通知",
                                        MailBody = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}【{scriptItemModel.ScriptName}】游戏状态【恢复正常】，当前游戏在线中...",
                                        ImagePath = ScreenGame()
                                    });
                                }
                                //判断是否在要塞
                                online = DM.FindPic(0, 0, 2000, 2000, "yaosai.bmp", "000000", 0.6, Const.DM.图像查找方向.从右到左_从上到下, out x, out y);
                                var onlinePvp = DM.FindPic(0, 0, 2000, 2000, "yaosaipvp.bmp", "000000", 0.6, Const.DM.图像查找方向.从左到右_从上到下, out x, out y);
                                //识别文字
                                DM.SetDict(0, scriptItemModel.ZikuPath);
                                hwnd = DM.FindWindowByProcessId(this.pid, "", "魔兽世界");
                                var dmbind = DM.BindWindowEx(hwnd, displayModel, "dx.mouse.input.lock.api3", keyboardModel, "", 0);
                                var sss = DM.Capture(1159, 3, 1188, 17, @$"{AppDomain.CurrentDomain.BaseDirectory}Resources\shibiezi.bmp");
                                var str = DM.Ocr(0, 0, 2000, 2000, "fed000-937703", 0.7);
                                Log($"要塞文字识别：{str}");
                                if (online == 0 || onlinePvp == 0 || (str.Contains("要") || str.Contains("塞")))
                                {
                                    yaosaiOut = 0;
                                    //在要塞
                                    if (FirstStartSuccess && !InYaoSai)
                                    {
                                        Log("回到了要塞，解除预警");
                                        //第一次启动成功后，上次不在要塞里，本次在要塞，发送恢复通知
                                        //把恢复的图片放到邮件里
                                        WarnScanSchedulerExecutor.GetInstance().ReportWarn(new WarnReportModel
                                        {
                                            MailSubject = $"【{scriptItemModel.ScriptName}】【回到了要塞】通知",
                                            MailBody = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}【{scriptItemModel.ScriptName}】游戏状态【回到了要塞】，当前回到了要塞中...",
                                            ImagePath = ScreenGame()
                                        });
                                    }
                                    InYaoSai = true;
                                }
                                else
                                {
                                    Log("不在要塞");
                                    //不在要塞
                                    if (FirstStartSuccess && InYaoSai)
                                    {
                                        yaosaiOut++;
                                        Log($"当前掉出要塞次数{yaosaiOut},分数线:{scriptItemModel.YaosaiOutLine}");
                                        if (yaosaiOut > scriptItemModel.YaosaiOutLine)
                                        {
                                            //第一次启动成功后，上次在要塞里，本次不在要塞，则发送预警
                                            WarnScanSchedulerExecutor.GetInstance().ReportWarn(new WarnReportModel
                                            {
                                                MailSubject = $"【{scriptItemModel.ScriptName}】【不在要塞】通知",
                                                MailBody = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}【{scriptItemModel.ScriptName}】游戏状态【不在要塞】，当前不在要塞中，可能是要塞队长掉线你被踢出要塞...",
                                                ImagePath = ScreenGame()
                                            });
                                            InYaoSai = false;
                                        }
                                    }
                                }
                                FirstStartSuccess = true;
                                scriptItemModel.GameOnline = true;
                            }
                            else
                            {
                                //首次启动成功之后，且上次状态是正常的，本次不正常才发邮件
                                if (FirstStartSuccess && scriptItemModel.GameOnline)
                                {
                                    gameDown++;
                                    Log($"当前掉线次数{gameDown},分数线:{scriptItemModel.GameDownLine}");
                                    if (gameDown > scriptItemModel.GameDownLine)
                                    {
                                        WarnScanSchedulerExecutor.GetInstance().ReportWarn(new WarnReportModel
                                        {
                                            MailSubject = $"【{scriptItemModel.ScriptName}】【掉线异常】通知",
                                            MailBody = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}【{scriptItemModel.ScriptName}】游戏状态【游戏掉线】，当前可能掉到了重连或者游戏登录页面...",
                                            ImagePath = ScreenGame()
                                        });
                                        scriptItemModel.GameOnline = false;
                                    }
                                }
                                Log("检测到游戏角色不在线");
                                //游戏掉线，进程存活
                                gameDiePause.Reset();
                                var fdsuiji = DM.FindPic(0, 0, 2000, 2000, "chonglian.bmp", "000000", 0.5, 0, out var shopX, out var shopY);
                                if (fdsuiji == 0)
                                {
                                    //重连按钮
                                    GameDownRetryConnect(time);
                                    continue;
                                }
                                //登录角色
                                GameLogin(time);
                            }
                        }
                        else
                        {
                            Log($"检测到游戏进程有问题pid={pid}hwnd={hwnd}");
                            if (FirstStartSuccess && scriptItemModel.GameOnline)
                            {
                                gameDown++;
                                Log($"当前掉线次数{gameDown},分数线:{scriptItemModel.GameDownLine}");
                                if (gameDown > scriptItemModel.GameDownLine)
                                {
                                    WarnScanSchedulerExecutor.GetInstance().ReportWarn(new WarnReportModel
                                    {
                                        MailSubject = $"【{scriptItemModel.ScriptName}】【掉线异常】通知",
                                        MailBody = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}【{scriptItemModel.ScriptName}】游戏状态【游戏掉线】，当前可能是游戏进程或者句柄消失...",
                                        ImagePath = ScreenGame()
                                    });
                                    scriptItemModel.GameOnline = false;
                                }
                            }
                            Log("检测到游戏进程已kill");
                            //进程已死亡，游戏掉线
                            //让任务线程暂停
                            gameDiePause.Reset();
                            Log("暂停游戏脚本执行任务");
                            //启动游戏
                            GameProcessStart();
                            //登录角色
                            GameLogin(time);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log($"游戏进程检测任务出错：{ex.Message}");
                    }
                    finally
                    {
                        time.Sleep(5 * 1000);
                    }
                }
            }, null
            , TaskCreationOptions.LongRunning);
        }
        /// <summary>
        /// 截取游戏画面
        /// </summary>
        /// <returns></returns>
        private string ScreenGame()
        {
            var cachePath = AppDomain.CurrentDomain.BaseDirectory + @"Cache\Warn\";
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }
            var iamgePath = cachePath + Guid.NewGuid().ToString() + ".bmp";
            DM.Capture(0, 0, scriptItemModel.Width, scriptItemModel.Height, iamgePath);
            return iamgePath;
        }
        private void GameDownRetryConnect(TimeSleep time)
        {
            retryLoginCount++;
            int shopX = 0, shopY = 0;
            //有重连按钮
            Log("检测到游戏掉线....");
            var move = DM.MoveTo(588, 343);
            var click = DM.LeftDoubleClick();
            time.Sleep(1000);
            move = DM.MoveTo(601, 386);
            click = DM.LeftDoubleClick();
            Log("正在操作重连中....");
            time.Sleep(4000);
            //判断有没有魔兽世界图标
            var fdsuiji = DM.FindPic(0, 0, 2000, 2000, "loginSelect.bmp", "000000", 0.7, 0, out shopX, out shopY);
            if (fdsuiji == -1)
            {
                throw new Exception("重连失败....");
            }
            fdsuiji = DM.FindPic(0, 0, 2000, 2000, "jinruwow.bmp", "000000", 0.7, 1, out shopX, out shopY);
            if (fdsuiji == -1)
            {
                throw new Exception("进入魔兽世界失败....");
            }
            move = DM.MoveTo(shopX + 2, shopY + 2);
            click = DM.LeftDoubleClick();
            Log("进入游戏世界成功....");
        }
        /// <summary>
        /// 启动游戏，更新pid和hwnd
        /// </summary>
        private void GameProcessStart()
        {
            hwnd = DM.FindWindowByProcessId(this.pid, "", "魔兽世界");
            if (hwnd != 0)
            {
                DM.TerminateProcess(this.pid);
            }
            this.gameProcess = Process.Start(scriptItemModel.GamePath);
            this.pid = gameProcess.Id;
            this.hwnd = this.DM.FindWindowByProcessId(this.pid, "", "魔兽世界");
            Log($"启动游戏成功：pid={this.pid}");
        }
        /// <summary>
        /// 登录角色
        /// </summary>
        private void GameLogin(TimeSleep time)
        {
            retryLoginCount++;
            //显卡兼容问题会弹窗
            var result = -1;
            //是否现在在首页
            //result = DM.BindWindowEx(hwnd, displayModel, "dx.mouse.input.lock.api3", keyboardModel, "", 0);
            var fdsuiji = DM.FindPic(0, 0, 2000, 2000, "shilingtishi.bmp", "000000", 0.7, 0, out var xx, out var yy);
            if (fdsuiji == 0)
            {
                var move = DM.MoveTo(609, 344);
                var click = DM.LeftDoubleClick();
                Debug.WriteLine("退到首页准备重登....");
                time.Sleep(500);
                move = DM.MoveTo(601, 599);
                click = DM.LeftDoubleClick();
                goto inputAccount;
            }
            if (scriptItemModel.NvidiaShow)
            {
                Log("需要处理NVIDIA显卡兼容提示");
                //如果会弹nvidia兼容才进入这里
                var i = 0;
                while (true)
                {
                    if (i >= 4)
                    {
                        Log("没有找到显卡兼容弹窗！");
                        //DM.TerminateProcess(this.pid);
                        throw new Exception("没有找到显卡兼容弹窗！");
                    }
                    i++;
                    Log($"正在查找pid={pid}的游戏窗口");
                    result = DM.FindWindowByProcessId(this.pid, "", "魔兽世界");
                    if (result == 0)
                    {
                        time.Sleep(1000);
                        continue;
                    }
                    this.hwnd = result;
                    var dmbind = DM.BindWindowEx(hwnd, "gdi", "windows3", "normal", "", 0);
                    fdsuiji = DM.FindPic(0, 0, 2000, 2000, "login0.bmp", "000000", 0.8, 3, out xx, out yy);
                    if (fdsuiji == 0)
                    {
                        Log("检测到显卡兼容OK按钮！");
                        time.Sleep(3000);
                        DM.MoveTo(xx, yy);
                        DM.LeftDoubleClick();
                        Log("点击显卡OK按钮成功！");
                        //int childHwnd = WinApi.FindWindowEx(hwnd, 0, null, "OK");//按钮控件标题
                        //var clickResult = WinApi.SendMessage(childHwnd, WinApi.BM_CLICK, 0, 0);
                        //Log($"SendMessage={clickResult}");

                        //time.Sleep(1000);
                        //fdsuiji = DM.FindPic(0, 0, 2000, 2000, "login0.bmp", "000000", 0.8, 3, out xx, out yy);
                        //if (fdsuiji == 0)
                        //{
                        //    Log("检测到显卡兼容提示窗口还在！");
                        //    time.Sleep(1000);
                        //    continue;
                        //}
                        break;
                    }
                    break;
                }
            }
            //句柄变更需要重新获取
            Log($"准备重新获取主窗口");
            var j = 0;
            while (true)
            {
                if (j > 15)
                {
                    Log("游戏登录界面未成功打开！");
                    //DM.TerminateProcess(this.pid);
                    throw new Exception("游戏登录界面未成功打开！");
                }
                hwnd = DM.FindWindowByProcessId(this.pid, "", "魔兽世界");
                Log($"游戏主窗口hwnd={hwnd}");
                if (hwnd == 0)
                {
                    time.Sleep(1000);
                    continue;
                }
                break;
            }
            Log($"句柄变更需要重新获取 wow hwnd:{hwnd}");
            result = DM.BindWindowEx(hwnd, displayModel, "dx.mouse.input.lock.api3", keyboardModel, "", 0);
            //移动窗口
            var results = DM.SetWindowSize(this.hwnd, this.scriptItemModel.Width, this.scriptItemModel.Height);
            Log($"SetWindowSize: width={this.scriptItemModel.Width} height={this.scriptItemModel.Height}");
            time.Sleep(300);
            results = DM.MoveWindow(this.hwnd, this.scriptItemModel.X, this.scriptItemModel.Y);
            Log($"MoveWindow:{results}  x={this.scriptItemModel.X}  y={this.scriptItemModel.Y}");
            time.Sleep(300);
        inputAccount:
            //点协议
            result = DM.MoveTo(623, 605);
            Log($"MoveTo:{result}");
            result = DM.LeftDoubleClick();
            Log($"LeftDoubleClick:{result}");
            time.Sleep(1000);
            //输入账号密码等
            var email = scriptItemModel.Account;
            var password = scriptItemModel.Password;
            //账号
            result = DM.MoveTo(552, 349);
            Log($"MoveTo:{result}");
            result = DM.LeftDoubleClick();
            Log($"LeftDoubleClick:{result}");
            time.Sleep(300);
            result = DM.SendString(this.hwnd, email);
            Log($"输入账号：{email}");
            time.Sleep(300);
            //密码
            //result = DM.MoveTo(552, 349);
            result = DM.MoveTo(552, 413);
            Log($"MoveTo:{result}");
            result = DM.LeftDoubleClick();
            Log($"LeftDoubleClick:{result}");
            result = DM.SendString(this.hwnd, password);
            Log($"输入账号：{password}");
            time.Sleep(300);
            //确定
            result = DM.MoveTo(552, 497);
            Log($"MoveTo:{result}");
            result = DM.LeftDoubleClick();
            Log($"LeftDoubleClick:{result}");
            Log($"确定");
            time.Sleep(6000);
            //输入动态码
            //获取动态码
            battleNetAuthenticator.Restore(this.scriptItemModel.SerialNumber, this.scriptItemModel.RestoreCode);
            var dynamicCode = this.battleNetAuthenticator.CurrentCode;
            result = DM.MoveTo(576, 350);
            Log($"MoveTo:{result}");
            result = DM.LeftDoubleClick();
            Log($"LeftDoubleClick:{result}");
            result = DM.SendString(this.hwnd, dynamicCode);
            //确定动态码
            result = DM.MoveTo(518, 396);
            Log($"MoveTo:{result}");
            result = DM.LeftDoubleClick();
            Log($"LeftDoubleClick:{result}");
            Log($"输入动态码：{dynamicCode}");
            time.Sleep(2000);
            //选子账号
            var subAccount = SubAccounts[scriptItemModel.SubAccount];
            result = DM.FindPic(0, 0, 2000, 2000, subAccount, "000000", 0.6, 0, out var x, out var y);
            if (result == 0)
            {
                Log($"找到{scriptItemModel.SubAccount},x={x},y={y}");
                //找到账号
                DM.MoveTo(x + 4, y + 4);
                DM.LeftDoubleClick();
            }
            else
            {
                //没找到
                throw new Exception($"没找到{scriptItemModel.SubAccount}");
            }
            time.Sleep(1000);
            //同意
            result = DM.MoveTo(541, 442);
            Log($"MoveTo:{result}");
            result = DM.LeftDoubleClick();
            Log($"LeftDoubleClick:{result}");
            time.Sleep(3000);
            //进入wow
            result = DM.KeyPressChar("enter");
            Log($"进入wow世界");
        }

        /// <summary>
        /// 脚本执行任务
        /// </summary>
        private void RegisterExecuteScript()
        {
            RegisterExecuteScriptTask = new Task((obj) =>
            {
                while (true)
                {
                    if (destroy) return;
                    Log("脚本执行中...");
                    try
                    {
                        gameDiePause.WaitOne();
                        this.scriptAction(DM, Log);
                    }
                    catch (Exception ex)
                    {
                        Log($"{scriptItemModel.ScriptName}-脚本执行错误");
                    }
                }
            }, null
            , TaskCreationOptions.LongRunning);
        }
        /// <summary>
        /// 设置运行的脚本
        /// </summary>
        /// <param name="action"></param>
        public void SetScriptAction(Action<Dmsoft, Action<string, Color?>> action)
        {
            this.scriptAction = action;
        }
        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            destroy = true;
            gameDiePause.Set();
            timeSleepSchedulerExecutor.OverAll();
            Task.WaitAll(registerWowDynamicCodeTask, RegisterExecuteScriptTask, RegisterMonitorGameDieTask, RegisterGameStateTask);
            registerWowDynamicCodeTask = null;
            RegisterExecuteScriptTask = null;
            RegisterMonitorGameDieTask = null;
            RegisterGameStateTask = null;
            DM.ReleaseObj();
        }
    }
}
