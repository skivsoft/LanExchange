using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Management;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WMIViewer
{
    public sealed class WMIPresenter : IDisposable
    {
        private readonly IWmiComputer m_Comp;
        private ManagementScope m_Namespace;

        private readonly IWMIView m_View;
        private ManagementClass m_Class;

        public WMIPresenter(IWmiComputer comp, IWMIView view)
        {
            m_Comp = comp;
            m_View = view;
        }

        public void Dispose()
        {
            if (m_Class != null)
            {
                m_Class.Dispose();
                m_Class = null;
            }
        }

        [Localizable(false)]
        private string MakeConnectionString()
        {
            if (m_Comp == null || 
                String.Compare(m_Comp.Name, SystemInformation.ComputerName, StringComparison.OrdinalIgnoreCase) == 0)
                return WMISettings.DefaultNamespace;
            return String.Format(@"\\{0}\{1}", m_Comp.Name, WMISettings.DefaultNamespace);
        }

        private void ShowFirewallConnectionError()
        {
            MessageBox.Show(
                String.Format("Unable to connect to computer \\\\{0}.\nPossible connection has been blocked by firewall.", m_Comp.Name),
                "WMI connection error",
                MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
        }

        private void ShowCommonConnectionError(Exception ex)
        {
            MessageBox.Show(
                String.Format("Unable to connect to computer \\\\{0}.\n{1}", m_Comp.Name, ex.Message),
                "WMI connection error",
                MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
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
                    using (var form = new WMIAuthForm())
                    {
                        if (autoLogon)
                            WMIAuthForm.ClearSavedPassword();

                        form.SetComputerName(m_Comp.Name);
                        autoLogon = form.AutoLogon();
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
            catch (Exception ex)
            {
                ShowCommonConnectionError(ex);
            }
            m_Namespace = null;
            return false;
        }

        public ManagementClass WMIClass 
        {
            get { return m_Class; }
        }

        public ManagementObject WMIObject { get; set; }

        [Localizable(false)]
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
                foreach (var prop in m_Class.Properties)
                {
                    if (prop.Name.Equals("Name")) continue;
                    if (prop.Name.Equals("Caption")) continue;
                    if (prop.Name.Equals("Description")) continue;
                    if (prop.IsLocal) continue;
                    bool isCimKey = false;
                    foreach (var qd in prop.Qualifiers)
                        if (qd.Name.Equals("CIM_Key"))
                        {
                            isCimKey = true;
                            break;
                        }
                    if (isCimKey || prop.IsArray || !prop.Type.Equals(CimType.String)
                        //|| Prop.Type.Equals(CimType.Boolean) || Prop.Type.Equals(CimType.DateTime)
                        )
                        continue;
                    m_View.LV.Columns.Add(prop.Name);
                }
                bCheckError = false;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
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
                        var lvi = new ListViewItem {Tag = wmiObject};
                        int index = 0;
                        foreach (ColumnHeader header in m_View.LV.Columns)
                        {
                            PropertyData prop = wmiObject.Properties[header.Text];

                            string value = prop.Value == null ? "null" : prop.Value.ToString();
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
                        m_View.LV.Items.Add(lvi);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }
        }

        public void Method_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem == null) return;
            var md = menuItem.Tag as MethodData;
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

        [Localizable(false)]
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
                    var menuItem = new ToolStripMenuItem();
                    menuItem.Text = method.ToString();
                    menuItem.Tag = md;
                    menuItem.Click += Method_Click;
                    m_View.MENU.Items.Add(menuItem);
                }
            }
            catch (Exception ex)
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
