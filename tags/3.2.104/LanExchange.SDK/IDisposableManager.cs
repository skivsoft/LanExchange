using System;

namespace LanExchange.SDK
{
    public interface IDisposableManager : IDisposable
    {
        void RegisterInstance(IDisposable instance);
    }
}
