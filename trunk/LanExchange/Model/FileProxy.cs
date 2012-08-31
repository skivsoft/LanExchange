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

        public FileProxy(IEnumObjectsStrategy strategy)
            : base(NAME, strategy)
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
    }

    public class FileEnumStrategy : IEnumObjectsStrategy
    {
        public IList<PanelItemVO> EnumObjects(string path)
        {
            List<PanelItemVO> Result = new List<PanelItemVO>();
            DirectoryInfo Dir = new DirectoryInfo(path);
            FileSystemInfo[] Files = Dir.GetFileSystemInfos();
            foreach (FileSystemInfo Item in Files)
            {
                string sType = (Item.Attributes & FileAttributes.Directory) != 0 ? AppFacade.T("TypeFolder") : AppFacade.T("TypeFile");
                Result.Add(new PanelItemVO(Item.Name, Item));
            }
            return Result;
        }
    }
}
