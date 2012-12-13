using System;
using System.Xml.Serialization;
using System.IO;

namespace LanExchange.Utils
{
    public static class SerializeUtils
    {
        public static string SerializeTypeToXML(object obj)
        {
            if (obj == null) return null;
            var ser = new XmlSerializer(obj.GetType());
            using (var sw = new StringWriter())
            {
                ser.Serialize(sw, obj);
                return sw.ToString();
            }
        }

        public static void SerializeTypeToXMLFile(string FileName, object obj)
        {
            if (obj == null) return;
            var writer = new XmlSerializer(obj.GetType());
            using (var file = new StreamWriter(FileName))
            {
                writer.Serialize(file, obj);
                file.Close();
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
