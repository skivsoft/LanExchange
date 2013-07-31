using System;
using LanExchange.Plugin.Network.Panel;
using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Plugin.Network.Tests.Panel
{
    internal class SharePanelItemTest
    {
        private SharePanelItem m_Share;

        [SetUp]
        public void SetUp()
        {
            var info = new NetApi32.SHARE_INFO_1();
            m_Share = new SharePanelItem(null, new ShareInfo(info));
        }

        [Test, ExpectedException(typeof (ArgumentNullException))]
        public void ExceptionSharePanelItem()
        {
            var share = new SharePanelItem(null, (ShareInfo) null);
        }

        [Test]
        public void TestSharePanelItem()
        {
            var share = new SharePanelItem(null, "C$");
            Assert.AreEqual("C$", share.SHI.Name);
            Assert.IsEmpty(share.Comment);
        }

        [Test]
        public void TestImageName()
        {
            m_Share.Name = PanelItemBase.s_DoubleDot;
            Assert.AreEqual(PanelImageNames.DoubleDot, m_Share.ImageName);
            m_Share.ShareType = (uint) NetApi32.SHARE_TYPE.STYPE_PRINTQ;
            m_Share.Name = "HP";
            Assert.AreEqual(PanelImageNames.SharePrinter, m_Share.ImageName);
            m_Share.ShareType = (uint) NetApi32.SHARE_TYPE.STYPE_DEVICE;
            Assert.AreEqual((uint)NetApi32.SHARE_TYPE.STYPE_DEVICE, m_Share.SHI.ShareType);
            Assert.AreEqual(PanelImageNames.ShareNormal, m_Share.ImageName);
            m_Share.Name = "C$";
            Assert.AreEqual(PanelImageNames.ShareHidden, m_Share.ImageName);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExceptionThis()
        {
            var value = m_Share[m_Share.CountColumns];
        }

        [Test]
        public void TestThis()
        {
            Assert.AreEqual(m_Share.Name, m_Share[0]);
            Assert.AreEqual(m_Share.Comment, m_Share[1]);
        }

        [Test]
        public void TestComputerName()
        {
            Assert.IsEmpty(m_Share.ComputerName);
        }

        [Test]
        public void TestShareType()
        {
            m_Share.SHI.ShareType = 100;
            Assert.AreEqual(100, m_Share.ShareType);
        }

        [Test]
        public void TestCreateColumnHeader()
        {
            for(int i = 0; i < m_Share.CountColumns; i++)
            {
                var header = m_Share.CreateColumnHeader(i);
                Assert.IsNotNull(header);
            }
            Assert.IsNull(m_Share.CreateColumnHeader(m_Share.CountColumns));
        }
    }
}
