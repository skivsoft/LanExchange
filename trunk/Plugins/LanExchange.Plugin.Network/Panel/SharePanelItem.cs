﻿using System;
using System.Windows.Forms;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network.Panel
{
    public class SharePanelItem : PanelItemBase//, IComparable<SharePanelItem>
    {
        public const string ID = "{B61F496F-3BED-4D28-A4A7-BFC81DC9C7FC}";
        
        private readonly ShareInfo m_SHI;

        /// <summary>
        /// Panel item for network shared resource.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public SharePanelItem(PanelItemBase parent, ShareInfo shi) : base(parent)
        {
            if (shi == null)
                throw new ArgumentNullException("shi");
            m_SHI = shi;
            Comment = m_SHI.Comment;
        }

        public SharePanelItem(PanelItemBase parent, string name) : base(parent)
        {
            m_SHI = new ShareInfo(new NetApi32.SHARE_INFO_1 {shi1_netname = name});
            Comment = string.Empty;
        }

        public override string ImageName
        {
            get
            {
                if (Name == s_DoubleDot)
                    return PanelImageNames.DoubleDot;
                if (SHI.IsPrinter)
                    return PanelImageNames.SharePrinter;
                return SHI.IsHidden ? PanelImageNames.ShareHidden : PanelImageNames.ShareNormal;
            }
        }

        public ShareInfo SHI
        {
            get { return m_SHI; }
        }

        public override int CountColumns
        {
            get { return 2; }
        }

        public override PanelColumnHeader CreateColumnHeader(int index)
        {
            PanelColumnHeader result = null;
            switch (index)
            {
                case 0:
                    result = new PanelColumnHeader("Resource name");
                    break;
                case 1:
                    result = new PanelColumnHeader("Description");
                    break;
            }
            return result;
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
                    default:
                        throw new ArgumentOutOfRangeException("index");
                }
            }
        }

        public string ComputerName
        {
            get
            {
                var comp = Parent as ComputerPanelItem;
                return comp != null ? comp.Name : String.Empty;
            }
        }

        public override string Name 
        { 
            get { return m_SHI.Name; }
            set { m_SHI.Name = value; }
        }

        public string Comment
        {
            get { return m_SHI.Comment; }
            set { m_SHI.Comment = value; }
        }

        public uint ShareType
        {
            get { return m_SHI.ShareType; }
            set { m_SHI.ShareType = value; }
        }
    }
}
