using NUnit.Framework;
using LanExchange.Application.Interfaces;
using LanExchange.Presentation.Interfaces;
using Moq;
using LanExchange.Presentation.Interfaces.Menu;

namespace LanExchange.Application.Presenters
{
    [TestFixture]
    public class MainPresenterTest
    {
        private IMainPresenter presenter;
        private Mock<IMainView> mockView;

        private static IMainPresenter CreatePresenter()
        {
            var presenter = new MainPresenter(
                new Mock<ILazyThreadPool>().Object,
                new Mock<IPanelColumnManager>().Object,
                new Mock<IAppPresenter>().Object,
                new Mock<IPagesPresenter>().Object,
                new Mock<ITranslationService>().Object,
                new Mock<IHotkeyService>().Object,
                new Mock<IDisposableManager>().Object,
                new Mock<IAboutModel>().Object,
                new Mock<IViewFactory>().Object,
                new Mock<IWaitingService>().Object,
                new Mock<IPanelItemFactoryManager>().Object,
                new Mock<IScreenService>().Object,
                new Mock<ICommandManager>().Object,
                new Mock<IAppView>().Object,
                new Mock<IProcessService>().Object,
                new Mock<IImageManager>().Object,
                new Mock<IMenuProducer>().Object
                );
            return presenter;
        }

        [SetUp]
        public void SetUp()
        {
            presenter = CreatePresenter();
            mockView = new Mock<IMainView>();
            presenter.Initialize(mockView.Object);
        }

        [Test]
        public void PerformMenuKeyDown_MenuInvisible_ShowMenu()
        {
            presenter.PerformMenuKeyDown();
            mockView.VerifySet(m => m.MenuVisible = true);
        }

        [Test]
        public void PerformMenuKeyDown_MenuVisible_DontShowMenu()
        {
            mockView.SetupGet(m => m.MenuVisible).Returns(true);
            presenter.PerformMenuKeyDown();
            mockView.VerifySet(m => m.MenuVisible = true, Times.Never);
        }

        [Test]
        public void PerformMenuKeyUp_MenuVisible_HideMenu()
        {
            mockView.SetupGet(m => m.MenuVisible).Returns(true);
            presenter.PerformMenuKeyUp();
            mockView.VerifySet(m => m.MenuVisible = false);
        }

        [Test]
        public void PerformMenuKeyUp_AfterMenuKeyDown_DontHideMenu()
        {
            presenter.PerformMenuKeyDown();
            presenter.PerformMenuKeyUp();
            mockView.VerifySet(m => m.MenuVisible = false, Times.Never);
        }

        [Test]
        public void PerformEscapeKeyDown_FormVisible_HideForm()
        {
            mockView.SetupGet(m => m.Visible).Returns(true);
            presenter.PerformEscapeKeyDown();
            mockView.VerifySet(m => m.Visible = false);
        }

        [Test]
        public void PerformEscapeKeyUp_MenuVisible_HideMenu()
        {
            mockView.SetupGet(m => m.MenuVisible).Returns(true);
            presenter.PerformEscapeKeyUp();
            mockView.VerifySet(m => m.MenuVisible = false);
        }
    }
}
