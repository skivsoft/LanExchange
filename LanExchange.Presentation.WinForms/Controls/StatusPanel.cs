using System;
using System.Diagnostics.Contracts;
using System.Windows.Forms;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Presentation.WinForms.Controls
{
    public sealed partial class StatusPanel : UserControl, IStatusPanelView
    {
        private readonly IStatusPanelPresenter presenter;

        public StatusPanel(IStatusPanelPresenter presenter)
        {
            Contract.Requires<ArgumentNullException>(presenter != null);

            InitializeComponent();
            this.presenter = presenter;
            presenter.Initialize(this);

            Dock = DockStyle.Bottom;
        }

        private void Status_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                presenter.PerformDoubleClick();
        }

        private void lCompName_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                presenter.PerformComputerRightClick();
        }

        private void lUserName_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                presenter.PerformUserRightClick();
        }

        public string ComputerName
        {
            get { return lCompName.Text; }
            set { lCompName.Text = value; }
        }

        public int ComputerImageIndex
        {
            get { return lCompName.ImageIndex; }
            set { lCompName.ImageIndex = value; }
        }

        public string UserName
        {
            get { return lUserName.Text; }
            set { lUserName.Text = value; }
        }

        public int UserImageIndex
        {
            get { return lUserName.ImageIndex; }
            set { lUserName.ImageIndex = value; }
        }

        public void SetImageList(object imageList)
        {
            Status.ImageList = (ImageList) imageList;
        }
    }
}