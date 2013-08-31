using System;
using System.Collections.Generic;

namespace LanExchange.SDK
{
    public interface IPanelItemFactoryManager
    {
        void RegisterPanelItemFactory(Type type, PanelItemFactoryBase factory);
        void CreateDefaultRoots();
        IList<PanelItemBase> DefaultRoots { get; }

        Type[] ToArray();
    }
}
