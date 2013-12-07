using System.Windows.Forms;

namespace LanExchange.UI.WinForms
{
    public partial class EscapeForm : Form
    {
        public EscapeForm()
        {
            InitializeComponent();
        }

        private void EscapeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
                e.Handled = true;
            }
        }
    }
}
