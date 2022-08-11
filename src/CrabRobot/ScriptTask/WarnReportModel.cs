using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrabRobot.ScriptTask
{
    public class WarnReportModel
    {
        /// <summary>
        /// 测试邮件标题
        /// </summary>
        public string MailSubject { get; set; } = "预警邮件";
        /// <summary>
        /// 测试邮件内容
        /// </summary>
        public string MailBody { get; set; } = "预警邮件测试";
        /// <summary>
        /// 邮件内的图片
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public string FilePath { get; set; }
    }
}
