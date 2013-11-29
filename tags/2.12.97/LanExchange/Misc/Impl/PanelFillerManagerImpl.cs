using System;
using System.Collections.Generic;
using System.Diagnostics;
using LanExchange.SDK;

namespace LanExchange.Misc.Impl
{
    public class PanelFillerManagerImpl : IPanelFillerManager
    {
        private readonly IDictionary<Type, PanelFillerBase> m_Fillers;

        public PanelFillerManagerImpl()
        {
            m_Fillers = new Dictionary<Type, PanelFillerBase>();
        }

        public void RegisterFiller<TPanelItem>(PanelFillerBase filler) where TPanelItem : PanelItemBase
        {
            m_Fillers.Add(typeof(TPanelItem), filler);
        }

        public Type GetFillType(PanelItemBase parent)
        {
            foreach (var pair in m_Fillers)
                if (pair.Value.IsParentAccepted(parent))
                    return pair.Key;
            return null;
        }

        public PanelFillerResult RetrievePanelItems(PanelItemBase parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");
            var result = new PanelFillerResult();
            foreach (var pair in m_Fillers)
                if (pair.Value.IsParentAccepted(parent))
                {
                    if (result.ItemsType == null)
                        result.ItemsType = pair.Key;
                    try
                    {
                        pair.Value.Fill(parent, result.Items);
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
            foreach (var pair in m_Fillers)
                if (pair.Value.IsParentAccepted(parent))
                    return true;
            return false;
        }
    }
}
