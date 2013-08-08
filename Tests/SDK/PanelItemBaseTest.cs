using System;
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
    }
}
