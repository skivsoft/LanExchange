using System.Collections.Generic;

namespace LanExchange.Sdk
{
    /// <summary>
    /// LanExchange panel model.
    /// </summary>
    public interface IPanelModel : ISubscriber, IFilterModel
    {
        /// <summary>
        /// Gets or sets the name of the tab.
        /// </summary>
        /// <value>
        /// The name of the tab.
        /// </value>
        string TabName { get; set; }
        /// <summary>
        /// Gets or sets the current view.
        /// </summary>
        /// <value>
        /// The current view.
        /// </value>
        PanelViewMode CurrentView { get; set; }
        /// <summary>
        /// Gets or sets the groups.
        /// </summary>
        /// <value>
        /// The groups.
        /// </value>
        IList<ISubject> Groups { get; }
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        int Count { get; }
        /// <summary>
        /// Gets a value indicating whether this instance has back item.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has back item; otherwise, <c>false</c>.
        /// </value>
        bool HasBackItem { get; }
        /// <summary>
        /// Gets the current path.
        /// </summary>
        /// <value>
        /// The current path.
        /// </value>
        ObjectPath CurrentPath { get; }
        /// <summary>
        /// Gets or sets the focused item text.
        /// </summary>
        /// <value>
        /// The focused item text.
        /// </value>
        string FocusedItemText { get; set; }
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        IList<PanelItemBase> Items { get; }
        /// <summary>
        /// Gets the tool tip text.
        /// </summary>
        /// <value>
        /// The tool tip text.
        /// </value>
        string ToolTipText { get; }
        /// <summary>
        /// Updates the subscription.
        /// </summary>
        void UpdateSubscription();
        /// <summary>
        /// Gets at.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        PanelItemBase GetItemAt(int index);
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        PanelItemBase GetItem(string key);
        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        int IndexOf(string key);
    }
}
