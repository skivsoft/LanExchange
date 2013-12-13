using System.Collections.Generic;

namespace LanExchange.SDK
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