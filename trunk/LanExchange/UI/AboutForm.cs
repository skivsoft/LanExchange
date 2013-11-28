using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using LanExchange.Intf;
using LanExchange.Properties;

namespace LanExchange.UI
{
    /// <summary>
    /// Concrete class for IAboutView.
    /// </summary>
    public sealed partial class AboutForm : EscapeForm, IAboutView
    {
        private readonly IAboutPresenter m_Presenter;
        private RichTextBox m_BoxDetails;
        private bool m_DetailsVisible;
        
        public AboutForm(IAboutPresenter presenter)
        {
            m_Presenter = presenter;
            m_Presenter.View = this;
            InitializeComponent();
            ApplyResources();
        }

        public void ApplyResources()
        {
            var resources = new ComponentResourceManager(typeof(AboutForm));
            resources.ApplyResources(bClose, bClose.Name);
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
                m_BoxDetails.Rtf = GetDetailsRtf();
            }
            m_Presenter.LoadFromModel();
        }
       
        private void SetupBoxDetails()
        {
            if (m_BoxDetails != null) return;
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

        [Localizable(false)]
        private string GetDetailsRtf()
        {
            var sb = new StringBuilder();
            //sb.Append(@"{\rtf1\ansi");
            sb.Append(@"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset204 Microsoft Sans Serif;}}");
            sb.Append(@"\viewkind4\uc1\pard\f0\fs17\ ");
            sb.AppendLine(string.Format(@"\b {0}\b0", Resources.AboutForm_Plugins));
            foreach (var pair in App.Resolve<IPluginManager>().PluginsAuthors)
            {
                sb.Append("    " + pair.Key);
                if (!string.IsNullOrEmpty(pair.Value))
                    sb.Append(@"\tab " + pair.Value);
                sb.AppendLine();
            }
            sb.AppendLine();
            var translations = App.TR.GetTranslations();
            if (translations.Count > 0)
            {
                sb.AppendLine(string.Format(@"\b {0}\b0", Resources.AboutForm_Translations));
                foreach (var pair in translations)
                {
                    var line = pair.Key;
                    if (!string.IsNullOrEmpty(pair.Value))
                        line += @" \tab " + pair.Value;
                    sb.AppendLine("    " + line);
                }
            }
            sb.Append("}");
            return sb.ToString().Replace("\r\n", @"\line ");
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
            set { eCopyright.Text = value; }
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

        public string EmailToolTip
        {
            get { return tipAbout.GetToolTip(picEmail); }
            set
            {
                tipAbout.SetToolTip(picEmail, value);
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

        private void picEmail_Click(object sender, EventArgs e)
        {
            m_Presenter.OpenEmailLink();
        }
    }
}
