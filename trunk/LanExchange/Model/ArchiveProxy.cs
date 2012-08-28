﻿using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model.VO;
using LanExchange.Model;
using LanExchange;

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
                new ColumnVO(AppFacade.T("ColumnFileName"), 100),
                new ColumnVO(AppFacade.T("ColumnDateModified"), 100),
                new ColumnVO(AppFacade.T("ColumnType"), 100),
                new ColumnVO(AppFacade.T("ColumnSize"), 100)
            };
        }

        public override void EnumObjects(string Resource)
        {
            Objects.Add(new PanelItemVO("..", null));
            
        }
    }
}
