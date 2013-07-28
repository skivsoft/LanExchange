using System;
using System.Diagnostics;
using System.Windows.Forms;
using LanExchange.Model;
using LanExchange.Presenter;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.UI
{
    /// <summary>
    /// Concrete class for IAboutView.
    /// </summary>
    public sealed partial class AboutForm : EscapeForm, IAboutView
    {
        public static AboutForm Instance;

        private readonly AboutPresenter m_Presenter;
        
        public AboutForm()
        {
            InitializeComponent();
            Text = String.Format(Text, AboutInfo.Product);
            eVersion.Text = AboutInfo.Version;
            eCopyright.Text = AboutInfo.Copyright;
            m_Presenter = new AboutPresenter(this);
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

        private void AboutForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Instance.Dispose();
            Instance = null;
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
