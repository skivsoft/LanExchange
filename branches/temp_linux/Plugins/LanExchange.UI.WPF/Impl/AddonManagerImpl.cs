using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanExchange.SDK;
using LanExchange.SDK.UI;


namespace LanExchange.UI.WPF.Impl
{
    class AddonManagerImpl : IAddonManager
    {

        public IDictionary<string, SDK.Addon.AddonProgram> Programs
        {
            get { return null; }
        }

        public IDictionary<string, SDK.Addon.AddonItemTypeRef> PanelItems
        {
            get { return null; }
        }

        public void LoadAddons()
        {
         
        }

        public bool BuildMenuForPanelItemType(object popTop, string Id)
        {
            return false;
        }

        public void RunDefaultCmdLine()
        {
         
        }

        public void ProcessKeyDown(object args)
        {
         
        }
    }
}
