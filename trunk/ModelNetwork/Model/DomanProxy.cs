using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using LanExchange.SDK.SDKModel.VO;
using ModelNetwork.Properties;
using LanExchange.SDK.SDKModel;
using ModelNetwork.OSLayer;

namespace ModelNetwork.Model
{
    public class DomainProxy : PanelItemProxy
    {
        public new const string NAME = "DomainProxy";

        public DomainProxy()
            : base(NAME)
        {

        }

        public override ColumnVO[] GetColumns()
        {
            return new ColumnVO[] { 
                new ColumnVO(Resources.ColumnDomainName, 130)
            };
        }

        public override void EnumObjects(string path)
        {
            NetApi32.SERVER_INFO_101 si;
            IntPtr pInfo = IntPtr.Zero;
            int entriesread = 0;
            int totalentries = 0;
            try
            {
                NetApi32.NERR err = NetApi32.NetServerEnum(null, 101, out pInfo, -1, ref entriesread, ref totalentries, NetApi32.SV_101_TYPES.SV_TYPE_DOMAIN_ENUM, null, 0);
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
                        Objects.Add(new PanelItemVO(si.sv101_name, si));
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
