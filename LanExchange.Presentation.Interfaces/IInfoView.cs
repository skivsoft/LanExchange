namespace LanExchange.Presentation.Interfaces
{
    /// <summary>
    /// View of top info panel.
    /// </summary>
    public interface IInfoView : IView
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IInfoView"/> is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if visible; otherwise, <c>false</c>.
        /// </value>
        bool Visible { get; set; }

        int NumLines { get; set; }
        string GetLine(int index);
        void SetLine(int index, string text);

        //TODO hide model use events
        //IPanelItem CurrentItem { get; set; }
    }
}
