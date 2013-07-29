using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using LanExchange.Utils;
using NUnit.Framework;

namespace LanExchange.Tests.Utils
{
    [TestFixture]
    class NetApi32UtilsTest
    {
        [Test, ExpectedException(typeof(Win32Exception))]
        public void ExceptionGetMachineNetBiosDomain()
        {
            NetApi32Utils.GetMachineNetBiosDomain("~!@#$%^&");
        }

        [Test]
        public void TestGetMachineNetBiosDomain()
        {
            var value = NetApi32Utils.GetMachineNetBiosDomain(null);
            Assert.NotNull(value);
        }

        [Test]
        public void TestNetServerEnumDomains()
        {
            var list = NetApi32Utils.NetServerEnum(null, NetApi32.SV_101_TYPES.SV_TYPE_DOMAIN_ENUM);
            var enumerator = list.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.IsNotEmpty(enumerator.Current.sv101_name);
        }

        [Test]
        public void TestNetServerEnumComps()
        {
            var domain = NetApi32Utils.GetMachineNetBiosDomain(null);
            var list = NetApi32Utils.NetServerEnum(domain, NetApi32.SV_101_TYPES.SV_TYPE_DOMAIN_ENUM);
            int count = 0;
            foreach(var item in list)
                count++;
            Assert.Greater(count, 0);
        }
    }
}
