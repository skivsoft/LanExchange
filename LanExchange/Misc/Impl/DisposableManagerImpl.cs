using System;
using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange.Misc.Impl
{
    public class DisposableManagerImpl : IDisposableManager
    {
        private readonly IList<IDisposable> list;

        public DisposableManagerImpl()
        {
            list = new List<IDisposable>();
        }

        public void RegisterInstance(IDisposable instance)
        {
            list.Add(instance);
        }

        public void Dispose()
        {
            foreach(var instance in list)
                instance.Dispose();
            list.Clear();
        }
    }
}
