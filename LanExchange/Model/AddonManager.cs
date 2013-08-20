using System;
using System.Collections.Generic;
using System.Diagnostics;
using LanExchange.Model.Addon;
using LanExchange.Utils;

namespace LanExchange.Model
{
    public class AddonManager
    {
        private static AddonManager s_Instance;
        private Dictionary<string, Addon.Program> m_Programs;
        private Dictionary<string, Addon.PanelItemBaseRef> m_PanelItems;

        private AddonManager()
        {
            m_Programs = new Dictionary<string, Addon.Program>();
            m_PanelItems = new Dictionary<string, PanelItemBaseRef>();
        }

        public static AddonManager Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new AddonManager();
                return s_Instance;
            }
        }

        public void LoadAddons()
        {
            foreach(var fileName in FolderManager.Instance.GetAddonsFiles())
            {
                try
                {
                    var addon = (LanExchangeAddon) SerializeUtils.DeserializeObjectFromXMLFile(fileName, typeof (LanExchangeAddon));
                    foreach (var item in addon.Programs)
                        if (!m_Programs.ContainsKey(item.Id))
                            m_Programs.Add(item.Id, item);
                    foreach (var item in addon.PanelItems)
                        if (m_PanelItems.ContainsKey(item.Id))
                        {
                            var found = m_PanelItems[item.Id];
                            foreach(var menuItem in item.ContextMenuStrip)
                                found.ContextMenuStrip.Add(menuItem);
                        } else
                            m_PanelItems.Add(item.Id, item);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }
            ResolveReferences();
        }

        private void ResolveReferences()
        {
            
        }
    }
}
