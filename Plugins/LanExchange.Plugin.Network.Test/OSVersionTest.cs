using LanExchange.Plugin.Network.NetApi;
using NUnit.Framework;

namespace LanExchange.Plugin.Network
{
    [TestFixture]
    internal class OSVersionTest
    {
        [SetUp]
        protected void SetUp()
        {
            m_Info = new OSVersion();
        }

        [TearDown]
        protected void TearDown()
        {
            m_Info = null;
        }

        private OSVersion m_Info;

        private void СheckVer(SV_101_PLATFORM platform, uint major, uint minor, string compVer,
            string serverVer, uint type)
        {
            m_Info.PlatformId = (uint)platform;
            m_Info.Major = major;
            m_Info.Minor = minor;
            // computer check
            m_Info.Type = type | (uint)SV_101_TYPES.SV_TYPE_SERVER_NT;
            Assert.AreEqual(compVer, m_Info.ToString());
            // server check
            m_Info.Type = type | (uint)SV_101_TYPES.SV_TYPE_SERVER |
                          (uint)SV_101_TYPES.SV_TYPE_SERVER_NT;
            Assert.AreEqual(serverVer, m_Info.ToString());
        }

        private void СheckVer(SV_101_PLATFORM platform, uint major, uint minor, string compVer, uint type)
        {
            СheckVer(platform, major, minor, compVer, compVer, type);
        }

        private OSVersion NewInfo(uint platform, bool server = false, bool ctrl = false, uint major = 0, uint minor = 0)
        {
            var result = new OSVersion();
            result.PlatformId = platform;
            result.Major = major;
            result.Minor = minor;
            result.Type = (uint)SV_101_TYPES.SV_TYPE_SERVER_NT;
            if (server)
            {
                result.Type |= (uint)SV_101_TYPES.SV_TYPE_SERVER;
                Assert.IsTrue(result.IsServer());
            }
            if (ctrl)
            {
                result.Type |= (uint)SV_101_TYPES.SV_TYPE_DOMAIN_CTRL;
                Assert.IsTrue(result.IsDomainController());
            }
            return result;
        }

        [Test]
        public void TestCompareVersionTo()
        {
            Assert.AreEqual(1, m_Info.CompareTo(null));
            Assert.AreEqual(-1, NewInfo(300).CompareTo(NewInfo(400)));
            Assert.AreEqual(1, NewInfo(400).CompareTo(NewInfo(300)));
            Assert.AreEqual(-1, NewInfo(400, false).CompareTo(NewInfo(400, true)));
            Assert.AreEqual(1, NewInfo(400, true).CompareTo(NewInfo(400, false)));
            Assert.AreEqual(1, NewInfo(400, false, false).CompareTo(NewInfo(400, false, true)));
            Assert.AreEqual(-1, NewInfo(400, false, true).CompareTo(NewInfo(400, false, false)));
            Assert.AreEqual(-1, NewInfo(400, false, false, 1).CompareTo(NewInfo(400, false, false, 2)));
            Assert.AreEqual(1, NewInfo(400, false, false, 2).CompareTo(NewInfo(400, false, false, 1)));
            Assert.AreEqual(-1, NewInfo(400, false, false, 1, 1).CompareTo(NewInfo(400, false, false, 1, 2)));
            Assert.AreEqual(1, NewInfo(400, false, false, 1, 2).CompareTo(NewInfo(400, false, false, 1, 1)));
            Assert.AreEqual(0, NewInfo(400).CompareTo(NewInfo(400)));
        }

        [Test]
        public void TestIsBackupBrowser()
        {
            m_Info.Type = (uint)SV_101_TYPES.SV_TYPE_BACKUP_BROWSER;
            Assert.IsTrue(m_Info.IsBackupBrowser());
        }

        [Test]
        public void TestIsDFSRoot()
        {
            m_Info.Type = (uint)SV_101_TYPES.SV_TYPE_DFS;
            Assert.IsTrue(m_Info.IsDfsRoot());
        }

