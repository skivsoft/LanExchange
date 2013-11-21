using System;
using System.Collections.Generic;

namespace LanExchange.SDK
{
    public interface IPanelItemFactoryManager
    {
        void RegisterPanelItemFactory(Type type, PanelItemFactoryBase factory);
        void CreateDefaultRoots();
        PanelItemBase CreateDefaultRoot(string typeName);
        IList<PanelItemBase> DefaultRoots { get; }

        Type[] ToArray();
    }
}
