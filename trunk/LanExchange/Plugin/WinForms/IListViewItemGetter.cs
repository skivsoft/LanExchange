using System.Windows.Forms;

namespace LanExchange.Plugin.WinForms
{
    public interface IListViewItemGetter
    {
        ListViewItem GetListViewItemAt(int index);
    }
}