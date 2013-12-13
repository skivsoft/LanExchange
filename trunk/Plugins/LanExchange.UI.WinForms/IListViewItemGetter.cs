using System.Windows.Forms;

namespace LanExchange.UI.WinForms
{
    public interface IListViewItemGetter
    {
        ListViewItem GetListViewItemAt(int index);
    }
}