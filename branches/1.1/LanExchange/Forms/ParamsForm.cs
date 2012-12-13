using System;
using System.Windows.Forms;

namespace LanExchange.Forms
{
    public partial class ParamsForm : Form
    {
        public static ParamsForm Instance;
        
        public ParamsForm()
        {
            InitializeComponent();
        }

        private void ParamsForm_Load(object sender, EventArgs e)
        {
            chAutorun.Checked = Settings.IsAutorun;
            chMinimized.Checked = Settings.Instance.RunMinimized;
            chAdvanced.Checked = Settings.Instance.AdvancedMode;
            eRefreshTime.Value = Settings.Instance.RefreshTimeInSec / 60;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            Settings.IsAutorun = chAutorun.Checked;
            Settings.Instance.RunMinimized = chMinimized.Checked;
            Settings.Instance.AdvancedMode = chAdvanced.Checked;
            Settings.Instance.RefreshTimeInSec = Int32.Parse(eRefreshTime.Value.ToString()) * 60;
            MainForm.Instance.AdminMode = Settings.Instance.AdvancedMode;
        }

        private void ParamsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                e.Handled = true;
            }
        }
    }
}
