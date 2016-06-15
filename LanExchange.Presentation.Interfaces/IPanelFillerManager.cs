using System;

namespace LanExchange.Presentation.Interfaces
{
    public interface IPanelFillerManager
    {
        void RegisterFiller<TPanelItem>(IPanelFiller filler) where TPanelItem : PanelItemBase;
        Type GetFillType(PanelItemBase parent);
        PanelFillerResult RetrievePanelItems(PanelItemBase parent);
        bool FillerExists(PanelItemBase panelItem);
    }
}
