using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.Intf;
using LanExchange.SDK;
using LanExchange.Utils;

namespace LanExchange.Misc.Addon
{
    [Localizable(false)]
    public class AddonManager
    {
        private static AddonManager s_Instance;
        private readonly Dictionary<string, AddonProgram> m_Programs;
        private readonly Dictionary<string, AddonItemTypeRef> m_PanelItems;

        private AddonManager()
        {
            m_Programs = new Dictionary<string, AddonProgram>();
            m_PanelItems = new Dictionary<string, AddonItemTypeRef>();
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
            foreach(var fileName in App.FolderManager.GetAddonsFiles())
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
            var addon = (Addon)SerializeUtils.DeserializeObjectFromXMLFile(fileName, typeof(Addon));
            foreach (var item in addon.Programs)
                if (!m_Programs.ContainsKey(item.Id))
                    m_Programs.Add(item.Id, item);
            foreach (var item in addon.PanelItemTypes)
                if (m_PanelItems.ContainsKey(item.Id))
                {
                    var found = m_PanelItems[item.Id];
                    if (item.CountVisible > 0)
                    {
                        // add separator to split item groups
                        if (found.ContextMenuStrip.Count > 0)
                            found.ContextMenuStrip.Add(new AddonMenuItem());
                        foreach (var menuItem in item.ContextMenuStrip)
                            if (menuItem.Visible)
                                found.ContextMenuStrip.Add(menuItem);
                    }
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

        private void InternalBuildMenu(ToolStripItemCollection Items, string Id)
        {
            Items.Clear();
            ToolStripMenuItem defaultItem = null;
            foreach (var item in m_PanelItems[Id].ContextMenuStrip)
            {
                if (item.IsSeparator)
                    Items.Add(new ToolStripSeparator());
                else
                {
                    var menuItem = new ToolStripMenuItem();
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
                    // lookup last default item
                    if (item.Default)
                        defaultItem = menuItem;
                    Items.Add(menuItem);
                }
            }
            if (defaultItem != null)
                defaultItem.Font = new Font(defaultItem.Font, FontStyle.Bold);
        }

        public bool BuildMenuForPanelItemType(ContextMenuStrip popTop, string Id)
        {
            if (!m_PanelItems.ContainsKey(Id))
                return false;
            if (popTop.Tag == null || !popTop.Tag.Equals(Id))
            {
                InternalBuildMenu(popTop.Items, Id);
                popTop.Tag = Id;
            }
            return popTop.Items.Count > 0;
        }

        public bool BuildMenuForPanelItemType(ToolStripMenuItem popTop, string Id)
        {
            if (!m_PanelItems.ContainsKey(Id))
                return false;
            if (popTop.Tag == null || !popTop.Tag.Equals(Id))
            {
                InternalBuildMenu(popTop.DropDownItems, Id);
                popTop.Tag = Id;
            }
            return popTop.DropDownItems.Count > 0;
        }

        private void InternalRunCmdLine(IPanelView pv, PanelItemBase panelItem, AddonMenuItem item)
        {
            var fmtValue = panelItem.FullItemName;
            if (fmtValue.StartsWith(@"\\"))
                fmtValue = fmtValue.Remove(0, 2);
            var programFileName = item.ProgramValue.ExpandedFileName;
            var programArgs = string.Format(AddonProgram.ExpandCmdLine(item.ProgramArgs), fmtValue);
            try
            {
                Process.Start(programFileName, programArgs);
            }
            catch(Exception ex)
            {
                pv.ShowRunCmdError(string.Format("{0} {1}\n{2}", programFileName, programArgs, ex.Message));
            }
        }

        /// <summary>
        /// Executes external program associated with menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void MenuItemOnClick(object sender, EventArgs eventArgs)
        {
            var menuItem = (sender as ToolStripMenuItem);
            if (menuItem == null) return;
            var item = (AddonMenuItem) menuItem.Tag;
            if (item == null || item.ProgramValue == null || !item.ProgramValue.Exists) return;
            var pv = App.MainPages.View.ActivePanelView;
            if (pv == null) return;
            var panelItem = pv.Presenter.GetFocusedPanelItem(false, true);
            if (panelItem == null) return;
            InternalRunCmdLine(pv, panelItem, item);
        }

        public void ProcessKeyDown(KeyEventArgs e)
        {
            var pv = App.MainPages.View.ActivePanelView;
            if (pv == null) return;
            var panelItem = pv.Presenter.GetFocusedPanelItem(false, true);
            if (panelItem == null) return;
            var typeId = panelItem.GetType().Name;
            if (!m_PanelItems.ContainsKey(typeId))
                return;
            var item = m_PanelItems[typeId];
            var shortcut = KeyboardUtils.KeyEventToString(e);
            foreach (var menuItem in item.ContextMenuStrip)
                if (menuItem.ShortcutPresent && menuItem.ShortcutKeys.Equals(shortcut))
                {
                    InternalRunCmdLine(pv, panelItem, menuItem);
                    e.Handled = true;
                    break;
                }
        }

        /// <summary>
        /// Run addon command marked with Default flag for current panelItem.
        /// </summary>
        public void RunDefaultCmdLine()
        {
            var pv = App.MainPages.View.ActivePanelView;
            if (pv == null) return;
            var panelItem = pv.Presenter.GetFocusedPanelItem(false, true);
            if (panelItem == null) return;
            var typeId = panelItem.GetType().Name;
            if (!m_PanelItems.ContainsKey(typeId))
                return;
            var item = m_PanelItems[typeId];
            AddonMenuItem defaultItem = null;
            foreach (var menuItem in item.ContextMenuStrip)
                if (menuItem.Default)
                    defaultItem = menuItem;
            if (defaultItem != null)
                InternalRunCmdLine(pv, panelItem, defaultItem);
        }
    }
}
