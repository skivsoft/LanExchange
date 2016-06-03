using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace LanExchange.Presentation.WinForms.Helpers
{
    public static class SerializeHelper
    {
        public static void ForceCreatePath(string path)
        {
            if (!Directory.Exists(path))
            {
                ForceCreatePath(Path.GetDirectoryName(path));
                Directory.CreateDirectory(path);
            }
        }

        public static void SerializeObjectToBinaryFile(string fileName, object obj)
        {
            ForceCreatePath(Path.GetDirectoryName(fileName));
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

        public static void SerializeObjectToXmlFile(string fileName, object obj)
        {
            if (obj == null) return;
            ForceCreatePath(Path.GetDirectoryName(fileName));
            var writer = new XmlSerializer(obj.GetType());
            using (var file = new StreamWriter(fileName))
            {
                writer.Serialize(file, obj);
            }
        }

        public static void SerializeObjectToXmlFile(string fileName, object obj, Type[] extraTypes)
        {
            if (obj == null) return;
            ForceCreatePath(Path.GetDirectoryName(fileName));
            var writer = new XmlSerializer(obj.GetType(), extraTypes);
            using (var file = new StreamWriter(fileName))
            {
                writer.Serialize(file, obj);
            }
        }

        public static object DeserializeObjectFromXml(string xml, Type tp)
        {
            var ser = new XmlSerializer(tp);
            using (TextReader tr = new StringReader(xml))
            {
                object obj = ser.Deserialize(tr);
                return obj;
            }
        }

        public static object DeserializeObjectFromXmlFile(string fileName, Type tp, Type[] extraTypes)
        {
            var ser = new XmlSerializer(tp, extraTypes);
            using (var tr = new StreamReader(fileName))
            {
                object obj = ser.Deserialize(tr);
                return obj;
            }
        }

        public static object DeserializeObjectFromXmlFile(string fileName, Type tp)
        {
            var ser = new XmlSerializer(tp);
            using (var tr = new StreamReader(fileName))
            {
                object obj = ser.Deserialize(tr);
                return obj;
            }
        }

        /// <summary>
        /// URL: http://www.dailycoding.com/Posts/convert_image_to_base64_string_and_base64_string_to_image.aspx
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                var imageBytes = ms.ToArray();
                // Convert byte[] to Base64 String
                return Convert.ToBase64String(imageBytes);
            }
        }

        public static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            var ms = new MemoryStream(imageBytes, 0, 
            imageBytes.Length);
            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            return Image.FromStream(ms, true);
        }
    }
}