using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.Model
{
    public class AboutModel : IAboutModel
    {
        private const string HOME_LINK = "https://code.google.com/p/lanexchange";
        private const string LOCALIZATION_LINK = "https://crowdin.net/project/lanexchange";
        private const string BUGTRACKER_LINK = "https://code.google.com/p/lanexchange/issues/list";
        private const string TWITTER = "TheLanExchange";
        private const string EMAIL   = "skivsoft@gmail.com";

        private Assembly GetAssembly()
        {
            return Assembly.GetEntryAssembly();
        }

        public string HomeLink
        {
            get { return HOME_LINK; }
        }

        public string LocalizationLink
        {
            get { return LOCALIZATION_LINK; }
        }

        public string BugTrackerLink
        {
            get { return BUGTRACKER_LINK; }
        }

        [Localizable(false)]
        public string TwitterLink
        {
            get { return "https://twitter.com/" + TWITTER; }
        }

        [Localizable(false)]
        public string EmailLink
        {
            get
            {
                var ver = GetVersion();
                var subject = string.Format(Resources.AboutModel_FeedbackFmt, Title, ver.ToString(3));
                return string.Format("mailto:{0}?subject={1}", EMAIL, subject);
            }
        }

        public string Title
        {
            get
            {
                var assembly = GetAssembly();
                if (assembly == null) return string.Empty;
                var attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                return Path.GetFileNameWithoutExtension(GetAssembly().CodeBase);
            }
        }

        private Version GetVersion()
        {
            var assembly = GetAssembly();
            return assembly == null ? null : assembly.GetName().Version;
        }

        [Localizable(false)]
        public string VersionShort
        {
            get
            {
                var ver = GetVersion();
                return ver == null ? string.Empty : ver.ToString(2);
            }
        }

        [Localizable(false)]
        public string VersionFull
        {
            get
            {
                var ver = GetVersion();
                if (ver == null) return string.Empty;
                var result = ver.ToString(2);
                if (ver.Build > 0)
                    result += String.Format(CultureInfo.CurrentCulture, Resources.AboutInfo_Build, ver.Build);
                return result;
            }
        }

        public string Description
        {
            get
            {
                var assembly = GetAssembly();
                if (assembly == null) return string.Empty;
                var attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                return attributes.Length == 0 ? string.Empty : ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string Product
        {
            get
            {
                var assembly = GetAssembly();
                if (assembly == null) return string.Empty;
                var attributes = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                return attributes.Length == 0 ? string.Empty : ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string Copyright
        {
            get
            {
                var assembly = GetAssembly();
                if (assembly == null) return string.Empty;
                var attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string Company
        {
            get
            {
                var assembly = GetAssembly();
                if (assembly == null) return string.Empty;
                var attributes = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
    }
}