using System;
using System.Collections.ObjectModel;
using LanExchange.Plugin.Network.NetApi;
using NUnit.Framework;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Network
{
    [TestFixture]
    internal class ComputerFillerTest
    {
        [Test]
        public void ExceptionAlgorithm()
        {
            var strategy = new ComputerFiller();
            var result = new Collection<PanelItemBase>();
            Assert.Throws<ArgumentNullException>(() => strategy.AsyncFill(null, result));
        }

        [Test]
        public void TestAsyncFill()
        {
            var strategy = new ComputerFiller();
            var result = new Collection<PanelItemBase>();
            string domain = WorkstationInfo.FromComputer(null).LanGroup;
            strategy.AsyncFill(new DomainPanelItem(null, domain), result);
            Assert.Greater(result.Count, 0);
        }

        [Test]
        public void TestIsSubjectAccepted()
        {
            var strategy = new ComputerFiller();
            Assert.IsFalse(strategy.IsParentAccepted(null));
            Assert.IsTrue(strategy.IsParentAccepted(new DomainPanelItem(null, "DOMAIN")));
        }
    }
}