using System;
using System.Text;
using NUnit.Framework;

namespace LanExchange.Model
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

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExceptionThis()
        {
            var value = m_Custom[m_Custom.CountColumns];
        }

        [Test]
        public void TestGetStringsUpper()
        {
            var lines = m_Custom.GetStringsUpper();
            Assert.AreEqual(3, lines.Length);
            Assert.AreEqual("TEST", lines[0]);
            Assert.AreEqual(" ", lines[1]);
            Assert.AreEqual(" ", lines[2]);
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

        [Test]
        public void TestTooltipText()
        {
            var sb = new StringBuilder();
            sb.Append(" ");
            sb.AppendLine();
            sb.Append(" ");
            Assert.AreEqual(sb.ToString(), m_Custom.ToolTipText);
        }
    }
}
