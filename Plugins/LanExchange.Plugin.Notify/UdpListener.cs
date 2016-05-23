using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace LanExchange.Plugin.Notify
{
    public class UdpListener : IDisposable
    {
        private readonly UdpClient udpClient;

        public event EventHandler<MessageEventArgs> MessageReceived;
        private IPEndPoint endPoint;

        public UdpListener(int port)
        {
            endPoint = new IPEndPoint(IPAddress.Any, port);
            udpClient = new UdpClient();
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true); 
            udpClient.Client.Bind(endPoint);
            udpClient.BeginReceive(ReceiveCallback, null);
        }

        public void Dispose()
        {
            udpClient.BeginReceive(ReceiveCallback, null);
            udpClient.Close();
        }

        public void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                var data = udpClient.EndReceive(ar, ref endPoint);
                if (MessageReceived != null)
                    MessageReceived(this, new MessageEventArgs(data));
                udpClient.BeginReceive(ReceiveCallback, null);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }
        }
    }
}