using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using LanExchange.OSLayer;
using LanExchange.Model;
using LanExchange.Model.VO;
using LanExchange;
using BrightIdeasSoftware;

namespace LanExchange.Model
{
    public class LanDomainProxy : PanelItemProxy
    {
        public new const string NAME = "LanDomainProxy";

        public LanDomainProxy()
            : base(NAME)
        {

        }

        public override OLVColumn[] GetColumns()
        {
            OLVColumn[] Result = new OLVColumn[] { 
                new OLVColumn(AppFacade.T("ColumnDomainName"), "Name"),
                new OLVColumn(AppFacade.T("ColumnMasterBrowser"), "MasterBrowser")
            };
            Result[0].Width = 140;
            Result[1].Width = 140;
            return Result;
        }

        public override void EnumObjects(string path)
        {
            Objects.Clear();

            NetApi32.SERVER_INFO_101 si;
            IntPtr pInfo = IntPtr.Zero;
            int entriesread = 0;
            int totalentries = 0;
            {
                try
                {
                    NetApi32.NERR err = NetApi32.NetServerEnum(null, 101, out pInfo, -1, ref entriesread, ref totalentries, NetApi32.SV_101_TYPES.SV_TYPE_DOMAIN_ENUM, null, 0);
                    if ((err == NetApi32.NERR.NERR_Success || err == NetApi32.NERR.ERROR_MORE_DATA) && pInfo != IntPtr.Zero)
                    {
                        int ptr = pInfo.ToInt32();
                        for (int i = 0; i < entriesread; i++)
                        {
                            si = (NetApi32.SERVER_INFO_101)Marshal.PtrToStructure(new IntPtr(ptr), typeof(NetApi32.SERVER_INFO_101));
                            Objects.Add(new DomainVO(si.sv101_name, si.sv101_comment));
                            ptr += Marshal.SizeOf(si);
                        }
                    }
                }
                catch (Exception) { }
                finally
                {
                    if (pInfo != IntPtr.Zero) NetApi32.NetApiBufferFree(pInfo);
                }
            }
        }
    }
}
