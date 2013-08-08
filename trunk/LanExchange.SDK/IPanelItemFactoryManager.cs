using System;
using System.Collections.Generic;

namespace LanExchange.SDK
{
    public interface IPanelItemFactoryManager
    {
        void RegisterPanelItemFactory(Type type, PanelItemBaseFactory factory);
        IEnumerable<PanelItemBase> CreateDefaultRoots();
        Type[] ToArray();
    }
}
