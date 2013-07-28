using System;
using System.Xml.Serialization;
using LanExchange.Utils;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace LanExchange.Tests.Utils
{
    [TestFixture]
    public class ServerInfoTest
    {
        private ServerInfo m_Info;

        [SetUp]
        protected  void SetUp()
        {
            m_Info = new ServerInfo();
        }

        [Test]
        public void TestConstructor()
        {
            m_Info.Name = "QQQ";
            m_Info.Comment = "WWW";
            m_Info.PlatformID = 1;
            m_Info.Type = 2;
            m_Info.VersionMajor = 3;
            m_Info.VersionMinor = 4;
            Assert.AreEqual("QQQ", m_Info.Name);
            Assert.AreEqual("WWW", m_Info.Comment);
            Assert.AreEqual(1, m_Info.PlatformID);
            Assert.AreEqual(2, m_Info.Type);
            Assert.AreEqual(3, m_Info.VersionMajor);
            Assert.AreEqual(4, m_Info.VersionMinor);
        }

        
        [Test]
        public void TestSerializeBinary()
        {
            var info = ServerInfo.FromNetApi32(new NetApi32.SERVER_INFO_101 {sv101_name = "QQQ", sv101_comment = "WWW"});
            //SI.Name = "QQQ";
            //SI.Comment = "WWW";
            var stream = new MemoryStream();
            var bformatter = new BinaryFormatter();
            // try serialize
            bformatter.Serialize(stream, info);
            // try deserialize
            stream.Position = 0;
            var result = bformatter.Deserialize(stream);
            Assert.IsNotNull(result, "Deserialize returns null");
            Assert.IsInstanceOf(typeof(ServerInfo), result, "Deserialize wrong type");
            Assert.AreEqual("QQQ", info.Name);
            Assert.AreEqual("WWW", info.Comment);
            stream.Close();
        }

        [Test]
        public void TestGetSchema()
        {
            Assert.IsNull(m_Info.GetSchema());
        }

        [Test]
        public void TestWriteXML()
        {
            m_Info.Name = "QQQ";
            m_Info.Comment = "WWW";
            m_Info.PlatformID = 1;
            m_Info.Type = 2;
            m_Info.VersionMajor = 3;
            m_Info.VersionMinor = 4;
            // try serialize
            var ser = new XmlSerializer(m_Info.GetType());
            string content;
            using (var sw = new StringWriter())
            {
                ser.Serialize(sw, m_Info);
                content = sw.ToString();
            }
            const string contentCheck = "<ServerInfo Name=\"QQQ\" PlatformID=\"1\" Version=\"3.4\" Type=\"2\" Comment=\"WWW\" />";
            Assert.IsTrue(content.EndsWith(contentCheck));
        }

        [Test]
        public void TestReadXML1()
        {
            m_Info.Name = "QQQ";
            m_Info.Comment = null;
            m_Info.PlatformID = 1;
            m_Info.Type = 2;
            m_Info.VersionMajor = 3;
            m_Info.VersionMinor = 4;
            // try serialize
            var ser = new XmlSerializer(m_Info.GetType());
            string content;
            using (var sw = new StringWriter())
            {
                ser.Serialize(sw, m_Info);
                content = sw.ToString();
            }
            // try deserialize
            TextReader tr = new StringReader(content);
            var result = ser.Deserialize(tr);
            tr.Close();
            // check deserialize result
            Assert.IsNotNull(result, "Deserialize returns null");
            Assert.IsInstanceOf(typeof(ServerInfo), result, "Deserialize wrong type");
            Assert.AreEqual("QQQ", ((ServerInfo)result).Name);
            Assert.AreEqual(String.Empty, ((ServerInfo)result).Comment);
        }

        [Test]
        public void TestReadXML2()
        {
            var ser = new XmlSerializer(typeof(ServerInfo));
            var content = "<ServerInfo PlatformID=\"500\" Comment=\"WWW\" Version=\"5.1\" Type=\"11407\" />";
            // try deserialize
            TextReader tr = new StringReader(content);
            var result = ser.Deserialize(tr);
            tr.Close();
            // check deserialize result
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ServerInfo), result);
            Assert.AreEqual(String.Empty, ((ServerInfo)result).Name);
            Assert.AreEqual("WWW", ((ServerInfo)result).Comment);
        }

        private void СheckVer(NetApi32.SV_101_PLATFORM platform, uint major, uint minor, string compVer, string serverVer, uint type)
        {
            m_Info.PlatformID = (uint) platform;
            m_Info.VersionMajor = major;
            m_Info.VersionMinor = minor;
            // computer check
            m_Info.Type = type | (uint)NetApi32.SV_101_TYPES.SV_TYPE_SERVER_NT;
            Assert.AreEqual(compVer, m_Info.Version());
            // server check
            m_Info.Type = type | (uint)NetApi32.SV_101_TYPES.SV_TYPE_SERVER | (uint)NetApi32.SV_101_TYPES.SV_TYPE_SERVER_NT;
            Assert.AreEqual(serverVer, m_Info.Version());
        }

        private void СheckVer(NetApi32.SV_101_PLATFORM platform, uint major, uint minor, string compVer, uint type)
        {
            СheckVer(platform, major, minor, compVer, compVer, type);
        }

        [Test]
        public void TestVersion()
        {
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_DOS, 4, 1, "MS-DOS 4.1", 0);
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_NT, 3, 0, "Windows NT 3.51", 0);
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_OS2, 4, 0, "Windows 95", 0);
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_NT, 4, 0, "Windows 95", 0);
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_NT, 4, 10, "Windows 98", 0);
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_NT, 4, 90, "Windows ME", 0);
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_NT, 4, 9000, "Windows NT 4.9000", 0);
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_NT, 5, 0, "Windows 2000", "Windows Server 2000", 0);
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_NT, 5, 1, "Windows XP", 0);
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_NT, 5, 2, "Windows Server 2003 R2", 0);
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_NT, 5, 9000, "Windows NT 5.9000", 0);
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_NT, 8000, 9000, "Linux Server 8000.9000", (uint)NetApi32.SV_101_TYPES.SV_TYPE_XENIX_SERVER);
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_NT, 6, 0, "Windows Vista", "Windows Server 2008", 0);
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_NT, 6, 1, "Windows 7", "Windows Server 2008 R2", 0);
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_NT, 6, 2, "Windows 8", "Windows 8 Server", 0);
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_NT, 6, 9000, "Windows NT 6.9000", 0);
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_NT, 8000, 9000, "Windows NT 8000.9000", 0);
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_OSF, 8000, 9000, "OSF 8000.9000", 0);
            СheckVer(NetApi32.SV_101_PLATFORM.PLATFORM_ID_VMS, 8000, 9000, "VMS 8000.9000", 0);
            СheckVer((NetApi32.SV_101_PLATFORM)7000, 8000, 9000, "7000 8000.9000", 0);
        }

        [Test]
        public void TestGetTopicallity()
        {
            m_Info.ResetUtcUpdated();
            Assert.IsEmpty(m_Info.GetTopicalityText());
            var info1 = MockRepository.GenerateStub<ServerInfo>();
            var info2 = MockRepository.GenerateStub<ServerInfo>();
            var current = new DateTime(2012, 01, 22, 18, 30, 40);
            var past1 = new DateTime(current.Year, current.Month, current.Day-1, current.Hour - 10, current.Minute - 20, current.Second);
            var past2 = new DateTime(current.Year, current.Month, current.Day, current.Hour, current.Minute-20, current.Second-30);
            info1.Stub(x => x.GetTopicality()).Return(current - past1);
            info2.Stub(x => x.GetTopicality()).Return(current - past2);
            // checks day, hour, min
            Assert.AreEqual("1d 10h 20m", info1.GetTopicalityText());
            // checks min, sec
            Assert.AreEqual("20m 30s", info2.GetTopicalityText());
        }

        [Test]
        public void TestCompareTo()
        {
            Assert.AreEqual(0, (new ServerInfo {Name = "AA"}).CompareTo((new ServerInfo {Name = "aa"})));
            Assert.Less((new ServerInfo {Name = "AA"}).CompareTo((new ServerInfo {Name = "ba"})), 0);
            Assert.Greater((new ServerInfo { Name = "BB" }).CompareTo((new ServerInfo { Name = "aA" })), 0);
        }

        private ServerInfo NewInfo(uint platform, bool server = false, bool ctrl = false, uint major = 0, uint minor = 0)
        {
            var result = new ServerInfo();
            result.PlatformID = platform;
            result.VersionMajor = major;
            result.VersionMinor = minor;
            result.Type = (uint) NetApi32.SV_101_TYPES.SV_TYPE_SERVER_NT;
            if (server)
            {
                result.Type |= (uint) NetApi32.SV_101_TYPES.SV_TYPE_SERVER;
                Assert.IsTrue(result.IsServer());
            }
            if (ctrl)
            {
                result.Type |= (uint) NetApi32.SV_101_TYPES.SV_TYPE_DOMAIN_CTRL;
                Assert.IsTrue(result.IsDomainController());
            }
            return result;
        }

        [Test]
        public void TestCompareVersionTo()
        {
            Assert.AreEqual(1, m_Info.CompareVersionTo(null));
            Assert.AreEqual(-1, NewInfo(300).CompareVersionTo(NewInfo(400)));
            Assert.AreEqual(1, NewInfo(400).CompareVersionTo(NewInfo(300)));
            Assert.AreEqual(-1, NewInfo(400, false).CompareVersionTo(NewInfo(400, true)));
            Assert.AreEqual(1, NewInfo(400, true).CompareVersionTo(NewInfo(400, false)));
            Assert.AreEqual(1, NewInfo(400, false, false).CompareVersionTo(NewInfo(400, false, true)));
            Assert.AreEqual(-1, NewInfo(400, false, true).CompareVersionTo(NewInfo(400, false, false)));
            Assert.AreEqual(-1, NewInfo(400, false, false, 1).CompareVersionTo(NewInfo(400, false, false, 2)));
            Assert.AreEqual(1, NewInfo(400, false, false, 2).CompareVersionTo(NewInfo(400, false, false, 1)));
            Assert.AreEqual(-1, NewInfo(400, false, false, 1, 1).CompareVersionTo(NewInfo(400, false, false, 1, 2)));
            Assert.AreEqual(1, NewInfo(400, false, false, 1, 2).CompareVersionTo(NewInfo(400, false, false, 1, 1)));
            Assert.AreEqual(0, NewInfo(400).CompareVersionTo(NewInfo(400)));
        }

        [Test]
        public void TestUtcUpdated()
        {
            var current = DateTime.UtcNow;
            m_Info.ResetUtcUpdated();
            Assert.AreEqual(current, m_Info.UtcUpdated);
            m_Info.UtcUpdated = current;
            Assert.AreEqual(current, m_Info.UtcUpdated);
        }

        [Test]
        public void TestIsDomainController()
        {
            m_Info.Type = (uint) NetApi32.SV_101_TYPES.SV_TYPE_DOMAIN_CTRL;
            Assert.IsTrue(m_Info.IsDomainController());
            m_Info.Type = (uint) NetApi32.SV_101_TYPES.SV_TYPE_DOMAIN_BAKCTRL;
            Assert.IsTrue(m_Info.IsDomainController());
        }

        [Test]
        public void TestIsServer()
        {
            m_Info.Type = (uint)NetApi32.SV_101_TYPES.SV_TYPE_SERVER | (uint)NetApi32.SV_101_TYPES.SV_TYPE_DOMAIN_CTRL;
            Assert.IsTrue(m_Info.IsServer());
            m_Info.Type = (uint)NetApi32.SV_101_TYPES.SV_TYPE_SERVER | (uint)NetApi32.SV_101_TYPES.SV_TYPE_SERVER_NT;
            Assert.IsTrue(m_Info.IsServer());
            m_Info.Type = (uint) NetApi32.SV_101_TYPES.SV_TYPE_SERVER_NT;
            Assert.IsFalse(m_Info.IsServer());
        }

        [Test]
        public void TestIsSQLServer()
        {
            m_Info.Type = (uint) NetApi32.SV_101_TYPES.SV_TYPE_SQLSERVER;
            Assert.IsTrue(m_Info.IsSQLServer());
        }

        [Test]
        public void TestIsTimeSource()
        {
            m_Info.Type = (uint) NetApi32.SV_101_TYPES.SV_TYPE_TIME_SOURCE;
            Assert.IsTrue(m_Info.IsTimeSource());
        }

        [Test]
        public void TestIsPrintServer()
        {
            m_Info.Type = (uint) NetApi32.SV_101_TYPES.SV_TYPE_PRINTQ_SERVER;
            Assert.IsTrue(m_Info.IsPrintServer());
        }

        [Test]
        public void TestIsDialInServer()
        {
            m_Info.Type = (uint) NetApi32.SV_101_TYPES.SV_TYPE_DIALIN_SERVER;
            Assert.IsTrue(m_Info.IsDialInServer());
        }

        [Test]
        public void TestIsPotentialBrowser()
        {

            m_Info.Type = (uint) NetApi32.SV_101_TYPES.SV_TYPE_POTENTIAL_BROWSER;
            Assert.IsTrue(m_Info.IsPotentialBrowser());
        }

        [Test]
        public void TestIsBackupBrowser()
        {
            m_Info.Type = (uint) NetApi32.SV_101_TYPES.SV_TYPE_BACKUP_BROWSER;
            Assert.IsTrue(m_Info.IsBackupBrowser());
        }

        [Test]
        public void TestIsMasterBrowser()
        {
            m_Info.Type = (uint) NetApi32.SV_101_TYPES.SV_TYPE_MASTER_BROWSER;
            Assert.IsTrue(m_Info.IsMasterBrowser());
        }

        [Test]
        public void TestIsDFSRoot()
        {
            m_Info.Type = (uint) NetApi32.SV_101_TYPES.SV_TYPE_DFS;
            Assert.IsTrue(m_Info.IsDFSRoot());
        }
    }
}
