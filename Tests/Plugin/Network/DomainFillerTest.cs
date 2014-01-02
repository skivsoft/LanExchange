using System.Collections.ObjectModel;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Plugin.Network
{
    class DomainFillerTest
    {
        [Test]
        public void TestIsSubjectAccepted()
        {
            var strategy = new DomainFiller();
            Assert.IsTrue(strategy.IsParentAccepted(new DomainRoot()));
            Assert.IsFalse(strategy.IsParentAccepted(new ComputerPanelItem(null, "COMP01")));
        }

        [Test]
        public void TestAlgorithm()
        {
            var strategy = new DomainFiller();
            var result = new Collection<PanelItemBase>();
            strategy.AsyncFill(null, result);
            Assert.Greater(result.Count, 0);
        }
    }
}
