using System;
using LanExchange.Core;
using LanExchange.Misc;
using LanExchange.Plugin.Network;
using LanExchange.Presenter;
using LanExchange.SDK;
using LanExchange.SDK.Model;
using Moq;
using NUnit.Framework;
using LanExchange.Intf;

namespace LanExchange.Model
{
    [TestFixture]
    class PanelItemListTest
    {
        private IPanelModel m_Objects;
        private ComputerPanelItem m_Comp01;
        private ComputerPanelItem m_Comp02;

        [SetUp]
        public void SetUp()
        {
            App.SetContainer(ContainerBuilder.Build());
            m_Objects = new PanelModel();
            m_Objects.TabName = "MyTab";
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
            var domain = new DomainPanelItem(PluginNetwork.ROOT_OF_DOMAINS, "TEST");
            m_Objects.CurrentPath.Push(PluginNetwork.ROOT_OF_DOMAINS);
            m_Objects.CurrentPath.Push(domain);
            var filler = new Mock<IPanelFillerManager>();
            var result = new PanelFillerResult();
            m_Comp01 = new ComputerPanelItem(domain, "COMP01");
            m_Comp02 = new ComputerPanelItem(domain, "COMP02");
            result.Items.Add(m_Comp01);
            result.Items.Add(m_Comp02);
            result.ItemsType = typeof(ComputerPanelItem);
            filler.Setup(f => f.RetrievePanelItems(domain)).Returns(result);
            App.PanelFillers = filler.Object;
            m_Objects.SyncRetrieveData();
            Assert.AreEqual(3, m_Objects.Count);
        }

        [Test]
        public void TestIndexOf()
        {
            m_Objects.CurrentPath.Push(PluginNetwork.ROOT_OF_DOMAINS);
            var filler = new Mock<IPanelFillerManager>();
            var result = new PanelFillerResult();
            var domain = new DomainPanelItem(PluginNetwork.ROOT_OF_DOMAINS, "TEST");
            result.Items.Add(domain);
            result.ItemsType = typeof (DomainPanelItem);
            filler.Setup(f => f.RetrievePanelItems(PluginNetwork.ROOT_OF_DOMAINS)).Returns(result);
            App.PanelFillers = filler.Object;
            m_Objects.SyncRetrieveData();
            Assert.AreEqual(1, m_Objects.Count);
            Assert.AreEqual(0, m_Objects.IndexOf(domain));
        }


        [Test]
        public void TestIndexOfDots()
        {
            RetrieveTwoComps();
            //Assert.AreEqual(0, m_Objects.IndexOf(new PanelItemDoubleDot(null)));
            Assert.AreEqual(1, m_Objects.IndexOf(m_Comp01));
            Assert.AreEqual(2, m_Objects.IndexOf(m_Comp02));
        }
    }
}
