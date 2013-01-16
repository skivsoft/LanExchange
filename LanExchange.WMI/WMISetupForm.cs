using System.Windows.Forms;

namespace LanExchange.WMI
{
    public partial class WMISetupForm : Form
    {
        public WMISetupForm()
        {
            InitializeComponent();
        }

        private void WMISetupForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                e.Handled = true;
            }
        }
    }
}
