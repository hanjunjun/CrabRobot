using HBNiuBi.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HBNiuBi.Config
{
    /// <summary>
    /// app入参管理器
    /// </summary>
    public static class AppArgsManager
    {
        #region Fields
        private static List<ArgsModel> _appArgsConfig;
        #endregion

        #region 属性
        /// <summary>
        /// 获取当前程序的日志级别
        /// </summary>
        public static Const.ArgsValue.LogLevel LogLevel = GetValueByKey(Const.ArgsKey.LogLevel, Const.ArgsValue.LogLevel.Info);
        #endregion

        #region 初始化
        static AppArgsManager()
        {
            var cmd = Environment.GetCommandLineArgs();
            var args = cmd.Where((val, idx) => idx != 0).ToList();
            if (cmd.Length == 0)
            {
                _appArgsConfig = new List<ArgsModel>();
            }
            _appArgsConfig = args.Select(x =>
            {
                var item = x.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                return new ArgsModel
                {
                    Key = item[0].Trim(),
                    Value = item.Length >= 2 ? item[1].Trim() : string.Empty
                };
            }).ToList();
        }


        #endregion

        #region Methods
        /// <summary>
        /// 根据传入的参数生成模型
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [Obsolete("此方法已废弃，现在AppArgsManager初始化时会自动加载全局配置")]
        public static void GenerateConfig(this string[] args)
        {
            try
            {
                if (args == null || args.Length == 0)
                {
                    _appArgsConfig = new List<ArgsModel>();
                }
                _appArgsConfig = args?.Select(x =>
                {
                    return new ArgsModel
                    {
                        Key = x.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)[0].Trim(),
                        Value = x.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim()
                    };
                }).ToList();
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// 根据入参key获取value
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="key">入参key</param>
        /// <param name="defaultValue">如果查询不到数据，设置一个默认值</param>
        /// <returns></returns>
        public static T GetValueByKey<T>(string key, T defaultValue = default)
        {
            try
            {
                if (_appArgsConfig == null || _appArgsConfig.Count == 0)
                {
                    return defaultValue;
                }

                var value = _appArgsConfig.FirstOrDefault(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
                if (value.IsNotNull())
                {
                    if (typeof(T).IsEnum)
                    {
                        return (T)Enum.Parse(typeof(T), value?.Value!, true);
                    }
                    return (T)Convert.ChangeType(value?.Value, typeof(T));
                }
                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 判断入参列表中是否存在key和value
        /// 不区分大小写
        /// </summary>
        /// <param name="key">配置key</param>
        /// <param name="value">配置value</param>
        /// <returns></returns>
        public static bool Exist(string key, string value)
        {
            try
            {
                if (_appArgsConfig == null || _appArgsConfig.Count == 0)
                {
                    return false;
                }
                //配置中包含key和value，且必须只有一条，多条记录直接返回false，false会进入最后分支显示主程序窗口
                return _appArgsConfig.Any(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase) &&
                                               x.Value.Equals(value, StringComparison.OrdinalIgnoreCase)) ||
                                               _appArgsConfig.Count(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase) &&
                                               x.Value.Equals(value, StringComparison.OrdinalIgnoreCase)) == 1;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断入参列表中是否存在key
        /// 不区分大小写
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exist(string key)
        {
            try
            {
                if (_appArgsConfig == null || _appArgsConfig.Count == 0)
                {
                    return false;
                }
                return _appArgsConfig.Any(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
