using LanExchange.SDK;
using LanExchange.SDK.Presentation.Interfaces;

namespace LanExchange.Presenter
{
    internal class PresenterBaseSpy<T> : PresenterBase<T> where T : IView
    {
        public bool InitializePresenterCalled { get; private set; }
        public T ExposedView { get; private set; }

        protected override void InitializePresenter()
        {
            InitializePresenterCalled = true;
            ExposedView = View;
        }
    }
}
