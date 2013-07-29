using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Plugin.Network.Tests
{
    class DomainEnumStrategyTest
    {
        [Test]
        public void TestIsSubjectAccepted()
        {
            var strategy = new DomainEnumStrategy();
            Assert.IsFalse(strategy.IsSubjectAccepted(null));
            Assert.IsTrue(strategy.IsSubjectAccepted(ConcreteSubject.s_Root));
        }

        [Test]
        public void TestAlgorithm()
        {
            var strategy = new DomainEnumStrategy();
            strategy.Algorithm();
            Assert.Greater(strategy.Result.Count, 0);
        }
    }
}
