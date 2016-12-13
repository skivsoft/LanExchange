using System;
using System.IO;
using System.Reflection;

namespace WMIViewer.Extensions
{
    internal static class AssemblyExtension
    {
        /// <exception cref="ArgumentNullException"></exception>
        internal static string GetProgramTitle(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            var attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
                var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                if (!string.IsNullOrEmpty(titleAttribute.Title))
                    return titleAttribute.Title;
            }

            return Path.GetFileNameWithoutExtension(assembly.CodeBase);
        }
    }
}