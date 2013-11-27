using LanExchange.SDK;
using NUnit.Framework;
using LanExchange.Intf;

namespace LanExchange.Model
{
    [TestFixture]
    class PanelIndexEvenArgsTest
    {
        [Test]
        public void TestParam()
        {
            var args = new PanelIndexEventArgs(100);
            Assert.AreEqual(100, args.Index);
        }
    }
}
