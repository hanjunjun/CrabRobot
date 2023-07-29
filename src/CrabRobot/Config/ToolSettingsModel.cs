using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrabRobot.Config
{
    public class ToolSettingsModel
    {
        /// <summary>
        /// dm组件秘钥设置
        /// </summary>
        public DMSecret DMSecret { get; set; } = new DMSecret();
        /// <summary>
        /// 录像设置
        /// </summary>
        public VideoConfig VideoConfig { get; set; } = new VideoConfig();
        /// <summary>
        /// 预警邮件服务配置
        /// </summary>
        public MailServerConfig MailServerConfig { get; set; } = new MailServerConfig();
    }
    public class VideoConfig
    {
        /// <summary>
        /// 是否启用录像
        /// 设置好录像区间，留出足够的硬盘容量
        /// </summary>
        public bool EnableVideo { get; set; } = true;
        /// <summary>
        /// 录像时间段开始时间
        /// </summary>
        public int StartHour { get; set; } = 0;
        /// <summary>
        /// 录像时间段结束时间
        /// </summary>
        public int EndHour { get; set; } = 23;
        /// <summary>
        /// 录像只保留最近x天
        /// </summary>
        public int SaveDayLine { get; set; } = 4;
        /// <summary>
        /// 录像保存位置
        /// </summary>
        public string VideoSavePath { get; set; } = AppDomain.CurrentDomain.BaseDirectory+"ScreenVideo";
    }
    public class MailServerConfig
    {
        /// <summary>
        /// 发件服务器
        /// </summary>
        public string MailServer { get; set; } = "smtp.qq.com";
        /// <summary>
        /// 发件服务器端口
        /// </summary>
        public int MailServerPort { get; set; } = 25;
        /// <summary>
        /// 发件人邮箱
        /// </summary>
        public string SendMail { get; set; } = "111111111111@qq.com";
        /// <summary>
        /// 发件人邮箱密码
        /// </summary>
        public string MailPassword { get; set; } = "qq网页邮箱-设置-账号-搜生成授权-获取密码";
        /// <summary>
        /// 测试的收件人邮箱地址
        /// </summary>
        public string ReceiveMail { get; set; } = "接收预警的的邮箱";
        /// <summary>
        /// 测试邮件标题
        /// </summary>
        public string MailSubject { get; set; } = "预警邮件";
        /// <summary>
        /// 测试邮件内容
        /// </summary>
        public string MailBody { get; set; } = "预警邮件测试";
    }
}
