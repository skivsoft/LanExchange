using NUnit.Framework;

namespace LanExchange.Plugin.Network.Tests
{
    [TestFixture]
    class ComputerEnumStrategyTest
    {
        [Test]
        public void TestIsSubjectAccepted()
        {
            var strategy = new ComputerEnumStrategy();
            Assert.IsFalse(strategy.IsSubjectAccepted(null));
            Assert.IsTrue(strategy.IsSubjectAccepted(new DomainPanelItem("DOMAIN")));
        }

        [Test]
        public void TestAlgorithm()
        {
            var strategy = new ComputerEnumStrategy();
            var domain = NetApi32Utils.Instance.GetMachineNetBiosDomain(null);
            strategy.Algorithm();
            Assert.AreEqual(strategy.Result.Count, 0);
            strategy.Subject = new DomainPanelItem(domain);
            strategy.Algorithm();
            Assert.Greater(strategy.Result.Count, 0);
        }
    }
}
