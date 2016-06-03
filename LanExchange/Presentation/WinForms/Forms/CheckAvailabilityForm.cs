using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.Application.Interfaces;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Presentation.WinForms.Forms
{
    public partial class CheckAvailabilityForm : Form, ICheckAvailabilityWindow
    {
        private readonly ICheckAvailabilityPresenter presenter;
        private PanelItemBase currentItem;

        public event EventHandler ViewClosed;

        public CheckAvailabilityForm(ICheckAvailabilityPresenter presenter)
        {
            Contract.Requires<ArgumentNullException>(presenter != null);

            InitializeComponent();

            this.presenter = presenter;
            presenter.Initialize(this);
        }

        public PanelItemBase CurrentItem
        {
            get { return currentItem; }
            set
            {
                currentItem = value;
                presenter.OnCurrentItemChanged();
            }
        }

        public string RunText
        {
            get { return bRun.Text; }
            set { bRun.Text = value; }
        }

        public Image RunImage
        {
            get { return bRun.Image; }
            set { bRun.Image = value; }
        }

        public Action RunAction { get; set; }

        public Func<bool> AvailabilityChecker { get; set; }

        public void StartChecking()
        {
            presenter.StartChecking();
        }

        public void WaitAndShow()
        {
            presenter.WaitAndShow();
        }

        private void bRun_Click(object sender, EventArgs e)
        {
            presenter.PerformOk();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            presenter.PerformCancel();
        }

        private void CheckAvailabilityForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ViewClosed != null)
                ViewClosed(this, EventArgs.Empty);
        }

        public Image ObjectImage
        {
            get { return picObject.Image; }
            set { picObject.Image = value; }
        }

        public string ObjectText
        {
            get { return lObject.Text; }
            set { lObject.Text = value; }
        }

        public void SetToolTip(string tooltip)
        {
            toolTip.SetToolTip(lObject, tooltip);
        }

        public void InvokeClose()
        {
            if (Visible)
                Invoke(new Action(Close));
        }

        public object CallerControl { get; set; }

        private void CheckAvailabilityForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                presenter.PerformCancel();
                e.Handled = true;
            }
        }
    }
}
