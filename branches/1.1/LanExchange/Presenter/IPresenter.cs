using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.Presenter
{
    public interface IPresenter
    {
        void LoadFromModel();
        void SaveToModel();
    }
}
