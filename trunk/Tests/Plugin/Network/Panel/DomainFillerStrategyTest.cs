using System.Collections.ObjectModel;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Plugin.Network.Panel
{
    class DomainFillerStrategyTest
    {
        [Test]
        public void TestIsSubjectAccepted()
        {
            var strategy = new DomainFiller();
            Assert.IsTrue(strategy.IsParentAccepted(Network.ROOT_OF_DOMAINS));
            Assert.IsFalse(strategy.IsParentAccepted(new ComputerPanelItem(null, "COMP01")));
        }

        [Test]
        public void TestAlgorithm()
        {
            var strategy = new DomainFiller();
            var result = new Collection<PanelItemBase>();
            strategy.Fill(null, result);
            Assert.Greater(result.Count, 0);
        }
    }
}
