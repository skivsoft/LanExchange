using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using LanExchange.Intf;

namespace LanExchange.Presenter
{
    /// <summary>
    /// Presenter for Settings (model) and AboutForm (view).
    /// </summary>
    [Localizable(false)]
    public sealed class AboutPresenter : PresenterBase<IAboutView>, IAboutPresenter
    {
        private readonly IAboutModel m_Model;

        public AboutPresenter(IAboutModel model)
        {
            m_Model = model;
        }

        public void LoadFromModel()
        {
            View.Text = string.Format(CultureInfo.CurrentCulture, View.Text, m_Model.Title);
            View.VersionText = m_Model.VersionFull;
            View.CopyrightText = m_Model.Copyright;
            View.WebText = m_Model.HomeLink;
            View.WebToolTip = m_Model.HomeLink;
            View.TwitterToolTip = m_Model.TwitterLink;
        }

        public void OpenHomeLink()
        {
            Process.Start(m_Model.HomeLink);
        }

        public void OpenLocalizationLink()
        {
            Process.Start(m_Model.LocalizationLink);
        }

        public void OpenBugTrackerWebLink()
        {
            Process.Start(m_Model.BugTrackerLink);
        }

        public void OpenTwitterLink()
        {
            Process.Start(m_Model.TwitterLink);
        }

        public void OpenEmailLink()
        {
            Process.Start(m_Model.EmailLink);
        }
    }
}