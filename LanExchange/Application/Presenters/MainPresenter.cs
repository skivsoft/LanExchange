using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Globalization;
using LanExchange.Application.Commands;
using LanExchange.Application.Interfaces;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.EventArgs;
using LanExchange.Properties;
using LanExchange.Application.Implementation.Menu;
using LanExchange.Presentation.Interfaces.Menu;
using System.Collections.Generic;

namespace LanExchange.Application.Presenters
{
    internal sealed class MainPresenter : PresenterBase<IMainView>, IMainPresenter
    {
        private readonly ILazyThreadPool threadPool;
        private readonly IPanelColumnManager columnManager;
        private readonly IAppPresenter appPresenter;
        private readonly IPagesPresenter pagesPresenter;
        private readonly ITranslationService translationService;
        private readonly IHotkeyService hotkeyService;
        private readonly IDisposableManager disposableManager;
        private readonly IAboutModel aboutModel;
        private readonly IViewFactory viewFactory;
        private readonly IWaitingService waitingService;
        private readonly IPanelItemFactoryManager factoryManager;
        private readonly IScreenService screenService;
        private readonly ICommandManager commandManager;
        private readonly IAppView appView;
        private readonly IProcessService processService;
        private readonly IImageManager imageManager;
        private readonly IMenuProducer menuProducer;

        public MainPresenter(
            ILazyThreadPool threadPool,
            IPanelColumnManager panelColumns,
            IAppPresenter appPresenter,
            IPagesPresenter pagesPresenter,
            ITranslationService translationService,
            IHotkeyService hotkeyService,
            IDisposableManager disposableManager,
            IAboutModel aboutModel,
            IViewFactory viewFactory,
            IWaitingService waitingService,
            IPanelItemFactoryManager factoryManager,
            IScreenService screenService,
            ICommandManager commandManager,
            IAppView appView,
            IProcessService processService,
            IImageManager imageManager,
            IMenuProducer menuProducer)
        {
            Contract.Requires<ArgumentNullException>(threadPool != null);
            Contract.Requires<ArgumentNullException>(panelColumns != null);
            Contract.Requires<ArgumentNullException>(appPresenter != null);
            Contract.Requires<ArgumentNullException>(pagesPresenter != null);
            Contract.Requires<ArgumentNullException>(translationService != null);
            Contract.Requires<ArgumentNullException>(hotkeyService != null);
            Contract.Requires<ArgumentNullException>(disposableManager != null);
            Contract.Requires<ArgumentNullException>(aboutModel != null);
            Contract.Requires<ArgumentNullException>(viewFactory != null);
            Contract.Requires<ArgumentNullException>(waitingService != null);
            Contract.Requires<ArgumentNullException>(factoryManager != null);
            Contract.Requires<ArgumentNullException>(screenService != null);
            Contract.Requires<ArgumentNullException>(commandManager != null);
            Contract.Requires<ArgumentNullException>(appView != null);
            Contract.Requires<ArgumentNullException>(processService != null);
            Contract.Requires<ArgumentNullException>(imageManager != null);
            Contract.Requires<ArgumentNullException>(menuProducer != null);

            this.threadPool = threadPool;
            this.columnManager = panelColumns;
            this.appPresenter = appPresenter;
            this.pagesPresenter = pagesPresenter;
            this.translationService = translationService;
            this.hotkeyService = hotkeyService;
            this.disposableManager = disposableManager;
            this.aboutModel = aboutModel;
            this.viewFactory = viewFactory;
            this.waitingService = waitingService;
            this.factoryManager = factoryManager;
            this.screenService = screenService;
            this.commandManager = commandManager;
            this.appView = appView;
            this.processService = processService;
            this.imageManager = imageManager;
            this.menuProducer = menuProducer;
        }

        protected override void InitializePresenter()
        {
            // set hotkey for activate: Ctrl+Win+X
            disposableManager.RegisterInstance(hotkeyService);
            if (hotkeyService.RegisterShowWindowKey(View.Handle))
                showWindowKey = hotkeyService.ShowWindowKey;

            SetupMenu();
            // init main form
            SetupStatusPanel();
            SetupPages();
            SetupForm();
            View.RightToLeftValue = translationService.RightToLeft;
            // set lazy events
            threadPool.DataReady += OnDataReady;
        }

