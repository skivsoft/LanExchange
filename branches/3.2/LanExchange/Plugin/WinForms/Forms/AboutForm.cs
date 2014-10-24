using System;
using System.Windows.Forms;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.WinForms.Forms
{
    /// <summary>
    /// Concrete class for IAboutView.
    /// </summary>
    public sealed partial class AboutForm : EscapeForm, IAboutView, ITranslationable
    {
        private readonly IAboutPresenter m_Presenter;
        private RichTextBox m_BoxDetails;
        private bool m_DetailsVisible;

        public event EventHandler ViewClosed;
        
        public AboutForm(IAboutPresenter presenter)
        {
            if (presenter == null)
                throw new ArgumentNullException("presenter");
            m_Presenter = presenter;
            m_Presenter.View = this;
            InitializeComponent();
            TranslateUI();
            FormClosed += OnFormClosed;
        }

        private void OnFormClosed(object sender, FormClosedEventArgs formClosedEventArgs)
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
            if (m_DetailsVisible)
            {
                SetupBoxDetails();
                m_BoxDetails.Rtf = m_Presenter.GetDetailsRtf();
            }
            m_Presenter.LoadFromModel();
        }
       
        private void SetupBoxDetails()
        {
            if (m_BoxDetails != null) return;
            m_BoxDetails = new RichTextBox();
            var rect = ClientRectangle;
			m_BoxDetails.Font = Font;
            m_BoxDetails.SetBounds(rect.Left+16, rect.Top+16, rect.Width-32, rect.Height-bShowDetails.Height-32);
            m_BoxDetails.Visible = false;
            m_BoxDetails.RightToLeft = RightToLeft;
            m_BoxDetails.ReadOnly = true;
            m_BoxDetails.BorderStyle = BorderStyle.None;
            m_BoxDetails.Rtf = m_Presenter.GetDetailsRtf();
            Controls.Add(m_BoxDetails);
            m_BoxDetails.BringToFront();
        }

        private void eWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_Presenter.OpenHomeLink();
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
            m_DetailsVisible = !m_DetailsVisible;
            SetupBoxDetails();
            m_BoxDetails.Visible = m_DetailsVisible;
            UpdateShowDetailsButton();
        }


        private void UpdateShowDetailsButton()
        {
            bShowDetails.Text = m_DetailsVisible ? Resources.HideDetails : Resources.ShowDetails;
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
            m_Presenter.OpenTwitterLink();
        }

        private void AboutForm_RightToLeftChanged(object sender, EventArgs e)
        {
            if (m_BoxDetails != null)
                m_BoxDetails.RightToLeft = RightToLeft;
        }
    }
}