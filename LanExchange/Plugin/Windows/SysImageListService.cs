using System.Drawing;
using LanExchange.Plugin.Windows.Utils;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Windows
{
    internal class SysImageListService : ISysImageListService
    {
        private SysImageList imageList;

        public void Create(SysImageListSize size)
        {
            imageList = new SysImageList(size);
        }

        public void Dispose()
        {
            if (imageList != null)
                imageList.Dispose();
        }

        public Size Size
        {
            get { return imageList == null ? Size.Empty : imageList.Size; }
        }

        public Icon GetIcon(int index)
        {
            if (imageList == null)
                return null;
            return imageList.Icon(index);
        }

        public int GetIconIndex(string fileName)
        {
            return imageList == null ? -1 : imageList.IconIndex(fileName);
        }
    }
}
