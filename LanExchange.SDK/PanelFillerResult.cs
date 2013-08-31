using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LanExchange.SDK
{
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