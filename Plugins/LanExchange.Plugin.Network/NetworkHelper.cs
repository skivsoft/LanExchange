using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using LanExchange.Plugin.Network.NetApi;

namespace LanExchange.Plugin.Network
{
    internal static class NetworkHelper
    {
        [Localizable(false)]
        public static IEnumerable<string> NetWorkstationUserEnumNames(string computer)
        {
            var users = new List<string>();
            var domain = WorkstationInfo.FromComputer(null).LanGroup;
            foreach (var item in NetApiHelper.NetWkstaUserEnum(computer))
            {
                if (item.username.EndsWith("$")) continue;
                string name;
                if (domain != item.logon_domain)
                    name = string.Format(
                        CultureInfo.CurrentCulture,
                        @"{0}\{1}",
                        item.logon_domain.ToUpper(CultureInfo.CurrentCulture),
                        item.username);
                else
                    name = item.username;
                if (!users.Contains(name))
                    users.Add(name);
            }

            users.Sort();
            return users;
        }
    }
}
