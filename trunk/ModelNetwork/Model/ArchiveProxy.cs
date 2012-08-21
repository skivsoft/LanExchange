using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.SDK.SDKModel.VO;
using ModelNetwork.Properties;
using LanExchange.SDK.SDKModel;

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
                new ColumnVO(Resources.ColumnFileName, 100),
                new ColumnVO(Resources.ColumnDateModified, 100),
                new ColumnVO(Resources.ColumnType, 100),
                new ColumnVO(Resources.ColumnSize, 100)
            };
        }

        public override void EnumObjects(string Resource)
        {
            Objects.Add(new PanelItemVO("..", null));
            
        }
    }
}
