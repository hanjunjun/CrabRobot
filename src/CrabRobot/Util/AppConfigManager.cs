using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace CrabRobot.Util
{
    public class AppConfigManager
    {
        #region "声明变量"

        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">节点名称[如[TypeName]]</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="def">值</param>
        /// <param name="retval">stringbulider对象</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        private static string strFilePath = AppDomain.CurrentDomain.BaseDirectory + "DeployTool.ini";//获取INI文件路径

        #endregion
        static Configuration config = null;
        static AppConfigManager()
        {
            //config = ConfigurationManager.OpenExeConfiguration(
            //ConfigurationUserLevel.None);
            if (!File.Exists(strFilePath))
            {
                FileStream fs = new FileStream(strFilePath, FileMode.Append, FileAccess.Write);
                //StreamWriter sr = new StreamWriter(fs);
                //sr.WriteLine(Log);//开始写入值
                //sr.Close();
                fs.Close();
            }
        }

        /// <summary>
        /// //添加键值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddAppSetting(string key, string value)
        {
            config.AppSettings.Settings.Add(key, value);
            config.Save();
        }

        /// <summary>
        /// //修改键值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SaveAppSetting(string key, string value)
        {
            var strSec = Path.GetFileNameWithoutExtension(strFilePath);
            WritePrivateProfileString(strSec, key, value, strFilePath);
        }
        private static string ContentValue(string Section, string key)
        {

            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }
        /// <summary>
        /// //获得键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetAppSetting<T>(string key)
        {
            if (File.Exists(strFilePath))//读取时先要判读INI文件是否存在
            {

                var strSec = Path.GetFileNameWithoutExtension(strFilePath);
                var value = ContentValue(strSec, key);
                T result = default;
                result = (T)Convert.ChangeType(value, typeof(T));
                return result;
            }
            throw new Exception("DeployTool.ini文件不存在");
        }

        /// <summary>
        /// //移除键值
        /// </summary>
        /// <param name="key"></param>
        public static void DelAppSetting(string key)
        {
            config.AppSettings.Settings.Remove(key);
            config.Save();
        }

        public static ArrayList GetXmlElements(string strElem)
        {
            ArrayList list = new ArrayList();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
            XmlNodeList listNode = xmlDoc.SelectNodes(strElem);
            foreach (XmlElement el in listNode)
            {
                list.Add(el.InnerText);
            }
            return list;
        }
    }
}
