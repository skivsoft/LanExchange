using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model;

namespace LanExchange.Interface
{
    public interface ITabParamsPresenter : ISubscriber
    {
        void SetView(ITabParamsView view);
        void SetInfo(PanelItemList info);
    }
}
