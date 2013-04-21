using System;
using System.Collections.Generic;

namespace LanExchange.Sdk
{
    /// <summary>
    /// View of LanExchange TabParams form.
    /// </summary>
    public interface ITabSettingView : IDisposable
    {
        /// <summary>
        /// Occurs when [ok clicked].
        /// </summary>
        event EventHandler OkClicked;
        /// <summary>
        /// Occurs when [closed].
        /// </summary>
        event EventHandler Closed;
        /// <summary>
        /// Gets or sets a value indicating whether [selected checked].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [selected checked]; otherwise, <c>false</c>.
        /// </value>
        bool SelectedChecked { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [dont scan checked].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [dont scan checked]; otherwise, <c>false</c>.
        /// </value>
        bool DoNotScanChecked { get; set; }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        string Text { get; set; }
        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <value>
        /// The groups.
        /// </value>
        IEnumerable<string> Groups { get; }
        /// <summary>
        /// Gets the domains count.
        /// </summary>
        /// <value>
        /// The domains count.
        /// </value>
        int DomainsCount { get; }
        /// <summary>
        /// Gets or sets the domains focused text.
        /// </summary>
        /// <value>
        /// The domains focused text.
        /// </value>
        string DomainsFocusedText { get; set; }
        /// <summary>
        /// Gets the checked list.
        /// </summary>
        /// <value>
        /// The checked list.
        /// </value>
        IList<string> CheckedList { get; }
        /// <summary>
        /// Shows the modal.
        /// </summary>
        /// <returns></returns>
        bool ShowModal();
        /// <summary>
        /// Domainses the clear.
        /// </summary>
        void DomainsClear();
        /// <summary>
        /// Domainses the add.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="checkedItem">if set to <c>true</c> [checked item].</param>
        void DomainsAdd(string value, bool checkedItem);
    }
}
