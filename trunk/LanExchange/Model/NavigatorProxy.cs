using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using PureInterfaces;
using LanExchange.SDK.SDKModel;

namespace LanExchange.Model
{
    public class NavigatorProxy : Proxy, INavigatorProxy, IProxy
    {
        public new const string NAME = "NavigatorProxy";
        private Dictionary<string, string> m_Forward;
        private Dictionary<string, string> m_Backward;

        public NavigatorProxy()
            : base(NAME)
        {
            m_Forward = new Dictionary<string, string>();
            m_Backward = new Dictionary<string, string>();
        }

        public void AddTransition(string FromProxy, string ToProxy)
        {
            if (!String.IsNullOrEmpty(FromProxy))
                m_Forward.Add(FromProxy, ToProxy);
            if (!String.IsNullOrEmpty(ToProxy))
                m_Backward.Add(ToProxy, FromProxy);
        }

        public IList<string> GetRoots()
        {
            List<string> Result = new List<string>();
            foreach (KeyValuePair<string, string> pair in m_Forward)
            {
                if (!Result.Contains(pair.Key))
                    Result.Add(pair.Key);
            }
            foreach (KeyValuePair<string, string> pair in m_Backward)
            {
                Result.Remove(pair.Key);
            }
            return Result;
        }
    }
}
