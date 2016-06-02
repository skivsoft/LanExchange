using System;

namespace LanExchange.Application.Interfaces
{
    public interface IDisposableManager : IDisposable
    {
        void RegisterInstance(IDisposable instance);
    }
}
