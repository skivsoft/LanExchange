using LanExchange.Plugin.Network.NetApi;
using NUnit.Framework;

namespace LanExchange.Plugin.Network
{
    internal class ShareInfoTest
    {
        private ShareInfo share;

        [SetUp]
        public void SetUp()
        {
            var info = new SHARE_INFO_1();
            share = new ShareInfo(info);
        }

        [TearDown]
        public void TearDown()
        {
            share = null;
        }

        [Test]
        public void TestCtor()
        {
            share = new ShareInfo();
            Assert.IsNull(share.Name);
        }

        [Test]
        public void TestName()
        {
            share.Name = "C$";
            Assert.AreEqual("C$", share.Name);
        }

        [Test]
        public void TestComment()
        {
            share.Comment = "the comment";
            Assert.AreEqual("the comment", share.Comment);
        }

        [Test]
        public void TestIsHidden()
        {
            share.Name = string.Empty;
            Assert.IsFalse(share.IsHidden);
            share.Name = "C";
            Assert.IsFalse(share.IsHidden);
            share.Name = "D$";
            Assert.IsTrue(share.IsHidden);
        }

        [Test]
        public void TestCompareTo()
        {
            var other = new ShareInfo(new SHARE_INFO_1());
            share.Name = "CCC";
            other.Name = "ccc";
            Assert.AreEqual(0, share.CompareTo(other));
            other.Name = "DDd";
            Assert.Less(share.CompareTo(other), 0);
            other.Name = "bbB";
            Assert.Greater(share.CompareTo(other), 0);
        }
    }
}