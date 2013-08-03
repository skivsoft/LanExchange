using System.Windows.Forms;
using System;

namespace LanExchange.SDK
{
    /// <summary>
    /// View of LanExchange Params form.
    /// </summary>
    public interface ISettingsView
    {
        TreeNode AddTab(TreeNode parentNode, string title, Type tabView);
        void SelectFirstNode();
    }
}
