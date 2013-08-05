using NUnit.Framework;

namespace LanExchange.Service
{
    [TestFixture]
    class PuntoSwitcherServiceFactoryTest
    {
        [Test]
        public void TestFactory()
        {
            var interface1 = PuntoSwitcherServiceFactory.GetPuntoSwitcherService();
            var interface2 = PuntoSwitcherServiceFactory.GetPuntoSwitcherService();
            Assert.NotNull(interface1);
            Assert.NotNull(interface2);
            Assert.AreSame(interface1, interface2);
        }
    }
}
