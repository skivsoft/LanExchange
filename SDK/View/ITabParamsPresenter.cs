using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Sdk.Model;

namespace LanExchange.Sdk.View
{
    public interface ITabParamsPresenter : ISubscriber
    {
        void SetView(ITabParamsView view);
        void SetInfo(IPanelModel info);
    }
}
