using System;
using System.IO;
using System.Reflection;
using System.Xml;
using CrabRobot.Config;
using Newtonsoft.Json;

namespace CrabRobot.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigManager<T>
    {
        private T XmlConfigModel;
        private string ConfigPath;
        public ConfigManager(string configPath)
        {
            ConfigPath = AppDomain.CurrentDomain.BaseDirectory +@"Config\"+ configPath;
        }

        private void Init()
        {
            if (!File.Exists(ConfigPath))
            {
                var xmlstr = XmlUtil.XmlSerialize(default(T));
                var dir = Path.GetDirectoryName(ConfigPath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                File.WriteAllText(ConfigPath, xmlstr);
            }
            XmlConfigModel = default(T);
            XmlDocument doc = new XmlDocument();
            doc.Load(ConfigPath);
            string json = JsonConvert.SerializeXmlNode(doc);
            //Json序列化为数据
            XmlConfigModel = JsonConvert.DeserializeObject<T>(json);
        }
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns></returns>
        public T GetConfig()
        {
            Init();
            return XmlConfigModel;
        }

        public T GetConfigValueBykey<T>(string key)
        {
            Init();
            PropertyInfo property = XmlConfigModel.GetType().GetProperty(key);
            if (property == null)
            {
                throw new Exception($"不存在key{key}");
            }
            var value = property.GetValue(XmlConfigModel, null);
            T result = default;
            result = (T)Convert.ChangeType(value, typeof(T));
            return result;
        }

        public void SaveConfig(string key, string value)
        {
            Init();
            PropertyInfo property = XmlConfigModel.GetType().GetProperty(key);
            if (property == null)
            {
                throw new Exception($"不存在key{key}");
            }

            var data = Convert.ChangeType(value, property.PropertyType);
            property.SetValue(XmlConfigModel, data, null);
            var xml = JsonConvert.DeserializeXmlNode(JsonConvert.SerializeObject(XmlConfigModel));
            xml.Save(ConfigPath);
        }
    }
}
