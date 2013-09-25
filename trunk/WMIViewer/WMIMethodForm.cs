using System;
using System.Drawing;
using System.Management;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using WMIViewer.Properties;

namespace WMIViewer
{
    public partial class WMIMethodForm : Form
    {
        private readonly WMIPresenter m_Presenter;
        private readonly WMIArgs m_Args;
        private bool m_OutParamPresent;

        public WMIMethodForm(WMIPresenter presenter)
        {
            m_Presenter = presenter;
            m_Args = m_Presenter.Args;
            InitializeComponent();
            Icon = Resources.WMIViewer16;
        }

        public ManagementClass WMIClass { get; set; }
        public ManagementObject WMIObject { get; set; }
        public MethodData WMIMethod { get; set; }

        [Localizable(false)]
        private string FormatQualifierValue(object value)
        {
            if (value is string[])
            {
                var list = value as string[];
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


        private void PrepareArgs()
        {
            if (WMIClass == null)
            {
                var op = new ObjectGetOptions(null, TimeSpan.MaxValue, true);
                var path = new ManagementPath(m_Args.ClassName);
                WMIClass = new ManagementClass(m_Presenter.Namespace, path, op);
            }
            if (WMIMethod == null)
            {
                foreach(var md in WMIClass.Methods)
                {
                    if (string.Compare(md.Name, m_Args.MethodName, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        WMIMethod = md;
                        break;
                    }
                }
            }
            if (WMIObject == null)
            {
                m_Presenter.Namespace.Connect();
                var query = new ObjectQuery("SELECT * FROM " + m_Args.ClassName);
                using (var searcher = new ManagementObjectSearcher(m_Presenter.Namespace, query))
                {
                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        WMIObject = queryObj;
                        break;
                    }
                }
            }
        }

        [Localizable(false)]
        public void PrepareForm()
        {
            PrepareArgs();
            Text = String.Format(Text, m_Args.NamespaceName, WMIClass.Path.ClassName, WMIMethod.Name);
            lMethodName.Text = String.Format("{0}()", WMIMethod.Name);
            foreach (var qd in WMIMethod.Qualifiers)
            {
                if (qd.Name.Equals("Description"))
                {
                    lDescription.Text = qd.Value.ToString();
                }
            }
            // output method qualifiers
            string str = String.Format("Class={0}, Method={1}", WMIClass.Path.ClassName, WMIMethod.Name);
            LB.Items.Add(str);
            foreach (var qd in WMIMethod.Qualifiers)
            {
                if (qd.Name.Equals("Description") || qd.Name.Equals("Implemented"))
                    continue;
                str = String.Format("{0}={1}", qd.Name, FormatQualifierValue(qd.Value));
                LB.Items.Add(str);
            }
            LB.Items.Add(string.Empty);
            // prepare properties list and sort it by ID
            var propList = new List<PropertyDataEx>();
            if (WMIMethod.InParameters != null)
                foreach (PropertyData pd in WMIMethod.InParameters.Properties)
                    propList.Add(new PropertyDataEx(pd));
            if (WMIMethod.OutParameters != null)
                foreach (PropertyData pd in WMIMethod.OutParameters.Properties)
                    propList.Add(new PropertyDataEx(pd));
            propList.Sort();
            // output properties
            var excludeList = new List<string>();
            excludeList.Add("CIMTYPE");
            excludeList.Add("ID");
            excludeList.Add("In");
            excludeList.Add("Out");
            excludeList.Add("in");
            excludeList.Add("out");
            foreach (PropertyDataEx prop in propList)
            {
                // detect return value parameter name
                if (prop.ParamType == WMIParamType.RETURN)
                    m_ReturnValueName = prop.Name;
                if (prop.ParamType == WMIParamType.OUT)
                    m_OutParamPresent = true;
                str = String.Format("{0} {1} {2} : {3}", prop.ID, prop.ParamType, prop.Name, prop.Type);
                LB.Items.Add(str);
                foreach (var qd in prop.Qualifiers)
                    if (!excludeList.Contains(qd.Name))
                    {
                        str = String.Format("    {0}={1}", qd.Name, FormatQualifierValue(qd.Value));
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
            if (m_Args.StartCmd == WMIStartCommand.ExecuteMethod && m_ExecuteOK)
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

        [Localizable(false)]
        private void RunTheMethod()
        {
            if (WMIObject == null || WMIMethod == null) return;
            //var watcher = new ManagementOperationObserver();
            var inParams = WMIMethod.InParameters;
            var options = new InvokeMethodOptions();
            ManagementBaseObject result = null;
            bRun.Enabled = false;
            try
            {
                result = WMIObject.InvokeMethod(WMIMethod.Name, inParams, options);
            }
            catch (Exception E)
            {
                ShowFAIL(E.Message);
            }
            bRun.Enabled = true;
            if (result == null) return;
            LB.Items.Add(string.Empty);
            string str;
            foreach (PropertyData pd in result.Properties)
            {
                str = String.Format("{0} = {1}", pd.Name, pd.Value);
                LB.Items.Add(str);
            }
            PropertyDataEx resultProp = new PropertyDataEx(result.Properties[m_ReturnValueName]);
            if (resultProp.Value != null)
            {
                int value = (int) ((UInt32) resultProp.Value);
                var message = new Win32Exception(value).Message;
                str = String.Format("[{0}] {1}", value, message);
                LB.Items.Add(str);
                if (value == 0)
                {
                    ShowOK("[0] Success");
                    if (!m_OutParamPresent)
                        timerOK.Enabled = true;
                }
                else
                {
                    ShowFAIL(String.Format("[{0}] {1}", value, message));
                }
            }
        }

        private void timerOK_Tick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        public ListView LV
        {
            get { return null; }
        }
    }
}
