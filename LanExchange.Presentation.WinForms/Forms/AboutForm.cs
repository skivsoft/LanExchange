using System;
using System.Windows.Forms;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.WinForms.Properties;

namespace LanExchange.Presentation.WinForms.Forms
{
    /// <summary>
    /// Concrete class for IAboutView.
    /// </summary>
    internal sealed partial class AboutForm : Form, IAboutView, IWindowTranslationable
    {
        private readonly IAboutPresenter presenter;
        private RichTextBox boxDetails;

        public event EventHandler ViewClosed;
        
        public AboutForm(IAboutPresenter presenter)
        {
            if (presenter != null) throw new ArgumentNullException(nameof(presenter));

            InitializeComponent();
            this.presenter = presenter;
            this.presenter.Initialize(this);

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
            bClose.Text = Resources.MainForm_Close;
            if (DetailsVisible)
            {
                SetupBoxDetails();
                boxDetails.Rtf = presenter.GetDetailsRtf();
            }
        }
       
        public void SetupBoxDetails()
        {
            if (boxDetails != null) return;
            boxDetails = new RichTextBox();
            var rect = ClientRectangle;
            boxDetails.Font = Font;
            boxDetails.SetBounds(rect.Left + 16, rect.Top + 16, rect.Width - 32, rect.Height - bShowDetails.Height - 32);
            boxDetails.Visible = false;
            boxDetails.RightToLeft = RightToLeftValue ? RightToLeft.Yes : RightToLeft.No;
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

        public bool DetailsVisible
        {
            get
            {
                if (boxDetails == null) return false;
                return boxDetails.Visible;
            }
            set
            {
                if (boxDetails == null)
                    SetupBoxDetails();
                boxDetails.Visible = value;
                bShowDetails.Text = value ? Resources.HideDetails : Resources.ShowDetails;
            }
        }

        private void bShowDetails_Click(object sender, EventArgs e)
        {
            presenter.PerformShowDetails();
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            bShowDetails.Text = Resources.ShowDetails;
        }

        private void AboutForm_RightToLeftChanged(object sender, EventArgs e)
        {
            if (boxDetails != null)
                boxDetails.RightToLeft = RightToLeftValue ? RightToLeft.Yes : RightToLeft.No;
        }

        public bool ShowModalDialog()
        {
            return ShowDialog() == DialogResult.OK;
        }

        public bool RightToLeftValue { get; set; }
    }
}