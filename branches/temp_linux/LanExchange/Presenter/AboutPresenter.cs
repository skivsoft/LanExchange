using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using LanExchange.Intf;
using LanExchange.Properties;
using LanExchange.SDK;
using LanExchange.SDK.Model;
using LanExchange.SDK.Presenter;
using LanExchange.SDK.UI;

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

        //[Localizable(false)]
        public string GetDetailsRtf()
        {
            var sb = new StringBuilder();
            //sb.Append(@"{\rtf1\ansi");
            sb.Append(@"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset204 Microsoft Sans Serif;}}");
            sb.Append(@"\viewkind4\uc1\pard\f0\fs17 ");
            var plugins = App.Resolve<IPluginManager>().PluginsAuthors;
            if (plugins.Count > 0)
            {
                sb.AppendLine(string.Format(@"\b {0}\b0", Resources.AboutForm_Plugins));
                foreach (var pair in plugins)
                {
                    sb.Append("    " + pair.Key);
                    if (!string.IsNullOrEmpty(pair.Value))
                        sb.Append(@"\tab " + pair.Value);
                    sb.AppendLine();
                }
                sb.AppendLine();
            }
            var translations = App.TR.GetTranslations();
            if (translations.Count > 0)
            {
                sb.AppendLine(string.Format(@"\b {0}\b0", Resources.AboutForm_Translations));
                foreach (var pair in translations)
                {
                    var line = pair.Key;
                    if (!string.IsNullOrEmpty(pair.Value))
                        line += @" \tab " + pair.Value;
                    sb.AppendLine("    " + line);
                }
            }
            sb.Append("}");
            return sb.ToString().Replace("\r\n", @"\line ");
        }

    }
}