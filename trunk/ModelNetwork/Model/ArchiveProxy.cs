using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.SDK.SDKModel.VO;
using LanExchange.SDK.SDKModel;
using LanExchange.SDK;

namespace ModelNetwork.Model
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

        public override ColumnVO[] GetColumns()
        {
            return new ColumnVO[] { 
                new ColumnVO(Globals.T("ColumnFileName"), 100),
                new ColumnVO(Globals.T("ColumnDateModified"), 100),
                new ColumnVO(Globals.T("ColumnType"), 100),
                new ColumnVO(Globals.T("ColumnSize"), 100)
            };
        }

        public override void EnumObjects(string Resource)
        {
            Objects.Add(new PanelItemVO("..", null));
            
        }
    }
}
