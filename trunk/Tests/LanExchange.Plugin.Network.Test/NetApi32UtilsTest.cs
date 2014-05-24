using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
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
            NetApi32Utils.GetMachineNetBiosDomain("~!@#$%^&");
        }

        [Test]
        public void TestGetMachineNetBiosDomain()
        {
            string value = NetApi32Utils.GetMachineNetBiosDomain(null);
            Assert.NotNull(value);
        }

        [Test]
        public void TestNetServerEnumComps()
        {
            string domain = NetApi32Utils.GetMachineNetBiosDomain(null);
            IEnumerable<NativeMethods.SERVER_INFO_101> list = NetApi32Utils.NetServerEnum(domain,
                NativeMethods.SV_101_TYPES.SV_TYPE_DOMAIN_ENUM);
            int count = 0;
            foreach (NativeMethods.SERVER_INFO_101 item in list)
                count++;
            Assert.Greater(count, 0);
        }

        [Test]
        public void TestNetServerEnumDomains()
        {
            IEnumerable<NativeMethods.SERVER_INFO_101> list = NetApi32Utils.NetServerEnum(null,
                NativeMethods.SV_101_TYPES.SV_TYPE_DOMAIN_ENUM);
            IEnumerator<NativeMethods.SERVER_INFO_101> enumerator = list.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.IsNotEmpty(enumerator.Current.sv101_name);
        }

        [Test]
        public void TestNetShareEnum()
        {
            string comp = SystemInformation.ComputerName;
            IEnumerable<NativeMethods.SHARE_INFO_1> list = NetApi32Utils.NetShareEnum(comp);
            int count = 0;
            foreach (NativeMethods.SHARE_INFO_1 item in list)
                count++;
            Assert.Greater(count, 0);
        }
    }
}