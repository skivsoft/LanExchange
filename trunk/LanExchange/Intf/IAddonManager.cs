using System.Collections.Generic;
using System.Windows.Forms;
using LanExchange.Misc.Addon;

namespace LanExchange.Intf
{
    public interface IAddonManager
    {
        IDictionary<string, AddonProgram> Programs { get; }
        IDictionary<string, AddonItemTypeRef> PanelItems { get; }

        void LoadAddons();
        bool BuildMenuForPanelItemType(ContextMenuStrip popTop, string Id);
        bool BuildMenuForPanelItemType(ToolStripMenuItem popTop, string Id);
        void RunDefaultCmdLine();
        void ProcessKeyDown(KeyEventArgs e);
    }
}