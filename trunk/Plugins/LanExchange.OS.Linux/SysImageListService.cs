using System.Drawing;
using LanExchange.SDK;

namespace LanExchange.OS.Linux
{
    public class SysImageListService : ISysImageListService
    {
        private SysImageListSize m_Size;

        public SysImageListService()
        {
            m_Size = SysImageListSize.SmallIcons;
        }

        public void Create(SysImageListSize size)
        {
            m_Size = size;
        }

        public void Dispose()
        {
        }

        public Size Size
        {
            get
            {
                switch (m_Size)
                {
                    case SysImageListSize.SmallIcons: return new Size(16, 16);
                    case SysImageListSize.LargeIcons: return new Size(32, 32);
                    case SysImageListSize.ExtraLargeIcons: return new Size(48, 48);
                    default: return Size.Empty;
                }
            }
        }

        public Icon GetIcon(int index)
        {
            return null;
        }

        public int GetIconIndex(string fileName)
        {
            return -1;
        }
    }
}