        [Test]
        public void TestIsDialInServer()
        {
            m_Info.Type = (uint)SV_101_TYPES.SV_TYPE_DIALIN_SERVER;
            Assert.IsTrue(m_Info.IsDialInServer());
        }

        [Test]
        public void TestIsDomainController()
        {
            m_Info.Type = (uint)SV_101_TYPES.SV_TYPE_DOMAIN_CTRL;
            Assert.IsTrue(m_Info.IsDomainController());
            m_Info.Type = (uint)SV_101_TYPES.SV_TYPE_DOMAIN_BAKCTRL;
            Assert.IsTrue(m_Info.IsDomainController());
        }

        [Test]
        public void TestIsMasterBrowser()
        {
            m_Info.Type = (uint)SV_101_TYPES.SV_TYPE_MASTER_BROWSER;
            Assert.IsTrue(m_Info.IsMasterBrowser());
        }

        [Test]
        public void TestIsPotentialBrowser()
        {
            m_Info.Type = (uint)SV_101_TYPES.SV_TYPE_POTENTIAL_BROWSER;
            Assert.IsTrue(m_Info.IsPotentialBrowser());
        }

        [Test]
        public void TestIsPrintServer()
        {
            m_Info.Type = (uint)SV_101_TYPES.SV_TYPE_PRINTQ_SERVER;
            Assert.IsTrue(m_Info.IsPrintServer());
        }

        [Test]
        public void TestIsSQLServer()
        {
            m_Info.Type = (uint)SV_101_TYPES.SV_TYPE_SQLSERVER;
            Assert.IsTrue(m_Info.IsSqlServer());
        }

        [Test]
        public void TestIsServer()
        {
            m_Info.Type = (uint)SV_101_TYPES.SV_TYPE_SERVER |
                          (uint)SV_101_TYPES.SV_TYPE_DOMAIN_CTRL;
            Assert.IsTrue(m_Info.IsServer());
            m_Info.Type = (uint)SV_101_TYPES.SV_TYPE_SERVER |
                          (uint)SV_101_TYPES.SV_TYPE_SERVER_NT;
            Assert.IsTrue(m_Info.IsServer());
            m_Info.Type = (uint)SV_101_TYPES.SV_TYPE_SERVER_NT;
            Assert.IsFalse(m_Info.IsServer());
        }

        [Test]
        public void TestIsTimeSource()
        {
            m_Info.Type = (uint)SV_101_TYPES.SV_TYPE_TIME_SOURCE;
            Assert.IsTrue(m_Info.IsTimeSource());
        }

        [Test]
        public void TestVersion()
        {
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_DOS, 4, 1, "MS-DOS 4.1", 0);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_NT, 3, 0, "Windows NT 3.51", 0);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_OS2, 4, 0, "Windows 95", 0);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_NT, 4, 0, "Windows 95", 0);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_NT, 4, 10, "Windows 98", 0);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_NT, 4, 90, "Windows ME", 0);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_NT, 4, 9000, "Windows NT 4.9000", 0);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_NT, 5, 0, "Windows 2000", "Windows Server 2000", 0);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_NT, 5, 1, "Windows XP", 0);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_NT, 5, 2, "Windows Server 2003 R2", 0);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_NT, 5, 9000, "Windows NT 5.9000", 0);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_NT, 8000, 9000, "Linux Server 8000.9000",
                (uint)SV_101_TYPES.SV_TYPE_XENIX_SERVER);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_NT, 6, 0, "Windows Vista", "Windows Server 2008", 0);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_NT, 6, 1, "Windows 7", "Windows Server 2008 R2", 0);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_NT, 6, 2, "Windows 8", "Windows Server 2012", 0);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_NT, 6, 3, "Windows 8.1", "Windows Server 2012 R2", 0);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_NT, 6, 9000, "Windows NT 6.9000", 0);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_NT, 8000, 9000, "Windows NT 8000.9000", 0);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_OSF, 8000, 9000, "OSF 8000.9000", 0);
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_VMS, 8000, 9000, "VMS 8000.9000", 0);
            СheckVer((SV_101_PLATFORM)7000, 8000, 9000, "7000 8000.9000", 0);
        }
    }
}