using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LanExchange.Utils;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Tests
{
    [TestClass]
    public class ServerInfoTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            var SI = new ServerInfo();
            SI.Name = "QQQ";
            SI.Comment = "WWW";
            SI.PlatformID = 1;
            SI.Type = 2;
            SI.VersionMajor = 3;
            SI.VersionMinor = 4;
            Assert.AreEqual("QQQ", SI.Name);
            Assert.AreEqual("WWW", SI.Comment);
            Assert.AreEqual<uint>(1, SI.PlatformID);
            Assert.AreEqual<uint>(2, SI.Type);
            Assert.AreEqual<uint>(3, SI.VersionMajor);
            Assert.AreEqual<uint>(4, SI.VersionMinor);
        }

        
        [TestMethod]
        public void TestSerializeBinary()
        {
            var SI = ServerInfo.FromNetApi32(new NetApi32.SERVER_INFO_101 {sv101_name = "QQQ", sv101_comment = "WWW"});
            //SI.Name = "QQQ";
            //SI.Comment = "WWW";
            var stream = new MemoryStream();
            var bformatter = new BinaryFormatter();
            // try serialize
            bformatter.Serialize(stream, SI);
            // try deserialize
            stream.Position = 0;
            var Result = bformatter.Deserialize(stream);
            Assert.IsNotNull(Result, "Deserialize returns null");
            Assert.IsInstanceOfType(Result, typeof(ServerInfo), "Deserialize wrong type");
            Assert.AreEqual("QQQ", SI.Name);
            Assert.AreEqual("WWW", SI.Comment);
            stream.Close();
        }

        [TestMethod]
        public void TestSerializeXML()
        {
            var SI = new ServerInfo();
            SI.Name = "QQQ";
            SI.Comment = "WWW";
            SI.PlatformID = 1;
            SI.Type = 2;
            SI.VersionMajor = 3;
            SI.VersionMinor = 4;
            // try serialize
            var ser = new XmlSerializer(SI.GetType());
            string content;
            using (var sw = new StringWriter())
            {
                ser.Serialize(sw, SI);
                content = sw.ToString();
            }
            // try deserialize
            TextReader tr = new StringReader(content);
            var Result = ser.Deserialize(tr);
            tr.Close();
            // check deserialize result
            Assert.IsNotNull(Result, "Deserialize returns null");
            Assert.IsInstanceOfType(Result, typeof(ServerInfo), "Deserialize wrong type");
            Assert.AreEqual("QQQ", SI.Name);
            Assert.AreEqual("WWW", SI.Comment);
        }
    }
}
