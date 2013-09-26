using System;
using System.Drawing;
using System.Globalization;
using System.Management;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using WMIViewer.Properties;

namespace WMIViewer
{
    public sealed partial class WmiMethodForm : Form
    {
        private readonly WmiPresenter m_Presenter;
        private readonly WmiArgs m_Args;
        private bool m_OutParamPresent;

        public WmiMethodForm(WmiPresenter presenter)
        {
            if (presenter == null)
                throw new ArgumentNullException("presenter");
            m_Presenter = presenter;
            m_Args = m_Presenter.Args;
            InitializeComponent();
            Icon = Resources.WMIViewer16;
        }

        public ManagementClass WmiClass { get; set; }
        public ManagementObject WmiObject { get; set; }
        public MethodData WmiMethod { get; set; }

        [Localizable(false)]
        private static string FormatQualifierValue(object value)
        {
            var list = value as string[];
            if (list != null)
            {
                var result = string.Empty;
                for (int i = 0; i < list.Length; i++)
                {
                    if (i > 0) result += ", ";
                    result += list[i];
                }
                return result;
            }
            return value.ToString();
        }

        private string m_ReturnValueName;

        [Localizable(false)]
        private void PrepareArgs()
        {
            if (WmiClass == null)
            {
                var op = new ObjectGetOptions(null, TimeSpan.MaxValue, true);
                var path = new ManagementPath(m_Args.ClassName);
                WmiClass = new ManagementClass(m_Presenter.Namespace, path, op);
            }
            if (WmiMethod == null)
            {
                foreach(var md in WmiClass.Methods)
                {
                    if (string.Compare(md.Name, m_Args.MethodName, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        WmiMethod = md;
                        break;
                    }
                }
            }
            if (WmiObject == null)
            {
                m_Presenter.Namespace.Connect();
                var query = new ObjectQuery("SELECT * FROM " + m_Args.ClassName);
                using (var searcher = new ManagementObjectSearcher(m_Presenter.Namespace, query))
                {
                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        WmiObject = queryObj;
                        break;
                    }
                }
            }
        }

        [Localizable(false)]
        public void PrepareForm()
        {
            PrepareArgs();
            Text = String.Format(CultureInfo.InvariantCulture, Text, m_Args.NamespaceName, WmiClass.Path.ClassName, WmiMethod.Name);
            lMethodName.Text = WmiMethod.Name;
            foreach (var qd in WmiMethod.Qualifiers)
            {
                if (qd.Name.Equals("Description"))
                {
                    lDescription.Text = qd.Value.ToString();
                    break;
                }
            }
            // output method qualifiers
            string str = String.Format(CultureInfo.InvariantCulture, "Class={0}, Method={1}", WmiClass.Path.ClassName, WmiMethod.Name);
            LB.Items.Add(str);
            foreach (var qd in WmiMethod.Qualifiers)
            {
                if (qd.Name.Equals("Description") || qd.Name.Equals("Implemented"))
                    continue;
                str = String.Format(CultureInfo.InvariantCulture, "{0}={1}", qd.Name, FormatQualifierValue(qd.Value));
                LB.Items.Add(str);
            }
            LB.Items.Add(string.Empty);
            // prepare properties list and sort it by ID
            var propList = new List<PropertyDataExt>();
            if (WmiMethod.InParameters != null)
                foreach (PropertyData pd in WmiMethod.InParameters.Properties)
                    propList.Add(new PropertyDataExt(pd));
            if (WmiMethod.OutParameters != null)
                foreach (PropertyData pd in WmiMethod.OutParameters.Properties)
                    propList.Add(new PropertyDataExt(pd));
            propList.Sort();
            // output properties
            var excludeList = new List<string>();
            excludeList.Add("CIMTYPE");
            excludeList.Add("ID");
            excludeList.Add("In");
            excludeList.Add("Out");
            excludeList.Add("in");
            excludeList.Add("out");
            foreach (PropertyDataExt prop in propList)
            {
                // detect return value parameter name
                if (prop.ParameterType == WmiParameterType.Return)
                    m_ReturnValueName = prop.Name;
                if (prop.ParameterType == WmiParameterType.Out)
                    m_OutParamPresent = true;
                str = String.Format(CultureInfo.InvariantCulture, "{0} {1} {2} : {3}", prop.Id, prop.ParameterType, prop.Name, prop.PropType);
                LB.Items.Add(str);
                foreach (var qd in prop.Qualifiers)
                    if (!excludeList.Contains(qd.Name))
                    {
                        str = String.Format(CultureInfo.InvariantCulture, "    {0}={1}", qd.Name, FormatQualifierValue(qd.Value));
                        LB.Items.Add(str);
                    }
            }
        }

        private void WMIMethodForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                RunTheMethod();
                e.Handled = true;
            }
            if (e.Control && e.Alt && e.KeyCode == Keys.C)
            {
                LB.Visible = !LB.Visible;
                e.Handled = true;
            }
        }

        private void bRun_Click(object sender, EventArgs e)
        {
            RunTheMethod();
            if (m_Args.StartCmd == WmiStartCommand.ExecuteMethod && m_ExecuteOK)
                DialogResult = DialogResult.OK;
        }

        private bool m_ExecuteOK;

        private void ShowOK(string message)
        {
            lResult.BackColor = Color.Green;
            lResult.Text = message;
            m_ExecuteOK = true;
        }

        private void ShowFAIL(string message)
        {
            lResult.BackColor = Color.Red;
            lResult.Text = message;
            m_ExecuteOK = false;
        }

        private void RunTheMethod()
        {
            if (WmiObject == null || WmiMethod == null) return;
            //var watcher = new ManagementOperationObserver();
            var inParams = WmiMethod.InParameters;
            var options = new InvokeMethodOptions();
            ManagementBaseObject result = null;
            bRun.Enabled = false;
            try
            {
                result = WmiObject.InvokeMethod(WmiMethod.Name, inParams, options);
            }
            catch (ManagementException ex)
            {
                ShowFAIL(ex.Message);
            }
            bRun.Enabled = true;
            if (result == null) return;
            LB.Items.Add(string.Empty);
            string str;
            foreach (PropertyData pd in result.Properties)
            {
                str = String.Format(CultureInfo.InvariantCulture, "{0} = {1}", pd.Name, pd.Value);
                LB.Items.Add(str);
            }
            var resultProp = new PropertyDataExt(result.Properties[m_ReturnValueName]);
            if (resultProp.Value != null)
            {
                var value = (int) ((UInt32) resultProp.Value);
                var message = new Win32Exception(value).Message;
                str = String.Format(CultureInfo.InvariantCulture, Resources.WMIMethodForm_Fail, value, message);
                LB.Items.Add(str);
                if (value == 0)
                {
                    ShowOK(Resources.WMIMethodForm_Success);
                    if (!m_OutParamPresent)
                        timerOK.Enabled = true;
                }
                else
                {
                    ShowFAIL(String.Format(CultureInfo.InvariantCulture, Resources.WMIMethodForm_Fail, value, message));
                }
            }
        }

        private void timerOK_Tick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
