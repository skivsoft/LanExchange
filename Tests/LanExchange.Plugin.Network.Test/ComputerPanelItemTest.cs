using System;
using System.IO;
using LanExchange.Plugin.Network.NetApi;
using System.Xml.Serialization;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Plugin.Network
{
    [TestFixture]
    internal class ComputerPanelItemTest
    {
        [SetUp]
        public void SetUp()
        {
            var info = new ServerInfo();
            info.Name = "COMP01";
            info.Comment = "Hello world";
            info.Version.PlatformID = (uint) SV_101_PLATFORM.PLATFORM_ID_NT;
            info.Version.Major = 6;
            info.Version.Minor = 2;
            m_Comp = new ComputerPanelItem(null, info);
        }

        [TearDown]
        public void TearDown()
        {
            m_Comp = null;
        }

        private ComputerPanelItem m_Comp;

        //[Test]
        public void TestGetSchema()
        {
            //Assert.IsNull(m_Comp.GetSchema());
        }

        //[Test]
        public void TestWriteXML()
        {
            m_Comp.SI.Name = "QQQ";
            m_Comp.SI.Comment = "WWW";
            m_Comp.SI.Version.PlatformID = 1;
            m_Comp.SI.Version.Type = 2;
            m_Comp.SI.Version.Major = 3;
            m_Comp.SI.Version.Minor = 4;
            // try serialize
            var ser = new XmlSerializer(m_Comp.GetType());
            string content;
            using (var sw = new StringWriter())
            {
                ser.Serialize(sw, m_Comp);
                content = sw.ToString();
            }
            const string contentCheck =
                "<ComputerPanelItem Name=\"QQQ\" PlatformID=\"1\" Version=\"3.4\" Type=\"2\" Comment=\"WWW\" />";
            Assert.IsTrue(content.EndsWith(contentCheck));
        }

        //[Test]
        public void TestReadXML1()
        {
            m_Comp.SI.Name = "QQQ";
            m_Comp.SI.Comment = null;
            m_Comp.SI.Version.PlatformID = 1;
            m_Comp.SI.Version.Type = 2;
            m_Comp.SI.Version.Major = 3;
            m_Comp.SI.Version.Minor = 4;
            // try serialize
            var ser = new XmlSerializer(m_Comp.GetType());
            string content;
            using (var sw = new StringWriter())
            {
                ser.Serialize(sw, m_Comp);
                content = sw.ToString();
            }
            // try deserialize
            TextReader tr = new StringReader(content);
            object result = ser.Deserialize(tr);
            tr.Close();
            // check deserialize result
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof (ComputerPanelItem), result);
            Assert.AreEqual("QQQ", ((ComputerPanelItem) result).Name);
            Assert.AreEqual(string.Empty, ((ComputerPanelItem) result).Comment);
        }

        //[Test]
        public void TestReadXML2()
        {
            const string content =
                "<ComputerPanelItem PlatformID=\"500\" Comment=\"WWW\" Version=\"5.1\" Type=\"11407\" />";
            var ser = new XmlSerializer(typeof (ComputerPanelItem));
            // try deserialize
            TextReader tr = new StringReader(content);
            object result = ser.Deserialize(tr);
            tr.Close();
            // check deserialize result
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof (ComputerPanelItem), result);
            Assert.AreEqual(string.Empty, ((ComputerPanelItem) result).Name);
            Assert.AreEqual("WWW", ((ComputerPanelItem) result).Comment);
        }

        [Test]
        public void ExceptionThis()
        {
            IComparable value;
            Assert.Throws<ArgumentOutOfRangeException>(() => value = m_Comp[m_Comp.CountColumns]);
        }

        [Test]
        public void TestComputerPanelItem()
        {
            m_Comp = new ComputerPanelItem(null, (ServerInfo) null);
            Assert.IsNotNull(m_Comp.SI);
        }

        [Test]
        public void TestFullName()
        {
            Assert.AreEqual(@"\\COMP01", m_Comp.FullName);
            var share = new SharePanelItem(m_Comp, "SHARE01");
            Assert.AreEqual(@"\\COMP01\SHARE01", share.FullName);
        }

        [Test]
        public void TestImageName()
        {
            Assert.AreEqual(PanelImageNames.COMPUTER + PanelImageNames.GREEN_POSTFIX, m_Comp.ImageName);
            m_Comp.IsReachable = false;
            Assert.AreEqual(PanelImageNames.COMPUTER + PanelImageNames.GREEN_POSTFIX, m_Comp.ImageName);
            m_Comp.Parent = new DomainRoot();
            m_Comp.IsReachable = true;
            Assert.AreEqual(PanelImageNames.COMPUTER, m_Comp.ImageName);
            m_Comp.IsReachable = false;
            Assert.AreEqual(PanelImageNames.COMPUTER + PanelImageNames.RED_POSTFIX, m_Comp.ImageName);
        }

        [Test]
        public void TestName()
        {
            Assert.AreEqual("COMP01", m_Comp.Name);
            m_Comp.Name = "test1";
            Assert.AreEqual("test1", m_Comp.Name);
            Assert.AreEqual("test1", m_Comp.SI.Name);
        }

        [Test]
        public void TestThis()
        {
            Assert.AreEqual(m_Comp.Name, m_Comp[0]);
            Assert.AreEqual(m_Comp.Comment, m_Comp[1]);
            Assert.AreEqual(m_Comp.SI.Version, m_Comp[2]);
        }
    }
}