using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using WMIViewer.Properties;

namespace WMIViewer
{
    public sealed partial class WMIEditProperty : Form
    {
        private readonly WMIPresenter m_Presenter;
        private readonly WMIArgs m_Args;
        private string m_OldValue;

        public WMIEditProperty(WMIPresenter presenter)
        {
            m_Presenter = presenter;
            m_Args = presenter.Args;
            InitializeComponent();
            UpdateTitle();
            SetArgsToControls();
            Icon = Resources.WMIViewer16;
        }

        public void UpdateTitle()
        {
            var description = WMIClassList.GetPropertyValue(m_Presenter.Namespace, "Win32_OperatingSystem",
                "Description");
            if (string.IsNullOrEmpty(description))
                Text = @"\\" + m_Args.ComputerName;
            else
                Text = string.Format(CultureInfo.InvariantCulture, @"\\{0} — {1}", m_Args.ComputerName, description);
        }

        [Localizable(false)]
        public void SetArgsToControls()
        {
            lClass.Text = string.Format(CultureInfo.InvariantCulture, @"{0}\{1}", m_Args.NamespaceName, m_Args.ClassName);
            eClass.Text = WMIClassList.GetPropertyValue(m_Presenter.Namespace, m_Args.ClassName, "Caption");
            lProperty.Text = "&" + m_Args.PropertyName;
            bool editable;
            bool propFound;
            lDescription.Text = WMIClassList.Instance.GetPropertyDescription(m_Args.ClassName, 
                m_Args.PropertyName, out editable, out propFound);
            m_OldValue = WMIClassList.GetPropertyValue(m_Presenter.Namespace, m_Args.ClassName,
                m_Args.PropertyName);
            eProp.Text = m_OldValue;
            eProp.ReadOnly = !editable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!eProp.ReadOnly && !m_OldValue.Equals(eProp.Text))
            {
                WMIClassList.SetPropertyValue(m_Presenter.Namespace, m_Args.ClassName, m_Args.PropertyName, eProp.Text);
            }
        }
    }
}
