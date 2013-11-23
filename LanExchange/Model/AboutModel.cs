using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using LanExchange.Intf;
using LanExchange.Properties;

namespace LanExchange.Model
{
    public class AboutModel : IAboutModel
    {
        private const string HOME_LINK = "https://code.google.com/p/lanexchange/";
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
            get { return "mailto:" + EMAIL; }
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

        [Localizable(false)]
        public string VersionShort
        {
            get
            {
                var assembly = GetAssembly();
                if (assembly == null) return string.Empty;
                var ver = assembly.GetName().Version;
                var result = String.Format(CultureInfo.CurrentCulture, "{0}.{1}", ver.Major, ver.Minor);
                return result;
            }
        }

        [Localizable(false)]
        public string VersionFull
        {
            get
            {
                var assembly = GetAssembly();
                if (assembly == null) return string.Empty;
                var ver = assembly.GetName().Version;
                var result = String.Format(CultureInfo.CurrentCulture, "{0}.{1}", ver.Major, ver.Minor);
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