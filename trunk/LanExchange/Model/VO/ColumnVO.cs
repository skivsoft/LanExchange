using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.Model.VO
{
    public struct ColumnVO
    {
        private string m_Title;
        private int m_Width;
        private bool m_Visible;

        public ColumnVO(string title, int width, bool visible)
        {
            m_Title = title;
            m_Width = width;
            m_Visible = visible;
        }

        public ColumnVO(string title, int width)
        {
            m_Title = title;
            m_Width = width;
            m_Visible = true;
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

        public bool Visible
        {
            get { return m_Visible; }
            set { m_Visible = value; }
        }
    }
}
