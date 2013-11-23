using LanExchange.Core;
using NUnit.Framework;
using LanExchange.Intf;

namespace LanExchange.Misc.Service
{
    [TestFixture]
    class PuntoSwitcherServiceFactoryTest
    {
        [Test]
        public void TestFactory()
        {
            App.SetContainer(ContainerBuilder.Build());
            var interface1 = App.Resolve<IPuntoSwitcherService>();
            var interface2 = App.Resolve<IPuntoSwitcherService>();
            Assert.NotNull(interface1);
            Assert.NotNull(interface2);
            Assert.AreSame(interface1, interface2);
        }
    }
}
