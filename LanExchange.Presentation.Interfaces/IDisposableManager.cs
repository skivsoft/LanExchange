using System;

namespace LanExchange.Presentation.Interfaces
{
    public interface IDisposableManager : IDisposable
    {
        void RegisterInstance(IDisposable instance);
    }
}
