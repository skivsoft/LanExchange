using System;
using System.Windows.Forms;

namespace LanExchange
{
    public partial class ParamsForm : Form
    {
        public ParamsForm()
        {
            InitializeComponent();
        }

        private void ParamsForm_Load(object sender, EventArgs e)
        {
            chAutorun.Checked = TSettings.IsAutorun;
            chMinimized.Checked = TSettings.IsRunMinimized;
            chAdvanced.Checked = TSettings.IsAdvancedMode;
            eRefreshTime.Value = TSettings.RefreshTimeInSec / 60;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TSettings.IsAutorun = chAutorun.Checked;
            TSettings.IsRunMinimized = chMinimized.Checked;
            TSettings.IsAdvancedMode = chAdvanced.Checked;
            TSettings.RefreshTimeInSec = Int32.Parse(eRefreshTime.Value.ToString()) * 60;

            MainForm.MainFormInstance.AdminMode = TSettings.IsAdvancedMode;
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
