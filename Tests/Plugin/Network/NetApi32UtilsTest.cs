using System.ComponentModel;
using System.Windows.Forms;
using NUnit.Framework;

namespace LanExchange.Plugin.Network
{
    [TestFixture]
    class NetApi32UtilsTest
    {
        [Test, ExpectedException(typeof(Win32Exception))]
        public void ExceptionGetMachineNetBiosDomain()
        {
            NetApi32Utils.Instance.GetMachineNetBiosDomain("~!@#$%^&");
        }

        [Test]
        public void TestGetMachineNetBiosDomain()
        {
            var value = NetApi32Utils.Instance.GetMachineNetBiosDomain(null);
            Assert.NotNull(value);
        }

        [Test]
        public void TestNetServerEnumDomains()
        {
            var list = NetApi32Utils.Instance.NetServerEnum(null, NetApi32.SV_101_TYPES.SV_TYPE_DOMAIN_ENUM);
            var enumerator = list.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.IsNotEmpty(enumerator.Current.sv101_name);
        }

        [Test]
        public void TestNetServerEnumComps()
        {
            var domain = NetApi32Utils.Instance.GetMachineNetBiosDomain(null);
            var list = NetApi32Utils.Instance.NetServerEnum(domain, NetApi32.SV_101_TYPES.SV_TYPE_DOMAIN_ENUM);
            int count = 0;
            foreach(var item in list)
                count++;
            Assert.Greater(count, 0);
        }

        [Test]
        public void TestNetShareEnum()
        {
            var comp = SystemInformation.ComputerName;
            var list = NetApi32Utils.Instance.NetShareEnum(comp);
            int count = 0;
            foreach (var item in list)
                count++;
            Assert.Greater(count, 0);
        }
    }
}
