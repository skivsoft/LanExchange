using System;
using System.Collections.ObjectModel;
using System.Windows.NetApi;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Plugin.Network
{
    [TestFixture]
    internal class ComputerFillerTest
    {
        [Test, ExpectedException(typeof (ArgumentNullException))]
        public void ExceptionAlgorithm()
        {
            var strategy = new ComputerFiller();
            var result = new Collection<PanelItemBase>();
            strategy.AsyncFill(null, result);
        }

        [Test]
        public void TestAsyncFill()
        {
            Utils.InitPlugins();
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