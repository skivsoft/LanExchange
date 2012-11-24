using System;
using System.Windows.Forms;

namespace LanExchange.Forms
{
    public partial class ParamsForm : Form
    {
        public ParamsForm()
        {
            InitializeComponent();
        }

        private void ParamsForm_Load(object sender, EventArgs e)
        {
            chAutorun.Checked = Settings.IsAutorun;
            chMinimized.Checked = Settings.IsRunMinimized;
            chAdvanced.Checked = Settings.IsAdvancedMode;
            eRefreshTime.Value = Settings.RefreshTimeInSec / 60;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.IsAutorun = chAutorun.Checked;
            Settings.IsRunMinimized = chMinimized.Checked;
            Settings.IsAdvancedMode = chAdvanced.Checked;
            Settings.RefreshTimeInSec = Int32.Parse(eRefreshTime.Value.ToString()) * 60;

            MainForm.MainFormInstance.AdminMode = Settings.IsAdvancedMode;
        }

        private void ParamsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                e.Handled = true;
            }
        }
    }
}
