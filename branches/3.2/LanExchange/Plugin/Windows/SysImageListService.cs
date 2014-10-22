using System.Drawing;
using LanExchange.Plugin.Windows.Utils;
using LanExchange.SDK;

namespace LanExchange.Plugin.Windows
{
    internal class SysImageListService : ISysImageListService
    {
        private SysImageList m_ImageList;

        public void Create(SysImageListSize size)
        {
            m_ImageList = new SysImageList(size);
        }

        public void Dispose()
        {
            if (m_ImageList != null)
                m_ImageList.Dispose();
        }

        public Size Size
        {
            get { return m_ImageList == null ? Size.Empty : m_ImageList.Size; }
        }

        public Icon GetIcon(int index)
        {
            if (m_ImageList == null)
                return null;
            return m_ImageList.Icon(index);
        }

        public int GetIconIndex(string fileName)
        {
            return m_ImageList == null ? -1 : m_ImageList.IconIndex(fileName, false);
        }
    }
}
