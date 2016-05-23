using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using LanExchange.Actions;
using LanExchange.Helpers;
using LanExchange.Interfaces;
using LanExchange.Plugin.Shortcut;
using LanExchange.SDK;
using LanExchange.Model;
using System.Diagnostics.Contracts;
using LanExchange.SDK.Managers;
using LanExchange.SDK.Factories;

namespace LanExchange.Presenter
{
    public class MainPresenter : PresenterBase<IMainView>, IMainPresenter
    {
        private readonly ILazyThreadPool threadPool;
        private readonly IPanelColumnManager panelColumns;
        private readonly IActionManager actionManager;
        private readonly IPagesPresenter pagesPresenter;
        private IHotkeysService hotkeys;

        public MainPresenter(
            ILazyThreadPool threadPool,
            IPanelColumnManager panelColumns,
            IActionManager actionManager,
            IPagesPresenter pagesPresenter,
            IWindowFactory windowFactory)
        {
            Contract.Requires<ArgumentNullException>(threadPool != null);
            Contract.Requires<ArgumentNullException>(panelColumns != null);
            Contract.Requires<ArgumentNullException>(actionManager != null);
            Contract.Requires<ArgumentNullException>(pagesPresenter != null);

            this.threadPool = threadPool;
            this.panelColumns = panelColumns;
            this.actionManager = actionManager;
            this.pagesPresenter = pagesPresenter;

            // TODO: delegate action registration to DI container
            actionManager.RegisterAction(new AboutAction(windowFactory));
            actionManager.RegisterAction(new PagesReReadAction(pagesPresenter));
            actionManager.RegisterAction(new PagesCloseTabAction(pagesPresenter));
            actionManager.RegisterAction(new PagesCloseOtherAction(pagesPresenter));
            actionManager.RegisterAction(new ShortcutKeysAction(this, pagesPresenter));
        }

        public void PrepareForm()
        {
            View.SetRunMinimized(App.Config.RunMinimized);
            // setup languages in menu
            View.SetupMenuLanguages();
            // init main form
            View.SetupPages();
            SetupForm();
            // set hotkey for activate: Ctrl+Win+X
            hotkeys = App.Resolve<IHotkeysService>();
            App.Resolve<IDisposableManager>().RegisterInstance(hotkeys);
            if (hotkeys.RegisterShowWindowKey(View.Handle))
                View.ShowWindowKey = hotkeys.ShowWindowKey;
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
            var aboutModel = App.Resolve<IAboutModel>();
            var text = String.Format(CultureInfo.CurrentCulture, "{0} {1}", aboutModel.Title, aboutModel.VersionShort);
            View.Text = text;
            // show tray
            View.TrayText = text;
            View.TrayVisible = true;
        }

        [Localizable(false)]
        public void ConfigOnChanged(object sender, PropertyChangedEventArgs e)
        {
            var config = sender as ConfigModel;
            if (config == null) return;
            switch (e.PropertyName)
            {
                case nameof(config.ShowInfoPanel):
                    App.MainView.ShowInfoPanel = config.ShowInfoPanel;
                    break;
                case nameof(config.ShowGridLines):
                    var panelView = pagesPresenter.View.ActivePanelView;
                    if (panelView != null)
                        panelView.GridLines = config.ShowGridLines;
                    break;
                case nameof(config.NumInfoLines):
                    App.MainView.NumInfoLines = config.NumInfoLines;
                    pagesPresenter.DoPanelViewFocusedItemChanged(pagesPresenter.View.ActivePanelView, EventArgs.Empty);
                    break;
                case nameof(config.Language):
                    App.TR.CurrentLanguage = config.Language;
                    GlobalTranslateUI();
                    break;
            }
        }

        public void OnDataReady(object sender, DataReadyArgs args)
        {
            View.SafeInvoke(new WaitCallback(MainForm_RefreshItem), args.Item);
        }

        private void MainForm_RefreshItem(object item)
        {
            var pv = App.Resolve<IPagesView>().ActivePanelView;
            if (pv != null)
            {
                var index = pv.Presenter.Objects.IndexOf(item as PanelItemBase);
                if (index >= 0)
                    pv.RedrawItem(index);
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
            var panelItem = pv.Presenter.GetFocusedPanelItem(true);
            // check if parent item more informative than current panel item
            while (panelItem != null)
            {
                if (panelItem.Parent is PanelItemRootBase) 
                    break;
                if (panelItem.Parent != null && (panelItem.Parent.Parent is PanelItemRootBase))
                    break;
                panelItem = panelItem.Parent;
            }
            if (panelItem == null || View.Info == null) return;
            View.Info.CurrentItem = panelItem;
            View.Info.NumLines = App.Config.NumInfoLines;
            var helper = new PanelModelCopyHelper(null, panelColumns);
            helper.CurrentItem = panelItem;
            int index = 0;
            foreach (var column in helper.Columns)
            {
                View.Info.SetLine(index, helper.GetColumnValue(column.Index));
                ++index;
                if (index >= View.Info.NumLines) break;
            }
            for (int i = index; i < View.Info.NumLines; i++)
                View.Info.SetLine(i, string.Empty);
        }


        public void GlobalTranslateUI()
        {
            var service = App.Resolve<IWaitingService>();
            if (App.MainView != null)
                service.BeginWait();
            try
            {
                // recreate all columns
                GlobalTranslateColumns();
                // Run TranslateUI() for all opened forms
                App.Resolve<IAppPresenter>().TranslateOpenForms();
            }
            finally
            {
                if (App.MainView != null)
                    service.EndWait();
            }
        }

        public bool IsHotKey(short id)
        {
            return id == hotkeys.HotkeyId;
        }

        private static void GlobalTranslateColumns()
        {
            var columnManager = App.Resolve<IPanelColumnManager>();
            var factoryManager = App.Resolve<IPanelItemFactoryManager>();
            if (columnManager == null || factoryManager == null || factoryManager.IsEmpty) 
                return;
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
            var mainFormWidth = App.Config.MainFormWidth;
            var mainFormX = App.Config.MainFormX;
            // correct width and height
            bool boundsIsNotSet = mainFormWidth == 0;
            Rectangle workingArea;
            var screen = App.Resolve<IScreenService>();
            if (boundsIsNotSet)
                workingArea = screen.PrimaryScreenWorkingArea;
            else
                workingArea = screen.GetWorkingArea(new Point(mainFormX + mainFormWidth / 2, 0));
            var rect = new Rectangle();
            rect.X = mainFormX;
            rect.Y = workingArea.Top;
            rect.Width = Math.Min(Math.Max(GetDefaultWidth(), mainFormWidth), workingArea.Width);
            rect.Height = workingArea.Height - screen.MenuHeight;
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
            var screen = App.Resolve<IScreenService>();
            Rectangle workingArea = screen.GetWorkingArea(rect);
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
            var mainFormWidth = App.Config.MainFormWidth;
            var mainFormX = App.Config.MainFormX;
            if (rect.Left != mainFormX || rect.Width != mainFormWidth)
            {
                App.Config.MainFormX = rect.Left;
                App.Config.MainFormWidth = rect.Width;
            }
        }

        public int FindShortcutKeysPanelIndex()
        {
            for (int index = 0; index < pagesPresenter.Count; index++)
            {
                var model = pagesPresenter.GetItem(index);
                if (model.DataType.Equals(typeof(ShortcutPanelItem).Name))
                    return index;
            }
            return -1;
        }
    }
}