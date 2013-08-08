using NUnit.Framework;

namespace LanExchange.Plugin.Network
{
    class ShareInfoTest
    {
        private ShareInfo m_Share;

        [SetUp]
        protected void SetUp()
        {
            var info = new NetApi32.SHARE_INFO_1();
            m_Share = new ShareInfo(info);
        }

        [TearDown]
        protected void TearDown()
        {
            m_Share = null;
        }

        [Test]
        public void TestCtor()
        {
            m_Share = new ShareInfo();
            Assert.IsNull(m_Share.Name);
        }

        [Test]
        public void TestName()
        {
            m_Share.Name = "C$";
            Assert.AreEqual("C$", m_Share.Name);
        }

        [Test]
        public void TestComment()
        {
            m_Share.Comment = "the comment";
            Assert.AreEqual("the comment", m_Share.Comment);
        }

        [Test]
        public void TestIsHidden()
        {
            m_Share.Name = "";
            Assert.IsFalse(m_Share.IsHidden);
            m_Share.Name = "C";
            Assert.IsFalse(m_Share.IsHidden);
            m_Share.Name = "D$";
            Assert.IsTrue(m_Share.IsHidden);
        }

        [Test]
        public void TestCompareTo()
        {
            var other = new ShareInfo(new NetApi32.SHARE_INFO_1());
            m_Share.Name = "CCC";
            other.Name = "ccc";
            Assert.AreEqual(0, m_Share.CompareTo(other));
            other.Name = "DDd";
            Assert.Less(m_Share.CompareTo(other), 0);
            other.Name = "bbB";
            Assert.Greater(m_Share.CompareTo(other), 0);

        }
    }
}
