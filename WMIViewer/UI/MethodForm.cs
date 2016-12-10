using System;
using System.Drawing;
using System.Globalization;
using System.Management;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using WMIViewer.Model;
using WMIViewer.Presenter;
using WMIViewer.Properties;

namespace WMIViewer.UI
{
    internal sealed partial class MethodForm : Form
    {
        const string METHOD_FAIL_FMT = "[{0}] {1}";

        private readonly WmiPresenter presenter;
        private readonly CmdLineArgs args;
        private bool outParamPresent;

        public MethodForm(WmiPresenter presenter)
        {
            this.presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            args = this.presenter.Args;
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

        private string returnValueName;

        [Localizable(false)]
        private void PrepareArgs()
        {
            if (WmiClass == null)
            {
                var op = new ObjectGetOptions(null, TimeSpan.MaxValue, true);
                var path = new ManagementPath(args.ClassName);
                WmiClass = new ManagementClass(presenter.Namespace, path, op);
            }
            if (WmiMethod == null)
            {
                foreach(var md in WmiClass.Methods)
                {
                    if (string.Compare(md.Name, args.MethodName, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        WmiMethod = md;
                        break;
                    }
                }
            }
            if (WmiObject == null)
            {
                presenter.Namespace.Connect();
                var query = new ObjectQuery("SELECT * FROM " + args.ClassName);
                using (var searcher = new ManagementObjectSearcher(presenter.Namespace, query))
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
            Text = String.Format(CultureInfo.InvariantCulture, Text, args.NamespaceName, WmiClass.Path.ClassName, WmiMethod.Name);
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
                if (prop.ParameterType == ParameterType.Return)
                    returnValueName = prop.Name;
                if (prop.ParameterType == ParameterType.Out)
                    outParamPresent = true;
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
            if (args.StartCmd == CmdLineCommand.ExecuteMethod && executeOk)
                DialogResult = DialogResult.OK;
        }

        private bool executeOk;

        private void ShowOk(string message)
        {
            lResult.BackColor = Color.Green;
            lResult.Text = message;
            executeOk = true;
        }

        private void ShowFail(string message)
        {
            lResult.BackColor = Color.Red;
            lResult.Text = message;
            executeOk = false;
        }

        [Localizable(false)]
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
                ShowFail(ex.Message);
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
            var resultProp = new PropertyDataExt(result.Properties[returnValueName]);
            if (resultProp.Value != null)
            {
                var value = (int) ((UInt32) resultProp.Value);
                var message = new Win32Exception(value).Message;
                str = String.Format(CultureInfo.InvariantCulture, METHOD_FAIL_FMT, value, message);
                LB.Items.Add(str);
                if (value == 0)
                {
                    ShowOk(Resources.MethodForm_Success);
                    if (!outParamPresent)
                        timerOK.Enabled = true;
                }
                else
                {
                    ShowFail(String.Format(CultureInfo.InvariantCulture, METHOD_FAIL_FMT, value, message));
                }
            }
        }

        private void timerOK_Tick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
