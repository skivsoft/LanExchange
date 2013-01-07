using System;
using System.Windows.Forms;

namespace LanExchange
{
    public partial class LegendForm : Form
    {
        public LegendForm()
        {
            InitializeComponent();
        }

        private void LegendForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                e.Handled = true;
            }
        }

        private void LegendForm_Load(object sender, EventArgs e)
        {
            lvLegend.SelectedIndices.Add(0);
            ActiveControl = lvLegend;
            lvLegend.SmallImageList = MainForm.MainFormInstance.ilSmall;
        }
    }
}
