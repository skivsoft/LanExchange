using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model.VO;
using LanExchange.Model;
using LanExchange;
using BrightIdeasSoftware;

namespace LanExchange.Model
{
    public class ArchiveProxy : PanelItemProxy
    {
        public new const string NAME = "ArchiveProxy";

        public ArchiveProxy()
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

        public override void EnumObjects(string Resource)
        {
            Objects.Clear();
            //Objects.Add(new PanelItemVO("..", null));
            
        }
    }
}
