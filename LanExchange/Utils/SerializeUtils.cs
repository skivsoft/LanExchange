using System;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LanExchange.Utils
{
    public static class SerializeUtils
    {
        public static void SerializeObjectToBinaryFile(string FileName, object obj)
        {
            var stream = File.Open(FileName, FileMode.Create);
            var bformatter = new BinaryFormatter();
            bformatter.Serialize(stream, obj);
            stream.Close();
        }

        public static object DeserializeObjectFromBinaryFile(string FileName)
        {
            var stream = File.Open(FileName, FileMode.Open);
            var bformatter = new BinaryFormatter();
            var Result = bformatter.Deserialize(stream);
            stream.Close();
            return Result;
        }

        public static string SerializeObjectToXML(object obj)
        {
            if (obj == null) return null;
            var ser = new XmlSerializer(obj.GetType());
            using (var sw = new StringWriter())
            {
                ser.Serialize(sw, obj);
                return sw.ToString();
            }
        }

        public static void SerializeObjectToXMLFile(string FileName, object obj)
        {
            if (obj == null) return;
            var writer = new XmlSerializer(obj.GetType());
            using (var file = new StreamWriter(FileName))
            {
                writer.Serialize(file, obj);
            }
        }

        public static object DeserializeObjectFromXML(string xml, Type tp)
        {
            var ser = new XmlSerializer(tp);
            using (TextReader tr = new StringReader(xml))
            {
                object obj = ser.Deserialize(tr);
                return obj;
            }
        }

        public static object DeserializeObjectFromXMLFile(string FileName, Type tp)
        {
            var ser = new XmlSerializer(tp);
            using (var tr = new StreamReader(FileName))
            {
                object obj = ser.Deserialize(tr);
                return obj;
            }
        }
    }
}
