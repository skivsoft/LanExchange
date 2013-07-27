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
            m_View.EmailText = Settings.Instance.GetEmailAddress();
        }

        public static void OpenWebLink()
        {
            Process.Start("https://" + Settings.Instance.GetWebSiteUrl());
        }

        public static void OpenEmailLink()
        {
            Process.Start("mailto:" + Settings.Instance.GetEmailAddress());
        }
    }
}
