using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HBNiuBi.Config;

namespace HBNiuBi.Util
{
    public class ToolSettingsConfig
    {
        private ToolSettingsModel JsonConfigData;
        private string ConfigPath;
        private ToolSettingsConfig()
        {

        }
        public ToolSettingsConfig(string configPath) : this()
        {
            ConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"Config\" + configPath;
        }

        private void Init()
        {
            if (!File.Exists(ConfigPath))
            {
                ToolSettingsModel result = new ToolSettingsModel();
                var jsonStr = JsonHelper.ModelToStr(result);
                var dir = Path.GetDirectoryName(ConfigPath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                File.WriteAllText(ConfigPath, jsonStr);
            }
            //Json序列化为数据
            JsonConfigData = JsonHelper.StrToModel<ToolSettingsModel>(File.ReadAllText(ConfigPath));
        }
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns></returns>
        public ToolSettingsModel GetConfig()
        {
            Init();
            return JsonConfigData;
        }
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="value"></param>
        public void SaveOrUpdateConfig(ToolSettingsModel value)
        {
            var jsonStr = JsonHelper.ModelToStr(value);
            File.WriteAllText(ConfigPath, jsonStr);
        }
    }
}
