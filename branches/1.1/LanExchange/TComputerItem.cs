using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using OSTools;
using System.Net;
using LanExchange.Network;

namespace LanExchange
{
    public class TComputerItem : TPanelItem
    {
        // индексы иконок компов
        public const int imgCompDefault = 0;
        public const int imgCompBlue = 1;
        public const int imgCompMagenta = 2;
        public const int imgCompGray = 3;
        public const int imgCompGreen = 4;
        public const int imgCompRed = 5;

        private string m_Name = String.Empty;
        private string m_Comment = String.Empty;
        private ServerInfo m_SI = null;

        private bool pingable;
        private bool logged;
        private IPEndPoint ipendpoint;

        public TComputerItem()
        {
            this.pingable = true;
        }

        public TComputerItem(string computer_name, object data)
        {
            m_Name = computer_name;
            pingable = true;
            if (data is NetApi32.SERVER_INFO_101)
            {
                m_SI = new ServerInfo((NetApi32.SERVER_INFO_101)data);
                m_Comment = m_SI.Comment;
            }
        }

        public static bool IsValidName(string name)
        {
            return true;
        }

        protected override string GetName()
        {
            return m_Name;
        }

        protected override void SetName(string value)
        {
            m_Name = value;
        }

        protected override string GetComment()
        {
            return m_Comment;
        }

        protected override void SetComment(string value)
        {
            m_Comment = value;
        }

        public ServerInfo SI
        {
            get
            {
                return m_SI;
            }
        }

        public override string[] getStrings()
        {
            return new string[3] { Comment.ToUpper(), Name.ToUpper(), m_SI.Version().ToUpper() };
        }

        protected override int GetImageIndex()
        {
            if (IsLogged)
                return imgCompGreen;
            else
                if (IsPingable)
                    return  imgCompDefault;
                else
                    return imgCompRed;
        }

        protected override string GetToolTipText()
        {
            return String.Format("{0}\n{1}", Comment, m_SI.Version());
        }

        public bool IsPingable
        {
            get { return pingable; }
            set { pingable = value; }
        }

        public bool IsLogged
        {
            get { return logged; }
            set { logged = value; }
        }

        public IPEndPoint EndPoint
        {
            get { return ipendpoint; }
            set { ipendpoint = value; }
        }

        public override void CopyExtraFrom(TPanelItem Comp)
        {
            if (Comp == null) return;
            pingable = (Comp as TComputerItem).pingable;
            logged = (Comp as TComputerItem).logged;
            ipendpoint = (Comp as TComputerItem).ipendpoint;
        }
    }
}
