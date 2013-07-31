using System;

namespace LanExchange.SDK
{
    public interface IPanelItemFactoryManager
    {
        void RegisterPanelItemFactory(Guid guid, PanelItemBaseFactory factory);
    }
}
