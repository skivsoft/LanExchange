using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Plugin.Network.Panel
{
    class ShareFillerStrategyTest
    {
        [Test]
        public void TestIsSubjectAccepted()
        {
            var strategy = new ShareFillerStrategy();
            Assert.IsFalse(strategy.IsParentAccepted(null));
            Assert.IsTrue(strategy.IsParentAccepted(new ComputerPanelItem(null, SystemInformation.ComputerName)));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ExceptionAlgorithm()
        {
            var strategy = new ShareFillerStrategy();
            var result = new Collection<PanelItemBase>();
            strategy.Algorithm(null, result);
        }

        [Test]
        public void TestAlgorithm()
        {
            var strategy = new ShareFillerStrategy();
            var domain = NetApi32Utils.Instance.GetMachineNetBiosDomain(null);
            var computer = new ComputerPanelItem(new DomainPanelItem(null, domain), SystemInformation.ComputerName);
            ShareFillerStrategy.ShowHiddenShares = true;
            var result = new Collection<PanelItemBase>();
            strategy.Algorithm(computer, result);
            Assert.Greater(result.Count, 1);
            Assert.IsInstanceOf<PanelItemDoubleDot>(result[0]);
            ShareFillerStrategy.ShowHiddenShares = false;
            strategy.Algorithm(computer, result);
        }
    }
}
