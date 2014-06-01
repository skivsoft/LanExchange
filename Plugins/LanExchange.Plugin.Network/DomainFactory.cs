using System;
using System.Security.Permissions;
using LanExchange.Plugin.Network.NetApi;
using LanExchange.Plugin.Network.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    public sealed class DomainFactory : IPanelItemFactory
    {
        public PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new DomainPanelItem(parent, name);
        }

        /// <summary>
        /// Starts with curent users's workgroup/domain as root.
        /// </summary>
        /// <returns></returns>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public PanelItemBase CreateDefaultRoot()
        {
            var domain = WorkstationInfo.FromComputer(null).LanGroup;
            return new DomainPanelItem(new DomainRoot(), domain);
        }

        public Func<PanelItemBase, bool> GetAvailabilityChecker()
        {
            return null;
        }

        public void RegisterColumns(IPanelColumnManager columnManager)
        {
            columnManager.RegisterColumn<DomainPanelItem>(new PanelColumnHeader(Resources.DomainName, 200));
        }
    }
}
