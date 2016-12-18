using System;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Presentation.WinForms.Forms
{
    internal sealed partial class CheckAvailabilityForm : Form, ICheckAvailabilityWindow
    {
        private readonly ICheckAvailabilityPresenter presenter;
        private PanelItemBase currentItem;

        public CheckAvailabilityForm(ICheckAvailabilityPresenter presenter)
        {
            if (presenter == null) throw new ArgumentNullException(nameof(presenter));

            InitializeComponent();

            this.presenter = presenter;
            presenter.Initialize(this);
        }

        public event EventHandler ViewClosed;

        public PanelItemBase CurrentItem
        {
            get
            {
                return currentItem;
            }

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

        public object CallerControl { get; set; }

        public void StartChecking()
        {
            presenter.StartChecking();
        }

        public void WaitAndShow()
        {
            presenter.WaitAndShow();
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

        private void ButtonRun_Click(object sender, EventArgs e)
        {
            presenter.PerformOk();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            presenter.PerformCancel();
        }

        private void CheckAvailabilityForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ViewClosed != null)
                ViewClosed(this, EventArgs.Empty);
        }

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
