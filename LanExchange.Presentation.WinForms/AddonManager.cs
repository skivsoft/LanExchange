using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Addons;
using LanExchange.Presentation.Interfaces.Persistence;
using LanExchange.Presentation.WinForms.Helpers;

namespace LanExchange.Presentation.WinForms
{
    [Localizable(false)]
    internal sealed class AddonManager : IAddonManager
    {
        private readonly IFolderManager folderManager;
        private readonly IAddonProgramFactory programFactory;
        private readonly IImageManager imageManager;
        private readonly IPagesPresenter pagesPresenter;
        private readonly ITranslationService translationService;
        private readonly IPanelItemFactoryManager factoryManager;
        private readonly IWindowFactory windowFactory;
        private readonly ILogService logService;
        private readonly IAddonPersistenceService persistenceService;

        public AddonManager(
            IFolderManager folderManager, 
            IAddonProgramFactory programFactory,
            IImageManager imageManager,
            IPagesPresenter pagesPresenter,
            ITranslationService translationService,
            IPanelItemFactoryManager factoryManager,
            IWindowFactory windowFactory,
            ILogService logService,
            IAddonPersistenceService persistenceService)
        {
            if (folderManager != null) throw new ArgumentNullException(nameof(folderManager));
            if (programFactory != null) throw new ArgumentNullException(nameof(programFactory));
            if (imageManager != null) throw new ArgumentNullException(nameof(imageManager));
            if (pagesPresenter != null) throw new ArgumentNullException(nameof(pagesPresenter));
            if (translationService != null) throw new ArgumentNullException(nameof(translationService));
            if (factoryManager != null) throw new ArgumentNullException(nameof(factoryManager));
            if (windowFactory != null) throw new ArgumentNullException(nameof(windowFactory));
            if (logService != null) throw new ArgumentNullException(nameof(logService));
            if (persistenceService != null) throw new ArgumentNullException(nameof(persistenceService));

            this.folderManager = folderManager;
            this.programFactory = programFactory;
            this.imageManager = imageManager;
            this.pagesPresenter = pagesPresenter;
            this.translationService = translationService;
            this.factoryManager = factoryManager;
            this.windowFactory = windowFactory;
            this.logService = logService;
            this.persistenceService = persistenceService;

            Programs = new Dictionary<string, AddonProgram>();
            PanelItems = new Dictionary<string, AddOnItemTypeRef>();
        }

        public IDictionary<string, AddonProgram> Programs { get; }

        public IDictionary<string, AddOnItemTypeRef> PanelItems { get; }

        public void LoadAddons()
        {
            InternalLoadAddons();
            RegisterProgramsImages();
        }

        private void InternalLoadAddons()
        {
            foreach (var fileName in folderManager.GetAddonsFiles())
            {
                logService.Log("loading addon: {0}", fileName);
                try
                {
                    LoadAddon(fileName);
                }
                catch (Exception exception)
                {
                    logService.Log(exception);
                }
            }
        }

        private void RegisterProgramsImages()
        {
            foreach (var pair in Programs.Where(pair => pair.Value.ProgramImage != null))
            {
                var imageName = string.Format(CultureInfo.InvariantCulture, PanelImageNames.AddonFmt, pair.Key);
                imageManager.RegisterImage(imageName, pair.Value.ProgramImage, pair.Value.ProgramImage);
            }
        }
        private void LoadAddon(string fileName)
        {
            var addon = persistenceService.Load(fileName);
            ProcessPrograms(addon);
            ProcessProtocols(addon);
            ProcessMenuItems(addon);
        }

        private void ProcessPrograms(AddOn addon)
        {
            foreach (var item in addon.Programs)
                if (!Programs.ContainsKey(item.Id))
                {
                    item.Info = programFactory.CreateAddonProgramInfo(item);
                    Programs.Add(item.Id, item);
                }
        }

        private void ProcessProtocols(AddOn addon)
        {
            foreach (var item in addon.ItemTypes)
                foreach (var menuItem in item.ContextMenu)
                    if (ProtocolHelper.IsProtocol(menuItem.ProgramRef.Id))
                    {
                        var itemProgram = programFactory.CreateFromProtocol(menuItem.ProgramRef.Id);
                        if (itemProgram != null)
                            Programs.Add(itemProgram.Id, itemProgram);
                    }
        }

        private void ProcessMenuItems(AddOn addon)
        {
            foreach (var item in addon.ItemTypes)
            {
                AddOnItemTypeRef found;
                if (PanelItems.ContainsKey(item.Id))
                {
                    found = PanelItems[item.Id];
                    PanelItems.Remove(item.Id);
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
                            if (Programs.ContainsKey(menuItem.ProgramRef.Id))
                                menuItem.ProgramValue = Programs[menuItem.ProgramRef.Id];
                            if (!found.ContextMenu.Contains(menuItem) &&
                                menuItem.ProgramRef != null &&
                                menuItem.ProgramValue != null)
                                found.ContextMenu.Add(menuItem);
                        }
                    }
                PanelItems.Add(item.Id, found);
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
                    menuItem.Text = translationService.Translate(item.Text);
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

                var item = (AddonMenuItem)menuItem1.Tag;
                if (item != null)
                    item.CurrentItem = panelItem;
            }
        }

        /// <summary>
        /// Executes external program associated with menu menuItem.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void MenuItemOnClick(object sender, EventArgs eventArgs)
        {
            var menuItem = (sender as ToolStripMenuItem);
            if (menuItem == null) return;

            var item = (AddonMenuItem)menuItem.Tag;
            if (item == null || !item.Enabled) return;

            new AddonCommandStarter(factoryManager, windowFactory, item, item.CurrentItem).Start();
        }

        public void ProcessKeyDown(object args)
        {
            // TODO hide model
            // var pv = pagesPresenter.ActivePanelView;
            // var e = args as KeyEventArgs;
            // if (pv == null || e == null) return;
            // var panelItem = pv.Presenter.GetFocusedPanelItem(true);
            // if (panelItem == null) return;
            // var typeId = panelItem.GetType().Name;
            // if (!PanelItems.ContainsKey(typeId))
            // return;
            // var item = PanelItems[typeId];
            // var shortcut = KeyboardUtils.KeyEventToString(e);
            // foreach (var menuItem in item.ContextMenu)
            // if (menuItem.ShortcutPresent && menuItem.ShortcutKeys.Equals(shortcut) && menuItem.Enabled)
            // {
            // new AddonCommandStarter(factoryManager, windowFactory, menuItem, panelItem).Start();
            // e.Handled = true;
            // break;
            // }
        }

        /// <summary>
        /// Run addon command marked with Default flag for current panelItem.
        /// </summary>
        public void RunDefaultCmdLine()
        {
            var pv = pagesPresenter.ActivePanelView;
            if (pv == null) return;

            // TODO hide model
            // var panelItem = pv.Presenter.GetFocusedPanelItem(true);
            // if (panelItem == null) return;

            // var typeId = panelItem.GetType().Name;
            // if (!PanelItems.ContainsKey(typeId))
            // return;

            // var item = PanelItems[typeId];
            // AddonMenuItem defaultItem = null;
            // foreach (var menuItem in item.ContextMenu)
            // if (menuItem.Default && menuItem.Enabled)
            // defaultItem = menuItem;

            // if (defaultItem != null)
            // new AddonCommandStarter(factoryManager, windowFactory, defaultItem, panelItem).Start();
        }
    }
}