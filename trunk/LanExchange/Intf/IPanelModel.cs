using System;
using System.Collections.Generic;
using LanExchange.Model.Settings;
using LanExchange.SDK;

namespace LanExchange.Intf
{
    /// <summary>
    /// LanExchange panel model.
    /// </summary>
    public interface IPanelModel : IFilterModel, IEquatable<IPanelModel>
    {
        event EventHandler Changed;
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
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        int Count { get; }
        /// <summary>
        /// Gets the current path.
        /// </summary>
        /// <value>
        /// The current path.
        /// </value>
        ObjectPath<PanelItemBase> CurrentPath { get; }
        /// <summary>
        /// Gets or sets the focused item text.
        /// </summary>
        /// <value>
        /// The focused item text.
        /// </value>
        PanelItemBase FocusedItem { get; set; }
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
        /// Gets at.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        PanelItemBase GetItemAt(int index);
        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        int IndexOf(PanelItemBase key);

        PanelItemFactoryBase ItemFactory { get; set; }
        void SyncRetrieveData(bool clearFilter = false);
        string DataType { get; set; }
        ColumnComparer Comparer { get; }
        void Sort(IComparer<PanelItemBase> sorter);

        bool Contains(PanelItemBase panelItem);

        void SetDefaultRoot(PanelItemBase root);

        string GetImageName();
    }
}