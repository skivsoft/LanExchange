using System;

namespace LanExchange.SDK
{
    public interface IPanelItemFactoryManager
    {
        void RegisterPanelItemFactory(Type type, PanelItemBaseFactory factory);
    }
}
