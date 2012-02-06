using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Network
{
    /// <summary>
    /// Модель "Общий ресурс"
    /// </summary>
    public class TShareItem : TPanelItem
    {
        public const int imgHiddenFolder = 6;
        public const int imgNormalFolder = 7;
        public const int imgPrinterFolder = 8;

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
            if (IsPrinter)
                return imgPrinterFolder;
            else
                return IsHidden ? imgHiddenFolder : imgNormalFolder;
        }

        public bool IsPrinter
        {
            get
            {
                return (share_type == (uint)LocalNetwork.SHARE_TYPE.STYPE_PRINTQ);
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
    }

    public static List<TPanelItem> EnumNetShares(string Server)
    {
        List<TPanelItem> Result = new List<TPanelItem>();
        Result.Add(new TShareItem("..", @"\\" + Server, 0, Server));
        int entriesread = 0;
        int totalentries = 0;
        int resume_handle = 0;
        int nStructSize = Marshal.SizeOf(typeof(LocalNetwork.SHARE_INFO_1));
        IntPtr bufPtr = IntPtr.Zero;
        StringBuilder server = new StringBuilder(Server);
        TLogger.Print("WINAPI NetShareEnum");
        int ret = LocalNetwork.NetShareEnum(server, 1, ref bufPtr, LocalNetwork.MAX_PREFERRED_LENGTH, ref entriesread, ref totalentries, ref resume_handle);
        if (ret == LocalNetwork.NERR_Success)
        {
            TLogger.Print("WINAPI NetServerEnum result: entriesread={0}, totalentries={1}", entriesread, totalentries);
            IntPtr currentPtr = bufPtr;
            for (int i = 0; i < entriesread; i++)
            {
                LocalNetwork.SHARE_INFO_1 shi1 = (LocalNetwork.SHARE_INFO_1)Marshal.PtrToStructure(currentPtr, typeof(LocalNetwork.SHARE_INFO_1));
                if ((shi1.shi1_type & (uint)LocalNetwork.SHARE_TYPE.STYPE_IPC) != (uint)LocalNetwork.SHARE_TYPE.STYPE_IPC)
                    Result.Add(new TShareItem(shi1.shi1_netname, shi1.shi1_remark, shi1.shi1_type, Server));
                else
                    TLogger.Print("Skiping IPC$ share");
                currentPtr = new IntPtr(currentPtr.ToInt32() + nStructSize);
            }
            LocalNetwork.NetApiBufferFree(bufPtr);
        }
        else
        {
            TLogger.Print("WINAPI NetServerEnum error: {0}", ret);
        }

        TPanelItemComparer comparer = new TPanelItemComparer();
        Result.Sort(comparer);
        return Result;
    }

}
