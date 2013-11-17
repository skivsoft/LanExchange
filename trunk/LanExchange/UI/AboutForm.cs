using System;
using System.Text;
using System.Windows.Forms;
using LanExchange.Core;
using LanExchange.Intf;
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
        private RichTextBox m_BoxDetails;
        
        public AboutForm(IAboutPresenter presenter)
        {
            m_Presenter = presenter;
            m_Presenter.View = this;
            InitializeComponent();
            m_Presenter.LoadFromModel();
            SetupBoxDetails();
        }
       
        private void SetupBoxDetails()
        {
            m_BoxDetails = new RichTextBox();
            var rect = ClientRectangle;
            m_BoxDetails.SetBounds(rect.Left+16, rect.Top+16, rect.Width-32, rect.Height-bShowDetails.Height-32);
            m_BoxDetails.Visible = false;
            m_BoxDetails.ReadOnly = true;
            m_BoxDetails.BorderStyle = BorderStyle.None;
            m_BoxDetails.Rtf = GetDetailsRtf();
            Controls.Add(m_BoxDetails);
            m_BoxDetails.BringToFront();
        }

        private string GetDetailsRtf()
        {
            var sb = new StringBuilder();
            sb.Append(@"{\rtf1\ansi");
            sb.AppendLine(@"\b Plugins:\b0");
            sb.AppendLine("  Network");
            sb.AppendLine("  Users");
            sb.AppendLine();
            sb.AppendLine(@"\b Translations:\b0");
            sb.AppendLine(@"  English - Translator1");
            sb.AppendLine(@"  Russian - Translator2");
            sb.AppendLine(@"  Kazakh - Translator3");
            sb.AppendLine(@"  Esperanto - Translator4");
            sb.Append("}");
            return sb.ToString().Replace("\r\n", @"\line ");
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
            m_BoxDetails.Visible = !m_BoxDetails.Visible;
            if (m_BoxDetails.Visible)
                bShowDetails.Text = Resources.HideDetails;
            else
                bShowDetails.Text = Resources.ShowDetails;
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            bShowDetails.Text = Resources.ShowDetails;
        }

    }
}
