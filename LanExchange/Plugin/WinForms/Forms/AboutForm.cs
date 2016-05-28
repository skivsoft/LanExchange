using System;
using System.Windows.Forms;
using LanExchange.Properties;
using LanExchange.SDK;
using System.Diagnostics.Contracts;

namespace LanExchange.Plugin.WinForms.Forms
{
    /// <summary>
    /// Concrete class for IAboutView.
    /// </summary>
    public sealed partial class AboutForm : Form, IAboutView, ITranslationable
    {
        private readonly IAboutPresenter presenter;
        private RichTextBox boxDetails;
        private bool detailsVisible;

        public event EventHandler ViewClosed;
        
        public AboutForm(IAboutPresenter presenter)
        {
            Contract.Requires<ArgumentNullException>(presenter != null);

            InitializeComponent();
            this.presenter = presenter;
            this.presenter.Initialize(this);

            TranslateUI();
            FormClosed += OnFormClosed;
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            if (ViewClosed != null)
                ViewClosed(this, EventArgs.Empty);
        }

        public void TranslateUI()
        {
            Text = Resources.AboutForm_Title;
            lVersion.Text = Resources.AboutForm_Version;
            lLicense.Text = Resources.AboutForm_License;
            eLicense.Text = Resources.AboutForm_MIT;
            lCopyright.Text = Resources.AboutForm_Copyright;
            lWeb.Text = Resources.AboutForm_Webpage;
            UpdateShowDetailsButton();
            bClose.Text = Resources.MainForm_Close;
            if (detailsVisible)
            {
                SetupBoxDetails();
                boxDetails.Rtf = presenter.GetDetailsRtf();
            }
            presenter.LoadFromModel();
        }
       
        private void SetupBoxDetails()
        {
            if (boxDetails != null) return;
            boxDetails = new RichTextBox();
            var rect = ClientRectangle;
			boxDetails.Font = Font;
            boxDetails.SetBounds(rect.Left+16, rect.Top+16, rect.Width-32, rect.Height-bShowDetails.Height-32);
            boxDetails.Visible = false;
            boxDetails.RightToLeft = RightToLeft;
            boxDetails.ReadOnly = true;
            boxDetails.BorderStyle = BorderStyle.None;
            boxDetails.Rtf = presenter.GetDetailsRtf();
            Controls.Add(boxDetails);
            boxDetails.BringToFront();
        }

        private void eWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            presenter.OpenHomeLink();
        }

        public string VersionText
        {
            get { return eVersion.Text; }
            set { eVersion.Text = value; }
        }

        public string CopyrightText
        {
            get { return eCopyright.Text; }
            set
            {
                eCopyright.Text = value;
                lWeb.Top = eCopyright.Top + eCopyright.Height + 8;
                eWeb.Top = lWeb.Top + lWeb.Height;
            }
        }

        public string WebText
        {
            get { return eWeb.Text; }
            set { eWeb.Text = value; }
        }

        public string WebToolTip
        {
            get { return tipAbout.GetToolTip(eWeb); }
            set { tipAbout.SetToolTip(eWeb, value); }
        }

        public string TwitterToolTip
        {
            get { return tipAbout.GetToolTip(picTwitter); }
            set
            {
                tipAbout.SetToolTip(picTwitter, value);
            }
        }

        private void bShowLicense_Click(object sender, EventArgs e)
        {
            detailsVisible = !detailsVisible;
            SetupBoxDetails();
            boxDetails.Visible = detailsVisible;
            UpdateShowDetailsButton();
        }


        private void UpdateShowDetailsButton()
        {
            bShowDetails.Text = detailsVisible ? Resources.HideDetails : Resources.ShowDetails;
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            bShowDetails.Text = Resources.ShowDetails;
        }

        private void picTwitter_Click(object sender, EventArgs e)
        {
            presenter.OpenTwitterLink();
        }

        private void AboutForm_RightToLeftChanged(object sender, EventArgs e)
        {
            if (boxDetails != null)
                boxDetails.RightToLeft = RightToLeft;
        }

        private void AboutForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
                e.Handled = true;
            }
        }
    }
}
