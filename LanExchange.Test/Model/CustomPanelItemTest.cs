using System;
using LanExchange.Misc;
using NUnit.Framework;

namespace LanExchange.Model
{
    [TestFixture]
    internal class CustomPanelItemTest
    {
        private CustomPanelItem custom;

        [SetUp]
        public void SetUp()
        {
            custom = new CustomPanelItem(null, "test");
        }

        [TearDown]
        public void TearDown()
        {
            custom = null;
        }

        [Test]
        public void ExceptionThis()
        {
            IComparable value;
            Assert.Throws<ArgumentOutOfRangeException>(() => value = custom[custom.CountColumns]);
        }

        [Test]
        public void GetValue()
        {
            Assert.IsNull(custom.GetValue(-1));
        }

        [Test]
        public void TestFullItemName()
        {
            Assert.AreEqual("test", custom.FullName);
            var subItem = new CustomPanelItem(custom, "hello");
            Assert.AreEqual("hello", subItem.FullName);
        }

        [Test]
        public void TestImageName()
        {
            Assert.IsEmpty(custom.ImageName);
        }
    }
}