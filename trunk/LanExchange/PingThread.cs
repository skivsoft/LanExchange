using System;
using System.Threading;
using System.Net.NetworkInformation;

namespace LanExchange
{
    class PingThread
    {
        bool PingOK = false;
        
        public static bool FastPing(string ip)
        {
            PingThread instance = new PingThread();
            Thread t = new Thread(instance.GO);
            t.Start(ip);
            t.Join(1000);
            return instance.PingOK;
        }

        public static bool SlowPing(string ip)
        {
            Ping ping = new Ping();
            PingReply pingReply = null;
            try
            {
                pingReply = ping.Send(ip);
            }
            catch
            {
            }
            return (pingReply != null) && (pingReply.Status == IPStatus.Success);
        }

        void GO(object ip_obj)
        {
            PingOK = false;
            if (ip_obj == null) return;
            try
            {
                var ping = new Ping();
                var pingReply = ping.Send((string)ip_obj);
                PingOK = (pingReply != null) && (pingReply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
            }
        }

    }
}
