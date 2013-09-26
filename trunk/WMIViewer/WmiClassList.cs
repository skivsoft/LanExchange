using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Management;
using System.Text;

namespace WMIViewer
{
    /// <summary>
    /// List of used wmi classes.
    /// </summary>
    [Localizable(false)]
    public sealed class WmiClassList
    {
        private static WmiClassList s_Instance;
        private bool m_Loaded;
        private readonly List<string> m_Classes;
        private readonly List<string> m_ReadOnlyClasses;
        private readonly List<string> m_IncludeClasses;
        private readonly List<string> m_AllClasses;
        private int m_PropCount;
        private int m_MethodCount;
        private ManagementScope m_Namespace;

        private WmiClassList()
        {
            m_Classes = new List<string>();
            m_IncludeClasses = new List<string>();
            m_ReadOnlyClasses = new List<string>();
            m_AllClasses = new List<string>();
        }

        public static WmiClassList Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new WmiClassList();
                return s_Instance;
            }
        }

        public IList<string> Classes
        {
            get { return m_Classes; }
        }

        public IList<string> ReadOnlyClasses
        {
            get { return m_ReadOnlyClasses; }
        }

        public IList<string> IncludeClasses
        {
            get { return m_IncludeClasses; }
        }

        public IEnumerable<string> AllClasses
        {
            get { return m_AllClasses; }
        }

        public int ClassCount
        {
            get { return m_Classes.Count + m_ReadOnlyClasses.Count; }
        }

        public int PropCount
        {
            get { return m_PropCount; }
        }

        public int MethodCount
        {
            get { return m_MethodCount; }
        }

        public bool Loaded
        {
            get { return m_Loaded; }
        }

        private bool ConnectToLocalMachine()
        {
            if (m_Namespace != null && m_Namespace.IsConnected)
                return true;
            m_Namespace = new ManagementScope(CmdLineArgs.DefaultNamespaceName, null);
            try
            {
                m_Namespace.Connect();
            }
            catch (ManagementException ex)
            {
                Debug.Print(ex.Message);
            }
            return m_Namespace.IsConnected;
        }

        public string GetClassQualifiers(ManagementScope scope, string className)
        {
            if (scope == null)
            {
                if (!ConnectToLocalMachine()) return string.Empty;
                scope = m_Namespace;
            }
            var sb = new StringBuilder();
            try
            {
                // Gets the property qualifiers.
                var op = new ObjectGetOptions(null, TimeSpan.MaxValue, true);
                var path = new ManagementPath(className);
                using (var mc = new ManagementClass(scope, path, op))
                {
                    mc.Options.UseAmendedQualifiers = true;
                    foreach (var qd in mc.Qualifiers)
                    {
                        if (qd.Name.Equals("Description"))
                            continue;
                        sb.Append(qd.Name);
                        sb.Append("=");
                        sb.Append(qd.Value);
                        sb.Append(", ");
                    }
                }
            }
            catch(ManagementException ex)
            {
                Debug.Print(ex.Message);
            }

            return sb.ToString();
        }

        public string GetClassDescription(ManagementScope scope, string className)
        {
            if (scope == null)
            {
                if (!ConnectToLocalMachine()) return string.Empty;
                scope = m_Namespace;
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
                using (var mc = new ManagementClass(m_Namespace, path, op))
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
                using (var mc = new ManagementClass(m_Namespace, path, op))
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
                using (var mc = new ManagementClass(m_Namespace, path, op))
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
                throw new ArgumentNullException("scope");
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
                throw new ArgumentNullException("scope");
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
            using (var searcher = new ManagementObjectSearcher(m_Namespace, query))
            {
                foreach (ManagementClass wmiClass in searcher.Get())
                {
                    // skip WMI events
                    if (wmiClass.Derivation.Contains("__Event")) continue;
                    // skip classes in exclude list
                    string className = wmiClass["__CLASS"].ToString();
                    if (!className.StartsWith("Win32_", StringComparison.Ordinal)) continue;
                    // skip classes without qualifiers (ex.: Win32_Perf*)
                    if (wmiClass.Qualifiers.Count == 0) continue;
                    //if (m_ExcludeClasses.Contains(ClassName)) continue;
                    bool isDynamic = false;
                    //bool IsAbstract = false;
                    bool isPerf = false;
                    bool isAssociation = false;
                    bool isSupportsUpdate = false;
                    bool isSupportsCreate = false;
                    bool isSupportsDelete = false;
                    foreach (var qd in wmiClass.Qualifiers)
                    {
                        //if (qd.Name.Equals("Aggregation")) IsAggregation = true;
                        if (qd.Name.Equals("Association")) isAssociation = true;
                        if (qd.Name.Equals("dynamic")) isDynamic = true;
                        //if (qd.Name.Equals("abstract")) IsAbstract = true;
                        if (qd.Name.ToUpperInvariant().Equals("GENERICPERFCTR")) isPerf = true;
                        if (qd.Name.Equals("SupportsUpdate")) isSupportsUpdate = true;
                        if (qd.Name.Equals("SupportsCreate")) isSupportsCreate = true;
                        if (qd.Name.Equals("SupportsDelete")) isSupportsDelete = true;
                    }
                    if (isAssociation || !isDynamic || isPerf) continue;
                    m_AllClasses.Add(className);
                    bool isInclude = m_IncludeClasses.Contains(className);
                    if (isInclude || isSupportsUpdate || isSupportsCreate || isSupportsDelete)
                    {
                        m_PropCount += wmiClass.Properties.Count;
                        // count implemented methods
                        bool foundMethod = false;
                        foreach (MethodData md in wmiClass.Methods)
                            foreach (var qd in md.Qualifiers)
                                if (qd.Name.Equals("Implemented"))
                                {
                                    foundMethod = true;
                                    m_MethodCount++;
                                    break;
                                }
                        if (!isInclude && !foundMethod)
                            continue;
                        if (foundMethod)
                            m_Classes.Add(className);
                        else
                            m_ReadOnlyClasses.Add(className);
                    }
                }
            }
            m_AllClasses.Sort();
            m_Classes.Sort();
            m_ReadOnlyClasses.Sort();
            m_Loaded = true;
        }
    }
}
