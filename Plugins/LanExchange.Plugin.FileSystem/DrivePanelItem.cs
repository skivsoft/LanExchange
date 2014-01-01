using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LanExchange.SDK;

namespace LanExchange.Plugin.FileSystem
{
    public class DrivePanelItem : PanelItemBase
    {
        private DriveInfo m_Info;

        public DrivePanelItem()
        {
        }

        public DrivePanelItem(PanelItemBase parent, string letter) : base(parent)
        {
            DriveLetter = letter;
        }

        public string DriveLetter { get; private set; }

        public DriveInfo Info
        {
            get { return m_Info; }
            set
            {
                m_Info = value;
                if (m_Info != null)
                {
                    var root = m_Info.RootDirectory.Name;
                    Name = string.Format("{0} ({1})", m_Info.VolumeLabel, root.Substring(0, 2));
                }
            }
        }

        public override string Name { get; set; }

        public override string FullName
        {
            get { return m_Info.RootDirectory.Name; }
        }

        public override string ImageName
        {
            get { return string.Empty; }
        }

        public override string ImageLegendText
        {
            get { return string.Empty; }
        }

        public override IComparable GetValue(int index)
        {
            switch (index)
            {
                case 0:
                    return new ColumnDrive(DriveLetter, Name);
                default:
                    return base.GetValue(index);
            }
        }

        public override object Clone()
        {
            var result = new DrivePanelItem(Parent, DriveLetter);
            result.Info = Info;
            return result;
        }
    }
}
