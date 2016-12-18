using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LanExchange.Plugin.Network.NetApi;
using NUnit.Framework;

namespace LanExchange.Plugin.Network
{
    [TestFixture]
    public class ServerInfoTest
    {
        private ServerInfo info;

        [SetUp]
        public void SetUp()
        {
            info = new ServerInfo();
        }

        [TearDown]
        public void TearDown()
        {
            info = null;
        }

        [Test]
        public void TestConstructor()
        {
            info.Name = "QQQ";
            info.Comment = "WWW";
            info.Version.PlatformId = 1;
            info.Version.Type = 2;
            info.Version.Major = 3;
            info.Version.Minor = 4;
            Assert.AreEqual("QQQ", info.Name);
            Assert.AreEqual("WWW", info.Comment);
            Assert.AreEqual(1, info.Version.PlatformId);
            Assert.AreEqual(2, info.Version.Type);
            Assert.AreEqual(3, info.Version.Major);
            Assert.AreEqual(4, info.Version.Minor);
        }

        [Test]
        public void TestSerializeBinary()
        {
            ServerInfo info = ServerInfo.FromNetApi32(new SERVER_INFO_101 { name = "QQQ", comment = "WWW" });

            var stream = new MemoryStream();
            var bformatter = new BinaryFormatter();

            // try serialize
            bformatter.Serialize(stream, info);
            
            // try deserialize
            stream.Position = 0;
            object result = bformatter.Deserialize(stream);
            Assert.IsNotNull(result, "Deserialize returns null");
            Assert.IsInstanceOf(typeof(ServerInfo), result, "Deserialize wrong type");
            Assert.AreEqual("QQQ", info.Name);
            Assert.AreEqual("WWW", info.Comment);
            stream.Close();
        }

        // [Test]
        // public void TestGetTopicallity()
        // {
        // info.ResetUtcUpdated();
        // Assert.IsEmpty(info.GetTopicalityText());
        // var info1 = MockRepository.GenerateStub<ServerInfo>();
        // var info2 = MockRepository.GenerateStub<ServerInfo>();
        // var current = new DateTime(2012, 01, 22, 18, 30, 40);
        // var past1 = new DateTime(current.Year, current.Month, current.Day - 1, current.Hour - 10, current.Minute - 20, current.Second);
        // var past2 = new DateTime(current.Year, current.Month, current.Day, current.Hour, current.Minute - 20, current.Second - 30);
        // info1.Stub(x => x.GetTopicality()).Return(current - past1);
        // info2.Stub(x => x.GetTopicality()).Return(current - past2);
        // // checks day, hour, min
        // Assert.AreEqual("1d 10h 20m", info1.GetTopicalityText());
        // // checks min, sec
        // Assert.AreEqual("20m 30s", info2.GetTopicalityText());
        // }
        // [Test]
        // public void TestCompareTo()
        // {
        // Assert.AreEqual(0, (new ServerInfo {Name = "AA"}).CompareTo((new ServerInfo {Name = "aa"})));
        // Assert.Less((new ServerInfo {Name = "AA"}).CompareTo((new ServerInfo {Name = "ba"})), 0);
        // Assert.Greater((new ServerInfo { Name = "BB" }).CompareTo((new ServerInfo { Name = "aA" })), 0);
        // }
        [Test]
        public void TestUtcUpdated()
        {
            DateTime current = DateTime.UtcNow;
            info.ResetUtcUpdated();
            Assert.AreEqual(current, info.UtcUpdated);
            info.UtcUpdated = current;
            Assert.AreEqual(current, info.UtcUpdated);
        }
    }
}