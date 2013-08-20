using System;
using System.Windows.Forms;

namespace WMIViewer
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
            //var index = 0;
            foreach (string str in WMIClassList.Instance.AllClasses)
            {
                if (WMIClassList.Instance.Classes.Contains(str) || WMIClassList.Instance.ReadOnlyClasses.Contains(str))
                    LB2.Items.Add(str);
                else
                    LB1.Items.Add(str);
                //index++;
            }
        }

        private void LB_SelectedIndexChanged(object sender, EventArgs e)
        {
            var checkBox = (sender as ListBox);
            if (checkBox == null) return;
            if (checkBox.SelectedIndex == -1) return;
            var className = checkBox.Items[checkBox.SelectedIndex].ToString();
            lClassName.Text = className;
            lDescription.Text = WMIClassList.Instance.GetClassDescription(null, className);
            //lDescription.Text = WMIClassList.Instance.GetClassQualifiers(null, className);
        }
    }
}
