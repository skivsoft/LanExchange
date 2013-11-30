using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.Intf;
using LanExchange.Presenter.Action;
using LanExchange.SDK;
using LanExchange.Utils;

namespace LanExchange.Presenter
{
    public class MainPresenter : PresenterBase<IMainView>, IMainPresenter
    {
        private readonly Dictionary<Type, IAction> m_Actions;

        public MainPresenter()
        {
            m_Actions = new Dictionary<Type, IAction>();
            RegisterAction(new ActionAbout());
            RegisterAction(new ActionReRead());
            RegisterAction(new ActionCloseTab());
            RegisterAction(new ActionCloseOther());
            RegisterAction(new ActionShortcutKeys());
        }

        [Localizable(false)]
        public void ConfigOnChanged(object sender, ConfigChangedArgs e)
        {
            var config = sender as IConfigModel;
            if (config == null) return;
            switch (e.Name)
            {
                case ConfigNames.ShowInfoPanel:
                    App.MainView.ShowInfoPanel = config.ShowInfoPanel;
                    break;
                case ConfigNames.ShowGridLines:
                    var panelView = App.MainPages.View.ActivePanelView;
                    if (panelView != null)
                        panelView.GridLines = config.ShowGridLines;
                    break;
                case ConfigNames.NumInfoLines:
                    App.MainView.NumInfoLines = config.NumInfoLines;
                    App.MainPages.DoPanelViewFocusedItemChanged(App.MainPages.View.ActivePanelView, EventArgs.Empty);
                    break;
                case ConfigNames.Language:
                    App.TR.CurrentLanguage = config.Language;
                    GlobalTranslateUI();
                    break;
            }
        }

        private void GlobalTranslateUI()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                // recreate all columns
                GlobalTranslateColumns();
                // Run TranslateUI() for all opened forms
                TranslationUtils.TranslateOpenForms();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void GlobalTranslateColumns()
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

        private int GetDefaultWidth()
        {
            const double phi2 = 0.6180339887498949;
            return (int)(Screen.PrimaryScreen.WorkingArea.Width * phi2 * phi2);
        }

        public Rectangle SettingsGetBounds()
        {
            var mainFormWidth = App.Config.MainFormWidth;
            var mainFormX = App.Config.MainFormX;
            // correct width and height
            bool boundsIsNotSet = mainFormWidth == 0;
            Rectangle workingArea;
            if (boundsIsNotSet)
                workingArea = Screen.PrimaryScreen.WorkingArea;
            else
                workingArea = Screen.GetWorkingArea(new Point(mainFormX + mainFormWidth / 2, 0));
            var rect = new Rectangle();
            rect.X = mainFormX;
            rect.Y = workingArea.Top;
            rect.Width = Math.Min(Math.Max(GetDefaultWidth(), mainFormWidth), workingArea.Width);
            rect.Height = workingArea.Height - SystemInformation.MenuHeight;
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
            Rectangle workingArea = Screen.GetWorkingArea(rect);
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

        public void RegisterAction(IAction action)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            m_Actions.Add(action.GetType(), action);
        }

        public void ExecuteAction<T>() where T : IAction
        {
            IAction action;
            if (m_Actions.TryGetValue(typeof(T), out action))
                action.Execute();
        }

        public bool IsActionEnabled<T>() where T : IAction
        {
            IAction action;
            if (m_Actions.TryGetValue(typeof(T), out action))
                return action.Enabled;
            return false;
        }
    }
}