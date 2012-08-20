using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model.VO;
using System.Runtime.InteropServices;
using LanExchange.OSLayer;
using LanExchange.SDK.SDKModel.VO;

namespace LanExchange.Model
{
    public class ResourceProxy : PanelItemProxy
    {
        public new const string NAME = "ResourceProxy";

        public ResourceProxy()
            : base(NAME)
        {

        }

        public override int NumObjects
        {
            get
            {
                return base.NumObjects - 1;
            }
        }

        public override ColumnVO[] GetColumns()
        {
            return new ColumnVO[] { 
                new ColumnVO("Общий ресурс", 130),
                new ColumnVO("*:", 20),
                new ColumnVO("Описание", 250)
            };
        }

        public override void EnumObjects(string Server)
        {
            NetApi32.SHARE_INFO_1 shi1;
            shi1.shi1_remark = Server;
            Objects.Add(new PanelItemVO("..", null));
            int entriesread = 0;
            int totalentries = 0;
            int resume_handle = 0;
            int nStructSize = Marshal.SizeOf(typeof(NetApi32.SHARE_INFO_1));
            IntPtr bufPtr = IntPtr.Zero;
            StringBuilder server = new StringBuilder(Server);
            int ret = NetApi32.NetShareEnum(server, 1, ref bufPtr, NetApi32.MAX_PREFERRED_LENGTH, ref entriesread, ref totalentries, ref resume_handle);
            if (ret == NetApi32.NERR_Success)
            {
                IntPtr currentPtr = bufPtr;
                for (int i = 0; i < entriesread; i++)
                {
                    shi1 = (NetApi32.SHARE_INFO_1)Marshal.PtrToStructure(currentPtr, typeof(NetApi32.SHARE_INFO_1));
                    if ((shi1.shi1_type & (uint)NetApi32.SHARE_TYPE.STYPE_IPC) != (uint)NetApi32.SHARE_TYPE.STYPE_IPC)
                        Objects.Add(new PanelItemVO(shi1.shi1_netname, shi1));
                        //Result.Add(new TShareItem(shi1.shi1_netname, shi1.shi1_remark, shi1.shi1_type, Server));
                    currentPtr = new IntPtr(currentPtr.ToInt32() + nStructSize);
                }
                NetApi32.NetApiBufferFree(bufPtr);
            }
        }
    }
}
