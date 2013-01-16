using System;
using System.Management;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using NLog;

namespace LanExchange.WMI
{
    public class WMIPresenter : IDisposable
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

        public virtual void Dispose()
        {
            if (m_Class != null)
            {
                m_Class.Dispose();
                m_Class = null;
            }
        }

        private string MakeConnectionString()
        {
            if (m_Comp == null || 
                String.Compare(m_Comp.Name, SystemInformation.ComputerName, StringComparison.OrdinalIgnoreCase) == 0)
                return WMISettings.DefaultNamespace;
            return String.Format(@"\\{0}\{1}", m_Comp.Name, WMISettings.DefaultNamespace);
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
            mc.Dispose();

            bool bCheckError = true;
            try
            {
                m_View.LV.Columns.Add("Name");
                m_View.LV.Columns.Add("Caption");
                foreach (var Prop in m_Class.Properties)
                {
                    if (Prop.Name.Equals("Name")) continue;
                    if (Prop.Name.Equals("Caption")) continue;
                    if (Prop.Name.Equals("Description")) continue;
                    if (Prop.IsLocal) continue;
                    bool isCimKey = false;
                    foreach (var qd in Prop.Qualifiers)
                        if (qd.Name.Equals("CIM_Key"))
                        {
                            isCimKey = true;
                            break;
                        }
                    if (isCimKey || Prop.IsArray || !Prop.Type.Equals(CimType.String)
                        //|| Prop.Type.Equals(CimType.Boolean) || Prop.Type.Equals(CimType.DateTime)
                        )
                        continue;
                    m_View.LV.Columns.Add(Prop.Name);
                }
                bCheckError = false;
            }
            catch (Exception E)
            {
                logger.Error("WMI: {0}", E.Message);
            }
            if (bCheckError) return;

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
            if (m_Class == null) return;
            try
            {
                foreach (MethodData md in m_Class.Methods)
                {
                    var method = new MethodDataEx(md);
                    if (!method.HasQualifier("Implemented")) continue;
                    var MI = new ToolStripMenuItem();
                    MI.Text = method.ToString();
                    MI.Tag = md;
                    MI.Click += Method_Click;
                    m_View.MENU.Items.Add(MI);
                }
            }
            catch (Exception E)
            {
                logger.Error("WMI: {0}", E.Message);
            }
        }

        public ManagementScope Namespace
        {
            get { return m_Namespace; }
        }
    }
}
