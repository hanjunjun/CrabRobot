using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using WinAuth;
using HBNiuBi.Native;
using HBNiuBi.Config;
using HBNiuBi.ScriptTask;
using static HBNiuBi.Const;
using System.Windows.Forms;

namespace HBNiuBi.Model
{
    public class ScriptItemModel : ScriptConfig
    {
        /// <summary>
        /// 动态码
        /// </summary>
        public string DynamicCode { get; set; }
        /// 工具的根目录
        /// </summary>
        public string MyAppDomainPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 工具的资源目录
        /// </summary>
        public string MyAppResourcesPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory + @"Resources\";
        public string ZikuPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory + @"Resources\ziku.txt";
        /// <summary>
        /// 角色是否在线
        /// </summary>
        public bool GameOnline { get; set; } = false;
        /// <summary>
        /// 状态
        /// </summary>
        public ScriptTaskState Status { get; set; } = Const.ScriptTaskState.Not_Started_State;
        /// <summary>
        /// tab控件容器
        /// </summary>
        public TabControl TabControl { get; set; }
        /// <summary>
        /// 日志消息容器
        /// </summary>
        public ConsoleMessageFormModel ConsoleMessageFormModel { get; set; } = new ConsoleMessageFormModel();
    }
    public class ConsoleMessageFormModel
    {
        /// <summary>
        /// 日志text框
        /// </summary>
        public RichTextBox RichTextBox { get; set; } = new RichTextBox();
        /// <summary>
        /// 页面标签页
        /// </summary>
        public TabPage TabPage { get; set; } = new TabPage();
    }
}
