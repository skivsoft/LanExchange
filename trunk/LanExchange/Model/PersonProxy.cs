﻿using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model;
using LanExchange.Model.VO;
using LanExchange;

namespace LanExchange.Model
{
    public class PersonProxy : PanelItemProxy
    {
        public new const string NAME = "PersonProxy";

        public PersonProxy()
            : base(NAME)
        {
        }

        public override ColumnVO[] GetColumns()
        {
            return new ColumnVO[] { 
                new ColumnVO(AppFacade.T("ColumnPersonName"), 100),
                new ColumnVO(AppFacade.T("ColumnPersonPosition"), 100),
                new ColumnVO(AppFacade.T("ColumnPersonSubdiv"), 100)
            };
        }

        public override void EnumObjects(string path)
        {
            Objects.Add(new PanelItemVO("IVANOV", null));
        }
    }
}
