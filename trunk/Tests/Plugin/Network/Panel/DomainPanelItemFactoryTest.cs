using NUnit.Framework;
using LanExchange.Plugin.Network.Panel;

namespace LanExchange.Plugin.Network.Tests.Panel
{
    [TestFixture]
    class DomainPanelItemFactoryTest
    {
        [Test]
        public void TestFactory()
        {
            var factory = new DomainPanelItemFactory();
            Assert.IsNotNull(factory.CreatePanelItem(null, null));
            Assert.IsNotNull(factory.CreateDefaultRoot());
        }
    }
}
