using System;
using System.Xml.Serialization;
using System.IO;

namespace LanExchange.Utils
{
    public static class SerializeUtils
    {
        public static string SerializeTypeToXML(object obj, Type tp)
        {
            XmlSerializer ser = new XmlSerializer(tp);
            using (StringWriter sw = new StringWriter())
            {
                ser.Serialize(sw, obj);
                return sw.ToString();
            }
        }

        public static void SerializeTypeToXMLFile(string FileName, object obj, Type tp)
        {
            
            XmlSerializer writer = new XmlSerializer(tp);
            using (StreamWriter file = new StreamWriter(FileName))
            {
                writer.Serialize(file, obj);
                file.Close();
            }
        }

        public static object DeserializeObjectFromXML(string xml, Type tp)
        {
            XmlSerializer ser = new XmlSerializer(tp);
            using (TextReader tr = new StringReader(xml))
            {
                object obj = ser.Deserialize(tr);
                return obj;
            }
        }

        public static object DeserializeObjectFromXMLFile(string FileName, Type tp)
        {
            XmlSerializer ser = new XmlSerializer(tp);
            using (StreamReader tr = new StreamReader(FileName))
            {
                object obj = ser.Deserialize(tr);
                return obj;
            }
        }
    }
}
