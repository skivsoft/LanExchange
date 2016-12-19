using System;
using System.Collections.Generic;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Implementation
{
    public sealed class DisposableManager : IDisposableManager
    {
        private readonly IList<IDisposable> list;

        public DisposableManager()
        {
            list = new List<IDisposable>();
        }

        public void RegisterInstance(IDisposable instance)
        {
            list.Add(instance);
        }

        public void Dispose()
        {
            foreach (var instance in list)
                instance.Dispose();
            list.Clear();
        }
    }
}