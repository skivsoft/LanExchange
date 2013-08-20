using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using LanExchange.Model.Addon;
using LanExchange.UI;
using LanExchange.Utils;

namespace LanExchange.Model
{
    [Localizable(false)]
    public class AddonManager
    {
        private static AddonManager s_Instance;
        private readonly Dictionary<string, Addon.AddonProgram> m_Programs;
        private readonly Dictionary<string, PanelItemBaseRef> m_PanelItems;

        private AddonManager()
        {
            m_Programs = new Dictionary<string, Addon.AddonProgram>();
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
            try
            {
                LoadAddon(fileName);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            ResolveReferences();
        }

        public void LoadAddon(string fileName)
        {
            var addon = (LanExchangeAddon)SerializeUtils.DeserializeObjectFromXMLFile(fileName, typeof(LanExchangeAddon));
            foreach (var item in addon.Programs)
                if (!m_Programs.ContainsKey(item.Id))
                    m_Programs.Add(item.Id, item);
            foreach (var item in addon.PanelItems)
                if (m_PanelItems.ContainsKey(item.Id))
                {
                    var found = m_PanelItems[item.Id];
                    // add separator to split item groups
                    if (found.ContextMenuStrip.Count > 0)
                        found.ContextMenuStrip.Add(new Addon.ToolStripMenuItem());
                    foreach (var menuItem in item.ContextMenuStrip)
                        found.ContextMenuStrip.Add(menuItem);
                }
                else
                    m_PanelItems.Add(item.Id, item);
        }

        private void ResolveReferences()
        {
            foreach (var program in m_Programs)
            {
                program.Value.PrepareFileNameAndIcon();
            }
            foreach(var item in m_PanelItems)
                foreach (var menuItem in item.Value.ContextMenuStrip)
                    if (!menuItem.IsSeparator)
                        if (m_Programs.ContainsKey(menuItem.ProgramRef.Id))
                            menuItem.ProgramValue = m_Programs[menuItem.ProgramRef.Id];
                        else
                            menuItem.ProgramValue = null;
        }

        public bool BuildMenuForPanelItemType(ContextMenuStrip popTop, string Id)
        {
            if (!m_PanelItems.ContainsKey(Id))
                return false;
            if (popTop.Tag == null || !popTop.Tag.Equals(Id))
            {
                popTop.Items.Clear();
                foreach (var item in m_PanelItems[Id].ContextMenuStrip)
                {
                    if (item.IsSeparator)
                        popTop.Items.Add(new ToolStripSeparator());
                    else
                    {
                        var menuItem = new System.Windows.Forms.ToolStripMenuItem();
                        menuItem.Tag = item;
                        menuItem.Text = item.Text;
                        menuItem.ShortcutKeyDisplayString = item.ShortcutKeys;
                        menuItem.Click += MenuItemOnClick;
                        if (item.ProgramValue != null)
                        {
                            if (!item.ProgramValue.Exists)
                                menuItem.Enabled = false;
                            menuItem.Image = item.ProgramValue.ProgramImage;
                        }
                        else
                            menuItem.Enabled = false;
                        popTop.Items.Add(menuItem);
                    }
                }
                popTop.Tag = Id;
            }
            return popTop.Items.Count > 0;
        }

        /// <summary>
        /// Executes external program associated with menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void MenuItemOnClick(object sender, EventArgs eventArgs)
        {
            var menuItem = (sender as System.Windows.Forms.ToolStripMenuItem);
            if (menuItem == null) return;
            var item = (Addon.ToolStripMenuItem) menuItem.Tag;
            if (item == null || item.ProgramValue == null || !item.ProgramValue.Exists) return;
            var pv = MainForm.Instance.Pages.ActivePanelView;
            if (pv == null) return;
            var panelItem = pv.Presenter.GetFocusedPanelItem(false, true);
            if (panelItem == null) return;
            var fmtValue = panelItem.FullItemName;
            if (fmtValue.StartsWith(@"\\"))
                fmtValue = fmtValue.Remove(0, 2);
            var programFileName = item.ProgramValue.ExpandedFileName;
            var programArgs = string.Format(Addon.AddonProgram.ExpandCmdLine(item.ProgramArgs), fmtValue);
            try
            {
                Process.Start(programFileName, programArgs);
            }
            catch
            {
                pv.ShowRunCmdError(string.Format("{0} {1}", programFileName, programArgs));
            }
        }
    }
}
