using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Management;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WMIViewer.Model;
using WMIViewer.Properties;
using WMIViewer.UI;

namespace WMIViewer.Presenter
{
    internal sealed class WmiPresenter : IDisposable
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
            if (String.Compare(Args.ComputerName, SystemInformation.ComputerName, StringComparison.OrdinalIgnoreCase) == 0)
                return Args.NamespaceName;
            return String.Format(CultureInfo.InvariantCulture, @"\\{0}\{1}", Args.ComputerName, Args.NamespaceName);
        }

        private void ShowFirewallConnectionError()
        {
            MessageBox.Show(
                String.Format(CultureInfo.InvariantCulture, Resources.WmiPresenter_ShowFirewallConnectionError, Args.ComputerName),
                Resources.WmiPresenter_ConnectionError_Caption,
                MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
        }

        private void ShowCommonConnectionError(Exception ex)
        {
            MessageBox.Show(
                String.Format(CultureInfo.InvariantCulture, Resources.WmiPresenter_ShowCommonConnectionError, Args.ComputerName, ex.Message),
                Resources.WmiPresenter_ConnectionError_Caption,
                MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
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
                            var prop = wmiObject.Properties[header.Text];

                            var value = prop.Value == null ? NULL : prop.Value.ToString();
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
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
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


        private static void DynObjAddProperty<T>(DynamicObject dynamicObj, PropertyData prop, string description, string category, bool isReadOnly)
        {
            if (prop.Value == null)
                dynamicObj.AddPropertyNull<T>(prop.Name, prop.Name, description, category, isReadOnly, null);
            else
                if (prop.IsArray)
                    dynamicObj.AddProperty(prop.Name, (T[])prop.Value, description, category, isReadOnly);
                else
                    dynamicObj.AddProperty(prop.Name, (T)prop.Value, description, category, isReadOnly);
        }

        [Localizable(false)]
        internal object CreateDynamicObject()
        {
            var dynObj = new DynamicObject();
            foreach (var prop in WmiObject.Properties)
            {
                // skip array of bytes
                if (prop.Type == CimType.UInt8 && prop.IsArray)
                    continue;

                var classProp = WmiClass.Properties[prop.Name];
                var info = new QualifiersInfo(classProp.Qualifiers);
                if (info.IsCimKey) continue;
                var category = prop.Type.ToString();
                var description = info.Description;
                var isReadOnly = info.IsReadOnly;
                switch (prop.Type)
                {

                    //     A signed 16-bit integer. This value maps to the System.Int16 type.
                    case CimType.SInt16:
                        DynObjAddProperty<Int16>(dynObj, prop, description, category, isReadOnly);
                        break;
                    //     A signed 32-bit integer. This value maps to the System.Int32 type.
                    case CimType.SInt32:
                        DynObjAddProperty<Int32>(dynObj, prop, description, category, isReadOnly);
                        break;
                    //     A floating-point 32-bit number. This value maps to the System.Single type.
                    case CimType.Real32:
                        DynObjAddProperty<Single>(dynObj, prop, description, category, isReadOnly);
                        break;
                    //     A floating point 64-bit number. This value maps to the System.Double type.
                    case CimType.Real64:
                        DynObjAddProperty<Double>(dynObj, prop, description, category, isReadOnly);
                        break;
                    //     A string. This value maps to the System.String type.
                    case CimType.String:
                        DynObjAddProperty<String>(dynObj, prop, description, category, isReadOnly);
                        break;
                    //     A Boolean. This value maps to the System.Boolean type.
                    case CimType.Boolean:
                        DynObjAddProperty<Boolean>(dynObj, prop, description, category, isReadOnly);
                        break;
                    //     An embedded object. Note that embedded objects differ from references in
                    //     that the embedded object does not have a path and its lifetime is identical
                    //     to the lifetime of the containing object. This value maps to the System.Object
                    //     type.
                    case CimType.Object:
                        DynObjAddProperty<Object>(dynObj, prop, description, category, isReadOnly);
                        break;
                    //     A signed 8-bit integer. This value maps to the System.SByte type.
                    case CimType.SInt8:
                        DynObjAddProperty<SByte>(dynObj, prop, description, category, isReadOnly);
                        break;
                    //     An unsigned 8-bit integer. This value maps to the System.Byte type.
                    case CimType.UInt8:
                        DynObjAddProperty<Byte>(dynObj, prop, description, category, isReadOnly);
                        break;
                    //     An unsigned 16-bit integer. This value maps to the System.UInt16 type.
                    case CimType.UInt16:
                        DynObjAddProperty<UInt16>(dynObj, prop, description, category, isReadOnly);
                        break;
                    //     An unsigned 32-bit integer. This value maps to the System.UInt32 type.
                    case CimType.UInt32:
                        DynObjAddProperty<UInt32>(dynObj, prop, description, category, isReadOnly);
                        break;
                    //     A signed 64-bit integer. This value maps to the System.Int64 type.
                    case CimType.SInt64:
                        DynObjAddProperty<Int64>(dynObj, prop, description, category, isReadOnly);
                        break;
                    //     An unsigned 64-bit integer. This value maps to the System.UInt64 type.
                    case CimType.UInt64:
                        DynObjAddProperty<UInt64>(dynObj, prop, description, category, isReadOnly);
                        break;
                    //     A date or time value, represented in a string in DMTF date/time format: yyyymmddHHMMSS.mmmmmmsUUU,
                    //     where yyyymmdd is the date in year/month/day; HHMMSS is the time in hours/minutes/seconds;
                    //     mmmmmm is the number of microseconds in 6 digits; and sUUU is a sign (+ or
                    //     -) and a 3-digit UTC offset. This value maps to the System.DateTime type.
                    case CimType.DateTime:
                        if (prop.Value == null)
                            dynObj.AddPropertyNull<DateTime>(prop.Name, prop.Name, description, category, isReadOnly, null);
                        else
                            dynObj.AddProperty(prop.Name, WmiHelper.ToDateTime(prop.Value.ToString()), description, category, isReadOnly);
                        break;
                    //     A reference to another object. This is represented by a string containing
                    //     the path to the referenced object. This value maps to the System.Int16 type.
                    case CimType.Reference:
                        DynObjAddProperty<Int16>(dynObj, prop, description, category, isReadOnly);
                        break;
                    //     A 16-bit character. This value maps to the System.Char type.
                    case CimType.Char16:
                        DynObjAddProperty<Char>(dynObj, prop, description, category, isReadOnly);
                        break;
                    default:
                        string value = prop.Value == null ? null : prop.Value.ToString();
                        dynObj.AddProperty(String.Format(CultureInfo.InvariantCulture, "{0} : {1}", prop.Name, prop.Type), value, description, "Unknown", isReadOnly);
                        break;
                }
            }
            return dynObj;
        }
    }
}
