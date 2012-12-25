using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.View;
using LanExchange.Model;
using LanExchange.UI;

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
            if (m_View == null) return;
            m_View.IsAutorun = Settings.IsAutorun;
            m_View.RunMinimized = Settings.Instance.RunMinimized;
            m_View.AdvancedMode = Settings.Instance.AdvancedMode;
            m_View.RefreshTimeInMin = Settings.Instance.RefreshTimeInSec / 60;
        }

        public void SaveToModel()
        {
            if (m_View == null) return;
            Settings.IsAutorun = m_View.IsAutorun;
            Settings.Instance.RunMinimized = m_View.RunMinimized;
            Settings.Instance.AdvancedMode = m_View.AdvancedMode;
            Settings.Instance.RefreshTimeInSec = m_View.RefreshTimeInMin * 60;
            MainForm.Instance.AdminMode = Settings.Instance.AdvancedMode;
        }
    }
}
