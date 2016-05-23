using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Management;

namespace WMIViewer.Model
{
    /// <summary>
    /// List of used wmi classes.
    /// </summary>
    [Localizable(false)]
    internal sealed class WmiClassList
    {
        private static WmiClassList wmiClassList;
        private bool loaded;
        private readonly List<string> classes;
        private readonly List<string> readOnlyClasses;
        private readonly List<string> includeClasses;
        private readonly List<string> allClasses;
        private int propCount;
        private int methodCount;
        private ManagementScope namespaceScope;

        private WmiClassList()
        {
            classes = new List<string>();
            includeClasses = new List<string>();
            readOnlyClasses = new List<string>();
            allClasses = new List<string>();
        }

        public static WmiClassList Instance
        {
            get
            {
                if (wmiClassList == null)
                    wmiClassList = new WmiClassList();
                return wmiClassList;
            }
        }

        public IList<string> Classes
        {
            get { return classes; }
        }

        public IList<string> ReadOnlyClasses
        {
            get { return readOnlyClasses; }
        }

        public IList<string> IncludeClasses
        {
            get { return includeClasses; }
        }

        //public IEnumerable<string> AllClasses
        //{
        //    get { return m_AllClasses; }
        //}

        public int ClassCount
        {
            get { return classes.Count + readOnlyClasses.Count; }
        }

        public int PropCount
        {
            get { return propCount; }
        }

        public int MethodCount
        {
            get { return methodCount; }
        }

        public bool Loaded
        {
            get { return loaded; }
        }

        private bool ConnectToLocalMachine()
        {
            if (namespaceScope != null && namespaceScope.IsConnected)
                return true;
            namespaceScope = new ManagementScope(CmdLineArgs.DefaultNamespaceName, null);
            try
            {
                namespaceScope.Connect();
            }
            catch (ManagementException ex)
            {
                Debug.Print(ex.Message);
            }
            return namespaceScope.IsConnected;
        }

        //public string GetClassQualifiers(ManagementScope scope, string className)
        //{
        //    if (scope == null)
        //    {
        //        if (!ConnectToLocalMachine()) return string.Empty;
        //        scope = m_Namespace;
        //    }
        //    var sb = new StringBuilder();
        //    try
        //    {
        //        // Gets the property qualifiers.
        //        var op = new ObjectGetOptions(null, TimeSpan.MaxValue, true);
        //        var path = new ManagementPath(className);
        //        using (var mc = new ManagementClass(scope, path, op))
        //        {
        //            mc.Options.UseAmendedQualifiers = true;
        //            foreach (var qd in mc.Qualifiers)
        //            {
        //                if (qd.Name.Equals("Description"))
        //                    continue;
        //                sb.Append(qd.Name);
        //                sb.Append("=");
        //                sb.Append(qd.Value);
        //                sb.Append(", ");
        //            }
        //        }
        //    }
        //    catch(ManagementException ex)
        //    {
        //        Debug.Print(ex.Message);
        //    }

        //    return sb.ToString();
        //}

        public string GetClassDescription(ManagementScope scope, string className)
        {
            if (scope == null)
            {
                if (!ConnectToLocalMachine()) return string.Empty;
                scope = namespaceScope;
            }
            var result = string.Empty;
            try
            {
                // Gets the property qualifiers.
                var op = new ObjectGetOptions(null, TimeSpan.MaxValue, true);

                var path = new ManagementPath(className);
                using (var mc = new ManagementClass(scope, path, op))
                {
                    mc.Options.UseAmendedQualifiers = true;
                    foreach (var dataObject in mc.Qualifiers)
                        if (dataObject.Name.Equals("Description"))
                        {
                            result = dataObject.Value.ToString();
                            break;
                        }
                }
            }
            catch(ManagementException ex)
            {
                Debug.Print(ex.Message);
            }
            return result;

        }

        public string GetPropertyDescription(string className, string propName)
        {
            if (!ConnectToLocalMachine()) return string.Empty;
            var result = string.Empty;
            try
            {
                // Gets the property qualifiers.
                var op = new ObjectGetOptions(null, TimeSpan.MaxValue, true);

                var path = new ManagementPath(className);
                using (var mc = new ManagementClass(namespaceScope, path, op))
                {
                    mc.Options.UseAmendedQualifiers = true;
                    var prop = mc.Properties[propName];
                    foreach (var dataObject in prop.Qualifiers)
                        if (dataObject.Name.Equals("Description"))
                            result = dataObject.Value.ToString();
                }
            }
            catch (ManagementException ex)
            {
                Debug.Print(ex.Message);
            }
            return result;
        }

        public bool IsPropertyEditable(string className, string propName)
        {
            if (!ConnectToLocalMachine()) return false;
            var result = false;
            try
            {
                // Gets the property qualifiers.
                var op = new ObjectGetOptions(null, TimeSpan.MaxValue, true);

                var path = new ManagementPath(className);
                using (var mc = new ManagementClass(namespaceScope, path, op))
                {
                    mc.Options.UseAmendedQualifiers = true;
                    var prop = mc.Properties[propName];
                    foreach (var dataObject in prop.Qualifiers)
                        if (dataObject.Name.Equals("write"))
                        {
                            result = true;
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

        public bool IsMethodExists(string className, string methodName)
        {
            if (!ConnectToLocalMachine()) return false;
            var result = false;
            try
            {
                var op = new ObjectGetOptions(null, TimeSpan.MaxValue, true);
                var path = new ManagementPath(className);
                using (var mc = new ManagementClass(namespaceScope, path, op))
                {
                    foreach (var md in mc.Methods)
                        if (string.Compare(md.Name, methodName, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            result = true;
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

        public static string GetPropertyValue(ManagementScope scope, string className, string propName)
        {
            if (scope == null)
                throw new ArgumentNullException(nameof(scope));
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

        public static void SetPropertyValue(ManagementScope scope, string className, string propName, string value)
        {
            if (scope == null)
                throw new ArgumentNullException(nameof(scope));
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

        public void EnumLocalMachineClasses()
        {
            if (!ConnectToLocalMachine()) return;
            var query = new ObjectQuery("select * from meta_class");
            using (var searcher = new ManagementObjectSearcher(namespaceScope, query))
            {
                foreach (ManagementClass wmiClass in searcher.Get())
                {
                    // skip WMI events
                    if (wmiClass.Derivation.Contains("__Event")) continue;
                    // skip classes in exclude list
                    var className = wmiClass["__CLASS"].ToString();
                    if (!className.StartsWith("Win32_", StringComparison.Ordinal)) continue;
                    // skip classes without qualifiers (ex.: Win32_Perf*)
                    if (wmiClass.Qualifiers.Count == 0) continue;
                    //if (m_ExcludeClasses.Contains(ClassName)) continue;
                    var info = new QualifiersInfo(wmiClass.Qualifiers);
                    if (info.IsAssociation || !info.IsDynamic || info.IsPerf) continue;
                    allClasses.Add(className);
                    var isInclude = includeClasses.Contains(className);
                    if (!isInclude && !info.IsSupportsModify) continue;
                    propCount += wmiClass.Properties.Count;
                    // count implemented methods
                    var foundMethod = WmiHelper.HasImplementedMethod(wmiClass);
                    if (foundMethod)
                        methodCount++;
                    if (!isInclude && !foundMethod)
                        continue;
                    if (foundMethod)
                        classes.Add(className);
                    else
                        readOnlyClasses.Add(className);
                }
            }
            allClasses.Sort();
            classes.Sort();
            readOnlyClasses.Sort();
            loaded = true;
        }
    }
}
