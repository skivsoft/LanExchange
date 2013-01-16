using System;
using LanExchange.Utils;
using LanExchange.WMI;
//using System.Net;

namespace LanExchange.Model
{
    public class ComputerPanelItem : AbstractPanelItem, IWMIComputer
    {
        private readonly ServerInfo m_SI;
        private string m_Name = String.Empty;
        private string m_Comment = String.Empty;

        /// <summary>
        /// Constructor creates ComputerPanelItem from <see cref="ServerInfo"/> object.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="si"></param>
        public ComputerPanelItem(ServerInfo si)
        {
            if (si == null)
                throw new ArgumentNullException("si");
            m_SI = si;
            m_Name = m_SI.Name;
            m_Comment = m_SI.Comment;
            IsPingable = true;
        }

        public override string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public override string Comment
        {
            get { return m_Comment; }
            set { m_Comment = value; }
        }

        public ServerInfo SI
        {
            get { return m_SI; }
        }

        public override string[] getStrings()
        {
            return new[] { Comment.ToUpper(), Name.ToUpper(), m_SI.Version().ToUpper() };
        }

        public override int ImageIndex
        {
            get
            {
                if (IsLogged)
                    return LanExchangeIcons.CompGreen;
                return IsPingable ? LanExchangeIcons.CompDefault : LanExchangeIcons.CompDisabled;
            }
        }

        public override string ToolTipText
        {
            get { return String.Format("{0}\n{1}", Comment, m_SI.Version()); }
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
