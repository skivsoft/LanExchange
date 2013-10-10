using System.Windows.Forms;

namespace LanTabs
{
    public partial class PageListView : UserControl, IPageListView
    {
        public PageListView()
        {
            InitializeComponent();
        }

        public IPagesPresenter Pages { get; set; }


        public string Title
        {
            get { return "List Name"; }
        }
    }
}
