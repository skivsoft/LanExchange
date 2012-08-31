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
            const int colName    = 0;
            const int colComment = 1;
            const int colVersion = 2;
            const int colIP      = 3;
            const int colStart   = 4;
            const int colEnd     = 13;

            string[] Aspects = { "Server", "SQLServer", "DomainController", "TimeSource", "PrintServer", 
                                 "DialInServer", "PotentialBrowser", "BackupBrowser", "MasterBrowser", "DFSRoot"};

            OLVColumn[] Result = new OLVColumn[] { 
                new OLVColumn(AppFacade.T("ColumnNetworkName"), "Name"),
                new OLVColumn(AppFacade.T("ColumnComment"), "Comment"),
                new OLVColumn(AppFacade.T("ColumnOSVersion"), "SI"),
                new OLVColumn(AppFacade.T("ColumnIP"), "IPAddresses"),
                null, null, null, null, null, null, null, null, null, null
            };
            Result[colName].Width = 140;
            Result[colComment].Width = 240;
            // OS Version
            Result[colVersion].Width = 140;
            Result[colVersion].AspectToStringConverter = delegate(object x)
            {
                ServerInfoVO info = x as ServerInfoVO;
                if (info != null)
                    return info.Version();
                else
                    return null;
            };
            Result[colVersion].IsVisible = false;
            // IP-address
            Result[colIP].Width = 90;
            Result[colIP].ToolTipText = AppFacade.T("ColumnIPHint");
            Result[colIP].IsVisible = false;
            // Server type flags
            for (int i = colStart; i <= colEnd; i++)
            {
                string asp = Aspects[i - colStart];
                Result[i] = new OLVColumn(AppFacade.T(String.Format("Column{0}", asp)), "Is"+asp);
                Result[i].TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                //Microsoft Sans Serif
                Result[i].HeaderFont = new System.Drawing.Font("", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Result[i].MinimumWidth = 30;
                Result[i].MaximumWidth = 30;
                Result[i].AspectToStringConverter = new AspectToStringConverterDelegate(BoolAsDot);
                Result[i].ToolTipText = AppFacade.T(String.Format("Column{0}Hint", asp));
                Result[i].IsVisible = false;
            }
            return Result;
        }

        public string BoolAsDot(object x)
        {
            return (bool)x ? "•" : "";
        }
    }

    public class NetApi32ComputerEnumStrategy : IEnumObjectsStrategy
    {
        public IList<PanelItemVO> EnumObjects(string domain)
        {
            List<PanelItemVO> Objects = new List<PanelItemVO>();
            NetApi32.SERVER_INFO_101 si;
            //si.sv101_name = domain;
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
                        Objects.Add(new ComputerVO(si.sv101_name, new ServerInfoVO(si)));
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
