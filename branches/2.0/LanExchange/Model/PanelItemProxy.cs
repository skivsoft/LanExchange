using System;
using System.Collections.Generic;
using System.Text;
using PureMVC.PurePatterns;
using PureMVC.PureInterfaces;
using LanExchange.Model.VO;
using BrightIdeasSoftware;


namespace LanExchange.Model
{
    /// <summary>
    /// Interface for object enum algorithm.
    /// </summary>
    /// <pattern>Strategy</pattern>>
    public interface IEnumObjectsStrategy
    {
        IList<PanelItemVO> EnumObjects(string path);
    }

    
    public abstract class PanelItemProxy : Proxy, IProxy
    {
        private IEnumObjectsStrategy m_EnumStrategy = null;

        public PanelItemProxy(string name, IEnumObjectsStrategy strategy)
            : base(name, new List<PanelItemVO>())
        {
            m_EnumStrategy = strategy;
        }

        public IEnumObjectsStrategy EnumStrategy
        {
            get { return m_EnumStrategy; }
            set { m_EnumStrategy = value; }
        }

        public IList<PanelItemVO> Objects
        {
            get { return (IList<PanelItemVO>)Data; }
            set
            {
                IList<PanelItemVO> Data = value;
            }
        }

        public virtual int NumObjects
        {
            get { return Objects.Count; }
        }

        public void EnumObjects(string path)
        {
            if (m_EnumStrategy != null)
            {
                IList<PanelItemVO> Result = m_EnumStrategy.EnumObjects(path);
                if (Result != null)
                {
                    Objects.Clear();
                    foreach (PanelItemVO Item in Result)
                    {
                        Objects.Add(Item);
                    }
                }
            }
        }

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
