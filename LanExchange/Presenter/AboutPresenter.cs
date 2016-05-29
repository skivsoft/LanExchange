using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using LanExchange.Properties;
using LanExchange.SDK;
using System.Diagnostics.Contracts;

namespace LanExchange.Presenter
{
    /// <summary>
    /// Presenter for Settings (model) and AboutForm (view).
    /// </summary>
    [Localizable(false)]
    public sealed class AboutPresenter : PresenterBase<IAboutView>, IAboutPresenter
    {
        private readonly IAboutModel model;
        private readonly ITranslationService translationService;
        private readonly IPluginManager pluginManager;

        public AboutPresenter(
            IAboutModel model,
            ITranslationService translationService,
            IPluginManager pluginManager)
        {
            Contract.Requires<ArgumentNullException>(model != null);
            Contract.Requires<ArgumentNullException>(translationService != null);
            Contract.Requires<ArgumentNullException>(pluginManager != null);

            this.model = model;
            this.translationService = translationService;
            this.pluginManager = pluginManager;
        }

        public void LoadFromModel()
        {
            View.Text = string.Format(CultureInfo.CurrentCulture, View.Text, model.Title);
            View.VersionText = model.VersionFull;
            View.CopyrightText = model.Copyright;
            View.WebText = model.HomeLink;
            View.WebToolTip = model.HomeLink;
            View.TwitterToolTip = model.TwitterLink;
        }

        public void OpenHomeLink()
        {
            Process.Start(model.HomeLink);
        }

        public void OpenLocalizationLink()
        {
            Process.Start(model.LocalizationLink);
        }

        public void OpenBugTrackerWebLink()
        {
            Process.Start(model.BugTrackerLink);
        }

        public void OpenTwitterLink()
        {
            Process.Start(model.TwitterLink);
        }

        public void OpenEmailLink()
        {
            Process.Start(model.EmailLink);
        }

        //[Localizable(false)]
        public string GetDetailsRtf()
        {
            var sb = new StringBuilder();
            //sb.Append(@"{\rtf1\ansi");
            sb.Append(@"{\rtf1\ansi\deff0{\fonttbl{\f0 Microsoft Sans Serif;}}"); // \fnil\fcharset204
            sb.Append(@"\viewkind4\uc1\pard\f0\fs17 ");
            var plugins = pluginManager.PluginsAuthors;
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
            var translations = translationService.GetTranslations();
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
            return sb.ToString().Replace(Environment.NewLine, @"\line ");
        }

        protected override void InitializePresenter()
        {
        }
    }
}