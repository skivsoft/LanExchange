using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Implementation
{
    public sealed class PanelFillerManager : IPanelFillerManager
    {
        private readonly IDictionary<Type, IPanelFiller> fillers;

        public PanelFillerManager()
        {
            fillers = new Dictionary<Type, IPanelFiller>();
        }

        public void RegisterFiller<TPanelItem>(IPanelFiller filler) where TPanelItem : PanelItemBase
        {
            fillers.Add(typeof(TPanelItem), filler);
        }

        public Type GetFillType(PanelItemBase parent)
        {
            foreach (var pair in fillers)
                if (pair.Value.IsParentAccepted(parent))
                    return pair.Key;
            return null;
        }

        public PanelFillerResult RetrievePanelItems(PanelItemBase parent)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            var result = new PanelFillerResult();
            foreach (var pair in fillers)
                if (pair.Value.IsParentAccepted(parent))
                {
                    if (result.ItemsType == null)
                        result.ItemsType = pair.Key;
                    try
                    {
                        pair.Value.AsyncFill(parent, result.Items);
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                    }
                }

            return result;
        }

        public bool FillerExists(PanelItemBase parent)
        {
            return fillers.Any(pair => pair.Value.IsParentAccepted(parent));
        }
    }
}