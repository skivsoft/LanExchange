using System;
using LanExchange.Utils;

namespace LanExchange.Model
{
    public abstract class SharePanelItem : PanelItem
    {
        private string m_Name;

        public SharePanelItem(string shareName, string shareComment, uint shareType, string computerName)
        {
            m_Name = shareName;
            Comment = shareComment;
            ShareType = shareType;
            ComputerName = computerName;
        }

        protected override string GetName()
        {
            return m_Name;
        }

        protected override void SetName(string value)
        {
            m_Name = value;
        }

        public override sealed string Comment { get; set; }

        public uint ShareType { get; set; }

        public string ComputerName { get; set; }

        protected override int GetImageIndex()
        {
            if (String.IsNullOrEmpty(m_Name))
                return LanExchangeIcons.FolderBack;
            if (IsPrinter)
                return LanExchangeIcons.FolderPrinter;
            return IsHidden ? LanExchangeIcons.FolderHidden : LanExchangeIcons.FolderNormal;
        }

        public bool IsPrinter
        {
            get
            {
                return (ShareType == (uint)NetApi32.SHARE_TYPE.STYPE_PRINTQ);
            }
        }

        public bool IsHidden
        {
            get
            {
                if (!String.IsNullOrEmpty(m_Name))
                    return m_Name[m_Name.Length - 1] == '$';
                return false;
            }
        }

        public override string[] GetSubItems()
        {
            return new[] { "", Comment };
        }

        /// <summary>
        /// Сортировка: сначала каталоги, затем принтеры.
        /// </summary>
        /// <param name="p2"></param>
        /// <returns></returns>
        public override int CompareTo(PanelItem p2)
        {
            var shareItem = (p2 as SharePanelItem);
            if (shareItem == null) return 1;
            bool b2 = shareItem.IsPrinter;
            int Result;
            if (IsPrinter)
                if (b2)
                    Result = base.CompareTo(p2);
                else
                    Result = +1;
            else
                if (b2)
                    Result = -1;
                else
                    Result = base.CompareTo(p2);
            return Result;
        }
    }
}
