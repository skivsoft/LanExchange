using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Management;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WMIViewer.Properties;

namespace WMIViewer
{
    public sealed class WmiPresenter : IDisposable
    {
        private const string NULL = "null";
        private ManagementScope m_Namespace;

        private ManagementClass m_Class;

        [Localizable(false)]
        public WmiPresenter(CmdLineArgs args)
        {
            Args = args;
            WmiClassList.Instance.IncludeClasses.Add("Win32_Desktop");
            WmiClassList.Instance.IncludeClasses.Add("Win32_DesktopMonitor");
            WmiClassList.Instance.IncludeClasses.Add("Win32_DiskDrive");
            WmiClassList.Instance.IncludeClasses.Add("Win32_BIOS");
            WmiClassList.Instance.IncludeClasses.Add("Win32_Processor");
            WmiClassList.Instance.IncludeClasses.Add("Win32_PhysicalMemory");
        }

        public CmdLineArgs Args { get; private set; }
        public MainForm View { get; set; }

        public void Dispose()
        {
            //if (m_Class != null)
            //{
            //    m_Class.Dispose();
            //    m_Class = null;
            //}
        }

        [Localizable(false)]
        private string MakeConnectionString()
        {
            if (string.Compare(Args.ComputerName, SystemInformation.ComputerName, StringComparison.OrdinalIgnoreCase) == 0)
                return Args.NamespaceName;
            return string.Format(CultureInfo.InvariantCulture, @"\\{0}\{1}", Args.ComputerName, Args.NamespaceName);
        }

        private void ShowFirewallConnectionError()
        {
            MessageBox.Show(
                string.Format(CultureInfo.InvariantCulture, Resources.WmiPresenter_ShowFirewallConnectionError, Args.ComputerName),
                Resources.WmiPresenter_ConnectionError_Caption,
                MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, RightToLeft.Options);
        }

        private void ShowCommonConnectionError(Exception ex)
        {
            MessageBox.Show(
                string.Format(CultureInfo.InvariantCulture, Resources.WmiPresenter_ShowCommonConnectionError, Args.ComputerName, ex.Message),
                Resources.WmiPresenter_ConnectionError_Caption,
                MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, RightToLeft.Options);
        }

