using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrabRobot.Util
{
    public static class RandomUtil
    {
        /// <summary>
        /// 返回一个值 大于等于start,小于end
        /// start和end相等则返回start
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int RandomInt(int start,int end)
        {
            Random r = new Random();
            return r.Next(start, end);
        }
        public static string RandomList(this List<string> ts)
        {
            var value = RandomInt(0,ts.Count);
            return ts[value];
        }
        /// <summary>
        /// 打乱集合顺序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> RandomSort<T>(this List<T> list)
        {
            var random = new Random();
            var newList = new List<T>();
            foreach (var item in list)
            {
                newList.Insert(random.Next(0, newList.Count), item);
            }
            return newList;
        }
    }
}
