using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.SDK.SDKModel
{
    public interface INavigatorProxy
    {
        void AddTransition(string FromProxy, string ToProxy);
        IList<string> GetRoots();
    }
}
