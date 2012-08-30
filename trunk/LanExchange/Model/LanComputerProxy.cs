using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;
using LanExchange.OSLayer;
using LanExchange.Model;
using LanExchange.Model.VO;
using LanExchange;
using BrightIdeasSoftware;

namespace LanExchange.Model
{
    public class LanComputerProxy : PanelItemProxy
    {
        public new const string NAME = "LanComputerProxy";

        public LanComputerProxy(IEnumObjectsStrategy strategy)
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
                new OLVColumn(AppFacade.T("ColumnNetworkName"), "Name"),
                new OLVColumn(AppFacade.T("ColumnComment"), "Comment"),
                new OLVColumn(AppFacade.T("ColumnOSVersion"), "SI"),
                new OLVColumn(AppFacade.T("ColumnSQLServer"), "IsSQLServer"),
                new OLVColumn(AppFacade.T("ColumnPrintServer"), "IsPrintServer")
            };
            Result[0].Width = 140;
            Result[1].Width = 240;
            // OS Version
            Result[2].Width = 140;
            Result[2].AspectToStringConverter = delegate(object x)
            {
                ServerInfoVO info = x as ServerInfoVO;
                if (info != null)
                    return info.Version();
                else
                    return null;
            };
            Result[2].IsVisible = false;
            // SQL Server
            Result[3].TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            Result[3].MinimumWidth = 20;
            Result[3].MaximumWidth = 20;
            Result[3].ToolTipText = AppFacade.T("ColumnSQLServerHint");
            Result[3].IsVisible = false;
            Result[3].AspectToStringConverter = delegate(object x)
            {
                return (bool)x ? "•" : "";
            };
            Result[4].TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            Result[4].MinimumWidth = 20;
            Result[4].MaximumWidth = 20;
            Result[4].ToolTipText = AppFacade.T("ColumnPrintServerHint");
            Result[4].IsVisible = false;
            Result[4].AspectToStringConverter = delegate(object x)
            {
                return (bool)x ? "•" : "";
            };
            return Result;
        }
    }

    public class NetApi32ComputerEnumStrategy : IEnumObjectsStrategy
    {
        public IList<PanelItemVO> EnumObjects(string domain)
        {
            List<PanelItemVO> Objects = new List<PanelItemVO>();
            NetApi32.SERVER_INFO_101 si;
            si.sv101_name = domain;
            Objects.Add(new ComputerVO(null, null));
            IntPtr pInfo = IntPtr.Zero;
            int entriesread = 0;
            int totalentries = 0;
            try
            {
                NetApi32.NERR err = NetApi32.NetServerEnum(null, 101, out pInfo, -1, ref entriesread, ref totalentries, NetApi32.SV_101_TYPES.SV_TYPE_ALL, domain, 0);
                if ((err == NetApi32.NERR.NERR_Success || err == NetApi32.NERR.ERROR_MORE_DATA) && pInfo != IntPtr.Zero)
                {
                    int ptr = pInfo.ToInt32();
                    for (int i = 0; i < entriesread; i++)
                    {
                        si = (NetApi32.SERVER_INFO_101)Marshal.PtrToStructure(new IntPtr(ptr), typeof(NetApi32.SERVER_INFO_101));
                        Objects.Add(new ComputerVO(@"\\" + si.sv101_name, new ServerInfoVO(si)));
                        ptr += Marshal.SizeOf(si);
                    }
                }
            }
            catch (Exception) { }
            finally
            { 
                if (pInfo != IntPtr.Zero) NetApi32.NetApiBufferFree(pInfo);
            }
            return Objects;
        }
    }
}
