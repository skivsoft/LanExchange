using System;
using System.Management;
using System.Threading;
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
        private ManagementScope m_Namespace;

        private readonly IWMIView m_View;
        private ManagementClass m_Class;

        public WMIPresenter(IWMIComputer comp, IWMIView view)
        {
            m_Comp = comp;
            m_View = view;
        }

        private string MakeConnectionString()
        {
            if (m_Comp == null || 
                String.Compare(m_Comp.Name, SystemInformation.ComputerName, StringComparison.OrdinalIgnoreCase) == 0)
                return @"root\cimv2";
            return String.Format(@"\\{0}\root\cimv2", m_Comp.Name);
        }

        public bool ConnectToComputer()
        {
            if (m_Namespace != null && m_Namespace.IsConnected)
                return true;
            try
            {
                string connectionString = MakeConnectionString();
                logger.Info("WMI: connect to namespace \"{0}\"", connectionString);
                //var connectionOptions = new ConnectionOptions();
                m_Namespace = new ManagementScope(connectionString, null);
                //m_Scope.Options.EnablePrivileges = true;
                m_Namespace.Connect();
                if (m_Namespace.IsConnected)
                {
                    logger.Info("WMI: Connected.");
                    return true;
                }
            }
            catch (COMException ex)
            {
                if ((uint)ex.ErrorCode == 0x800706BA)
                    MessageBox.Show(
                        String.Format("Не удалось подключиться к компьютеру \\\\{0}.\nВозможно удалённое подключение было заблокировано брэнмауэром.", m_Comp.Name), 
                        "Ошибка подключения WMI",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    String.Format("Не удалось подключиться к компьютеру \\\\{0}.\n{1}", m_Comp.Name, ex.Message),
                    "Ошибка подключения WMI",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
            }
            logger.Error("WMI: Not connected.");
            m_Namespace = null;
            return false;
        }

        public string GetClassDescription(string className)
        {
            string Result = "";
            try
            {
                // Gets the property qualifiers.
                var op = new ObjectGetOptions(null, TimeSpan.MaxValue, true);

                var path = new ManagementPath(className);
                using (var mc = new ManagementClass(m_Namespace, path, op))
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

        //public bool EnumDynamicClasses()
        //{
        //    ConnectToComputer();

        //    int ClassCount = 0;
        //    int PropCount = 0;
        //    int MethodCount = 0;
        //    m_View.ClearClasses();
        //    bool Result = true;
        //    m_View.ShowStat(ClassCount, PropCount, MethodCount);
        //    return Result;
        //}

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
            var mc = new ManagementClass(m_Namespace, new ManagementPath(className), op);
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
            using (var searcher = new ManagementObjectSearcher(m_Namespace, query))
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
