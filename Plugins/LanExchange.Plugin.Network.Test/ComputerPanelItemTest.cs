using System;
using System.IO;
using System.Xml.Serialization;
using LanExchange.Plugin.Network.NetApi;
using LanExchange.Presentation.Interfaces;
using NUnit.Framework;

namespace LanExchange.Plugin.Network
{
    [TestFixture]
    internal class ComputerPanelItemTest
    {
        private ComputerPanelItem comp;

        [SetUp]
        public void SetUp()
        {
            var info = new ServerInfo();
            info.Name = "COMP01";
            info.Comment = "Hello world";
            info.Version.PlatformId = (uint)SV_101_PLATFORM.PLATFORM_ID_NT;
            info.Version.Major = 6;
            info.Version.Minor = 2;
            comp = new ComputerPanelItem(null, info);
        }

        [TearDown]
        public void TearDown()
        {
            comp = null;
        }

        [Test]
        public void TestWriteXML()
        {
            comp.SI.Name = "QQQ";
            comp.SI.Comment = "WWW";
            comp.SI.Version.PlatformId = 1;
            comp.SI.Version.Type = 2;
            comp.SI.Version.Major = 3;
            comp.SI.Version.Minor = 4;
            
            // try serialize
            var ser = new XmlSerializer(comp.GetType());
            string content;
            using (var sw = new StringWriter())
            {
                ser.Serialize(sw, comp);
                content = sw.ToString();
            }

            const string ContentCheck =
                "<ComputerPanelItem Name=\"QQQ\" PlatformID=\"1\" Version=\"3.4\" Type=\"2\" Comment=\"WWW\" />";
            Assert.IsTrue(content.EndsWith(ContentCheck));
        }

        [Test]
        public void TestReadXML1()
        {
            comp.SI.Name = "QQQ";
            comp.SI.Comment = null;
            comp.SI.Version.PlatformId = 1;
            comp.SI.Version.Type = 2;
            comp.SI.Version.Major = 3;
            comp.SI.Version.Minor = 4;

            // try serialize
            var ser = new XmlSerializer(comp.GetType());
            string content;
            using (var sw = new StringWriter())
            {
                ser.Serialize(sw, comp);
                content = sw.ToString();
            }
            
            // try deserialize
            TextReader tr = new StringReader(content);
            object result = ser.Deserialize(tr);
            tr.Close();
            
            // check deserialize result
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ComputerPanelItem), result);
            Assert.AreEqual("QQQ", ((ComputerPanelItem)result).Name);
            Assert.AreEqual(string.Empty, ((ComputerPanelItem)result).Comment);
        }

        [Test]
        public void TestReadXML2()
        {
            const string Content =
                "<ComputerPanelItem PlatformID=\"500\" Comment=\"WWW\" Version=\"5.1\" Type=\"11407\" />";
            var ser = new XmlSerializer(typeof(ComputerPanelItem));

            // try deserialize
            TextReader tr = new StringReader(Content);
            object result = ser.Deserialize(tr);
            tr.Close();

            // check deserialize result
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ComputerPanelItem), result);
            Assert.AreEqual(string.Empty, ((ComputerPanelItem)result).Name);
            Assert.AreEqual("WWW", ((ComputerPanelItem)result).Comment);
        }

        [Test]
        public void ExceptionThis()
        {
            IComparable value;
            Assert.Throws<ArgumentOutOfRangeException>(() => value = comp[comp.CountColumns]);
        }

        [Test]
        public void TestComputerPanelItem()
        {
            comp = new ComputerPanelItem(null, (ServerInfo)null);
            Assert.IsNotNull(comp.SI);
        }

        [Test]
        public void TestFullName()
        {
            Assert.AreEqual(@"\\COMP01", comp.FullName);
            var share = new SharePanelItem(comp, "SHARE01");
            Assert.AreEqual(@"\\COMP01\SHARE01", share.FullName);
        }

        [Test]
        public void TestImageName()
        {
            Assert.AreEqual(PanelImageNames.COMPUTER + PanelImageNames.GreenPostfix, comp.ImageName);
            comp.IsReachable = false;
            Assert.AreEqual(PanelImageNames.COMPUTER + PanelImageNames.GreenPostfix, comp.ImageName);
            comp.Parent = new DomainRoot();
            comp.IsReachable = true;
            Assert.AreEqual(PanelImageNames.COMPUTER, comp.ImageName);
            comp.IsReachable = false;
            Assert.AreEqual(PanelImageNames.COMPUTER + PanelImageNames.RedPostfix, comp.ImageName);
        }

        [Test]
        public void TestName()
        {
            Assert.AreEqual("COMP01", comp.Name);
            comp.Name = "test1";
            Assert.AreEqual("test1", comp.Name);
            Assert.AreEqual("test1", comp.SI.Name);
        }

        [Test]
        public void TestThis()
        {
            Assert.AreEqual(comp.Name, comp[0]);
            Assert.AreEqual(comp.Comment, comp[1]);
            Assert.AreEqual(comp.SI.Version, comp[2]);
        }
    }
}