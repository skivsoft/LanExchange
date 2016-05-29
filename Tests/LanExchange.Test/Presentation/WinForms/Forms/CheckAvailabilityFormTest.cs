using LanExchange.Plugin.WinForms.Forms;
using LanExchange.Presentation.Interfaces;
using Moq;
using NUnit.Framework;

namespace LanExchange.Presentation.WinForms.Forms
{
    [TestFixture]
    class CheckAvailabilityFormTest
    {
        [Test]
        public void Ctor_Presenter_InitializeCalled()
        {
            var presenter = new Mock<ICheckAvailabilityPresenter>();
            var form = new CheckAvailabilityForm(presenter.Object);
            presenter.Verify(m => m.Initialize(form));
        }
    }
}
