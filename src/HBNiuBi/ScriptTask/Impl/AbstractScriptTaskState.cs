using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBNiuBi.ScriptTask.Impl
{
    /// <summary>
    /// 脚本状态抽象类模板
    /// </summary>
    public abstract class AbstractScriptTaskState : IScriptTaskState
    {
        private readonly ScriptTaskManager scriptTaskManager;
        public AbstractScriptTaskState(ScriptTaskManager scriptTaskManager)
        {
            this.scriptTaskManager = scriptTaskManager;
        }
        public abstract void Continue();

        public abstract void Init();

        public abstract void Start();

        public abstract void Stop();

        public abstract void Suspend();
    }
}
