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
        private readonly WmiPresenter presenter;
        private readonly CmdLineArgs args;
        private string oldValue;

        public EditPropertyForm(WmiPresenter presenter)
        {
            if (presenter == null)
                throw new ArgumentNullException(nameof(presenter));
            this.presenter = presenter;
            args = presenter.Args;
            InitializeComponent();
            UpdateTitle();
            SetArgsToControls();
            Icon = Resources.WMIViewer16;
        }

        [Localizable(false)]
        public void UpdateTitle()
        {
            var description = WmiClassList.GetPropertyValue(
                presenter.Namespace,
                "Win32_OperatingSystem",
                "Description");
            if (string.IsNullOrEmpty(description))
                Text = @"\\" + args.ComputerName;
            else
                Text = string.Format(CultureInfo.InvariantCulture, MainForm.TITLE_FMT, args.ComputerName, description);
        }

        [Localizable(false)]
        public void SetArgsToControls()
        {
            lClass.Text = string.Format(CultureInfo.InvariantCulture, @"{0}\{1}", args.NamespaceName, args.ClassName);
            eClass.Text = WmiClassList.GetPropertyValue(presenter.Namespace, args.ClassName, "Caption");
            lProperty.Text = "&" + args.PropertyName;
            lDescription.Text = WmiClassList.Instance.GetPropertyDescription(
                args.ClassName, 
                args.PropertyName);
            oldValue = WmiClassList.GetPropertyValue(
                presenter.Namespace,
                args.ClassName,
                args.PropertyName);
            eProp.Text = oldValue;
            eProp.ReadOnly = !WmiClassList.Instance.IsPropertyEditable(args.ClassName, args.PropertyName);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!eProp.ReadOnly && !oldValue.Equals(eProp.Text))
            {
                WmiClassList.SetPropertyValue(presenter.Namespace, args.ClassName, args.PropertyName, eProp.Text);
            }
        }
    }
}
