using System;
using LanExchange.Plugin.Network.Panel;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Plugin.Network.Tests.Panel
{
    [TestFixture]
    internal class ComputerPanelItemTest
    {
        private ComputerPanelItem m_Comp;

        [SetUp]
        public void SetUp()
        {
            var info = new ServerInfo();
            info.Name = "COMP01";
            info.Comment = "Hello world";
            info.PlatformID = (uint) NetApi32.SV_101_PLATFORM.PLATFORM_ID_NT;
            info.VersionMajor = 6;
            info.VersionMinor = 2;
            m_Comp = new ComputerPanelItem(null, info);
        }

        [TearDown]
        public void TearDown()
        {
            m_Comp = null;
        }

        [Test, ExpectedException(typeof (ArgumentNullException))]
        public void ExceptionComputerPanelItem()
        {
            new ComputerPanelItem(null, (ServerInfo)null);
        }

        [Test]
        public void TestName()
        {
            Assert.AreEqual("COMP01", m_Comp.Name);
            m_Comp.Name = "test1";
            Assert.AreEqual("test1", m_Comp.Name);
            Assert.AreEqual("test1", m_Comp.SI.Name);
        }

        [Test, ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void ExceptionThis()
        {
            var value = m_Comp[m_Comp.CountColumns];
        }

        [Test]
        public void TestThis()
        {
            Assert.AreEqual(m_Comp.Name, m_Comp[0]);
            Assert.AreEqual(m_Comp.Comment, m_Comp[1]);
            Assert.AreEqual(m_Comp.SI.Version(), m_Comp[2]);
        }

        [Test]
        public void TestImageName()
        {
            m_Comp.IsPingable = true;
            Assert.AreEqual(PanelImageNames.ComputerNormal, m_Comp.ImageName);
            m_Comp.IsPingable = false;
            Assert.AreEqual(PanelImageNames.ComputerDisabled, m_Comp.ImageName);
            m_Comp.Name = PanelItemBase.s_DoubleDot;
            Assert.AreEqual(PanelImageNames.DoubleDot, m_Comp.ImageName);
        }

        [Test]
        public void TestToolTipText()
        {
            Assert.IsNotEmpty(m_Comp.ToolTipText);
        }

        [Test]
        public void TestToString()
        {
            Assert.AreEqual(@"\\COMP01", m_Comp.ToString());
        }
    }
}
