using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;
using LanExchange.OSLayer;
using LanExchange.Model;
using LanExchange.Model.VO;
using ModelNetwork.Model.VO;
using LanExchange;

namespace ModelNetwork.Model
{
    public class ComputerProxy : PanelItemProxy
    {
        public new const string NAME = "ComputerProxy";

        public ComputerProxy()
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
                new ColumnVO(Globals.T("ColumnNetworkName"), 130),
                new ColumnVO(Globals.T("ColumnComment"), 250),
                new ColumnVO(Globals.T("ColumnOSVersion"), 130)
            };
        }

        public override void EnumObjects(string Domain)
        {
            NetApi32.SERVER_INFO_101 si;
            si.sv101_name = Domain;
            Objects.Add(new PanelItemVO("..", null));
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
                        Objects.Add(new ComputerVO(@"\\" + si.sv101_name, si));
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
