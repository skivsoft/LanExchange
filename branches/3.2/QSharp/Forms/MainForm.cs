using System.Windows.Forms;
using LanExchange.Controls;

namespace LanExchange.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LV.VirtualListSize = 1000;
        }

        private void LV_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem("Item No. " + e.ItemIndex);
            e.Item.SubItems.Add(string.Empty);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                Close();
                e.Handled = true;
            }
        }
    }
}
