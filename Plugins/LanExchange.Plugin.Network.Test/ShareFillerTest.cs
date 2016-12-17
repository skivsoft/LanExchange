using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using LanExchange.Plugin.Network.NetApi;
using NUnit.Framework;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Network
{
    internal class ShareFillerTest
    {
        [Test]
        public void TestIsSubjectAccepted()
        {
            var strategy = new ShareFiller();
            Assert.IsFalse(strategy.IsParentAccepted(null));
            Assert.IsTrue(strategy.IsParentAccepted(new ComputerPanelItem(null, SystemInformation.ComputerName)));
        }

        [Test]
        public void ExceptionAlgorithm()
        {
            var strategy = new ShareFiller();
            var result = new Collection<PanelItemBase>();
            Assert.Throws<ArgumentNullException>(() => strategy.AsyncFill(null, result));
        }

        [Test]
        public void TestAsyncFill()
        {
            var strategy = new ShareFiller();
            string domain = WorkstationInfo.FromComputer(null).LanGroup;
            var computer = new ComputerPanelItem(new DomainPanelItem(new DomainRoot(), domain),
                SystemInformation.ComputerName);
            ShareFiller.ShowHiddenShares = true;
            var result = new Collection<PanelItemBase>();
            strategy.AsyncFill(computer, result);
            Assert.Greater(result.Count, 0);
            Assert.IsInstanceOf<SharePanelItem>(result[0]);
            ShareFiller.ShowHiddenShares = false;
            strategy.AsyncFill(computer, result);
        }
    }
}