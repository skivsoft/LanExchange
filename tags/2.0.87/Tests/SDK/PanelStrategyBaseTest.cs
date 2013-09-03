using NUnit.Framework;
using Rhino.Mocks;

namespace LanExchange.SDK
{
    [TestFixture]
    class PanelStrategyBaseTest
    {
        [Test]
        public void TestIsSubjectAccepted()
        {
            var mock = MockRepository.GenerateMock<IPanelFiller>();
            Assert.IsFalse(mock.IsParentAccepted(null));
        }
    }
}
