using NUnit.Framework;

namespace LanExchange.Plugin.Network.Panel
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
