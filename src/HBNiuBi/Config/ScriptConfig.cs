using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBNiuBi.Config
{
    public class ScriptConfig
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
        /// 游戏目录
        /// </summary>
        public string GamePath { get; set; }
        /// <summary>
        /// 掉线最大次数
        /// </summary>
        public int GameDownLine { get; set; } = 6;
        /// <summary>
        /// 不在要塞最大次数，超过就报警
        /// </summary>
        public int YaosaiOutLine { get; set; } = 6;
        /// <summary>
        /// 最大重试登录次数
        /// </summary>
        public int MaxRetryLoginCount { get; set; } = 20;
        /// <summary>
        /// 延迟等待分钟数
        /// </summary>
        public int WaitTimeMinute { get; set; } = 60;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Width { get; set; } = 1253;
        public int Height { get; set; } = 706;
        /// <summary>
        /// 你的机器是否会弹显卡兼容提示
        /// </summary>
        public bool NvidiaShow { get; set; } = true;
        /// <summary>
        /// 游戏子账号WOW1
        /// </summary>
        public string SubAccount { get; set; } = "WOW1";
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }= DateTime.Now;
    }
}
