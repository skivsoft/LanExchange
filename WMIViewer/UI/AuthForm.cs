using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using WMIViewer.Properties;

namespace WMIViewer.UI
{
    public sealed partial class AuthForm : Form
    {
        private static string userName;
        private static string userPassword;
        
        public AuthForm()
        {
            InitializeComponent();
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

        public bool AutoLogOn()
        {
            if (!string.IsNullOrEmpty(userName))
            {
                eUserName.Text = userName;
                ePassword.Text = userPassword;
                return true;
            }

            return false;
        }

        [Localizable(false)]
        public void SetComputerName(string computerName)
        {
            Text = string.Format(CultureInfo.InvariantCulture, Text, computerName);
            var userName = AutoLogOn() ? AuthForm.userName : 
                string.Format(CultureInfo.InvariantCulture, @"{0}\{1}", Environment.UserDomainName, Environment.UserName);
            lMessage.Text = string.Format(CultureInfo.InvariantCulture, lMessage.Text, userName);
        }

        private void WMIAuthForm_Load(object sender, EventArgs e)
        {

            picShield.Image = SystemIcons.Error.ToBitmap();
            bOK.NotifyDefault(true);
            ActiveControl = eUserName;
            UserName = userName;
            UserPassword = userPassword;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(eUserName.Text.Trim()))
            {
                Error.SetError(eUserName, Resources.AuthForm_UserNameNotSpecified);
                DialogResult = DialogResult.None;
                return;
            }

            userName = UserName;
            userPassword = UserPassword;
        }

        public static void ClearSavedPassword()
        {
            userName = string.Empty;
            userPassword = string.Empty;
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
