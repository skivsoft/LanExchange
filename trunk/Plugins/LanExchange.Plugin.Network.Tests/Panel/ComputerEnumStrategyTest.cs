using LanExchange.Plugin.Network.Panel;
using NUnit.Framework;

namespace LanExchange.Plugin.Network.Tests.Panel
{
    [TestFixture]
    class ComputerEnumStrategyTest
    {
        [Test]
        public void TestIsSubjectAccepted()
        {
            var strategy = new ComputerFillerStrategy();
            Assert.IsFalse(strategy.IsParentAccepted(null));
            Assert.IsTrue(strategy.IsParentAccepted(new DomainPanelItem("DOMAIN")));
        }

        [Test]
        public void TestAlgorithm()
        {
            Assert.IsTrue(false);
            //var strategy = new ComputerFillerStrategy();
            //var domain = NetApi32Utils.Instance.GetMachineNetBiosDomain(null);
            //strategy.Algorithm();
            //Assert.AreEqual(strategy.Result.Count, 0);
            //strategy.Subject = new DomainPanelItem(domain);
            //strategy.Algorithm();
            //Assert.Greater(strategy.Result.Count, 0);
        }
    }
}
