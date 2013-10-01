using System;

namespace LanExchange.SDK
{
    public interface IPanelFillerManager
    {
        void RegisterPanelFiller(IPanelFiller filler);
        Type GetFillType(PanelItemBase parent);
        PanelFillerResult RetrievePanelItems(PanelItemBase parent);
        bool FillerExists(PanelItemBase panelItem);
    }
}
