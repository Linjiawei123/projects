using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MicroService.Method
{
    /// <summary>
    /// 帮助类：序列化/反序列化
    /// </summary>
    public static class SerializationHelper
    {
        #region JSON 序列化
        /// <summary>
        /// 序列化对象为JSON格式
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="dateFormat">日期序列化格式</param>
        /// <param name="ignoreNull">是否忽略NULL值</param>
        /// <returns>Json字符串</returns>
        public static string SerializeToJson(object obj, string dateFormat = "yyyy-MM-dd HH:mm:ss", bool ignoreNull = false)
        {
            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
            {
                DateFormatString = dateFormat,
                NullValueHandling = ignoreNull ? NullValueHandling.Ignore : NullValueHandling.Include
            });
        }
        /// <summary>
        /// 反序列化JSON对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns>json对象</returns>
        public static T DeserializeFromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        #endregion

        #region Xml序列化
        private static void SerializeXmlInternal(Stream stream, object obj, Encoding encoding)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));
            XmlSerializer serializer = new(obj.GetType());
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                NewLineChars = "\r\n",
                Encoding = encoding,
                IndentChars = "    "
            };
            using XmlWriter writer = XmlWriter.Create(stream, settings);
            serializer.Serialize(writer, obj);
            writer.Close();
        }
        /// <summary>
        /// 将一个对象序列化为XML字符串
        /// </summary>
        public static string SerializeXml(object obj, Encoding encoding)
        {
            using MemoryStream stream = new();
            SerializeXmlInternal(stream, obj, encoding);
            stream.Position = 0;
            using StreamReader reader = new(stream, encoding);
            return reader.ReadToEnd();
        }
        /// <summary>
        /// 将一个对象按XML序列化的方式写入到一个文件
        /// </summary>
        public static void SerializeXmlToFile(object obj, string path, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            using FileStream file = new(path, FileMode.Create, FileAccess.Write);
            SerializeXmlInternal(file, obj, encoding);
        }
        /// <summary>
        /// 从XML字符串中反序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="xmlString">xml字符串</param>
        /// <returns>对象</returns>
        public static T DeserializeXml<T>(string xmlString) where T : new()
        {
            try
            {
                using StringReader sr = new(xmlString);
                XmlSerializer xmldes = new(typeof(T));
                return (T)xmldes.Deserialize(sr);
            }
            catch
            {
                return new T();
            }
        }
        /// <summary>
        /// 读入一个文件，并按XML的方式反序列化对象。
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="path">保存路径</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>对象值</returns>
        public static T DeserializeXmlFromFile<T>(string path, Encoding encoding) where T : new()
        {
            string xml = string.Empty;
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));
            if (File.Exists(path))
                xml = File.ReadAllText(path, encoding);
            return DeserializeXml<T>(xml);
        }
        #endregion
    }
}
