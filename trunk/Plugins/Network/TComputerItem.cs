#define NOUSE_NORTHWIND_DATA

using System;
using System.Collections.Generic;
using SkivSoft.LanExchange.SDK;
using System.Net;
using System.Runtime.InteropServices;
using Network.Properties;

namespace SkivSoft.LanExchange.SDK
{

    /// <summary>
    /// Модель "Компьютер"
    /// </summary>
    public class TComputerItem : ILanEXItem, IComparable<ILanEXItem>
    {
        private string computer_name = String.Empty;
        private string comment = String.Empty;
        private string version = String.Empty;
        private uint platform_id;
        private uint ver_major;
        private uint ver_minor;
        private uint type_id;
        private bool pingable;
        private bool logged;
        private IPEndPoint ipendpoint;

        public TComputerItem()
        {
            this.pingable = true;
        }

        public TComputerItem(string computer_name, string comment, uint platform_id, uint ver_major, uint ver_minor, uint type_id)
        {
            this.computer_name = computer_name;
            this.comment = comment;
            this.SetPlatform(platform_id, ver_major, ver_minor, type_id);
            this.pingable = true;
        }

        public static bool IsValidName(string name)
        {
            return true;
        }

        public void SetPlatform(uint platform_id, uint ver_major, uint ver_minor, uint type_id)
        {
            this.platform_id = platform_id;
            this.ver_major = ver_major;
            this.ver_minor = ver_minor;
            this.type_id = type_id;
            this.version = LocalNetwork.PlatformToString(platform_id, ver_major, ver_minor, type_id);
        }

        public string Name { get { return this.computer_name; } set { this.computer_name = value; } }
        public string Comment { get { return this.comment; } set { this.comment = value; } }

        public string[] GetStrings()
        {
            return new string[3] { Comment.ToUpper(), Name.ToUpper(), OSVersion.ToUpper() };
        }

        public string[] GetSubItems()
        {
            return new String[1] { Name };
        }

        public TLanEXColumnInfo[] GetColumns()
        {
            return new TLanEXColumnInfo[2] {
                new TLanEXColumnInfo(Resources.sNetworkName, 130),
                new TLanEXColumnInfo(Resources.sDescription, 250)
            };
        }

        public int ImageIndex
        {
            get
            {
                if (IsLogged)
                    return Globals.imgCompOnline;
                else
                    if (IsPingable)
                        return Globals.imgCompDefault;
                    else
                        return Globals.imgCompOff;
            }
        }

        public string ToolTipText
        {
            get { return String.Format("{0}\n{1}", Comment, OSVersion); }
        }

        public string OSVersion
        {
            get { return version; }
            set { version = value; }
        }

        public uint Type
        {
            get { return type_id; }
            set { type_id = value; }
        }

        public bool IsPingable
        {
            get { return pingable; }
            set { pingable = value; }
        }

        public bool IsLogged
        {
            get { return logged; }
            set { logged = value; }
        }

        public IPEndPoint EndPoint
        {
            get { return ipendpoint; }
            set { ipendpoint = value; }
        }

        public void CopyExtraFrom(ILanEXItem Comp)
        {
            if (Comp == null) return;
            pingable = (Comp as TComputerItem).pingable;
            logged = (Comp as TComputerItem).logged;
            ipendpoint = (Comp as TComputerItem).ipendpoint;
        }

        public int CompareTo(ILanEXItem p2)
        {
            return Name.CompareTo(p2.Name);
        }

#if USE_NORTHWIND_DATA
        struct TItem
        {
            public string name;
            public string comment;

            public TItem(string s1, string s2)
            {
                name = s1;
                comment = s2;
            }
        };

