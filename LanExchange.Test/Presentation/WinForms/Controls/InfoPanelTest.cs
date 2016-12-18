using LanExchange.Presentation.Interfaces;
using Moq;
using NUnit.Framework;

namespace LanExchange.Presentation.WinForms.Controls
{
    [TestFixture]
    public class InfoPanelTest
    {
        [Test]
        public void Ctor_Presenter_InitializeCalled()
        {
            var presenter = new Mock<IInfoPresenter>();
            var control = new InfoPanel(presenter.Object);
            presenter.Verify(m => m.Initialize(control));
        }
    }
}
