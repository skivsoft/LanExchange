using NUnit.Framework;

namespace LanExchange.SDK
{
    [TestFixture]
    class PanelItemDoubleDotTest
    {
        [Test]
        public void TestCtor()
        {
            var dot = new PanelItemDoubleDot(null);
            Assert.AreEqual(PanelImageNames.DoubleDot, dot.ImageName);
            var item = new PanelItemBase();
            Assert.AreEqual(-1, dot.CompareTo(item));
            Assert.AreEqual(1, item.CompareTo(dot));
        }
    }
}
