using System;
using LanExchange.Misc;
using NUnit.Framework;

namespace LanExchange.Model
{
    [TestFixture]
    internal class CustomPanelItemTest
    {
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

        private CustomPanelItem m_Custom;

        [Test, ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void ExceptionThis()
        {
            IComparable value = m_Custom[m_Custom.CountColumns];
        }

        [Test]
        public void GetValue()
        {
            Assert.IsNull(m_Custom.GetValue(-1));
        }

        [Test]
        public void TestFullItemName()
        {
            Assert.AreEqual("test", m_Custom.FullName);
            var subItem = new CustomPanelItem(m_Custom, "hello");
            Assert.AreEqual("hello", subItem.FullName);
        }

        [Test]
        public void TestImageName()
        {
            Assert.IsEmpty(m_Custom.ImageName);
        }
    }
}