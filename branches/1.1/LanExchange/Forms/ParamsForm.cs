using System;
using System.Windows.Forms;

namespace LanExchange.Forms
{
    public partial class ParamsForm : Form
    {
        private Settings m_Settings = null;

        public ParamsForm()
        {
            InitializeComponent();
            m_Settings = Settings.GetInstance();
        }

        private void ParamsForm_Load(object sender, EventArgs e)
        {
            chAutorun.Checked = m_Settings.IsAutorun;
            chMinimized.Checked = m_Settings.IsRunMinimized;
            chAdvanced.Checked = m_Settings.IsAdvancedMode;
            eRefreshTime.Value = m_Settings.RefreshTimeInSec / 60;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            m_Settings.IsAutorun = chAutorun.Checked;
            m_Settings.IsRunMinimized = chMinimized.Checked;
            m_Settings.IsAdvancedMode = chAdvanced.Checked;
            m_Settings.RefreshTimeInSec = Int32.Parse(eRefreshTime.Value.ToString()) * 60;
            MainForm.GetInstance().AdminMode = m_Settings.IsAdvancedMode;
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
