using LanExchange.Presentation.Interfaces;
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
                new Mock<ICommandManager>().Object,
                new Mock<ITranslationService>().Object,
                new Mock<IViewFactory>().Object
                );
            presenter.Verify(m => m.Initialize(form));
        }
    }
}
