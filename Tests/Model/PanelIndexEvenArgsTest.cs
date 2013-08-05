using NUnit.Framework;
using LanExchange.Model;

namespace LanExchange.Tests.Model
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
