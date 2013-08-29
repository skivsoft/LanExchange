using System;
using System.Windows.Forms;
using LanExchange.Core;
using LanExchange.Presenter;
using LanExchange.Properties;
using LanExchange.UI;

namespace LanExchange.UI
{
    /// <summary>
    /// Concrete class for IAboutView.
    /// </summary>
    public sealed partial class AboutForm : EscapeForm, IAboutView
    {
        private readonly IAboutPresenter m_Presenter;
        
        public AboutForm(IAboutPresenter presenter)
        {
            m_Presenter = presenter;
            m_Presenter.View = this;
            InitializeComponent();
            m_Presenter.LoadFromModel();
            boxLicense.BringToFront();
        }
       
        private void eWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_Presenter.OpenWebLink();
        }

        private void eTwitter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_Presenter.OpenTwitterLink();
        }

        private void eEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_Presenter.OpenEmailLink();
        }

        public string VersionText
        {
            get { return eVersion.Text; }
            set { eVersion.Text = value; }
        }

        public string CopyrightText
        {
            get { return eCopyright.Text; }
            set { eCopyright.Text = value; }
        }

        public string WebText
        {
            get { return eWeb.Text; }
            set { eWeb.Text = value; }
        }

        public string TwitterText
        {
            get { return eTwitter.Text; }
            set { eTwitter.Text = value; }
        }

        public string EmailText
        {
            get { return eEmail.Text; }
            set { eEmail.Text = value; }
        }

        public string WebToolTip
        {
            get { return tipAbout.GetToolTip(eWeb); }
            set { tipAbout.SetToolTip(eWeb, value); }
        }

        public string TwitterToolTip
        {
            get { return tipAbout.GetToolTip(eTwitter); }
            set { tipAbout.SetToolTip(eTwitter, value); }
        }

        public string EmailToolTip
        {
            get { return tipAbout.GetToolTip(eEmail); }
            set { tipAbout.SetToolTip(eEmail, value); }
        }

        private void bShowLicense_Click(object sender, EventArgs e)
        {
            boxLicense.Visible = !boxLicense.Visible;
            if (boxLicense.Visible)
                bShowLicense.Text = Resources.HideLicense;
            else
                bShowLicense.Text = Resources.ShowLicense;
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            bShowLicense.Text = Resources.ShowLicense;
        }

    }
}
