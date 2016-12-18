using LanExchange.Presentation.Interfaces;
using Moq;
using NUnit.Framework;

namespace LanExchange.Presentation.WinForms.Controls
{
    [TestFixture]
    internal class StatusPanelTest
    {
        [Test]
        public void Ctor_Presenter_InitializeCalled()
        {
            var presenter = new Mock<IStatusPanelPresenter>();
            var control = new StatusPanel(presenter.Object);
            presenter.Verify(m => m.Initialize(control));
        }
    }
}
