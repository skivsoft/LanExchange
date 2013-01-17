using System;
using LanExchange.Utils;

namespace LanExchange.Model
{
    public abstract class SharePanelItem : AbstractPanelItem, IComparable<SharePanelItem>
    {
        private readonly ShareInfo m_SHI;
        private string m_Name;
        private string m_Comment;

        /// <summary>
        /// Panel item for network shared resource.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="shi"></param>
        public SharePanelItem(ShareInfo shi)
        {
            if (shi == null)
                throw new ArgumentNullException("shi");
            m_SHI = shi;
            m_Name = m_SHI.Name;
            m_Comment = m_SHI.Comment;
        }

        public override string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public override string Comment
        {
            get { return m_Comment; }
            set { m_Comment = value; }
        }

        public uint ShareType { get; set; }

        public override int ImageIndex
        {
            get
            {
                if (String.IsNullOrEmpty(m_Name))
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
        
        public override string[] GetSubItems()
        {
            return new[] { "", Comment };
        }

        public int CompareTo(SharePanelItem other)
        {
            if (other == null) return 1;
            bool b2 = other.SHI.IsPrinter;
            int Result;
            if (SHI.IsPrinter)
                if (b2)
                    Result = base.CompareTo(other);
                else
                    Result = +1;
            else
                if (b2)
                    Result = -1;
                else
                    Result = base.CompareTo(other);
            return Result;
        }

        public ComputerPanelItem Computer { get; set; }

        public string ComputerName
        {
            get
            {
                return Computer == null ? String.Empty : Computer.Name;
            }
        }

    }
}
