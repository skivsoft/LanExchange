using LanExchange.Presentation.Interfaces;
using Moq;
using NUnit.Framework;

namespace LanExchange.Presentation.WinForms.Forms
{
    [TestFixture]
    class AppViewTest
    {
        [Test]
        public void Ctor_Presenter_InitializeCalled()
        {
            var presenter = new Mock<IAppPresenter>();
            var application = new AppView(presenter.Object);
            presenter.Verify(m => m.Initialize(application));
        }
    }
}
