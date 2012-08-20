using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.SDK.SDKModel.VO
{
    public struct ColumnVO
    {
        private string m_Title;
        private int m_Width;

        public ColumnVO(string title, int width)
        {
            m_Title = title;
            m_Width = width;
        }

        public string Title
        {
            get { return m_Title; }
            set { m_Title = value; }
        }

        public int Width
        {
            get { return m_Width; }
            set { m_Width = value; }
        }
    }
}