        public bool ConnectToComputer()
        {
            if (m_Namespace != null && m_Namespace.IsConnected)
                return true;
            string connectionString = MakeConnectionString();
            ConnectionOptions options = null;
            bool autoLogon = false;

            TryAgainWithPassword:
            try
            {
                options = new ConnectionOptions();
                //options.Impersonation = true;
                m_Namespace = new ManagementScope(connectionString, options);
                m_Namespace.Options.EnablePrivileges = true;
                m_Namespace.Options.Impersonation = ImpersonationLevel.Impersonate;
                m_Namespace.Connect();
                if (m_Namespace.IsConnected)
                {
                    return true;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                if (options == null || String.IsNullOrEmpty(options.Username) || autoLogon)
                    using (var form = new AuthForm())
                    {
                        if (autoLogon)
                            AuthForm.ClearSavedPassword();

                        form.SetComputerName(Args.ComputerName);
                        autoLogon = form.AutoLogOn();
                        bool ok;
                        if (autoLogon)
                            ok = true;
                        else
                            ok = (form.ShowDialog() == DialogResult.OK);
                        if (ok)
                        {
                            m_Namespace = null;
                            options = new ConnectionOptions
                                {
                                    Username = form.UserName, 
                                    Password = form.UserPassword
                                };
                            goto TryAgainWithPassword;
                        }
                    }
                else
                    ShowCommonConnectionError(ex);
            }
            catch (COMException ex)
            {
                if ((uint)ex.ErrorCode == 0x800706BA)
                    ShowFirewallConnectionError();
                else
                    ShowCommonConnectionError(ex);
            }
            //catch (Exception ex)
            //{
            //    ShowCommonConnectionError(ex);
            //}
            m_Namespace = null;
            return false;
        }

        public ManagementClass WmiClass 
        {
            get { return m_Class; }
        }

        public ManagementObject WmiObject { get; set; }

        [Localizable(false)]
        public void EnumObjects(string className)
        {
            View.LV.Clear();

            var op = new ObjectGetOptions(null, TimeSpan.MaxValue, true);
            using (var mc = new ManagementClass(m_Namespace, new ManagementPath(className), op))
            {
                mc.Options.UseAmendedQualifiers = true;
                m_Class = mc;
            }
            if (!SetupColumns()) return;

            var query = new ObjectQuery("select * from " + className);
            using (var searcher = new ManagementObjectSearcher(m_Namespace, query))
            {
                try
                {
                    foreach (ManagementObject wmiObject in searcher.Get())
                    {
                        if (wmiObject == null) continue;
                        var lvi = new ListViewItem {Tag = wmiObject};
                        int index = 0;
                        foreach (ColumnHeader header in View.LV.Columns)
                        {
                            PropertyData prop = wmiObject.Properties[header.Text];

                            string value = prop.Value == null ? NULL : prop.Value.ToString();
                            if (prop.Name.Equals("Name"))
                            {
                                string[] sList = value.Split('|');
                                if (sList.Length > 0)
                                    value = sList[0];
                            }
                            if (index == 0)
                                lvi.Text = value;
                            else
                                lvi.SubItems.Add(value);
                            index++;
                        }
                        View.LV.Items.Add(lvi);
                    }
                }
                catch (ManagementException ex)
                {
                    Debug.Print(ex.Message);
                }
            }
        }

        private bool SetupColumns()
        {
            const string WMI_NAME        = "Name";
            const string WMI_CAPTION     = "Caption";
            const string WMI_DESCRIPTION = "DESCRIPTION";
            const string WMI_CIM_KEY     = "CIM_Key";
            bool checkError = true;
            try
            {
                foreach (var prop in m_Class.Properties)
                {
                    if (prop.Name.Equals(WMI_NAME) || prop.Name.Equals(WMI_CAPTION))
                    {
                        View.LV.Columns.Insert(0, prop.Name);
                        continue;
                    }
                    if (prop.Name.Equals(WMI_DESCRIPTION)) continue;
                    if (prop.IsLocal) continue;
                    bool isCimKey = false;
                    foreach (var qd in prop.Qualifiers)
                        if (qd.Name.Equals(WMI_CIM_KEY))
                        {
                            isCimKey = true;
                            break;
                        }
                    if (isCimKey || prop.IsArray || !prop.Type.Equals(CimType.String)
                        //|| Prop.Type.Equals(CimType.Boolean) || Prop.Type.Equals(CimType.DateTime)
                        )
                        continue;
                    View.LV.Columns.Add(prop.Name);
                }
                checkError = false;
            }
            catch (ManagementException ex)
            {
                Debug.Print(ex.Message);
            }
            return !checkError;
        }

        public void MethodOnClick(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem == null) return;
            var md = menuItem.Tag as MethodData;
            if (md == null) return;
            using (var form = new MethodForm(this))
            {
                form.WmiClass = m_Class;
                form.WmiObject = WmiObject;
                form.WmiMethod = md;
                form.PrepareForm();
                form.ShowDialog();
            }
        }

        [Localizable(false)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public void BuildContextMenu(ToolStripItemCollection items)
        {
            if (items == null)
                throw new ArgumentNullException("items");
            items.Clear();
            if (m_Class == null) return;
            try
            {
                foreach (MethodData md in m_Class.Methods)
                {
                    var method = new MethodDataExt(md);
                    if (!method.HasQualifier("Implemented")) continue;
                    var menuItem = new ToolStripMenuItem();
                    menuItem.Text = method.ToString();
                    menuItem.Tag = md;
                    menuItem.Click += MethodOnClick;
                    items.Add(menuItem);
                }
            }
            catch (ManagementException ex)
            {
                Debug.Print(ex.Message);
            }
        }

        public ManagementScope Namespace
        {
            get { return m_Namespace; }
        }

    }
}
