using System;
using System.Collections.Generic;

namespace LanExchange.SDK
{
    public abstract class PanelFillerBase
    {
        public abstract bool IsParentAccepted(PanelItemBase parent);
        public abstract void Fill(PanelItemBase parent, ICollection<PanelItemBase> result);
    }
}
