using System.Windows.Forms;

namespace LanExchange.Plugin.WinForms.Impl
{
    public class SubMenuAdapter
    {
        private readonly ContextMenuStrip subMenu1;
        private readonly ToolStripMenuItem subMenu2;

        private SubMenuAdapter(object subMenu)
        {
            subMenu1 = subMenu as ContextMenuStrip;
            subMenu2 = subMenu as ToolStripMenuItem;
        }

        public static SubMenuAdapter CreateFrom(object subMenu)
        {
            if (subMenu is ContextMenuStrip || subMenu is ToolStripMenuItem)
                return new SubMenuAdapter(subMenu);
            return null;
        }

        public object Tag
        {
            get { return subMenu1 != null ? subMenu1.Tag : subMenu2.Tag; }
            set
            {
                if (subMenu1 != null)
                    subMenu1.Tag = value;
                else
                    subMenu2.Tag = value;
            }
        }

        public ToolStripItemCollection Items
        {
            get { return subMenu1 != null ? subMenu1.Items : subMenu2.DropDownItems; }
        }
    }
}
