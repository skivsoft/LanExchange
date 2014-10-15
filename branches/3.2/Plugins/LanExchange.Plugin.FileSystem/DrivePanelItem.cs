using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using LanExchange.SDK;

namespace LanExchange.Plugin.FileSystem
{
    public sealed class DrivePanelItem : PanelItemBase
    {
        private DriveInfo m_Info;
        private string m_DisplayName;

        public DrivePanelItem()
        {
        }

        public DrivePanelItem(PanelItemBase parent, string name) : base(parent)
        {
            Name = name;
        }

        [Localizable(false)]
        [XmlIgnore]
        public DriveInfo Info
        {
            get { return m_Info; }
            set
            {
                m_Info = value;
                if (m_Info != null)
                {
                    var root = m_Info.RootDirectory.Name;
                    m_DisplayName = string.Format("{0} ({1})", m_Info.VolumeLabel, root.Substring(0, 2));
                }
            }
        }

        [XmlAttribute]
        public override string Name { get; set; }

        public override string FullName
        {
            get { return Name; }
        }

        public override string ImageName
        {
            get { return FullName; }
        }

        public override string ImageLegendText
        {
            get { return string.Empty; }
        }

        public override int CountColumns
        {
            get
            {
                return base.CountColumns + 4;
            }
        }

        public override IComparable GetValue(int index)
        {
            switch (index)
            {
                case 0:
                    return new ColumnDrive(Name, m_DisplayName);
                case 1:
                    return m_Info != null ? m_Info.DriveType.ToString() : string.Empty;
                case 2:
                    return m_Info != null ? new ColumnSize(m_Info.TotalSize) : ColumnSize.Zero;
                case 3:
                    return m_Info != null ? new ColumnSize(m_Info.TotalFreeSpace) : ColumnSize.Zero; 
                case 4:
                    return m_Info != null ? m_Info.DriveFormat : string.Empty;
                default:
                    return base.GetValue(index);
            }
        }

        public override object Clone()
        {
            var result = new DrivePanelItem(Parent, Name);
            result.Info = Info;
            return result;
        }
    }
}
