using System.Collections.Generic;

namespace LanExchange.SDK
{
    public interface IPanelFillerManager
    {
        void RegisterPanelFiller(IPanelFiller filler);
        PanelFillerResult RetrievePanelItems(PanelItemBase parent);
        bool FillerExists(PanelItemBase panelItem);
    }
}
