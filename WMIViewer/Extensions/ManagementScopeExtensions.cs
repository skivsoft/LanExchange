using System;
using System.Diagnostics;
using System.Management;

namespace WMIViewer.Extensions
{
    public static class ManagementScopeExtensions
    {
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetPropertyValue(this ManagementScope scope, string className, string propName)
        {
            if (scope == null) throw new ArgumentNullException(nameof(scope));
            var result = string.Empty;
            try
            {
                scope.Connect();
                var query = new ObjectQuery("SELECT * FROM " + className);
                using (var searcher = new ManagementObjectSearcher(scope, query))
                {
                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        result = queryObj[propName].ToString();
                        break;
                    }
                }
            }
            catch (ManagementException ex)
            {
                Debug.Print(ex.Message);
            }

            return result;
        }

        /// <exception cref="ArgumentNullException"></exception>
        public static void SetPropertyValue(this ManagementScope scope, string className, string propName, string value)
        {
            if (scope == null) throw new ArgumentNullException(nameof(scope));
            try
            {
                scope.Connect();
                var query = new ObjectQuery("SELECT * FROM " + className);
                using (var searcher = new ManagementObjectSearcher(scope, query))
                {
                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        queryObj[propName] = value;
                        queryObj.Put();
                        break;
                    }
                }
            }
            catch (ManagementException ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}