        public static void AddNorthWindData(List<IPanelItem> Result)
        {
            TItem[] Data = new TItem[38] {
                new TItem("GLADKIH-A", "Гладких Андрей"),
                new TItem("ILINA-YU", "Ильина Юлия"),
                new TItem("KLIMOV-S", "Климов Сергей"),
                new TItem("KOREPIN-V", "Корепин Вадим"),
                new TItem("KULIKOV-E", "Куликов Евгений"),
                new TItem("NOVIKOV-N", "Новиков Николай"),
                new TItem("OZHOGINA-I", "Ожогина Инна"),
                new TItem("POPKOVA-D", "Попкова Дарья"),
                new TItem("SERGIENKO-M", "Сергиенко Мария"),
                new TItem("KOSTERINA-O", "Костерина Ольга"),
                new TItem("VERNYJ-G", "Верный Григорий"),
                new TItem("EGOROV-V", "Егоров Владимир"),
                new TItem("OMELCHENKO-S", "Омельченко Светлана"),
                new TItem("PESOTSKIJ-S", "Песоцкий Станислав"),
                new TItem("SHASHKOV-R", "Шашков Руслан"),
                new TItem("VRONSKIJ-YU", "Вронский Юрий"),
                new TItem("PODKOLZINA-E", "Подколзина Екатерина"),
                new TItem("ERJOMENKO-A", "Ерёменко Алексей"),
                new TItem("GRACHEV-N", "Грачев Николай"),
                new TItem("OREHOV-A", "Орехов Алексей"),
                new TItem("VOLODIN-V", "Володин Виктор"),
                new TItem("TUMANOV-A", "Туманов Александр"),
                new TItem("GORNOZHENKO-D", "Горноженко Дмитрий"),
                new TItem("SALIMZYANOVA-D", "Салимзянова Дина"),
                new TItem("USHAKOV-V", "Ушаков Валерий"),
                new TItem("VAZHIN-F", "Важин Филип"),
                new TItem("MISHKOVA-E", "Мишкова Екатерина"),
                new TItem("EFIMOV-A", "Ефимов Александр"),
                new TItem("FOMIN-G", "Фомин Георгий"),
                new TItem("TOLMACHEV-V", "Толмачев Виктор"),
                new TItem("IGNATOV-S", "Игнатов Степан"),
                new TItem("ENTIN-M", "Энтин Михаил"),
                new TItem("SHUTOV-I", "Шутов Игнат"),
                new TItem("BORISOV-S", "Борисов Сергей"),
                new TItem("IVANOV-A", "Иванов Андрей"),
                new TItem("TIMOFEEVA-K", "Тимофеева Кристина"),
                new TItem("BEREZIN-A", "Березин Артур"),
                new TItem("YARTSEV-S", "Ярцев Семен")
            };
            foreach (TItem Item in Data)
            {
                TComputerItem Comp = new TComputerItem(Item.name, Item.comment, 500, 5, 1, 0);
                Result.Add(Comp);
            }
        }
#endif
        // получим список всех компьюетеров
        public static List<ILanEXItem> GetItems()
        {
            LocalNetwork.SERVER_INFO_101 si;
            IntPtr pInfo = IntPtr.Zero;
            int entriesread = 0;
            int totalentries = 0;
            List<ILanEXItem> Result = new List<ILanEXItem>();
            try
            {
                //TLogger.Print("WINAPI NetServerEnum");
                LocalNetwork.NERR err = LocalNetwork.NetServerEnum(null, 101, out pInfo, -1, ref entriesread, ref totalentries, LocalNetwork.SV_101_TYPES.SV_TYPE_ALL, null, 0);
                if ((err == LocalNetwork.NERR.NERR_Success || err == LocalNetwork.NERR.ERROR_MORE_DATA) && pInfo != IntPtr.Zero)
                {
                    //TLogger.Print("WINAPI NetServerEnum result: entriesread={0}, totalentries={1}", entriesread, totalentries);
                    int ptr = pInfo.ToInt32();
                    for (int i = 0; i < entriesread; i++)
                    {
                        si = (LocalNetwork.SERVER_INFO_101)Marshal.PtrToStructure(new IntPtr(ptr), typeof(LocalNetwork.SERVER_INFO_101));
                        // в режиме пользователя не сканируем: сервера, контроллеры домена
                        //bool bServer = (si.sv101_type & 0x8018) != 0;
                        //if (Program.AdminMode || !bServer)
                        Result.Add(new TComputerItem(si.sv101_name, si.sv101_comment, si.sv101_platform_id, si.sv101_version_major, si.sv101_version_minor, si.sv101_type));
                        ptr += Marshal.SizeOf(si);
                    }
                }
            }
            catch (Exception) { }
            finally
            { // освобождаем выделенную память
                if (pInfo != IntPtr.Zero) LocalNetwork.NetApiBufferFree(pInfo);
            }
#if USE_NORTHWIND_DATA
            AddNorthWindData(Result);
#endif
            return Result;
        }
    }
}
