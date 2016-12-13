using System;
using System.IO;
using System.Xml.Serialization;
using NUnit.Framework;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Network
{
    [TestFixture]
    internal class DomainPanelItemTest
    {
        [SetUp]
        public void SetUp()
        {
            m_Domain = new DomainPanelItem(null, "DOMAIN");
        }

        [TearDown]
        public void TearDown()
        {
            m_Domain = null;
        }

        private DomainPanelItem m_Domain;

        [Test]
        public void ExceptionThis()
        {
            IComparable value;
            Assert.Throws<ArgumentOutOfRangeException>(() => value = m_Domain[m_Domain.CountColumns]);
        }

        [Test]
        public void TestFullName()
        {
            Assert.IsEmpty(m_Domain.FullName);
        }

        [Test]
        public void TestImageName()
        {
            Assert.AreEqual(PanelImageNames.DOMAIN, m_Domain.ImageName);
        }

        [Test]
        public void TestName()
        {
            // Assert.IsTrue(m_Domain.IsCacheable);

            m_Domain.Name = "DOMAIN1";
            Assert.AreEqual("DOMAIN1", m_Domain.Name);
        }

        [Test]
        public void TestSerialization()
        {
            var extraTypes = new[] {typeof(DomainPanelItem)};

           var ser = new XmlSerializer(typeof(PanelItemBase), extraTypes);
            using (var sw = new StringWriter())
            {
                ser.Serialize(sw, m_Domain);
                string result = sw.ToString();
                Assert.IsNotEmpty(result);
            }
        }

        [Test]
        public void TestThis()
        {
            Assert.AreEqual(m_Domain.Name, m_Domain[0]);
        }
    }
}