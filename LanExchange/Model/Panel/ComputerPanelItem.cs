using System;
using LanExchange.Sdk;
using LanExchange.Utils;

//using System.Net;

namespace LanExchange.Model.Panel
{
    public class ComputerPanelItem : PanelItemBase, IWmiComputer
    {
        private readonly ServerInfo m_SI;

        /// <summary>
        /// Constructor creates ComputerPanelItem from <see cref="ServerInfo"/> object.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public ComputerPanelItem(PanelItemBase parent, ServerInfo si) : base(parent)
        {
            if (si == null)
                throw new ArgumentNullException("si");
            m_SI = si;
            Comment = m_SI.Comment;
            IsPingable = true;
        }

        public override string Name
        {
            get { return m_SI.Name; }
            set { m_SI.Name = value; }
        }

        public string Comment { get; set; }

        public ServerInfo SI
        {
            get { return m_SI; }
        }

        public override IComparable this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return Name;
                    case 1:
                        return Comment;
                    case 2:
                        return m_SI.Version();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public override string ImageName
        {
            get
            {
                //if (IsLogged)
                //    return LanExchangeIcons.CompGreen;
                return IsPingable ? PanelImageNames.ComputerNormal : PanelImageNames.ComputerDisabled;
            }
        }

        public override string ToolTipText
        {
            get
            {
                return String.Format("{0}\n{1}\n{2}", Comment, m_SI.Version(), m_SI.GetTopicality());
            }
        }

        public bool IsPingable { get; set; }

        //private bool IsLogged { get; set; }

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

        public override int CountColumns
        {
            get { return 3; }
        }

        public override PanelColumnHeader CreateColumnHeader(int index)
        {
            //IPanelColumnHeader result = new ColumnHeaderEx();
            //result.SetVisible(true);
            //switch (index)
            //{
            //    case 0:
            //        result.Text = "Сетевое имя";
            //        break;
            //    case 1:
            //        result.Text = "Описание";
            //        break;
            //    case 2:
            //        result.Text = "Версия ОС";
            //        break;
            //}
            //return result;
            return null;
        }

        public override string ToString()
        {
            return @"\\" + base.ToString();
        }
    }
}
