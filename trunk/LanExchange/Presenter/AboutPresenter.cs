using System.ComponentModel;
using System.Diagnostics;
using LanExchange.Core;
using LanExchange.Intf;
using LanExchange.Model;
using LanExchange.UI;

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
            View.Text = string.Format(View.Text, m_Model.Product);
            View.VersionText = m_Model.VersionFull;
            View.CopyrightText = m_Model.Copyright;
            View.WebText = m_Model.WebSite;
            View.WebToolTip = GetFullWebLink();
            View.TwitterText = "@" + m_Model.Twitter;
            View.TwitterToolTip = GetFullTwitterLink();
            View.EmailText = m_Model.Email;
            View.EmailToolTip = GetFullEmailLink();
        }

        private string GetFullWebLink()
        {
            return "https://" + m_Model.WebSite;
        }

        private string GetFullTwitterLink()
        {
            return "https://twitter.com/" + m_Model.Twitter;
        }

        private string GetFullEmailLink()
        {
            return "mailto:" + m_Model.Email;
        }

        public void OpenWebLink()
        {
            Process.Start(GetFullWebLink());
        }

        public void OpenTwitterLink()
        {
            Process.Start(GetFullTwitterLink());
        }

        public void OpenEmailLink()
        {
            Process.Start(GetFullEmailLink());
        }
    }
}