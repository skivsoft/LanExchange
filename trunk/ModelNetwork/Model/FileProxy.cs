using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LanExchange.Model.VO;
using LanExchange.Model;
using LanExchange;

namespace ModelNetwork.Model
{
    public class FileProxy : PanelItemProxy
    {
        public new const string NAME = "FileProxy";

        public FileProxy()
            : base(NAME)
        {

        }

        public override int NumObjects
        {
            get
            {
                return base.NumObjects - 1;
            }
        }

        public override ColumnVO[] GetColumns()
        {
            return new ColumnVO[] { 
                new ColumnVO(Globals.T("ColumnFileName"), 100),
                new ColumnVO(Globals.T("ColumnDateModified"), 100),
                new ColumnVO(Globals.T("ColumnType"), 100),
                new ColumnVO(Globals.T("ColumnSize"), 100)
            };
        }

        public override void EnumObjects(string Path)
        {
            Objects.Add(new PanelItemVO("..", null));
            DirectoryInfo Dir = new DirectoryInfo(Path);
            FileSystemInfo[] Files = Dir.GetFileSystemInfos();
            foreach (FileSystemInfo Item in Files)
            {
                string sType = (Item.Attributes & FileAttributes.Directory) != 0 ? Globals.T("TypeFolder") : Globals.T("TypeFile");
                Objects.Add(new PanelItemVO(Item.Name, Item));
            }
        }
    }
}
