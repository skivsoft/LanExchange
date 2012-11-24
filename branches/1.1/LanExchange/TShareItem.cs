using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using OSTools;
using System.Net;
using LanExchange.Network;

namespace LanExchange
{
    public class TShareItem : TPanelItem
    {
        public const int imgHiddenFolder  = 6;
        public const int imgNormalFolder  = 7;
        public const int imgPrinterFolder = 8;
        public const int imgBackFolder    = 9;

        private string share_name;
        private string share_comment;
        private uint share_type;
        private string computer_name;

        public TShareItem(string share_name, string share_comment, uint share_type, string computer_name)
        {
            this.share_name = share_name;
            this.share_comment = share_comment;
            this.share_type = share_type;
            this.computer_name = computer_name;
        }

        protected override string GetName()
        {
            return share_name;
        }

        protected override void SetName(string value)
        {
            share_name = value;
        }

        protected override string GetComment()
        {
            return this.share_comment;
        }

        protected override void SetComment(string value)
        {
            this.share_comment = value;
        }

        public uint Type
        {
            get { return share_type; }
            set { share_type = value; }
        }

        public string ComputerName
        {
            get { return computer_name; }
            set { computer_name = value; }
        }

        protected override int GetImageIndex()
        {
            if (String.IsNullOrEmpty(share_name))
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
                return (share_type == (uint)NetApi32.SHARE_TYPE.STYPE_PRINTQ);
            }
        }

        public bool IsHidden
        {
            get
            {
                if (!String.IsNullOrEmpty(share_name))
                    return share_name[share_name.Length - 1] == '$';
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
        public override int CompareTo(TPanelItem p2)
        {
            bool b2 = (p2 as TShareItem).IsPrinter;
            int Result;
            if (this.IsPrinter)
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
