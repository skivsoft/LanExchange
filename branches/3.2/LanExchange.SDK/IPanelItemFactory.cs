using System;

namespace LanExchange.SDK
{
    public interface IPanelItemFactory
    {
        PanelItemBase CreatePanelItem(PanelItemBase parent, string name);

        PanelItemBase CreateDefaultRoot();

        Func<PanelItemBase, bool> GetAvailabilityChecker();

        void RegisterColumns(IPanelColumnManager columnManager);
    }
}