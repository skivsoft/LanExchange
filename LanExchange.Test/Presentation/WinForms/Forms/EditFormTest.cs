using LanExchange.Presentation.Interfaces;
using Moq;
using NUnit.Framework;

namespace LanExchange.Presentation.WinForms.Forms
{
    [TestFixture]
    public class EditFormTest
    {
        [Test]
        public void Ctor_Presenter_InitializeCalled()
        {
            var presenter = new Mock<IEditPresenter>();
            var form = new EditForm(presenter.Object);
            presenter.Verify(m => m.Initialize(form));
        }
    }
}
