using System.Windows.Forms;

namespace LanExchange.SDK
{
    /// <summary>
    /// View of LanExchange Params form.
    /// </summary>
    public interface ISettingsView
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is autorun.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is autorun; otherwise, <c>false</c>.
        /// </value>
        bool IsAutoRun { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [run minimized].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [run minimized]; otherwise, <c>false</c>.
        /// </value>
        bool RunMinimized { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [advanced mode].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [advanced mode]; otherwise, <c>false</c>.
        /// </value>
        bool AdvancedMode { get; set; }
        TreeNode AddTab(TreeNode parentNode, string title, ISettingsTabViewFactory tabFactory);
        void SelectFirstNode();
    }
}
