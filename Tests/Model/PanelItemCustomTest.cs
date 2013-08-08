using System;
using System.Text;
using NUnit.Framework;

namespace LanExchange.Model
{
    [TestFixture]
    class PanelItemCustomTest
    {
        private CustomPanelItem m_Custom;

        [SetUp]
        public void SetUp()
        {
            m_Custom = new CustomPanelItem(null, "test");
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
            m_Custom.SetCountColumns(3);
            m_Custom[0] = "TEST";
            m_Custom[1] = " ";
            m_Custom[2] = " ";
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
        public void TestFullItemName()
        {
            Assert.AreEqual("test", m_Custom.FullItemName);
            var subItem = new CustomPanelItem(m_Custom, "hello");
            Assert.AreEqual(@"test\hello", subItem.FullItemName);
        }

        [Test]
        public void TestTooltipText()
        {
            m_Custom.SetCountColumns(3);
            m_Custom[0] = "TEST";
            m_Custom[1] = " ";
            m_Custom[2] = " ";
            var sb = new StringBuilder();
            sb.Append(" ");
            sb.AppendLine();
            sb.Append(" ");
            Assert.AreEqual(sb.ToString(), m_Custom.ToolTipText);
        }
    }
}
