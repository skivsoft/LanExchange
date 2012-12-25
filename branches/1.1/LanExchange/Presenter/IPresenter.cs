using System;

namespace LanExchange.Presenter
{
    public interface IPresenter
    {
        void LoadFromModel();
        void SaveToModel();
    }
}
