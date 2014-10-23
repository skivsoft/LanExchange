using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using LanExchange.SDK;

namespace LanExchange.Plugin.FileSystem
{
    public sealed class DrivePanelItem : PanelItemBase
    {
        private string m_Name;
        private ColumnDrive m_Drive;
        private DriveType m_DriveType;
        private ColumnSize m_TotalSize;
        private ColumnSize m_TotalFreeSpace;
        private string m_DriveFormat;

        public DrivePanelItem()
        {
        }

        [Localizable(false)]
        public DrivePanelItem(PanelItemBase parent, string name)
            : base(parent)
        {
            Name = name;
        }

        [XmlAttribute]
        [Localizable(false)]
        public override string Name
        {
            get { return m_Name; }
            set
            {
                var drive = new DriveInfo(value);
                m_Name = drive.RootDirectory.Name;
                PluginFileSystem.RegisterImageForFileName(m_Name);
                
                m_DriveType = drive.DriveType;
                var volumeLabel = string.Empty;
                if (drive.IsReady)
                {
                    volumeLabel = drive.VolumeLabel;
                    m_TotalSize = new ColumnSize(drive.TotalSize, true);
                    m_TotalFreeSpace = new ColumnSize(drive.TotalFreeSpace, true);
                    m_DriveFormat = drive.DriveFormat;
                }
                else
                {
                    m_TotalSize = ColumnSize.Zero;
                    m_TotalFreeSpace = ColumnSize.Zero;
                }
                if (string.IsNullOrEmpty(volumeLabel))
                    m_Drive = new ColumnDrive(Name, Name);
                else
                    m_Drive = new ColumnDrive(Name, string.Format(CultureInfo.CurrentCulture, "{0} ({1})", volumeLabel, Name));
            }
        }

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
                case 0: return m_Drive;
                case 1: return m_DriveType;
                case 2: return m_TotalSize;
                case 3: return m_TotalFreeSpace;
                case 4: return m_DriveFormat;
                default:
                    return base.GetValue(index);
            }
        }

        public override object Clone()
        {
            var result = new DrivePanelItem(Parent, Name);
            result.m_Drive = m_Drive;
            result.m_DriveType = m_DriveType;
            result.m_TotalSize = m_TotalSize;
            result.m_TotalFreeSpace = m_TotalFreeSpace;
            result.m_DriveFormat = m_DriveFormat;
            return result;
        }
    }
}