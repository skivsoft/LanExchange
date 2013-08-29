using System.ComponentModel;
using System.Diagnostics;
using LanExchange.Core;
using LanExchange.Misc;
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
        public void LoadFromModel()
        {
            View.Text = string.Format(View.Text, AboutInfo.Product);
            View.VersionText = AboutInfo.VersionFull;
            View.CopyrightText = AboutInfo.Copyright;
            View.WebText = AboutInfo.WebSite;
            View.WebToolTip = GetFullWebLink();
            View.TwitterText = "@" + AboutInfo.Twitter;
            View.TwitterToolTip = GetFullTwitterLink();
            View.EmailText = AboutInfo.Email;
            View.EmailToolTip = GetFullEmailLink();
        }

        private string GetFullWebLink()
        {
            return "https://" + AboutInfo.WebSite;
        }

        private string GetFullTwitterLink()
        {
            return "https://twitter.com/" + AboutInfo.Twitter;
        }

        private string GetFullEmailLink()
        {
            return "mailto:" + AboutInfo.Email;
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