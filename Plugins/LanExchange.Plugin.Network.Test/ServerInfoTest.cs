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
        [SetUp]
        protected void SetUp()
        {
            m_Info = new ServerInfo();
        }

        [TearDown]
        protected void TearDown()
        {
            m_Info = null;
        }

        private ServerInfo m_Info;

        [Test]
        public void TestConstructor()
        {
            m_Info.Name = "QQQ";
            m_Info.Comment = "WWW";
            m_Info.Version.PlatformId = 1;
            m_Info.Version.Type = 2;
            m_Info.Version.Major = 3;
            m_Info.Version.Minor = 4;
            Assert.AreEqual("QQQ", m_Info.Name);
            Assert.AreEqual("WWW", m_Info.Comment);
            Assert.AreEqual(1, m_Info.Version.PlatformId);
            Assert.AreEqual(2, m_Info.Version.Type);
            Assert.AreEqual(3, m_Info.Version.Major);
            Assert.AreEqual(4, m_Info.Version.Minor);
        }


        [Test]
        public void TestSerializeBinary()
        {
            ServerInfo info =
                ServerInfo.FromNetApi32(new SERVER_INFO_101 {name = "QQQ", comment = "WWW"});
            // SI.Name = "QQQ";

            // SI.Comment = "WWW";

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

        // m_Info.ResetUtcUpdated();

        // Assert.IsEmpty(m_Info.GetTopicalityText());

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
            m_Info.ResetUtcUpdated();
            Assert.AreEqual(current, m_Info.UtcUpdated);
            m_Info.UtcUpdated = current;
            Assert.AreEqual(current, m_Info.UtcUpdated);
        }
    }
}