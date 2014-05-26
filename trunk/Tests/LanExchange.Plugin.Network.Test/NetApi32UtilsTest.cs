using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.NetApi;
using NUnit.Framework;

namespace LanExchange.Plugin.Network
{
    [TestFixture]
    internal class NetApi32UtilsTest
    {
        [SetUp]
        public void SetUp()
        {
            Utils.InitPlugins();
        }

        [Test, ExpectedException(typeof (Win32Exception))]
        public void ExceptionGetMachineNetBiosDomain()
        {
            WorkstationInfo.FromComputer("~!@#$%^&");
        }

        [Test]
        public void TestGetMachineNetBiosDomain()
        {
            string value = WorkstationInfo.FromComputer(null).LanGroup;
            Assert.NotNull(value);
        }

        [Test]
        public void TestNetServerEnumComps()
        {
            string domain = WorkstationInfo.FromComputer(null).LanGroup;
            IEnumerable<SERVER_INFO_101> list = NetApiHelper.NetServerEnum(domain,
                SV_101_TYPES.SV_TYPE_DOMAIN_ENUM);
            int count = 0;
            foreach (SERVER_INFO_101 item in list)
                count++;
            Assert.Greater(count, 0);
        }

        [Test]
        public void TestNetServerEnumDomains()
        {
            IEnumerable<SERVER_INFO_101> list = NetApiHelper.NetServerEnum(null,
                SV_101_TYPES.SV_TYPE_DOMAIN_ENUM);
            IEnumerator<SERVER_INFO_101> enumerator = list.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.IsNotEmpty(enumerator.Current.name);
        }

        [Test]
        public void TestNetShareEnum()
        {
            string comp = SystemInformation.ComputerName;
            IEnumerable<SHARE_INFO_1> list = NetApiHelper.NetShareEnum(comp);
            int count = 0;
            foreach (SHARE_INFO_1 item in list)
                count++;
            Assert.Greater(count, 0);
        }
    }
}