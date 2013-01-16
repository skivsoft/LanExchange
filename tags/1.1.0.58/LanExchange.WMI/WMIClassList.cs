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
        //private readonly List<string> m_ReadOnlyClasses;
        //private readonly List<string> m_ExcludeClasses;
        private int m_PropCount;
        private int m_MethodCount;

        protected WMIClassList()
        {
            m_Classes = new List<string>();
            //m_ReadOnlyClasses = new List<string>();
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

        public IEnumerable<string> Classes
        {
            get { return m_Classes; }
        }

        //public IEnumerable<string> ReadOnlyClasses
        //{
        //    get { return m_ReadOnlyClasses; }
        //}

        public int ClassCount
        {
            get { return m_Classes.Count; }
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

        public void EnumLocalMachineClasses()
        {
            const string connectionString = @"root\cimv2";
            logger.Info("WMI: connect to namespace \"{0}\"", connectionString);
            var scope = new ManagementScope(connectionString, null);
            try
            {
                scope.Connect();
            }
            catch(Exception E)
            {
                logger.Error("WMI: {0}", E.Message);
            }
            if (!scope.IsConnected)
            {
                logger.Info("WMI: Not connected.");
                return;
            }
            logger.Info("WMI: Connected.");
            var query = new ObjectQuery("select * from meta_class");
            using (var searcher = new ManagementObjectSearcher(scope, query))
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
                    if (!IsAssociation && IsDynamic && (IsSupportsUpdate || IsSupportsCreate || IsSupportsDelete))
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
                        if (foundMethod)
                            m_Classes.Add(ClassName);
                        //else
                        //    m_ReadOnlyClasses.Add(ClassName);
                    }
                }
            }
            m_Classes.Sort();
            //m_ReadOnlyClasses.Sort();
            m_Loaded = true;
        }
    }
}
