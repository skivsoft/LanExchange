using LanExchange.Interfaces;
using LanExchange.Presentation.Interfaces;
using LanExchange.SDK;
using Moq;
using NUnit.Framework;

namespace LanExchange.Presentation.WinForms.Controls
{
    [TestFixture]
    class PanelViewTest
    {
        [Test]
        public void Ctor_Presenter_InitializeCalled()
        {
            var presenter = new Mock<IPanelPresenter>();
            var control = new PanelView(
                presenter.Object,
                new Mock<IAddonManager>().Object,
                new Mock<IPanelItemFactoryManager>().Object,
                new Mock<ILazyThreadPool>().Object,
                new Mock<IImageManager>().Object,
                new Mock<IPanelColumnManager>().Object,
                new Mock<IPagesPresenter>().Object,
                new Mock<IFilterPresenter>().Object,
                new Mock<IUser32Service>().Object,
                new Mock<IPuntoSwitcherService>().Object);
            presenter.Verify(m => m.Initialize(control));
        }
    }
}
