using System.Collections.Generic;

namespace LanExchange.Presentation.Interfaces
{
    public interface IPanelFiller
    {
        bool IsParentAccepted(PanelItemBase parent);

        /// <summary>
        /// This method will be called asynchronous and can get items from remote source.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="result"></param>
        void AsyncFill(PanelItemBase parent, ICollection<PanelItemBase> result);
    }
}
