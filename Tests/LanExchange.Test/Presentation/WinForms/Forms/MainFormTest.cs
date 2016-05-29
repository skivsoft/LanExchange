using LanExchange.Interfaces;
using LanExchange.Plugin.WinForms.Forms;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Factories;
using LanExchange.SDK;
using LanExchange.SDK.Managers;
using Moq;
using NUnit.Framework;

namespace LanExchange.Presentation.WinForms.Forms
{
    [TestFixture]
    class MainFormTest
    {
        [Test]
        public void Ctor_Presenter_InitializeCalled()
        {
            var presenter = new Mock<IMainPresenter>();
            var form = new MainForm(
                presenter.Object,
                new Mock<IAboutPresenter>().Object,
                new Mock<ILazyThreadPool>().Object,
                new Mock<IImageManager>().Object,
                new Mock<IActionManager>().Object,
                new Mock<ITranslationService>().Object,
                new Mock<IViewFactory>().Object
                );
            presenter.Verify(m => m.Initialize(form));
        }
    }
}
