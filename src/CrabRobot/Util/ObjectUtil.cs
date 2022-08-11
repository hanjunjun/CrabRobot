using System;

namespace CrabRobot.Util
{
    /// <summary>
    /// 对象工具类
    /// </summary>
    public static class ObjectUtil
    {
        #region Fields

        #endregion

        #region Ctor

        #endregion

        #region Properties

        #endregion

        #region Methods
        /// <summary>
        /// 如果是null返回true，如果不是null返回false
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        //public static bool IsNull(this object[] objs)
        //{
        //    var length = objs.Length;
        //    for (int i = 0; i < length; i++)
        //    {
        //        object obj = objs[i];
        //        if (IsEmpty(obj))
        //        {
        //            return true;
        //        }
        //    }
        //}

        /// <summary>
        /// 如果不是null返回true，是null返回false
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        //public static bool IsEmpty(this object obj)
        //{
        //    if (obj == null)
        //    {
        //        return true;
        //    }
        //    else if (obj is Array)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        //public static bool IsNotEmpty()
        //{

        //}

        #endregion
    }
}
