using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrabRobot.ScriptTask
{
    /// <summary>
    /// 脚本状态行为
    /// </summary>
    public interface IScriptTaskState
    {
        /// <summary>
        /// 初始状态
        /// </summary>
        void Init();
        /// <summary>
        /// 启动
        /// </summary>
        void Start();
        /// <summary>
        /// 暂停
        /// </summary>
        void Suspend();
        /// <summary>
        /// 继续
        /// </summary>
        /// <summary>
        /// 停止
        /// </summary>
        void Stop();
    }
}
