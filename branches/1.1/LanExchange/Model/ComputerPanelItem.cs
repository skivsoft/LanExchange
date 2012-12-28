using System;
using System.Net;
using LanExchange.Utils;

namespace LanExchange.Model
{
    public class ComputerPanelItem : PanelItem
    {
        private string m_Name = String.Empty;
        private string m_Comment = String.Empty;
        private readonly ServerInfo m_SI;

        public ComputerPanelItem()
        {
            IsPingable = true;
        }

        public ComputerPanelItem(string computer_name, object data)
        {
            m_Name = computer_name;
            IsPingable = true;
            m_SI = data as ServerInfo;
            if (data != null)
                m_Comment = m_SI.Comment;
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

        public override string Comment
        {
            get { return m_Comment; }
            set { m_Comment = value; }
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
                return LanExchangeIcons.imgCompGreen;
            else
                if (IsPingable)
                    return LanExchangeIcons.imgCompDefault;
                else
                    return LanExchangeIcons.imgCompDisabled;
        }

        protected override string GetToolTipText()
        {
            return String.Format("{0}\n{1}", Comment, m_SI.Version());
        }

        public bool IsPingable { get; set; }

        public bool IsLogged { get; set; }

        public IPEndPoint EndPoint { get; set; }

        public override void CopyExtraFrom(PanelItem Comp)
        {
            if (Comp == null) return;
            IsPingable = (Comp as ComputerPanelItem).IsPingable;
            IsLogged = (Comp as ComputerPanelItem).IsLogged;
            EndPoint = (Comp as ComputerPanelItem).EndPoint;
        }
    }
}
