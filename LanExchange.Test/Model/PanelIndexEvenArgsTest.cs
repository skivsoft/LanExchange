using LanExchange.Application.Interfaces.EventArgs;
using NUnit.Framework;

namespace LanExchange.Model
{
    [TestFixture]
    internal class PanelIndexEvenArgsTest
    {
        [Test]
        public void TestParam()
        {
            var args = new PanelIndexEventArgs(100);
            Assert.AreEqual(100, args.Index);
        }
    }
}