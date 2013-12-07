using System.Windows.Forms;

namespace LanExchange.UI
{
    public interface IListViewItemGetter
    {
        ListViewItem GetListViewItemAt(int index);
    }
}