using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange.Misc.Impl
{
    public class PanelFillerManagerImpl : IPanelFillerManager
    {
        private readonly IList<IPanelFiller> m_Fillers;

        public PanelFillerManagerImpl()
        {
            m_Fillers = new List<IPanelFiller>();
        }

        public void RegisterPanelFiller(IPanelFiller filler)
        {
            m_Fillers.Add(filler);
        }

        public PanelFillerResult RetrievePanelItems(PanelItemBase parent)
        {
            var result = new PanelFillerResult();
            if (parent != null)
            {
                foreach (var filler in m_Fillers)
                {
                    if (filler.IsParentAccepted(parent))
                    {
                        if (result.ItemsType == null)
                            result.ItemsType = filler.GetFillType();
                        filler.Fill(parent, result.Items);
                    }
                }
            }
            return result;
        }

        public bool FillerExists(PanelItemBase parent)
        {
            foreach (var strategy in m_Fillers)
                if (strategy.IsParentAccepted(parent))
                    return true;
            return false;
        }
    }
}
