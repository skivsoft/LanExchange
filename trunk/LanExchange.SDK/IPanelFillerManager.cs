using System;

namespace LanExchange.SDK
{
    public interface IPanelFillerManager
    {
        void RegisterFiller<TPanelItem>(PanelFillerBase filler) where TPanelItem : PanelItemBase;
        Type GetFillType(PanelItemBase parent);
        PanelFillerResult RetrievePanelItems(PanelItemBase parent);
        bool FillerExists(PanelItemBase panelItem);
    }
}
