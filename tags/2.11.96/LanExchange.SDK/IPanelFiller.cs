using System;
using System.Collections.Generic;

namespace LanExchange.SDK
{
    /// <summary>
    /// Base class for item enumeration strategy displayed in a panel.
    /// </summary>
    public interface IPanelFiller
    {
        bool IsParentAccepted(PanelItemBase parent);
        Type GetFillType();
        void Fill(PanelItemBase parent, ICollection<PanelItemBase> result);
    }
}
