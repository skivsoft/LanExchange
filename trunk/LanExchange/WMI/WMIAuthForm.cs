using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.Properties;

namespace LanExchange.WMI
{
    public partial class WMIAuthForm : Form
    {
        private static string m_UserName;
        private static string m_UserPassword;
        
        public WMIAuthForm()
        {
            InitializeComponent();
        }

        private void WMIAuthForm_Load(object sender, EventArgs e)
        {

            picShield.Image = SystemIcons.Error.ToBitmap();
            bOK.NotifyDefault(true);
            ActiveControl = eUserName;
            UserName = m_UserName;
            UserPassword = m_UserPassword;
        }

        public bool AutoLogon()
        {
            if (!String.IsNullOrEmpty(m_UserName))
            {
                eUserName.Text = m_UserName;
                ePassword.Text = m_UserPassword;
                chSavePassword.Checked = true;
                return true;
            }
            return false;
        }

        [Localizable(false)]
        public void SetComputerName(string computerName)
        {
            Text = String.Format(Text, computerName);
            string userName;
            if (AutoLogon())
                userName = m_UserName;
            else
                userName = string.Format(@"{0}\{1}", Environment.UserDomainName, Environment.UserName);
            lMessage.Text = String.Format(lMessage.Text, userName);
        }

        public string UserName
        {
            get { return eUserName.Text; }
            set { eUserName.Text = value; }
        }

        public string UserPassword
        {
            get { return ePassword.Text; }
            set { ePassword.Text = value; }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(eUserName.Text.Trim()))
            {
                Error.SetError(eUserName, Resources.WMIAuthForm_UserNameError);
                DialogResult = DialogResult.None;
                return;
            }
            if (chSavePassword.Checked)
            {
                m_UserName = UserName;
                m_UserPassword = UserPassword;
            }
        }

        public static void ClearSavedPassword()
        {
            m_UserName = string.Empty;
            m_UserPassword = string.Empty;
        }

        private void WMIAuthForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                e.Handled = true;
            }
        }
    }
}
