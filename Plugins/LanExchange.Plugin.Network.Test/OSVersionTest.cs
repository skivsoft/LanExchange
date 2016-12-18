using LanExchange.Plugin.Network.NetApi;
using NUnit.Framework;

namespace LanExchange.Plugin.Network
{
    [TestFixture]
    internal class OSVersionTest
    {
        private OSVersion info;

        [SetUp]
        public void SetUp()
        {
            info = new OSVersion();
        }

        [TearDown]
        public void TearDown()
        {
            info = null;
        }

        [Test]
        public void TestCompareVersionTo()
        {
            Assert.AreEqual(1, info.CompareTo(null));
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
            info.Type = (uint)SV_101_TYPES.SV_TYPE_BACKUP_BROWSER;
            Assert.IsTrue(info.IsBackupBrowser());
        }

        [Test]
        public void TestIsDFSRoot()
        {
            info.Type = (uint)SV_101_TYPES.SV_TYPE_DFS;
            Assert.IsTrue(info.IsDfsRoot());
        }

        [Test]
        public void TestIsDialInServer()
        {
            info.Type = (uint)SV_101_TYPES.SV_TYPE_DIALIN_SERVER;
            Assert.IsTrue(info.IsDialInServer());
        }

        [Test]
        public void TestIsDomainController()
        {
            info.Type = (uint)SV_101_TYPES.SV_TYPE_DOMAIN_CTRL;
            Assert.IsTrue(info.IsDomainController());
            info.Type = (uint)SV_101_TYPES.SV_TYPE_DOMAIN_BAKCTRL;
            Assert.IsTrue(info.IsDomainController());
        }

        [Test]
        public void TestIsMasterBrowser()
        {
            info.Type = (uint)SV_101_TYPES.SV_TYPE_MASTER_BROWSER;
            Assert.IsTrue(info.IsMasterBrowser());
        }

        [Test]
        public void TestIsPotentialBrowser()
        {
            info.Type = (uint)SV_101_TYPES.SV_TYPE_POTENTIAL_BROWSER;
            Assert.IsTrue(info.IsPotentialBrowser());
        }

        [Test]
        public void TestIsPrintServer()
        {
            info.Type = (uint)SV_101_TYPES.SV_TYPE_PRINTQ_SERVER;
            Assert.IsTrue(info.IsPrintServer());
        }

        [Test]
        public void TestIsSQLServer()
        {
            info.Type = (uint)SV_101_TYPES.SV_TYPE_SQLSERVER;
            Assert.IsTrue(info.IsSqlServer());
        }

        [Test]
        public void TestIsServer()
        {
            info.Type = (uint)SV_101_TYPES.SV_TYPE_SERVER |
                          (uint)SV_101_TYPES.SV_TYPE_DOMAIN_CTRL;
            Assert.IsTrue(info.IsServer());
            info.Type = (uint)SV_101_TYPES.SV_TYPE_SERVER |
                          (uint)SV_101_TYPES.SV_TYPE_SERVER_NT;
            Assert.IsTrue(info.IsServer());
            info.Type = (uint)SV_101_TYPES.SV_TYPE_SERVER_NT;
            Assert.IsFalse(info.IsServer());
        }

        [Test]
        public void TestIsTimeSource()
        {
            info.Type = (uint)SV_101_TYPES.SV_TYPE_TIME_SOURCE;
            Assert.IsTrue(info.IsTimeSource());
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
            СheckVer(SV_101_PLATFORM.PLATFORM_ID_NT, 8000, 9000, "Linux Server 8000.9000", (uint)SV_101_TYPES.SV_TYPE_XENIX_SERVER);
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

        private void СheckVer(
            SV_101_PLATFORM platform,
            uint major,
            uint minor,
            string compVer,
            string serverVer,
            uint type)
        {
            info.PlatformId = (uint)platform;
            info.Major = major;
            info.Minor = minor;

            // computer check
            info.Type = type | (uint)SV_101_TYPES.SV_TYPE_SERVER_NT;
            Assert.AreEqual(compVer, info.ToString());

            // server check
            info.Type = type | (uint)SV_101_TYPES.SV_TYPE_SERVER |
                          (uint)SV_101_TYPES.SV_TYPE_SERVER_NT;
            Assert.AreEqual(serverVer, info.ToString());
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
    }
}