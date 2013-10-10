using System.Windows.Forms;

namespace LanTabs
{
    public partial class Form1 : Form
    {
        private readonly PagesView m_Pages;

        public Form1()
        {
            InitializeComponent();
            // init Pages presenter
            m_Pages = (PagesView)App.Resolve<IPagesView>();
            m_Pages.Dock = DockStyle.Fill;
            Controls.Add(m_Pages);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                Close();
                e.Handled = true;
            }
        }
    }
}
