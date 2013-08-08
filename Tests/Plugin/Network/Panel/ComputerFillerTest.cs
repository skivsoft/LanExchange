using System;
using System.Collections.ObjectModel;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Plugin.Network.Panel
{
    [TestFixture]
    internal class ComputerFillerTest
    {
        [Test]
        public void TestIsSubjectAccepted()
        {
            var strategy = new ComputerFiller();
            Assert.IsFalse(strategy.IsParentAccepted(null));
            Assert.IsTrue(strategy.IsParentAccepted(new DomainPanelItem(null, "DOMAIN")));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ExceptionAlgorithm()
        {
            var strategy = new ComputerFiller();
            var result = new Collection<PanelItemBase>();
            strategy.Fill(null, result);
        }

        [Test]
        public void TestAlgorithm()
        {
            var strategy = new ComputerFiller();
            var result = new Collection<PanelItemBase>();
            var domain = NetApi32Utils.Instance.GetMachineNetBiosDomain(null);
            strategy.Fill(new DomainPanelItem(null, domain), result);
            Assert.Greater(result.Count, 0);
        }
    }
}
