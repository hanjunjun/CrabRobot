using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrabRobot.ScriptTask.Impl
{
    /// <summary>
    /// 脚本正在运行中状态
    /// </summary>
    public class ScriptTaskRunningState : AbstractScriptTaskState
    {
        public ScriptTaskRunningState(ScriptTaskManager scriptTaskManager) : base(scriptTaskManager)
        {
        }

        public override void Continue()
        {
            throw new NotImplementedException();
        }

        public override void Init()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        public override void Suspend()
        {
            throw new NotImplementedException();
        }
    }
}
