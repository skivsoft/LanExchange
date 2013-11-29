using NUnit.Framework;

namespace LanExchange.Plugin.Network
{
    [TestFixture]
    class ComputerPanelItemFactoryTest
    {
        [Test]
        public void TestFactory()
        {
            var factory = new ComputerFactory();
            Assert.IsNotNull(factory.CreatePanelItem(null, null));
            Assert.IsNull(factory.CreateDefaultRoot());
        }
    }
}
