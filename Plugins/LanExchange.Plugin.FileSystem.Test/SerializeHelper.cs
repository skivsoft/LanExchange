using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace LanExchange.Plugin.FileSystem.Test
{
    public static class SerializeHelper
    {
        public static string SerializeObjectToXml(object obj)
        {
            if (obj == null) return null;
            var ser = new XmlSerializer(obj.GetType());
            using (var sw = new StringWriter(CultureInfo.InvariantCulture))
            {
                ser.Serialize(sw, obj);
                return sw.ToString();
            }
        }
    }
}
