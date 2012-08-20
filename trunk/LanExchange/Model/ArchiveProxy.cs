using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model.VO;
using LanExchange.SDK.SDKModel.VO;

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

        public override ColumnVO[] GetColumns()
        {
            return new ColumnVO[] { 
                new ColumnVO("Имя", 100),
                new ColumnVO("Дата изменения", 100),
                new ColumnVO("Тип", 100),
                new ColumnVO("Размер", 100)
            };
        }

        public override void EnumObjects(string Resource)
        {
            Objects.Add(new PanelItemVO("..", null));
            
        }
    }
}
