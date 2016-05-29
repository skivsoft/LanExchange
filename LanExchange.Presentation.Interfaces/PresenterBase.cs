using System;

namespace LanExchange.Presentation.Interfaces
{
    public abstract class PresenterBase<TView> : IPresenter<TView> where TView : IView
    {
        public void Initialize(TView view)
        {
            if (view == null) throw new ArgumentNullException(nameof(view));
            View = view;
            InitializePresenter();
        }

        protected abstract void InitializePresenter();

        protected TView View { get; private set; }
    }
}