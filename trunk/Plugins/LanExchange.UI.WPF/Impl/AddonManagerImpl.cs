using System;
using System.Collections.Generic;
using LanExchange.SDK;


namespace LanExchange.UI.WPF.Impl
{
    class AddonManagerImpl : IAddonManager
    {

        public IDictionary<string, AddonProgram> Programs
        {
            get { return null; }
        }

        public IDictionary<string, AddonItemTypeRef> PanelItems
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
