using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrabRobot.ScriptTask
{
    public class TimeSleepSchedulerExecutor
    {
        private readonly  List<TimeSleep> timeSleeps = new List<TimeSleep>();

        /// <summary>
        /// 
        /// </summary>
        public TimeSleepSchedulerExecutor()
        {

        }

        /// <summary>
        /// 暂停之后要重置定时器的状态
        /// </summary>
        public void Init()
        {
            foreach (var timeSleep in timeSleeps)
            {
                timeSleep.Init();
            }
        }
        /// <summary>
        /// 生成一个定时器，并放到list里
        /// </summary>
        /// <returns></returns>
        public TimeSleep GenTimeSleep()
        {
            var timeSleep = new TimeSleep();
            timeSleeps.Add(timeSleep);
            return timeSleep;
        }
        /// <summary>
        /// 退出所有定时器
        /// </summary>
        public void OverAll()
        {
            foreach(var timeSleep in timeSleeps)
            {
                timeSleep.Over();
            }
        }
    }

    public sealed class TimeSleep
    {
        private volatile bool _isSleep = true;

        public void Over()
        {
            _isSleep = false;
        }

        public void Init()
        {
            _isSleep = true;
        }

        public void Sleep(int ms)
        {
            const int baseTime = 10;

            if (ms < baseTime)
            {
                System.Threading.Thread.Sleep(ms);
            }
            else
            {
                int loopCount = ms / baseTime;  // 除以 baseTime，代表是 baseTime 毫秒的多少倍
                int surplusMS = ms % baseTime;  // 剩余毫秒数

                while (_isSleep && loopCount > 0)
                {
                    System.Threading.Thread.Sleep(baseTime);
                    --loopCount;
                }

                if (_isSleep && surplusMS > 0)
                {
                    System.Threading.Thread.Sleep(surplusMS);
                }
            }
        }
    }
}
