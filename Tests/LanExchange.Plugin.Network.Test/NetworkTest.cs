using System;
using Moq;
using NUnit.Framework;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Network
{
    [TestFixture]
    internal class NetworkTest
    {
        [Test]
        public void TestInitialize()
        {
            var network = new PluginNetwork();
            var mock = new Mock<IServiceProvider>();
            var typeManager = Mock.Of<IPanelItemFactoryManager>();
            var columnManager = Mock.Of<IPanelColumnManager>();
            var fillerManager = Mock.Of<IPanelFillerManager>();
            mock.Setup(f => f.GetService(typeof(IPanelItemFactoryManager))).Returns(typeManager);
            mock.Setup(f => f.GetService(typeof(IPanelColumnManager))).Returns(columnManager);
            mock.Setup(f => f.GetService(typeof(IPanelFillerManager))).Returns(fillerManager);
            network.Initialize(mock.Object);
        }
    }
}