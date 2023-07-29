using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrabRobot.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class TimeUtil
    {
        public static bool IsInTimeSlot(DateTime dateTime, DateTime startTime, DateTime endTime)
        {
            return dateTime >= startTime && dateTime < endTime;
        }
    }
}
