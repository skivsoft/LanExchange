using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using LanExchange.Application.Extensions;
using LanExchange.Application.Interfaces;
using LanExchange.Properties;

namespace LanExchange.Application.Models
{
    internal sealed class AboutModel : IAboutModel
    {
        private const string ISSUES = "/issues";
        private const string LOCALIZATION_LINK = "https:// crowdin.net/project/lanexchange";


        private readonly Assembly entryAssembly;
        private readonly string homeLink;

        public AboutModel()
        {
            entryAssembly = Assembly.GetEntryAssembly();
            homeLink = entryAssembly.GetCustomAttribute<AssemblyCompanyAttribute>().Company ?? string.Empty;
        }

        public string HomeLink
        {
            get { return homeLink; }
        }

        public string LocalizationLink
        {
            get { return LOCALIZATION_LINK; }
        }

        public string BugTrackerLink
        {
            get { return homeLink + ISSUES; }
        }

        public string Title
        {
            get
            {
                var result = entryAssembly.GetCustomAttribute<AssemblyTitleAttribute>().Title;
                if (string.IsNullOrEmpty(result))
                    return Path.GetFileNameWithoutExtension(entryAssembly.CodeBase);
                return result;
            }
        }

        private Version GetVersion()
        {
            return entryAssembly == null ? null : entryAssembly.GetName().Version;
        }

        [Localizable(false)]
        public string VersionShort
        {
            get
            {
                var version = GetVersion();
                return version == null ? string.Empty : version.ToString(2);
            }
        }

        [Localizable(false)]
        public string VersionFull
        {
            get
            {
                var version = GetVersion();
                if (version == null) return string.Empty;
                var result = version.ToString(2);
                if (version.Build > 0)
                    result += string.Format(CultureInfo.CurrentCulture, Resources.AboutInfo_Build, version.Build);
                return result;
            }
        }

        public string Description
        {
            get
            {
                return entryAssembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description ?? string.Empty;
            }
        }

        public string Product
        {
            get
            {
                return entryAssembly.GetCustomAttribute<AssemblyProductAttribute>().Product ?? string.Empty;
            }
        }

        public string Copyright
        {
            get
            {
                return entryAssembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright ?? string.Empty;
            }
        }
    }
}