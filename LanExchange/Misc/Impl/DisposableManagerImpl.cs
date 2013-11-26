using System;
using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange.Misc.Impl
{
    public class DisposableManagerImpl : IDisposableManager
    {
        private readonly IList<IDisposable> m_List;

        public DisposableManagerImpl()
        {
            m_List = new List<IDisposable>();
        }

        public void RegisterInstance(IDisposable instance)
        {
            m_List.Add(instance);
        }

        public void Dispose()
        {
            foreach(var instance in m_List)
                instance.Dispose();
            m_List.Clear();
        }
    }
}
