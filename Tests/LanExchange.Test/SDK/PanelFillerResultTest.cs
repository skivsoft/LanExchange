using LanExchange.Presentation.Interfaces;
using NUnit.Framework;

namespace LanExchange.SDK
{
    [TestFixture]
    internal class PanelFillerResultTest
    {
        [Test]
        public void TestCtor()
        {
            var result = new PanelFillerResult();
            Assert.NotNull(result.Items);
            Assert.AreEqual(0, result.Items.Count);
            Assert.IsNull(result.ItemsType);
        }
    }
}