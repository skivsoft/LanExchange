using System;
using System.Windows.Forms;

namespace LanExchange.WMI
{
    public partial class WMISetupForm : Form
    {
        //public WMIPresenter Presenter { get; set; }

        public WMISetupForm()
        {
            InitializeComponent();
            // Enable double buffer for CheckedListBox


        }

        private void WMISetupForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                e.Handled = true;
            }
        }

        public void PrepareForm()
        {
            LB1.Items.Clear();
            var index = 0;
            foreach (string str in WMIClassList.Instance.AllClasses)
            {
                if (WMIClassList.Instance.Classes.Contains(str) || WMIClassList.Instance.ReadOnlyClasses.Contains(str))
                    LB2.Items.Add(str);
                else
                    LB1.Items.Add(str);
                index++;
            }
        }

        private void LB_SelectedIndexChanged(object sender, EventArgs e)
        {
            var chLB = (sender as ListBox);
            if (chLB == null) return;
            if (chLB.SelectedIndex == -1) return;
            var className = chLB.Items[chLB.SelectedIndex].ToString();
            lClassName.Text = className;
            lDescription.Text = WMIClassList.Instance.GetClassDescription(null, className);
            //lDescription.Text = WMIClassList.Instance.GetClassQualifiers(null, className);
        }
    }
}
