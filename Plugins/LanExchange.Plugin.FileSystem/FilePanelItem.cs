using System;
using System.IO;
using System.Xml.Serialization;
using LanExchange.SDK;

namespace LanExchange.Plugin.FileSystem
{
    public class FilePanelItem : PanelItemBase
    {

        private string fileName;
        private FileInfo fileInfo;

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
            get { return fileName; }
            set { 
                fileName = value;
                Name = Path.GetFileName(fileName);
                fileInfo = new FileInfo(fileName);
            }
        }

        public override string FullName
        {
            get { return fileName; }
        }

        public bool IsDirectory
        {
            get { return (fileInfo.Attributes & FileAttributes.Directory) != 0; }
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
                    return (fileInfo.Attributes & (FileAttributes.Hidden | FileAttributes.System)) != 0
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
                    return new ColumnSize(fileInfo.Length, false, string.Empty);
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