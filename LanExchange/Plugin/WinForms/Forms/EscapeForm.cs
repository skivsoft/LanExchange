using System.Windows.Forms;
using LanExchange.SDK;

namespace LanExchange.Plugin.WinForms.Forms
{
    public partial class EscapeForm : Form
    {
        public EscapeForm()
        {
            InitializeComponent();
            SetupRtl();
        }

        private void SetupRtl()
        {
            if (App.TR != null && App.TR.RightToLeft)
            {
                RightToLeftLayout = true;
                RightToLeft = RightToLeft.Yes;
            }
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
