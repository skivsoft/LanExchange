using System;
//using System.Net;

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

        public ComputerPanelItem(string computerName, object data)
        {
            m_Name = computerName;
            IsPingable = true;
            m_SI = data as ServerInfo;
            if (data != null && m_SI != null)
                m_Comment = m_SI.Comment;
        }

        //public static bool IsValidName(string name)
        //{
        //    return true;
        //}

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
            return new[] { Comment.ToUpper(), Name.ToUpper(), m_SI.Version().ToUpper() };
        }

        protected override int GetImageIndex()
        {
            if (IsLogged)
                return LanExchangeIcons.CompGreen;
            return IsPingable ? LanExchangeIcons.CompDefault : LanExchangeIcons.CompDisabled;
        }

        protected override string GetToolTipText()
        {
            return String.Format("{0}\n{1}", Comment, m_SI.Version());
        }

        public bool IsPingable { get; set; }

        private bool IsLogged { get; set; }

        //private IPEndPoint EndPoint { get; set; }

        //public override void CopyExtraFrom(PanelItem pitem)
        //{
        //    var comp = pitem as ComputerPanelItem;
        //    if (comp != null)
        //    {
        //        IsPingable = comp.IsPingable;
        //        IsLogged = comp.IsLogged;
        //        EndPoint = comp.EndPoint;
        //    }
        //}
    }
}
