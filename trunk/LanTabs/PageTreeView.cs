using System.Windows.Forms;

namespace LanTabs
{
    public partial class PageTreeView : UserControl, IPageTreeView
    {
        public PageTreeView()
        {
            InitializeComponent();
        }

        public IPagesPresenter Pages { get; set; }


        public string Title
        {
            get { return "Tree Name"; }
        }
    }
}
