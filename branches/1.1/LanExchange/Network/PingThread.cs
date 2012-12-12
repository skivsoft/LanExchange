using System;
using System.Threading;
using System.Net.NetworkInformation;

namespace LanExchange
{
    class PingThread
    {
        private bool PingOK;
        
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
            bool Result;
            using (Ping ping = new Ping())
            {
                PingReply pingReply = null;
                try
                {
                    pingReply = ping.Send(ip);
                }
                catch { }
                Result = (pingReply != null) && (pingReply.Status == IPStatus.Success);
            }
            return Result;
        }

        void GO(object ip_obj)
        {
            PingOK = false;
            if (ip_obj == null) return;
            try
            {
                using (var ping = new Ping())
                {
                    var pingReply = ping.Send((string)ip_obj);
                    PingOK = (pingReply != null) && (pingReply.Status == IPStatus.Success);
                }
            }
            catch { }
        }

    }
}
