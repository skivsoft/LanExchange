using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using LanExchange.Model.VO;
using LanExchange.Model;
using LanExchange.OSLayer;
using LanExchange;
using BrightIdeasSoftware;

namespace LanExchange.Model
{
    public class LanShareProxy : PanelItemProxy
    {
        public new const string NAME = "LanShareProxy";

        public LanShareProxy(IEnumObjectsStrategy strategy)
            : base(NAME, strategy)
        {
        }

        public override int NumObjects
        {
            get
            {
                return base.NumObjects - 1;
            }
        }

        public override OLVColumn[] GetColumns()
        {
            OLVColumn[] Result = new OLVColumn[] { 
                new OLVColumn(AppFacade.T("ColumnShareName"), "Name"),
                new OLVColumn("*:", "MappedLetter"),
                new OLVColumn(AppFacade.T("ColumnComment"), "Comment")
            };
            Result[0].Width = 130;
            Result[2].Width = 250;
            return Result;
        }
    }

    public class NetApi32ShareEnumStrategy : IEnumObjectsStrategy
    {
        public IList<PanelItemVO> EnumObjects(string server)
        {
            List<PanelItemVO> Objects = new List<PanelItemVO>();
            NetApi32.SHARE_INFO_1 shi1;
            shi1.shi1_remark = server;
            Objects.Add(new PanelItemVO(null, null));
            int entriesread = 0;
            int totalentries = 0;
            int resume_handle = 0;
            int nStructSize = Marshal.SizeOf(typeof(NetApi32.SHARE_INFO_1));
            IntPtr bufPtr = IntPtr.Zero;
            StringBuilder Server = new StringBuilder(server);
            int ret = NetApi32.NetShareEnum(Server, 1, ref bufPtr, NetApi32.MAX_PREFERRED_LENGTH, ref entriesread, ref totalentries, ref resume_handle);
            if (ret == NetApi32.NERR_Success)
            {
                IntPtr currentPtr = bufPtr;
                for (int i = 0; i < entriesread; i++)
                {
                    shi1 = (NetApi32.SHARE_INFO_1)Marshal.PtrToStructure(currentPtr, typeof(NetApi32.SHARE_INFO_1));
                    //if ((shi1.shi1_type & (uint)NetApi32.SHARE_TYPE.STYPE_IPC) != (uint)NetApi32.SHARE_TYPE.STYPE_IPC)
                        Objects.Add(new PanelItemVO(shi1.shi1_netname, shi1));
                    //Result.Add(new TShareItem(shi1.shi1_netname, shi1.shi1_remark, shi1.shi1_type, Server));
                    currentPtr = new IntPtr(currentPtr.ToInt32() + nStructSize);
                }
                NetApi32.NetApiBufferFree(bufPtr);
            }
            return Objects;
        }
    }
}
