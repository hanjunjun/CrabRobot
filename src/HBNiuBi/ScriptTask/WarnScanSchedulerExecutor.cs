using HBNiuBi.Config;
using HBNiuBi.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBNiuBi.ScriptTask
{
    public class WarnScanSchedulerExecutor
    {
        private readonly static object mLock = new object();
        /// <summary>
        /// 预警队列
        /// </summary>
        private readonly static ConcurrentQueue<WarnReportModel> ScriptTaskContainer = new ConcurrentQueue<WarnReportModel>();
        /// <summary>
        /// 单例对象
        /// </summary>
        private volatile static WarnScanSchedulerExecutor schedulerExecutor = null;
        private static Task writeTask = default;
        static ManualResetEvent pause = new ManualResetEvent(false);//开始是无信号的
        /// <summary>
        /// 私有化
        /// </summary>
        private WarnScanSchedulerExecutor()
        {
            writeTask = new Task((obj) =>
            {
                while (true)
                {
                    try
                    {
                        pause.WaitOne();
                        pause.Reset();
                        if (ScriptTaskContainer.TryDequeue(out var mailServerConfig))
                        {
                            ToolSettingsConfig toolSettingsConfig = new ToolSettingsConfig(Const.ScriptXmlConfig.ToolSettingsConfig);
                            var config = toolSettingsConfig.GetConfig().MailServerConfig;
                            MailServerUtil.SendMailImage(mailServerConfig, config);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.Message, new string[] { "预警服务" });
                    }
                }
            }
            , null
            , TaskCreationOptions.LongRunning);//意味着该任务将长时间运行，因此他不是在线程池中执行。
            writeTask.Start();
        }
        public static WarnScanSchedulerExecutor GetInstance()
        {
            if (schedulerExecutor == null)
            {
                lock (mLock)
                {
                    if (schedulerExecutor == null)
                    {
                        schedulerExecutor = new WarnScanSchedulerExecutor();
                    }
                }
            }
            return schedulerExecutor;
        }

        public void ReportWarn(WarnReportModel mailServerConfig)
        {
            ScriptTaskContainer.Enqueue(mailServerConfig);
            pause.Set();
        }
    }
}
