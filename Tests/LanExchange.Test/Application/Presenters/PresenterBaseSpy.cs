using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Presenters
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
