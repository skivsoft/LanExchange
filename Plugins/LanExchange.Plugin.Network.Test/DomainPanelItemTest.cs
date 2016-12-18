using System;
using System.IO;
using System.Xml.Serialization;
using LanExchange.Presentation.Interfaces;
using NUnit.Framework;

namespace LanExchange.Plugin.Network
{
    [TestFixture]
    internal class DomainPanelItemTest
    {
        private DomainPanelItem domain;

        [SetUp]
        public void SetUp()
        {
            domain = new DomainPanelItem(null, "DOMAIN");
        }

        [TearDown]
        public void TearDown()
        {
            domain = null;
        }

        [Test]
        public void ExceptionThis()
        {
            IComparable value;
            Assert.Throws<ArgumentOutOfRangeException>(() => value = domain[domain.CountColumns]);
        }

        [Test]
        public void TestFullName()
        {
            Assert.IsEmpty(domain.FullName);
        }

        [Test]
        public void TestImageName()
        {
            Assert.AreEqual(PanelImageNames.DOMAIN, domain.ImageName);
        }

        [Test]
        public void TestName()
        {
            // Assert.IsTrue(domain.IsCacheable);
            domain.Name = "DOMAIN1";
            Assert.AreEqual("DOMAIN1", domain.Name);
        }

        [Test]
        public void TestSerialization()
        {
            var extraTypes = new[] { typeof(DomainPanelItem) };

           var ser = new XmlSerializer(typeof(PanelItemBase), extraTypes);
            using (var sw = new StringWriter())
            {
                ser.Serialize(sw, domain);
                string result = sw.ToString();
                Assert.IsNotEmpty(result);
            }
        }

        [Test]
        public void TestThis()
        {
            Assert.AreEqual(domain.Name, domain[0]);
        }
    }
}