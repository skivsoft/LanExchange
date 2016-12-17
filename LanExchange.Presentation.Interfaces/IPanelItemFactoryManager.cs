using System;
using System.Collections.Generic;

namespace LanExchange.Presentation.Interfaces
{
    public interface IPanelItemFactoryManager
    {
        IDictionary<Type, IPanelItemFactory> Types { get; }

        IList<PanelItemBase> DefaultRoots { get; }

        bool IsEmpty { get; }

        void RegisterFactory<TPanelItem>(IPanelItemFactory factory) where TPanelItem : PanelItemBase;

        Func<PanelItemBase, bool> GetAvailabilityChecker(Type type);

        void CreateDefaultRoots();

        PanelItemBase CreateDefaultRoot(string typeName);
        
        Type[] ToArray();
    }
}
