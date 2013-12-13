using System.Collections.Generic;

namespace LanExchange.SDK
{
    public class PanelFillerBase
    {
        public virtual bool IsParentAccepted(PanelItemBase parent)
        {
            return false;
        }

        /// <summary>
        /// This method should return known items instantly.
        /// For filling items from remote source use <see cref="Fill"/>.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="result"></param>
        public virtual void SyncFill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
        }

        /// <summary>
        /// This method will be called asynchronous and can get items from remote source.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="result"></param>
        public virtual void Fill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {

        }
    }
}
