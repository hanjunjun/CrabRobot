using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBNiuBi.Model
{
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
        private ScriptItemModel scriptItemModel;
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
            if (result != 1)
            {
                throw new Exception($"f2 启动失败！");
            }
            this.scriptItemModel = scriptItemModel;
        }
        /// <summary>
        /// 注册wow动态码自动刷新服务 7秒一次
        /// </summary>
        private void RegisterWowDynamicCode()
        {
            registerWowDynamicCodeTask = new Task((obj) =>
              {
                  Thread.Sleep(7 * 1000);
              }, null
            , TaskCreationOptions.LongRunning);
            registerWowDynamicCodeTask.Start();
        }

        public void SetScriptAction(Action action)
        {
            this.scriptAction = action;
        }
    }
}
