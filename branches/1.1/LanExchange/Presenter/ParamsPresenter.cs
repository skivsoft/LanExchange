﻿using System;
using LanExchange.View;
using LanExchange.Model;

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
        }

        public void SaveToModel()
        {
            Settings.IsAutorun = m_View.IsAutorun;
            Settings.Instance.RunMinimized = m_View.RunMinimized;
            Settings.Instance.AdvancedMode = m_View.AdvancedMode;
            Settings.Instance.RefreshTimeInSec = m_View.RefreshTimeInMin * 60;
            MainPresenter.Instance.MainView.AdminMode = Settings.Instance.AdvancedMode;
        }
    }
}