using System;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LanExchange.Utils
{
    public static class SerializeUtils
    {
        public static void SerializeObjectToBinaryFile(string fileName, object obj)
        {
            var stream = File.Open(fileName, FileMode.Create);
            var bformatter = new BinaryFormatter();
            bformatter.Serialize(stream, obj);
            stream.Close();
        }

        public static object DeserializeObjectFromBinaryFile(string fileName)
        {
            var stream = File.Open(fileName, FileMode.Open);
            var bformatter = new BinaryFormatter();
            var result = bformatter.Deserialize(stream);
            stream.Close();
            return result;
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

        public static void SerializeObjectToXMLFile(string fileName, object obj)
        {
            if (obj == null) return;
            var writer = new XmlSerializer(obj.GetType());
            using (var file = new StreamWriter(fileName))
            {
                writer.Serialize(file, obj);
            }
        }

        public static void SerializeObjectToXMLFile(string fileName, object obj, Type[] extraTypes)
        {
            if (obj == null) return;
            var writer = new XmlSerializer(obj.GetType(), extraTypes);
            using (var file = new StreamWriter(fileName))
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

        public static object DeserializeObjectFromXMLFile(string fileName, Type tp)
        {
            var ser = new XmlSerializer(tp);
            using (var tr = new StreamReader(fileName))
            {
                object obj = ser.Deserialize(tr);
                return obj;
            }
        }
    }
}