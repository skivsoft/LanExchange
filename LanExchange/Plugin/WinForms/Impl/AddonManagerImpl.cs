using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using LanExchange.Base;
using LanExchange.Helpers;
using LanExchange.Interfaces;
using LanExchange.Plugin.WinForms.Utils;
using LanExchange.SDK;

namespace LanExchange.Plugin.WinForms.Impl
{
    [Localizable(false)]
    public class AddonManagerImpl : IAddonManager
    {
        private readonly IDictionary<string, AddonProgram> m_Programs;
        private readonly IDictionary<string, AddOnItemTypeRef> m_PanelItems;

        private bool m_Loaded;

        public AddonManagerImpl()
        {
            m_Programs = new Dictionary<string, AddonProgram>();
            m_PanelItems = new Dictionary<string, AddOnItemTypeRef>();
        }

        public IDictionary<string, AddonProgram> Programs 
        { 
            get
            {
                LoadAddons();
                return m_Programs;
            }
        }
        
        public IDictionary<string, AddOnItemTypeRef> PanelItems
        {
            get
            {
                LoadAddons();
                return m_PanelItems;
            }
        }

        public void LoadAddons()
        {
            if (m_Loaded) return;
            foreach (var fileName in App.FolderManager.GetAddonsFiles())
                try
                {
                    LoadAddon(fileName);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            // register programs images
            foreach (var pair in m_Programs)
                if (pair.Value.ProgramImage != null)
                {
                    var imageName = string.Format(CultureInfo.InvariantCulture, PanelImageNames.ADDON_FMT, pair.Key);
                    App.Images.RegisterImage(imageName, pair.Value.ProgramImage, pair.Value.ProgramImage);
                }
            m_Loaded = true;
        }

        private void LoadAddon(string fileName)
        {
            // load addon from xml
            var addon = (AddOn)SerializeUtils.DeserializeObjectFromXmlFile(fileName, typeof(AddOn));
            // process programs
            foreach (var item in addon.Programs)
                if (!m_Programs.ContainsKey(item.Id))
                {
                    item.PrepareFileNameAndIcon();
                    m_Programs.Add(item.Id, item);
                }
            // process protocols
            foreach (var item in addon.ItemTypes)
                foreach(var menuItem in item.ContextMenu)
                    if (ProtocolHelper.IsProtocol(menuItem.ProgramRef.Id))
                    {
                        var itemProgram = AddonProgram.CreateFromProtocol(menuItem.ProgramRef.Id);
                        if (itemProgram != null)
                            m_Programs.Add(itemProgram.Id, itemProgram);
                    }
            // process menu items
            foreach (var item in addon.ItemTypes)
            {
                AddOnItemTypeRef found;
                if (m_PanelItems.ContainsKey(item.Id))
                {
                    found = m_PanelItems[item.Id];
                    m_PanelItems.Remove(item.Id);
                }
                else
                    found = new AddOnItemTypeRef();
                if (item.CountVisible == 0) continue;

                // add separator to split menuItem groups
                if (found.ContextMenu.Count > 0)
                    found.ContextMenu.Add(new AddonMenuItem());
                foreach (var menuItem in item.ContextMenu)
                    if (menuItem.Visible)
                    {
                        if (menuItem.IsSeparator)
                            found.ContextMenu.Add(menuItem);
                        else
                        {
                            if (m_Programs.ContainsKey(menuItem.ProgramRef.Id))
                                menuItem.ProgramValue = m_Programs[menuItem.ProgramRef.Id];
                            if (!found.ContextMenu.Contains(menuItem) && 
                                menuItem.ProgramRef != null && 
                                menuItem.ProgramValue != null)
                                found.ContextMenu.Add(menuItem);
                        }
                    }
                m_PanelItems.Add(item.Id, found);
            }
        }

        private void InternalBuildMenu(ToolStripItemCollection items, string id)
        {
            items.Clear();
            ToolStripMenuItem defaultItem = null;
            foreach (var item in PanelItems[id].ContextMenu)
            {
                if (item.IsSeparator)
                    items.Add(new ToolStripSeparator());
                else
                {
                    var menuItem = new ToolStripMenuItem();
                    menuItem.Tag = item;
                    menuItem.Text = App.TR.Translate(item.Text);
                    menuItem.ShortcutKeyDisplayString = item.ShortcutKeys;
                    menuItem.Click += MenuItemOnClick;
                    if (item.ProgramValue != null)
                        menuItem.Image = item.ProgramValue.ProgramImage;
                    menuItem.Enabled = item.Enabled;
                    // lookup last default menuItem
                    if (item.Default)
                        defaultItem = menuItem;
                    items.Add(menuItem);
                }
            }
            if (defaultItem != null)
                defaultItem.Font = new Font(defaultItem.Font, FontStyle.Bold);
        }

        public bool BuildMenuForPanelItemType(object popTop, string id)
        {
            if (!PanelItems.ContainsKey(id))
                return false;

            var subMenu = SubMenuAdapter.CreateFrom(popTop);
            if (subMenu == null)
                return false;

            var tag = subMenu.Tag;
            if (tag == null || !tag.Equals(id))
            {
                InternalBuildMenu(subMenu.Items, id);
                subMenu.Tag = id;
            }
            return subMenu.Items.Count > 0;
        }

        public void SetupMenuForPanelItem(object popTop, PanelItemBase panelItem)
        {
            var subMenu = SubMenuAdapter.CreateFrom(popTop);
            if (subMenu == null) return;

            foreach (var menuItem in subMenu.Items)
            {
                var menuItem1 = menuItem as ToolStripMenuItem;
                if (menuItem1 == null) continue;

                var addonMenuItem = menuItem1.Tag as AddonMenuItem;
                if (addonMenuItem == null) continue;

                menuItem1.ToolTipText = string.Join(" ", AddonCommandStarter.BuildCmdLine(panelItem, addonMenuItem));
            }
        }

        /// <summary>
        /// Executes external program associated with menu menuItem.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private static void MenuItemOnClick(object sender, EventArgs eventArgs)
        {
            var menuItem = (sender as ToolStripMenuItem);
            if (menuItem == null) return;
            var item = (AddonMenuItem) menuItem.Tag;
            if (item == null || item.ProgramValue == null || !item.ProgramValue.Exists) return;
            var pv = App.MainPages.View.ActivePanelView;
            if (pv == null) return;
            new AddonCommandStarter(App.MainView.Info.CurrentItem, item).Start();
        }

        public void ProcessKeyDown(object args)
        {
            var pv = App.MainPages.View.ActivePanelView;
            var e = args as KeyEventArgs;
            if (pv == null || e == null) return;
            var panelItem = pv.Presenter.GetFocusedPanelItem(false, true);
            if (panelItem == null) return;
            var typeId = panelItem.GetType().Name;
            if (!PanelItems.ContainsKey(typeId))
                return;
            var item = PanelItems[typeId];
            var shortcut = KeyboardUtils.KeyEventToString(e);
            foreach (var menuItem in item.ContextMenu)
                if (menuItem.ShortcutPresent && menuItem.ShortcutKeys.Equals(shortcut) && menuItem.Enabled)
                {
                    new AddonCommandStarter(panelItem, menuItem).Start();
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
            if (!PanelItems.ContainsKey(typeId))
                return;
            var item = PanelItems[typeId];
            AddonMenuItem defaultItem = null;
            foreach (var menuItem in item.ContextMenu)
                if (menuItem.Default && menuItem.Enabled)
                    defaultItem = menuItem;
            if (defaultItem != null)
                new AddonCommandStarter(panelItem, defaultItem).Start();
        }
    }
}