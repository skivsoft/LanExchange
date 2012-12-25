using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using LanExchange.View;
using LanExchange.Presenter;

// This module must use only View-layer and Presentation-layer.
namespace LanExchange.UI
{
    /// <summary>
    /// Concrete class for IAboutView.
    /// </summary>
    partial class AboutForm : Form, IAboutView
    {
        public static AboutForm Instance;

        private Control MsgControl;
        private readonly AboutPresenter m_Presenter;
        
        public AboutForm()
        {
            InitializeComponent();
            Text = String.Format("О программе «{0}»", AssemblyProduct);
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = String.Format("Версия {0}", AssemblyVersion);
            labelWeb.LinkArea = new LinkArea(labelWeb.Text.Length, labelWeb.LinkArea.Length);
            labelEmail.LinkArea = new LinkArea(labelEmail.Text.Length, labelEmail.LinkArea.Length);
            labelCopyright.Text = AssemblyCopyright;

            m_Presenter = new AboutPresenter(this);
            m_Presenter.LoadFromModel();
        }

        public void HideMessage()
        {
            if (MsgControl != null)
            {
                tableLayoutPanel.Controls.Remove(MsgControl);
                MsgControl.Dispose();
                MsgControl = null;
            }
        }

        public void ShowMessage(string text, Color color)
        {
            HideMessage();
            Label label = new Label();
            label.AutoSize = true;
            label.Text = text;
            label.ForeColor = color;
            label.Font = new Font(label.Font, FontStyle.Italic);
            tableLayoutPanel.Controls.Add(label, 1, 2);
            tableLayoutPanel.SetColumnSpan(label, 2);
            MsgControl = label;
        }


        #region Assembly Attribute Accessors

        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void AboutForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                e.Handled = true;
            }
        }

        public void ShowProgressBar()
        {
            // рисуем прогресс обновления
            ProgressBar Progress = new ProgressBar { Width = MsgControl.Width, Style = ProgressBarStyle.Marquee };
            HideMessage();
            tableLayoutPanel.Controls.Add(Progress, 1, 3);
            Progress.Update();
            MsgControl = Progress;
        }

        public void ShowUpdateButton(Version version)
        {
            Button B = new Button
            {
                AutoSize = true,
                Text = String.Format("Обновить до версии {0}", version)
            };
            B.Click += new EventHandler(UpdateButton_Click);
            HideMessage();
            tableLayoutPanel.Controls.Add(B, 1, 2);
            MsgControl = B;
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            m_Presenter.StartUpdate();
        }
        
        private void labelWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AboutPresenter.OpenWebLink();
        }

        private void labelEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AboutPresenter.OpenEmailLink();
        }

        public string WebText
        {
            get
            {
                return labelWeb.Text;
            }
            set
            {
                labelWeb.Text = value;
            }
        }

        public string EmailText
        {
            get
            {
                return labelEmail.Text;
            }
            set
            {
                labelEmail.Text = value;
            }
        }


        public void CancelView()
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
