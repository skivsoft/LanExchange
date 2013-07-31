using LanExchange.Model;
using LanExchange.Model.Settings;
using LanExchange.Properties;
using LanExchange.SDK;
using LanExchange.UI;

namespace LanExchange.Presenter
{
    /// <summary>
    /// Presenter for Settings (model) and ParamsForm (view).
    /// </summary>
    public class SettingsPresenter
    {
        private readonly ISettingsView m_View;

        public SettingsPresenter(ISettingsView view)
        {
            m_View = view;
        }

        public void LoadFromModel()
        {
            m_View.AddTab(null, Resources.Settings_General, new SettingsTabGeneralFactory());
            var pluginsHandle = m_View.AddTab(null, Resources.Settings_Plugins, new SettingsTabPluginsFactory());
            foreach(var item in AppPresenter.Plugins.Items)
            {
                var factory = item.GetSettingsTabViewFactory();
                if (factory == null)
                    continue;
                m_View.AddTab(pluginsHandle, item.GetType().Name, factory);
            }
            m_View.SelectFirstNode();
            // TODO UNCOMMENT THIS!
            //m_View.IsAutoRun = Settings.IsAutorun;
            //m_View.RunMinimized = Settings.Instance.RunMinimized;
            //m_View.AdvancedMode = Settings.Instance.AdvancedMode;
            //m_View.RefreshTimeInMin = Settings.Instance.RefreshTimeInSec / 60;
            //m_View.ShowHiddenShares = Settings.Instance.ShowHiddenShares;
            //m_View.ShowPrinters = Settings.Instance.ShowPrinters;
        }

        public void SaveToModel()
        {
            // TODO UNCOMMENT THIS!
            //Settings.IsAutorun = m_View.IsAutoRun;
            //Settings.Instance.RunMinimized = m_View.RunMinimized;
            //Settings.Instance.AdvancedMode = m_View.AdvancedMode;
            //Settings.Instance.RefreshTimeInSec = m_View.RefreshTimeInMin * 60;
            //Settings.Instance.ShowHiddenShares = m_View.ShowHiddenShares;
            //Settings.Instance.ShowPrinters = m_View.ShowPrinters;
            //Settings.SaveIfModified();
            //PanelSubscription.Instance.RefreshInterval = (int)Settings.Instance.RefreshTimeInSec * 1000;
        }
    }
}
