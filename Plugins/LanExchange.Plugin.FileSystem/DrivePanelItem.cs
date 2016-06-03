using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using LanExchange.Application.Interfaces;

namespace LanExchange.Plugin.FileSystem
{
    public sealed class DrivePanelItem : PanelItemBase
    {
        private DriveInfo driveInfo;
        private string displayName;

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
            get { return driveInfo; }
            set
            {
                driveInfo = value;
                if (driveInfo != null)
                {
                    var root = driveInfo.RootDirectory.Name;
                    displayName = string.Format("{0} ({1})", driveInfo.VolumeLabel, root.Substring(0, 2));
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
                    return new ColumnDrive(Name, displayName);
                case 1:
                    return driveInfo != null ? driveInfo.DriveType.ToString() : string.Empty;
                case 2:
                    return driveInfo != null ? new ColumnSize(driveInfo.TotalSize) : ColumnSize.Zero;
                case 3:
                    return driveInfo != null ? new ColumnSize(driveInfo.TotalFreeSpace) : ColumnSize.Zero; 
                case 4:
                    return driveInfo != null ? driveInfo.DriveFormat : string.Empty;
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
