using System.Windows.Forms;
using LanExchange.Plugin.Network.Panel;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Plugin.Network.Tests.Panel
{
    class ShareEnumStrategyTest
    {
        [Test]
        public void TestIsSubjectAccepted()
        {
            var strategy = new ShareFillerStrategy();
            Assert.IsFalse(strategy.IsParentAccepted(null));
            var info = new ServerInfo();
            info.Name = SystemInformation.ComputerName;
            Assert.IsTrue(strategy.IsParentAccepted(new ComputerPanelItem(null, info)));
        }

        [Test]
        public void TestAlgorithm()
        {
            Assert.IsTrue(false);
            //var strategy = new ShareFillerStrategy();
            //strategy.Algorithm();
            //Assert.AreEqual(strategy.Result.Count, 0);
            //var domain = NetApi32Utils.Instance.GetMachineNetBiosDomain(null);
            //var info = new ServerInfo();
            //info.Name = SystemInformation.ComputerName;
            //strategy.Subject = new ComputerPanelItem(new DomainPanelItem(domain), info);
            //ShareFillerStrategy.ShowHiddenShares = true;
            //strategy.Algorithm();
            //Assert.AreSame(strategy.Result[0].Name, PanelItemBase.s_DoubleDot);
            //Assert.Greater(strategy.Result.Count, 1);
            //ShareFillerStrategy.ShowHiddenShares = false;
            //strategy.Algorithm();
        }
    }
}
