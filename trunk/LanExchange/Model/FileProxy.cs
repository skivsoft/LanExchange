using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LanExchange.Model.VO;
using LanExchange.Model;
using LanExchange;
using BrightIdeasSoftware;

namespace LanExchange.Model
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

        public override OLVColumn[] GetColumns()
        {
            OLVColumn[] Result = new OLVColumn[] { 
                new OLVColumn(AppFacade.T("ColumnFileName"), "Name"),
                new OLVColumn(AppFacade.T("ColumnDateModified"), "DateModified"),
                new OLVColumn(AppFacade.T("ColumnType"), "FileType"),
                new OLVColumn(AppFacade.T("ColumnSize"), "FileSize")
            };
            return Result;
        }

        public override void EnumObjects(string Path)
        {
            Objects.Clear();
            Objects.Add(new PanelItemVO("..", null));
            DirectoryInfo Dir = new DirectoryInfo(Path);
            FileSystemInfo[] Files = Dir.GetFileSystemInfos();
            foreach (FileSystemInfo Item in Files)
            {
                string sType = (Item.Attributes & FileAttributes.Directory) != 0 ? AppFacade.T("TypeFolder") : AppFacade.T("TypeFile");
                Objects.Add(new PanelItemVO(Item.Name, Item));
            }
        }
    }
}
