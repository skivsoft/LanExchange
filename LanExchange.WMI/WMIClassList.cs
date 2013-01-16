using System;
using System.Collections.Generic;
using System.Management;
using NLog;

namespace LanExchange.WMI
{
    /// <summary>
    /// List of used wmi classes.
    /// </summary>
    public class WMIClassList
    {
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();

        private static WMIClassList m_Instance;
        private bool m_Loaded;
        private readonly List<string> m_Classes;
        private readonly List<string> m_ReadOnlyClasses;
        //private readonly List<string> m_ExcludeClasses;
        private readonly List<string> m_IncludeClasses;
        private int m_PropCount;
        private int m_MethodCount;
        private ManagementScope m_Namespace;

        protected WMIClassList()
        {
            m_Classes = new List<string>();
            m_IncludeClasses = new List<string>();
            m_ReadOnlyClasses = new List<string>();
            //m_ExcludeClasses = new List<string>();
            //m_ExcludeClasses.Add("Win32_Registry");
            //m_ExcludeClasses.Add("Win32_NTEventlogFile");
        }

        public static WMIClassList Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new WMIClassList();
                return m_Instance;
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
            const string connectionString = WMISettings.DefaultNamespace;
            logger.Info("WMI: connect to namespace \"{0}\"", connectionString);
            m_Namespace = new ManagementScope(connectionString, null);
            try
            {
                m_Namespace.Connect();
            }
            catch (Exception E)
            {
                logger.Error("WMI: {0}", E.Message);
            }
            if (!m_Namespace.IsConnected)
            {
                logger.Info("WMI: Not connected.");
                return false;
            }
            logger.Info("WMI: Connected.");
            return true;
        }

        public string GetClassDescription(ManagementScope scope, string className)
        {
            if (scope == null)
            {
                if (!ConnectToLocalMachine()) return string.Empty;
                scope = m_Namespace;
            }
            string Result = "";
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
                            Result = dataObject.Value.ToString();
                            break;
                        }
                }
            }
            catch { }
            return Result;

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
                    if (wmiClass.Derivation.Contains("__Event"))
                        continue;
                    // skip classes in exclude list
                    string ClassName = wmiClass["__CLASS"].ToString();
                    //if (m_ExcludeClasses.Contains(ClassName)) continue;
                    bool IsDynamic = false;
                    bool IsAssociation = false;
                    //bool IsAggregation = false;
                    bool IsSupportsUpdate = false;
                    bool IsSupportsCreate = false;
                    bool IsSupportsDelete = false;
                    foreach (var qd in wmiClass.Qualifiers)
                    {
                        //if (qd.Name.Equals("Aggregation")) IsAggregation = true;
                        if (qd.Name.Equals("Association")) IsAssociation = true;
                        if (qd.Name.Equals("dynamic")) IsDynamic = true;
                        if (qd.Name.Equals("SupportsUpdate")) IsSupportsUpdate = true;
                        if (qd.Name.Equals("SupportsCreate")) IsSupportsCreate = true;
                        if (qd.Name.Equals("SupportsDelete")) IsSupportsDelete = true;
                    }
                    if (IsAssociation || !IsDynamic) continue;
                    bool bInclude = m_IncludeClasses.Contains(ClassName);
                    if (bInclude || IsSupportsUpdate || IsSupportsCreate || IsSupportsDelete)
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
                        if (!bInclude && !foundMethod)
                            continue;
                        if (foundMethod)
                            m_Classes.Add(ClassName);
                        else
                            m_ReadOnlyClasses.Add(ClassName);
                    }
                }
            }
            m_Classes.Sort();
            m_ReadOnlyClasses.Sort();
            m_Loaded = true;
        }
    }
}
