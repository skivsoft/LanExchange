using LanExchange.Model;
using LanExchange.Model.Settings;
using LanExchange.SDK;

namespace LanExchange.Presenter
{
    /// <summary>
    /// Presenter for Settings (model) and ParamsForm (view).
    /// </summary>
    public class ParamsPresenter
    {
        private readonly ISettingsView m_View;

        public ParamsPresenter(ISettingsView view)
        {
            m_View = view;
        }

        public void LoadFromModel()
        {
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
