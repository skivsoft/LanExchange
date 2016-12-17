using System.Collections.Generic;
using LanExchange.Presentation.Interfaces;
using NUnit.Framework;
using LanExchange.Misc;

namespace LanExchange.SDK
{
    [TestFixture]
    internal class PanelItemBaseTest
    {
        [SetUp]
        public void SetUp()
        {
            m_Item = new CustomPanelItem(null, string.Empty);
        }

        [TearDown]
        public void TearDown()
        {
            m_Item = null;
        }

        private PanelItemBase m_Item;

        [Test]
        public void TestDictionaryCS()
        {
            m_Item.Name = "hello";
            var item2 = new CustomPanelItem(null, "hello");
            var item3 = new CustomPanelItem(null, "hi");
            var dict = new Dictionary<PanelItemBase, int>();
            dict.Add(m_Item, 1);
            int result;
            Assert.IsTrue(dict.TryGetValue(m_Item, out result));
            Assert.IsTrue(dict.TryGetValue(item2, out result));
            Assert.IsFalse(dict.TryGetValue(item3, out result));
        }
    }
}