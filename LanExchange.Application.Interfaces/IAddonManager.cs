using System.Collections.Generic;
using LanExchange.Application.Interfaces.Addons;

namespace LanExchange.Application.Interfaces
{
    public interface IAddonManager
    {
        IDictionary<string, AddonProgram> Programs { get; }
        IDictionary<string, AddOnItemTypeRef> PanelItems { get; }

        void LoadAddons();
        bool BuildMenuForPanelItemType(object popTop, string id);
        void RunDefaultCmdLine();
        void ProcessKeyDown(object args);
        void SetupMenuForPanelItem(object popTop, PanelItemBase currentItem);
    }
}