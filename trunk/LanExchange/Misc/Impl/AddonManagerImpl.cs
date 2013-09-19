using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.Intf;
using LanExchange.Intf.Addon;
using LanExchange.Properties;
using LanExchange.SDK;
using LanExchange.Utils;

namespace LanExchange.Misc.Impl
{
    [Localizable(false)]
    public class AddonManagerImpl : IAddonManager
    {
        public AddonManagerImpl()
        {
            Programs = new Dictionary<string, AddonProgram>();
            PanelItems = new Dictionary<string, AddonItemTypeRef>();
        }

        public IDictionary<string, AddonProgram> Programs { get; private set; }
        public IDictionary<string, AddonItemTypeRef> PanelItems { get; private set; }

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
            RegisterImages();
        }

        private void RegisterImages()
        {
            // register programs images
            foreach (var pair in Programs)
                if (pair.Value.ProgramImage != null)
                    App.Images.RegisterImage(string.Format(Resources.ProgramImageFormat, pair.Key), pair.Value.ProgramImage, pair.Value.ProgramImage);
        }

        private void LoadAddon(string fileName)
        {
            // load addon from xml
            var addon = (Addon)SerializeUtils.DeserializeObjectFromXMLFile(fileName, typeof(Addon));
            // process programs
            foreach (var item in addon.Programs)
                if (!Programs.ContainsKey(item.Id))
                {
                    item.PrepareFileNameAndIcon();
                    Programs.Add(item.Id, item);
                }
            // process protocols
            foreach (var item in addon.PanelItemTypes)
                foreach(var menuItem in item.ContextMenuStrip)
                    if (ProtocolHelper.IsProtocol(menuItem.ProgramRef.Id))
                    {
                        var itemProgram = AddonProgram.CreateFromProtocol(menuItem.ProgramRef.Id);
                        if (itemProgram != null)
                            Programs.Add(itemProgram.Id, itemProgram);
                    }
            // process menu items
            foreach (var item in addon.PanelItemTypes)
            {
                AddonItemTypeRef found;
                if (PanelItems.ContainsKey(item.Id))
                {
                    found = PanelItems[item.Id];
                    PanelItems.Remove(item.Id);
                }
                else
                    found = new AddonItemTypeRef();
                if (item.CountVisible == 0) continue;

                // add separator to split item groups
                if (found.ContextMenuStrip.Count > 0)
                    found.ContextMenuStrip.Add(new AddonMenuItem());
                foreach (var menuItem in item.ContextMenuStrip)
                    if (menuItem.Visible)
                    {
                        if (menuItem.IsSeparator)
                            found.ContextMenuStrip.Add(menuItem);
                        else
                        {
                            if (Programs.ContainsKey(menuItem.ProgramRef.Id))
                                menuItem.ProgramValue = Programs[menuItem.ProgramRef.Id];
                            if (!found.ContextMenuStrip.Contains(menuItem) && 
                                menuItem.ProgramRef != null && 
                                menuItem.ProgramValue != null)
                                found.ContextMenuStrip.Add(menuItem);
                        }
                    }
                PanelItems.Add(item.Id, found);
            }
        }

        private void InternalBuildMenu(ToolStripItemCollection Items, string Id)
        {
            Items.Clear();
            ToolStripMenuItem defaultItem = null;
            foreach (var item in PanelItems[Id].ContextMenuStrip)
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
            if (!PanelItems.ContainsKey(Id))
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
            if (!PanelItems.ContainsKey(Id))
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
            var programFileName = item.ProgramValue.ExpandedFileName;
            var programArgs = AddonProgram.ExpandCmdLine(item.ProgramArgs);
            programArgs = MacroHelper.ExpandPublicProperties(programArgs, panelItem);
            try
            {
                if (ProtocolHelper.IsProtocol(item.ProgramRef.Id))
                    Process.Start(item.ProgramRef.Id + programArgs);
                else
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
            if (!PanelItems.ContainsKey(typeId))
                return;
            var item = PanelItems[typeId];
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
            if (!PanelItems.ContainsKey(typeId))
                return;
            var item = PanelItems[typeId];
            AddonMenuItem defaultItem = null;
            foreach (var menuItem in item.ContextMenuStrip)
                if (menuItem.Default)
                    defaultItem = menuItem;
            if (defaultItem != null)
                InternalRunCmdLine(pv, panelItem, defaultItem);
        }
    }
}