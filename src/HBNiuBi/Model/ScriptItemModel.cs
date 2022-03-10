using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace HBNiuBi.Model
{
    public static class ScriptContainer
    {
        public static ConcurrentDictionary<string, ScriptModel> ScriptTaskContainer = new ConcurrentDictionary<string, ScriptModel>();
    }
    public class ScriptItemModel
    {
        /// <summary>
        /// 脚本id
        /// </summary>
        public string ScriptId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 脚本名称
        /// </summary>
        public string ScriptName { get; set; }
        /// <summary>
        /// 游戏角色姓名
        /// </summary>
        public string PlayerName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 还原码
        /// </summary>
        public string RestoreCode { get; set; }
        /// <summary>
        /// 动态码
        /// </summary>
        public string DynamicCode { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public ScriptStatus Status { get; set; }
    }

    /// <summary>
    /// 脚本运行状态
    /// </summary>
    public enum ScriptStatus
    {
        Pause,
        Running
    }

    public class DMSecret
    {
        public string Code { get; set; }
        public string Ver { get; set; }
    }

    public class ScriptModel
    {
        /// <summary>
        /// 游戏进程对象
        /// </summary>
        private Process gameProcess;
        /// <summary>
        /// 脚本信息
        /// </summary>
        public ScriptItemModel scriptItemModel;
        public DMSecret dMSecret;
        /// <summary>
        /// dm对象
        /// </summary>
        public dmsoft DM = new dmsoft();
        /// <summary>
        /// 角色是否在线
        /// </summary>
        private bool gameOnline = false;
        /// <summary>
        /// 脚本动作
        /// </summary>
        private Action scriptAction;
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

        public ScriptModel(ScriptItemModel scriptItemModel, DMSecret dMSecret)
        {
            this.scriptItemModel = scriptItemModel;
            this.dMSecret = dMSecret;
            //注册任务
            RegisterWowDynamicCode();
            RegisterExecuteScript();
        }

        public void Start()
        {
            DM.SetShowErrorMsg(0);
            var path = AppDomain.CurrentDomain.BaseDirectory + Process.GetCurrentProcess().ProcessName + ".exe";
            var result = DM.Reg(dMSecret.Code, dMSecret.Ver);
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
            //if (result != 1)
            //{
            //    throw new Exception($"f2 启动失败！");
            //}
            //启动任务
            registerWowDynamicCodeTask.Start();
            RegisterExecuteScriptTask.Start();
        }
        /// <summary>
        /// 注册wow动态码自动刷新服务 7秒一次
        /// </summary>
        private void RegisterWowDynamicCode()
        {
            registerWowDynamicCodeTask = new Task((obj) =>
              {
                  Debug.WriteLine("注册码刷新任务");
                  Thread.Sleep(7 * 1000);
              }, null
            , TaskCreationOptions.LongRunning);
        }

        private void RegisterExecuteScript()
        {
            RegisterExecuteScriptTask = new Task((obj) =>
            {
                Debug.WriteLine("脚本执行中...");
                this.scriptAction();
                Thread.Sleep(7 * 1000);
            }, null
            , TaskCreationOptions.LongRunning);
        }

        public void SetScriptAction(Action action)
        {
            this.scriptAction = action;
        }
    }
}
