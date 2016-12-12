using System;

namespace LanExchange.Presentation.Interfaces
{
    public class PanelItemRootFactory<TRoot> : IPanelItemFactory where TRoot : PanelItemRootBase, new()
    {
        public PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new TRoot();
        }

        public PanelItemBase CreateDefaultRoot()
        {
            return null;
        }

        public Func<PanelItemBase, bool> GetAvailabilityChecker()
        {
            return null;
        }

        public void RegisterColumns(IPanelColumnManager columnManager)
        {
        }
    }
}