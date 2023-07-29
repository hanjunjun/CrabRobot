using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrabRobot
{
    public class Const
    {
        public class ScriptXmlConfig
        {
            public const string ScriptConfig = "ScriptConfig.json";
            public const string ToolSettingsConfig = "ToolSettingsConfig.json";
            public const string ScriptId = "ScriptId";
        }
        /// <summary>
        /// 脚本执行状态枚举
        /// </summary>
        public class ScriptTaskState
        {
            /// <summary>
            /// 未开始
            /// </summary>
            public static readonly ScriptTaskState Not_Started_State = new ScriptTaskState(0, "未开始", "开始", "Start");
            /// <summary>
            /// 运行中
            /// </summary>
            public static readonly ScriptTaskState Running_State = new ScriptTaskState(0, "运行中", "暂停", "Suspend");
            /// <summary>
            /// 暂停
            /// </summary>
            public static readonly ScriptTaskState Suspend_State = new ScriptTaskState(0, "暂停", "继续", "Continue");
            /// <summary>
            /// 停止
            /// </summary>
            public static readonly ScriptTaskState Stopped_State = new ScriptTaskState(0, "停止", "开始", "Start");
            /// <summary>
            /// 列出所有的值
            /// </summary>
            public static IEnumerable<ScriptTaskState> Values
            {
                get
                {
                    yield return Not_Started_State;
                    yield return Running_State;
                    yield return Suspend_State;
                    yield return Stopped_State;
                }
            }
            /// <summary>
            /// 根据code获取枚举对象
            /// </summary>
            /// <param name="code"></param>
            /// <returns></returns>
            public static ScriptTaskState GetScriptTaskStateByCode(int code)
            {
                var state = Values.FirstOrDefault(x => x.Code == code);
                if (state == null)
                {
                    throw new Exception($"ScriptTaskState枚举中不存在{code}");
                }
                return state;
            }
            /// <summary>
            /// 状态编码
            /// </summary>
            public int Code { get; private set; }
            /// <summary>
            /// 状态解释
            /// </summary>
            public string Msg { get; private set; }
            /// <summary>
            /// 状态对应的按钮文本
            /// </summary>
            public string ButtonText { get; private set; }
            /// <summary>
            /// 状态对应的操作方法
            /// </summary>
            public string FunctionName { get; private set; }

            ScriptTaskState(int code, string msg, string buttonText, string functionName)
            {
                this.Code = code;
                this.Msg = msg;
                this.ButtonText = buttonText;
                this.FunctionName = functionName;
            }
        }
        /// <summary>
        /// 游戏状态
        /// </summary>
        public enum GameState
        {
            /// <summary>
            /// 游戏进程已死亡
            /// </summary>
            ProcessDied = 0,
            /// <summary>
            /// 显卡兼容提示
            /// </summary>
            NvidiaOkTips,
            /// <summary>
            /// 游戏账号输入界面
            /// </summary>
            AccountEntry,
            /// <summary>
            /// 角色选择页面
            /// </summary>
            RoleChoice,
            /// <summary>
            /// 游戏角色在线
            /// </summary>
            GameRoleOnline,
            /// <summary>
            /// 未知状态
            /// </summary>
            Other,
            /// <summary>
            /// 游戏掉线，在重连界面
            /// </summary>
            GameRoleDownRetryConnect
        }
        /// <summary>
        /// dm组件相关设置枚举
        /// </summary>
        public class DM
        {
            public class 图像查找方向
            {
                public const int 从左到右_从上到下 = 0;
                public const int 从左到右_从下到上 = 1;
                public const int 从右到左_从上到下 = 2;
                public const int 从右到左_从下到上 = 3;
            }
        }
        /// <summary>
        /// 入参key常量
        /// </summary>
        public class ArgsKey
        {
            /// <summary>
            /// 进程类型
            /// </summary>
            public const string Type = "--type";
            /// <summary>
            /// 运行级别
            /// </summary>
            public const string Debugger = "--debugger";
            /// <summary>
            /// 日志级别
            /// </summary>
            public const string LogLevel = "--log-level";
        }
        /// <summary>
        /// 程序入参
        /// </summary>
        public static class ArgsValue
        {
            /// <summary>
            /// 进程所属模块
            /// </summary>
            public static Module Module = new();
            /// <summary>
            /// 运行环境
            /// </summary>
            public class Environment
            {
                /// <summary>
                /// dev模式
                /// </summary>
                public const string Dev = "dev";
                /// <summary>
                /// test模式
                /// </summary>
                public const string Test = "test";
                /// <summary>
                /// prod生产模式
                /// </summary>
                public const string Prod = "prod";
            }
            /// <summary>
            /// 默认是info级别
            /// </summary>
            public enum LogLevel
            {
                /// <summary>
                /// 调试级别
                /// </summary>
                Debug = 4,
                /// <summary>
                /// 提示级别
                /// </summary>
                Info = 3,
                /// <summary>
                /// 警告级别
                /// </summary>
                Warning = 2,
                /// <summary>
                /// 错误级别
                /// </summary>
                Error = 1,
                /// <summary>
                /// 致命级别
                /// </summary>
                Fatal = 0
            }
        }
        public class Module
        {
            /// <summary>
            /// 主进程
            /// </summary>
            public string Main => "main";
            /// <summary>
            /// 录像进程
            /// </summary>
            public string ScreenVideo => "screen-video";
        }
    }
}
