using System;
using System.Windows.Forms;
using LanExchange.Presenter;
using LanExchange.SDK;

namespace LanExchange.UI
{
    public partial class SettingsForm : EscapeForm, ISettingsView
    {
        /// <summary>
        /// This field for external use.
        /// </summary>
        public static SettingsForm Instance;

        private readonly SettingsPresenter m_Presenter;

        public SettingsForm()
        {
            InitializeComponent();
            m_Presenter = new SettingsPresenter(this);
            m_Presenter.LoadFromModel();
            tvSettings.ExpandAll();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            m_Presenter.SaveToModel();
            Close();
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Instance = null;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public TreeNode AddTab(TreeNode parentNode, string title, Type tabView)
        {
            //var node = parentNode == null ? tvSettings.Nodes.Add(title) : parentNode.Nodes.Add(title);
            //node.Tag = new SettingsTabInstance { Factory = tabFactory };
            //return node;
            return null;
        }

        private void tvSettings_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //var tabInstance = (SettingsTabInstance) e.Node.Tag;
            //if (tabInstance.Instance == null)
            //    tabInstance.Instance = tabInstance.Factory.Create();
            //lTop.Text = e.Node.Text;
            //pContent.Controls.Clear();
            //pContent.Controls.Add((Control)tabInstance.Instance);
        }


        public void SelectFirstNode()
        {
            if (tvSettings.Nodes.Count > 0)
                tvSettings.SelectedNode = tvSettings.Nodes[0];
        }
    }
}
