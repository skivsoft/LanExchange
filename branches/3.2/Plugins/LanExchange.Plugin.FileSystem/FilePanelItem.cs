using System;
using System.IO;
using System.Xml.Serialization;
using LanExchange.SDK;

namespace LanExchange.Plugin.FileSystem
{
    public class FilePanelItem : PanelItemBase
    {

        private string m_FileName;
        private FileInfo m_FI;

        public FilePanelItem()
        {
        }

        public FilePanelItem(PanelItemBase parent, string fileName) : base(parent)
        {
            FileName = fileName;
        }

        [XmlAttribute]
        public override string Name { get; set; }

        public string FileName
        {
            get { return m_FileName; }
            set { 
                m_FileName = value;
                Name = Path.GetFileName(m_FileName);
                m_FI = new FileInfo(m_FileName);
            }
        }

        public override string FullName
        {
            get { return m_FileName; }
        }

        public bool IsDirectory
        {
            get { return (m_FI.Attributes & FileAttributes.Directory) != 0; }
        }

        public override int CountColumns
        {
            get { return base.CountColumns + 1; }
        }

        public override string ImageName
        {
            get
            {
                if (IsDirectory)
                    return (m_FI.Attributes & (FileAttributes.Hidden | FileAttributes.System)) != 0
                               ? PanelImageNames.FOLDER + PanelImageNames.HIDDEN_POSTFIX
                               : PanelImageNames.FOLDER;
                return FullName;
            }
        }

        public override IComparable GetValue(int index)
        {
            switch (index)
            {
                case 0: 
                    return new ColumnName(Name, IsDirectory);
                case 1:
                    if (IsDirectory)
                        return ColumnSize.Zero;
                    return new ColumnSize(m_FI.Length, false);
                default:
                    return base.GetValue(index);
            }
        }

        public override string ImageLegendText
        {
            get { return string.Empty; }
        }

        public override object Clone()
        {
            var result = new FilePanelItem(Parent, FileName);
            return result;
        }
    }
}