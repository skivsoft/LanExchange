using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using LanExchange.Properties;

namespace LanExchange.Model
{
    public class AboutModel : IAboutModel
    {
        const string WEBSITE = "code.google.com/p/lanexchange/";
        const string TWITTER = "LanExchangeHere";
        const string EMAIL   = "skivsoft@gmail.com";

        private Assembly GetAssembly()
        {
            return Assembly.GetEntryAssembly();
        }

        public string WebSite
        {
            get { return WEBSITE; }
        }

        public string Twitter
        {
            get { return TWITTER; }
        }

        public string Email
        {
            get { return EMAIL; }
        }

        public string Title
        {
            get
            {
                var attributes = GetAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
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
                var ver = GetAssembly().GetName().Version;
                var result = String.Format("{0}.{1}", ver.Major, ver.Minor);
                return result;
            }
        }

        [Localizable(false)]
        public string VersionFull
        {
            get
            {
                var ver = GetAssembly().GetName().Version;
                var result = String.Format("{0}.{1}", ver.Major, ver.Minor);
                if (ver.Build > 0)
                    result += String.Format(Resources.AboutInfo_Build, ver.Build);
                return result;
            }
        }

        public string Description
        {
            get
            {
                var attributes = GetAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string Product
        {
            get
            {
                var attributes = GetAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string Copyright
        {
            get
            {
                var attributes = GetAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string Company
        {
            get
            {
                var attributes = GetAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
    }
}