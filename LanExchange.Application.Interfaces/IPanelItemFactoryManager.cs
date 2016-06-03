using System;
using System.Collections.Generic;

namespace LanExchange.Application.Interfaces
{
    public interface IPanelItemFactoryManager
    {
        void RegisterFactory<TPanelItem>(IPanelItemFactory factory) where TPanelItem : PanelItemBase;
        Func<PanelItemBase, bool> GetAvailabilityChecker(Type type);
        void CreateDefaultRoots();
        PanelItemBase CreateDefaultRoot(string typeName);
        
        IDictionary<Type, IPanelItemFactory> Types { get; }
        IList<PanelItemBase> DefaultRoots { get; }
        Type[] ToArray();
        bool IsEmpty { get; }
    }
}
