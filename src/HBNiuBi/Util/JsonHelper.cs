﻿/************************************************************************
 * 文件名：JsonHelper
 * 文件功能描述：xx控制层
 * 作    者：  韩俊俊
 * 创建日期：2018/12/21 14:42:11
 * 修 改 人：
 * 修改日期：
 * 修改原因：
 * Copyright (c) 2017 Han . All Rights Reserved. 
 * ***********************************************************************/

using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace HBNiuBi.Util
{
    public class JsonHelper
    {
        public static string ConvertJsonString(string str)
        {
            //格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }
        /// <summary>
        /// 将Json模型转换成json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ModelToStr(object obj)
        {
            return ConvertJsonString(JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 将字符串转换成Json模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T StrToModel<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json == null ? "" : json);
        }

        /// <summary>
        /// 将Json模型转换成byte[]数组
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static byte[] ModelToBytes(object item)
        {
            var jsonString = JsonConvert.SerializeObject(item);

            return Encoding.UTF8.GetBytes(jsonString);
        }

        //public static string ObjToJson(object obj)
        //{
        //    if (obj == null)
        //        return string.Empty;
        //    JavaScriptSerializer jss = new JavaScriptSerializer();
        //    return jss.Serialize(obj);
        //}

        //public static T ObjFromJson<T>(string cookie)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(cookie))
        //            return default(T);
        //        JavaScriptSerializer jss = new JavaScriptSerializer();
        //        return jss.Deserialize<T>(cookie);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }
        //}
    }
}
