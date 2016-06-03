using System.Drawing;

namespace LanExchange.Presentation.Interfaces
{
    /// <summary>
    /// LanExchange panel image manager.
    /// </summary>
    public interface IImageManager
    {
        /// <summary>
        /// Registers the image.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="imageSmall">The image small.</param>
        /// <param name="imageLarge">The image large.</param>
        void RegisterImage(string name, Image imageSmall, Image imageLarge);
        /// <summary>
        /// Registers the disabled image.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="imageSmall">The image small.</param>
        /// <param name="imageLarge">The image large.</param>
        void RegisterDisabledImage(string name, Image imageSmall, Image imageLarge);
        /// <summary>
        /// Unregister image.
        /// </summary>
        /// <param name="name">The name.</param>
        void UnregisterImage(string name);
        /// <summary>
        /// Gets index by image name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The index of image in internal ImageList.</returns>
        int IndexOf(string name);
        /// <summary>
        /// Gets the small image.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Image GetSmallImage(string key);
        /// <summary>
        /// Gets the large image.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Image GetLargeImage(string key);
        /// <summary>
        /// Gets the small icon.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Icon GetSmallIcon(string key);
        /// <summary>
        /// Gets the large icon.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Icon GetLargeIcon(string key);
        Image GetSmallImageOfFileName(string fileName);
        Image GetLargeImageOfFileName(string fileName);
        void SetImagesTo(object control);
    }
}