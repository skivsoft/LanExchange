using System.Collections.ObjectModel;
using LanExchange.Plugin.Network.Panel;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Plugin.Network.Tests.Panel
{
    class DomainFillerStrategyTest
    {
        [Test]
        public void TestIsSubjectAccepted()
        {
            var strategy = new DomainFillerStrategy();
            Assert.IsTrue(strategy.IsParentAccepted(null));
            Assert.IsFalse(strategy.IsParentAccepted(new ComputerPanelItem(null, "COMP01")));
        }

        [Test]
        public void TestAlgorithm()
        {
            var strategy = new DomainFillerStrategy();
            var result = new Collection<PanelItemBase>();
            strategy.Algorithm(null, result);
            Assert.Greater(result.Count, 0);
        }
    }
}
