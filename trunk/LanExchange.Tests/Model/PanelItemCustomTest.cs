using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using LanExchange.Model;

namespace LanExchange.Tests.Model
{
    [TestFixture]
    class PanelItemCustomTest
    {
        private PanelItemCustom m_Custom;

        [SetUp]
        public void SetUp()
        {
            m_Custom = new PanelItemCustom(null, "test");
        }

        [TearDown]
        public void TearDown()
        {
            m_Custom = null;
        }

        [Test]
        public void TestGetStringsUpper()
        {
            var lines = m_Custom.GetStringsUpper();
            Assert.AreEqual(1, lines.Length);
            Assert.AreEqual("TEST", lines[0]);
        }

        [Test]
        public void TestImageName()
        {
            Assert.IsEmpty(m_Custom.ImageName);
        }

        [Test]
        public void TestCreateColumnHeader()
        {
            for (int i = 0; i < m_Custom.CountColumns; i++)
            {
                var header = m_Custom.CreateColumnHeader(i);
                Assert.IsNotNull(header);
            }
            Assert.IsNull(m_Custom.CreateColumnHeader(m_Custom.CountColumns));
        }

        [Test]
        public void TestFullItemName()
        {
            Assert.AreEqual("test", m_Custom.FullItemName);
            var subItem = new PanelItemCustom(m_Custom, "hello");
            Assert.AreEqual(@"test\hello", subItem.FullItemName);
        }
    }
}
