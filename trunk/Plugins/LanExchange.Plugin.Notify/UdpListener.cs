using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace LanExchange.Plugin.Notify
{
    public class UdpListener : IDisposable
    {
        private readonly UdpClient m_Udp;

        public event EventHandler<MessageEventArgs> MessageReceived;
        private IPEndPoint m_EndPoint;

        public UdpListener(int port)
        {
            m_EndPoint = new IPEndPoint(IPAddress.Any, port);
            m_Udp = new UdpClient();
            m_Udp.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true); 
            m_Udp.Client.Bind(m_EndPoint);
            m_Udp.BeginReceive(ReceiveCallback, null);
        }

        public void Dispose()
        {
            m_Udp.BeginReceive(ReceiveCallback, null);
            m_Udp.Close();
        }

        public void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                var data = m_Udp.EndReceive(ar, ref m_EndPoint);
                if (MessageReceived != null)
                    MessageReceived(this, new MessageEventArgs(data));
                m_Udp.BeginReceive(ReceiveCallback, null);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }
        }
    }
}