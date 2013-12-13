using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using LanExchange.OS.Windows.Utils;
using LanExchange.SDK;

namespace LanExchange.OS.Windows
{
    internal class SysImageListService : ISysImageListService
    {
        private SysImageList m_ImageList;

        public SysImageListService()
        {
            
        }

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
            get { return m_ImageList == null ? new Size(0, 0) : m_ImageList.Size; }
        }

        public Icon GetIcon(int index)
        {
            if (m_ImageList == null)
                return null;
            return m_ImageList.Icon(index);
        }

        public int GetIconIndex(string fileName)
        {
            return m_ImageList == null ? -1 : m_ImageList.IconIndex(fileName);
        }
    }
}
