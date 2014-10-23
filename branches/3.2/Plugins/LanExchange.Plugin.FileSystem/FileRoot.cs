using LanExchange.Plugin.FileSystem.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.FileSystem
{
    /// <summary>
    /// Root panel item for FileSystem objects.
    /// </summary>
    public class FileRoot : PanelItemRootBase
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns></returns>
        protected override string GetName()
        {
            return Resources.Computer;
        }

        /// <summary>
        /// Gets the name of the image.
        /// </summary>
        /// <value>
        /// The name of the image.
        /// </value>
        public override string ImageName
        {
            get { return PanelImageNames.COMPUTER; }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new FileRoot();
        }
    }
}
