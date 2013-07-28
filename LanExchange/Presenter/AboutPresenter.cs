namespace LanExchange.Presenter
{
    using System.Diagnostics;
    using Model.Settings;
    using SDK;

    /// <summary>
    /// Presenter for Settings (model) and AboutForm (view).
    /// </summary>
    internal sealed class AboutPresenter
    {
        private readonly IAboutView m_View;

        public AboutPresenter(IAboutView view)
        {
            m_View = view;
        }

        public void LoadFromModel()
        {
            m_View.WebText = Settings.Instance.GetWebSiteUrl();
            m_View.WebToolTip = GetFullWebLink();
            m_View.TwitterText = Settings.Instance.GetTwitter();
            m_View.TwitterToolTip = GetFullTwitterLink();
            m_View.EmailText = Settings.Instance.GetEmailAddress();
            m_View.EmailToolTip = GetFullEmailLink();
        }

        private string GetFullWebLink()
        {
            return "https://" + Settings.Instance.GetWebSiteUrl();
        }

        private string GetFullTwitterLink()
        {
            var name = Settings.Instance.GetTwitter();
            if (name.Substring(0, 1).Equals("@"))
                name = name.Remove(0, 1);
            return "https://twitter.com/" + name;
        }

        private string GetFullEmailLink()
        {
            return "mailto:" + Settings.Instance.GetEmailAddress();
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
