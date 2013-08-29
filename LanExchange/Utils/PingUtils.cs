using System;
using System.Diagnostics;
using System.Threading;
using System.Net.NetworkInformation;

namespace LanExchange.Utils
{
    public class PingUtils
    {
        public const int DELAY_FOR_FAST_PING = 1000; // 1 sec
        private bool m_PingOk;
        
        public static bool FastPing(string ip)
        {
            var instance = new PingUtils();
            var thread = new Thread(instance.GO);
            thread.Start(ip);
            thread.Join(DELAY_FOR_FAST_PING);
            return instance.m_PingOk;
        }

        void GO(object ip)
        {
            m_PingOk = false;
            if (ip == null) return;
            try
            {
                using (var ping = new Ping())
                {
                    var pingReply = ping.Send((string) ip);
                    m_PingOk = (pingReply != null) && (pingReply.Status == IPStatus.Success);
                }
            }
            catch(Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}