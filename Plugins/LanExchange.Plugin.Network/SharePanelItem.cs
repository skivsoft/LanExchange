﻿using System;
using System.ComponentModel;
using LanExchange.Plugin.Network.NetApi;
using System.Xml.Serialization;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    [Serializable]
    public sealed class SharePanelItem : PanelItemBase//, IComparable<SharePanelItem>
    {
        private readonly ShareInfo m_SHI;

        public SharePanelItem()
        {
            m_SHI = new ShareInfo();
        }

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
            m_SHI = new ShareInfo(new SHARE_INFO_1 {netname = name});
            Comment = string.Empty;
        }

        public override string ImageName
        {
            get
            {
                if (SHI.IsPrinter)
                    return string.Empty;
                if (SHI.ShareType == 100)
                    return PanelImageNames.USER;
                return SHI.IsHidden ? PanelImageNames.FOLDER + PanelImageNames.HIDDEN_POSTFIX : PanelImageNames.FOLDER;
            }
        }

        public ShareInfo SHI
        {
            get { return m_SHI; }
        }

        public override int CountColumns
        {
            get { return base.CountColumns + 1; }
        }

        public override IComparable GetValue(int index)
        {
            switch (index)
            {
                case 1: return Comment;
                default:
                    return base.GetValue(index);
            }
        }

        public string ComputerName
        {
            get
            {
                var comp = Parent as ComputerPanelItem;
                return comp != null ? comp.Name : string.Empty;
            }
        }

        [XmlAttribute]
        public override string Name 
        { 
            get { return m_SHI.Name; }
            set { m_SHI.Name = value; }
        }

        [Localizable(false)]
        public override string FullName
        {
            get
            {
                return Parent == null ? base.FullName : Parent.FullName + @"\" + base.FullName;
            }
        }

        [XmlAttribute]
        public uint ShareType
        {
            get { return m_SHI.ShareType; }
            set { m_SHI.ShareType = value; }
        }

        [XmlAttribute]
        public string Comment
        {
            get { return m_SHI.Comment; }
            set { m_SHI.Comment = value; }
        }

        public override object Clone()
        {
            var result = new SharePanelItem(Parent, SHI);
            result.Name = Name;
            result.Comment = Comment;
            result.ShareType = ShareType;
            return result;
        }

        public override string ImageLegendText
        {
            get { return string.Empty; }
        }
    }
}
