using LanExchange.Model;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Tests.Model
{
    class ComputerItemsStrategyTest
    {
        [Test]
        public void TestIsSubjectAccepted()
        {
            var strategy = new ComputerItemsStrategy();
            Assert.IsFalse(strategy.IsSubjectAccepted(null));
            Assert.IsTrue(strategy.IsSubjectAccepted(ConcreteSubject.s_UserItems));
        }

        [Test]
        public void TestAlgorithm()
        {
            var strategy = new ComputerItemsStrategy();
            strategy.Algorithm();
            Assert.AreEqual(0, strategy.Result.Count);
        }
    }
}
