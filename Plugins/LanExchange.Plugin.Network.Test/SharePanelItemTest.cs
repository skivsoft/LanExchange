using System;
using LanExchange.Plugin.Network.NetApi;
using LanExchange.Presentation.Interfaces;
using NUnit.Framework;

namespace LanExchange.Plugin.Network.Test
{
    internal class SharePanelItemTest
    {
        private SharePanelItem share;

        [SetUp]
        public void SetUp()
        {
            var info = new SHARE_INFO_1();
            share = new SharePanelItem(null, new ShareInfo(info));
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
            share.ShareType = (uint)SHARE_TYPE.STYPE_PRINTQ;
            share.Name = "HP";
            Assert.AreEqual(string.Empty, share.ImageName);
            share.ShareType = (uint)SHARE_TYPE.STYPE_DEVICE;
            Assert.AreEqual((uint)SHARE_TYPE.STYPE_DEVICE, share.SHI.ShareType);
            Assert.AreEqual(PanelImageNames.FOLDER, share.ImageName);
            share.Name = "C$";
            Assert.AreEqual(PanelImageNames.FOLDER + PanelImageNames.HiddenPostfix, share.ImageName);
        }

        [Test]
        public void ExceptionThis()
        {
            IComparable value;
            Assert.Throws<ArgumentOutOfRangeException>(() => value = share[share.CountColumns]);
        }

        [Test]
        public void TestThis()
        {
            Assert.AreEqual(share.Name, share[0]);
            Assert.AreEqual(share.Comment, share[1]);
        }

        [Test]
        public void TestComputerName()
        {
            Assert.IsEmpty(share.ComputerName);
        }

        [Test]
        public void TestShareType()
        {
            share.SHI.ShareType = 100;
            Assert.AreEqual(100, share.ShareType);
        }
    }
}