using System;
using LanExchange.SDK.Model;

namespace LanExchange.SDK
{
    public interface IPanelUpdater : IDisposable
    {
        void Stop();
        void Start(IPanelModel model, bool clearFilter);
    }
}