using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LanExchange.OSLayer
{
    public class Tools
    {
        public static void ExplodeCmd(string CmdLine, out string FName, out string Params)
        {
            FName = "";
            Params = "";
            bool bQuote = false;
            bool bParam = false;
            for (int i = 0; i < CmdLine.Length; i++)
            {
                if (!bParam && CmdLine[i] == '"')
                {
                    bQuote = !bQuote;
                    continue;
                }
                if (!bParam && !bQuote && CmdLine[i] == ' ')
                    bParam = true;
                else
                    if (bParam)
                        Params += CmdLine[i].ToString();
                    else
                        FName += CmdLine[i].ToString();
            }
        }

        // Возвращает имя операционной системы по кодам платформы и версии
        public static string PlatformToString(uint platform_id, uint ver_major, uint ver_minor, uint type)
        {
            //return String.Format("{0}.{1}.{2}.{3}", platform_id, ver_major, ver_minor, type);
            bool bServer = (type & (uint)NetApi32.SV_101_TYPES.SV_TYPE_SERVER_NT) != 0;
            switch ((NetApi32.SV_101_PLATFORM)platform_id)
            {
                case NetApi32.SV_101_PLATFORM.PLATFORM_ID_DOS:
                    return String.Format("MS-DOS {0}.{1}", ver_major, ver_minor);
                case NetApi32.SV_101_PLATFORM.PLATFORM_ID_OS2:
                    switch (ver_major)
                    {
                        case 3: return "Windows NT 3.51";
                        case 4:
                            switch (ver_minor)
                            {
                                case 0: return "Windows 95";
                                case 10: return "Windows 98";
                                case 90: return "Windows ME";
                                default:
                                    return "Windows 9x";
                            }
                        default:
                            return String.Format("Windows NT {0}.{1}", ver_major, ver_minor);
                    }
                case NetApi32.SV_101_PLATFORM.PLATFORM_ID_NT:
                    switch (ver_major)
                    {
                        case 3: return "Windows NT 3.51";
                        case 4:
                            switch (ver_minor)
                            {
                                case 0: return "Windows 95";
                                case 10: return "Windows 98";
                                case 90: return "Windows ME";
                                default:
                                    return "Windows 9x";
                            }
                        case 5:
                            switch (ver_minor)
                            {
                                case 0: return "Windows 2000";
                                case 1: return "Windows XP";
                                case 2: return "Windows Server 2003";
                                default:
                                    return String.Format("Windows NT {0}.{1}", ver_major, ver_minor);
                            }
                        case 6:
                            switch (ver_minor)
                            {
                                case 0: return bServer ? "Windows Server 2008" : "Windows Vista";
                                case 1: return bServer ? "Windows Server 2008 R2" : "Windows 7";
                                case 2: return bServer ? "Windows 8 Server" : "Windows 8";
                                default:
                                    return String.Format("Windows NT {0}.{1}", ver_major, ver_minor);
                            }
                        default:
                            return String.Format("Windows NT {0}.{1}", ver_major, ver_minor);
                    }
                case NetApi32.SV_101_PLATFORM.PLATFORM_ID_OSF:
                    return String.Format("OSF {0}.{1}", ver_major, ver_minor);
                case NetApi32.SV_101_PLATFORM.PLATFORM_ID_VMS:
                    return String.Format("VMS {0}.{1}", ver_major, ver_minor);
                default:
                    return String.Format("{0} {1}.{2}", platform_id, ver_major, ver_minor);
            }
        }

        /*
        public static void ShowProperties(object obj)
        {
            Form F = new Form();
            F.Text = obj.ToString();
            PropertyGrid Grid = new PropertyGrid();
            Grid.Dock = DockStyle.Fill;
            Grid.SelectedObject = obj;
            F.Controls.Add(Grid);
            F.Show();
        }
        */
    }
}
