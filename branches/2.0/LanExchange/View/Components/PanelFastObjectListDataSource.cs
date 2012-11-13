using System;
using System.Collections.Generic;
using System.Text;
using BrightIdeasSoftware;
using System.Windows.Forms;
using System.Collections;
using LanExchange.Model.VO;

namespace LanExchange.View.Components
{
    /// <summary>
    /// Provide a data source for a FastObjectListView
    /// </summary>
    /// <remarks>
    /// This class isn't intended to be used directly, but it is left as a public
    /// class just in case someone wants to subclass it.
    /// </remarks>
    public class FastPanelItemDataSource : FastObjectListDataSource
    {
        /// <summary>
        /// Create a FastObjectListDataSource
        /// </summary>
        /// <param name="listView"></param>
        public FastPanelItemDataSource(FastObjectListView listView)
            : base(listView)
        {
        }

        #region IVirtualListDataSource Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="sortOrder"></param>
        public override void Sort(OLVColumn column, SortOrder sortOrder)
        {
            if (sortOrder != SortOrder.None)
            {
                PanelItemComparer comparer = new PanelItemComparer(column, sortOrder, this.listView.SecondarySortColumn, this.listView.SecondarySortOrder);
                base.fullObjectList.Sort(comparer);
                base.filteredObjectList.Sort(comparer);
            }
            this.RebuildIndexMap();
        }


        #endregion

    }

    /// <summary>
    /// This comparer can be used to sort a collection of model objects by a given column
    /// </summary>
    public class PanelItemComparer : IComparer, IComparer<object>
    {
        /// <summary>
        /// Create a model object comparer
        /// </summary>
        /// <param name="col"></param>
        /// <param name="order"></param>
        public PanelItemComparer(OLVColumn col, SortOrder order)
        {
            this.column = col;
            this.sortOrder = order;
        }

        /// <summary>
        /// Create a model object comparer with a secondary sorting column
        /// </summary>
        /// <param name="col"></param>
        /// <param name="order"></param>
        /// <param name="col2"></param>
        /// <param name="order2"></param>
        public PanelItemComparer(OLVColumn col, SortOrder order, OLVColumn col2, SortOrder order2)
            : this(col, order)
        {
            // There is no point in secondary sorting on the same column
            if (col != col2 && col2 != null && order2 != SortOrder.None)
                this.secondComparer = new PanelItemComparer(col2, order2);
        }

        /// <summary>
        /// Compare the two model objects
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            /*
            if (x is PanelItemVO)
            {
                try
                {
                    PanelItemVO Item1 = x as PanelItemVO;
                    if (Item1 != null)
                        if (Item1.Name != null)
                            if (Item1.Name != PanelItemVO.BACK)
                                return -1;
                }
                catch(Exception)
                {
                    x = y;
                }
            }
             */
            
            if (this.sortOrder == SortOrder.None)
                return 0;

            int result = 0;
            object x1 = this.column.GetValue(x);
            object y1 = this.column.GetValue(y);
            //bool bAnyIsBack = Item1.IsBackButton || Item2.IsBackButton;

            // Handle nulls. Null values come last
            bool xIsNull = (x1 == null || x1 == System.DBNull.Value);
            bool yIsNull = (y1 == null || y1 == System.DBNull.Value);
            if (xIsNull || yIsNull)
            {
                if (xIsNull && yIsNull)
                    result = 0;
                else
                    result = (xIsNull ? -1 : 1);
            }
            else
                result = this.CompareValues(x1, y1);

            if (this.sortOrder == SortOrder.Descending)
                result = 0 - result;

            // If the result was equality, use the secondary comparer to resolve it
            if (result == 0 && this.secondComparer != null)
                result = this.secondComparer.Compare(x, y);

            return result;
        }

        /// <summary>
        /// Compare the actual values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int CompareValues(object x, object y)
        {
            // Force case insensitive compares on strings
            String xStr = x as String;
            if (xStr != null)
                return String.Compare(xStr, (String)y, StringComparison.CurrentCultureIgnoreCase);
            else
            {
                IComparable comparable = x as IComparable;
                if (comparable != null)
                    return comparable.CompareTo(y);
                else
                    return 0;
            }
        }

        private OLVColumn column;
        private SortOrder sortOrder;
        private PanelItemComparer secondComparer;
    }
}
