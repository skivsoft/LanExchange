using System;
using System.IO;
using System.Xml.Serialization;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Plugin.Network.Panel
{
    [TestFixture]
    class DomainPanelItemTest
    {
        private DomainPanelItem m_Domain;

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

        [Test]
        public void TestName()
        {
            Assert.IsTrue(m_Domain.IsCacheable);
            m_Domain.Name = "DOMAIN1";
            Assert.AreEqual("DOMAIN1", m_Domain.Name);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExceptionThis()
        {
            var value = m_Domain[m_Domain.CountColumns];
        }

        [Test]
        public void TestThis()
        {
            Assert.AreEqual(m_Domain.Name, m_Domain[0]);
        }

        [Test]
        public void TestToString()
        {
            Assert.IsEmpty(m_Domain.ToString());
        }

        [Test]
        public void TestImageName()
        {
            Assert.AreEqual(PanelImageNames.Workgroup, m_Domain.ImageName);
        }

        [Test]
        public void TestCreateColumnHeader()
        {
            for (int i = 0; i < m_Domain.CountColumns; i++)
            {
                var header = m_Domain.CreateColumnHeader(i);
                Assert.IsNotNull(header);
            }
        }

        [Test]
        public void TestSerialization()
        {
            var extraTypes = new Type[] { typeof(DomainPanelItem) };
            var ser = new XmlSerializer(typeof(PanelItemBase), extraTypes);
            using (var sw = new StringWriter())
            {
                ser.Serialize(sw, m_Domain);
                var result = sw.ToString();
                Assert.IsNotEmpty(result);
            }
        }
    }
}
