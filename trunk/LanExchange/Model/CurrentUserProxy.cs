using System;
using System.Collections.Generic;
using System.Text;
using PureMVC.Patterns;
using PureMVC.Interfaces;

namespace LanExchange.Model
{
    public class CurrentUserProxy : Proxy, IProxy
    {
        public new const string NAME = "CurrentUserProxy";
        
        private string m_ComputerName = "";
        private string m_UserName = "";

        public CurrentUserProxy()
            : base(NAME)
        {
            m_ComputerName = Environment.MachineName;
            using (System.Security.Principal.WindowsIdentity user = System.Security.Principal.WindowsIdentity.GetCurrent())
            {
                string[] A = user.Name.Split('\\');
                if (A.Length > 1)
                    m_UserName = A[1];
                else
                    m_UserName = A[0];
            }
        }

        public string ComputerName
        {
            get { return m_ComputerName; }
        }

        public string UserName
        {
            get { return m_UserName; }
        }
    }
}
