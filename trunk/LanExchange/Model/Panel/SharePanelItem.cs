using System;
using LanExchange.Utils;

namespace LanExchange.Model.Panel
{
    public class SharePanelItem : AbstractPanelItem//, IComparable<SharePanelItem>
    {
        private readonly ShareInfo m_SHI;

        /// <summary>
        /// Panel item for network shared resource.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public SharePanelItem(AbstractPanelItem parent, ShareInfo shi) : base(parent)
        {
            if (shi == null)
                throw new ArgumentNullException("shi");
            m_SHI = shi;
            Comment = m_SHI.Comment;
        }

        public SharePanelItem(AbstractPanelItem parent, string name) : base(parent)
        {
            m_SHI = new ShareInfo(new NetApi32.SHARE_INFO_1 {shi1_netname = name});
            Comment = string.Empty;
        }

        public override int ImageIndex
        {
            get
            {
                if (Name == BACK)
                    return LanExchangeIcons.FolderBack;
                if (SHI.IsPrinter)
                    return LanExchangeIcons.FolderPrinter;
                return SHI.IsHidden ? LanExchangeIcons.FolderHidden : LanExchangeIcons.FolderNormal;
            }
        }

        public ShareInfo SHI
        {
            get { return m_SHI; }
        }
        
        public override IComparable this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: 
                        return Name;
                    case 1:
                        return Comment;
                    default:
                        throw new ArgumentOutOfRangeException("index");
                }
            }
        }

        //public int CompareTo(SharePanelItem other)
        //{
        //    if (other == null) return 1;
        //    bool b2 = other.SHI.IsPrinter;
        //    int Result;
        //    if (SHI.IsPrinter)
        //        if (b2)
        //            Result = base.CompareTo(other);
        //        else
        //            Result = +1;
        //    else
        //        if (b2)
        //            Result = -1;
        //        else
        //            Result = base.CompareTo(other);
        //    return Result;
        //}

        public string ComputerName
        {
            get
            {
                var comp = Parent as ComputerPanelItem;
                return comp != null ? comp.Name : String.Empty;
            }
        }

        public override string Name 
        { 
            get { return m_SHI.Name; }
            set { m_SHI.Name = value; }
        }

        public string Comment { get; set; }
        public uint ShareType { get; set; }

        public override int CountColumns
        {
            get { return 2; }
        }

        public override string ToolTipText
        {
            get { return String.Empty; }
        }

        public override IPanelColumnHeader CreateColumnHeader(int index)
        {
            //IPanelColumnHeader result = new ColumnHeaderEx();
            //result.SetVisible(true);
            //switch (index)
            //{
            //    case 0:
            //        result.Text = "Общий ресурс";
            //        break;
            //    case 1:
            //        result.Text = "Описание";
            //        break;
            //}
            //return result;
            return null;
        }
    }
}
