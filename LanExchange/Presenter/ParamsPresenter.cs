using LanExchange.Interface;
using LanExchange.Model;
using LanExchange.Model.Settings;

namespace LanExchange.Presenter
{
    /// <summary>
    /// Presenter for Settings (model) and ParamsForm (view).
    /// </summary>
    public class ParamsPresenter : IPresenter
    {
        private readonly IParamsView m_View;

        public ParamsPresenter(IParamsView view)
        {
            m_View = view;
        }

        public void LoadFromModel()
        {
            m_View.IsAutorun = Settings.IsAutorun;
            m_View.RunMinimized = Settings.Instance.RunMinimized;
            m_View.AdvancedMode = Settings.Instance.AdvancedMode;
            m_View.RefreshTimeInMin = Settings.Instance.RefreshTimeInSec / 60;
            m_View.ShowHiddenShares = Settings.Instance.ShowHiddenShares;
            m_View.ShowPrinters = Settings.Instance.ShowPrinters;
        }

        public void SaveToModel()
        {
            Settings.IsAutorun = m_View.IsAutorun;
            Settings.Instance.RunMinimized = m_View.RunMinimized;
            Settings.Instance.AdvancedMode = m_View.AdvancedMode;
            Settings.Instance.RefreshTimeInSec = m_View.RefreshTimeInMin * 60;
            Settings.Instance.ShowHiddenShares = m_View.ShowHiddenShares;
            Settings.Instance.ShowPrinters = m_View.ShowPrinters;
            Settings.SaveIfModified();
            PanelSubscription.Instance.RefreshInterval = (int)Settings.Instance.RefreshTimeInSec * 1000;
        }
    }
}
