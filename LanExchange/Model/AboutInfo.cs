using System;
using System.IO;
using System.Reflection;

namespace LanExchange.Model
{
    internal class AboutInfo
    {
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

        public static string Version
        {
            get
            {
                var ver = GetAssembly().GetName().Version;
                var result = string.Format("{0}.{1}", ver.Major, ver.Minor);
                if (ver.MajorRevision > 0)
                    result += string.Format(".{0}", ver.MajorRevision);
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