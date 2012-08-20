using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model.VO;
using System.IO;
using LanExchange.SDK.SDKModel.VO;

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

        public override ColumnVO[] GetColumns()
        {
            return new ColumnVO[] { 
                new ColumnVO("Имя", 100),
                new ColumnVO("Дата изменения", 100),
                new ColumnVO("Тип", 100),
                new ColumnVO("Размер", 100)
            };
        }

        public override void EnumObjects(string Path)
        {
            Objects.Add(new PanelItemVO("..", null));
            DirectoryInfo Dir = new DirectoryInfo(Path);
            FileSystemInfo[] Files = Dir.GetFileSystemInfos();
            foreach (FileSystemInfo Item in Files)
            {
                string sType = (Item.Attributes & FileAttributes.Directory) != 0 ? "Папка с файлами" : "Файл";
                Objects.Add(new PanelItemVO(Item.Name, Item));
            }
        }
    }
}
