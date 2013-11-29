using System;
using System.ComponentModel;
using System.Net;
using System.Net.NetworkInformation;
using LanExchange.Plugin.Network.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    public sealed class ComputerFactory : PanelItemFactoryBase
    {
        public override PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new ComputerPanelItem(parent, name);
        }

        public override PanelItemBase CreateDefaultRoot()
        {
            return null;
        }

        public override void RegisterColumns(IPanelColumnManager columnManager)
        {
            columnManager.RegisterColumn<ComputerPanelItem>(new PanelColumnHeader(Resources.NetworkName));
            columnManager.RegisterColumn<ComputerPanelItem>(new PanelColumnHeader(Resources.Description, 240));
            columnManager.RegisterColumn<ComputerPanelItem>(new PanelColumnHeader(Resources.OSVersion) { Visible = false, Width = 110 });
            // lazy columns
            columnManager.RegisterColumn<ComputerPanelItem>(new PanelColumnHeader(Resources.Ping) { Callback = GetReachable, Visible = false, Width = 110, Refreshable = true });
            columnManager.RegisterColumn<ComputerPanelItem>(new PanelColumnHeader(Resources.IPAddress) { Callback = GetIPAddress, Visible = false, Width = 80 });
            columnManager.RegisterColumn<ComputerPanelItem>(new PanelColumnHeader(Resources.MACAddress) { Callback = GetMACAddress, Visible = false, Width = 110 });
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
            var ipAddr = InternalGetIPAddress(item.Name);
            var mac = new byte[6];
            var len = (uint)mac.Length;
            NativeMethods.SendARP(ipAddr.GetHashCode(), 0, mac, ref len);
            return BitConverter.ToString(mac, 0, 6);
        }

        private static IPAddress InternalGetIPAddress(string computerName)
        {
            return Dns.GetHostEntry(computerName).AddressList[0];
        }
    }
}
