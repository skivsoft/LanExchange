using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Plugin.Network.Panel
{
    class ShareFillerTest
    {
        [Test]
        public void TestIsSubjectAccepted()
        {
            var strategy = new ShareFiller();
            Assert.IsFalse(strategy.IsParentAccepted(null));
            Assert.IsTrue(strategy.IsParentAccepted(new ComputerPanelItem(null, SystemInformation.ComputerName)));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ExceptionAlgorithm()
        {
            var strategy = new ShareFiller();
            var result = new Collection<PanelItemBase>();
            strategy.Fill(null, result);
        }

        [Test]
        public void TestAlgorithm()
        {
            var strategy = new ShareFiller();
            var domain = NetApi32Utils.Instance.GetMachineNetBiosDomain(null);
            var computer = new ComputerPanelItem(new DomainPanelItem(Network.ROOT_OF_DOMAINS, domain), SystemInformation.ComputerName);
            ShareFiller.ShowHiddenShares = true;
            var result = new Collection<PanelItemBase>();
            strategy.Fill(computer, result);
            Assert.Greater(result.Count, 1);
            Assert.IsInstanceOf<PanelItemDoubleDot>(result[0]);
            ShareFiller.ShowHiddenShares = false;
            strategy.Fill(computer, result);
        }
    }
}
