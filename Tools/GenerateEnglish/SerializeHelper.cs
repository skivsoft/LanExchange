using System;
using System.IO;
using System.Xml.Serialization;

namespace GenerateEnglish
{
    public static class SerializeUtils
    {

        public static object DeserializeObjectFromXmlFile(string fileName, Type tp)
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