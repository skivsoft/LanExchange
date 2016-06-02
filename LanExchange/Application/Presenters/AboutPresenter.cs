﻿using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text;
using LanExchange.Application.Interfaces;
using LanExchange.Presentation.Interfaces;
using LanExchange.Properties;

namespace LanExchange.Application.Presenters
{
    /// <summary>
    /// Presenter for Settings (model) and AboutForm (view).
    /// </summary>
    [Localizable(false)]
    internal sealed class AboutPresenter : PresenterBase<IAboutView>, IAboutPresenter
    {
        private readonly IAboutModel model;
        private readonly ITranslationService translationService;
        private readonly IProcessService processService;

        public AboutPresenter(
            IAboutModel model,
            ITranslationService translationService,
            IProcessService processService)
        {
            Contract.Requires<ArgumentNullException>(model != null);
            Contract.Requires<ArgumentNullException>(translationService != null);
            Contract.Requires<ArgumentNullException>(processService != null);

            this.model = model;
            this.translationService = translationService;
            this.processService = processService;
        }

        protected override void InitializePresenter()
        {
            View.TranslateUI();
            LoadFromModel();
        }

        private void LoadFromModel()
        {
            View.Text = string.Format(CultureInfo.CurrentCulture, View.Text, model.Title);
            View.VersionText = model.VersionFull;
            View.CopyrightText = model.Copyright;
            View.WebText = model.HomeLink;
            View.WebToolTip = model.HomeLink;
        }

        public void OpenHomeLink()
        {
            processService.Start(model.HomeLink);
        }

        public void OpenLocalizationLink()
        {
            processService.Start(model.LocalizationLink);
        }

        public void OpenBugTrackerWebLink()
        {
            processService.Start(model.BugTrackerLink);
        }

        public string GetDetailsRtf()
        {
            var sb = new StringBuilder();
            //sb.Append(@"{\rtf1\ansi");
            sb.Append(@"{\rtf1\ansi\deff0{\fonttbl{\f0 Microsoft Sans Serif;}}"); // \fnil\fcharset204
            sb.Append(@"\viewkind4\uc1\pard\f0\fs17 ");

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

        public void PerformShowDetails()
        {
            View.DetailsVisible = !View.DetailsVisible;
        }
    }
}