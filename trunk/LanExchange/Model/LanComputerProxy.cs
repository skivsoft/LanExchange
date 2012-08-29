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

        public LanComputerProxy()
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

        public override OLVColumn[] GetColumns()
        {
            OLVColumn[] Result = new OLVColumn[] { 
                new OLVColumn(AppFacade.T("ColumnNetworkName"), "Name"),
                new OLVColumn(AppFacade.T("ColumnComment"), "Comment"),
                new OLVColumn(AppFacade.T("ColumnOSVersion"), "SI")
            };
            Result[0].Width = 140;
            Result[1].Width = 240;
            Result[2].Width = 140;
            /*
            Result[2].AspectGetter = delegate(object rowObject) {
                ComputerVO comp = rowObject as ComputerVO;
                if (comp != null)
                    return comp.SI;
                else
                    return null;
            };
             */
            Result[2].AspectToStringConverter = delegate(object x)
            {
                ServerInfoVO info = x as ServerInfoVO;
                if (info != null)
                    return info.Version();
                else
                    return null;
            };
            //Result[2].IsVisible = false;
            return Result;
        }

        public override void EnumObjects(string Domain)
        {
            Objects.Clear();

            NetApi32.SERVER_INFO_101 si;
            si.sv101_name = Domain;
            Objects.Add(new ComputerVO(null, null));
            IntPtr pInfo = IntPtr.Zero;
            int entriesread = 0;
            int totalentries = 0;
            try
            {
                NetApi32.NERR err = NetApi32.NetServerEnum(null, 101, out pInfo, -1, ref entriesread, ref totalentries, NetApi32.SV_101_TYPES.SV_TYPE_ALL, Domain, 0);
                if ((err == NetApi32.NERR.NERR_Success || err == NetApi32.NERR.ERROR_MORE_DATA) && pInfo != IntPtr.Zero)
                {
                    int ptr = pInfo.ToInt32();
                    for (int i = 0; i < entriesread; i++)
                    {
                        si = (NetApi32.SERVER_INFO_101)Marshal.PtrToStructure(new IntPtr(ptr), typeof(NetApi32.SERVER_INFO_101));
                        // в режиме пользователя не сканируем: сервера, контроллеры домена
                        //bool bServer = (si.sv101_type & 0x8018) != 0;
                        //if (Program.AdminMode || !bServer)
                        //Result.Add(new TComputerItem(si.sv101_name, si.sv101_comment, si.sv101_platform_id, si.sv101_version_major, si.sv101_version_minor, si.sv101_type));
                        Objects.Add(new ComputerVO(@"\\" + si.sv101_name, new ServerInfoVO(si)));
                        ptr += Marshal.SizeOf(si);
                    }
                }
            }
            catch (Exception) { /* обработка ошибки нифига не делаем :(*/ }
            finally
            { // освобождаем выделенную память
                if (pInfo != IntPtr.Zero) NetApi32.NetApiBufferFree(pInfo);
            }
        }
    }
}
