using NUnit.Framework;

namespace LanExchange.Misc.Service
{
    [TestFixture]
    class PuntoSwitcherServiceFactoryTest
    {
        [Test]
        public void TestFactory()
        {
            App.Setup();
            var interface1 = App.Ioc.Resolve<IPuntoSwitcherService>();
            var interface2 = App.Ioc.Resolve<IPuntoSwitcherService>();
            Assert.NotNull(interface1);
            Assert.NotNull(interface2);
            Assert.AreSame(interface1, interface2);
        }
    }
}
