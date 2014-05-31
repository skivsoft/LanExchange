using System;
using System.Collections.Generic;

namespace LanExchange.SDK
{
    public interface IPanelItemFactoryManager
    {
        void RegisterFactory<TPanelItem>(IPanelItemFactory factory) where TPanelItem : PanelItemBase;
        void CreateDefaultRoots();
        IDictionary<Type, IPanelItemFactory> Types { get; }
        PanelItemBase CreateDefaultRoot(string typeName);
        IList<PanelItemBase> DefaultRoots { get; }

        Type[] ToArray();

        bool IsEmpty { get; }
    }
}
