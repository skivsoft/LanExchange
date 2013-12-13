using System;

namespace LanExchange.SDK
{
    public delegate IComparable LazyCallback(PanelItemBase item);

    public delegate void SetTabImageDelegate(IPanelModel model, string imageName);

    public delegate void SetFillerResultDelegate(IPanelModel model, PanelFillerResult fillerResult);
}
