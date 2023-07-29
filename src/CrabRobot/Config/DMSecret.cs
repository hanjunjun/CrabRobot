using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrabRobot.Config
{
    public class DMSecret
    {
        public string Code { get; set; }
        public string Ver { get; set; }
        /// <summary>
        /// 是否启用调试模式
        /// </summary>
        public bool Debug { get; set; } = false;
    }
}
