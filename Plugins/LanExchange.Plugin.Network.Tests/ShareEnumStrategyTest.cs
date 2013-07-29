using System.Windows.Forms;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Plugin.Network.Tests
{
    class ShareEnumStrategyTest
    {
        [Test]
        public void TestIsSubjectAccepted()
        {
            var strategy = new ShareEnumStrategy();
            Assert.IsFalse(strategy.IsSubjectAccepted(null));
            var info = new ServerInfo();
            info.Name = SystemInformation.ComputerName;
            Assert.IsTrue(strategy.IsSubjectAccepted(new ComputerPanelItem(null, info)));
        }

        [Test]
        public void TestAlgorithm()
        {
            var strategy = new ShareEnumStrategy();
            strategy.Algorithm();
            Assert.AreEqual(strategy.Result.Count, 0);
            var domain = NetApi32Utils.Instance.GetMachineNetBiosDomain(null);
            var info = new ServerInfo();
            info.Name = SystemInformation.ComputerName;
            strategy.Subject = new ComputerPanelItem(new DomainPanelItem(domain), info);
            ShareEnumStrategy.ShowHiddenShares = true;
            strategy.Algorithm();
            Assert.AreSame(strategy.Result[0].Name, PanelItemBase.s_DoubleDot);
            Assert.Greater(strategy.Result.Count, 1);
            ShareEnumStrategy.ShowHiddenShares = false;
            strategy.Algorithm();
        }
    }
}
