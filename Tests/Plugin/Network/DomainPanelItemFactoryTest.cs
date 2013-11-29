using NUnit.Framework;

namespace LanExchange.Plugin.Network
{
    [TestFixture]
    class DomainPanelItemFactoryTest
    {
        [Test]
        public void TestFactory()
        {
            var factory = new DomainFactory();
            Assert.IsNotNull(factory.CreatePanelItem(null, null));
            Assert.IsNotNull(factory.CreateDefaultRoot());
        }
    }
}
