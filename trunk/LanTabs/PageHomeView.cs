using System.Windows.Forms;

namespace LanTabs
{
    public partial class PageHomeView : UserControl, IPageHomeView
    {
        public PageHomeView()
        {
            InitializeComponent();
            Text = "New Tab";
        }

        public IPagesPresenter Pages { get; set; }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var listView = App.Resolve<IPageListView>();
            Pages.AssignTab(Pages.SelectedIndex, listView);
        }


        private void button2_Click(object sender, System.EventArgs e)
        {
            var treeView = App.Resolve<IPageTreeView>();
            Pages.AssignTab(Pages.SelectedIndex, treeView);
        }


        public string Title
        {
            get { return "New Tab"; }
        }
    }
}
