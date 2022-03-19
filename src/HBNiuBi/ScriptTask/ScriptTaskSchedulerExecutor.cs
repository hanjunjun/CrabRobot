using HBNiuBi.Config;
using HBNiuBi.Model;
using HBNiuBi.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBNiuBi.ScriptTask
{
    public class ScriptTaskSchedulerExecutor
    {
        private readonly static object mLock = new object();
        /// <summary>
        /// 脚本任务集合
        /// </summary>
        private readonly static ConcurrentDictionary<string, ScriptTaskManager> ScriptTaskContainer = new ConcurrentDictionary<string, ScriptTaskManager>();
        /// <summary>
        /// 单例对象
        /// </summary>
        private volatile static ScriptTaskSchedulerExecutor schedulerExecutor = null;

        /// <summary>
        /// 私有化
        /// </summary>
        private ScriptTaskSchedulerExecutor()
        {

        }

        /// <summary>
        /// 获取下载任务调度器单例对象
        /// </summary>
        /// <returns></returns>
        public static ScriptTaskSchedulerExecutor GetInstance()
        {
            if (schedulerExecutor == null)
            {
                lock (mLock)
                {
                    if (schedulerExecutor == null)
                    {
                        schedulerExecutor = new ScriptTaskSchedulerExecutor();
                    }
                }
            }
            return schedulerExecutor;
        }

        /// <summary>
        /// 根据状态执行不同操作
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <param name="operateType">操作类型</param>
        /// <returns></returns>
        public void OperateOn(string taskId, string operateType)
        {
            //执行不同操作
            if (!ScriptTaskContainer.ContainsKey(taskId))
            {
                throw new Exception("任务不存在：" + taskId);
            }

            if (!ScriptTaskContainer.TryGetValue(taskId, out var value))
            {
                throw new Exception("从任务池中获取任务失败！");
            }
            ScriptTaskManager scriptTaskManager = value;
            if (string.IsNullOrEmpty(operateType))
            {
                throw new Exception($"传入的操作方法不能为空！");
            }
            var function = scriptTaskManager?.GetType()?.GetMethod(operateType);
            if (function == null)
            {
                throw new Exception($"获取脚本任务管理器方法{operateType}失败！");
            }
            function.Invoke(scriptTaskManager, null);
        }
        /// <summary>
        /// 加载任务到调度器-不保存配置到本地
        /// </summary>
        /// <param name="scriptItemModel"></param>
        public void LoadTask(ScriptItemModel scriptItemModel)
        {
            ScriptTaskContainer.TryAdd(scriptItemModel.ScriptId, new ScriptTaskManager(scriptItemModel));
            LoadMessageControls(scriptItemModel);
        }
        private void LoadMessageControls(ScriptItemModel scriptItemModel)
        {
            var tabPage = scriptItemModel.ConsoleMessageFormModel.TabPage;
            var richTextBox = scriptItemModel.ConsoleMessageFormModel.RichTextBox;
            var tabControl = scriptItemModel.TabControl;
            tabPage.SuspendLayout();
            //tab窗体添加tab页
            tabPage.Controls.Add(richTextBox);
            //设置tabpage
            tabPage.Location = new System.Drawing.Point(4, 26);
            tabPage.Name = "tabPage1";
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(513, 402);
            tabPage.TabIndex = 0;
            tabPage.Text = scriptItemModel.ScriptName;
            tabPage.UseVisualStyleBackColor = true;
            tabPage.ResumeLayout(false);
            tabControl.TabPages.Add(tabPage);
            //添加log窗口
            richTextBox.BackColor = System.Drawing.Color.White;
            richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            richTextBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            richTextBox.Location = new System.Drawing.Point(3, 3);
            richTextBox.Name = "richTextBox1";
            richTextBox.ReadOnly = true;
            richTextBox.Size = new System.Drawing.Size(507, 396);
            richTextBox.TabIndex = 0;
            richTextBox.Text = "";
            richTextBox.Tag = scriptItemModel.ScriptName;
        }
        /// <summary>
        /// 添加脚本管理器到调度器，并将脚本配置持久化到本地
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <returns></returns>
        public void AddTask(ScriptItemModel scriptItemModel)
        {
            ScriptTaskContainer.TryAdd(scriptItemModel.ScriptId, new ScriptTaskManager(scriptItemModel));
            LoadMessageControls(scriptItemModel);
            //持久化到本地
            var configManager = new JsonConfig(Const.ScriptXmlConfig.ScriptConfig);
            var model = new ScriptConfig();
            model.ScriptId = scriptItemModel.ScriptId;
            model.Sort = scriptItemModel.Sort;
            model.ScriptName = scriptItemModel.ScriptName;
            model.PlayerName = scriptItemModel.PlayerName;
            model.Account = scriptItemModel.Account;
            model.Password = scriptItemModel.Password;
            model.SerialNumber = scriptItemModel.SerialNumber;
            model.RestoreCode = scriptItemModel.RestoreCode;
            model.GamePath = scriptItemModel.GamePath;
            model.X = scriptItemModel.X;
            model.Y = scriptItemModel.Y;
            model.Width = scriptItemModel.Width;
            model.Height = scriptItemModel.Height;
            model.NvidiaShow = scriptItemModel.NvidiaShow;
            model.SubAccount = scriptItemModel.SubAccount;
            model.GameDownLine = scriptItemModel.GameDownLine;
            model.YaosaiOutLine = scriptItemModel.YaosaiOutLine;
            model.MaxRetryLoginCount = scriptItemModel.MaxRetryLoginCount;
            model.WaitTimeMinute = scriptItemModel.WaitTimeMinute;
            model.StartTime = scriptItemModel.StartTime;
            configManager.AddConfig(model);
        }
        /// <summary>
        /// 根据脚本id获取脚本管理器
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ScriptTaskManager GetScriptTaskManagerById(string taskId)
        {
            if(ScriptTaskContainer.TryGetValue(taskId, out var value))
            {
                return value;
            }
            throw new Exception($"获取脚本id={taskId}脚本管理器失败！");
        }

        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="scriptItemModel"></param>
        /// <returns></returns>
        public void UpdateTask(ScriptItemModel scriptItemModel)
        {
            //更新内存里的任务
            if (!ScriptTaskContainer.TryGetValue(scriptItemModel.ScriptId, out var outValue))
            {
                throw new Exception($"未能从字典中获取{scriptItemModel.ScriptName}的值！");
            }
            //if (!ScriptTaskContainer.TryUpdate(scriptItemModel.ScriptId, new ScriptTaskManager(scriptItemModel), outValue))
            //{
            //    throw new Exception($"更新{scriptItemModel.ScriptName}的字典值失败！");
            //}
            //更新本地配置文件
            UpdateConfig(scriptItemModel);
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="taskId"></param>
        public void DeleteTask(string taskId)
        {
            //删除本地存储的配置
            var configManager = new JsonConfig(Const.ScriptXmlConfig.ScriptConfig);
            configManager.DeleteConfig(taskId);
            //停止任务
            ScriptTaskContainer[taskId].Stop();
            //删除内存数据
            ScriptTaskContainer.Remove(taskId, out var scriptModel);
        }

        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="scriptItemModel"></param>
        private void UpdateConfig(ScriptItemModel scriptItemModel)
        {
            var configManager = new JsonConfig(Const.ScriptXmlConfig.ScriptConfig);
            var model = new ScriptConfig();
            model.ScriptId = scriptItemModel.ScriptId;
            model.Sort = scriptItemModel.Sort;
            model.ScriptName = scriptItemModel.ScriptName;
            model.PlayerName = scriptItemModel.PlayerName;
            model.Account = scriptItemModel.Account;
            model.Password = scriptItemModel.Password;
            model.SerialNumber = scriptItemModel.SerialNumber;
            model.RestoreCode = scriptItemModel.RestoreCode;
            model.GamePath = scriptItemModel.GamePath;
            model.X = scriptItemModel.X;
            model.Y = scriptItemModel.Y;
            model.Width = scriptItemModel.Width;
            model.Height = scriptItemModel.Height;
            model.NvidiaShow = scriptItemModel.NvidiaShow;
            model.SubAccount = scriptItemModel.SubAccount;
            model.GameDownLine = scriptItemModel.GameDownLine;
            model.YaosaiOutLine = scriptItemModel.YaosaiOutLine;
            model.MaxRetryLoginCount = scriptItemModel.MaxRetryLoginCount;
            model.WaitTimeMinute = scriptItemModel.WaitTimeMinute;
            model.StartTime = scriptItemModel.StartTime;
            configManager.UpdateConfig(model.ScriptId, model);
        }
    }
}
