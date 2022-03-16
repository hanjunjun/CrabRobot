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
    public class JsonConfig
    {
        private List<ScriptConfig> JsonConfigData;
        private string ConfigPath;
        public JsonConfig()
        {

        }
        public JsonConfig(string configPath) : this()
        {
            ConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"Config\" + configPath;
        }

        private void Init()
        {
            if (!File.Exists(ConfigPath))
            {
                List<ScriptConfig> result = new List<ScriptConfig>();
                var jsonStr = JsonHelper.ModelToStr(result);
                var dir = Path.GetDirectoryName(ConfigPath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                File.WriteAllText(ConfigPath, jsonStr);
            }
            //Json序列化为数据
            JsonConfigData = JsonHelper.StrToModel<List<ScriptConfig>>(File.ReadAllText(ConfigPath));
        }
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns></returns>
        public List<ScriptConfig> GetConfig()
        {
            Init();
            return JsonConfigData;
        }

        public ScriptConfig GetConfigValueBykey(string scriptId)
        {
            Init();
            if (JsonConfigData == null)
            {
                return new ScriptConfig();
            }
            var data = JsonConfigData.Where(x =>
             {
                 var id = scriptId;
                 PropertyInfo property = x.GetType().GetProperty("ScriptId");
                 if (property == null)
                 {
                     return false;
                 }
                 var value = property.GetValue(x, null);
                 if (value.Equals(scriptId))
                 {
                     return true;
                 }
                 return false;
             }).ToList().FirstOrDefault();
            if (data == null)
            {
                return new ScriptConfig();
            }
            return data;
        }
        public void AddConfig(ScriptConfig value)
        {
            Init();
            JsonConfigData.Add(value);
            var jsonStr = JsonHelper.ModelToStr(JsonConfigData);
            File.WriteAllText(ConfigPath, jsonStr);
        }
        public void DeleteConfig(string scriptId)
        {
            Init();
            var item = JsonConfigData.Where(x =>
            {
                var id = scriptId;
                PropertyInfo property = x.GetType().GetProperty("ScriptId");
                if (property == null)
                {
                    return false;
                }
                var value = property.GetValue(x, null);
                if (value.Equals(scriptId))
                {
                    return true;
                }
                return false;
            }).ToList();
            if (item == null)
            {
                return;
            }
            foreach(var data in item)
            {
                JsonConfigData.Remove(data);
            }
            var jsonStr = JsonHelper.ModelToStr(JsonConfigData);
            File.WriteAllText(ConfigPath, jsonStr);
        }
        public void UpdateConfig(string scriptId, ScriptConfig value)
        {
            Init();
            var item= JsonConfigData.Where(x=>
            {
                var id = scriptId;
                PropertyInfo property = x.GetType().GetProperty("ScriptId");
                if (property == null)
                {
                    return false;
                }
                var value = property.GetValue(x, null);
                if (value.Equals(scriptId))
                {
                    return true;
                }
                return false;
            }).ToList().FirstOrDefault();
            if (item == null)
            {
                throw new Exception($"没有找到配置项：{scriptId}");
            }
            JsonConfigData.Remove(item);
            JsonConfigData.Add(value);
            var jsonStr = JsonHelper.ModelToStr(JsonConfigData);
            File.WriteAllText(ConfigPath, jsonStr);
        }
    }
}
