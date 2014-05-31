using System.Windows.Forms;

namespace LanExchange.Plugin.WinForms.Interfaces
{
    public interface IListViewItemGetter
    {
        ListViewItem GetListViewItemAt(int index);
    }
}