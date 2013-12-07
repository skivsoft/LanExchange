using System.Collections.Generic;
using LanExchange.SDK.Addon;

namespace LanExchange.SDK.UI
{
    public interface IAddonManager
    {
        IDictionary<string, AddonProgram> Programs { get; }
        IDictionary<string, AddonItemTypeRef> PanelItems { get; }

        void LoadAddons();
        bool BuildMenuForPanelItemType(object popTop, string Id);
        void RunDefaultCmdLine();
        void ProcessKeyDown(object args);
    }
}