using System.Collections.Generic;

namespace LanExchange.SDK
{
    public abstract class PanelFillerBase
    {
        public abstract bool IsParentAccepted(PanelItemBase parent);

        /// <summary>
        /// This method should return known items instantly.
        /// For filling items from remote source use <see cref="AsyncFill"/>.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="result"></param>
        public abstract void SyncFill(PanelItemBase parent, ICollection<PanelItemBase> result);

        /// <summary>
        /// This method will be called asynchronous and can get items from remote source.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="result"></param>
        public abstract void AsyncFill(PanelItemBase parent, ICollection<PanelItemBase> result);
    }
}
