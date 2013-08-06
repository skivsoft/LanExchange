using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Presenter;
using LanExchange.SDK;
using System.Collections.ObjectModel;

namespace LanExchange
{
    public class PanelFillerStrategyManager : IPanelFillerStrategyManager
    {
        private readonly IList<IPanelFillerStrategy> m_Strategies;

        public PanelFillerStrategyManager()
        {
            m_Strategies = new List<IPanelFillerStrategy>();
        }

        public void RegisterPanelFillerStrategy(IPanelFillerStrategy strategy)
        {
            m_Strategies.Add(strategy);
        }

        public ICollection<PanelItemBase> RetrievePanelItems(PanelItemBase parent)
        {
            var result = new Collection<PanelItemBase>();
            foreach(var strategy in m_Strategies)
            {
                if (strategy.IsParentAccepted(parent))
                    strategy.Algorithm(parent, result);
            }
            return result;
        }

        public bool HasStrategyForParent(PanelItemBase parent)
        {
            foreach (var strategy in m_Strategies)
                if (strategy.IsParentAccepted(parent))
                    return true;
            return false;
        }
    }
}
