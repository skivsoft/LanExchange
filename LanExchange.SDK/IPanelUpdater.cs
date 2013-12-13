using System;

namespace LanExchange.SDK
{
    public interface IPanelUpdater : IDisposable
    {
        void Stop();
        void Start(IPanelModel model, bool clearFilter);
    }
}