using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

    public class PanelFillerResult
    {
        public readonly ICollection<PanelItemBase> Items;
        public Type ItemsType;

        public PanelFillerResult()
        {
            Items = new Collection<PanelItemBase>();
        }
    }

}
