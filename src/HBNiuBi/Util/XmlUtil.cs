using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace HBNiuBi.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlUtil
    {
        /// <summary>
        /// 对象序列化成 XML String
        /// </summary>
        public static string XmlSerialize<T>(T obj)
        {
            string xmlString = string.Empty;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                xmlSerializer.Serialize(ms, obj);
                xmlString = Encoding.UTF8.GetString(ms.ToArray());
            }
            return xmlString;
        }

        /// <summary>
        /// XML String 反序列化成对象
        /// </summary>
        public static T XmlDeserialize<T>(string xmlString)
        {
            T t = default;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
            {
                using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                {
                    object obj = xmlSerializer.Deserialize(xmlReader);
                    t = (T)obj;
                }
            }
            return t;
        }
    }
}