        private void SetupMenu()
        {
            menuProducer.MainMenu = BuildMainMenu();
            menuProducer.TrayMenu = BuildTrayMenu();

            View.InitializeMainMenu(menuProducer.MainMenu);
            View.InitializeTrayMenu(menuProducer.TrayMenu);
            View.MenuVisible = false;
        }

        private IMenuElement BuildMainMenu()
        {
            return new MenuGroup(
                new MenuGroup(Resources.mPanel_Text,
                    new MenuElement(Resources.mPanelReRead_Text, "Ctrl+R", NullCommand.Instance),
                    new MenuSeparator(),
                    new MenuElement(Resources.mPanelExit_Text, "Alt+F4", new ExitCommand(appView))
                ),
                new MenuGroup(Resources.mView_Text,
                    new MenuElement(Resources.mViewLarge_Text, NullCommand.Instance),
                    new MenuElement(Resources.mViewSmall_Text, NullCommand.Instance),
                    new MenuElement(Resources.mViewList_Text, NullCommand.Instance),
                    new MenuElement(Resources.mViewDetails_Text, NullCommand.Instance)
                ),
                BuildLanguageMenu(),
                new MenuGroup(Resources.mHelp_Text,
                    new MenuElement(Resources.mHelpWeb_Text, NullCommand.Instance),
                    new MenuElement(Resources.mHelpBugs_Text, NullCommand.Instance),
                    new MenuElement(Resources.mHelpLangs_Text, NullCommand.Instance),
                    new MenuSeparator(),
                    new MenuElement(Resources.mHelpAbout_Text, commandManager.GetCommand(nameof(AboutCommand)))
                )
            );
        }

        private string showWindowKey;

        private IMenuElement BuildTrayMenu()
        {
            return new MenuGroup(
                    new MenuElement(Resources.mTrayClose_Text, showWindowKey, new ToggleVisibleCommand(View), MenuElementKind.Default),
                    new MenuSeparator(),
                    new MenuElement(Resources.mHelpAbout_Text, commandManager.GetCommand(nameof(AboutCommand))),
                    new MenuElement(Resources.mPanelExit_Text, commandManager.GetCommand(nameof(ExitCommand)))
            );
        }

        private IMenuElement BuildLanguageMenu()
        {
            var languagesDict = translationService.GetLanguagesNames();
            if (languagesDict.Count <= 1)
                return NullMenu.Instance;

            var menuItems = new List<IMenuElement>();
            foreach (var pair in languagesDict)
            {
                var menuItem = new MenuElement(pair.Value, NullCommand.Instance);
                //menuItem.RadioCheck = true;
                //menuItem.Tag = pair.Key;
                //menuItem.Click += MenuItemOnClick;
                //mLanguage.MenuItems.Add(menuItem);
                menuItems.Add(menuItem);
            }

            return new MenuGroup(Resources.mLanguage_Text, menuItems.ToArray());
        }

        public void SetupMenuTags()
        {
            //mViewLarge.Tag = PanelViewMode.LargeIcon;
            //mViewSmall.Tag = PanelViewMode.SmallIcon;
            //mViewList.Tag = PanelViewMode.List;
            //mViewDetails.Tag = PanelViewMode.Details;
        }

        private void SetupStatusPanel()
        {
            var statusPanel = viewFactory.CreateStatusPanelView();
            imageManager.SetImagesTo(statusPanel);
            View.AddView(statusPanel, ViewDockStyle.Bottom);
        }

        private void SetupPages()
        {
            //Pages = (PagesView)viewFactory.GetPagesView();
            //Pages.Dock = DockStyle.Fill;
            //Controls.Add(Pages);
            //Pages.BringToFront();

            //// setup images
            //imageManager.SetImagesTo(Pages.Pages);
            //// load saved pages from config
            //Pages.SetupContextMenu();
        }

        [Localizable(false)]
        public void SetupForm()
        {
            //App.MainPages.View.SetupContextMenu();
            pagesPresenter.PanelViewFocusedItemChanged += Pages_PanelViewFocusedItemChanged;
            // set mainform bounds
            var rect = SettingsGetBounds();
            View.SetBounds(rect.Left, rect.Top, rect.Width, rect.Height);
            // set mainform title
            var text = string.Format(CultureInfo.CurrentCulture, "{0} {1}", aboutModel.Title, aboutModel.VersionShort);
            View.Text = text;
            // show tray
            View.TrayText = text;
            View.TrayVisible = true;
        }

        public void OnDataReady(object sender, DataReadyArgs args)
        {
            //View.SafeInvoke(new WaitCallback(MainForm_RefreshItem), args.Item);
        }

