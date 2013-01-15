using System;
using System.Management;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NLog;

namespace LanExchange.WMI
{
    public class WMIPresenter
    {
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IWMIComputer m_Comp;
        private ConnectionOptions m_Connection;
        private ManagementScope m_Scope;

        private readonly IWMIView m_View;
        private ManagementClass m_Class;
        private readonly List<string> m_ExcludeClasses;

        public WMIPresenter(IWMIComputer comp, IWMIView view)
        {
            m_Comp = comp;
            m_View = view;
            m_ExcludeClasses = new List<string>();
            m_ExcludeClasses.Add("Win32_Registry");
            m_ExcludeClasses.Add("Win32_NTEventlogFile");
        }

        private string MakeConnectionString()
        {
            if (m_Comp == null || 
                String.Compare(m_Comp.Name, SystemInformation.ComputerName, StringComparison.OrdinalIgnoreCase) == 0)
                return @"root\cimv2";
            return String.Format(@"\\{0}\root\cimv2", m_Comp.Name);
        }

        private void ConnectToComputer()
        {
            if (m_Scope != null && m_Scope.IsConnected)
                return;
            try
            {
                string ConnectionString = MakeConnectionString();
                logger.Info("WMI: connect to namespace \"{0}\"", ConnectionString);
                m_Connection = new ConnectionOptions();
                m_Scope = new ManagementScope(ConnectionString, m_Connection);
                m_Scope.Options.EnablePrivileges = true;
                m_Scope.Connect();
                if (m_Scope.IsConnected)
                    logger.Info("WMI: Connected.");
                else
                    logger.Error("WMI: Not connected.");
            }
            catch (COMException ex)
            {
                if ((uint)ex.ErrorCode == 0x800706BA)
                    MessageBox.Show(String.Format(
                        "Не удалось поделючиться к компьютеру \\\\{0}.\n" +
                        "Возможно удаленное подключение было заблокировано брэнмауэром.", m_Comp.Name), "Ошибка подключения",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка подключения",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
            }

        }

        public string GetClassDescription(string className)
        {
            string Result = "";
            try
            {
                // Gets the property qualifiers.
                var op = new ObjectGetOptions(null, TimeSpan.MaxValue, true);

                var path = new ManagementPath(className);
                using (var mc = new ManagementClass(m_Scope, path, op))
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
            catch {}
            return Result;

        }

        public bool EnumDynamicClasses()
        {
            ConnectToComputer();

            int ClassCount = 0;
            int PropCount = 0;
            int MethodCount = 0;
            m_View.ClearClasses();
            bool Result = true;
            var query = new ObjectQuery("select * from meta_class");
            using (var searcher = new ManagementObjectSearcher(m_Scope, query))
            {
                try
                {
                    foreach (ManagementClass wmiClass in searcher.Get())
                    {
                        // skip WMI events
                        if (wmiClass.Derivation.Contains("__Event"))
                            continue;
                        // skip classes in exclude list
                        string ClassName = wmiClass["__CLASS"].ToString();
                        if (m_ExcludeClasses.Contains(ClassName))
                            continue;
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
                            m_View.AddClass(ClassName);
                            ClassCount++;
                            PropCount += wmiClass.Properties.Count;
                            // count implemented methods
                            foreach (MethodData md in wmiClass.Methods)
                                foreach (var qd in md.Qualifiers)
                                    if (qd.Name.Equals("Implemented"))
                                    {
                                        MethodCount++;
                                        break;
                                    }
                        }
                    }
                }
                catch
                {
                    Result = false;
                }
            }
            m_View.ShowStat(ClassCount, PropCount, MethodCount);
            return Result;
        }

        public ManagementClass WMIClass 
        {
            get { return m_Class; }
        }

        public ManagementObject WMIObject { get; set; }

        public void EnumObjects(string className)
        {
            ConnectToComputer();

            m_View.LV.Clear();


            var op = new ObjectGetOptions(null, TimeSpan.MaxValue, true);
            var mc = new ManagementClass(m_Scope, new ManagementPath(className), op);
            mc.Options.UseAmendedQualifiers = true;

            m_Class = mc;

            foreach (var Prop in m_Class.Properties)
            {
                bool isCimKey = false;
                foreach (var qd in Prop.Qualifiers)
                    if (qd.Name.Equals("CIM_Key"))
                    {
                        isCimKey = true;
                        break;
                    }
                if (isCimKey || Prop.IsArray || Prop.Type.Equals(CimType.Boolean) || Prop.Type.Equals(CimType.DateTime))
                    continue;
                m_View.LV.Columns.Add(Prop.Name);
            }

            var query = new ObjectQuery("select * from " + className);
            using (var searcher = new ManagementObjectSearcher(m_Scope, query))
            {
                try
                {
                    foreach (ManagementObject wmiObject in searcher.Get())
                    {
                        if (wmiObject == null) continue;
                        ListViewItem LVI = new ListViewItem { Tag = wmiObject };
                        int Index = 0;
                        foreach (ColumnHeader Header in m_View.LV.Columns)
                        {
                            PropertyData Prop = wmiObject.Properties[Header.Text];

                            string Value = Prop.Value == null ? "null" : Prop.Value.ToString();
                            if (Index == 0)
                                LVI.Text = Value;
                            else
                                LVI.SubItems.Add(Value);
                            Index++;
                        }
                        m_View.LV.Items.Add(LVI);
                    }
                }
                catch { }
            }
        }

        public void Method_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem == null) return;
            MethodData md = menuItem.Tag as MethodData;
            if (md == null) return;
            using (var form = new WMIMethodForm())
            {
                form.WMIClass = m_Class;
                form.WMIObject = WMIObject;
                form.WMIMethod = md;
                form.PrepareForm();
                form.ShowDialog();
            }
        }

        public void BuildContextMenu()
        {
            m_View.MENU.Items.Clear();
            foreach (MethodData md in m_Class.Methods)
            {
                bool IsImplemented = false;
                foreach(var qd in md.Qualifiers)
                    if (qd.Name.Equals("Implemented"))
                    {
                        IsImplemented = true;
                        break;
                    }
                if (!IsImplemented) continue;
                ToolStripMenuItem MI = new ToolStripMenuItem();
                MI.Text = string.Format("{0}()", md.Name);
                MI.Tag = md;
                MI.Click += Method_Click;
                m_View.MENU.Items.Add(MI);
            }
        }
    }
}
