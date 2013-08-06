using LanExchange.SDK;

namespace LanExchange.Plugin.Network.Panel
{
    public class DomainPanelItemFactory : PanelItemBaseFactory
    {
        public override PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new DomainPanelItem(parent, name);
        }

        /// <summary>
        /// Starts with curent users's workgroup/domain as root.
        /// </summary>
        /// <returns></returns>
        public override PanelItemBase CreateDefaultRoot()
        {
            var domain = NetApi32Utils.Instance.GetMachineNetBiosDomain(null);
            return new DomainPanelItem(Network.ROOT_OF_DOMAINS, domain);
        }
    }
}
