using System;
using System.Collections.ObjectModel;
using LanExchange.Plugin.Network.Panel;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Plugin.Network.Tests.Panel
{
    [TestFixture]
    internal class ComputerEnumStrategyTest
    {
        [Test]
        public void TestIsSubjectAccepted()
        {
            var strategy = new ComputerFillerStrategy();
            Assert.IsFalse(strategy.IsParentAccepted(null));
            Assert.IsTrue(strategy.IsParentAccepted(new DomainPanelItem(null, "DOMAIN")));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ExceptionAlgorithm()
        {
            var strategy = new ComputerFillerStrategy();
            var result = new Collection<PanelItemBase>();
            strategy.Algorithm(null, result);
        }

        [Test]
        public void TestAlgorithm()
        {
            var strategy = new ComputerFillerStrategy();
            var result = new Collection<PanelItemBase>();
            var domain = NetApi32Utils.Instance.GetMachineNetBiosDomain(null);
            strategy.Algorithm(new DomainPanelItem(null, domain), result);
            Assert.Greater(result.Count, 0);
        }
    }
}
