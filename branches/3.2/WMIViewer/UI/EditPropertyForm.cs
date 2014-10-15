using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using WMIViewer.Model;
using WMIViewer.Presenter;
using WMIViewer.Properties;

namespace WMIViewer.UI
{
    internal sealed partial class EditPropertyForm : Form
    {
        private readonly WmiPresenter m_Presenter;
        private readonly CmdLineArgs m_Args;
        private string m_OldValue;

        public EditPropertyForm(WmiPresenter presenter)
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
            var description = WmiClassList.GetPropertyValue(m_Presenter.Namespace, "Win32_OperatingSystem",
                "Description");
            if (string.IsNullOrEmpty(description))
                Text = @"\\" + m_Args.ComputerName;
            else
                Text = string.Format(CultureInfo.InvariantCulture, MainForm.TITLE_FMT, m_Args.ComputerName, description);
        }

        [Localizable(false)]
        public void SetArgsToControls()
        {
            lClass.Text = string.Format(CultureInfo.InvariantCulture, @"{0}\{1}", m_Args.NamespaceName, m_Args.ClassName);
            eClass.Text = WmiClassList.GetPropertyValue(m_Presenter.Namespace, m_Args.ClassName, "Caption");
            lProperty.Text = "&" + m_Args.PropertyName;
            lDescription.Text = WmiClassList.Instance.GetPropertyDescription(m_Args.ClassName, 
                m_Args.PropertyName);
            m_OldValue = WmiClassList.GetPropertyValue(m_Presenter.Namespace, m_Args.ClassName,
                m_Args.PropertyName);
            eProp.Text = m_OldValue;
            eProp.ReadOnly = !WmiClassList.Instance.IsPropertyEditable(m_Args.ClassName, m_Args.PropertyName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!eProp.ReadOnly && !m_OldValue.Equals(eProp.Text))
            {
                WmiClassList.SetPropertyValue(m_Presenter.Namespace, m_Args.ClassName, m_Args.PropertyName, eProp.Text);
            }
        }
    }
}
