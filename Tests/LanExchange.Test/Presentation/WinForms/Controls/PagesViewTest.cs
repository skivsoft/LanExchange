using LanExchange.Application.Interfaces;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Factories;
using LanExchange.SDK;
using LanExchange.SDK.Factories;
using LanExchange.SDK.Managers;
using Moq;
using NUnit.Framework;

namespace LanExchange.Presentation.WinForms.Controls
{
    [TestFixture]
    class PagesViewTest
    {
        [Test]
        public void Ctor_Presenter_InitializeCalled()
        {
            var presenter = new Mock<IPagesPresenter>();
            var control = new PagesView(
                presenter.Object,
                new Mock<IPanelItemFactoryManager>().Object,
                new Mock<IImageManager>().Object,
                new Mock<IPanelFillerManager>().Object,
                new Mock<ICommandManager>().Object,
                new Mock<IModelFactory>().Object,
                new Mock<IViewFactory>().Object);
            presenter.Verify(m => m.Initialize(control));
        }
    }
}
