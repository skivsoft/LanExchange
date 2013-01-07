using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model.VO;
using LanExchange.OSLayer;
using System.Net;

namespace LanExchange.Model.VO
{
    public class IPComparable : IPAddress, IComparable
    {
        new public static readonly IPComparable None;

        public IPComparable(IPAddress ip) : base(ip.GetAddressBytes())
        {
        }

        public int CompareTo(object obj)
        {
            byte[] A = this.GetAddressBytes();
            byte[] B = ((IPAddress)obj).GetAddressBytes();
            if (A.Length < B.Length) return -1;
            else
                if (A.Length > B.Length) return 1;
                else
                    for (int i = 0; i < A.Length; i++)
                    {
                        if (A[i] < B[i]) return -1;
                        else
                            if (A[i] > B[i]) return 1;
                    }
            return 0;
        }
    }

    public class IPList : IComparable
    {
        private IPComparable[] m_List = new IPComparable[0];

        public IPList()
        {

        }

        public IPList(IPAddress[] list)
        {
            m_List = new IPComparable[list.Length];
            for (int i = 0; i < m_List.Length; i++)
                m_List[i] = new IPComparable(list[i]);
            Array.Sort(m_List);
        }

        public IPComparable First
        {
            get
            {
                if (m_List.Length == 0)
                    return IPComparable.None;
                else
                    return m_List[0];
            }
        }

        public override string ToString()
        {
            string Result = "";
            for (int i = 0; i < m_List.Length; i++)
            {
                if (i > 0) Result += ", ";
                Result += m_List[i].ToString();
            }
            return Result;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return this.First.CompareTo(((IPList)obj).First);
        }

        #endregion
    }
    
    public class ComputerVO : PanelItemVO
    {
        private string m_Comment = "";
        private ServerInfoVO m_SI = null;
        private IPList m_IPAddresses = new IPList();
        private string m_MAC = "";

        public ComputerVO(string name, object data)
            : base(name, data)
        {
            m_SI = data as ServerInfoVO;
            if (m_SI != null)
                m_Comment = m_SI.Comment;
            if (!String.IsNullOrEmpty(name))
            {
                m_IPAddresses = new IPList(Dns.GetHostAddresses(name));
            }
        }

        private int GetIPRange(IPAddress IP)
        {
            byte[] B = IP.GetAddressBytes();
            // ip in range 10.0.0.0 - 10.255.255.255
            if (B[0] == 10)
                return 1;
            else
                // ip in range 172.16.0.0 - 172.31.255.255
                if (B[0] == 172 && (B[1] >= 16 || B[1] <= 31))
                    return 2;
                else
                    // ip in range 192.168.0.0 - 192.168.255.255
                    if (B[0] == 192 && B[1] == 168)
                        return 3;
                    else
                        return 0;
        }

        public string Comment
        {
            get { return m_Comment; }
            set { m_Comment = value; }
        }

        public ServerInfoVO SI
        {
            get { return m_SI; }
        }

        public IPList IPAddresses
        {
            get { return m_IPAddresses; }
        }

        public string MAC
        {
            get { return m_MAC; }
        }

        public bool IsServer
        {
            get { return m_SI != null ? m_SI.IsServer() : false; }
        }

        public bool IsSQLServer
        {
            get { return m_SI != null ? m_SI.IsSQLServer() : false; }
        }

        public bool IsDomainController
        {
            get {return m_SI != null ? m_SI.IsDomainController() : false; } 
        }

        public bool IsTimeSource
        {
            get { return m_SI != null ? m_SI.IsTimeSource() : false; }
        }

        public bool IsPrintServer
        {
            get { return m_SI != null ? m_SI.IsPrintServer() : false; }
        }

        public bool IsDialInServer
        {
            get { return m_SI != null ? m_SI.IsDialInServer() : false; }
        }

        public bool IsPotentialBrowser
        {
            get { return m_SI != null ? m_SI.IsPotentialBrowser() : false; }
        }

        public bool IsBackupBrowser
        {
            get { return m_SI != null ? m_SI.IsBackupBrowser() : false; }
        }

        public bool IsMasterBrowser
        {
            get { return m_SI != null ? m_SI.IsMasterBrowser() : false; }
        }
        
        public bool IsDFSRoot
        {
            get { return m_SI != null ? m_SI.IsDFSRoot() : false; }
        }
    }
}
