using System.Collections.ObjectModel;
using NUnit.Framework;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Network
{
    internal class DomainFillerTest
    {
        [Test]
        public void TestIsSubjectAccepted()
        {
            var strategy = new DomainFiller();
            Assert.IsTrue(strategy.IsParentAccepted(new DomainRoot()));
            Assert.IsFalse(strategy.IsParentAccepted(new ComputerPanelItem(null, "COMP01")));
        }

        [Test]
        public void TestAsyncFill()
        {
            var strategy = new DomainFiller();
            var result = new Collection<PanelItemBase>();
            strategy.AsyncFill(null, result);
            Assert.Greater(result.Count, 0);
        }
    }
}