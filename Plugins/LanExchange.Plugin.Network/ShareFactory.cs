using System;
using LanExchange.Plugin.Network.Properties;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Network
{
    public sealed class ShareFactory : IPanelItemFactory
    {
        public PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new SharePanelItem(parent, name);
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
            columnManager.RegisterColumn<SharePanelItem>(new PanelColumnHeader(Resources.ResourceName, 200));
            columnManager.RegisterColumn<SharePanelItem>(new PanelColumnHeader(Resources.Description));
        }
    }
}
