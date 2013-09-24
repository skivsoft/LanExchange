using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WMIViewer
{
    public partial class WMIEditProperty : Form, IWMIView
    {
        private WMIPresenter m_Presenter;
        private WMIArgs m_Args;
        private string m_OldValue;

        public WMIEditProperty(WMIPresenter presenter)
        {
            m_Presenter = presenter;
            m_Presenter.View = this;
            m_Args = presenter.Args;
            InitializeComponent();
            UpdateTitle();
            SetArgsToControls();
        }

        public void UpdateTitle()
        {
            var description = WMIClassList.Instance.GetPropertyValue(m_Presenter.Namespace, "Win32_OperatingSystem",
                "Description");
            if (string.IsNullOrEmpty(description))
                Text = @"\\" + m_Args.ComputerName;
            else
                Text = string.Format(@"\\{0} — {1}", m_Args.ComputerName, description);
        }

        [Localizable(false)]
        public void SetArgsToControls()
        {
            lClass.Text = string.Format(@"{0}\{1}", m_Args.NamespaceName, m_Args.ClassName);
            eClass.Text = WMIClassList.Instance.GetPropertyValue(m_Presenter.Namespace, m_Args.ClassName, "Caption");
            lProperty.Text = "&" + m_Args.PropertyName;
            bool editable;
            lDescription.Text = WMIClassList.Instance.GetPropertyDescription(m_Args.ClassName, 
                m_Args.PropertyName, out editable);
            m_OldValue = WMIClassList.Instance.GetPropertyValue(m_Presenter.Namespace, m_Args.ClassName,
                m_Args.PropertyName);
            eProp.Text = m_OldValue;
            eProp.ReadOnly = !editable;
        }

        public ListView LV
        {
            get { return null; }
        }

        private void WMIEditProperty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!eProp.ReadOnly && !m_OldValue.Equals(eProp.Text))
            {
                WMIClassList.Instance.SetPropertyValue(m_Presenter.Namespace, m_Args.ClassName, m_Args.PropertyName, eProp.Text);
            }
        }
    }
}
