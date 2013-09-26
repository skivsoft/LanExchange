using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using WMIViewer.Properties;

namespace WMIViewer
{
    public sealed partial class EditPropertyForm : Form
    {
        private readonly Presenter m_Presenter;
        private readonly CmdLineArgs m_Args;
        private string m_OldValue;

        public EditPropertyForm(Presenter presenter)
        {
            if (presenter == null)
                throw new ArgumentNullException("presenter");
            m_Presenter = presenter;
            m_Args = presenter.Args;
            InitializeComponent();
            UpdateTitle();
            SetArgsToControls();
            Icon = Resources.WMIViewer16;
        }

        [Localizable(false)]
        public void UpdateTitle()
        {
            var description = ClassList.GetPropertyValue(m_Presenter.Namespace, "Win32_OperatingSystem",
                "Description");
            if (string.IsNullOrEmpty(description))
                Text = Resources.WMIForm_CompPrefix + m_Args.ComputerName;
            else
                Text = string.Format(CultureInfo.InvariantCulture, Resources.WMIForm_Title, m_Args.ComputerName, description);
        }

        public void SetArgsToControls()
        {
            lClass.Text = string.Format(CultureInfo.InvariantCulture, Resources.WMIEditProperty_PropertyFmt, m_Args.NamespaceName, m_Args.ClassName);
            eClass.Text = ClassList.GetPropertyValue(m_Presenter.Namespace, m_Args.ClassName, "Caption");
            lProperty.Text = Resources.AMP + m_Args.PropertyName;
            lDescription.Text = ClassList.Instance.GetPropertyDescription(m_Args.ClassName, 
                m_Args.PropertyName);
            m_OldValue = ClassList.GetPropertyValue(m_Presenter.Namespace, m_Args.ClassName,
                m_Args.PropertyName);
            eProp.Text = m_OldValue;
            eProp.ReadOnly = !ClassList.Instance.IsPropertyEditable(m_Args.ClassName, m_Args.PropertyName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!eProp.ReadOnly && !m_OldValue.Equals(eProp.Text))
            {
                ClassList.SetPropertyValue(m_Presenter.Namespace, m_Args.ClassName, m_Args.PropertyName, eProp.Text);
            }
        }
    }
}