        private void MainForm_RefreshItem(object item)
        {
            var pv = viewFactory.GetPagesView().ActivePanelView;
            if (pv != null)
            {
                //TODO hide model
                //var index = pv.Presenter.Objects.IndexOf(item as PanelItemBase);
                //if (index >= 0)
                //    pv.RedrawItem(index);
            }
        }

        /// <summary>
        /// This event fires when focused item of PanelView has been changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pages_PanelViewFocusedItemChanged(object sender, EventArgs e)
        {
            // get focused item from current PanelView
            var pv = sender as IPanelView;
            if (pv == null) return;
            //TODO hide model
            //var panelItem = pv.Presenter.GetFocusedPanelItem(true);
            //// check if parent item more informative than current panel item
            //while (panelItem != null)
            //{
            //    if (panelItem.Parent is PanelItemRootBase) 
            //        break;
            //    if (panelItem.Parent != null && (panelItem.Parent.Parent is PanelItemRootBase))
            //        break;
            //    panelItem = panelItem.Parent;
            //}
        }


        public void GlobalTranslateUI()
        {
            if (View != null)
                waitingService.BeginWait();
            try
            {
                // recreate all columns
                GlobalTranslateColumns();
                // Run TranslateUI() for all opened forms
                appPresenter.TranslateOpenForms();
            }
            finally
            {
                if (View != null)
                    waitingService.EndWait();
            }
        }

        public bool IsHotKey(short id)
        {
            return hotkeyService.IsHotKey(id);
        }

        public void DoPagesReRead()
        {
            //commandManager.ExecuteCommand<PagesReReadCommand>();
        }

        private void GlobalTranslateColumns()
        {
            foreach (var pair in factoryManager.Types)
            {
                columnManager.UnregisterColumns(pair.Key.Name);
                pair.Value.RegisterColumns(columnManager);
            }
        }

        private static int GetDefaultWidth()
        {
            return 500;
        }

        public Rectangle SettingsGetBounds()
        {
            var mainFormWidth = Settings.Default.MainFormWidth;
            var mainFormLeft = Settings.Default.MainFormLeft;
            // correct width and height
            bool boundsIsNotSet = mainFormWidth == 0;
            Rectangle workingArea;
            if (boundsIsNotSet)
                workingArea = screenService.PrimaryScreenWorkingArea;
            else
                workingArea = screenService.GetWorkingArea(new Point(mainFormLeft + mainFormWidth / 2, 0));
            var rect = new Rectangle();
            rect.X = mainFormLeft;
            rect.Y = workingArea.Top;
            rect.Width = Math.Min(Math.Max(GetDefaultWidth(), mainFormWidth), workingArea.Width);
            rect.Height = workingArea.Height;
            // determination side to snap right or left
            int centerX = (rect.Left + rect.Right) >> 1;
            int workingAreaCenterX = (workingArea.Left + workingArea.Right) >> 1;
            if (boundsIsNotSet || centerX >= workingAreaCenterX)
                // snap to right side
                rect.X = workingArea.Right - rect.Width;
            else
                // snap to left side
                rect.X -= rect.Left - workingArea.Left;
            return rect;
        }

        public void DoToggleVisible()
        {
            commandManager.GetCommand(nameof(ToggleVisibleCommand)).Execute();
        }

        public void OpenHomeLink()
        {
            processService.Start(aboutModel.HomeLink);
        }

        public void OpenLocalizationLink()
        {
            processService.Start(aboutModel.LocalizationLink);
        }

        public void OpenBugTrackerWebLink()
        {
            processService.Start(aboutModel.BugTrackerLink);
        }

        public void DoChangeView(PanelViewMode viewMode)
        {
            pagesPresenter.ViewMode = viewMode;
        }

        private bool menuIsOpening;

        public void PerformMenuKeyDown()
        {
            if (!View.MenuVisible)
            {
                View.MenuVisible = true;
                menuIsOpening = true;
            }
        }

        public bool PerformMenuKeyUp()
        {
            if (menuIsOpening)
            {
                menuIsOpening = false;
                return false;
            }
            View.MenuVisible = false;
            return true;
        }

        public void PerformEscapeKeyDown()
        {
            View.Visible = false;
        }

        public void PerformEscapeKeyUp()
        {
            if (View.MenuVisible)
                View.MenuVisible = false;
        }
    }
}