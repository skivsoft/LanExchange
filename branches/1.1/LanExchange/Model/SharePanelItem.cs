using System;
using LanExchange.Utils;

namespace LanExchange.Model
{
    public class SharePanelItem : PanelItem
    {
        public const int imgHiddenFolder  = 6;
        public const int imgNormalFolder  = 7;
        public const int imgPrinterFolder = 8;
        public const int imgBackFolder    = 9;

        private string m_Name;

        public SharePanelItem(string share_name, string share_comment, uint share_type, string computer_name)
        {
            m_Name = share_name;
            Comment = share_comment;
            ShareType = share_type;
            ComputerName = computer_name;
        }

        protected override string GetName()
        {
            return m_Name;
        }

        protected override void SetName(string value)
        {
            m_Name = value;
        }

        public override string Comment { get; set; }

        public uint ShareType { get; set; }

        public string ComputerName { get; set; }

        protected override int GetImageIndex()
        {
            if (String.IsNullOrEmpty(m_Name))
                return imgBackFolder;
            else
            if (IsPrinter)
                return imgPrinterFolder;
            else
                return IsHidden ? imgHiddenFolder : imgNormalFolder;
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
                else
                    return false;
            }
        }

        public override string[] GetSubItems()
        {
            return new string[2] { "", Comment };
        }

        /// <summary>
        /// Сортировка: сначала каталоги, затем принтеры.
        /// </summary>
        /// <param name="p2"></param>
        /// <returns></returns>
        public override int CompareTo(PanelItem p2)
        {
            bool b2 = (p2 as SharePanelItem).IsPrinter;
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
