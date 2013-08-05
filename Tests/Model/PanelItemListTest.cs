using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanExchange.Model;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Tests.Model
{
    [TestFixture]
    class PanelItemListTest
    {
        private IPanelModel m_Objects;

        [SetUp]
        public void SetUp()
        {
            m_Objects = new PanelItemList("MyTab");
        }

        [TearDown]
        public void TearDown()
        {
            m_Objects = null;
        }

        [Test]
        public void TestApplyFilter_FilterTextNull()
        {
            m_Objects.FilterText = null;
            m_Objects.ApplyFilter();
            Assert.IsEmpty(m_Objects.FilterText);
        }

        [Test]
        public void TestCurrentPath()
        {
            Assert.IsNotNull(m_Objects.CurrentPath);
        }
    }
}
