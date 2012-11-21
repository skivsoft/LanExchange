using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange
{
    class OSLayer
    {
        public static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }
    }
}
