using System;
using System.Net;

namespace LanExchange.Model
{
    public class ComputerPanelItem : PanelItem
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
