using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.NetApi;
using LanExchange.SDK;
using NUnit.Framework;

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

        [Test, ExpectedException(typeof (ArgumentNullException))]
        public void ExceptionAlgorithm()
        {
            var strategy = new ShareFiller();
            var result = new Collection<PanelItemBase>();
            strategy.AsyncFill(null, result);
        }

        [Test]
        public void TestAsyncFill()
        {
            Utils.InitPlugins();
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