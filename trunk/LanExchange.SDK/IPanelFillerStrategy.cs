using System.Collections.Generic;

namespace LanExchange.SDK
{
    /// <summary>
    /// Base class for item enumeration strategy displayed in a panel.
    /// </summary>
    public interface IPanelFillerStrategy
    {
        bool IsParentAccepted(PanelItemBase parent);
        void Algorithm(PanelItemBase parent, ICollection<PanelItemBase> result);
    }
}
