using LanExchange.Presentation.Interfaces;
using Moq;
using NUnit.Framework;

namespace LanExchange.Presentation.WinForms.Forms
{
    [TestFixture]
    class AboutFormTest
    {
        [Test]
        public void Ctor_Presenter_InitializeCalled()
        {
            var presenter = new Mock<IAboutPresenter>();
            var form = new AboutForm(presenter.Object);
            presenter.Verify(m => m.Initialize(form));
        }
    }
}
