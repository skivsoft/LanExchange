using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using LanExchange.Plugin.Network.Properties;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Network
{
    public sealed class ComputerFactory : IPanelItemFactory
    {
        public PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new ComputerPanelItem(parent, name);
        }

        public PanelItemBase CreateDefaultRoot()
        {
            return null;
        }

        public Func<PanelItemBase, bool> GetAvailabilityChecker()
        {
            return InternalPing;
        }

        public void RegisterColumns(IPanelColumnManager columnManager)
        {
            columnManager.RegisterColumn<ComputerPanelItem>(new PanelColumnHeader(Resources.NetworkName));
            columnManager.RegisterColumn<ComputerPanelItem>(new PanelColumnHeader(Resources.Description, 240));
            columnManager.RegisterColumn<ComputerPanelItem>(new PanelColumnHeader(Resources.OSVersion, 110) { Visible = false});
            // lazy columns
            columnManager.RegisterColumn<ComputerPanelItem>(new PanelColumnHeader(Resources.Ping, 110) { Callback = GetReachable, Visible = false, Refreshable = true });
            columnManager.RegisterColumn<ComputerPanelItem>(new PanelColumnHeader(Resources.IPAddress, 80) { Callback = GetIPAddress, Visible = false});
            columnManager.RegisterColumn<ComputerPanelItem>(new PanelColumnHeader(Resources.MACAddress, 110) { Callback = GetMACAddress, Visible = false});
            columnManager.RegisterColumn<ComputerPanelItem>(new PanelColumnHeader(Resources.LoggedUsers, 300) {Callback = GetLoggedUsers, Visible = false} );
        }

        private static bool InternalPing(PanelItemBase item)
        {
            var ipAddr = InternalGetIPAddress(item.Name);
            using (var ping = new Ping())
            {
                var pingReply = ping.Send(ipAddr);
                return pingReply != null && pingReply.Status == IPStatus.Success;
            }
        }

        [Localizable(false)]
        public static IComparable GetReachable(PanelItemBase item)
        {
            var ipAddr = InternalGetIPAddress(item.Name);
            string result = string.Empty;
            using (var ping = new Ping())
            {
                var pingReply = ping.Send(ipAddr);
                if (pingReply == null)
                    item.IsReachable = false;
                else if (pingReply.Status == IPStatus.Success)
                {
                    result = pingReply.RoundtripTime == 0 ? Resources.OK : string.Format(Resources.OK_ms, pingReply.RoundtripTime);
                    item.IsReachable = true;
                }
                else
                {
                    result = pingReply.Status.ToString();
                    if (result.StartsWith("Destination"))
                        result = result.Substring("Destination".Length);
                    item.IsReachable = false;
                }
            }
            return result;
        }

        /// <summary>
        /// Returns ip addres for specified computer "item".
        /// </summary>
        /// <param name="item">ComputerPanelItem object.</param>
        /// <returns>IPAddressComparable object.</returns>
        public static IComparable GetIPAddress(PanelItemBase item)
        {
            return new IPAddressComparable(InternalGetIPAddress(item.Name));
        }

        /// <summary>
        /// Returns MAC address for specified computer "item".
        /// Sends ARP request to computer's ip address.
        /// URL: http://www.codeproject.com/KB/IP/host_info_within_network.aspx
        /// </summary>
        /// <param name="item">ComputerPanelItem object.</param>
        /// <returns>MAC-address string.</returns>
        public static IComparable GetMACAddress(PanelItemBase item)
        {
            if (PluginNetwork.macAddressService == null)
                return null;
            var ipAddress = InternalGetIPAddress(item.Name);
            return PluginNetwork.macAddressService.GetMACAddress(ipAddress);
        }

        private static IPAddress InternalGetIPAddress(string computerName)
        {
            return Dns.GetHostEntry(computerName).AddressList[0];
        }

        [Localizable(false)]
        public static IComparable GetLoggedUsers(PanelItemBase item)
        {
            var list = NetworkHelper.NetWorkstationUserEnumNames(item.Name);
            return string.Join(", ", list.ToArray());
        }
    }
}
