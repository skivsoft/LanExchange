using System.Windows.Forms;

namespace LanExchange.SDK
{
    /// <summary>
    /// View of LanExchange Params form.
    /// </summary>
    public interface ISettingsView
    {
        TreeNode AddTab(TreeNode parentNode, string title, ISettingsTabViewFactory tabFactory);
        void SelectFirstNode();
    }
}
