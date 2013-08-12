using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model;
using NUnit.Framework;

namespace LanExchange.SDK
{
    [TestFixture]
    class PanelItemBaseTest
    {
        private PanelItemBase m_Item;

        [SetUp]
        public void SetUp()
        {
            m_Item = new PanelItemBase();
        }

        [TearDown]
        public void TearDown()
        {
            m_Item = null;
        }

        [Test]
        public void TestDictionaryCS()
        {
            m_Item.Name = "hello";
            var item2 = new PanelItemBase();
            item2.Name = "hello";
            var item3 = new PanelItemBase();
            item3.Name = "hi";
            var dict = new Dictionary<PanelItemBase, int>();
            dict.Add(m_Item, 1);
            int result;
            Assert.IsTrue(dict.TryGetValue(m_Item, out result));
            Assert.IsTrue(dict.TryGetValue(item2, out result));
            Assert.IsFalse(dict.TryGetValue(item3, out result));
        }
    }
}
