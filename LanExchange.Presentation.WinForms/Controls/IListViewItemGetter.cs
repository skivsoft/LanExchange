using System.Windows.Forms;

namespace LanExchange.Presentation.WinForms.Controls
{
    public interface IListViewItemGetter
    {
        ListViewItem GetListViewItemAt(int index);
    }
}