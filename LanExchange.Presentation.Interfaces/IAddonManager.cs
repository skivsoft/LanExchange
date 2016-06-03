using System.Collections.Generic;
using LanExchange.Presentation.Interfaces.Addons;

namespace LanExchange.Presentation.Interfaces
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