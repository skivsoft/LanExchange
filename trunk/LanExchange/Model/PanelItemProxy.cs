using System;
using System.Collections.Generic;
using System.Text;
using PureMVC.PurePatterns;
using PureMVC.PureInterfaces;
using LanExchange.Model.VO;
using BrightIdeasSoftware;


namespace LanExchange.Model
{
    public abstract class PanelItemProxy : Proxy, IProxy
    {

        public PanelItemProxy(string name)
            : base(name, new List<PanelItemVO>())
        {
        }

        public IList<PanelItemVO> Objects
        {
            get { return (IList<PanelItemVO>)Data; }
        }

        public virtual int NumObjects
        {
            get { return Objects.Count; }
        }

        public abstract void EnumObjects(string path);

        public void Sort(IComparer<PanelItemVO> comparer)
        {
            ((List<PanelItemVO>)Data).Sort(comparer);
        }

        public virtual OLVColumn[] GetColumns()
        {
            OLVColumn column = new OLVColumn("", "Name");
            column.Width = 100;
            return new OLVColumn[] { column };
        }
    }
}
