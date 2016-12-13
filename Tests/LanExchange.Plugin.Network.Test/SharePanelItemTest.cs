using System;
using LanExchange.Plugin.Network.NetApi;
using NUnit.Framework;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Network.Test
{
    internal class SharePanelItemTest
    {
        private SharePanelItem m_Share;

        [SetUp]
        public void SetUp()
        {
            var info = new SHARE_INFO_1();
            m_Share = new SharePanelItem(null, new ShareInfo(info));
        }

        [Test]
        public void ExceptionSharePanelItem()
        {
            SharePanelItem share;
            Assert.Throws<ArgumentNullException>(() => share = new SharePanelItem(null, (ShareInfo)null));
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
            m_Share.ShareType = (uint)SHARE_TYPE.STYPE_PRINTQ;
            m_Share.Name = "HP";
            Assert.AreEqual(string.Empty, m_Share.ImageName);
            m_Share.ShareType = (uint)SHARE_TYPE.STYPE_DEVICE;
            Assert.AreEqual((uint)SHARE_TYPE.STYPE_DEVICE, m_Share.SHI.ShareType);
            Assert.AreEqual(PanelImageNames.FOLDER, m_Share.ImageName);
            m_Share.Name = "C$";
            Assert.AreEqual(PanelImageNames.FOLDER + PanelImageNames.HiddenPostfix, m_Share.ImageName);
        }

        [Test]
        public void ExceptionThis()
        {
            IComparable value;
            Assert.Throws<ArgumentOutOfRangeException>(() => value = m_Share[m_Share.CountColumns]);
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
    }
}