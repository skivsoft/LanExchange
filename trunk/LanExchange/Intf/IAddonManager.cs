using System.Collections.Generic;
using System.Windows.Forms;
using LanExchange.Intf.Addon;

namespace LanExchange.Intf
{
    public interface IAddonManager
    {
        IDictionary<string, AddonProgram> Programs { get; }
        IDictionary<string, AddonItemTypeRef> PanelItems { get; }

        bool BuildMenuForPanelItemType(ContextMenuStrip popTop, string Id);
        bool BuildMenuForPanelItemType(ToolStripMenuItem popTop, string Id);
        void RunDefaultCmdLine();
        void ProcessKeyDown(KeyEventArgs e);
    }
}