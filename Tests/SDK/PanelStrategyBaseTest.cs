using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;

namespace LanExchange.SDK.Tests
{
    [TestFixture]
    class PanelStrategyBaseTest
    {
        [Test]
        public void TestIsSubjectAccepted()
        {
            var mock = MockRepository.GenerateMock<IPanelFillerStrategy>();
            Assert.IsFalse(mock.IsParentAccepted(null));
        }
    }
}
