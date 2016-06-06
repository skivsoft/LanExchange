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
            ICommandManager commandManager)
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
        }

        protected override void InitializePresenter()
        {
            PrepareForm();
        }

        private void PrepareForm()
        {
            // setup languages in menu
            View.SetupMenuLanguages();
            // init main form
            View.SetupPages();
            SetupForm();
            // set hotkey for activate: Ctrl+Win+X
            disposableManager.RegisterInstance(hotkeyService);
            if (hotkeyService.RegisterShowWindowKey(View.Handle))
                View.ShowWindowKey = hotkeyService.ShowWindowKey;
            // set lazy events
            threadPool.DataReady += OnDataReady;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        [Localizable(false)]
        public void SetupForm()
        {
            //App.MainPages.View.SetupContextMenu();
            pagesPresenter.PanelViewFocusedItemChanged += Pages_PanelViewFocusedItemChanged;
            // set mainform bounds
            var rect = SettingsGetBounds();
            View.SetBounds(rect.Left, rect.Top, rect.Width, rect.Height);
            // set mainform title
            var text = String.Format(CultureInfo.CurrentCulture, "{0} {1}", aboutModel.Title, aboutModel.VersionShort);
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

        public void DoPagesCloseTab()
        {
            //commandManager.ExecuteCommand<PagesCloseTabCommand>();
        }

        public void DoAbout()
        {
            commandManager.ExecuteCommand<AboutCommand>();
        }

        public void DoPagesCloseOther()
        {
            //commandManager.ExecuteCommand<PagesCloseOtherCommand>();
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

        public void SettingsSetBounds(Rectangle rect)
        {
            Rectangle workingArea = screenService.GetWorkingArea(rect);
            // shift rect into working area
            if (rect.Left < workingArea.Left) rect.X = workingArea.Left;
            if (rect.Top < workingArea.Top) rect.Y = workingArea.Top;
            if (rect.Right > workingArea.Right) rect.X -= rect.Right - workingArea.Right;
            if (rect.Bottom > workingArea.Bottom) rect.Y -= rect.Bottom - workingArea.Bottom;
            // determination side to snap right or left
            int centerX = (rect.Left + rect.Right) >> 1;
            int workingAreaCenterX = (workingArea.Left + workingArea.Right) >> 1;
            if (centerX >= workingAreaCenterX)
                // snap to right side
                rect.X = workingArea.Right - rect.Width;
            else
                // snap to left side
                rect.X -= rect.Left - workingArea.Left;
            // set properties
            var mainFormWidth = Settings.Default.MainFormWidth;
            var mainFormLeft = Settings.Default.MainFormLeft;
            if (rect.Left != mainFormLeft || rect.Width != mainFormWidth)
            {
                Settings.Default.MainFormLeft = rect.Left;
                Settings.Default.MainFormWidth = rect.Width;
            }
        }
    }
}