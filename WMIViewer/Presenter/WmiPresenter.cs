using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WMIViewer.Model;
using WMIViewer.Properties;
using WMIViewer.UI;

namespace WMIViewer.Presenter
{
    internal sealed class WmiPresenter : IDisposable
    {
        private const string NULL_TEXT = "null";
        private ManagementScope managementScope;

        private ManagementClass managementClass;

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

        public CmdLineArgs Args { get; }

        public MainForm View { get; set; }

        public ManagementClass WmiClass
        {
            get { return managementClass; }
        }

        public ManagementObject WmiObject { get; set; }

        public ManagementScope ManagementScope
        {
            get { return managementScope; }
        }

        public void Dispose()
        {
            if (managementClass != null)
            {
                managementClass.Dispose();
                managementClass = null;
            }
        }

        public bool ConnectToComputer()
        {
            if (managementScope != null && managementScope.IsConnected)
                return true;
            string connectionString = MakeConnectionString();
            ConnectionOptions options = null;
            bool autoLogon = false;

            TryAgainWithPassword:
            try
            {
                options = new ConnectionOptions();

                // options.Impersonation = true;
                managementScope = new ManagementScope(connectionString, options);
                managementScope.Options.EnablePrivileges = true;
                managementScope.Options.Impersonation = ImpersonationLevel.Impersonate;
                managementScope.Connect();
                if (managementScope.IsConnected)
                {
                    return true;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                if (options == null || string.IsNullOrEmpty(options.Username) || autoLogon)
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
                            ok = form.ShowDialog() == DialogResult.OK;
                        if (ok)
                        {
                            managementScope = null;
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

            managementScope = null;
            return false;
        }

        [Localizable(false)]
        public void EnumObjects(string className)
        {
            View.LV.Clear();

            var op = new ObjectGetOptions(null, TimeSpan.MaxValue, true);
            using (var mc = new ManagementClass(managementScope, new ManagementPath(className), op))
            {
                mc.Options.UseAmendedQualifiers = true;
                managementClass = mc;
            }

            if (!SetupColumns()) return;

            var query = new ObjectQuery("select * from " + className);
            using (var searcher = new ManagementObjectSearcher(managementScope, query))
            {
                try
                {
                    foreach (ManagementObject wmiObject in searcher.Get())
                    {
                        if (wmiObject == null) continue;
                        var lvi = new ListViewItem { Tag = wmiObject };

                       int index = 0;
                        foreach (ColumnHeader header in View.LV.Columns)
                        {
                            var prop = wmiObject.Properties[header.Text];

                            var value = prop.Value == null ? NULL_TEXT : prop.Value.ToString();
                            if (prop.Name.Equals("Name"))
                            {
                                string[] list = value.Split('|');
                                if (list.Length > 0)
                                    value = list[0];
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

        public void MethodOnClick(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem == null) return;
            var md = menuItem.Tag as MethodData;
            if (md == null) return;
            using (var form = new MethodForm(this))
            {
                form.WmiClass = managementClass;
                form.WmiObject = WmiObject;
                form.WmiMethod = md;
                form.PrepareForm();
                form.ShowDialog();
            }
        }

        [Localizable(false)]
        public void BuildContextMenu(ToolStripItemCollection items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            items.Clear();
            if (managementClass == null) return;
            try
            {
                foreach (MethodData md in managementClass.Methods)
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
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
        }

        private void ShowCommonConnectionError(Exception ex)
        {
            MessageBox.Show(
                string.Format(CultureInfo.InvariantCulture, Resources.WmiPresenter_ShowCommonConnectionError, Args.ComputerName, ex.Message),
                Resources.WmiPresenter_ConnectionError_Caption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
        }

        private bool SetupColumns()
        {
            const string WMI_NAME = "Name";
            const string WMI_CAPTION = "Caption";
            const string WMI_DESCRIPTION = "DESCRIPTION";
            const string WMI_CIM_KEY = "CIM_Key";
            bool checkError = true;
            try
            {
                foreach (var prop in managementClass.Properties)
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

                    if (isCimKey || prop.IsArray || !prop.Type.Equals(CimType.String))
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
    }
}