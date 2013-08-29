using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using LanExchange.Properties;

namespace LanExchange.Misc
{
    internal static class AboutInfo
    {
        public const string WebSite = "code.google.com/p/lanexchange/";
        public const string Twitter = "LanExchangeHere";
        public const string Email   = "skivsoft@gmail.com";

        private static Assembly GetAssembly()
        {
            return Assembly.GetEntryAssembly();
        }

        public static string Title
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
        public static string VersionShort
        {
            get
            {
                var ver = GetAssembly().GetName().Version;
                var result = String.Format("{0}.{1}", ver.Major, ver.Minor);
                return result;
            }
        }

        public static string VersionFull
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

        public static string Description
        {
            get
            {
                var attributes = GetAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string Product
        {
            get
            {
                var attributes = GetAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string Copyright
        {
            get
            {
                var attributes = GetAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string Company
        {
            get
            {
                var attributes = GetAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
    }
}