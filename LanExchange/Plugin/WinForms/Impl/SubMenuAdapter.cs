using System.Windows.Forms;

namespace LanExchange.Plugin.WinForms.Impl
{
    public class SubMenuAdapter
    {
        private readonly ContextMenuStrip m_SubMenu1;
        private readonly ToolStripMenuItem m_SubMenu2;

        private SubMenuAdapter(object subMenu)
        {
            m_SubMenu1 = subMenu as ContextMenuStrip;
            m_SubMenu2 = subMenu as ToolStripMenuItem;
        }

        public static SubMenuAdapter CreateFrom(object subMenu)
        {
            if (subMenu is ContextMenuStrip || subMenu is ToolStripMenuItem)
                return new SubMenuAdapter(subMenu);
            return null;
        }

        public object Tag
        {
            get { return m_SubMenu1 != null ? m_SubMenu1.Tag : m_SubMenu2.Tag; }
            set
            {
                if (m_SubMenu1 != null)
                    m_SubMenu1.Tag = value;
                else
                    m_SubMenu2.Tag = value;
            }
        }

        public ToolStripItemCollection Items
        {
            get { return m_SubMenu1 != null ? m_SubMenu1.Items : m_SubMenu2.DropDownItems; }
        }
    }
}
