using System;
using System.Diagnostics;
using System.Threading;
using System.Net.NetworkInformation;

namespace LanExchange.Model
{
    public class PingThread
    {
        private bool m_PingOk;
        
        public static bool FastPing(string ip)
        {
            PingThread instance = new PingThread();
            Thread t = new Thread(instance.GO);
            t.Start(ip);
            t.Join(1000);
            return instance.m_PingOk;
        }

        //public static bool SlowPing(string ip)
        //{
        //    bool Result;
        //    using (Ping ping = new Ping())
        //    {
        //        PingReply pingReply = null;
        //        try
        //        {
        //            pingReply = ping.Send(ip);
        //        }
        //        catch { }
        //        Result = (pingReply != null) && (pingReply.Status == IPStatus.Success);
        //    }
        //    return Result;
        //}

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
