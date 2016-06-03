using System;

namespace LanExchange.Application.Interfaces
{
    public interface IPanelUpdater : IDisposable
    {
        void Stop();
        void Start(IPanelModel model, bool clearFilter);
    }
}