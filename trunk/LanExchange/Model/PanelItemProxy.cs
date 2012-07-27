using System;
using System.Collections.Generic;
using System.Text;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using LanExchange.Model.VO;

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

        protected abstract void EnumObjects(string path);

        public void EnumObjectsSorted(string path)
        {
            EnumObjects(path);
            PanelItemComparer comparer = new PanelItemComparer();
            ((List<PanelItemVO>)Data).Sort(comparer);
        }

        public virtual ColumnVO[] GetColumns()
        {
            return new ColumnVO[] { new ColumnVO("", 100) };
        }
    }
}
