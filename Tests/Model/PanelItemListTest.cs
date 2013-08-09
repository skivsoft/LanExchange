using System;
using LanExchange.Plugin.Network;
using LanExchange.Plugin.Network.Panel;
using LanExchange.Presenter;
using LanExchange.SDK;
using Moq;
using NUnit.Framework;

namespace LanExchange.Model
{
    [TestFixture]
    class PanelItemListTest
    {
        private IPanelModel m_Objects;

        [SetUp]
        public void SetUp()
        {
            m_Objects = new PanelItemList("MyTab");
        }

        [TearDown]
        public void TearDown()
        {
            m_Objects = null;
        }

        [Test]
        public void TestApplyFilter_FilterTextNull()
        {
            m_Objects.FilterText = null;
            m_Objects.ApplyFilter();
            Assert.IsEmpty(m_Objects.FilterText);
        }

        [Test]
        public void TestCurrentPath()
        {
            Assert.IsNotNull(m_Objects.CurrentPath);
        }

        private void RetrieveTwoComps()
        {
            var domain = new DomainPanelItem(Network.ROOT_OF_DOMAINS, "TEST");
            m_Objects.CurrentPath.Push(Network.ROOT_OF_DOMAINS);
            m_Objects.CurrentPath.Push(domain);
            var filler = new Mock<IPanelFillerManager>();
            var result = new PanelFillerResult();
            result.Items.Add(new ComputerPanelItem(domain, "COMP01"));
            result.Items.Add(new ComputerPanelItem(domain, "COMP02"));
            result.ItemsType = typeof(ComputerPanelItem);
            filler.Setup(f => f.RetrievePanelItems(domain)).Returns(result);
            AppPresenter.PanelFillers = filler.Object;
            m_Objects.SyncRetrieveData();
            Assert.AreEqual(3, m_Objects.Count);
        }

        [Test]
        public void TestIndexOf()
        {
            m_Objects.CurrentPath.Push(Network.ROOT_OF_DOMAINS);
            var filler = new Mock<IPanelFillerManager>();
            var result = new PanelFillerResult();
            result.Items.Add(new DomainPanelItem(Network.ROOT_OF_DOMAINS, "TEST"));
            result.ItemsType = typeof (DomainPanelItem);
            filler.Setup(f => f.RetrievePanelItems(Network.ROOT_OF_DOMAINS)).Returns(result);
            AppPresenter.PanelFillers = filler.Object;
            m_Objects.SyncRetrieveData();
            Assert.AreEqual(1, m_Objects.Count);
            Assert.AreEqual(0, m_Objects.IndexOf("TEST"));
        }


        [Test]
        public void TestIndexOfDots()
        {
            RetrieveTwoComps();
            Assert.AreEqual(0, m_Objects.IndexOf(string.Empty));
            Assert.AreEqual(1, m_Objects.IndexOf("COMP01"));
            Assert.AreEqual(2, m_Objects.IndexOf("COMP02"));
        }

        [Test]
        public void TestGetItem()
        {
            RetrieveTwoComps();
            Assert.IsNull(m_Objects.GetItem(null));
            var item = m_Objects.GetItem("");
            Assert.IsNotNull(item);
            Assert.IsInstanceOf<PanelItemDoubleDot>(item);
        }

    }
}
