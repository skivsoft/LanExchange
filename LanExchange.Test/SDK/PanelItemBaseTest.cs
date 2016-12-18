using System.Collections.Generic;
using LanExchange.Misc;
using LanExchange.Presentation.Interfaces;
using NUnit.Framework;

namespace LanExchange.SDK
{
    [TestFixture]
    internal class PanelItemBaseTest
    {
        private PanelItemBase item;

        [SetUp]
        public void SetUp()
        {
            item = new CustomPanelItem(null, string.Empty);
        }

        [TearDown]
        public void TearDown()
        {
            item = null;
        }

        [Test]
        public void TestDictionaryCS()
        {
            item.Name = "hello";
            var item2 = new CustomPanelItem(null, "hello");
            var item3 = new CustomPanelItem(null, "hi");
            var dict = new Dictionary<PanelItemBase, int>();
            dict.Add(item, 1);
            int result;
            Assert.IsTrue(dict.TryGetValue(item, out result));
            Assert.IsTrue(dict.TryGetValue(item2, out result));
            Assert.IsFalse(dict.TryGetValue(item3, out result));
        }
    }
}