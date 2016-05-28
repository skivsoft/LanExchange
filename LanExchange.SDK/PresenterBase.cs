using System;
using LanExchange.SDK.Presentation.Interfaces;

namespace LanExchange.SDK
{
    public abstract class PresenterBase<TView> : IPresenter<TView> where TView : IView
    {
        public void Initialize(TView view)
        {
            if (view == null) throw new ArgumentNullException(nameof(view));
            View = view;
            InitializePresenter();
        }

        protected virtual void InitializePresenter()
        {
        }

        protected TView View { get; private set; }
    }
}